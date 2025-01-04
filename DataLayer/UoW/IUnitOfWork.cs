using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.UoW
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        void SaveChanges();
    }
}