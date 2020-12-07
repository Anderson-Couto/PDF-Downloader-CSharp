using System;
using System.Threading.Tasks;
using apiPhotos.Data;
using Microsoft.AspNetCore.Mvc;

namespace apiPhotos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public UsuarioController(UsuarioRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{IdUser}")]
        public async Task<ActionResult> GetUser(int IdUser)
        {
            try
            {
                var user = await _repository.GetUser(IdUser);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }
    }
}