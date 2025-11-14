using Microsoft.AspNetCore.Mvc;
using WebAPI1.Models.DTOs;
using WebAPI1.Services;

namespace WebAPI1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiembrosController : ControllerBase
    {
        private readonly MiembroService _miembroService;

        public MiembrosController(MiembroService miembroService)
        {
            _miembroService = miembroService;
        }

        /// <summary>
        /// Listar todos los miembros
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarMiembros()
        {
            try
            {
                var miembros = await _miembroService.ListarMiembrosAsync();
                return Ok(new
                {
                    success = true,
                    message = "Miembros obtenidos exitosamente",
                    data = miembros
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener un miembro por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerMiembroPorId(int id)
        {
            try
            {
                var miembro = await _miembroService.ObtenerMiembroPorIdAsync(id);
                if (miembro == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"El miembro con Id {id} no existe"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Miembro obtenido exitosamente",
                    data = miembro
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Crear un miembro
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearMiembro([FromBody] CreateMiembroDto dto)
        {
            try
            {
                var miembro = await _miembroService.CrearMiembroAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Miembro creado exitosamente",
                    data = miembro
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Editar un miembro
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarMiembro(int id, [FromBody] UpdateMiembroDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del miembro"
                    });
                }

                var miembro = await _miembroService.EditarMiembroAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Miembro actualizado exitosamente",
                    data = miembro
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Eliminar un miembro
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMiembro(int id)
        {
            try
            {
                var miembro = await _miembroService.EliminarMiembroAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Miembro eliminado exitosamente",
                    data = miembro
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
