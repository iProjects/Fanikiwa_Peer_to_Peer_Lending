using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace fanikiwaGL.Framework.ExceptionTypes
{


    [Serializable]
    public class PostingException : BusinessLogicCustomException
    {
        public PostingException()
            : base() { }

        public PostingException(string message)
            : base(message) { }

        public PostingException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public PostingException(string message, Exception innerException)
            : base(message, innerException) { }

        public PostingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PostingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class PrePostingException : BusinessLogicCustomException
    {
        public PrePostingException()
            : base() { }

        public PrePostingException(string message)
            : base(message) { }

        public PrePostingException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public PrePostingException(string message, Exception innerException)
            : base(message, innerException) { }

        public PrePostingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PrePostingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }


    [Serializable]
    public class LimitException : PostingException
    {
        public LimitException()
            : base() { }

        public LimitException(string message)
            : base(message) { }

        public LimitException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public LimitException(string message, Exception innerException)
            : base(message, innerException) { }

        public LimitException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected LimitException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class AccountLockException : PostingException
    {
        public AccountLockException()
            : base() { }

        public AccountLockException(string message)
            : base(message) { }

        public AccountLockException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public AccountLockException(string message, Exception innerException)
            : base(message, innerException) { }

        public AccountLockException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected AccountLockException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class StaticPostingException : PostingException
    {
        public StaticPostingException()
            : base() { }

        public StaticPostingException(string message)
            : base(message) { }

        public StaticPostingException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public StaticPostingException(string message, Exception innerException)
            : base(message, innerException) { }

        public StaticPostingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected StaticPostingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class SimulationException : PrePostingException
    {
        public SimulatePostStatus SimulatePostStatus;
        public SimulationException()
            : base()
        {
            // Add implementation (if required)
        }
        public SimulationException(SimulatePostStatus SimulateStatus, string message)
            : base(message)
        {
            SimulatePostStatus = SimulateStatus;
        }

        public SimulationException(string message)
            : base(message)
        {
            // Add Implementation (if required)
        }

        public SimulationException(string message, System.Exception inner)
            : base(message, inner)
        {
            // Add implementation (if required)
        }

        protected SimulationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation (if required)
        }
    }

    [Serializable]
    public class BatchSimulationException : PrePostingException
    {
        public BatchSimulateStatus SimulatePostStatus;
        public BatchSimulationException()
            : base()
        {
            // Add implementation (if required)
        }
        public BatchSimulationException(BatchSimulateStatus SimulateStatus, string message)
            : base(message)
        {
            SimulatePostStatus = SimulateStatus;
        }

        public BatchSimulationException(string message)
            : base(message)
        {
            // Add Implementation (if required)
        }

        public BatchSimulationException(string message, System.Exception inner)
            : base(message, inner)
        {
            // Add implementation (if required)
        }

        protected BatchSimulationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation (if required)
        }
    }
}
