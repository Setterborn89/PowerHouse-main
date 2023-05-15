using AutoFixture;
using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Models;
using PowerHouse.Server.Repositories;

namespace PowerHouse.tests
{
	public class ConversationRepository_Tests
	{
		private readonly Fixture _fixture;
		private readonly PowerHouseDbContext _dbContext;
		private readonly ConversationRepository _repository;

        public ConversationRepository_Tests()
		{
			var options = new DbContextOptionsBuilder<PowerHouseDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

            _dbContext = new PowerHouseDbContext(options);
			_repository = new ConversationRepository(_dbContext);
			_fixture = new Fixture();
			_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
				.ForEach(b => _fixture.Behaviors.Remove(b));
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());
			_dbContext.Conversation.AddRange(_fixture.CreateMany<Conversation>(5));
			_dbContext.SaveChanges();
		}


        [Fact]
		public async Task GetAllConversations()
		{
			List<Conversation> conversations = await _repository.GetConversationsAsync();
			Assert.Equal(5, conversations.Count);
		}

		[Fact]
		public async Task GetOneConversation()
		{
			Conversation conversation = await _dbContext.Conversation.FirstAsync();
			Conversation conversationFromRepository = await _repository.GetConversationByIdAsync(conversation.Id);
			Assert.Equal(conversation, conversationFromRepository);
		}

		[Fact]
		public async Task AddConversation()
		{
			Conversation newConversation = _fixture.Create<Conversation>();
			await _repository.InsertAsync(newConversation);
			await _repository.SaveChangesAsync();

			Conversation? conversationExists = await _repository.GetConversationByIdAsync(newConversation.Id);
			Assert.NotNull(conversationExists);
		}

		[Fact]
		public async Task UpdateConversation()
		{
			Conversation? conversation = await _dbContext.Conversation.FirstOrDefaultAsync();
			Assert.NotNull(conversation);

			conversation.Topic = "Testar att byta topic";
			await _repository.UpdateAsync(conversation);
			await _repository.SaveChangesAsync();

			Conversation? updatedConversation = await _repository.GetConversationByIdAsync(conversation.Id);
			Assert.True(updatedConversation.Topic == "Testar att byta topic");
		}

		[Fact]
		public async Task DeleteConversation()
		{
			Conversation? conversation = await _dbContext.Conversation.FirstOrDefaultAsync();
			Assert.NotNull(conversation);

			await _repository.DeleteAsync(conversation.Id);
			await _repository.SaveChangesAsync();

			Conversation? deletedConversation = await _repository.GetConversationByIdAsync(conversation.Id);
			Assert.Null(deletedConversation);
		}
	}
}