using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Entities;
using University.Domain.Repository;

namespace University.Application.Features.Trainings.Repositories
{
    public interface IStudentRepository:IRepositoryBase<Student, Guid>
    {
    
        Task<(IList<Student> records, int total, int totalDisplay)>
         GetTableDataAsync(Expression<Func<Student, bool>> expression, string orderBy,
         int pageIndex, int pageSize);
        bool IsDuplicateName(string name,string address, Guid? id);
    }
}
