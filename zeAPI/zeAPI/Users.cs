using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace zeAPI
{
    public class Users
    {
        public int Id { get; }
        public string Name { get; }
        internal string Email { get; }
        internal string Login { get; }
        internal string Password { get; }
        public string Avatar { get; }

        static void Main(string[] args)
        {
            //Delete this shit!!! Cause it's doing nothing
            
        }

        [JsonConstructor]
        private Users(int id, string name, string email, string login, string password, string avatar)
        {
            Id = id;
            Name = name;
            Email = email;
            Login = login;
            Password = password;
            Avatar = avatar;
        }



        public static bool Register(string login, string password, string name, string email = null)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = new Dictionary<string, string>();
                        content.Add("Login", login);
                        content.Add("Password", password);
                        content.Add("Name", name);

                        if (null != email)
                        {
                            content.Add("Email", email);
                        }

                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.PostAsync(String.Format("{0}Users/Create", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }



        public static Users Authorization(string login, string password)
        {
            try
            {
                return JsonConvert.DeserializeObject<Users>(
                        Task.Run(async () =>
                        {
                            var httpClient = new HttpClient();
                            //FIX: How to take server URL
                            var response = await httpClient.GetAsync(String.Format("{0}Users/IsExists?Login={1}&Password={2}", "http://localhost:58040/", login, password));
                            return await response.Content.ReadAsStringAsync();
                        }).Result);
            }
            catch { }

            return null;
        }



        //public bool Delete()
        //{
        //    try
        //    {
        //        return JsonConvert.DeserializeObject<bool>(
        //            Task.Run(async () =>
        //            {
        //                var content = UserDictionary(this);
        //                content["IsDeleted"] = true.ToString();
        //                var httpClient = new HttpClient();
        //                //FIX: How to take server URL
        //                var response = await httpClient.PostAsync(String.Format("{0}users/edit", "http://localhost:58040/"), new FormUrlEncodedContent(content));
        //                return await response.Content.ReadAsStringAsync();
        //            }).Result);
        //    }
        //    catch { }

        //    return false;
        //}



        public static List<Users> FindUsers(string name, int? Start = null, int? Count = null)
        {
            var users = new List<Users>();
            try
            {
                users.AddRange(JsonConvert.DeserializeObject<Users[]>(
                        Task.Run(async () =>
                        {
                            var httpClient = new HttpClient();
                            //FIX: How to take server URL
                            var response = await httpClient.GetAsync(String.Format("{0}Users/FindUsers?Name={1}&Start={2}&Count={3}", "http://localhost:58040/", name, Start, Count));
                            return await response.Content.ReadAsStringAsync();
                        }).Result));
            }
            catch { }

            return users;
        }



        public static Users GetInfo(int id)
        {
            Users user = null;

            try
            {
                user = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(
                    Task.Run(async () =>
                    {
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.GetAsync(String.Format("{0}Users/Details?id={1}", "http://localhost:58040/", id));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return user;
        }



        public bool CreateChat(string name, ChatType type, HashSet<Users> users = null)
        {
            if (null == users)
            {
                users = new HashSet<Users>();
            }

            users.Add(this);

            switch (type)
            {
                case ChatType.Conversation: break;
                case ChatType.Dialog: break;
                case ChatType.PrivateDialog: break;
                case ChatType.Public: break;
            }

            Chats chat;
            try
            {
                chat = JsonConvert.DeserializeObject<Chats>(
                    Task.Run(async () => {
                        var content = new Dictionary<string, string>();
                        content.Add("Name", name);
                        content.Add("Type", "public");
                        content.Add("Creator", Id.ToString());
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.PostAsync(String.Format("{0}Chats/Create", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);

                if (null == chat)
                {
                    return false;
                }
            }

            catch { return false; }


            try
            {
                foreach (var user in users)
                {
                    Task.Run(async () =>
                    {
                        var content = new Dictionary<string, string>();
                        content.Add("UserId", user.Id.ToString());
                        content.Add("ChatId", chat.Id.ToString());
                        content.Add("CanWrite", "True");
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.PostAsync(String.Format("{0}UsersInChats/Create", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    });
                }
            }

            catch { }

            Connection.hubProxy.Invoke("newChat", chat, users.ToList());

            return true;
        }



        public bool LeaveFromChat(Chats chat)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = new Dictionary<string, string>();
                        content.Add("Login", Login);
                        content.Add("Password", Password);
                        content.Add("ChatId", chat.Id.ToString());
                        content.Add("userId", Id.ToString());
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.PostAsync(String.Format("{0}usersInChats/delete", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }



        public List<Chats> GetChats(int? Start = null, int? Count = null)
        {
            var chats = new List<Chats>();
            try
            {
                chats.AddRange(JsonConvert.DeserializeObject<Chats[]>(
                    Task.Run(async () => {
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.GetAsync(String.Format("{0}Chats/UserChats?UserId={1}&Start={2}&Count={3}", "http://localhost:58040/", Id, Start, Count));
                        return await response.Content.ReadAsStringAsync();
                    }).Result));
            }
            catch { }

            return chats;
        }



        public List<Messages> GetMessages(Chats chat, int? Start = null, int? Count = null)
        {
            var messages = new List<Messages>();
            try
            {
                messages.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<Messages[]>(
                    Task.Run(async () => {
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.GetAsync(String.Format("{0}Messages/ChatMessages?ChatId={1}&UserId={2}&Start={3}&Count={4}", "http://localhost:58040/", chat.Id, Id, Start, Count));
                        return await response.Content.ReadAsStringAsync();
                    }).Result));
            }
            catch { }

            return messages;
        }



        public bool ChangePassword(string password)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = UserDictionary(this);
                        content["Password"] = password;
                        content.Add("oldPassword", Password);
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.PostAsync(String.Format("{0}Users/Edit", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }



        public bool ChangeName(string name)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = UserDictionary(this);
                        content["Name"] = name;
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.PostAsync(String.Format("{0}Users/Edit", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }



        public bool SendMessage(string text, Chats chat)
        {
            Messages message = null;
            try
            {
                message = JsonConvert.DeserializeObject<Messages>(Task.Run(async () =>
                {
                    var content = new Dictionary<string, string>();
                    content.Add("Text", text);
                    content.Add("ChatId", chat.Id.ToString());
                    content.Add("UserId", Id.ToString());
                    var httpClient = new HttpClient();
                    //FIX: How to take server URL
                    var response = await httpClient.PostAsync(String.Format("{0}Messages/Create", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                    return await response.Content.ReadAsStringAsync();
                }).Result);
            }
            catch { }

            return false;
        }



        public bool DeleteMessage(Messages message)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = new Dictionary<string, string>();
                        content.Add("MessageId", message.Id.ToString());
                        content.Add("UserId", Id.ToString());
                        var httpClient = new HttpClient();
                        //FIX: How to take server URL
                        var response = await httpClient.PostAsync(String.Format("{0}deletedMessages/create", "http://localhost:58040/"), new FormUrlEncodedContent(content));
                        return await response.Content.ReadAsStringAsync();
                    }).Result);
            }
            catch { }

            return false;
        }



        internal static Dictionary<string, string> UserDictionary(Users user)
        {
            var output = new Dictionary<string, string>();
            output.Add("Id", user.Id.ToString());
            output.Add("Email", user.Email);
            output.Add("Login", user.Login);
            output.Add("Password", user.Password);
            output.Add("Name", user.Name);
            output.Add("Avatar", user.Avatar);
            return output;
        }
    }
}
