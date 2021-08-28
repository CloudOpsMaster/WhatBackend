﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CharlieBackend.Core.DTO.Schedule;
using CharlieBackend.Core.Models.ResultModel;

namespace CharlieBackend.Business.Services.Interfaces
{
    public interface IEventsService
    {
        public Task<ScheduledEventDTO> UpdateAsync(long id, UpdateScheduledEventDto scheduleModel);

        public Task<Result<bool>> DeleteAsync(long id);

        public Task<ScheduledEventDTO> GetAsync(long id);

        public Task<Result<ScheduledEventDTO>> ConnectScheduleToLessonById(long eventId, long lessonId);
    }
}
