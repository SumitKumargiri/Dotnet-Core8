using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class EmployeesRepository : BaseRepository<Employees>,IEmployeesRepository
{
    public EmployeesRepository(EnglishBuddyDbContext context) : base(context)
    {
    }
    public Task<Employees> Connected(string email, CancellationToken cancellationToken)
    {
        return Context.Employees.FirstOrDefaultAsync(x => x.FirstName == email, cancellationToken);
    }

    public async Task<Employees> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Delete(Employees employee)
    {
        Context.Employees.Remove(employee);
    }

}