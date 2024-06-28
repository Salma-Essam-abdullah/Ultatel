using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Responses
{
    public class StudentResponse:ValidationResponse
    {
    
        public StudentDto studentDto {  get; set; }
        public Student student { get; set; }
        public List<StudentDto> Students { get; set; }

    }
}
