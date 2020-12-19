using System.Linq;
using customModelValidation.Models;
using customModelValidation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace customModelValidation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomController : ControllerBase
    {
        private readonly ILogger<CustomController> _logger;
        private readonly AppDbContext _db;

        public CustomController(ILogger<CustomController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost, Route("order")]
        public ActionResult<ApiResponse> AddOrder(OrderModel input)
        {
            var response = new ApiResponse();
            if (!ModelState.IsValid)
            {
                response.ErrorMessage = "Invalid parameteres detacted";
                response.FaultyParameters = ModelState.GetFaultyParametres();
                return Ok(response);
            }

            response.ErrorMessage = _db.posMagaza.Single(x => x.mekanID == input.StoreId).mekanAd;
            response.Success = true;
            //_logger.LogInformation("order requested");
            return Ok(response);
        }
    }
}