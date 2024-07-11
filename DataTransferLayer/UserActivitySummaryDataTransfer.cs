using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer
{
    public class UserActivitySummaryDataTransfer
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public int NumberOfGoalSet { get; set; }

        public int NumberOfActivitySet { get; set; }
    }
}
