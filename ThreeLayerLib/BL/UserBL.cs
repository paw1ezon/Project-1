using Persistence;
using DAL;

namespace BL
{
    public class UserBL
    {
        private UserDAL uDAL = new UserDAL();
        public int Authorize(string userName, string password)
        {
            User user = new User();
            user = uDAL.GetUserAccount(userName);
            if (user.Password == password)
            {
                if (user.Role == 1) return 1;
                else if ( user.Role == 2) return 2;
                else return -1;
            }
            return -1;
        }
        public int GetUserIDByUserName(string userName)
        {
            User user = new User();
            user = uDAL.GetUserAccount(userName);
            return user.ID;
        }
    }
    
}