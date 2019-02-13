using System;
using System.IO;
using Newtonsoft.Json;

namespace planner
{
    public class ChatDao
    {
        private readonly string FilePath;

        public ChatDao()
        {
            this.FilePath = Directory.GetCurrentDirectory();
        }

        public Chat GetChatById(Guid chatId)
        {
            var input = File.ReadAllText($"{FilePath}\\{chatId}.json");
            return JsonConvert.DeserializeObject<Chat>(input);
        }

        public void SaveChat(Chat chat)
        {
            var output = JsonConvert.SerializeObject(chat);
            File.WriteAllText($"{FilePath}\\{chat.Id}.json", output);
        }
    }
}