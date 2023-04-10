using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BP_RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public ClassesController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassModel>>> Get()
        {
            return await _dbContext.Classes.ToListAsync();
        }
        [HttpGet("/class/Find/{id}")]
        public async Task<ActionResult<ClassModel>> GetSecret(int id)
        {
            var ClassModel = await _dbContext.Classes.FindAsync(id);
            if (ClassModel == null)
            {
                return NotFound();
            }
            return ClassModel;
        }
        [HttpPost("/class/Create/{IdTeacher}/{Region}/{District}/{City}/{Name}/{Grade}")]
        public async Task<ActionResult<ClassModel>> CreateOwnClass(ClassModel addedClass)
        {
            _dbContext.Classes.Add(addedClass);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = addedClass.Id }, addedClass);
        }
        [HttpDelete("/class/Delete/{Name}/{Grade}")]
        public async Task<ActionResult<ClassModel>> DeleteClass(string name, string grade)
        {
            var deletedClass = await _dbContext.Classes.FirstOrDefaultAsync(x => x.Name == name && x.Grade == Convert.ToInt32(grade));
            if(deletedClass == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Classes.Remove(deletedClass);
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
