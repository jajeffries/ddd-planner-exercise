using System;
using System.Linq;
using NUnit.Framework;
using planner;
using Planner;

namespace Tests
{
    public class ChatTests
    {
        [Test]
        public void TestAddingChat()
        {
            var role = new Role(new[] {Permission.CreateChat});
            var user = new User("TestUser", role);
            user.Login();
            var chatDao = new ChatDao();
            var chatService = new ChatService(new HttpContext(), chatDao, new UserDao());
            var chatId = chatService.NewChat(user);
            var chat = chatDao.GetChatById(chatId);
            Assert.That(chat.CreatedBy, Is.EqualTo(user.Username));
        }

        [Test]
        public void TestAddingThreadToChat()
        {
            var role = new Role(new[] {Permission.CreateChat, Permission.CreateThread});
            var user = new User("TestUser", role);
            user.Login();
            var chatDao = new ChatDao();
            var httpContext = new HttpContext();
            var chatService = new ChatService(httpContext, chatDao, new UserDao());
            var chatId = chatService.NewChat(user);

            const string title = "A new thread";
            const string message = "Let's discuss things...";

            chatService.AddThread(chatId, user, user.Username, title, message);

            var chat = chatDao.GetChatById(chatId);

            Assert.That(chat.CreatedBy, Is.EqualTo(user.Username));
            Assert.That(chat.Threads.First().Username, Is.EqualTo(user.Username));
            Assert.That(chat.Threads.First().Title, Is.EqualTo(title));
            Assert.That(chat.Threads.First().Message, Is.EqualTo(message));
            Assert.That(httpContext.Path, Is.EqualTo($"/{chatId}/threads/"));
        }

        [Test]
        public void TestAddThreadNullUser()
        {
            var role = new Role(new[] { Permission.CreateChat, Permission.CreateThread });
            var user = new User("TestUser", role);
            user.Login();
            var chatService = new ChatService(new HttpContext(), new ChatDao(), new UserDao());
            var chatId = chatService.NewChat(user);
            Assert.Throws<Exception>(() => chatService.AddThread(chatId, null, user.Username, "A new thread", "Let's discuss things..."));
        }

        [Test]
        public void TestAddThreadNullUsername()
        {
            var role = new Role(new[] { Permission.CreateChat, Permission.CreateThread });
            var user = new User("TestUser", role);
            user.Login();
            var chatService = new ChatService(new HttpContext(), new ChatDao(), new UserDao());
            var chatId = chatService.NewChat(user);
            Assert.Throws<Exception>(() => chatService.AddThread(chatId, user, null, "A new thread", "Let's discuss things..."));
        }

        [Test]
        public void TestAddThreadNullBigTitle()
        {
            var bigTitle = @"72I99cSDwYde35dfwEWQotonxqH0jnmgwmXYkWlzz5AXnmXeufiUSO3sosVeX6rHSK4XfgWS8RzTdkPIV1Kl5WydmfLu81jgWxI1KG7QvrGkpRBAicvFr42mVPjgMVK6q5jNBAfwEyWHChT5CYboNYXz0uApYjFuedbIo7he1hy8iUd2cttBXQ74LnDvPl7uoatL2iJwTuhtbAknQTfBcbEmLMZgK0lzWuDUri6KLArgglCchXGdfKQx14H";
            var role = new Role(new[] { Permission.CreateChat, Permission.CreateThread });
            var user = new User("TestUser", role);
            user.Login();
            var chatService = new ChatService(new HttpContext(), new ChatDao(), new UserDao());
            var chatId = chatService.NewChat(user);
            Assert.Throws<Exception>(() => chatService.AddThread(chatId, user, user.Username, bigTitle, "Let's discuss things..."));
        }

        [Test]
        public void TestAddThreadNullBigMessage()
        {
            var bigMessage =
                @"m5Aslui7JrlllllvPmJ3WN1U0RurLXQnr7ffGuOJZpHkoA3dTxE9F97dEgW81q9vbvCNQWhxWtwytGB2eOgULO8M9zSAoBBMa5DkuhrMEMp70S2ih114RNsWyOz4l23jwXkgZqwVf4rd6Qwxtavqop1gagWtmNiVHRmgTfYJHa7qKkJO5TkVagh7USR1Rl9o3T0jJAa2RO3zM7cQgugRne6Lwq6KsujXyeBpfZ6NKgrkhv7EJ8o1CjYDI2eKOaCdnoCInHPQpmjTiibClHzPVqBoLhHui1zT9k7lp9evhNj0IsNmAwAHxpr7cbqh5RpIlId7HClrpmQFo2NH9o9pddvqHV7ekzKZclKTIsm2p6eNhDcxULtQPiy4AuYDChqUaZhSpX3q8CtkdihwcIBecU9PKFA7lappbhlddlIqaNg4b3urRfAyKjhEc2P7NetMYn9AXjPWOAwHMBPyvrvD0HZ10w7ZW3G6zqRGu14LwnnOOCHNNqbJesD1IJkLNUI6gQe8wY3tlXXDkg65fHzJ0f7UE6YVQYrrzgI4N59Ml7i6l1roVlgEcTBlFTrAShHl9gnernqweg9eay421KbKEsVxeypY5w1dvOQzWsuRF1B0eDGkaXfsW9lI9jdQxiaBd9sIr3p69yfXXt1ljq6rimEmQAb9ydg4Gdg794qZ6HFl1oS2JTets1FWuMj4l0WCvo82IZbSx3Sn4uBj0zvCmptCaz9kElruEn2no3haXsffyvlhHlgmuN9DSSO9oWq7f4tEdYjQLPhsvwGs3eDahArfDjd54shPQGzYTKDYrPs54wADt1brZ7dQM211hTfrGHgKhl0dYILbD2qG1BOodrGA0lM95JExmXfMnmgPjuDspKFUxtzu20HVpDaxh8aCtf50iiWInCNFq2CAHwITjjhs2GnWXMTNQs4fRRacYrsS6oTr4CLcim80HrXIHUbURhnVC7JaOCqK8ZuTyEnejTQYkr3iz0TSAVfgqvJnQ";
            var role = new Role(new[] { Permission.CreateChat, Permission.CreateThread });
            var user = new User("TestUser", role);
            user.Login();
            var chatService = new ChatService(new HttpContext(), new ChatDao(), new UserDao());
            var chatId = chatService.NewChat(user);
            Assert.Throws<Exception>(() => chatService.AddThread(chatId, user, user.Username, "A new thread", bigMessage));
        }

        [Test]
        public void TestAddingCommentToThread()
        {
            var role = new Role(new[]
            {
                Permission.CreateChat,
                Permission.CreateThread,
                Permission.AddMessage
            });

            var user = new User("TestUser", role);
            user.Login();

            var chatDao = new ChatDao();
            var httpContext = new HttpContext();
            var chatService = new ChatService(httpContext, chatDao, new UserDao());
            var chatId = chatService.NewChat(user);

            const string title = "A new thread";
            const string message = "Let's discuss things...";

            chatService.AddThread(chatId, user, user.Username, title, message);

            var chat = chatDao.GetChatById(chatId);
            var thread = chat.Threads.First();

            var dev1 = new User("JaneDoe", role);
            var dev2 = new User("JohnDoe", role);
            dev1.Login();
            dev2.Login();

            var userDao = new UserDao();
            userDao.SaveUser(dev1);
            userDao.SaveUser(dev2);

            chatService.AddCommentToThread(thread.ThreadId, chatId, "Hello, world!", dev1.Username);
            chatService.AddCommentToThread(thread.ThreadId, chatId, "Foo, bar, baz.", dev2.Username);

            chat = chatDao.GetChatById(chatId);

            var comments = chat.Threads.First().Comments;

            Assert.That(comments[0].Username, Is.EqualTo("JaneDoe"));
            Assert.That(comments[0].Message, Is.EqualTo("Hello, world!"));
            Assert.That(comments[1].Username, Is.EqualTo("JohnDoe"));
            Assert.That(comments[1].Message, Is.EqualTo("Foo, bar, baz."));
        }
    }
}