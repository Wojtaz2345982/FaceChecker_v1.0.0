using HackHeroes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.GetClassById
{
    public class GetClassByIdQuery : IRequest<Class>
    {
        public GetClassByIdQuery(int classId)
        {
            ClassId = classId;
        }
        public int ClassId { get; set; }
    }
}
