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

        public static bool Register(string name, string login, string password, string email = null)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(
                    Task.Run(async () =>
                    {
                        var content = new Dictionary<string, string>();
                        content.Add("Name", name);
                        content.Add("Login", login);
                        content.Add("Password", password);
                        if (email != null)
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
