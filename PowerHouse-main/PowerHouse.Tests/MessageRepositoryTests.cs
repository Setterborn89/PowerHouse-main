using AutoFixture;
using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Models;
using PowerHouse.Server.Repositories;

namespace PowerHouse.tests
{
	public class MessageRepositoryTests
	{

        private readonly Fixture _fixture;
        private readonly PowerHouseDbContext _dbContext;
        private readonly MessageRepository _repository;
        public MessageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PowerHouseDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _dbContext = new PowerHouseDbContext(options);
            _repository = new MessageRepository(_dbContext);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _dbContext.Messages.AddRange(_fixture.CreateMany<Message>(5));
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAllMessages()
        {
            List<Message> messages = await _repository.GetAllMessagesAsync();
            Assert.Equal(5, messages.Count());
        }

        [Fact]
        public async Task GetOneMessage()
        {
            Message message = await _dbContext.Messages.FirstAsync();
            Message messageFromRepository = await _repository.GetMessageByIdAsync(message.Id);
            Assert.Equal(message.Id, messageFromRepository.Id);
        }

        [Fact]
        public async Task AddMessage()
        {
            Message newMessage = _fixture.Create<Message>();
            await _repository.PostMessage(newMessage);
            await _repository.SaveChangesAsync();

            Message? messageExists = await _repository.GetMessageByIdAsync(newMessage.Id);
            Assert.NotNull(messageExists);
        }

        [Fact]
        public async Task UpdateMessage()
        {
            Message? message = await _dbContext.Messages.FirstOrDefaultAsync();
            Assert.NotNull(message);

            message.Text = "Testar att byta text";
            await _repository.UpdateMessage(message);
            await _repository.SaveChangesAsync();

            Message? updatedMessage = await _repository.GetMessageByIdAsync(message.Id);
            Assert.True(updatedMessage.Text == "Testar att byta text");
        }

        [Fact]
        public async Task DeleteMessage()
        {
            Message? message = await _dbContext.Messages.FirstOrDefaultAsync();
            Assert.NotNull(message);

            await _repository.DeleteMessage(message.Id);
            await _repository.SaveChangesAsync();

            Message? deletedMessage = await _repository.GetMessageByIdAsync(message.Id);
            Assert.True(deletedMessage.IsDeleted);
            
        }
    }
}
