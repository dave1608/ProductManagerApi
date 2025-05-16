using Microsoft.AspNetCore.Mvc;
using ProductManagerApi.Models;
using ProductManagerApi.Repository;

namespace ProductManagerApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _userRepository.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserModel user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest();
            }
            var existingUser = await _userRepository.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _userRepository.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.Delete(id);
            return NoContent();
        }
    }
}
