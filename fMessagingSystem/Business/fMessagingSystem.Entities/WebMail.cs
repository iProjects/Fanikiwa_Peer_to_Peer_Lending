using System;
using System.Runtime.Serialization;

namespace fMessagingSystem.Entities
{

    [Serializable]
    [DataContract]
    public partial class WebMail: Message
    {
        [DataMember]
        public string subject { get; set; }
    }
}