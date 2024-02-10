using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.ToDoItems.Requests;
using Dooshka.Application.Persistence.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dooshka.Application.Features.ToDoItems.Handlers.Queries
{
    public class GetToDoItemsByDateRangeRequestHandler : IRequestHandler<GetToDoItemsByDateRangeRequest, List<CreatedToDoItemDTO>>
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContext;


        public GetToDoItemsByDateRangeRequestHandler(IRepository<User> userRepository, IRepository<ToDoItem> toDoItemRepository, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _toDoItemRepository = toDoItemRepository;
            _httpContext = httpContext;

        }

        public async Task<List<CreatedToDoItemDTO>> Handle(GetToDoItemsByDateRangeRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContext.HttpContext.Items["User"];


            DateOnly startDate = new DateOnly(request.StartDate.Year, request.StartDate.Month, request.StartDate.Day); // начало дня
            DateOnly endDate = new DateOnly(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day + 1);

            var result = await _toDoItemRepository.FindAllAsync(p => p.CompletionDate >= startDate && p.CompletionDate <= endDate);

            List<ToDoItem> allToDoItems = result.ToList();

            var list = new List<CreatedToDoItemDTO>();

            foreach (ToDoItem toDoItem in allToDoItems)
            {

                if (toDoItem.User == user && toDoItem.ParentItemId == null)
                {
                    var subItemsList = new List<CreatedToDoItemDTO>();

                    if (toDoItem.SubItems == null)
                    {
                        foreach (ToDoItem subItem in toDoItem.SubItems!)
                        {
                            subItemsList.Add(new CreatedToDoItemDTO
                            {
                                Id = toDoItem.Id,
                                Title = toDoItem.Title,
                                Description = toDoItem.Description,
                                CompletionDate = toDoItem.CompletionDate,
                                CreatedDate = toDoItem.CreatedDate,
                                Status = toDoItem.Status
                            });
                        }
                    }

                    list.Add(new CreatedToDoItemDTO
                    {
                        Id = toDoItem.Id,
                        Title = toDoItem.Title,
                        Description = toDoItem.Description,
                        CompletionDate = toDoItem.CompletionDate,
                        CreatedDate = toDoItem.CreatedDate,
                        Status = toDoItem.Status,
                        SubItems = subItemsList
                    });
                }
            }

            return list;
        }
    }
}
