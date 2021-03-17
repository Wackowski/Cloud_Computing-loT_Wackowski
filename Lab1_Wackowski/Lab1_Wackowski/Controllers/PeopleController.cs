using Lab1_Wackowski.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Lab1_Wackowski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        public PeopleDb db;

        public PeopleController(PeopleDb db)
        {
            this.db = db;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var people = db.People.ToList();
        
            return Ok(people);
        }
    }
    
}
