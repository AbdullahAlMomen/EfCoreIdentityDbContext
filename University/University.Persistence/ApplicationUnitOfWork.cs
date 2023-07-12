using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Application.Features.Trainings.Repositories;
using University.Application.UnitOfWorks;

namespace University.Persistence
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IStudentRepository StudentRepository { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            IStudentRepository studentRepository) : base((DbContext)dbContext)
        {
            StudentRepository = studentRepository;
        }
    }
}
