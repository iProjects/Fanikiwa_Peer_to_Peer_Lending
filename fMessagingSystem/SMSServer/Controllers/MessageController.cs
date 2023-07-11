using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using fMessagingSystem.Entities;
using SMSGateway;

namespace SMSServer.Controllers
{
    public class MessageController : ApiController

    {
        public void SendSMS(SMSMessage msg)
        {
            //MyApplicationContext ctx = new MyApplicationContext();
            //ctx.SendSMS(msg.body.ToString(), msg.addressFrom);
        }

        public string[] Get()
        {
            return new string[]
        {
             "Hello",
             "World"
        };
        }

        public string GetTime([FromUri] Time t)
        {
            return string.Format("Received Time: {0}:{1}.{2}", t.Hour, t.Minute, t.Second);
        }
    }


    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
    }

}





    