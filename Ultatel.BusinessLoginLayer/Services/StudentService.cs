using AutoMapper;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Ultatel.BusinessLoginLayer.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _logger = logger;
        }
        public async Task<Response> AddStudentAsync(StudentDto model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), ErrorMsg.NullModel);
                }

                var student = _mapper.Map<Student>(model);
                await _unitOfWork._studentRepository.AddAsync(student);

                return new Response
                {
                    Message = "Student registered successfully",
                    isSucceeded = true
                };
            }
            catch (ArgumentNullException argEx)
            {


                return new Response
                {
                    Message = argEx.Message,
                    isSucceeded = false,
                    Errors = new[] { argEx.InnerException?.Message ?? argEx.Message }
                };
            }
            catch (Exception ex)
            {


                return new Response
                {
                    Message = "An error occurred while registering the student.",
                    isSucceeded = false,
                    Errors = new[] { ex.InnerException?.Message ?? ex.Message }
                };
            }
        }



        public async Task<Response> DeleteStudentAsync(int studentId)
        {
            try
            {
                if (studentId == 0)
                {
                    throw new ArgumentNullException(ErrorMsg.NullModel);
                }
                var model = await _unitOfWork._studentRepository.GetByIdAsync(studentId);
                if (model == null)
                {
                    return new Response
                    {
                        Message = ErrorMsg.NotFound,
                        isSucceeded = false,

                    };
                }

                await _unitOfWork._studentRepository.DeleteAsync(studentId);

                return new Response
                {
                    Message = "Student Deleted successfully",
                    isSucceeded = true
                };
            }
            catch (ArgumentNullException argEx)
            {


                return new Response
                {
                    Message = argEx.Message,
                    isSucceeded = false,
                    Errors = new[] { argEx.InnerException?.Message ?? argEx.Message }
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = "An error occurred while deleting the student.",
                    isSucceeded = false,
                    Errors = new[] { ex.InnerException?.Message ?? ex.Message }
                };
            }
        }


        public async Task<IEnumerable<StudentDto>> ShowAllStudentsAsync()
        {
            try
            {
                var student = await _unitOfWork._studentRepository.GetAllAsync();
                if (student == null)
                {
                    throw new Exception("No Students Found");
                }

                IEnumerable<StudentDto> studentDto = _mapper.Map<IEnumerable<StudentDto>>(student);
                return studentDto;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching students data", ex);
            }
        }


        public async Task<StudentDto> ShowStudentAsync(int studentId)
        {
            if (studentId == 0)
            {
                throw new ArgumentNullException(nameof(studentId), "Student ID cannot be zero");
            }

            try
            {
                var student = await _unitOfWork._studentRepository.GetByIdAsync(studentId);
                if (student == null)
                {
                    throw new Exception("Student not found");
                }

                var studentDto = _mapper.Map<StudentDto>(student);
                return studentDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching student data", ex);
            }
        }


        public async Task<UpdateStudentDto> UpdateStudentAsync(int studentId, UpdateStudentDto model)
        {
            try
            {
                var studentToUpdate = await _unitOfWork._studentRepository.GetByIdAsync(studentId);
                if (studentToUpdate == null)
                {
                    throw new Exception(ErrorMsg.NotFound);
                }

                foreach (var property in typeof(UpdateStudentDto).GetProperties())
                {
                    var value = property.GetValue(model);
                    if (value != null)
                    {
                        var studentProperty = typeof(Student).GetProperty(property.Name);
                        studentProperty.SetValue(studentToUpdate, value);
                    }
                }

                await _unitOfWork._studentRepository.UpdateAsync(studentToUpdate);

                var updatedStudentDto = _mapper.Map<UpdateStudentDto>(studentToUpdate);

                return updatedStudentDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the student.");
                throw new Exception("An error occurred while updating the student.", ex);
            }
        }
    }
    }
