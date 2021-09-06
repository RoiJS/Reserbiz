using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserNotificationController(IUserNotificationRepository<UserNotification> userNotificationRepository, IMapper mapper, IPaginationService paginationService)
        {
            _mapper = mapper;
            _paginationService = paginationService;
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
        public async Task<ActionResult<UserNotificationForListDto>> GetUserNotifications(DateTime notificationDateTime, SortOrderEnum sortOrder, int page)
        {
            var userNotificationsFromRepo = await _userNotificationRepository.GetUserNotificationAsync(CurrentUserId, CurrentUserType);

            var userNotificationFilter = new UserNotificationFilter
            {
                NotificationDateTime = notificationDateTime,
                SortOrder = sortOrder
            };

            var filteredUserNotificationsFromRepo = _userNotificationRepository.GetFilteredUserNotifications(userNotificationsFromRepo.ToList(), userNotificationFilter);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<UserNotificationPaginationListDto>(filteredUserNotificationsFromRepo, page);

            return Ok(entityPaginationListDto);
        }
    }
}