using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.ToDoItems.Requests;
using Dooshka.Application.Persistence.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application.Features.ToDoItems.Handlers.Queries
{
    public class GetToDoItemByIdRequestHandler : IRequestHandler<GetToDoItemByIdRequest, CreatedToDoItemDTO>
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContext;

        public GetToDoItemByIdRequestHandler(IRepository<ToDoItem> toDoItemRepository, IRepository<User> userRepository, IHttpContextAccessor httpContext)
        {
            _toDoItemRepository = toDoItemRepository;
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        public async Task<CreatedToDoItemDTO> Handle(GetToDoItemByIdRequest request, CancellationToken cancellationToken)
        {
            var userIdentity = _httpContext.HttpContext.User.Identity;
            var email = userIdentity.Name;


            User user = _userRepository.Find(p => p.Email == email);

            ToDoItem? toDoItem = _toDoItemRepository.Find(p => p.Id == request.Id);

            if (toDoItem == null)
            {
                throw new NotFoundException("ToDoItem doesn't found");
            }

            if (toDoItem.UserId != user.Id)
            {
                throw new BadRequestException("ToDoitem isn't yours");
            }


            List<ToDoItem> allSubItems = _toDoItemRepository.FindAllAsync(p => p.ParentItemId == request.Id).Result.ToList();

            List<CreatedToDoItemDTO> subItems = new();


            foreach (ToDoItem subToDoItem in allSubItems)
            {
                subItems.Add(new CreatedToDoItemDTO
                {
                    Id = subToDoItem.Id,
                    Title = subToDoItem.Title,
                    Description = subToDoItem.Description,
                    CompletionDate = subToDoItem.CompletionDate,
                    CreatedDate = subToDoItem.CreatedDate,
                    Status = subToDoItem.Status
                });
            }

            var item = new CreatedToDoItemDTO()
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                Description = toDoItem.Description,
                CompletionDate = toDoItem.CompletionDate,
                CreatedDate = toDoItem.CreatedDate,
                Status = toDoItem.Status,
                SubItems = subItems
            };



            return await Task.FromResult(item);
        }
    }
}
