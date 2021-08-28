﻿using CharlieBackend.Core.DTO.HomeworkStudent;
using CharlieBackend.Core.Models.ResultModel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CharlieBackend.Business.Services.Interfaces
{
    public interface IHomeworkStudentService
    {
        Task<Result<HomeworkStudentDto>> CreateHomeworkFromStudentAsync(HomeworkStudentRequestDto homeworkStudent);

        Task<IList<HomeworkStudentDto>> GetHomeworkStudentForMentor(long homeworkId);

        Task<IList<HomeworkStudentDto>> GetHomeworkStudentForStudent();

        Task<Result<HomeworkStudentDto>> UpdateHomeworkFromStudentAsync(HomeworkStudentRequestDto homeworkStudent, long homeworkId);
    }
}
