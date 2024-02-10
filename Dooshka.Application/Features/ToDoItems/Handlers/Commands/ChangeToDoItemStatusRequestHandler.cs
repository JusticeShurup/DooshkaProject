using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.ToDoItems.Requests;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.ToDoItemsLogic.Handlers
{
    public class ChangeToDoItemStatusRequestHandler : IRequestHandler<ChangeToDoItemStatusRequest>
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        private readonly IHttpContextAccessor _httpContext;

        public ChangeToDoItemStatusRequestHandler(IRepository<ToDoItem> toDoItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _toDoItemRepository = toDoItemRepository;
            _httpContext = httpContextAccessor;
        }

        public async Task Handle(ChangeToDoItemStatusRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContext.HttpContext.Items["User"];

            ToDoItem? toDoItem;

            toDoItem = _toDoItemRepository.Find(x => x.Id == request.ToDoItemId);

            if (toDoItem == null)
            {
                throw new NotFoundException("ToDoItem doesn't found");
            }

            List<ToDoItem> subItems = _toDoItemRepository.FindAllAsync(p => p.ParentItemId == toDoItem.Id).Result.ToList();

            toDoItem.Status = (ToDoItemStatusType)Enum.Parse(typeof(ToDoItemStatusType), request.Status.ToString());

            await _toDoItemRepository.UpdateAsync(toDoItem);

            foreach (ToDoItem subItem in subItems)
            {
                subItem.Status = toDoItem.Status;
                await _toDoItemRepository.UpdateAsync(subItem);
            }

        }
    }
}
