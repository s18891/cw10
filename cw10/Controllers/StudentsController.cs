using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw10.DTOs.Requests;
using cw10.Models;
using cw10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private IStudentDbService _service;
        public StudentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_service.GetStudents());
        }


        [HttpPost]
        public IActionResult ModifyStudent(ModifyStudentRequest request)
        {
            try
            {
                Student student = _service.ModifyStudent(request);
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        public IActionResult DeleteStudent(DeleteStudentRequest request)
        {
            try
            {
                Student student = _service.DeleteStudent(request);
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
