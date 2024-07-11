using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTransferLayer;
using BusinessLogicLayer;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        static AuthBLL Authentication = new AuthBLL();
        public ActionResult Index()
        {

            DataTransferLayer.UserDataTransfer userDataTransfer = new DataTransferLayer.UserDataTransfer();

            return View(userDataTransfer);
        }

        [HttpPost]
        public ActionResult Index(UserDataTransfer DTO)
        {
            if (ModelState.IsValid)
            {
                var user = Authentication.Login(DTO);

                if (user != null && user.ID != 0)
                {
                    Session["UserID"] = user.ID;
                    return RedirectToAction("Index", "Profile");
                }

                return View(DTO);
            }

            return View(DTO);
        }


        public ActionResult Register()
        {

            DataTransferLayer.UserDataTransfer userDataTransfer = new DataTransferLayer.UserDataTransfer();

            return View(userDataTransfer);

           
        }

        [HttpPost]
        public ActionResult Register(UserDataTransfer DTO)
        {

            if (ModelState.IsValid)
            {
                var user = Authentication.Register(DTO);

                if (user != null && user.ID != 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                return View(DTO);
            }

            return View(DTO);


        }
    }
}