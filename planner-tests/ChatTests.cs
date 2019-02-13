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