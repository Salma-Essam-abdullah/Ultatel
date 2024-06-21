using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Responses
{
    public class Response
    {

        public string Message { get; set; }
        public bool isSucceeded { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public DateTime? ExpireDate { get; set; }

        public TokenResponse TokenData { get; set; }

        public Student std { get; set; }

    }
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
