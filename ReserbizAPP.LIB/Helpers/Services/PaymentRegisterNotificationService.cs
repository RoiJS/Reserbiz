using System.Threading.Tasks;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Helpers.Services
{
    public class PaymentRegisterNotificationService
        : IBaseNotificationService
    {
        public NotificationTypeEnum notificationType
        {
            get
            {
                return NotificationTypeEnum.PaymentRegister;
            }
        }

        public string notificationTextFormatIdentifier
        {
            get
            {
                return "PaymentRegisterNotificationFormatIdentifier";
            }
        }

        private readonly IPaymentRegisterNotificationRepository<PaymentRegisterNotification> _paymentRegisterNotificationRepository;
        private readonly PaymentRegisterNotification _paymentRegisterNotification;

        public PaymentRegisterNotificationService(
            IPaymentRegisterNotificationRepository<PaymentRegisterNotification> paymentRegisterNotificationRepository,
            PaymentRegisterNotification paymentRegister
            )
        {
            _paymentRegisterNotification = paymentRegister;
            _paymentRegisterNotificationRepository = paymentRegisterNotificationRepository;
        }

        public PaymentRegisterNotificationService()
        {

        }

        public async Task<int> Register()
        {
            await _paymentRegisterNotificationRepository.AddEntity(_paymentRegisterNotification);
            return _paymentRegisterNotification.Id;
        }

        public Task<string> ConvertNotificationDetailsToText(IReserbizRepository<Entity> reserbizRepository, string textFormat, int notificationTypeId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GenerateNotificationUrl(IReserbizRepository<Entity> reserbizRepository, int notificationTypeId)
        {
            throw new System.NotImplementedException();
        }
    }
}