using System;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class TestRepository
        : BaseRepository<IEntity>, ITestRepository<IEntity>
    {

        public TestRepository(IReserbizRepository<IEntity> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task SendPushNotification()
        {
            var path = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "reserbiz-firebase-adminsdk.json");
            FirebaseApp app = null;
            try
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                }, "myApp");
            }
            catch
            {
                app = FirebaseApp.GetInstance("myApp");
            }

            var fcm = FirebaseAdmin.Messaging.FirebaseMessaging.GetMessaging(app);
            Message message = new Message()
            {
                Notification = new Notification
                {
                    Title = "My push notification title",
                    Body = "Content for this push notification"
                },
                Data = new Dictionary<string, string>()
                 {
                     { "AdditionalData1", "data 1" },
                     { "AdditionalData2", "data 2" },
                     { "AdditionalData3", "data 3" },
                 },

                Topic = "/topics/testtopic"
            };

            var result = await fcm.SendAsync(message);
        }
    }
}