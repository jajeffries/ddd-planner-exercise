using System;
using Planner;

namespace planner
{
    public class UserService
    {
        private readonly UserDao userDao;

        public UserService(UserDao userDao)
        {
            this.userDao = userDao;
        }

        public void ChangeName(string userUsername, string firstName, string lastName)
        {
            var user = userDao.GetUserByUsername(userUsername);

            if (user.IsAuthenticated() && user.HasPermission(Permission.ChangeUserDetails))
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                userDao.SaveUser(user);
            }
            else
            {
                throw new Exception("Incorrect permissions");
            }
        }

        public void ChangeEmail(string username, string email)
        {
            var user = userDao.GetUserByUsername(username);

            if (user.IsAuthenticated() && user.HasPermission(Permission.ChangeUserDetails))
            {
                user.Email = email;
                userDao.SaveUser(user);
            }
            else
            {
                throw new Exception("Incorrect permissions");
            }
        }

        public void ChangeBillingAddress(string username, string billingAddress1, string billingAddress2,
            string billingAddressTown, string billingAddressPostcode)
        {
            var user = userDao.GetUserByUsername(username);

            if (user.IsAuthenticated() && user.HasPermission(Permission.ChangeUserDetails))
            {
                user.BillingAddress1 = billingAddress1;
                user.BillingAddress2 = billingAddress2;
                user.BillingTown = billingAddressTown;
                user.BillingPostcode = billingAddressPostcode;
                userDao.SaveUser(user);
            }
            else
            {
                throw new Exception("Incorrect permissions");
            }
        }
    }
}