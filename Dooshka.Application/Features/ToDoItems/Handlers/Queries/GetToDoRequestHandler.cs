using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.ToDoItems.Requests;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.ToDoItemsLogic.Handlers
{
    public class GetToDoRequestHandler : IRequestHandler<GetToDoRequest, List<CreatedToDoItemDTO>>
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContext;

        public GetToDoRequestHandler(IRepository<ToDoItem> toDoItemRepository, IRepository<User> userRepository, IHttpContextAccessor httpContext) 
        {
            _toDoItemRepository = toDoItemRepository;
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        public async Task<List<CreatedToDoItemDTO>> Handle(GetToDoRequest request, CancellationToken cancellationToken)
        {
            var userIdentity = _httpContext.HttpContext.User.Identity;
            var email = userIdentity.Name;


            User user = _userRepository.Find(p => p.Email == email);

            List<ToDoItem> allMainToDoItems = _toDoItemRepository.FindAllAsync(p => p.UserId == user.Id && p.ParentItemId == null).Result.ToList();

            var list = new List<CreatedToDoItemDTO>();

            foreach (ToDoItem toDoItem in allMainToDoItems)
            {
                var subItemsList = new List<CreatedToDoItemDTO>();

                List<ToDoItem> subItems = _toDoItemRepository.FindAllAsync(p => p.ParentItemId == toDoItem.Id).Result.ToList();

                foreach (var subItem in subItems)
                {
                    subItemsList.Add(new CreatedToDoItemDTO
                    {
                        Id = subItem.Id,
                        Title = subItem.Title,
                        Description = subItem.Description,
                        CompletionDate = subItem.CompletionDate,
                        CreatedDate = subItem.CreatedDate,
                        Status = subItem.Status
                    });
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
            
           

            return await Task.FromResult(list);
        }
    }
}
