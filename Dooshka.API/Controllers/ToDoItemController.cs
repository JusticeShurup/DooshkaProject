using BLL.ToDoItemsLogic.Commands;
using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.ToDoItems.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [EnableCors("AllowAll")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : Controller
    {

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(ISender sender, [FromBody] CreateToDoItemRequest request)
        {
            var result = await sender.Send(request);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSubItem(ISender sender, [FromBody] CreateToDoItemRequest request)
        {
            var result = await sender.Send(request);

            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Get(ISender sender)
        {
            List<CreatedToDoItemDTO> result;
            try
            {
                result = await sender.Send(new GetToDoRequest());
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteItemById(ISender sender, [FromQuery] Guid id)
        {
            DeleteToDoItemResponseDTO result;
            try
            {
               result = await sender.Send(new DeleteToDoItemRequest() { Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoItemsByCompletionDate(ISender sender, [FromQuery] string dateAsString)
        {
            List<CreatedToDoItemDTO> result;
            try
            {
                result = await sender.Send(new GetToDoItemsByDateRequest() { Date = DateOnly.Parse(dateAsString)});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoItemsByDateRange(ISender sender, [FromQuery] string startDateAsString, [FromQuery] string endDateAsString)
        {
            List<CreatedToDoItemDTO> result;
            try
            {
                result = await sender.Send(new GetToDoItemsByDateRangeRequest() { StartDate = DateOnly.Parse(startDateAsString), EndDate = DateOnly.Parse(endDateAsString) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeToDoItemStatus(ISender sender, [FromQuery] Guid id, [FromQuery] int status)
        {
            
            await sender.Send(new ChangeToDoItemStatusRequest() { ToDoItemId = id, Status = status});


            return Ok(); 
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateToDoItem(ISender sender, [FromBody] UpdateToDoItemRequest command)
        {
            var result = await sender.Send(command);
        
            return Ok(result);
        }
    }
}
