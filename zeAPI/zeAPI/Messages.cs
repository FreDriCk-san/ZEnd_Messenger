using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace zeAPI
{
    public class Messages
    {
        public int Id { get; }
        public int ChatId { get; }
        public int UserId { get; }
        public string Text { get; }
        public DateTime Date { get; }
        bool IsRead { get; }

        [JsonConstructor]
        private Messages(int id, string text, int chatId, int userId, DateTime date, bool isRead)
        {
            Id = id;
            ChatId = chatId;
            UserId = userId;
            Text = text;
            Date = date;
            IsRead = isRead;
        }

        public bool Delete(Users user)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(Task.Run(async () =>
                {
                    var content = new Dictionary<string, string>();
                    content.Add("messageId", Id.ToString());
                    content.Add("chatId", ChatId.ToString());
                    content.Add("login", user.Login);
                    content.Add("password", user.Password);
                    var httpClient = new HttpClient();
                    var response = await httpClient.PostAsync(String.Format("{0}Messages/Delete", Resources.ServerURL), new FormUrlEncodedContent(content));
                    return await response.Content.ReadAsStringAsync();
                }).Result);
            }
            catch { }

            return false;
        }

    }

}
