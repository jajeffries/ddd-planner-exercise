using System;
using System.Linq;
using Planner;

namespace planner
{
    public class ChatService
    {
        private readonly ChatDao chatDao;
        private readonly HttpContext httpContext;
        private readonly UserDao userDao;

        public ChatService(HttpContext httpContext, ChatDao chatDao) : this(httpContext, chatDao, null)
        {
        }

        public ChatService(HttpContext httpContext, ChatDao chatDao, UserDao userDao)
        {
            this.httpContext = httpContext;
            this.chatDao = chatDao;
            this.userDao = userDao;
        }

        public Guid NewChat(User user)
        {
            if (user.IsAuthenticated() && user.HasPermission(Permission.CreateChat))
            {
                var chatId = Guid.NewGuid();
                var chat = new Chat(chatId, user.Username);
                chatDao.SaveChat(chat);
                return chatId;
            }
            else
            {
                throw new Exception("User does not have permission to add a chat");
            }
        }

        public void AddThread(Guid chatId, User user, string username, string title, string message)
        {
            if (user == null)
            {
                throw new Exception("User can't be null");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Username can't be null or empty");
            }

            if (string.IsNullOrEmpty(title) || title.Length > 250)
            {
                throw new Exception("Invalid title length");
            }

            if (string.IsNullOrEmpty(message) || message.Length > 1000)
            {
                throw new Exception("Invalid message length");
            }

            if (user.IsAuthenticated() && user.HasPermission(Permission.DeleteThread))
            {
                var chat = chatDao.GetChatById(chatId);
                chat.Threads.Add(new SoAgileThread(username, title, message));
                chatDao.SaveChat(chat);
                httpContext.Redirect(302, $"/{chatId}/threads/");
            }
            else
            {
                throw new Exception("User does not have permission to add a discussion.");
            }
        }

        public void AddCommentToThread(Guid threadId, Guid chatId, string message, string username)
        {
            var user = userDao.GetUserByUsername(username);

            if (user.IsAuthenticated() && user.HasPermission(Permission.AddMessage))
            {
                var chat = chatDao.GetChatById(chatId);
                var thread = chat.Threads.First(t => t.ThreadId.Equals(threadId));
                thread.Comments.Add(new Comment(username, message));
                chatDao.SaveChat(chat);
            }
            else
            {
                throw new Exception("User doesn't have permission to post comments.");
            }
        }

        public void DeleteThread(User user, Guid chatId, Guid threadId)
        {
            if (user.IsAuthenticated() && user.HasPermission(Permission.DeleteThread))
            {
                var chat = chatDao.GetChatById(chatId);
                var thread = chat.Threads.First(t => t.ThreadId.Equals(threadId));
                chat.Threads.Remove(thread);
                chatDao.SaveChat(chat);
            }
            else
            {
                throw new Exception("User doesn't have permission to remove threads.");
            }
        }
    }
}