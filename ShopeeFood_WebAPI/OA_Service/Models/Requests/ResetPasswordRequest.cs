using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
