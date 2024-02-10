using BLL.ToDoItemsLogic.Handlers;
using Dooshka.Application.Features.ToDoItems.Requests;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Dooshka.Application.UnitTests
{
    public class GetToDoItemRequestTests
    {
        private readonly Mock<IRepository<ToDoItem>> _toDoItemRepository;
        private readonly Mock<IRepository<User>> _userRepository;
        private readonly Mock<IHttpContextAccessor> _httpContext;


        public GetToDoItemRequestTests() 
        {
            _toDoItemRepository = new();
            _userRepository = new();
            _httpContext = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {

            _userRepository.Setup(repo => repo.Find(p => p.Email == new Mock<string>().Object)).Returns(new User() { Email = "mixas_2004", Password = ""});


            var request = new GetToDoRequest();

            var handler = new GetToDoRequestHandler(_toDoItemRepository.Object, _userRepository.Object, _httpContext.Object);

            var result = await handler.Handle(request, default);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestRepository()
        {
            _userRepository.Setup(repo => repo.Find(It.IsAny<Func<User, bool>>())).Returns((Func<User, bool> expr) => new User() { Email = "mixas_2004@mail.ru", Password = "" } );

            var result = _userRepository.Object.Find(p => p.Email == "mixas_2004@mail.ru");

            Assert.NotNull(result);
        }
    }
}