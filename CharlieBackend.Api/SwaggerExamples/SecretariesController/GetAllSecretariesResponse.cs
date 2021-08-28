﻿using CharlieBackend.Core.DTO.Secretary;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharlieBackend.Api.SwaggerExamples.SecretariesController
{
    class GetAllSecretariesResponse : IExamplesProvider<List<SecretaryDto>>
    {
        public List<SecretaryDto> GetExamples()
        {
            return new List<SecretaryDto>()
            {
                new SecretaryDto
                {
                    Id = 145,
                    Email = "secretaryemail@example.com",
                    FirstName = "Isabella",
                    LastName = "Smith"
                },
                new SecretaryDto
                {
                    Id = 146,
                    Email = "secretary2email@example.com",
                    FirstName = "Adriana",
                    LastName = "Bullock"
                }
            };
        }
    }
}
