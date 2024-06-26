﻿using AutoMapper;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;
using Ultatel.BusinessLoginLayer.Helpers;
using System.Security.Claims;


namespace Ultatel.BusinessLoginLayer.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateStudentValidator _updateStudentValidator;
        private readonly StudentValidator _validator;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, StudentValidator validator, UpdateStudentValidator updateStudentValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _updateStudentValidator = updateStudentValidator;
        }
        public async Task<StudentResponse> AddStudentAsync(StudentDto model, ClaimsPrincipal user)
        {
            if (model == null)
            {
                return new StudentResponse
                {
                    Message = "NullModel",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "model", "The provided model is null." } }
                };
            }

            var errors = _validator.Validate(model);
            if (errors.Count > 0)
            {
                return new StudentResponse
                {
                    Message = "ValidationError",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            if (await EmailExistsAsync(model.Email))
            {
                return new StudentResponse
                {
                    Message = "ValidationError",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "email", "A student with this email already exists." } }
                };
            }

            var student = _mapper.Map<Student>(model);

            var userGuidClaim = user.FindFirst("UserGuid");
            if (userGuidClaim == null || string.IsNullOrEmpty(userGuidClaim.Value) || !Guid.TryParse(userGuidClaim.Value, out var adminId))
            {
                return new StudentResponse
                {
                    Message = "AdminIdError",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "adminId", "Invalid admin ID." } }
                };
            }

            var admin = await _unitOfWork._adminRepository.GetByIdAsync(adminId);
            if (admin == null)
            {
                return new StudentResponse
                {
                    Message = "AdminNotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "admin", "Admin not found." } }
                };
            }

            student.Admins.Add(admin);

            var std = await _unitOfWork._studentRepository.AddAsync(student);

            var studentLog = new StudentLogs
            {
                StudentId = std.Id,
                CreateAdminId = adminId,
                CreateTimeStamps = DateTime.Now,
            };
            await _unitOfWork._studentLogsRepository.AddAsync(studentLog);



            var studentDto = _mapper.Map<StudentDto>(std);

            return new StudentResponse
            {
                Message = "Student registered successfully",
                isSucceeded = true,
                studentDto = studentDto
            };
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _unitOfWork._studentRepositoryNonGeneric.AnyAsync(s => s.Email == email);
        }



        public async Task<ValidationResponse> DeleteStudentAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return new ValidationResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } }
                };
            }

            var model = await _unitOfWork._studentRepository.GetByIdAsync(studentId);
            if (model == null)
            {
                return new ValidationResponse
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } }
                };
            }

            await _unitOfWork._studentRepository.DeleteAsync(studentId);

            return new ValidationResponse
            {
                Message = "Student deleted successfully",
                isSucceeded = true
            };
        }




       public async Task<StudentResponse> ShowAllStudentsAsync(int pageIndex, int pageSize, string? sortBy, bool isDescending)
        {
           
                var students = await _unitOfWork._studentRepositoryNonGeneric.GetAllStudentsAsync(pageIndex, pageSize,sortBy,isDescending);
                if (students == null || !students.Any())
                {
                    throw new Exception("No Students Found");
                }

                var totalCount = await _unitOfWork._studentRepository.CountAsync();
                var studentsList = _mapper.Map<IEnumerable<StudentDto>>(students);

                var studentDtos =  new Pagination<StudentDto>(pageIndex, pageSize, totalCount, studentsList.ToList());

            return new StudentResponse
            {
                Message = "Student data retreived successfully",
                isSucceeded = true,
                StudentDtos = studentDtos
            };




        }
          
        



        public async Task<StudentResponse> ShowStudentAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return new StudentResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } },
                    
                };
            }

            var student = await _unitOfWork._studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                return new StudentResponse
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } },
                   
                };
            }
            var std = _mapper.Map<StudentDto>(student);
            return new StudentResponse
            {
                Message = "Success",
                isSucceeded = true,
                Errors = null,
                studentDto = std
            };
        }
        public async Task<bool> EmailUpdateExistsAsync(string newEmail, string currentEmail)
        {
            if (newEmail == currentEmail)
            {
                return false;
            }

            return await _unitOfWork._studentRepositoryNonGeneric.AnyAsync(s => s.Email == newEmail);
        }


        public async Task<StudentResponse> UpdateStudentAsync(Guid studentId, UpdateStudentDto model, ClaimsPrincipal user)
        {
            if (studentId == Guid.Empty)
            {
                return new StudentResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } },
                    
                };
            }

            var studentToUpdate = await _unitOfWork._studentRepository.GetByIdAsync(studentId);
            if (studentToUpdate == null)
            {
                return new StudentResponse
                {
                    Message = "NotFound",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "Student not found." } },
                };
            }

            if (await EmailUpdateExistsAsync(model.Email, studentToUpdate.Email))
            {
                return new StudentResponse
                {
                    Message = "ValidationError",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "email", "A student with this email already exists." } }
                };
            }

            var validationErrors = _updateStudentValidator.Validate(model);
            if (validationErrors.Any())
            {
                return new StudentResponse
                {
                    Message = "ValidationFailed",
                    isSucceeded = false,
                    Errors = validationErrors,
                   
                };
            }

            foreach (var property in typeof(UpdateStudentDto).GetProperties())
            {
                var value = property.GetValue(model);
                if (value != null)
                {
                    var studentProperty = typeof(Student).GetProperty(property.Name);
                    if (studentProperty != null)
                    {
                        studentProperty.SetValue(studentToUpdate, value);
                    }
                }
            }


            var userGuidClaim = user.FindFirst("UserGuid");
            if (userGuidClaim == null || string.IsNullOrEmpty(userGuidClaim.Value) || !Guid.TryParse(userGuidClaim.Value, out var adminId))
            {
                return new StudentResponse
                {
                    Message = "AdminIdError",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "adminId", "Invalid admin ID." } }
                };
            }


            var studentlogsToUpdate = await _unitOfWork._studentLogsRepositoryNonGeneric.GetStudentLogsById(studentId);
            studentlogsToUpdate.UpdateTimeStamps = DateTime.Now;
            studentlogsToUpdate.UpdateAdminId = adminId;

            await _unitOfWork._studentLogsRepository.UpdateAsync(studentlogsToUpdate);


            var stdUpdated = await _unitOfWork._studentRepository.UpdateAsync(studentToUpdate);

            var studentDto = _mapper.Map<StudentDto>(stdUpdated);
            return new StudentResponse
            {
                Message = "Student updated successfully",
                isSucceeded = true,
                Errors = null,
                studentDto = studentDto
            };
        }



        public async Task<StudentResponse> SearchStudentsAsync(StudentSearchDto searchDto, int pageIndex, int pageSize, string? sortBy, bool isDescending)
        {
            var students = await _unitOfWork._studentRepositoryNonGeneric.SearchStudentsAsync(
                searchDto.Name,
                searchDto.AgeFrom,
                searchDto.AgeTo,
                searchDto.Country,
                searchDto.Gender,
                pageIndex, pageSize, sortBy, isDescending
            );

            var totalCount = await _unitOfWork._studentRepositoryNonGeneric.CountStudentsAsync(
                searchDto.Name,
                searchDto.AgeFrom,
                searchDto.AgeTo,
                searchDto.Country,
                searchDto.Gender
            );

            var studentsList = _mapper.Map<IEnumerable<StudentDto>>(students);

            var studentDtos = new Pagination<StudentDto>(pageIndex, pageSize, totalCount, studentsList.ToList());
            return new StudentResponse
            {
                Message = "Student search completed successfully",
                isSucceeded = true,
                Errors = null,
                StudentDtos = studentDtos
            };
        }



        public async Task <StudentLogsDto> ShowStudentLogs(Guid studentId)
        {

                var studentLogs = await _unitOfWork._studentLogsRepositoryNonGeneric.GetStudentLogsById(studentId);
             
       
                var studentLogsDto = _mapper.Map<StudentLogsDto>(studentLogs);
                return studentLogsDto;
            
        }

        


    }
}
