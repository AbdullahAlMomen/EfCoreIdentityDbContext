using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Application.Features.Trainings.Repositories;
using University.Domain.UnitOfWork;

namespace University.Application.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
    }
}
