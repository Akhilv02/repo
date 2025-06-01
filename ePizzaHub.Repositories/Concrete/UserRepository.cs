using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PizzaHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _dbContext
                .Users.
                Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == userName);
        }
    }
}
