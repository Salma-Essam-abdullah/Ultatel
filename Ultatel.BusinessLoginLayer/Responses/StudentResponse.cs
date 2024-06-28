using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Responses
{
    public class StudentResponse
    {
        public string Message { get; set; }
        public bool isSucceeded { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public Student student { get; set; }
        public List<StudentDto> Students { get; set; }

    }
}
