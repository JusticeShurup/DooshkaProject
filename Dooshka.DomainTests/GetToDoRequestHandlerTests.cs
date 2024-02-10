using BLL.ToDoItemsLogic.Handlers;
using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.ToDoItems.Requests;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Dooshka.DomainTests
{
    public class GetToDoRequestHandlerTests
    {
        private readonly Mock<IRepository<ToDoItem>> _toDoItemRepository;
        private readonly Mock<IHttpContextAccessor> _httpContext;

        public GetToDoRequestHandlerTests() 
        {
            _toDoItemRepository = new();
            _httpContext = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnAllToDoItems()
        {
            var request = new GetToDoRequest();

            var handler = new GetToDoRequestHandler(_toDoItemRepository.Object, _httpContext.Object);

            var result = await handler.Handle(request, default);

            Xunit.Assert.NotNull(result);
        }
    }
}