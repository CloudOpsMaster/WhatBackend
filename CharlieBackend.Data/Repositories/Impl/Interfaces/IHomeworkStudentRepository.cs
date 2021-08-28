﻿using CharlieBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharlieBackend.Data.Repositories.Impl.Interfaces
{
    public interface IHomeworkStudentRepository : IRepository<HomeworkStudent>
    {
        /// <summary>
        /// this method check if the student has already done homework for task(homeworkId)
        /// </summary>
        /// <param name="studentId"> Student Id from Student table</param>
        /// <param name="homeworkId">Its homework Id which have been to created by mentor</param>
        /// <returns></returns>
        Task<bool> IsStudentHasHomeworkAsync(long studentId, long homeworkId);

        /// <summary>
        /// return student's homework
        /// </summary>
        /// <param name="studentId"> Student Id</param>
        /// <returns></returns>
        Task<IList<HomeworkStudent>> GetHomeworkStudentForStudent(long studentId);

        /// <summary>
        /// return all students homework 
        /// </summary>
        /// <param name="homeworkId">Homework ID which have been created by this Mentor</param>
        /// <returns></returns>
        Task<IList<HomeworkStudent>> GetHomeworkStudentForMentor(long homeworkId);

        void UpdateManyToMany(IEnumerable<AttachmentOfHomeworkStudent> currentHomeworkAttachments,
                            IEnumerable<AttachmentOfHomeworkStudent> newHomeworkAttachments);
    }
}
