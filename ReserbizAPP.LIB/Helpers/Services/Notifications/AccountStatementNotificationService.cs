using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Helpers.Services
{
    public class AccountStatementNotificationService
        : IBaseNotificationService
    {
        public NotificationTypeEnum notificationType
        {
            get
            {
                return NotificationTypeEnum.AccountStatement;
            }
        }

        public string notificationTextFormatIdentifier
        {
            get
            {
                return "account_statement_notification_message";
            }
        }

        private readonly IGenerateAccountStatementNotificationRepository<GeneratedAccountStatementNotification> _generateAccountStatementNotificationRepository;
        private readonly GeneratedAccountStatementNotification _generatedAccountStatement;

        public AccountStatementNotificationService(
            IGenerateAccountStatementNotificationRepository<GeneratedAccountStatementNotification> generateAccountStatementNotificationRepository,
            GeneratedAccountStatementNotification generatedAccountStatement
            )
        {
            _generatedAccountStatement = generatedAccountStatement;
            _generateAccountStatementNotificationRepository = generateAccountStatementNotificationRepository;
        }


        public AccountStatementNotificationService()
        {

        }

        public async Task<int> Register()
        {
            await _generateAccountStatementNotificationRepository.AddEntity(_generatedAccountStatement);
            return _generatedAccountStatement.Id;
        }

        public async Task<string> ConvertNotificationDetailsToText(IReserbizRepository<Entity> reserbizRepository, string textFormat, int notificationTypeId)
        {
            var accountStatementNotificationFromRepo = await reserbizRepository.ClientDbContext.GeneratedAccountStatementNotifications
                                                                .Where(n => n.Id == notificationTypeId)
                                                                .FirstOrDefaultAsync();

            var contractFromRepo = await reserbizRepository.ClientDbContext.Contracts
                .Where(c => c.Id == accountStatementNotificationFromRepo.ContractId)
                .AsQueryable()
                .Includes(
                    c => c.AccountStatements,
                    c => c.Tenant,
                    c => c.Term,
                    c => c.Space
                ).FirstOrDefaultAsync();

            var formattedNotificationMessage = String.Format(textFormat, contractFromRepo.Tenant.PersonFullName, accountStatementNotificationFromRepo.AccountStatementDateTime.ToString("MM/dd/yyyy"), contractFromRepo.Code);

            return formattedNotificationMessage;
        }

        public async Task<string> GenerateNotificationUrl(IReserbizRepository<Entity> reserbizRepository, int notificationTypeId)
        {
            var accountStatementNotificationFromRepo = await reserbizRepository.ClientDbContext.GeneratedAccountStatementNotifications
                                                                .Where(n => n.Id == notificationTypeId)
                                                                .FirstOrDefaultAsync();

            var notificationUrl = String.Format("contracts/{0}/account-statement/{1}", accountStatementNotificationFromRepo.ContractId, accountStatementNotificationFromRepo.AccountStatementId);

            return notificationUrl;
        }

        public async Task MarkItemAsRead(IReserbizRepository<Entity> reserbizRepository, int itemId, int currentUserId, UserTypeEnum currentUserType)
        {

            // Get unread user notification for a particular statement of account.
            var unreadNotification = await (from un in reserbizRepository.ClientDbContext.UserNotifications

                                            join n in reserbizRepository.ClientDbContext.Notifications
                                            on un.NotificationId equals n.Id

                                            join gan in reserbizRepository.ClientDbContext.GeneratedAccountStatementNotifications
                                            on n.NotificationTypeId equals gan.Id

                                            where un.UserId == currentUserId &&
                                            un.UserType == currentUserType &&
                                            n.NotificationType == NotificationTypeEnum.AccountStatement &&
                                            gan.AccountStatementId == itemId &&
                                            un.ReadStatus == false

                                            select new
                                            {
                                                UserNotification = un,
                                            }).FirstOrDefaultAsync();

            if (unreadNotification != null)
            {
                // Mark the notification to read status.
                unreadNotification.UserNotification.ReadStatus = true;

                await reserbizRepository.ClientDbContext.SaveChangesAsync();
            }
        }
    }
}