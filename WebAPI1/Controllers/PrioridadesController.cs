using Microsoft.AspNetCore.Mvc;
using WebAPI1.Models.DTOs;
using WebAPI1.Services;

namespace WebAPI1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrioridadesController : ControllerBase
    {
        private readonly PrioridadService _prioridadService;

        public PrioridadesController(PrioridadService prioridadService)
        {
            _prioridadService = prioridadService;
        }

        /// <summary>
        /// Listar todas las prioridades
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarPrioridades()
        {
            try
            {
                var prioridades = await _prioridadService.ListarPrioridadesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Prioridades obtenidas exitosamente",
                    data = prioridades
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
        /// Obtener una prioridad por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPrioridadPorId(int id)
        {
            try
            {
                var prioridad = await _prioridadService.ObtenerPrioridadPorIdAsync(id);
                if (prioridad == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"La prioridad con Id {id} no existe"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Prioridad obtenida exitosamente",
                    data = prioridad
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
        /// Crear una prioridad
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearPrioridad([FromBody] CreatePrioridadDto dto)
        {
            try
            {
                var prioridad = await _prioridadService.CrearPrioridadAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Prioridad creada exitosamente",
                    data = prioridad
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
        /// Editar una prioridad
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarPrioridad(int id, [FromBody] UpdatePrioridadDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID de la prioridad"
                    });
                }

                var prioridad = await _prioridadService.EditarPrioridadAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Prioridad actualizada exitosamente",
                    data = prioridad
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
        /// Eliminar una prioridad
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPrioridad(int id)
        {
            try
            {
                var prioridad = await _prioridadService.EliminarPrioridadAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Prioridad eliminada exitosamente",
                    data = prioridad
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
