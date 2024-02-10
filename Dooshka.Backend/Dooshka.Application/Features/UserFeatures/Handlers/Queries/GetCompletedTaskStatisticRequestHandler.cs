using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.UserFeatures.Requests;
using Dooshka.Application.Persistence.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dooshka.Application.Features.UserFeatures.Handlers.Queries
{
    public class GetCompletedTaskStatisticRequestHandler : IRequestHandler<GetCompletedTaskStatisticRequest, List<CreatedToDoItemDTO>>
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCompletedTaskStatisticRequestHandler(IRepository<ToDoItem> toDoItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _toDoItemRepository = toDoItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CreatedToDoItemDTO>> Handle(GetCompletedTaskStatisticRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContextAccessor.HttpContext.Items["User"];

            List<ToDoItem> result = _toDoItemRepository.FindAllAsync(x => x.UserId == user.Id && x.Status == ToDoItemStatusType.Done && x.ParentItemId == null).Result.ToList();

            List<CreatedToDoItemDTO> createdToDoItemDTOs = new List<CreatedToDoItemDTO>();

            foreach (var item in result)
            {
                createdToDoItemDTOs.Add(
                    new CreatedToDoItemDTO
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Title = item.Title,
                        Description = item.Description,
                        Status = item.Status,
                        CompletionDate = item.CompletionDate
                    });
            }

            return createdToDoItemDTOs;
        }

        /*
        public async Task<int> Handle(GetCompletedTaskStatisticQuery request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContextAccessor.HttpContext.Items["User"];

            List<ToDoItem> result = _toDoItemRepository.FindAll(x => x.UserId == user.Id && x.Status == ToDoItemStatusType.Done).Result.ToList();

            return result.Count;
        }
        */



    }
}


