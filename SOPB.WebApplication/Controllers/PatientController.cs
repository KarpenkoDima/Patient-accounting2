using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using BAL.ORM;

namespace SOPB.WebApplication.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        [HttpPost]
        public ActionResult Index(string name)
        {
            CustomerService service = new CustomerService();
            DataSet  data = (DataSet)service.GetCustomersByLastName(name);
            foreach (DataRow dataRow in data.Tables["Customer"].Rows)
            {
                ViewBag.LastName = dataRow["LastName"];
                ViewBag.FirstName = dataRow["FirstName"];
                ViewBag.BirthOfDay = dataRow["BirthOfDay"];
            }

            return View();
        }
    }
}