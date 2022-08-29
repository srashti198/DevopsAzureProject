using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Filters.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Filters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CustomAuthFilter]         //uncomment for using Custom Auth Filter
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext context;
        public EmployeeController(EmployeeDBContext context)
        {
            this.context = context;
        }


        private IEnumerable<Employee> GetStandaradEmployeeList()
        {
            return context.Employees;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(IReadOnlyCollection<Employee>))]
        public ActionResult<Employee> Get()
        {
            IEnumerable<Employee> employees = GetStandaradEmployeeList();
            throw new Exception("Exception arised");        //to demonstrate custom exception filter
            return Ok(employees);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee == null)
                return NotFound($"Employee with id: {id} not found");
            return Ok(employee);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee emp)
        {
            context.Employees.Add(emp);
            int rows = context.SaveChanges();
            if (rows == 1)
                return Ok(emp);
            return BadRequest("Add failed");

        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Employee emp)
        {
            context.Employees.Update(emp);
            int rows = context.SaveChanges();
            if (rows == 1)
                return Ok(emp);
            return BadRequest("Update failed");
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Employee emp = context.Employees.Find(id);
            if (emp == null)
                return NotFound($"Employee with id : {id} not found");
            context.Employees.Remove(emp);
            int rows = context.SaveChanges();
            if (rows == 1)
                return Ok(emp);
            return BadRequest("Delete Failed");
        }
    }
}
