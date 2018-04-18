using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace zeAPI
{
    public class Chats
    {
   
        public int Id { get; }
        public int Creator { get; }
        public string Name { get; }
        public string Type { get; }
        public string Avatar { get; }

        [JsonConstructor]
        private Chats(int id, int creator, string name, string type, string avatar)
        {
            Id = id;
            Creator = creator;
            Name = name;
            Type = type;
            Avatar = avatar;

        }

        //public bool Create(string name)
        //{
        //    try
        //    {
        //        return JsonConvert.DeserializeObject<bool>(
        //            Task.Run(async () =>
        //            {
        //                var content = ChatDictionary(this);
        //                content["Name"] = name;
        //                var httpClient = new HttpClient();
        //                //FIX: How to take server URL
        //                var response = await httpClient.PostAsync(String.Format("{0}Chats/Create", "http://localhost:58040/"), new FormUrlEncodedContent(content));
        //                return await response.Content.ReadAsStringAsync();
        //            }).Result);
        //    }
        //    catch { }

        //    return false;
        //}

        public List<Users> GetUsers()
        {
            var users = new List<Users>();

            try
            {
                users.AddRange(JsonConvert.DeserializeObject<Users[]>(
                    Task.Run(async () => {
                        var httpClient = new HttpClient();
                        var response = await httpClient.GetAsync(String.Format("{0}Users/ChatUsers?ChatId={1}", Resources.ServerURL, Id));
                        return await response.Content.ReadAsStringAsync();
                    }).Result));
            }
            catch { }

            return users;
        }

        public bool ChangeName(string name)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = ChatDictionary(this);
                        content["Name"] = name;
                        var httpClient = new HttpClient();
                        var response = await httpClient.PostAsync(String.Format("{0}Chats/Edit", Resources.ServerURL), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }

        public static List<Chats> FindPublics(string name, int? start, int? count)
        {
            var chats = new List<Chats>();

            try
            {
                chats.AddRange(JsonConvert.DeserializeObject<Chats[]>(
                    Task.Run(async () => {
                        var httpClient = new HttpClient();
                        var response = await httpClient.GetAsync(String.Format("{0}Chats/FindPublics?Name={1}&Start={2}&Count={3}", Resources.ServerURL, name, start, count));
                        return await response.Content.ReadAsStringAsync();
                    }).Result));
            }
            catch { }

            return chats;
        }

        public bool DeleteUser(Users user, Users user1)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = new Dictionary<string, string>();
                        content.Add("ChatId", Id.ToString());
                        content.Add("UserId", user1.Id.ToString());
                        content.Add("Login", user.Login.ToString());
                        content.Add("Password", user.Password.ToString());
                        var httpClient = new HttpClient();
                        var response = await httpClient.PostAsync(String.Format("{0}UsersInChats/Delete", Resources.ServerURL), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }

        public bool Delete(Users user)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = new Dictionary<string, string>();
                        content.Add("ChatId", Id.ToString());
                        content.Add("Login", user.Login.ToString());
                        content.Add("Password", user.Password.ToString());
                        var httpClient = new HttpClient();
                        var response = await httpClient.PostAsync(String.Format("{0}Chats/Delete", Resources.ServerURL), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }

        internal static Dictionary<string, string> ChatDictionary(Chats chat)
        {
            var output = new Dictionary<string, string>();
            output.Add("Id", chat.Id.ToString());
            output.Add("Creator", chat.Creator.ToString());
            output.Add("Name", chat.Name);
            output.Add("Type", chat.Type);
            output.Add("Avatar", chat.Avatar);

            return output;
        }

    }
}
