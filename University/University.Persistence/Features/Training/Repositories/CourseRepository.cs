
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using University.Application.Features.Trainings.Repositories;
using University.Domain.Entities;
using University.Persistence;

namespace FirstDemo.Persistence.Features.Training.Repositories
{
    public class StudentRepository : Repository<Student, Guid>, IStudentRepository
    {
        public StudentRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<Student> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<Student, bool>> expression,
            string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null,
                pageIndex, pageSize, true);
        }

        public bool IsDuplicateName(string name, string address, Guid? id)
        {
            int? existingCourseCount = null;

            if (id.HasValue)
                existingCourseCount = GetCount(x => x.Name == name && x.Address == address && x.Id != id.Value);
            else
                existingCourseCount = GetCount(x => x.Name == name && x.Address == address);

            return existingCourseCount > 0;
        }

    }
}
