using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageHighSchool.Models
{
    public class Admin : BaseModel
    {
        
        public string FullName { get; set; }
        public string Code { get; set; } = "admin";

    }
}
