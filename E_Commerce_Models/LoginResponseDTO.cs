using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Models
{
    public class LoginResponseDTO
    {
        public bool IsAuthSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
