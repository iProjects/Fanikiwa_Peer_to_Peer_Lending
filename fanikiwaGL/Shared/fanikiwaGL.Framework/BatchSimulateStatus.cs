using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Runtime.Serialization;

namespace fanikiwaGL.Framework
{
    [DataContract]
    public class BatchSimulateStatus
    {

        public BatchSimulateStatus(List<SimulatePostStatus> status)
        {
            SimulateStatus = status;
        }
        [DataMember]
        public bool CanPost
        {
            get
            {
                bool canPost = true;
                List<bool> CanPosts = (from s in SimulateStatus
                                       select s.CanPost).ToList();
                if (CanPosts.Contains(false)) return false;
                return canPost;
            }
        }
        [DataMember]
        public List<SimulatePostStatus> SimulateStatus { get; set; }
    }
}
