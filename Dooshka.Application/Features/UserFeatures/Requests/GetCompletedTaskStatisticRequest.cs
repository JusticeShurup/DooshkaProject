using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;

namespace BLL.UserLogic.Queries
{
    public class GetCompletedTaskStatisticRequest : IRequest<List<CreatedToDoItemDTO>> {}
}
