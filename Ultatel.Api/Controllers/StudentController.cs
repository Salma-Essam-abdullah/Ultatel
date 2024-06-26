﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;


namespace Ultatel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddStudent(StudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new StudentResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    )
                });
            }

            var result = await _studentService.AddStudentAsync(model, User);

            if (result.isSucceeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



        [Authorize(Roles = "Admin,superAdmin")]
        [HttpGet("{studentId}")]
        public async Task<ActionResult> ShowStudent(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return BadRequest(new ValidationResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } }
                });
            }

            var result = await _studentService.ShowStudentAsync(studentId);

            if (result == null)
            {
                return NotFound(new ValidationResponse
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } }
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{studentId}")]
        public async Task<ActionResult> DeleteStudent(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return BadRequest(new ValidationResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } }
                });
            }

            var result = await _studentService.DeleteStudentAsync(studentId);

            if (result == null)
            {
                return NotFound(new ValidationResponse
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } }
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin,superAdmin")]
        [HttpGet]
        public async Task<ActionResult> ShowAllStudents(string sortBy = null, bool isDescending = false, int pageIndex = 1, int pageSize = 10)
        {


            var result = await _studentService.ShowAllStudentsAsync(pageIndex, pageSize, sortBy, isDescending);

            if (result == null)
            {
                return NotFound(new ValidationResponse
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "students", "No students found" } }
                });
            }

            return Ok(result);
        }
    

        [Authorize(Roles = "Admin")]
        [HttpPatch("{studentId}")]
        public async Task<ActionResult> UpdateStudent(Guid studentId, [FromBody] UpdateStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationResponse
                {
                    Message = "InvalidProperties",
                    isSucceeded = false,
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    )
                });
            }

            var studentBeforeUpdate = await _studentService.ShowStudentAsync(studentId);
            if (studentBeforeUpdate == null)
            {
                return BadRequest(new ValidationResponse
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } }
                });
            }

            var updatedStudent = await _studentService.UpdateStudentAsync(studentId, model, User);

            if (!updatedStudent.isSucceeded)
            {
                return BadRequest(updatedStudent);
            }

            return Ok(updatedStudent);
        }
      

        [Authorize(Roles = "Admin,superAdmin")]
        [HttpPost("Search")]
        public async Task<IActionResult> Search(StudentSearchDto searchDto , string sortBy = null, bool isDescending = false, int pageIndex = 1, int pageSize = 10)
        {
            var students = await _studentService.SearchStudentsAsync(searchDto,pageIndex,pageSize,sortBy,isDescending);
            return Ok(students);
        }

        [Authorize(Roles = "superAdmin")]
        [HttpGet("Logs/{studentId}")]
        public async Task<ActionResult> ShowStudentLogs(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return BadRequest(new ValidationResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } }
                });
            }

            var result = await _studentService.ShowStudentLogs(studentId);

           

            return Ok(result);
        }



    }
}
