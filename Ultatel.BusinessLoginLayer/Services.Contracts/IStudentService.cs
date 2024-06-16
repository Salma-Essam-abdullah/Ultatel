using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IStudentService
    {
        Task<Response> AddStudentAsync(StudentDto model);
        Task<Response> UpdateStudentAsync(StudentDto model);
        Task<Response> DeleteStudentAsync(int studentId);

        Task<Response> ShowStudentAsync(int studentId);


    }
}
