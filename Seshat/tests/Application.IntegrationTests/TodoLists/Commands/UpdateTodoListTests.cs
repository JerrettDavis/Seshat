using Seshat.Application.Common.Exceptions;
using Seshat.Application.TodoLists.Commands.CreateTodoList;
using Seshat.Application.TodoLists.Commands.UpdateTodoList;
using Seshat.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Seshat.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class UpdateTodoListTests : TestBase
    {
        [Test]
        public async Task ShouldRequireValidTodoListId()
        {
            var command = new UpdateTodoListCommand
            {
                Id = 99,
                Title = "New Title"
            };

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            await RunAsDefaultUserAsync();
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            await SendAsync(new CreateTodoListCommand
            {
                Title = "Other List"
            });

            var command = new UpdateTodoListCommand
            {
                Id = listId,
                Title = "Other List"
            };

            (await FluentActions.Invoking(() =>
                    SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("The specified title already exists.");
        }

        [Test]
        public async Task ShouldUpdateTodoList()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var command = new UpdateTodoListCommand
            {
                Id = listId,
                Title = "Updated List Title"
            };

            await SendAsync(command);

            var list = await FindAsync<TodoList>(listId);

            list.Title.Should().Be(command.Title);
            list.LastModifiedBy.Should().NotBeNull();
            list.LastModifiedBy.Should().Be(userId);
            list.LastModified.Should().NotBeNull();
            list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}