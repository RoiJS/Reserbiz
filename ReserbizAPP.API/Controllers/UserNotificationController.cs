using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserNotificationController : ReserbizBaseController
    {
        private readonly IUserNotificationRepository<UserNotification> _userNotificationRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;
        private readonly IStringLocalizer _stringLocalizer;

        public UserNotificationController(
            IUserNotificationRepository<UserNotification> userNotificationRepository,
            IMapper mapper,
            IPaginationService paginationService,
            IStringLocalizer stringLocalizer)
        {
            _mapper = mapper;
            _paginationService = paginationService;
            _stringLocalizer = stringLocalizer;
            _userNotificationRepository = userNotificationRepository;
        }

        [HttpPut("updateUserNotificationStatus/{userNotificationId}/{status}")]
        public async Task<ActionResult> UpdateUserNotificationStatus(int userNotificationId, bool status)
        {
            var userNotificationFromRepo = await _userNotificationRepository.GetEntity(userNotificationId).ToObjectAsync();

            if (userNotificationFromRepo == null)
                return NotFound("User notification does not exists.");

            _userNotificationRepository.SetCurrentUserId(CurrentUserId);

            userNotificationFromRepo.ReadStatus = status;

            if (!_userNotificationRepository.HasChanged())
            {
                return BadRequest("There are no changes to applied.");
            }

            if (await _userNotificationRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating user notification with an id of {userNotificationId} failed on save.");
        }

        [HttpGet("getUserNotifications")]
        public async Task<ActionResult<UserNotificationPaginationListDto>> GetUserNotifications(DateTime fromDate, DateTime toDate, SortOrderEnum sortOrder, int page)
        {
            var userNotificationsFromRepo = await _userNotificationRepository.GetUserNotificationAsync(CurrentUserId, CurrentUserType);

            var userNotificationFilter = new UserNotificationFilter
            {
                FromDate = fromDate,
                ToDate = toDate,
                SortOrder = sortOrder
            };

            var filteredUserNotificationsFromRepo = _userNotificationRepository.GetFilteredUserNotifications(userNotificationsFromRepo.ToList(), userNotificationFilter);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<UserNotificationPaginationListDto>(filteredUserNotificationsFromRepo, page);

            AssembleNotificationFinalList(ref entityPaginationListDto);

            return Ok(entityPaginationListDto);
        }

        [HttpGet("getUserUnreadNotificationsCount")]
        public async Task<ActionResult<int>> GetUserUnreadNotificationsCount()
        {
            var count = await _userNotificationRepository.GetUnreadNotificationsCount();
            return Ok(count);
        }

        [HttpPut("setReadStatus/{id}/{status}")]
        public async Task<IActionResult> SetTenantStatus(int id, bool status)
        {
            var userNotificationFromRepo = await _userNotificationRepository.GetEntity(id).ToObjectAsync();

            if (userNotificationFromRepo == null)
                return NotFound("User Notification does not exists.");

            _userNotificationRepository.SetCurrentUserId(CurrentUserId);

            userNotificationFromRepo.ReadStatus = status;

            if (!_userNotificationRepository.HasChanged())
                return BadRequest("Nothing was changed on the object");

            if (await _userNotificationRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating notification read status with an id of {id} failed on save.");
        }

        private void AssembleNotificationFinalList(ref UserNotificationPaginationListDto userNotificationPaginationList)
        {
            var finalNoticationList = new List<UserNotificationForListDto>();
            var currentNotificationList = userNotificationPaginationList.Items
                                                .Select(i => ((UserNotificationForListDto)i))
                                                .ToList();

            var distinctNotificationDate = currentNotificationList
                                                .Select(i => ((UserNotificationForListDto)i).NotificationDateTime.ToString("MM/dd/yyyy"))
                                                .Distinct()
                                                .ToList();

            foreach (var notificationDate in distinctNotificationDate)
            {
                var notificationHeaderMessage = notificationDate == DateTime.Now.ToString("MM/dd/yyyy") ? _stringLocalizer["today_label"] : Convert.ToDateTime(notificationDate).ToString("MMMM dd, yyyy");

                finalNoticationList.Add(new UserNotificationForListDto
                {
                    Id = 0,
                    NotificationItemType = NotificationItemTypeEnum.NotificationHeader,
                    NotificationMessage = notificationHeaderMessage,
                    NotificationDateTime = Convert.ToDateTime(notificationDate)
                });

                var notificationsBasedOnDate = currentNotificationList.Where(c => c.NotificationDateTime.ToString("MM/dd/yyyy") == notificationDate);

                finalNoticationList.AddRange(notificationsBasedOnDate);
            }

            userNotificationPaginationList.Items = finalNoticationList;
        }
    }
}