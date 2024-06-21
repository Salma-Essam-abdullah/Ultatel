using AutoMapper;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;
using Microsoft.Extensions.Logging;
using Ultatel.BusinessLoginLayer.Helpers;
using Ultatel.DataAccessLayer.Repositories;

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

               var std =  await _unitOfWork._studentRepository.AddAsync(student);

                var studentLogsDto = new StudentLogsDto
                {
                    StudentId = student.Id,
                    Operation = "added",
                    OperationTime = DateTime.Now,
                    UserId = student.AppUserId,
                };
                var studentLogs = _mapper.Map<StudentLogs>(studentLogsDto);
              
                await _unitOfWork._studentLogsRepository.AddAsync(studentLogs);

                return new Response
                {
                    Message = "Student registered successfully",
                    isSucceeded = true,
                    std = std,
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


        public async Task<Pagination<StudentDto>> ShowAllStudentsAsync(int pageIndex, int pageSize)
        {
            try
            {
                var students = await _unitOfWork._studentRepository.GetAllAsync(pageIndex, pageSize);
                if (students == null || !students.Any())
                {
                    throw new Exception("No Students Found");
                }

                var totalCount = await _unitOfWork._studentRepository.CountAsync();
                var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

                return new Pagination<StudentDto>(pageIndex, pageSize, totalCount, studentDtos.ToList());

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


        public async Task<Response> UpdateStudentAsync(int studentId, UpdateStudentDto model)
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

               var std = await _unitOfWork._studentRepository.UpdateAsync(studentToUpdate);

                var studentLogsDto = new StudentLogsDto
                {
                    StudentId = studentToUpdate.Id,
                    Operation = "updated",
                    OperationTime = DateTime.Now,
                    UserId = studentToUpdate.AppUserId,
                };
                var studentLogs = _mapper.Map<StudentLogs>(studentLogsDto);
                var updatedStudentDto = _mapper.Map<UpdateStudentDto>(studentToUpdate);
                await _unitOfWork._studentLogsRepository.AddAsync(studentLogs);

                   return new Response
                {
                    Message = "Student updated successfully",
                    isSucceeded = true,
                    std = std,
                }; ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the student.");
                throw new Exception("An error occurred while updating the student.", ex);
            }
        }

        public async Task<Pagination<StudentDto>> ShowAllStudentsByUserId(string userId, int pageIndex, int pageSize)
        {   
            try
            {
                var students = await _unitOfWork._studentRepositoryN.GetStudentsByUserIdAsync(userId,pageIndex,pageSize);

                if (students == null || !students.Any())
                {
                    throw new Exception("No Students Found for the specified User");
                }

                var totalCount = await _unitOfWork._studentRepositoryN.CountAsyncByUserId(userId);
                var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

                return new Pagination<StudentDto>(pageIndex, pageSize, totalCount, studentDtos.ToList());
           
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching students data", ex);
            }
        }







        public async Task<IEnumerable<StudentDto>> SearchStudentsAsync(StudentSearchDto searchDto)
        {
            var students = await _unitOfWork._studentRepositoryN.SearchStudentsAsync(
                searchDto.Name,
                searchDto.AgeFrom,
                searchDto.AgeTo,
                searchDto.Country,
                searchDto.Gender
            );
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }


        public async Task<IEnumerable<StudentLogsDto>> ShowStudentLogs(int studentId)
        {
            if (studentId == 0)
            {
                throw new ArgumentNullException(nameof(studentId), "Student ID cannot be zero");
            }

            try
            {
                var studentLogs = await _unitOfWork._studentLogsRepositoryN.GetStudentLogs(studentId);
             
                if (studentLogs == null || !studentLogs.Any())
                {
                    throw new Exception("Student Logs not found");
                }

                var studentLogsDto = _mapper.Map<IEnumerable<StudentLogsDto>>(studentLogs);
                return studentLogsDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching student logs data", ex);
            }
        }


    }
}
