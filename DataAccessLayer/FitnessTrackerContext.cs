using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FitnessTrackerContext
    {
        public static FitnessTrackerEntities db_connect = new FitnessTrackerEntities();
    }
}
