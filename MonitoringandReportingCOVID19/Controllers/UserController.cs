using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LinqToExcel;
using System.Data.Entity.Validation;
using MonitoringandReportingCOVID19.Models;
using MonitoringandReportingCOVID19.viewmodel;
using ClosedXML.Excel;
using System.IO;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;

namespace MonitoringandReportingCOVID19.Controllers
{
    public class UserController : Controller
    {
        private MRC19Entities db = new MRC19Entities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult login(string username, string password, string location)
        {
            var data = db.AccountUsers.Where(s => s.username.Equals(username) && s.password.Equals(password)).ToList();
            var check = db.AccountUsers.Where(s => s.username.Equals(username)).Select(s => s.User_type).SingleOrDefault();
            var data1 = db.V_statuemp.Where(s => s.username.Equals(username)).ToList();
            if (data.Count() > 0)
            {
                if (check == "Admin")
                {
                    Session["username"] = data.FirstOrDefault().username;
                    Session["User_type"] = data.FirstOrDefault().User_type;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["username"] = data.FirstOrDefault().username;
                    Session["User_type"] = data.FirstOrDefault().User_type;
                    Session["Status"] = data1.FirstOrDefault().Status;

                    return RedirectToAction("Index", "Home");
                }
                
            }
            else
            {
                TempData["message"] = "Your have wrong Username/Password";
                return RedirectToAction("login");
            }
        }


    }
}