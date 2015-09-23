using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;

namespace BusinessObjects.Controllers
{
    public class EmployeeController : Controller
    {
        internal EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();

        public ActionResult Index()
        {
            List<Employee> employees = employeeBusinessLayer.Employees;
            return View(employees);
        }

        /*        */
        /* CREATE */
        /*        */
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //public ActionResult Create(string Name, string City, string Gender, DateTime DateOfBirth)
        //public ActionResult Create(Employee employee)
        public ActionResult Create(FormCollection formCollection)
        {
            Employee employee = new Employee
            {
                Name = formCollection["Name"],
                City = formCollection["City"],
                Gender = formCollection["Gender"],
                DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirth"])
            };

            Session["Message"] = employeeBusinessLayer.AddEmployee(employee) ? "Success!" : "Failed!";

            return RedirectToAction("Index");
        }

        /*      */
        /* EDIT */
        /*      */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.ID == id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeBusinessLayer.SaveEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        /*        */
        /* DELETE */
        /*        */

        [HttpGet]
        public ActionResult Delete(int id)
        {
            employeeBusinessLayer.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
