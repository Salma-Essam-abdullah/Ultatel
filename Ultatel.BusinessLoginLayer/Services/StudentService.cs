using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;

namespace Ultatel.BusinessLoginLayer.Services
{
    public class StudentService : IStudentService
    {
        public Task<Response> AddStudentAsync(StudentDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteStudentAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> ShowStudentAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateStudentAsync(StudentDto model)
        {
            throw new NotImplementedException();
        }
    }
}
