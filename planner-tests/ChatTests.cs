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
            var role = new Role(new[] {Permission.CreateChat });
            var user = new User("TestUser", role);
            user.Login();
            var chatDao = new ChatDao();
            var chatService = new ChatService(new HttpContext(), chatDao);
            var chatId = chatService.NewChat(user);
            var chat = chatDao.GetChatById(chatId);
            Assert.That(chat.CreatedBy, Is.EqualTo(user.Username));
        }

        [Test]
        public void TestAddingThreadToChat()
        {
            var role = new Role(new[] { Permission.CreateChat, Permission.CreateThread });
            var user = new User("TestUser", role);
            user.Login();
            var chatDao = new ChatDao();
            var httpContext = new HttpContext();
            var chatService = new ChatService(httpContext, chatDao);
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
    }
}