﻿using System.Threading.Tasks;
using System.Collections.Generic;
using CharlieBackend.Data.Helpers;
using CharlieBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CharlieBackend.Data.Repositories.Impl.Interfaces;
using System.Linq;
using CharlieBackend.Core.DTO.Schedule;
using CharlieBackend.Core;
using System;

using CharlieBackend.Core.DTO.Attachment;
using CharlieBackend.Data.Exceptions;

namespace CharlieBackend.Data.Repositories.Impl
{
    class ScheduledEventRepository : Repository<ScheduledEvent>, IScheduledEventRepository
    {
        public ScheduledEventRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        public async Task<IList<ScheduledEvent>> GetEventsFilteredAsync(ScheduledEventFilterRequestDTO request)
        {
            return await _applicationContext.ScheduledEvents
                    .WhereIf(request.EventOccurrenceID.HasValue, x => _applicationContext.ScheduledEvents
                        .Where(y => y.EventOccurrenceId == request.EventOccurrenceID)
                        .Select(y => y.Id)
                        .Contains(x.Id))
                    .WhereIf(request.CourseID.HasValue, x => _applicationContext.StudentGroups
                        .Where(y => y.CourseId == request.CourseID.Value)
                        .SelectMany(y => y.ScheduledEvents)
                        .Select(y => y.Id)
                        .Contains(x.Id))
                    .WhereIf(request.MentorID.HasValue, x => x.MentorId == request.MentorID.Value)
                    .WhereIf(request.GroupID.HasValue, x => x.StudentGroupId == request.GroupID.Value)
                    .WhereIf(request.ThemeID.HasValue, x => x.ThemeId == request.ThemeID.Value)
                    .WhereIf(request.StudentAccountID.HasValue, x => _applicationContext.StudentsOfStudentGroups
                        .Where(y => y.StudentId == request.StudentAccountID)
                        .Select(y => y.StudentGroup)
                        .SelectMany(y => y.ScheduledEvents)
                        .Select(y => y.Id)
                        .Contains(x.Id))
                    .WhereIf(request.StartDate.HasValue, x => x.EventFinish >= request.StartDate)
                    .WhereIf(request.FinishDate.HasValue, x => x.EventStart <= request.FinishDate)
                    .ToListAsync();
        }

        public override async Task<ScheduledEvent> GetByIdAsync(long id)
        {
            var schedule = await _applicationContext.ScheduledEvents.FirstOrDefaultAsync(entity => entity.Id == id);
            
            if(schedule is null)
            {
                throw new NotFoundException("Scheduled event does not exist");
            }

            return schedule;
        }

        public async Task<ScheduledEvent> ConnectEventToLessonById (long? eventId, long? lessonId)
        {
            var schedule = await _applicationContext.ScheduledEvents.FirstOrDefaultAsync(entity => entity.Id == eventId);

            if (schedule != null)
            {
                schedule.LessonId = lessonId;
                _applicationContext.SaveChanges();
            }

            return schedule;
        }
    }
}
