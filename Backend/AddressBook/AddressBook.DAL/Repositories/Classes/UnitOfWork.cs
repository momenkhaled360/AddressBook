using AddressBook.DAL.Data;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Repositories.Classes;
using AddressBook.DAL.Repositories.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    private readonly Dictionary<Type, object> _repositories = [];

    public IContactRepository ContactRepository { get; }

    public UnitOfWork(
        AppDbContext dbContext,
        IContactRepository contactRepository)
    {
        _dbContext = dbContext;

        ContactRepository = contactRepository;
    }

    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : BaseEntity, new()
    {
        var type = typeof(TEntity);

        if (_repositories.TryGetValue(type, out var repo))
            return (IRepository<TEntity>)repo;

        var newRepo = new Repository<TEntity>(_dbContext);

        _repositories[type] = newRepo;

        return newRepo;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}