﻿using PR.Domain.Commands.Inputs;
using PR.Domain.Commands.Result;
using PR.Domain.Entities;
using PR.Domain.Helper;
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
        public ResponsibleHandler(IResponsibleRepository RREP)
        {
            _RREP = RREP;          
        }
        public async Task<ICommandResult> Handler(InsertResponsibleCommandInput command)
        {
            var resul = _RREP.GetByCREA(command.CREA);
            var responsavel = new Responsible(command.Name, command.CREA, command.Email, command.Phone);

            if (responsavel.Invalid)
                return new CommandResult(_BuildResult.BuildResult(responsavel.Notifications).Result);

            if (!resul.Result.CREA.Contains(responsavel.CREA))
                _RREP.Insert(responsavel);

            return new CommandResult(new string[] { "O Responsavel cadastrado com sucesso!" });


        }
        public async Task<ICommandResult> Handler(UpdateResponsibleCommandInput command)
        {
            var responsavel = await _RREP.GetById(command.Id);
            responsavel.Update(command.Name, command.Email, command.Phone);

            _RREP.Update(responsavel);

            return new CommandResult(new string[] { "Responsável atualizado com sucesso !" });
        }

        public async Task<Responsible> ListId(Guid Id)
        {
            var responsible = await _RREP.GetById(Id);
            return responsible;
        }

        public async Task<Responsible> ListCREA(string crea)
        {
            var responsible = await _RREP.GetByCREA(crea);
            return responsible;
        }
    }
}
