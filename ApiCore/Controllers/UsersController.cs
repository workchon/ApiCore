using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPut]
        public async Task<ActionResult<List<Users>>> LogIn(Users request)
        {
            var user = await _dataContext.users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user != null)
            {
                if (request.Password == user.Password)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("password incorrecta");
                }
            }
            else { 
                return BadRequest("Usuario no encontrado");
            }
        }


        [HttpPost]
        public async Task<ActionResult<List<Users>>> AddUsers(Users request) {

            var user = _dataContext.users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null)
            {
                _dataContext.users.Add(request);
                await _dataContext.SaveChangesAsync();
                return Ok("Usuario creado correctamente");
            }
            else {
                return BadRequest("Usuario ya registrado");
            }

        }

    }
}
