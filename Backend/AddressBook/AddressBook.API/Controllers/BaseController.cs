using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}