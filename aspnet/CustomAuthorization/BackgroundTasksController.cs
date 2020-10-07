using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize("InternalApiToken")]
    [ApiController]
    public class BackgroundTasksController : ControllerBase
    {
        private readonly IBackgroundTask BackgroundTask;

        public BackgroundTasksController(IBackgroundTask BackgroundTask)
        {
            this.BackgroundTask = BackgroundTask;
        }

        [HttpPost("updateStatus")]
        public async Task<OkResult> UpdateStatus()
        {
            await BackgroundTask.UpdateStatusAsync();
            return Ok();
        }
    }
}
