using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer;

namespace DataAccessLayer
{
    public class ProfileDAL : FitnessTrackerContext
    {
        public UserActivitySummaryDataTransfer GetUserActivitySummary(int ID)
        {

            var user = db_connect.Users.FirstOrDefault(u => u.UserID == ID);
            if (user == null)
            {
                // Handle case where user is not found
                return null;
            }


            // Retrieve number of goals set by the user
            int numberOfGoals = db_connect.FitnessGoals.Count(g => g.UserID == ID);

            // Retrieve number of fitness activities recorded by the user
            int numberOfActivities = db_connect.FitnessRecords.Count(a => a.UserID == ID);

            // Create and return user activity summary object
            var summary = new UserActivitySummaryDataTransfer
            {
                Username = user.Username,
                NumberOfGoalSet = numberOfGoals,
                NumberOfActivitySet = numberOfActivities
            };

            return summary;
        }


        public List<FitnessActivityDataTransfer> GetAllUserActivities(int userId)
        {
            // Retrieve all fitness activities recorded by the user along with their names
            var userActivities = (from record in db_connect.FitnessRecords
                                  join activity in db_connect.Activities
                                  on record.ActivityID equals activity.ActivityID
                                  where record.UserID == userId
                                  select new FitnessActivityDataTransfer
                                  {
                                      ActivityId = (int)record.ActivityID,
                                      Name = activity.Name,
                                      UserID = (int)record.UserID,
                                      ID = record.RecordID,
                                      
                                      
                                  }).ToList();

            return userActivities;
        }


        public List<UserFitnessGoalDataTransfer> GetAllUserGoals(int userId)
        {
            // Retrieve all fitness goals set by the user along with the associated activity name
            var userGoals = (from goal in db_connect.FitnessGoals
                             join activity in db_connect.Activities
                             on goal.ActivityID equals activity.ActivityID
                             where goal.UserID == userId
                             select new UserFitnessGoalDataTransfer
                             {
                                 GoalId = goal.GoalID,
                                 UserId = (int)goal.UserID,
                                 ActivityId = (int)goal.ActivityID,
                                 ActivityName = activity.Name,
                                 GoalAmount = (int)goal.TargetCalories,
                                 // Add other properties as needed
                             }).ToList();

            return userGoals;
        }


        public List<MetricDataTransfer> GetAllUserMetrics(int userId, int ActivityID)
        {
            try
            {
                // Retrieve all metrics associated with the specified user ID
                var userMetrics = db_connect.Metrics
                    .Where(m => m.UserID == userId && m.ActivityID == ActivityID)
                    .Select(m => new MetricDataTransfer
                    {
                        ID = m.MetricID,
                        Name = m.Name,
                        ActivityID = (int)m.ActivityID,
                        UserID = (int)m.UserID
                
            })
                    .ToList();

                return userMetrics;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return new List<MetricDataTransfer>();
            }
        }

        public List<DataTransferLayer.Activity> GetActivities()
        {
            List<DataTransferLayer.Activity> activities = new List<DataTransferLayer.Activity>();

            try
            {
               
                var dbActivities = db_connect.Activities.ToList();

                
                foreach (var dbActivity in dbActivities)
                {
                    var activityDTO = new DataTransferLayer.Activity
                    {
                        ID = dbActivity.ActivityID,
                        Name = dbActivity.Name
                        // Map other properties as needed
                    };
                    activities.Add(activityDTO);
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error fetching activities: " + ex.Message);
            }

            return activities;
        }

        public List<DataTransferLayer.MetricDataTransfer> GetMetricsByUserActivity(int userId, int activityId)
        {
            List<DataTransferLayer.MetricDataTransfer> Metrics = new List<DataTransferLayer.MetricDataTransfer>();
            try
            {
                var metrics = db_connect.Metrics
                    .Where(m => m.UserID == userId && m.ActivityID == activityId)
                    .ToList();

                foreach (var metric in metrics)
                {
                    var metricDTO = new DataTransferLayer.MetricDataTransfer
                    {
                        ID = metric.MetricID,
                        Name = metric.Name,
                        UnitOfMeasurement = metric.UnitOfMeasurement // Corrected property name
                    };
                    Metrics.Add(metricDTO);
                }

                return Metrics;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return new List<DataTransferLayer.MetricDataTransfer>();
            }
        }



        public List<UserFitnessGoalDataTransfer> GetGoalsByUserActivity(int userId, int activityId)
        {
            List<UserFitnessGoalDataTransfer> UserFitnessGoals = new List<UserFitnessGoalDataTransfer>();
            try
            {
                
                var goals = db_connect.FitnessGoals
                    .Where(g => g.UserID == userId && g.ActivityID == activityId)
                    .ToList();

                foreach(var goal in goals)
                {
                    UserFitnessGoalDataTransfer UserFitnessGoal = new UserFitnessGoalDataTransfer();

                    UserFitnessGoal.GoalAmount = (double)goal.TargetCalories;
                    UserFitnessGoal.ActivityId = (int)goal.ActivityID;
                    UserFitnessGoal.GoalId = goal.GoalID;

                    UserFitnessGoals.Add(UserFitnessGoal);


                }


                return UserFitnessGoals;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                
                return UserFitnessGoals;
            }
        }

        public double GetTotalTargetCalories(int userId, int activityId)
        {
            try
            {
                var totalCalories = db_connect.FitnessGoals
                    .Where(g => g.UserID == userId && g.ActivityID == activityId)
                    .Sum(g => g.TargetCalories);

                return (double)totalCalories;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return 0; // Return 0 if an error occurs
            }
        }



        public bool AddUserActivity(FitnessActivityDataTransfer DTO)
        {
            try
            {
                
                bool alreadyExists = db_connect.FitnessRecords
                    .Any(fr => fr.UserID == DTO.UserID && fr.ActivityID == DTO.ActivityId);

                if (alreadyExists)
                {
                   
                    return false;
                }

                
                var FitnessActivityDataTransfer = new FitnessRecord
                {
                    UserID = DTO.UserID,
                    ActivityID = DTO.ActivityId,
                };

                // Add the new activity record to the database
                db_connect.FitnessRecords.Add(FitnessActivityDataTransfer);
                db_connect.SaveChanges();

                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }


        public bool AddserGoal(UserFitnessGoalDataTransfer DTO)
        {
            try
            {
                // Create a new FitnessGoal object to represent the user's goal
                var newGoal = new FitnessGoal
                {
                    UserID = DTO.UserId,
                    ActivityID = DTO.ActivityId,
                    TargetCalories = (int?)DTO.GoalAmount,
                    // Add other properties as needed
                };

                // Add the new goal to the database
                db_connect.FitnessGoals.Add(newGoal);
                db_connect.SaveChanges();

                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);

                // Return false if an exception occurs
                return false;
            }
        }

        public bool AddMetric(MetricDataTransfer DTO)
        {
            try
            {

                bool alreadyExists = db_connect.Metrics
                   .Any(fr => fr.Name == DTO.Name && fr.ActivityID == DTO.ActivityID);

                if (alreadyExists)
                {

                    return false;
                }
                // Create a new Metric object
                var newMetric = new Metric
                {
                    Name = DTO.Name,
                    ActivityID = DTO.ActivityID,
                    UserID = DTO.UserID,
                    UnitOfMeasurement = DTO.UnitOfMeasurement,
                    TargetCalories = DTO.Calories
                    




                };

                // Add the new metric to the database context
                db_connect.Metrics.Add(newMetric);

                // Save changes to the database
                db_connect.SaveChanges();

                return true; // Return true if metric is added successfully
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);

                // Return false if an exception occurs
                return false;
            }
        }

        public bool AddGoal(int userId, int activityId, int targetCal)
        {
            try
            {
                
                var existingGoal = db_connect.FitnessGoals
                    .FirstOrDefault(g => g.UserID == userId && g.ActivityID == activityId);

                if (existingGoal != null)
                {
                    
                    existingGoal.TargetCalories += targetCal;
                }
                else
                {
                    
                    var newGoal = new FitnessGoal
                    {
                        UserID = userId,
                        ActivityID = activityId,
                        TargetCalories = targetCal,
                    };

                   
                    db_connect.FitnessGoals.Add(newGoal);
                }

               
                db_connect.SaveChanges();

                return true; 
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false; 
            }
        }

        public bool UpdateGoal(int userId, int activityId, int targetCal)
        {
            try
            {

                var existingGoal = db_connect.FitnessGoals
                    .FirstOrDefault(g => g.UserID == userId && g.ActivityID == activityId);

                if (existingGoal != null)
                {

                    existingGoal.ActualCalories += targetCal;
                    db_connect.SaveChanges();

                    return true;
                }

                return false;
                

                

               
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

        public bool AddWorkOut(UserWorkoutsDataTransfer DTO)
        {
            try
            {


                // Create a new Metric object
                var newUserWork = new UserWorkout
                {
                    MetricID = DTO.MetricID,
                    ActivityID = DTO.ActivityID,
                    UserID = DTO.UserID,
                    CaloriesBurnt = (int)DTO.CaloriesBurnt,
                    WorkoutDate = DateTime.Now





                };

                // Add the new metric to the database context
                db_connect.UserWorkouts.Add(newUserWork);

                // Save changes to the database
                db_connect.SaveChanges();

                return true; // Return true if metric is added successfully
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);

                // Return false if an exception occurs
                return false;
            }
        }

        
    }


}
