using Microsoft.AspNetCore.Mvc;

namespace TAdmin.Web.Controllers
{ 
    [ApiController]
    [Route("data")]
    public class DataSourceController : ControllerBase
    {
        // GET
        public string Index()
        {
            return "test";
        }
    }
}