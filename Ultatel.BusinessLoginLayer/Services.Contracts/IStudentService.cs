using System.Security.Claims;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Helpers;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IStudentService
    {
        Task<StudentResponse> AddStudentAsync(StudentDto model, ClaimsPrincipal user);
        Task<StudentResponse> UpdateStudentAsync(Guid studentId, UpdateStudentDto model);
        Task<ValidationResponse> DeleteStudentAsync(Guid studentId);
        Task<StudentResponse> ShowStudentAsync(Guid studentId);
        Task<StudentResponse> ShowAllStudentsAsync(int pageIndex, int pageSize, string? sortBy, bool isDescending);


        Task<StudentResponse> SearchStudentsAsync(StudentSearchDto searchDto, int pageIndex, int pageSize, string? sortBy, bool isDescending);

        Task<IEnumerable<StudentLogsDto>> ShowStudentLogs(Guid studentId);

        Task<bool> EmailExistsAsync(string email);
        Task<bool> EmailUpdateExistsAsync(string newEmail, string currentEmail);


    }

}
