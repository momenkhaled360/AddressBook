using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IContactRepository ContactRepository { get; }

        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseEntity, new();

        Task<int> SaveChangesAsync();
    }
}
