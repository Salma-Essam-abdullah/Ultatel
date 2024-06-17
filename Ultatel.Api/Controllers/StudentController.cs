using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.Models.Entities;

namespace Ultatel.Api.Controllers
{

    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }


        [HttpPost("AddStudent/{userId}")]
        public async Task<ActionResult> AddStudent(StudentDto model, string? userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response
                    {
                        Message = ErrorMsg.InvalidProperties,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                if (userId == null)
                {
                    return BadRequest(new Response
                    {
                        Message = ErrorMsg.InvalidProperties,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                model.AppUserId = userId;

                var result = await _studentService.AddStudentAsync(model);

                if (result.isSucceeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMsg.AnErrorOccured);
                return StatusCode(500, new Response
                {
                    Message = ErrorMsg.GeneralErrorMsg,
                    isSucceeded = false,
                    Errors = new[] { ex.Message }
                });
            }
        }




        [HttpGet("ShowStudent/{studentId}")]
        public async Task<ActionResult> ShowStudent(int studentId)
        {
            try
            {
                if (studentId == 0)
                {
                    return BadRequest(new Response
                    {
                        Message = ErrorMsg.InvalidProperties,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var result = await _studentService.ShowStudentAsync(studentId);

                if (result == null)
                {
                    return NotFound(new Response
                    {
                        Message = ErrorMsg.NotFound,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMsg.NotFound);
                return StatusCode(500, new Response
                {
                    Message = ErrorMsg.GeneralErrorMsg,
                    isSucceeded = false,
                    Errors = new[] { ex.Message }
                });
            }
        }



        [HttpDelete("DeleteStudent/{studentId}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            try
            {
                if (studentId == 0)
                {
                    return BadRequest(new Response
                    {
                        Message = ErrorMsg.InvalidProperties,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var result = await _studentService.DeleteStudentAsync(studentId);

                if (result == null)
                {
                    return NotFound(new Response
                    {
                        Message = ErrorMsg.NotFound,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMsg.AnErrorOccured);
                return StatusCode(500, new Response
                {
                    Message = ErrorMsg.GeneralErrorMsg,
                    isSucceeded = false,
                    Errors = new[] { ex.Message }
                });
            }
        }




        [HttpGet("ShowStudent")]
        public async Task<ActionResult> ShowAllStudents()
        {
            try
            {


                var result = await _studentService.ShowAllStudentsAsync();

                if (result == null)
                {
                    return NotFound(new Response
                    {
                        Message = ErrorMsg.NotFound,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }


                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMsg.NotFound);
                return StatusCode(500, new Response
                {
                    Message = ErrorMsg.GeneralErrorMsg,
                    isSucceeded = false,
                    Errors = new[] { ex.Message }
                });
            }
        }


        [HttpPatch("UpdateStudent/{studentId}")]
        public async Task<ActionResult> UpdateStudent(int studentId, [FromBody] UpdateStudentDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response
                    {
                        Message = ErrorMsg.InvalidProperties,
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                var studentBeforeUpdate = await _studentService.ShowStudentAsync(studentId);
                if (studentBeforeUpdate == null)
                {
                    return BadRequest(new Response
                    {
                        Message = ErrorMsg.NotFound,
                        isSucceeded = false,
                        Errors = new[] { "Student not found." }
                    });
                }

                var updatedStudent = await _studentService.UpdateStudentAsync(studentId, model);

                return Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMsg.AnErrorOccured);
                return StatusCode(500, new Response
                {
                    Message = ErrorMsg.GeneralErrorMsg,
                    isSucceeded = false,
                    Errors = new[] { ex.Message }
                });
            }
        }

    }
}