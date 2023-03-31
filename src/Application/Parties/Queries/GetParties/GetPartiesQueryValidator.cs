using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Saiketsu.Service.Party.Application.Parties.Queries.GetParties
{
    public sealed class GetPartiesQueryValidator : AbstractValidator<GetPartiesQuery>
    {
        public GetPartiesQueryValidator()
        {
            
        }
    }
}
