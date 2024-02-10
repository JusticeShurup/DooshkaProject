using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;

namespace Dooshka.Application.Features.ToDoItems.Requests
{
    public class GetToDoItemsByDateRangeRequest : IRequest<List<CreatedToDoItemDTO>>
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

    }
}
