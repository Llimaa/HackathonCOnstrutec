﻿using PR.Domain.Commands.Inputs;
using PR.Domain.Commands.Result;
using PR.Domain.Entities;
using PR.Domain.Repositories;
using PR.Shared.Commands;
using System;
using System.Threading.Tasks;

namespace PR.Domain.Commands.Handlers
{
    public class ResponsibleHandler : ICommandHandler<InsertResponsibleCommandInput>,
                                    ICommandHandler<UpdateResponsibleCommandInput>
    {
        private readonly IResponsibleRepository _RREP;
        public async Task<ICommandResult> Handler(InsertResponsibleCommandInput command)
        {
            var resul = _RREP.GetCREA(command.CREA);
            var responsavel = new Responsible(command.Name, command.CREA, command.Email, command.Phone);

            if (responsavel.Invalid)
                return new CommandResult(responsavel.Notifications);

            if (!resul.Result.CREA.Contains(responsavel.CREA))
                _RREP.Insert(responsavel);

            return new CommandResult("Responsible cadastrado com sucesso!");


        }
        public Task<ICommandResult> Handler(UpdateResponsibleCommandInput command)
        {
            var responsavel = _RREP.GetId(command.Id);

            throw new NotImplementedException();
        }

        public async Task<Responsible> ListId(Guid Id)
        {
            return await _RREP.GetId(Id);
        }

        public async Task<Responsible> ListCREA(string crea)
        {
            return await _RREP.GetCREA(crea);
        }
    }
}
