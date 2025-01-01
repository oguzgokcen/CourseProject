using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.UoW
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        void SaveChanges();
    }
}