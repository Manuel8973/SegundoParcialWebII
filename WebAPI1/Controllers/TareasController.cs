using Microsoft.AspNetCore.Mvc;
using WebAPI1.Models.DTOs;
using WebAPI1.Services;

namespace WebAPI1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly TareaService _tareaService;

        public TareasController(TareaService tareaService)
        {
            _tareaService = tareaService;
        }

        /// <summary>
        /// 1. Listar todas las tareas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarTareas()
        {
            try
            {
                var tareas = await _tareaService.ListarTareasAsync();
                return Ok(new
                {
                    success = true,
                    message = "Tareas obtenidas exitosamente",
                    data = tareas
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
        /// 2. Crear tarea
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearTarea([FromBody] CreateTareaDto dto)
        {
            try
            {
                var tarea = await _tareaService.CrearTareaAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Tarea creada exitosamente",
                    data = tarea
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
        /// 3. Editar tarea
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTarea(int id, [FromBody] UpdateTareaDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID de la tarea"
                    });
                }

                var tarea = await _tareaService.EditarTareaAsync(dto);
                return Ok(new
                {
                    success = true,
                    message = "Tarea actualizada exitosamente",
                    data = tarea
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
        /// 4. Eliminar tarea
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(int id)
        {
            try
            {
                var tarea = await _tareaService.EliminarTareaAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Tarea eliminada exitosamente",
                    data = tarea
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
        /// 5. Ordenar tareas por prioridad
        /// </summary>
        [HttpGet("ordenar-por-prioridad")]
        public async Task<IActionResult> OrdenarPorPrioridad()
        {
            try
            {
                var tareas = await _tareaService.OrdenarTareasPorPrioridadAsync();
                return Ok(new
                {
                    success = true,
                    message = "Tareas ordenadas por prioridad",
                    data = tareas
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
        /// 6. Buscar tareas por título
        /// </summary>
        [HttpGet("buscar/{titulo}")]
        public async Task<IActionResult> BuscarPorTitulo(string titulo)
        {
            try
            {
                var tareas = await _tareaService.BuscarTareasPorTituloAsync(titulo);
                return Ok(new
                {
                    success = true,
                    message = $"Búsqueda completada. Se encontraron {tareas.Count} tarea(s)",
                    data = tareas
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
        /// 7. Listar tareas asignadas a un miembro
        /// </summary>
        [HttpGet("miembro/{idMiembro}")]
        public async Task<IActionResult> ListarPorMiembro(int idMiembro)
        {
            try
            {
                var tareas = await _tareaService.ListarTareasPorMiembroAsync(idMiembro);
                return Ok(new
                {
                    success = true,
                    message = $"Se encontraron {tareas.Count} tarea(s) asignadas al miembro",
                    data = tareas
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
        /// Obtener una tarea por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTareaPorId(int id)
        {
            try
            {
                var tarea = await _tareaService.ObtenerTareaPorIdAsync(id);
                if (tarea == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"La tarea con Id {id} no existe"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Tarea obtenida exitosamente",
                    data = tarea
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
