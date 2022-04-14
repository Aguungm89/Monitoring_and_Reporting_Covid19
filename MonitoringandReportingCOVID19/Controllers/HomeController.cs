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
using Xunit;
using Xunit.Sdk;
using System.ComponentModel.DataAnnotations;

namespace MonitoringandReportingCOVID19.Controllers
{
    public class HomeController : Controller
    {
        private MRC19Entities db = new MRC19Entities();

        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var table = db.V_ActiveCases.ToList()
               .GroupBy(x => new { x.Departement })
               .Select(x => new DepartmentACmodel
               {
                   Departement = x.Key.Departement,
                   count = x.Count()

               }).OrderByDescending(c => c.count);
                ViewBag.DepartActive = table;

                CountCases();
                CountCasesRD();
                CountGenderCases();
                return View();
            }
            
        }

        public ActionResult GetData()
        {
            MRC19Entities context = new MRC19Entities();

            var query = context.V_BarChart.Include("DaMo")
                   .GroupBy(p => p.DaMo)
                   .Select(g => new LineChartmodel
                   {
                       DaMo = g.Key,
                       count = g.Count()

                   }).ToList();
            //.Select(g => new { name = g.Key, count= g.Count(w => w.DaMo) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MonthlyChart()
        {
            MRC19Entities context = new MRC19Entities();

            var query = context.V_BarChart.Include("MoYe")
                   .GroupBy(p => p.MoYe)
                   .Select(g => new Mothnlychartmodel
                   {
                       MoYe = g.Key,
                       count = g.Count()

                   }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PieChartGender()
        {

            var table = db.CovidConfirmCases
                    .Include("Gender")
                    .GroupBy(e => e.Gender)
                    .Select(y => new Chartmodel
                    {
                        Gender = y.Key,
                        count = y.Count()

                    }).ToList();
            ViewBag.piechartG = table;
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BarchartD()
        {

            var table = ("from m in db.CovidConfirmCases " +
                "select COUNT(Confirmation_Date) as countdate, (Datename(DAY, Confirmation_Date) + ' ' + Datename(MONTH, Confirmation_Date)) as DM ,(Datename(MONTH, Confirmation_Date) + ' ' + Datename(YEAR, Confirmation_Date)) as MY");

            ViewBag.BarchartD = table;
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PieChartGender(DateTime STARTDATE, DateTime ENDDATE)
        {

            var table = db.CovidConfirmCases
                    .Include("Gender")
                    .GroupBy(e => e.Gender)
                    .Select(y => new Chartmodel
                    {
                        Gender = y.Key,
                        count = y.Count()

                    }).ToList();
            ViewBag.piechartG = table;

            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public void CountCases()
        {
            ViewBag.CountConfirm = db.CovidConfirmCases.Count();
            ViewBag.CountActive = db.V_ActiveCases.Count();
            
        }
        public void CountCasesRD()
        {
            ViewBag.CountRecovery = db.V_RecoveryCases.Count();
            ViewBag.CountDeath = db.V_DeathCases.Count();
        }

        public void CountGenderCases()
        {
            ViewBag.CountMG = db.V_CMgender.Count();
            ViewBag.CountFG = db.V_CFgender.Count();
        }

        public ActionResult UploadEmployee()
        {
            var table = new ViMoviewmodel
            {
                Temp_Employees = db.Temp_Employee.ToList()
            };
            return View(table);
        }

        public FileResult DownloadExcelUEMP()
        {
            string path = "/temp/Employee_temp.xlsx";
            return File(path, "application/vnd.ms-excel", "Employee_temp.xlsx");
        }

        [HttpPost]
        public ActionResult UploadExcelEMP(Temp_Employee tEMP_MEMBERUP, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    try
                    {
                        adapter.Fill(ds, "ExcelTable");
                    }
                    catch (Exception)
                    {
                        if ((System.IO.File.Exists(pathToExcelFile)))
                        {
                            System.IO.File.Delete(pathToExcelFile);
                        }
                        TempData["Error"] = "Sheet1 not found";
                        return RedirectToAction("UploadEmployee");
                    }
                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<Temp_Employee>(sheetName) select a;

                    try
                    {
                        foreach (var a in artistAlbums)
                        {
                            try
                            {
                                var isIDAlreadyExists = db.Employees.Any(x => x.Emp_id == a.EmployeeID);
                                if (isIDAlreadyExists)
                                {
                                    TempData["Error"] = "Employee ID already exist";
                                    return RedirectToAction("UploadEmployee");
                                }
                                else if (a.EmployeeID != "" )
                                {
                                    Temp_Employee TU = new Temp_Employee();

                                    TU.EmployeeID = a.EmployeeID;
                                    TU.Employee_Name = a.Employee_Name;
                                    TU.DateofBirth = a.DateofBirth;
                                    TU.Gender = a.Gender;
                                    TU.Departement = a.Departement;
                                    TU.Address = a.Address;
                                    db.Temp_Employee.Add(TU);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    if (a.EmployeeID == "" || a.EmployeeID == null)
                                    {
                                        TempData["Error"] = "Employee ID is required";
                                    }
                                    
                                    return RedirectToAction("UploadEmployee");
                                }
                            }

                            catch (DbEntityValidationException ex)
                            {
                                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                                {

                                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                                    {

                                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                    }

                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if ((System.IO.File.Exists(pathToExcelFile)))
                        {
                            System.IO.File.Delete(pathToExcelFile);
                        }
                        TempData["Error"] = "Date Of Birth Must a Date Format";
                        return RedirectToAction("UploadEmployee");
                    }
                    //deleting excel file from folder

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return RedirectToAction("CheckUploadEMP");
                }
                else
                {
                    //alert message for invalid file format  
                    TempData["Error"] = "Only Excel file format is allowed";
                    return RedirectToAction("UploadEmployee");
                }
            }
            else
            {
                TempData["Error"] = "Please choose Excel file";
                return RedirectToAction("UploadEmployee");
            }
        }

        public ActionResult CheckUploadEMP( string EmployeeID)
        {
            
            var table = new ViMoviewmodel
            {
                V_Uploadnews = db.V_Uploadnew.ToList(),
                V_Uploadolds = db.V_Uploadold.ToList(),
                Temp_Employees = db.Temp_Employee.ToList()
            };
            if (table.Temp_Employees.Count() == 0)
            {
                TempData["Error"] = "Uploaded data not found";
                return RedirectToAction("UploadEmployee");
            }
            else
            {
                return View(table);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckUploadEMP(string command, string type, string UPLOADID, string CREATED_BY, string CREATED_DATETIME)
        {
            if (command.Equals("save"))
            {
                if (type == "new")
                {
                    
                    db.Database.ExecuteSqlCommand("insert into Employee(Emp_id,Emp_name,DOB,Gender,Departement,Address) " +
                        "select EmployeeID, Employee_Name, DateofBirth, Gender, Departement, Address from Temp_Employee tm " +
                        "WHERE NOT EXISTS(SELECT * FROM Employee m WHERE tm.EmployeeID = m.Emp_id)");
                    db.Database.ExecuteSqlCommand("delete from Temp_Employee");
                }
                else if (type == "old")
                {
                    db.Database.ExecuteSqlCommand("update Employee set Emp_id = a.EmployeeID, Emp_name = a.Employee_Name, " +
                        "DOB = a.DateofBirth, Gender = a.Gender, Departement = a.Departement, Address = a.Address from " +
                        "Employee inner join Temp_Employee a on a.EmployeeID = Employee.Emp_id WHERE EXISTS" +
                        "(SELECT * FROM Employee m WHERE a.EmployeeID = m.Emp_id)");
                    db.Database.ExecuteSqlCommand("delete from Temp_Employee");
                }
                else if (type == "all")
                {
                    db.Database.ExecuteSqlCommand("insert into Employee(Emp_id,Emp_name,DOB,Gender,Departement,Address) " +
                        "select EmployeeID, Employee_Name, DateofBirth, Gender, Departement, Address from Temp_Employee tm " +
                        "WHERE NOT EXISTS(SELECT * FROM Employee m WHERE tm.EmployeeID = m.Emp_id)");
                    db.Database.ExecuteSqlCommand("update Employee set Emp_id = a.EmployeeID, Emp_name = a.Employee_Name, " +
                        "DOB = a.DateofBirth, Gender = a.Gender, Departement = a.Departement, Address = a.Address from " +
                        "Employee inner join Temp_Employee a on a.EmployeeID = Employee.Emp_id WHERE EXISTS" +
                        "(SELECT * FROM Employee m WHERE a.EmployeeID = m.Emp_id)");
                    db.Database.ExecuteSqlCommand("delete from Temp_Employee");
                }
                TempData["Error"] = "Upload successful";
                return RedirectToAction("UploadEmployee");
            }
            else
            {
                db.Database.ExecuteSqlCommand("delete from Temp_Employee");
                db.SaveChanges();
                TempData["Error"] = "Upload Canceled";
                return RedirectToAction("UploadEmployee");
            }
        }

        public ActionResult AddEmployee()
        {
            var table = new ViMoviewmodel
            {
                Departments = db.Departments.ToList()
            };
            CountEmployee();
            return View(table);
        }

        public class idemployee
        {
            
            [Required(ErrorMessage = "Employee ID is required.")]
            public string IDemp { get; set; }
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(string Emp_id, string Emp_name, string DOB, string Gender, string Department, string Address)
        {
            var isIDAlreadyExists = db.Employees.Any(x => x.Emp_id == Emp_id);
            if (isIDAlreadyExists)
            {
                TempData["Error"] = "Employee ID already exist";
                return RedirectToAction("AddEmployee");
            }
            else
            {
                db.Database.ExecuteSqlCommand("insert into AccountUser (username ,password ,Name ,User_type) " +
                "select '" + Emp_id + "', '" + Emp_id + "', '" + Emp_name + "', 'Employee' ");
                db.Database.ExecuteSqlCommand("insert into Employee (Emp_id ,Emp_name ,DOB ,Gender ,Departement ,Address) " +
                    "select '" + Emp_id + "', '" + Emp_name + "', '" + DOB + "', '" + Gender + "', '" + Department + "', '" + Address + "' ");
                db.SaveChanges();

                return RedirectToAction("AddEmployee");
            }

        }

        public void CountEmployee()
        {
            ViewBag.Employee = db.Employees.Count();
            ViewBag.EmployeeM = db.V_EmpMale.Count();
            ViewBag.EmployeeF = db.V_EmpFemale.Count();
        }

        public ActionResult ConfirmCases()
        {
            var table = new ViMoviewmodel
            {
                Employees = db.Employees.ToList(),
                V_ConfirmEMPs = db.V_ConfirmEMP.ToList()
            };
            return View(table);
        }


        public ActionResult AddConfirm(int ids)
        {
            var today = DateTime.Now.ToString("MM/dd/yyyy");
            db.Database.ExecuteSqlCommand("insert into CovidConfirmCases (Emp_id ,Emp_name ,Gender ,Departement ,Confirmation_Date ,Status) " +
                "select Emp_id , Emp_name, Gender, Departement,'" + today + "', 'Active' from Employee  Where Emp_ids = '" + ids + "' ");
            db.SaveChanges();
            return RedirectToAction("ConfirmCases");
        }

        public ActionResult ConfirmationCases(string command)
        {
            if (command == "Download")
            {
                DataTable dt = new DataTable("Sheet1");
                dt.Columns.AddRange(new DataColumn[8] { new DataColumn("EMPLOYEE ID"),
                                                     new DataColumn("EMPLOYEE NAME"),
                                                     new DataColumn("GENDER"),
                                                     new DataColumn("DEPARTMENT"),
                                                     new DataColumn("CONFIRMATION DATE"),
                                                     new DataColumn("RECOVERY DATE"),
                                                     new DataColumn("DEATH DATE"),
                                                     new DataColumn("STATUS"),
            });

                var DownloadEx = db.CovidConfirmCases;

                foreach (var ConfirmCases in DownloadEx)
                {

                    dt.Rows.Add(ConfirmCases.Emp_id, ConfirmCases.Emp_name, ConfirmCases.Gender, ConfirmCases.Departement, ConfirmCases.Confirmation_Date,
                        ConfirmCases.Recovery_Date, ConfirmCases.Death_Date, ConfirmCases.Status);

                }



                using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConfirmationCases_List.xlsx");
                    }
                }
            }
            else
            {
                var table = new ViMoviewmodel
                {
                    CovidConfirmCas = db.CovidConfirmCases.ToList()
                };
                return View(table);
            }
            
        }

        public ActionResult ActiveCases()
        {
            var table = new ViMoviewmodel
            {
                V_ActiveCases = db.V_ActiveCases.ToList(),
                CovidConfirmCas = db.CovidConfirmCases.ToList()
            };
            return View(table);
        }

        public ActionResult AddRecovery(int ids)
        {
            var today = DateTime.Now.ToString("MM/dd/yyyy");
            db.Database.ExecuteSqlCommand("update CovidConfirmCases set Status = 'Recovery', Recovery_Date = '" + today + "' where ConfirmID = '" + ids + "' ");
            db.SaveChanges();
            return RedirectToAction("ActiveCases");
        }

        public ActionResult AddDeath(int ids)
        {
            var today = DateTime.Now.ToString("MM/dd/yyyy");
            db.Database.ExecuteSqlCommand("update CovidConfirmCases set Status = 'Death', Death_Date = '" + today + "' where ConfirmID = '" + ids + "' ");
            db.SaveChanges();
            return RedirectToAction("ActiveCases");
        }

        public ActionResult RecoveryCases()
        {
            var table = new ViMoviewmodel
            {
                V_RecoveryCases = db.V_RecoveryCases.ToList(),
                CovidConfirmCas = db.CovidConfirmCases.ToList()
            };
            return View(table);
        }

        public ActionResult DeathCases()
        {
            var table = new ViMoviewmodel
            {
                V_DeathCases = db.V_DeathCases.ToList(),
                CovidConfirmCas = db.CovidConfirmCases.ToList()
            };
            return View(table);
        }

        public ActionResult SetDepartment()
        {
            var table = new ViMoviewmodel
            {
                Departments = db.Departments.ToList()
            };
            return View(table);
        }

        [HttpPost, ActionName("Add New")]
        [ValidateAntiForgeryToken]
        public ActionResult SetDepartment(string id, string STOCK, string ids, string STAID, string CREATED_BY, string CREATED_DATETIME, string LOCATION, string Notes)
        {
            db.Database.ExecuteSqlCommand("insert into RecordStockAdjustment (MEDICINEID ,MEDICINE_NAME ,STOCK ,STOCK_ADJUST ,Notes ,STATUS, CREATED_BY, CREATED_DATETIME, LOCATION) select MEDICINEID , MEDICINE_NAME, STOCK, '" + STOCK + "', '" + Notes + "', 'ADJUSTMENT', '" + CREATED_BY + "','" + CREATED_DATETIME + "', PROVIDERID from V_Stocklistadjusment  WHERE id = '" + ids + "' ");
            db.Database.ExecuteSqlCommand("update MEDICINE_DETAIL set STOCK = '" + STOCK + "' WHERE id = '" + ids + "' ");
            return RedirectToAction("SetDepartment");
        }


        public ActionResult DeleteDepart(int ids)
        {
            db.Database.ExecuteSqlCommand("delete from Department where DepartID = '" + ids + "'");
            db.SaveChanges();
            return RedirectToAction("SetDepartment");
        }

        public ActionResult AddDepart()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepart(string Department_Name)
        {
            db.Database.ExecuteSqlCommand("insert into Department (Department_Name) " +
                "select '" + Department_Name + "' ");
            db.SaveChanges();
            return RedirectToAction("SetDepartment");
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login", "User");
        }

        public ActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLocation(string Emp_id, string Date_AddLocation, string Days_Isolation, string Current_position, string titik_kordinat, string Address, string Description)
        {
            
                db.Database.ExecuteSqlCommand("insert into Employee_location (Emp_id ,Date_AddLocation ,Days_Isolation ,Current_position, titik_kordinat, Address, Description ) " +
                "select '" + Emp_id + "', '" + Date_AddLocation + "', '" + Days_Isolation + "',  '" + Current_position + "', '" + titik_kordinat + "','" + Address + "','" + Description + "' ");
                db.SaveChanges();

                return RedirectToAction("AddLocation");

        }


        public ActionResult LocationEmployee()
        {
            var table = new ViMoviewmodel
            {
                Employee_Locations = db.Employee_location.ToList()
            };
            return View(table);
        }

    }
}