﻿using AutoMapper;
using CharlieBackend.Business.Services;
using CharlieBackend.Core.Entities;
using CharlieBackend.Core.Mapping;
using CharlieBackend.Data;
using CharlieBackend.Data.Exceptions;
using CharlieBackend.Data.Repositories.Impl;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CharlieBackend.Api.UnitTest.RepositoriesTests
{
    public class LessonRepositoryTests
    {
        [Fact]
        public async Task UpdateMarkAsyncTest_ValidDataPassed_ShouldReturnVisit()
        {
            // Arrange
            long lessonId = 1;
            sbyte mark = 5;
            long visitId_one = 1;
            long visitId_two = 2;
            long studentId_one = 1;
            long studentId_two = 2;
            long homeworkId = 1;
            long homeworkStudentId_one = 1;
            long homeworkStudentId_two = 2;

            var visitTrue = new Visit
            {
                Id = visitId_one,
                StudentMark = mark,
                Presence = true,
                StudentId = studentId_one
            };
            var visitFalse = new Visit
            {
                Id = visitId_two,
                StudentMark = mark,
                Presence = false,
                StudentId = studentId_two
            };

            Lesson lesson = new Lesson()
            {
                Id = lessonId,
                Visits = new List<Visit>()
                {
                   visitTrue,
                   visitFalse
                }
            };

            var homeworkStudent_one = new HomeworkStudent
            {
                Id = homeworkStudentId_one,
                HomeworkId = homeworkId,
                StudentId = studentId_one
            };

            var homeworkStudent_two = new HomeworkStudent
            {
                Id = homeworkStudentId_two,
                HomeworkId = homeworkId,
                StudentId = studentId_two,
            };

            var homework = new Homework
            {
                LessonId = lessonId,
                Lesson = lesson,
                Id = homeworkId,
                HomeworkStudents = new List<HomeworkStudent>
                {
                    homeworkStudent_one,
                    homeworkStudent_two
                }
            };

            homeworkStudent_one.Homework = homework;
            visitTrue.Lesson = lesson;

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                context.Lessons.Add(lesson);
                context.Homeworks.Add(homework);
                context.HomeworkStudents.Add(homeworkStudent_one);
                context.HomeworkStudents.Add(homeworkStudent_two);
                context.Visits.Add(visitFalse);
                context.Visits.Add(visitTrue);
                context.SaveChanges();
            }

            using (var context = new ApplicationContext(options))
            {
                var lessonRepository = new LessonRepository(context);

                // Act
                var foundVisitTrue = await lessonRepository.GetVisitByStudentHomeworkIdAsync(homeworkStudentId_one);
                var foundVisitFalse = await lessonRepository.GetVisitByStudentHomeworkIdAsync(homeworkStudentId_two);

                // Assert
                foundVisitTrue.Id.Should().Equals(visitId_one);
                foundVisitFalse.Id.Should().Equals(visitId_two);
            }
        }

        [Fact]
        public async Task UpdateMarkAsyncTest_NoDataExisted_ShouldThrowException()
        {
            // Arrange
            int wrongHomeworkStudentId = 1;
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "EmptyTestDatabase").Options;

            using (var context = new ApplicationContext(options))
            {
                var lessonRepository = new LessonRepository(context);

                // Act
                Func<Task> wrongRequest = async () => await lessonRepository.GetVisitByStudentHomeworkIdAsync(wrongHomeworkStudentId);

                // Assert
                await wrongRequest.Should().ThrowAsync<NotFoundException>();
            }
        }

    }
}
