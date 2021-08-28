﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CharlieBackend.Core.DTO.Lesson
{
    public class StudentLessonDto
    {
        public string ThemeName { get; set; }

        public long Id { get; set; }

        public bool Presence { get; set; }

        public sbyte? Mark { get; set; }

        public string Comment { get; set; }

        public long? StudentGroupId { get; set; }

        public DateTime LessonDate { get; set; }
    }
}
