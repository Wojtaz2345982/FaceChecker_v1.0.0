using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.GetClassByEncodedName
{
    public class GetClassByEncodedNameQuery : IRequest<ClassDto>
    {
        public string EncodedName { get; set; }

        public GetClassByEncodedNameQuery(string endodedName)
        {
            EncodedName = endodedName;
        }
    }
}
