using System;
using System.Linq;
using APITest.Data;
using APITest.Models;
using APITest.Dto;
using APITest.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APITest.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(IDbContextFactory<DataContext> dataContextFactory)
        {
            context = dataContextFactory.CreateDbContext();
        }

        public async Task<User> save(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> update(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> findById(string id)
        {
            return await context.Users.SingleOrDefaultAsync(user => user.id.ToString() == id);
        }

        public async Task<User> findByEmail(string email)
        {
            return await context.Users.SingleOrDefaultAsync(user => user.email == email);
        }

        public async Task<List<User>> findSimilarUsersByEmail(string email)
        {
            return context.Users.FromSqlRaw($"SELECT * FROM \"Users\" WHERE email LIKE '%{email}%'").ToList();
        }

        public async Task<User> delete(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}