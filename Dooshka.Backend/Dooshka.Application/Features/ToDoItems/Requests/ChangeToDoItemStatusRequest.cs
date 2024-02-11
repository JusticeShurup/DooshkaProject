using MediatR;

namespace Dooshka.Application.Features.ToDoItems.Requests
{
    public class ChangeToDoItemStatusRequest : IRequest
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
}
