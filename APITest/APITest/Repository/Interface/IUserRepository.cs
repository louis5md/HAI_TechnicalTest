using APITest.Models;
using APITest.Dto;

namespace APITest.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<User> save(User user);
        public Task<User> findById(string uuid);
        public Task<User> findByEmail(string email);
        public Task<User> update(User user);
        public Task<List<User>> findSimilarUsersByEmail(string email);
        public Task<User> delete(User user);

    }
}
