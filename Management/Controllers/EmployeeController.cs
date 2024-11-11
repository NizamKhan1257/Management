using ManagementDTOs;
using ManagementRepository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Management.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public IActionResult EmployeeIndex()
        {
            var result1 = _employee.GetEmployees();
            return View(result1);
        }


        [HttpGet]
        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = _employee.GetCity(stateId);
            return Json(cities);
        }

        [HttpGet]
        public IActionResult EmployeeAdd()
        {
            var states = _employee.GetStates();
            ViewBag.State = new SelectList(states, "StateId", "StateName");
            ViewBag.Cities = new List<SelectListItem>(); // Initially empty
            return View();
        }

        [HttpPost]
        public IActionResult EmployeeAdd(EmployeeDTO employeeDTO)
        {
            _employee.AddEmployee(employeeDTO);
            return RedirectToAction("EmployeeIndex", "Employee");
        }

    }
}
