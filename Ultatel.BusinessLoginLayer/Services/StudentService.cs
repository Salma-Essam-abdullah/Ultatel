using AutoMapper;
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




       public async Task<Pagination<StudentDto>> ShowAllStudentsAsync(int pageIndex, int pageSize, string? sortBy, bool isDescending)
        {
           
                var students = await _unitOfWork._studentRepositoryNonGeneric.GetAllStudentsAsync(pageIndex, pageSize,sortBy,isDescending);
                if (students == null || !students.Any())
                {
                    throw new Exception("No Students Found");
                }

                var totalCount = await _unitOfWork._studentRepository.CountAsync();
                var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

                return new Pagination<StudentDto>(pageIndex, pageSize, totalCount, studentDtos.ToList());

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
                    student = null
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
                    student = null
                };
            }

            return new StudentResponse
            {
                Message = "Success",
                isSucceeded = true,
                Errors = null,
                student = student
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


        public async Task<StudentResponse> UpdateStudentAsync(Guid studentId, UpdateStudentDto model)
        {
            if (studentId == Guid.Empty)
            {
                return new StudentResponse
                {
                    Message = "Invalid",
                    isSucceeded = false,
                    Errors = new Dictionary<string, string> { { "studentId", "The provided student ID is empty." } },
                    student = null
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
                    student = null
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
                    student = null
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

            //var std = await _unitOfWork._studentRepository.UpdateAsync(studentToUpdate);

            //var studentLogsDto = new StudentLogsDto
            //{
            //    StudentId = studentToUpdate.Id,
            //    Operation = "updated",
            //    OperationTime = DateTime.Now,
            //    AdminId = studentToUpdate.AdminId,
            //};
            //var studentLogs = _mapper.Map<StudentLogs>(studentLogsDto);
            //await _unitOfWork._studentLogsRepository.AddAsync(studentLogs);

            return new StudentResponse
            {
                Message = "Student updated successfully",
                isSucceeded = true,
                Errors = null,
                student = studentToUpdate
            };
        }



        public async Task<IEnumerable<StudentDto>> SearchStudentsAsync(StudentSearchDto searchDto)
        {
            var students = await _unitOfWork._studentRepositoryNonGeneric.SearchStudentsAsync(
                searchDto.Name,
                searchDto.AgeFrom,
                searchDto.AgeTo,
                searchDto.Country,
                searchDto.Gender
            );
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }


        public async Task<IEnumerable<StudentLogsDto>> ShowStudentLogs(Guid studentId)
        {

            try
            {
                var studentLogs = await _unitOfWork._studentLogsRepositoryNonGeneric.GetStudentLogs(studentId);
             
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
