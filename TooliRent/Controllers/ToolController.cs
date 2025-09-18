using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.ToolDtos;
using Services.Service_Interfaces;

namespace TooliRent.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolController : ControllerBase
    {
        private readonly IToolService _toolService;
        public ToolController(IToolService toolService)
        {
            _toolService = toolService;
        }

        //this one kind of works
        //get all tools
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ToolDto>), StatusCodes.Status200OK)]
        //difference between iaction and actionresult - actionresult better for webapi and returns more clearer and specific results 
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetAllTools()
        {
            var tools = await _toolService.GetAllToolsAsync();

            if (tools is null || !tools.Any()) return NotFound("No tools found.");

            //else
            return Ok(tools);
        }

        // Get tool by id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ToolDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //not using ienumerable  - just want to return one tool 
        public async Task<ActionResult<ToolDto>> GetToolById(int id)
        {
            var tool = await _toolService.GetToolByIdAsync(id);

            if (tool == null) return NotFound($"Could not find the id {id}");

            //else
            return Ok(tool);
        }

        // Create a new tool
        [HttpPost]
        [ProducesResponseType(typeof(ToolDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ToolDto>> CreateTool([FromBody] ToolCreateDto toolCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdTool = await _toolService.AddToolAsync(toolCreateDto);
            return CreatedAtAction(nameof(GetToolById), new { id = createdTool.ToolId }, createdTool);
        }

        // Update an existing tool
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ICollection<ToolUpdateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTool(int id, [FromBody] ToolUpdateDto toolUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedTool = await _toolService.UpdateToolAsync(toolUpdateDto, id);

            if (updatedTool is null)
            {
                return NotFound($"Tool with id {id} not found");
            }

            //else
            return Ok(updatedTool);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTool(int id)
        {
            var result = await _toolService.DeleteToolAsync(id);

            if (!result) return NotFound($"Tool with id {id} not found");
            
            return NoContent();
        }
    }
}
