using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer
{
    public class UserWorkoutsDataTransfer
    {
        public int UserWorkoutID { get; set; }
        public int MetricID { get; set; }

        public string MetricName { get; set; }
        public int ActivityID { get; set; }

        public string ActivityName { get; set; }
        public int UserID { get; set; }
        public int UserWorkout { get; set; }
        public int CaloriesBurnt { get; set; }
        public DateTime WorkoutDate { get; set; }
    }
}
