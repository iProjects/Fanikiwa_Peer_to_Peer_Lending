//====================================================================================================
// Code generated with Velocity: SL Gen (Build 2.4.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by Administrator at SAPSERVER on 10/20/2013 16:53:17 
//====================================================================================================

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace fanikiwaGL.Services.Contracts
{
    /// <summary>
    /// FinancialPostingFaultHandler class.
    /// </summary>
    public class FinancialPostingFaultHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            string reason = "An unhandled exception has occurred in the service.";

            FinancialPostingFault faultContract = new FinancialPostingFault(error.Message);

            var faultEx = new FaultException<FinancialPostingFault>(faultContract, reason);

            MessageFault messageFault = faultEx.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultEx.Action);
        }
    }
}
