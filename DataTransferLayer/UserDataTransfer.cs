using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer
{
    public class UserDataTransfer
    {

        public int ID { get; set; }

        [Required(ErrorMessage="Username is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        
    }
}
