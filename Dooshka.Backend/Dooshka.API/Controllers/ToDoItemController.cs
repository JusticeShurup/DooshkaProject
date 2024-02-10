using Dooshka.Application.Features.DTOs.ToDoItems;
using Dooshka.Application.Features.ToDoItems.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dooshka.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors("AllowAll")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : Controller
    {
        /// <summary>
        /// Method for create MainToDoItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(ISender sender, [FromBody] CreateToDoItemRequest request)
        {
            var result = await sender.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Method for create SubToDoItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSubItem(ISender sender, [FromBody] CreateSubToDoItemRequest request)
        {
            var result = await sender.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Get all current user ToDoItems. 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(ISender sender)
        {
            var result = await sender.Send(new GetToDoRequest());
            return Ok(result);
        }

        /// <summary>
        /// Method for delete MainToDoItem by Id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteItemById(ISender sender, [FromQuery] Guid id)
        {
            await sender.Send(new DeleteToDoItemRequest() { Id = id });


            return Ok("Успешно удалено");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteSubItemById(ISender sender, [FromQuery] Guid id)
        {
            await sender.Send(new DeleteSubToDoItemRequest() { Id = id });

            return Ok("Успешно удалено");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoItemById(ISender sender, [FromQuery] Guid id)
        {
            var result = await sender.Send(new GetToDoItemByIdRequest() {Id = id});

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubToDoItemById(ISender sender, [FromQuery] Guid id)
        {
            var result = await sender.Send(new GetSubToDoItemByIdRequest() { Id = id });

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dateAsString"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoItemsByCompletionDate(ISender sender, [FromQuery] string dateAsString)
        {
            var result = await sender.Send(new GetToDoItemsByDateRequest() { Date = DateOnly.Parse(dateAsString) });

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="startDateAsString"></param>
        /// <param name="endDateAsString"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoItemsByDateRange(ISender sender, [FromQuery] string startDateAsString, [FromQuery] string endDateAsString)
        {
            var result = await sender.Send(new GetToDoItemsByDateRangeRequest() { StartDate = DateOnly.Parse(startDateAsString), EndDate = DateOnly.Parse(endDateAsString) });

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeToDoItemStatus(ISender sender, [FromQuery] Guid id, [FromQuery] int status)
        {

            await sender.Send(new ChangeToDoItemStatusRequest() { ToDoItemId = id, Status = status });


            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateToDoItem(ISender sender, [FromBody] UpdateToDoItemRequest request)
        {
            var result = await sender.Send(request);

            return Ok(result);
        }
    }
}
