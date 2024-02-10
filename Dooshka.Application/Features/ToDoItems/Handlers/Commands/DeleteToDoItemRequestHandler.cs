using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.ToDoItems.Requests;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.ToDoItemsLogic.Handlers
{
    public class DeleteToDoItemRequestHandler : IRequestHandler<DeleteToDoItemRequest, DeleteToDoItemResponseDTO>
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContext;

        public DeleteToDoItemRequestHandler(IRepository<ToDoItem> toDoItemRepository, IRepository<User> userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _toDoItemRepository = toDoItemRepository;
            _userRepository = userRepository;
            _httpContext = httpContextAccessor;

        }

        public async Task<DeleteToDoItemResponseDTO> Handle(DeleteToDoItemRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContext.HttpContext.Items["User"];

            var toDoItem = _toDoItemRepository.Find(x => x.Id == request.Id);
            
            if (toDoItem == null )
            {
                throw new NotFoundException("ToDoItem doesn't exists");
            }

            if (toDoItem.UserId != user.Id)
            {
                throw new BadRequestException("This ToDoItem isn't user own");
            }

            var subToDoItems = await _toDoItemRepository.FindAllAsync(p => p.ParentItemId == toDoItem.Id);

            foreach ( var subItem in subToDoItems )
            {
                await _toDoItemRepository.DeleteAsync(subItem);
            }

            await _toDoItemRepository.DeleteAsync(toDoItem);

            return new DeleteToDoItemResponseDTO() { Response = "All deleted successffully"}; 



        }
    }
}
