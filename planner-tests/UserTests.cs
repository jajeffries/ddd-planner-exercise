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

        [Test]
        public void TestUpdatingEmail()
        {
            var role = new Role(new[] { Permission.ChangeUserDetails });
            var user = new User("Jane", "Doe", "JaneDoe", role);
            user.Email = "janedoe@email.com";

            var userDao = new UserDao();
            user.Login();

            userDao.SaveUser(user);

            var userService = new UserService(userDao);

            userService.ChangeEmail(user.Username, "joandoh@email.com");

            user = userDao.GetUserByUsername(user.Username);

            Assert.That(user.Email, Is.EqualTo("joandoh@email.com"));
        }

        [Test]
        public void TestUpdatingBillingAddress()
        {
            var role = new Role(new[] { Permission.ChangeUserDetails });
            var user = new User("Jane", "Doe", "JaneDoe", role);
            user.BillingAddress1 = "Address 1 Foo";
            user.BillingAddress2 = "Address 2 Foo";
            user.BillingTown = "Address Town Foo";
            user.BillingPostcode = "Address Postcode Foo";

            var userDao = new UserDao();
            user.Login();

            userDao.SaveUser(user);

            var userService = new UserService(userDao);

            userService.ChangeBillingAddress(user.Username, "Address 1 Bar", "Address 2 Bar", "Address Town Bar", "Address Postcode Bar");

            user = userDao.GetUserByUsername(user.Username);

            Assert.That(user.BillingAddress1, Is.EqualTo("Address 1 Bar"));
            Assert.That(user.BillingAddress2, Is.EqualTo("Address 2 Bar"));
            Assert.That(user.BillingTown, Is.EqualTo("Address Town Bar"));
            Assert.That(user.BillingPostcode, Is.EqualTo("Address Postcode Bar"));
        }
    }
}