using BETarjetaCredito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BETarjetaCredito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TarjetaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] // GET -- IMPRIMIR TARJETA --
        public async Task<IActionResult> Get()
        {
            try
            {
                //Retorna +
                //+78lista de registros
                var listTarjetas = await _context.TarjetaCredito.ToListAsync();
                return Ok(listTarjetas);
                /*
                  Retorna solo un registro 
                  TarjetaCredito listTarjetas = await _context.TarjetaCredito.FirstOrDefaultAsync();

                   //retornar primer tarjeta de felipe

             TarjetaCredito tarjetaFelipe = await _context.TarjetaCredito
             Where(objetoDeLaTabla => objetoDeLaTabla.Titular == "Felipe")
             .FirstOrDefaultAsync(); //linq

             
                        .ToListAsync();

                   //retornar primer tarjeta de felipe
                    List<TarjetaCredito> tarjetasconfecha2024 = await _context.TarjetaCredito
                        .Where(objetoDeLaTabla => objetoDeLaTabla.FechaExpiracion > new DateTime(2023, 12, 31))
                        .ToListAsync();

                   //retornar primer tarjeta de felipe
                    List<TarjetaCredito> tarjeta2defelipe = await _context.TarjetaCredito
                        .Where(objetoDeLaTabla => objetoDeLaTabla.Id > 3)
                        .ToListAsync();

                   //retornar primer tarjeta de mariano
                        TarjetaCredito tarjetaMariano = await _context.TarjetaCredito
                         .Where(objetoDeLaTabla => objetoDeLaTabla.Titular == "Mariano")
                         .FirstOrDefaultAsync();

                    if (tarjetasdeFelipe != null)
                    {
                        return Ok(tarjetasdeFelipe);
                    }
                    else
                    {
                        return Ok(listTarjetas); */
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /* ----- GET DONDE DEVUELVE 1 SOLA TARJETA DE CREDITO ---------
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST api/<TarjetaController>
        [HttpPost] // POST -- ALTA DE TARJETA --
        public async Task<IActionResult> Post([FromBody] TarjetaCredito tarjeta)
        {
            try
            {
                _context.Add(tarjeta);
                await _context.SaveChangesAsync();
                return Ok(tarjeta);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT -- MODIFICAR TARJETA --
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TarjetaCredito tarjeta)
        {
            try
            {
                if (id != tarjeta.Id)
                {
                    return NotFound();
                }

                _context.Update(tarjeta);
                await _context.SaveChangesAsync();
                return Ok(new { message = "La tarjeta fue actualizada exitosamente." });

                }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE -- ELIMINAR TARJETA --
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tarjeta = await _context.TarjetaCredito.FindAsync(id);

                if (tarjeta == null)
                {
                    return NotFound(tarjeta);
                }

                _context.TarjetaCredito.Remove(tarjeta);
                await _context.SaveChangesAsync(true);
                return Ok(new { message = "La tarjeta fue eliminada exitosamente." });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
