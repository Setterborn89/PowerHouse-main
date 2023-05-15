using AutoFixture;
using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Models;
using PowerHouse.Server.Repositories;

namespace PowerHouse.tests
{
    public class UserRepository_Test
    {
        private readonly PowerHouseDbContext _dbContext;
        private readonly UserRepository _repository;
        private readonly Fixture _fixture;
        public UserRepository_Test()
        {
            var options = new DbContextOptionsBuilder<PowerHouseDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
       .Options;

            _dbContext = new PowerHouseDbContext(options);
            _repository = new UserRepository(_dbContext);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _dbContext.Users.AddRange(_fixture.CreateMany<User>(5));
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAllUsers()
        {
            List<User> users = await _repository.GetAllUsersAsync();
            Assert.Equal(5, users.Count());
        }

        [Fact]
        public async Task GetOneUser()
        {
            User user = await _dbContext.Users.FirstAsync();
            User userFromRepository = await _repository.GetUserById(user.Id);
            Assert.Equal(user, userFromRepository);
        }

        [Fact]
        public async Task AddUser()
        {
            User newUser = _fixture.Create<User>();
            await _repository.PostOrUpdateUser(newUser);
            await _repository.SaveChangesAsync();

            User userExists = await _repository.GetUserById(newUser.Id);
            Assert.Equal(newUser, userExists);
        }

        [Fact]
        public async Task UpdateUser()
        {
            User newUser = _fixture.Create<User>();
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            newUser.Username = "Test";
            await _repository.PostOrUpdateUser(newUser);
            await _repository.SaveChangesAsync();

            User userExists = await _repository.GetUserById(newUser.Id);
            Assert.Equal(newUser.Username, userExists.Username);
        }

        [Fact]
        public async Task RemoveUser()
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync();
            Assert.NotNull(user);

            await _repository.DeleteUser(user.Id);
            await _repository.SaveChangesAsync();

            User? deletedUser = await _repository.GetUserById(user.Id);
            Assert.Null(deletedUser);
        }
    }
}
