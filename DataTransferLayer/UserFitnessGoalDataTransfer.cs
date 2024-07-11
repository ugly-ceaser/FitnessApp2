using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer
{
    public class UserFitnessGoalDataTransfer
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }

        public string ActivityName { get; set; }
        public int MetricId { get; set; }
        public double GoalAmount { get; set; }
    }
}
