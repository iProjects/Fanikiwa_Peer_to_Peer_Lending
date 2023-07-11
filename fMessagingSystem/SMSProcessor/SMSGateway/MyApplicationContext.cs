using fCommon.Utility;
using fMessagingSystem.Business;
using fMessagingSystem.Data;
using fMessagingSystem.Entities;
using fMessagingSystem.Framework;
using fMessagingSystem.Framework.ExceptionHandlers;
using fMessagingSystem.Services;
using fPeerLending.MessageProcessor;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using log4net;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Windows.Forms;


namespace SMSGateway
{
    public class MyApplicationContext : ApplicationContext
    {
        //Component declarations
        private NotifyIcon TrayIcon;

        //Comms
        private Int16 ReceiveComm_Port = 3;
        private Int32 ReceiveComm_BaudRate = 460800;
        private Int32 ReceiveComm_TimeOut = 5000;
        public GsmCommMain receivingModem;

        private Int16 sendingComm_Port = 4;
        private Int32 sendingComm_BaudRate = 460800;
        private Int32 sendingComm_TimeOut = 5000;
        public GsmCommMain sendingModem;
        public bool USEONEMODEMFORSENDANDRECIEVE = true;

        public bool IsRecievingModemConnected = false;
        public bool IsSendingModemConnected = false;
        public bool EnableLog = true;
        public bool SendToCloud = true;

        ILog log;
        System.Windows.Forms.Timer timer;
        private ServiceHost Host;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public MyApplicationContext(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            try
            {
                _notificationmessageEventname = notificationmessageEventname;

                InitializeComponent();

                //main_form main_form = new main_form();
                //main_form.Show();

                timer = new System.Windows.Forms.Timer();
                timer.Interval = Config.GetInt("SMSReadSecs") * 1000;
                timer.Tick += timer_Tick;
                timer.Start();

                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

                USEONEMODEMFORSENDANDRECIEVE = Config.GetBool("USEONEMODEMFORSENDANDRECIEVE", USEONEMODEMFORSENDANDRECIEVE);
                SendToCloud = Config.GetBool("SendToCloud");
                TrayIcon.Visible = true;

                //host the service and
                //pass the receiving and sending modems to the service 
                MessagingService ms = new MessagingService(sendingModem, receivingModem);
                Host = new ServiceHost(ms);
                Host.Open();

                showBalloonTipText("Timer Tick", "Checking messages...");

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                Utils.ShowError(ex);
            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                ReadAndProcessMessagesFromModem();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                Utils.ShowError(ex);
            }
        }

        private void InitializeComponent()
        {
            try
            {
                TrayIcon = new NotifyIcon();

                log4net.Config.BasicConfigurator.Configure();
                log = log4net.LogManager.GetLogger(typeof(MyApplicationContext));

                TrayIcon.BalloonTipIcon = ToolTipIcon.Info;
                TrayIcon.BalloonTipText = "Right click to see the menu";
                TrayIcon.BalloonTipTitle = "SMS Gateway";
                TrayIcon.Text = "SMS Gateway - for sim communications";

                //The icon is added to the project resources. Here I assume that the name of the file is 'TrayIcon.ico'
                TrayIcon.Icon = Properties.Resources.splogo;

                //Optional - handle doubleclicks on the icon:
                TrayIcon.DoubleClick += TrayIcon_DoubleClick;

                //Optional - Add a context menu to the TrayIcon:
                TrayIcon.ContextMenuStrip = new ContextMenuStrip();
                TrayIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Show &Connection", showConnectionItem_Click));
                TrayIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&Help/About", showHelpItem_Click));
                TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                TrayIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&Exit", exitItem_Click));

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        public void Connect()
        {
            try
            {
                //connect onother modem for sending - always connected
                ConnectSendingModem();

                //connect onother modem for receiving
                ConnectRecievingModem();

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                //the error message will not stop the app from running. you may connect via the connection form
            }
        }

        private void ConnectSendingModem()
        {
            try
            {
                //Sending Modem config
                sendingComm_Port = short.Parse(Config.GetInt("SComm_Port").ToString());
                sendingComm_BaudRate = Config.GetInt("SComm_BaudRate");
                sendingComm_TimeOut = Config.GetInt("SComm_TimeOut");

                sendingModem = new GsmCommMain(sendingComm_Port, sendingComm_BaudRate, sendingComm_TimeOut);
                sendingModem.PhoneConnected += sendingModem_PhoneConnected;
                sendingModem.LoglineAdded += sendingModem_LoglineAdded;
                sendingModem.MessageReceived += sendingModem_MessageReceived;
                sendingModem.PhoneDisconnected += sendingModem_PhoneDisconnected;
                sendingModem.Open();
                IsSendingModemConnected = true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                //the error message will not stop the app from running. you may connect via the connection form
            }
        }
        private void ConnectRecievingModem()
        {
            try
            {
                //Receiving Modem config
                ReceiveComm_Port = short.Parse(Config.GetInt("RComm_Port").ToString());
                ReceiveComm_BaudRate = Config.GetInt("RComm_BaudRate");
                ReceiveComm_TimeOut = Config.GetInt("RComm_TimeOut");

                receivingModem = new GsmCommMain(ReceiveComm_Port, ReceiveComm_BaudRate, ReceiveComm_TimeOut);
                receivingModem.PhoneConnected += receivingModem_PhoneConnected;
                receivingModem.MessageReceived += receivingModem_MessageReceived;
                // receivingModem.ReceiveComplete += receivingModem_ReceiveComplete;
                receivingModem.LoglineAdded += receivingModem_LoglineAdded;
                receivingModem.PhoneDisconnected += receivingModem_PhoneDisconnected;
                receivingModem.Open();
                IsRecievingModemConnected = true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                //the error message will not stop the app from running. you may connect via the connection form
            }
        }

        //processing
        public int ReadAndProcessMessagesFromModem()
        {
            try
            {
                int rcount = ReadAndProcessMessagesFromModem("R", receivingModem);
                int scount = ReadAndProcessMessagesFromModem("S", sendingModem);
                return rcount + scount;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                Utils.ShowError(ex);
                return 0;
            }
        }
        private int ReadAndProcessMessagesFromModem(string symbol, GsmCommMain modem)
        {
            int count = 0;
            try
            {
                if (modem.IsConnected() == true)
                {
                    //STEP 1: query sendingModem for messages

                    DecodedShortMessage[] _ModemSms = modem.ReadMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

                    count = _ModemSms.Count();

                    if (EnableLog)
                    {
                        if (count > 0)
                            log.Info(string.Format("processing {0} messages", count));
                    }

                    //STEP 2: convert to FanikiwaMessage
                    FanikiwaMessage fmessage;

                    foreach (DecodedShortMessage _Modem_Message in _ModemSms.OrderBy(i => i.Data.GetTimestamp()))
                    {
                        if (_Modem_Message.Data is SmsDeliverPdu)
                        {
                            // Received message
                            SmsDeliverPdu _Sms_Data = (SmsDeliverPdu)_Modem_Message.Data;


                            //create message from PDU
                            fmessage = FanikiwaMessageFactory.CreateMessage(

                                _Sms_Data.OriginatingAddress, //.TrimStart(new Char[] { '+' }),

                                DateTime.Parse(_Sms_Data.SCTimestamp.ToString()),

                                _Sms_Data.UserDataText);

                            //STEP 3: save the message in db
                            if (fmessage != null)
                            {

                                if (EnableLog)

                                    log.Info(string.Format("saving message [{0}]", fmessage.Body));

                                SaveFanikiwaMessage(fmessage);

                                if (EnableLog)

                                    log.Info(string.Format("saved message [{0}]", fmessage.Body));
                            }

                            //STEP 4: delete message from sim
                            if (EnableLog)

                                log.Info(string.Format("deleting message [{0}] from sim", fmessage.Body));

                            DeleteArchivedSms(symbol, modem, _Modem_Message.Index, _Modem_Message.Storage);

                            if (EnableLog)

                                log.Info(string.Format("deleted message [{0}] from sim", fmessage.Body));

                            //STEP 5: make an asynchronous call to processor

                            if (EnableLog)

                                log.Info(string.Format("Calling processor"));

                            //ProcessFanikiwaMessage(fmessage);
                            SMSMessage msg = new SMSMessage();
                            msg.addressFrom = _Sms_Data.OriginatingAddress;
                            msg.messageDate = DateTime.Parse(_Sms_Data.SCTimestamp.ToString());
                            msg.body = _Sms_Data.UserDataText;

                            if (SendToCloud)
                            {
                                string resp = WriteToCloud(fmessage, msg);
                                string addr = msg.addressFrom.StartsWith("+") ? msg.addressFrom.TrimStart('+') : msg.addressFrom;
                                log.Info(string.Format("Response from cloud = [" + resp + "]"));
                                if (addr.IsNumeric())
                                {
                                    SendSMS(resp, msg.addressFrom);
                                }
                                else
                                {
                                    log.Info(string.Format("Cannot send SMS [" + resp + "] to [" + msg.addressFrom + "]"));
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (EnableLog)

                    log.Error("An error occurred while processing messages from modem", ex);
            }

            return count;
        }

        private void showBalloonTipText(string title, string msg)
        {
            //save
            string oldt = TrayIcon.BalloonTipText;
            string oldtt = TrayIcon.BalloonTipTitle;

            //new text
            TrayIcon.BalloonTipText = msg;
            oldtt = TrayIcon.BalloonTipTitle = title;

            TrayIcon.ShowBalloonTip(10000);

            //revert to old text
            TrayIcon.BalloonTipText = oldt;
            oldtt = TrayIcon.BalloonTipTitle = oldtt;

        }
        void sendingModem_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            showBalloonTipText("Message Received", "A new message has been received from sending modem");
        }

        #region Recieving Modem Event Handlers
        // void receivingModem_ReceiveComplete(object sender, ProgressEventArgs e)
        //{
        //    //Read all messages
        //    log.Info("Retrieving messages");
        //     ReadAndProcessMessagesFromModem();

        //    //var retrieveTask = Task<int>.Factory.StartNew(() => ReadAndProcessMessagesFromModem());
        //    //await retrieveTask;
        //    //log.Info(retrieveTask.Result.ToString() + " Messages processed");

        //}

        void receivingModem_PhoneDisconnected(object sender, EventArgs e)
        {
            IsRecievingModemConnected = false;
            log.Fatal("Receiving sendingModem disconnected");

        }

        void receivingModem_LoglineAdded(object sender, string text)
        {
            //log.Info(text);
        }

        void receivingModem_PhoneConnected(object sender, EventArgs e)
        {
            IsRecievingModemConnected = true;
            log.Info("Receiving sendingModem connected on port [" + receivingModem.PortNumber + "]");
        }

        void receivingModem_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //log.Info("Message received");
            showBalloonTipText("Message Received", "A new message has been received from recievieng modem");
        }
        #endregion

        #region Sending Modem Event Handlers
        void sendingModem_LoglineAdded(object sender, string text)
        {
            //log.Info(text);
        }
        void sendingModem_PhoneDisconnected(object sender, EventArgs e)
        {
            IsSendingModemConnected = false;
            log.Fatal("Sending sendingModem disconnected");
        }
        void sendingModem_PhoneConnected(object sender, EventArgs e)
        {
            IsSendingModemConnected = true;
            log.Info("Sending Modem connected on port [" + sendingModem.PortNumber + "]");
        }
        #endregion

        private void Disconnect()
        {
            DisconnectSendingModem();
            DisconnectRecievingModem();//now disconnect the recieving modem

        }
        private void DisconnectRecievingModem()
        {
            if (receivingModem != null)
            {
                receivingModem.PhoneConnected -= receivingModem_PhoneConnected;
                receivingModem.MessageReceived -= receivingModem_MessageReceived;
                //receivingModem.ReceiveComplete -= receivingModem_ReceiveComplete;
                receivingModem.LoglineAdded -= receivingModem_LoglineAdded;
            }

            if (receivingModem != null)
            {
                // Close connection to phone
                if (receivingModem != null && receivingModem.IsOpen())
                    receivingModem.Close();

                receivingModem = null;
            }

            IsRecievingModemConnected = false;

        }
        private void DisconnectSendingModem()
        {
            if (sendingModem != null)
            {
                sendingModem.PhoneConnected -= sendingModem_PhoneConnected;
                sendingModem.MessageReceived -= sendingModem_MessageReceived;
                sendingModem.LoglineAdded -= sendingModem_LoglineAdded;
            }

            if (sendingModem != null)
            {
                // Close connection to phone
                if (sendingModem != null && sendingModem.IsOpen())
                    sendingModem.Close();

                sendingModem = null;
            }

            IsSendingModemConnected = false;

        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            TrayIcon.Visible = false;

            //close service
            Host.Close();
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            //Here you can do stuff if the tray icon is doubleclicked
            TrayIcon.ShowBalloonTip(10000);
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to close me?",
                                "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ShowHelpForm()
        {
            help_form f = new help_form();
            f.Show();
        }
        private void ShowConnectionForm()
        {
            settings_form f = new settings_form();
            f.Show();
        }

        # region Form event handlers
        // attach to context menu items
        private void showHelpItem_Click(object sender, EventArgs e) { ShowHelpForm(); }
        private void showConnectionItem_Click(object sender, EventArgs e) { ShowConnectionForm(); }
        /// <summary>
        /// When the exit menu item is clicked, make a call to terminate the ApplicationContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit application? This will stop all communications",
                                "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ExitThread();
            }

        }

        /// <summary>
        /// If we are presently showing a form, clean it up.
        /// </summary>
        protected override void ExitThreadCore()
        {
            // before we exit, let forms clean themselves up.
            //if (introForm != null) { introForm.Close(); }
            //if (detailsForm != null) { detailsForm.Close(); }

            Disconnect();

            TrayIcon.Visible = false; // should remove lingering tray icon
            base.ExitThreadCore();
        }

        # endregion event handlers

        # region support methods

        private ToolStripMenuItem ToolStripMenuItemWithHandler(
            string displayText, int enabledCount, int disabledCount, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null) { item.Click += eventHandler; }

            item.Image = (enabledCount > 0 && disabledCount > 0) ? Properties.Resources.signal_yellow
                         : (enabledCount > 0) ? Properties.Resources.signal_green
                         : (disabledCount > 0) ? Properties.Resources.signal_red
                         : null;
            item.ToolTipText = (enabledCount > 0 && disabledCount > 0) ?
                                                 string.Format("{0} enabled, {1} disabled", enabledCount, disabledCount)
                         : (enabledCount > 0) ? string.Format("{0} enabled", enabledCount)
                         : (disabledCount > 0) ? string.Format("{0} disabled", disabledCount)
                         : "";
            return item;
        }

        public ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, EventHandler eventHandler)
        {
            return ToolStripMenuItemWithHandler(displayText, 0, 0, eventHandler);
        }

        # endregion support methods

        #region Business
        //receiving and saving messages 
        public void SaveFanikiwaMessage(FanikiwaMessage fmessage)
        {
            /* Create a DB with the following columns
             * 
             *  private int MemberId;
                private string SenderTelno;
             *  private string Body;
                private DateTime MessageDate;
             *  private string MessageType;
                private string Status;
            *   private int AccountId;
                private DateTime StartDate;
                private DateTime EndDate;
             *  private decimal Amount;
             * 
             */

            FanikiwaDBMessage dbmessage = ConvertToDBMessage(fmessage);
            FanikiwaMessageDAC fDAC = new FanikiwaMessageDAC();

            if (EnableLog)
                log.Info(string.Format("creating message [{0}] in db", dbmessage.Body));

            fDAC.Create(dbmessage);

            if (EnableLog)
                log.Info(string.Format("created message [{0}] in db", dbmessage.Body));
        }
        public FanikiwaDBMessage ConvertToDBMessage(FanikiwaMessage fmessage)
        {
            FanikiwaDBMessage dbMsg = new FanikiwaDBMessage();

            //fillup  properties in abstract class
            dbMsg.Body = fmessage.Body;
            dbMsg.MessageType = (int)fmessage.FanikiwaMessageType;
            dbMsg.MemberId = fmessage.MemberId;
            dbMsg.SenderTelno = fmessage.SenderTelno;
            dbMsg.MessageDate = fmessage.MessageDate;
            if (dbMsg.StartDate.Date == DateTime.Parse("01/01/0001"))
            {
                dbMsg.StartDate = DateTime.Now.Date;
            }
            if (dbMsg.EndDate.Date == DateTime.Parse("01/01/0001"))
            {
                dbMsg.EndDate = DateTime.Now.Date;
            }
            if (dbMsg.ST_StartDate.Date == DateTime.Parse("01/01/0001"))
            {
                dbMsg.ST_StartDate = DateTime.Now.Date;
            }
            if (dbMsg.ST_EndDate.Date == DateTime.Parse("01/01/0001"))
            {
                dbMsg.ST_EndDate = DateTime.Now.Date;
            }
            dbMsg.Status = fmessage.Status;

            //fillup properties in inherited classes according to message type
            if (fmessage is HelpMessage)
            {
                HelpMessage mm = (HelpMessage)fmessage;
                if (!string.IsNullOrEmpty(mm.HelpCommand))
                {
                    dbMsg.HM_Param = mm.HelpCommand;
                }
            }
            else if (fmessage is MpesaDepositMessage)
            {
                MpesaDepositMessage mPesa = (MpesaDepositMessage)fmessage;
                dbMsg.CustomerTelno = mPesa.CustomerTelno;
                dbMsg.SenderTelno = mPesa.CustomerTelno;
                dbMsg.AccountId = mPesa.AccountId;
                dbMsg.Amount = mPesa.Amount;
                dbMsg.MpesaSentDate = mPesa.SentDate;
                dbMsg.MpesaRef = mPesa.Mpesaref;
                dbMsg.MpesaBal = mPesa.Bal;
            }
            else if (fmessage is BalanceEnquiryMessage)
            {
                BalanceEnquiryMessage bm = (BalanceEnquiryMessage)fmessage;
                dbMsg.AccountId = bm.AccountId.ToString();
                dbMsg.BE_AccLabel = bm.AccountLabel;
                dbMsg.Pwd = bm.Pwd;
            }
            else if (fmessage is StatementEnquiryMessage)
            {
                StatementEnquiryMessage sm = (StatementEnquiryMessage)fmessage;
                dbMsg.AccountId = sm.AccountId.ToString();
                dbMsg.StartDate = sm.StartDate;
                dbMsg.EndDate = sm.EndDate;
                dbMsg.ST_StartDate = sm.StartDate;
                dbMsg.ST_EndDate = sm.EndDate;
                dbMsg.Pwd = sm.Pwd;
            }
            else if (fmessage is EarlyLoanRepaymentMessage)
            {
                EarlyLoanRepaymentMessage em = (EarlyLoanRepaymentMessage)fmessage;
                dbMsg.OfferId = em.OfferId;
                dbMsg.Amount = em.Amount;
                dbMsg.Pwd = em.Pwd;
            }
            else if (fmessage is RegisterMessage)
            {
                RegisterMessage rm = (RegisterMessage)fmessage;
                dbMsg.Email = rm.Email;
                dbMsg.Pwd = rm.Pwd;
                dbMsg.NationalID = rm.NationalID;
                dbMsg.NotificationMethod = rm.NotificationMethod;
            }
            else if (fmessage is ErrorMessage)
            {
                ErrorMessage rm = (ErrorMessage)fmessage;
                dbMsg.Exception = rm.Error_Message;
            }
            else if (fmessage is MakeLendOfferMessage)
            {
                MakeLendOfferMessage mlo = (MakeLendOfferMessage)fmessage;
                dbMsg.Amount = mlo.Amount;
                dbMsg.MO_Term = mlo.Term;
                dbMsg.MO_Interest = mlo.InterestRate;
                dbMsg.Pwd = mlo.Pwd;
            }
            else if (fmessage is MakeBorrowOfferMessage)
            {
                MakeBorrowOfferMessage mbo = (MakeBorrowOfferMessage)fmessage;
                dbMsg.Amount = mbo.Amount;
                dbMsg.MO_Term = mbo.Term;
                dbMsg.MO_Interest = mbo.InterestRate;
                dbMsg.Pwd = mbo.Pwd;
            }
            else if (fmessage is AcceptLendOfferMessage)
            {
                AcceptLendOfferMessage alo = (AcceptLendOfferMessage)fmessage;
                dbMsg.OfferId = alo.OfferId;
                dbMsg.Amount = alo.Amount;
                dbMsg.Pwd = alo.Pwd;
            }
            else if (fmessage is AcceptBorrowOfferMessage)
            {
                AcceptBorrowOfferMessage abo = (AcceptBorrowOfferMessage)fmessage;
                dbMsg.OfferId = abo.OfferId;
                dbMsg.Amount = abo.Amount;
                dbMsg.Pwd = abo.Pwd;
            }
            else if (fmessage is LendOffersMessage)
            {
                LendOffersMessage lo = (LendOffersMessage)fmessage;
                dbMsg.Pwd = lo.Pwd;
            }
            else if (fmessage is BorrowOffersMessage)
            {
                BorrowOffersMessage bo = (BorrowOffersMessage)fmessage;
                dbMsg.Pwd = bo.Pwd;
            }
            else if (fmessage is ChangePinMessage)
            {
                ChangePinMessage cpm = (ChangePinMessage)fmessage;
                dbMsg.Pwd = cpm.OldPassword;
                dbMsg.CP_NewPassword = cpm.NewPassword;
                dbMsg.CP_ConfirmPassword = cpm.ConfirmPassword;
            }
            else if (fmessage is DeRegisterMessage)
            {
                DeRegisterMessage dm = (DeRegisterMessage)fmessage;
                dbMsg.Email = dm.Email;
                dbMsg.Pwd = dm.Pwd;
            }
            else if (fmessage is WithdrawMessage)
            {
                WithdrawMessage wm = (WithdrawMessage)fmessage;
                dbMsg.Amount = wm.Amount;
                dbMsg.Pwd = wm.Pwd;
            }

            return dbMsg;
        }
        private void DeleteArchivedSms(string symbol, GsmCommMain modem, int index, string storage)
        {
            if (modem.IsConnected() == true)
            {
                modem.DeleteMessage(index, storage);
            }
            else if (modem.IsConnected() == false)
            {
                if (symbol.Equals("R"))
                {
                    ConnectRecievingModem();
                }
                else
                {
                    ConnectSendingModem();
                }

                modem.DeleteMessage(index, storage);
            }
        }


        private string WriteToCloud(FanikiwaMessage fmessage, SMSMessage sms)
        {
            string response = "";
            string Request = Config.GetString("CloudUrl", "http://www.kufanikiwa.co.ke");
            if (fmessage.FanikiwaMessageType == FanikiwaMessageType.MpesaDepositMessage)
            {
                MpesaDepositMessage msg = (MpesaDepositMessage)fmessage;
                Request = Request + "/Mpesa?" +
                    "id=" + msg.SenderTelno +
                    "&orig=" + msg.orig +
                    "&dest=" + msg.dest +
                    "&tstamp=" + msg.tstamp +
                    "&text=" + msg.text +
                    "&user=" + msg.user +
                    "&pass=" + msg.pass +
                    "&mpesa_code=" + msg.mpesa_code +
                    "&mpesa_acc=" + msg.mpesa_acc +
                    "&mpesa_msisdn=" + msg.mpesa_msisdn +
                    "&mpesa_trx_date=" + msg.mpesa_trx_date +
                    "&mpesa_trx_time=" + msg.mpesa_trx_time +
                    "&mpesa_amt=" + msg.mpesa_amt +
                    "&mpesa_sender=" + msg.mpesa_sender;
                response = CloudComponent.HttpGet(Request);
                return response;
            }
            else
            {
                //sms.messageDate = sms.messageDate.ToString("yyyy-MM-dd'T'HH:mm:ss");

                var json = JsonConvert.SerializeObject(sms);
                log.Info("Writing to cloud. Json [" + json + "]");
                string addr = Request + "FanikiwaSMS";
                response = CloudComponent.HttpJsonPOST(addr, json);

                return response;
            }

        }

        public void ProcessFanikiwaMessage(FanikiwaMessage fmessage)
        {
            try
            {
                SMSProcessorComponent sp = new SMSProcessorComponent();
                string message = sp.ProcessFanikiwaMessage(fmessage);
                SendSMS(message, fmessage.SenderTelno);
            }
            catch (fPeerLending.Framework.ExceptionTypes.PeerLendingException plEx) //Peerlending exceptions
            {
                SendSMS(plEx.Message, fmessage.SenderTelno);
                throw;
            }
            catch (fanikiwaGL.Framework.ExceptionTypes.PostingException pEx) //Accountstatus,limit,staticposting
            {
                SendSMS(pEx.Message, fmessage.SenderTelno);
                throw;
            }
            catch (fanikiwaGL.Framework.ExceptionTypes.SimulationException simEx) //simulation
            {
                string errMsg = "";
                foreach (var m in simEx.SimulatePostStatus.Errors)
                {
                    errMsg += m.Message + System.Environment.NewLine;
                }
                SendSMS(errMsg, fmessage.SenderTelno);
                throw;
            }
            catch (fanikiwaGL.Framework.ExceptionTypes.BatchSimulationException BsimEx) //batchsimulation
            {
                string errMsg = "";
                foreach (var m in BsimEx.SimulatePostStatus.SimulateStatus)
                    foreach (var s in m.Errors)
                        errMsg += s.Message + System.Environment.NewLine;

                SendSMS(errMsg, fmessage.SenderTelno);
                throw;
            }
            catch (ArgumentNullException argNEx)
            {
                SendSMS(argNEx.Message, fmessage.SenderTelno);
                throw;
            }
            catch (ArgumentException argEx)
            {
                SendSMS(argEx.Message, fmessage.SenderTelno);
                throw;
            }
            catch (fPeerLending.Framework.ExceptionTypes.UserInterfaceException smsEx)
            {
                SendSMS(smsEx.Message, fmessage.SenderTelno);
                if (smsEx.InnerException != null)
                    throw smsEx.InnerException;
            }
            catch (Exception ex)
            {
                SendSMS("An error occurred while processing your request, please contact the administrator", fmessage.SenderTelno);
                bool rethrow = false;
                rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);

                if (rethrow)
                {
                    throw;
                }
            }
        }

        public void SendSMS(string message, string Telno)
        {
            MessagerComponent mc = new MessagerComponent(sendingModem);
            mc.SendSMS(message, Telno);
        }

        public void SendEmail(string addressTo, string Subject, String Body)
        {
            MessagerComponent mc = new MessagerComponent(sendingModem);
            mc.SendEmailMessage(addressTo, Subject, Body);
        }

        //sending
        #endregion
    }

    public class MyServiceHostFactory : ServiceHostFactory
    {
        private readonly GsmCommMain receivingModem;
        private readonly GsmCommMain sendingModem;

        public MyServiceHostFactory(GsmCommMain sModem, GsmCommMain rModem)
        {
            this.receivingModem = rModem;
            this.sendingModem = sModem;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType,
            Uri[] baseAddresses)
        {
            return new MyServiceHost(sendingModem, receivingModem, serviceType, baseAddresses);
        }
    }

    public class MyServiceHost : ServiceHost
    {
        public MyServiceHost(GsmCommMain sendingModem, GsmCommMain receivingModem, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (sendingModem == null)
            {
                throw new ArgumentNullException("sendingModem");
            }
            if (receivingModem == null)
            {
                throw new ArgumentNullException("receivingModem");
            }

            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new MyInstanceProvider(sendingModem, receivingModem));
            }
        }
    }

    public class MyInstanceProvider : IContractBehavior, IInstanceProvider
    {
        private readonly GsmCommMain receivingModem;
        private readonly GsmCommMain sendingModem;

        public MyInstanceProvider(GsmCommMain sendingModem, GsmCommMain receivingModem)
        {
            if (receivingModem == null)
            {
                throw new ArgumentNullException("receivingModem");
            }
            if (sendingModem == null)
            {
                throw new ArgumentNullException("sendingModem");
            }
            this.sendingModem = sendingModem;
            this.receivingModem = receivingModem;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new MessagingService(this.sendingModem, this.receivingModem);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
