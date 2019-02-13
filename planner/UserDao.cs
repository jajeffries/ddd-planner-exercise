using System.IO;
using Newtonsoft.Json;
using Planner;

namespace planner
{
    public class UserDao
    {
        private readonly string FilePath;

        public UserDao()
        {
            this.FilePath = Directory.GetCurrentDirectory();
        }

        public User GetUserByUsername(string username)
        {
            var input = File.ReadAllText($"{FilePath}\\{username}.json");
            return JsonConvert.DeserializeObject<User>(input);
        }

        public void SaveUser(User user)
        {
            var output = JsonConvert.SerializeObject(user);
            File.WriteAllText($"{FilePath}\\{user.Username}.json", output);
        }
    }
}