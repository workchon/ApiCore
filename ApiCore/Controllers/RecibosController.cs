using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecibosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RecibosController(DataContext dataContext) {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recibos>>> Get()
        {
            return Ok(await _dataContext.recibos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Recibos>>> Get(int id)
        {
            var dbRecibo = await _dataContext.recibos.Where(x => x.UserId == id).ToListAsync();
            if (dbRecibo == null)
                return BadRequest("Recibo no encontrado");
            return Ok(dbRecibo);
        }

        [HttpPost]
        public async Task<ActionResult<List<Recibos>>> AddRecibo(Recibos request) {

            _dataContext.recibos.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.recibos.Where(x => x.UserId == request.UserId).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Recibos>>> UpdateRecibo(Recibos request)
        {
            var dbRecibo = await _dataContext.recibos.FindAsync(request.Id);
            if (dbRecibo == null)
                return BadRequest("Recibo no encontrado");

            dbRecibo.Proveedor = request.Proveedor;
            dbRecibo.Comentario = request.Comentario;
            dbRecibo.Fecha = request.Fecha;
            dbRecibo.Moneda = request.Moneda;
            dbRecibo.Monto = request.Monto;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.recibos.Where(x => x.UserId == request.UserId).ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Recibos>>> Delete(int id)
        {

            var dbRecibo = await _dataContext.recibos.FindAsync(id);
            if (dbRecibo == null)
                return BadRequest("Recibo no encontrado");

            _dataContext.recibos.Remove(dbRecibo);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.recibos.Where(x => x.UserId == dbRecibo.UserId).ToListAsync());
        }
    }
}
