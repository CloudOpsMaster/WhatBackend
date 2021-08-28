﻿using CharlieBackend.Core.DTO.Student;
using CharlieBackend.Core.Models.ResultModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharlieBackend.Business.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Result<StudentDto>> CreateStudentAsync(long accountId);

        Task<Result<IList<StudentDetailsDto>>> GetAllStudentsAsync();

        Task<Result<IList<StudentDetailsDto>>> GetAllActiveStudentsAsync();

        Task<Result<IList<StudentStudyGroupsDto>>> GetStudentStudyGroupsByStudentIdAsync(long id);

        Task<long?> GetAccountId(long studentId);

        Task<Result<StudentDto>> UpdateStudentAsync(long id, UpdateStudentDto studentModel);

        Task<Result<StudentDto>> GetStudentByAccountIdAsync(long accountId);

        Task<Result<StudentDto>> GetStudentByIdAsync(long studentId);

        Task<Result<StudentDto>> GetStudentByEmailAsync(string email);

        Task<Result<bool>> DisableStudentAsync(long studentId);

        Task<Result<bool>> EnableStudentAsync(long studentId);
    }
}
