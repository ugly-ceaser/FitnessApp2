using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer
{
    public class MetricDataTransfer
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string UnitOfMeasurement { get; set; }

        public int ActivityID { get; set; }

        public string ActivityName { get; set; }

        public int UserID { get; set; }

        public int Calories { get; set; }
    }
}
