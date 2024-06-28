using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Helpers;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IStudentService
    {
        Task<StudentResponse> AddStudentAsync(StudentDto model);
        Task<StudentResponse> UpdateStudentAsync(Guid studentId, UpdateStudentDto model);
        Task<Response> DeleteStudentAsync(Guid studentId);
        Task<StudentResponse> ShowStudentAsync(Guid studentId);
        Task<Pagination<StudentDto>> ShowAllStudentsAsync(int pageIndex, int pageSize);

        Task<Pagination<StudentDto>> ShowAllStudentsByAdminId(Guid adminId, int pageIndex, int pageSize);

        Task<IEnumerable<StudentDto>> SearchStudentsAsync(StudentSearchDto searchDto);

        Task<IEnumerable<StudentLogsDto>> ShowStudentLogs(Guid studentId);

        Task<bool> EmailExistsAsync(string email);
        Task<bool> EmailUpdateExistsAsync(string newEmail, string currentEmail);
    }
}
