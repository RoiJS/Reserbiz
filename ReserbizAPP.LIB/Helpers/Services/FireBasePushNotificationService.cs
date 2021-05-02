using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Helpers.Services
{
    public class FireBasePushNotificationService : IFireBasePushNotificationService
    {
        private readonly string _topic;

        public FireBasePushNotificationService(string topic)
        {
            _topic = topic;

        }

        public async Task Send(string title, string body, Dictionary<string, string> data = null)
        {
            var path = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "reserbiz-firebase-adminsdk.json");
            FirebaseApp app = null;
            try
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                }, "reserbizApp");
            }
            catch
            {
                app = FirebaseApp.GetInstance("reserbizApp");
            }

            var fcm = FirebaseAdmin.Messaging.FirebaseMessaging.GetMessaging(app);
            Message message = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Topic = _topic
            };

            if (data != null)
            {
                message.Data = data;
            }

            var result = await fcm.SendAsync(message);
        }
    }
}