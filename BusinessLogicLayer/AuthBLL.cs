using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferLayer;

namespace BusinessLogicLayer
{
   public class AuthBLL
    {
        static AuthDAL Authentication = new AuthDAL();


        public UserDataTransfer Register(UserDataTransfer dto)
        {

            var user = Authentication.Register(dto);



            return user;
        }


        public UserDataTransfer Login(UserDataTransfer dto)
        {
            var user = Authentication.Login(dto);

            return user;
        }
    }
}
