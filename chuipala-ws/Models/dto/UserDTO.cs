using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chuipala_ws.Models
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        
        public bool IsStudent { get; set; }
        public bool IsProfessor { get; set; }
    }
}