using System;
using System.Runtime.Serialization;

namespace fMessagingSystem.Entities
{

    [Serializable]
    [DataContract]
    public partial class EmailMessage :Message
    {

        [DataMember]
        public string subject { get; set; }

        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string password { get; set; }

    }
}