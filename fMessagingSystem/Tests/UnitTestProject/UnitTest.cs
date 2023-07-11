
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
 
using fPeerLending.MessageProcessor;
using fanikiwaGL.Framework.ExceptionTypes;
using fanikiwaGL.Framework;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestModem()
        {
            //SMSGateway.MyApplicationContext app = new SMSGateway.MyApplicationContext();
            //app.ReadAndProcessMessagesFromModem();
            //string Telno= "0715413144";
            //FanikiwaMessage fmsg = FanikiwaMessageFactory.CreateMessage(Telno,DateTime.Now,"B pwd.123");
            //app.ProcessFanikiwaMessage(fmsg);
            Debug.WriteLine( "Successful");
        }

        [TestMethod]
        public void TestProcessHelp()
        {
            Debug.WriteLine("Starting...");
            SMSProcessorComponent sc = new SMSProcessorComponent();
            List<string> ret = new List<string>();
            foreach (FanikiwaMessage m in GetMessages())
            {

                try
                {
                    //ret.Add(sc.ProcessFanikiwaMessage(m));
                }
                catch (SimulationException sex)
                {
                    SimulatePostStatus st = sex.SimulatePostStatus;
                    foreach (var er in st.Errors)
                    {
                        ret.Add(er.Message);
                    }
                }
                catch (Exception ex)
                {
                    ret.Add(ex.ToString());
                }
            }

            foreach (var s in ret)
            {
                System.Diagnostics.Debug.WriteLine("Sent to customer: " + s);
            }
        }
        private List<FanikiwaMessage> GetMessages()
        {
            List<FanikiwaMessage> fm = new List<FanikiwaMessage>();

            string mpesamsg = @"FX12UB729 Confirmed.  
                                                        on 31/10/14 at 7:49 PM  
                                                        Ksh30300.00 received from FRANCIS MURAYA 254717769329.  
                                                        Account Number 212  
                                                        New Utility balance is Ksh1420.00 ";

            //string stMsg = mpesamsg;//WithdrawSymbol*Pwd*Amount
            //string Telno = "MPESA";
            //FanikiwaMessage s = FanikiwaMessageFactory.CreateMessage(Telno, DateTime.Today, mpesamsg);
            //fm.Add(s);

            //string MS2 = "abo pwd123 2";
            //FanikiwaMessage M2 = FanikiwaMessageFactory.CreateMessage("+254715413144", DateTime.Today, MS2);
            //fm.Add(M2);

            ////string MS3 = "mbo pwd123 10000.00 5 9 ";
            ////FanikiwaMessage M3 = FanikiwaMessageFactory.CreateMessage("254717769329", DateTime.Today, MS3);
            ////fm.Add(M3);

            //string MS4 = "b pwd123";
            //FanikiwaMessage M4 = FanikiwaMessageFactory.CreateMessage("+254715413144", DateTime.Today, MS4);
            //fm.Add(M4);

            return fm;
        }


    }
}