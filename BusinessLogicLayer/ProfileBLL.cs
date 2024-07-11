using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class ProfileBLL
    {
        public static ProfileDAL profileDAL = new ProfileDAL();

        public UserActivitySummaryDataTransfer Index(int ID)
        {
            UserActivitySummaryDataTransfer DTO = new UserActivitySummaryDataTransfer();

            var Summary = profileDAL.GetUserActivitySummary(ID);

            if(Summary != null && Summary.Username.Trim() != "")
            {
                DTO.Username = Summary.Username;
                DTO.NumberOfActivitySet = Summary.NumberOfActivitySet;
                DTO.NumberOfGoalSet = Summary.NumberOfGoalSet;

                return DTO;


            }


            return DTO;
        }

        public List<FitnessActivityDataTransfer> GetAllUserActivities(int ID)
        {
            var ActivityList = profileDAL.GetAllUserActivities(ID);

            return ActivityList;

        }

        public List<MetricDataTransfer> GetMetricsByUserActivity(int userId, int activityId)
        {
            var UserActivitiesMetric = profileDAL.GetMetricsByUserActivity(userId, activityId);

            return UserActivitiesMetric;
        }


        public List<UserFitnessGoalDataTransfer> GetGoalsByUserActivity(int userId, int activityId)
        {
            var UserActivitiesGoal = profileDAL.GetGoalsByUserActivity(userId, activityId);

            return UserActivitiesGoal;
        }

        public double GetTotalTargetCalories(int userId, int activityId)
        {
            try
            {
                return profileDAL.GetTotalTargetCalories(userId, activityId);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return 0; // Return 0 if an error occurs
            }
        }


        public List<DataTransferLayer.Activity> GetActivities()
        {

            List<DataTransferLayer.Activity> activites = new List<DataTransferLayer.Activity>();

            activites = profileDAL.GetActivities();

            return activites;
        
        }

        public bool AddUserActivity(FitnessActivityDataTransfer DTO)
        {
            var success = profileDAL.AddUserActivity(DTO);

            return success;
        }

        public bool AddMetric(MetricDataTransfer DTO)
        {
            var success = profileDAL.AddMetric(DTO);

            if (success)
            {
                profileDAL.AddGoal(DTO.UserID, DTO.ActivityID,(int)DTO.Calories);
            }


            return success;
        }
       
        public bool AddWorkOut(UserWorkoutsDataTransfer DTO)
        {
            
            if(DTO.MetricName == "Distance")
            {
                DTO.CaloriesBurnt = DTO.UserWorkout * 3;
            }
            else if(DTO.MetricName == "Speed")
            {
                DTO.CaloriesBurnt = DTO.UserWorkout * 2;
            }
            else if (DTO.MetricName == "Time")
            {
                DTO.CaloriesBurnt = DTO.UserWorkout * 4;
            }

            var success = profileDAL.AddWorkOut(DTO);

            if (success)
            {
                profileDAL.UpdateGoal(DTO.UserID, DTO.ActivityID, DTO.CaloriesBurnt);
            }


            return success;
        }
    }
}
