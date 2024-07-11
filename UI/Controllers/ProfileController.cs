using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTransferLayer;
using BusinessLogicLayer;
using Newtonsoft.Json;

namespace UI.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile

        public static ProfileBLL profileBLL = new ProfileBLL();
        public ActionResult Index()
        {
            
            if (Session["UserID"] is int ID && ID != 0)
            {
               
                var Summary = profileBLL.Index(ID);

                if (Summary != null && !string.IsNullOrWhiteSpace(Summary.Username))
                {
                    
                    return View(Summary);
                }
                else
                {
                   
                    return View(new UserActivitySummaryDataTransfer());
                }
            }
            else
            {
                
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult ViewActivity()
        {
            if(Session["UserID"] is int ID && ID != 0)
            {

                var ActivityList = profileBLL.GetAllUserActivities(ID);

                if(ActivityList != null)
                {
                    return View(ActivityList);
                }

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddActivity()
        {
            if (Session["UserID"] is int ID && ID != 0)
            {
              List<Activity> activites = new List<Activity>();

                activites = profileBLL.GetActivities();

              return View(activites);
                

            }
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public ActionResult AddActivity(int activitySelect)
        {
            if (ModelState.IsValid)
            {
                if (Session["UserID"] is int ID && ID != 0)
                {
                    FitnessActivityDataTransfer DTO = new FitnessActivityDataTransfer();

                    DTO.ActivityId = activitySelect;
                    DTO.UserID = ID;

                    var success = profileBLL.AddUserActivity(DTO);

                    if (success)
                    {
                        ViewBag.SuccessMessage = "Activity added successfully!";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to add activity. Please try again.";
                    }
                }
            }

            List<Activity> activities = profileBLL.GetActivities();
            return View(activities);
        }


        public ActionResult ViewSingleActivity(int activityId=0, string Name="")
        {
            if (Session["UserID"] is int ID && ID != 0)
            {
                ViewBag.Metrics = profileBLL.GetMetricsByUserActivity(ID, activityId);
                ViewBag.Goals = profileBLL.GetTotalTargetCalories(ID, activityId);
                ViewBag.Name = Name;
                ViewBag.ActivityId = activityId;

                if (ViewBag.Metrics != null && ViewBag.Goals != null)
                {
                    return View(ViewBag);
                }
                else
                {
                    // Handle case when Metrics or Goals are null
                    // You can return a different view or redirect to an error page
                    return RedirectToAction("Error");
                }
            }

            return RedirectToAction("Index", "Home");



        }


        public ActionResult AddMetric(int ActivityId = 0,string Name = "")
        {
            
            if (Session["UserID"] is int ID && ID != 0)
            {
                var metricData = new MetricDataTransfer();
                metricData.ActivityID = ActivityId;
                metricData.ActivityName = Name;
                return View(metricData);

            }

            return RedirectToAction("Index", "Home");


        }

       

        [HttpPost]
        public ActionResult AddMetric( MetricDataTransfer DTO)
        {
            if (Session["UserID"] is int ID && ID != 0)
            {
                if (ModelState.IsValid)
                {
                    DTO.UserID = ID;

                    var success = profileBLL.AddMetric(DTO);

                    if (success)
                    {

                        ViewBag.SuccessMessage = "Metric added successfully!";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to add Metric. Please try again.";
                    }

                }
               
                
                
                return View(DTO);

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddWorkOut(int ActivityId = 0, string Name = "", int MetricID = 0)
        {

            if (Session["UserID"] is int ID && ID != 0)
            {
                var workOutSession = new UserWorkoutsDataTransfer();
                workOutSession.ActivityID = ActivityId;
                workOutSession.MetricName = Name;
                return View(workOutSession);

            }

            return RedirectToAction("Index", "Home");


        }

        [HttpPost]
        public ActionResult AddWorkOut(UserWorkoutsDataTransfer DTO)
        {
            if (Session["UserID"] is int ID && ID != 0)
            {
                if (ModelState.IsValid)
                {
                    DTO.UserID = ID;

                    var success = profileBLL.AddWorkOut(DTO);

                    if (success)
                    {

                        ViewBag.SuccessMessage = "Work Session added successfully!";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to add Work Session. Please try again.";
                    }

                }



                return View(DTO);

            }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult FitnessGoals()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Remove("UserID");
            return RedirectToAction("Index", "Home");
        }
    }
}