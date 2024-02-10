using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;

namespace Dooshka.Application.Features.UserFeatures.Requests
{
    public class GetCompletedTaskStatisticRequest : IRequest<List<CreatedToDoItemDTO>> { }
}
