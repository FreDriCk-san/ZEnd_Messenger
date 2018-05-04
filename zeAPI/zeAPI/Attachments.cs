using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace zeAPI
{
    public class Attachments
    {
        public int Id { get; }
        public string Link { get; }
        public string FileName { get; }
        public long FileSize { get; }
        internal long CDNId { get; }
        public int MessageId { get; }

        [JsonConstructor]
        private Attachments(int id, string link, long fileSize, long cdnId, int messageId)
        {
            Id = id;
            Link = link;
            FileName = link.Split('/').Last();
            FileSize = fileSize;
            CDNId = cdnId;
            MessageId = messageId;
        }

        public byte[] GetFileBytes()
        {
            byte[] output = null;
            try
            {
                output = (new HttpClient()).GetByteArrayAsync(Link).Result;
            }
            catch
            {

            }

            return output;
        }

        public static byte[] GetFileAsBytesAsync(string link)
        {
            try
            {
                return (new System.Net.Http.HttpClient()).GetByteArrayAsync(link).Result;
            }
            catch
            {

            }

            return null;
        }
    }
}
