using Seshat.Application.Common.Exceptions;
using Seshat.Application.TodoLists.Commands.CreateTodoList;
using Seshat.Application.TodoLists.Commands.DeleteTodoList;
using Seshat.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Seshat.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class DeleteTodoListTests : TestBase
    {
        [Test]
        public async Task ShouldRequireValidTodoListId()
        {
            var command = new DeleteTodoListCommand {Id = 99};

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoList()
        {
            await RunAsDefaultUserAsync();
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            await SendAsync(new DeleteTodoListCommand
            {
                Id = listId
            });

            var list = await FindAsync<TodoList>(listId);

            list.Should().BeNull();
        }
    }
}