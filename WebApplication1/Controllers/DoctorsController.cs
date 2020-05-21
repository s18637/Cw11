using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService service;
        public DoctorsController(IDbService _service)
        {
            service = _service;
        }

        [HttpGet("get")]
        public IActionResult GetDoctors()
        {
            MyHelper helper = service.GetDoctors();
            if (helper.status == 0)
            {
                return Ok(helper.doctors);
            }
            return NotFound(helper.Message);
        }
        [HttpPut("add")]
        public IActionResult AddDoctor(Doctor doctor)
        {
            MyHelper helper = service.AddDoctor(doctor);
            if (helper.status == 0)
            {
                return StatusCode((int)HttpStatusCode.Created);
            }
            return NotFound(helper.Message);
        }
        [HttpPut("update")]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            MyHelper helper = service.UpdateDoctor(doctor);
            if (helper.status == 0)
            {
                return Ok(helper.Message);
            }
            return NotFound(helper.Message);
        }
        [HttpDelete("Delete/{LastName}")]
        public IActionResult DeleteDoctor(string LastName)
        {
            MyHelper helper = service.DeleteDoctor(LastName);
            if (helper.status == 0)
            {
                return Ok(helper.Message);
            }
            return NotFound(helper.Message);
        }
    }
}