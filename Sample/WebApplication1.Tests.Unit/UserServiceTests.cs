using FluentAssertions;
using Force.DeepCloner;
using Moq;
using StandardApi.PoC.Tests.Unit.Services.Standards;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Data.Brokers.StandardStorages;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Tests.Unit
{
    public class UserServiceTests : StandardServiceTests<User>
    {
        public async override Task ShouldRetrieveEntityByIdAsync()
        {
            // given
            User randomEntity = CreateRandomEntity();
            User inputEntity = randomEntity;
            User storageEntity = randomEntity;
            User expectedEntity = storageEntity.DeepClone();

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<User>(inputEntity.GetPrimaryKeys()))
                    .ReturnsAsync(storageEntity);

            // when
            User actualEntity =
                await this.standardService.RetrieveEntityByIdAsync(inputEntity.GetPrimaryKey<User, Guid>());

            // then
            actualEntity.Should().BeEquivalentTo(expectedEntity);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<User>(inputEntity.GetPrimaryKeys()),
                    Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}