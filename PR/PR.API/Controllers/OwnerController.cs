﻿using Microsoft.AspNetCore.Mvc;
using PR.Domain.Commands.Handlers;
using PR.Domain.Commands.Inputs;
using PR.Domain.Entities;
using PR.Shared.Commands;
using System;
using System.Threading.Tasks;

namespace PR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        OwnerHandler OwnerHandler;
        public OwnerController(OwnerHandler constructionHandler)
        {
            OwnerHandler = constructionHandler;

        }
        // GET: api/OwnerAPI
        [HttpGet("{Id}")]
        public async Task<Owner> Get(Guid Id)
        {
            var ListOwner = await OwnerHandler.ListOwner(Id);
            return ListOwner;
        }

        // POST: api/OwnerAPI
        [HttpPost]
        public async Task<ICommandResult> Post([FromBody] InsertOwnerCommandInput value)
        {
            var result  = await OwnerHandler.Handler(value);
            return result;
        }

        // PUT: api/OwnerAPI/5
        [HttpPut]
        public async Task<ICommandResult> Put([FromBody] UpdateOwnerCommandInput value)
        {
            var result = await OwnerHandler.Handler(value);
            return result;
        }
    }
}
