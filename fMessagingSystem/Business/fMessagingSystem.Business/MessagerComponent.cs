using fMessagingSystem.Data;
using fMessagingSystem.Entities;
using fCommon.Utility;

using GsmComm.GsmCommunication;
using GsmComm.PduConverter;

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq; 
using System.Threading;
using log4net;
using System.Configuration;

namespace fMessagingSystem.Business
{
    public class MessagerComponent
    {
        private bool EnableLog = true;
        ILog log;
        private GsmCommMain comm;

        public MessagerComponent(GsmCommMain Modem)
        {
            if (Modem == null) throw new ArgumentNullException("Modem", "Modem cannot be null");
            comm = Modem;

            log4net.Config.BasicConfigurator.Configure();
            log = log4net.LogManager.GetLogger(typeof(MessagerComponent));
        }
        private bool IsModemConnected()
        {
            return comm.IsConnected();
        }
        private bool IsPortOpen()
        {
            return comm.IsOpen();
        }
        private IEnumerable<string> SplitSmsIntoEqualChunks(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
        }

        #region SMS
        public void SendSMS(InformMessage message)
        {
            SendSMS(message.Body, message.AddressTo);
        }
        public void SendSMS(string message, string Telno)
        {
            if (EnableLog) log.Info(string.Format("sending message: {0} on modem at port[{1}]", message, comm.PortNumber));
            int smsCharSize = Config.GetInt("smsCharSize");
            if (message.Length > smsCharSize)
            {
                List<string> msgs = SplitSmsIntoEqualChunks(message, smsCharSize).ToList();
                foreach (var msg in msgs)
                {
                    SendSMSNow(msg, Telno);
                }
            }
            else
            {
                SendSMSNow(message, Telno);
            }

        }
        public void SendSMSNow(string message, string Telno)
        {
            if (comm != null)
                comm.SendMessage(new SmsSubmitPdu(message, Telno));
        }
        public void SendSMS(SMSMessage m)
        { 
            SendSMS(m.body.ToString(),m.addressTo);
        }
        #endregion

        #region Database
        public void SendAllDBMessages()
        {
            SendAllDBSMS();
            SendAllDBEmails();
        }
        public void SendAllDBSMS()
        {

            InformDAC informDac = new InformDAC();
            List<fMessagingSystem.Entities.InformMessage> _sms = new List<fMessagingSystem.Entities.InformMessage>();

            //get all new messages from db
            var _smsquery = from sms in informDac.Select()
                            where sms.Status == "New".ToUpper()
                            //where messages.Contains( sms.MessageType)
                            where sms.MessageInformType == "S".ToUpper()
                            select sms;
            _sms = _smsquery.ToList();

            if (comm.IsConnected())
            {
                foreach (var message in _sms)
                {


                    if (EnableLog) log.Info(string.Format("message sent: {0} ", message.Body));

                    //update message status to processed
                    message.Status = "Processed".ToUpper();

                    informDac.UpdateById(message);
                    if (EnableLog) log.Info(string.Format("updated status to processed message: {0} ", message.Body));
                }
                //Disconnect
                comm.Close();
            }
        }
        public void SendAllDBEmails()
        {
            InformDAC informDac = new InformDAC();
            List<fMessagingSystem.Entities.InformMessage> _emails = new List<fMessagingSystem.Entities.InformMessage>();
            EmailComponent ec = new EmailComponent();

            //get all new messages from db
            var _emailquery = from sms in informDac.Select()
                              where sms.Status == "New".ToUpper()
                              //where messages.Contains( sms.MessageType)
                              where sms.MessageInformType == "E".ToUpper()
                              select sms;
            _emails = _emailquery.ToList();

            foreach (var message in _emails)
            {
                SendEmailMessage(message.AddressTo, message.Subject, message.Body);

                //update message status to processed
                message.Status = "Processed".ToUpper();
                informDac.UpdateById(message);
            }
        }

        public void WriteMessageToDB(string MessageType, string addressTo, string message)
        {
            string addressFrom = Config.GetString("FANIKIWAMESSAGESTELNO");
            WriteMessageToDB(MessageType, addressFrom, addressTo, message);
        }
        public void WriteMessageToDB(string MessageType, string addressFrom, string addressTo, string message)
        {
            InformMessage infoMsg = new InformMessage();
            infoMsg.AddressFrom = addressFrom;
            infoMsg.AddressTo = addressTo;
            infoMsg.Body = message;
            infoMsg.MessageType = MessageType;
            infoMsg.Status = "NEW";
            infoMsg.MessageDate = DateTime.Today;

            WriteMessageToDB(infoMsg);

        }
        public void WriteMessageToDB(InformMessage InformMessage)
        {
            InformDAC infoDac = new InformDAC();
            infoDac.Create(InformMessage);
        }
        public void WriteMessageToDB(SMSMessage m)
        {
            InformMessage infoMsg = new InformMessage();
            infoMsg.AddressFrom = m.addressFrom;
            infoMsg.AddressTo = m.addressTo;
            infoMsg.Body = m.body.ToString();
            infoMsg.MessageType = "S"; //sms
            infoMsg.Status = "NEW";
            infoMsg.MessageDate = DateTime.Today;

            WriteMessageToDB(infoMsg);

        }
        public void WriteMessageToDB(EmailMessage m)
        {
            InformMessage infoMsg = new InformMessage();
            infoMsg.AddressFrom = m.addressFrom;
            infoMsg.AddressTo = m.addressTo;
            infoMsg.Body = m.body.ToString();
            infoMsg.MessageType = "E"; //sms
            infoMsg.Status = "NEW";
            infoMsg.MessageDate = DateTime.Today;

            WriteMessageToDB(infoMsg);

        }
        #endregion

        #region Email

        public void SendEmailMessage(string addressTo, string subject, string Body)
        {

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            MailAddress _addressTo = new MailAddress(addressTo);
            message.To.Add(_addressTo);
            message.Subject = subject;
            message.From = new System.Net.Mail.MailAddress(Config.GetString("AddressFrom"));
            message.Body = Body;
            //message.ReplyToList.Add(addressFrom); 
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.UseDefaultCredentials = false;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = Config.GetString("UserName");
            NetworkCred.Password = Config.GetString("Password");
            smtp.Credentials = NetworkCred;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Host = Config.GetString("Host");
            smtp.EnableSsl =  Config.GetBool("EnableSsl");
            smtp.Port = Config.GetInt("Port");
            smtp.Timeout =  Config.GetInt("Timeout");
            smtp.Send(message);

            Thread.Sleep(Config.GetInt("ThreadSleep"));
            smtp.Send(message);

        }
        public void SendEmail(EmailMessage m)
        {
            EmailComponent emailChannel = new EmailComponent();
            emailChannel.SendMessage(m);
        }
        #endregion

        #region Message - Both Email and SMs

        public void SendListMessage(List<Message> ms)
        {
            foreach (var m in ms)
            {
                SendMessage(m);
            }
        }
        public void SendMessage(Message m)
        {
            if (m is SMSMessage)
            {
                this.SendSMS((SMSMessage)m);
            }
            else if (m is EmailMessage)
            {
                this.SendEmail((EmailMessage)m);
            }
        }

        public void Inform(Message m) { SendMessage(m); }
        public void InformDB(InformMessage m) { WriteMessageToDB(m); }
        #endregion

    }
}
