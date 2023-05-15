using AutoFixture;
using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Models;
using PowerHouse.Server.Repositories;

namespace PowerHouse.tests
{
    public class ReportRepository_Tests
    {
        private readonly PowerHouseDbContext _dbContext;
        private readonly ReportRepository _repository;
        private readonly MessageRepository _repositoryMessage;
        private readonly Fixture _fixture;

        public ReportRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<PowerHouseDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _dbContext = new PowerHouseDbContext(options);
            _repository = new ReportRepository(_dbContext);
            _repositoryMessage = new MessageRepository(_dbContext);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _dbContext.Reports.AddRange(_fixture.CreateMany<Report>(5));
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAllReports()
        {
            List<Report> reports = await _repository.GetAllReportsAsync(null);
            Assert.Equal(5, reports.Count());
        }

        [Fact]
        public async Task GetOneReport()
        {
            Report report = await _dbContext.Reports.FirstAsync();
            Report reportFromRepository = await _repository.GetReportByIdAsync(report.Id);
            Assert.Equal(report, reportFromRepository);
        }

        [Fact]
        public async Task AddReport()
        {
            // Create a message to make sure the message exist
            // Arrange
            var message = _fixture.Create<Message>();
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            Report newReport = _fixture.Create<Report>();

            var report = _fixture.Build<Report>().With(x => x.MessageId, message.Id) // set messageId to the id of the message that we added above
                .With(x => x.Type, Shared.Enums.Type.Message) // set type to message
        .Create();

            // Act
            await _repository.PostReportAsync(report);
            await _repository.SaveChangesAsync();
            // Assert
            Report repprtExist = await _repository.GetReportByIdAsync(report.Id);
            Assert.Equal(report, repprtExist);
        }

        [Fact]
        public async Task UpdateMessage()
        {
            Report? report = await _dbContext.Reports.FirstOrDefaultAsync();
            Assert.NotNull(report);

            report.Decision = "You are BLOOOOOCCCCKED!";
            await _repository.UpdateReportAsync(report);
            await _repository.SaveChangesAsync();

            Report? updatedReport = await _repository.GetReportByIdAsync(report.Id);
            Assert.True(updatedReport.Decision == "You are BLOOOOOCCCCKED!");
        }

        [Fact]
        public async Task RemoveReport()
        {
            Report? report = await _dbContext.Reports.FirstOrDefaultAsync();
            Assert.NotNull(report);

            await _repository.DeleteReportAsync(report.Id);
            await _repository.SaveChangesAsync();

            Report? removeReport = await _repository.GetReportByIdAsync(report.Id);
            Assert.Null(removeReport);
        }
    }
}
