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

        /**********/
        /* CREATE */
        /**********/
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            Employee employee = new Employee();
            TryUpdateModel(employee);

            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                employeeBusinessLayer.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        /********/
        /* EDIT */
        /********/
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.Id == id);
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

        /**********/
        /* DELETE */
        /**********/
        [HttpPost]
        public ActionResult Delete(int id)
        {
            employeeBusinessLayer.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
