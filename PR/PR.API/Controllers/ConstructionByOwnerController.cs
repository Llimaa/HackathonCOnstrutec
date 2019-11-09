﻿using Microsoft.AspNetCore.Mvc;
using PR.Domain.Commands.Handlers;
using PR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionByOwnerController : ControllerBase
    {
        private readonly ConstructionHandler ConstructionHandler;

        public ConstructionByOwnerController(ConstructionHandler constructionHandler)
        {
            ConstructionHandler = constructionHandler;
        }
        // GET: api/ConstructionByOwner/ownerId
        [HttpGet("{ownerId}")]
        public async Task<IEnumerable<Construction>> Get(Guid ownerId)
        {
            var report = await ConstructionHandler.ListByOwnerId(ownerId);

            return report;
        }
    }
}
