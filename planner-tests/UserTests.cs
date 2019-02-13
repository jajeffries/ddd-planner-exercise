using System;
using System.Xml;
using NUnit.Framework;
using planner;
using Planner;

namespace Tests
{
    public class UserTests
    {
        [Test]
        public void TestUpdatingFirstNameLastName()
        {
            var role = new Role(new [] { Permission.ChangeUserDetails });
            var user = new User("Jane", "Doe", "JaneDoe", role);
            
            var userDao = new UserDao();
            user.Login();

            userDao.SaveUser(user);

            var userService = new UserService(userDao);

            userService.ChangeName(user.Username, "Joan", "Doh");

            user = userDao.GetUserByUsername(user.Username);

            Assert.That(user.FirstName, Is.EqualTo("Joan"));
            Assert.That(user.LastName, Is.EqualTo("Doh"));
        }
    }

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
    }
}