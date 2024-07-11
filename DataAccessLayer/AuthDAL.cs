using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer;

namespace DataAccessLayer
{
    public class AuthDAL : FitnessTrackerContext
    {

       

        public UserDataTransfer Register(UserDataTransfer UserObj)
        {

            var user = new User
            {
                Username = UserObj.Username,
                Password = UserObj.Password
            };


            db_connect.Users.Add(user);
            db_connect.SaveChanges();

            return UserObj;
        }

        public UserDataTransfer Login(UserDataTransfer UserObj)
        {
            var DTO = new UserDataTransfer();

            var User = db_connect.Users.FirstOrDefault(x => x.Username == UserObj.Username && x.Password == UserObj.Password);


            if (User != null && User.UserID != 0)
            {
                DTO.Username = User.Username;
                DTO.ID = User.UserID;

                return DTO;
            }


            return UserObj;
        }
    }
}
