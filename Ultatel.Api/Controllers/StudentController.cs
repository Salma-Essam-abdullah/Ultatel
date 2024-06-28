using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [HttpPost("AddStudent")]
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

            var userGuidClaim = User.FindFirst("UserGuid");
            string userGuidValue = userGuidClaim.Value;
            Guid adminId = Guid.Parse(userGuidValue);

            model.AdminId = adminId;

            var result = await _studentService.AddStudentAsync(model);

            if (result.isSucceeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin,superAdmin")]
        [HttpGet("ShowStudent/{studentId}")]
        public async Task<ActionResult> ShowStudent(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return BadRequest(new Response
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } }
                });
            }

            var result = await _studentService.ShowStudentAsync(studentId);

            if (result == null)
            {
                return NotFound(new Response
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } }
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteStudent/{studentId}")]
        public async Task<ActionResult> DeleteStudent(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return BadRequest(new Response
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } }
                });
            }

            var result = await _studentService.DeleteStudentAsync(studentId);

            if (result == null)
            {
                return NotFound(new Response
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } }
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin,superAdmin")]
        [HttpGet("ShowAllStudents")]
        public async Task<ActionResult> ShowAllStudents(int pageIndex = 1, int pageSize = 10)
        {
            var result = await _studentService.ShowAllStudentsAsync(pageIndex, pageSize);

            if (result == null || !result.Data.Any())
            {
                return NotFound(new Response
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "students", "No students found." } }
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("UpdateStudent/{studentId}")]
        public async Task<ActionResult> UpdateStudent(Guid studentId, [FromBody] UpdateStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
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
                return BadRequest(new Response
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } }
                });
            }

            var updatedStudent = await _studentService.UpdateStudentAsync(studentId, model);

            if (!updatedStudent.isSucceeded)
            {
                return BadRequest(updatedStudent);
            }

            return Ok(updatedStudent);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("User/ShowAllStudentsByUserId")]
        public async Task<ActionResult> ShowAllStudentsByUserId(int pageIndex = 1, int pageSize = 10)
        {
            var userGuidClaim = User.FindFirst("UserGuid");
            string userGuidValue = userGuidClaim.Value;
            Guid adminId = Guid.Parse(userGuidValue);

            var result = await _studentService.ShowAllStudentsByAdminId(adminId, pageIndex, pageSize);

            if (result == null)
            {
                return NotFound(new Response
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "students", "No students found for the specified admin." } }
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin,superAdmin")]
        [HttpPost("Search")]
        public async Task<IActionResult> Search(StudentSearchDto searchDto)
        {
            var students = await _studentService.SearchStudentsAsync(searchDto);
            return Ok(students);
        }

        [Authorize(Roles = "superAdmin")]
        [HttpGet("Logs/{studentId}")]
        public async Task<ActionResult> ShowStudentLogs(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return BadRequest(new Response
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } }
                });
            }

            var result = await _studentService.ShowStudentLogs(studentId);

            if (result == null || !result.Any())
            {
                return NotFound(new Response
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "logs", "No logs found for the given student ID." } }
                });
            }

            return Ok(result);
        }
    }
}
