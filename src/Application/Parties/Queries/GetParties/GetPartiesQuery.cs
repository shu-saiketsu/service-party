using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Parties.Queries.GetParties
{
    public sealed class GetPartiesQuery : IRequest<List<PartyEntity>>
    {
    }
}
