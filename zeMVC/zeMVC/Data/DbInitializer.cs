using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zeMVC.Models;

namespace zeMVC.Data
{
    public class DbInitializer
    {

        public static void Initialize(MessangerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any userss.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{ Login="Fred", Password="123", Name="FreDriCk", Mail="fred@gmail.com", Avatar="", EnrollmentDate=DateTime.Parse("01-02-2018")},
            new User{ Login="Sasha", Password="111", Name="LaserKoala", Mail="sasha228@gmail.com", Avatar="", EnrollmentDate=DateTime.Parse("04-12-2018")},

            };

            foreach (User usr in users)
            {
                context.Users.Add(usr);
            }
            context.SaveChanges();

            var messages = new Message[]
            {
            new Message{Text="Hello, world from Fred!", Emoji="", SendTime=DateTime.Parse("13:44:55")},
            new Message{Text="Hi from Sasha!", Emoji="", SendTime=DateTime.Parse("21:00:55")},

            };
            foreach (Message msg in messages)
            {
                context.Messages.Add(msg);
            }
            context.SaveChanges();

            var chats = new Chat[]
            {
            new Chat{ UserId=1, MessageId=1 },
            new Chat{ UserId=2, MessageId=2 },

            };
            foreach (Chat cht in chats)
            {
                context.Chats.Add(cht);
            }
            context.SaveChanges();
        }

    }
}
