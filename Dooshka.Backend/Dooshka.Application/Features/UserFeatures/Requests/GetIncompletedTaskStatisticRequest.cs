using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;

namespace Dooshka.Application.Features.UserFeatures.Requests
{
    public class GetIncompletedTaskStatisticRequest : IRequest<List<CreatedToDoItemDTO>>
    {
    }
}
