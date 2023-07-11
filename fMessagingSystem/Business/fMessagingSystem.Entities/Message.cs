using System;
using System.Runtime.Serialization;


namespace fMessagingSystem.Entities
{
    
    [Serializable]
    [DataContract]
    [KnownType(typeof(EmailMessage))]
    [KnownType(typeof(SMSMessage))]
    public abstract class Message
    {
        /// <summary>
        /// Gets or sets a messageDate.
        /// </summary>
        [DataMember]
        public DateTime messageDate { get; set; }

        /// <summary>
        /// Gets or sets addressFrom.
        /// </summary>
        [DataMember]
        public string addressFrom { get; set; }

        /// <summary>
        /// Gets or sets addressTo.
        /// </summary>
        [DataMember]
        public string addressTo { get; set; }

        [DataMember]
        public virtual object body { get; set; }
    }
}