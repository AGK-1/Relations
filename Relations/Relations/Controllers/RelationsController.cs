using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Relations.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RelationsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public RelationsController(AppDbContext context)
		{
			_context = context;
		}



		// GET: api/users
		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _context.Users
				.Include(u => u.UserProfile)
				.Select(u => new 
				{
					Id = u.Id,
					Name = u.Name,
					Address = u.UserProfile.Address
				})
				.ToListAsync();

			return Ok(users);
		}

		[HttpGet("one-to-one")]
		public async Task<ActionResult<IEnumerable<object>>> GetUsers()
		{
			// Project to an anonymous type containing only the properties you want
			var users = await _context.Users
				.Include(u => u.UserProfile) // Include UserProfile if needed
				.Select(u => new
				{
					u.Id,
					u.Name,
					Address = u.UserProfile.Address // Directly access the property
				})
				.ToListAsync();

			return Ok(users);
		}

		[HttpGet("authors-books")]
		public async Task<ActionResult<IEnumerable<object>>> GetAuthorsWithBooks()
		{
			// Project to an anonymous type containing only the properties you want
			var authors = await _context.Authors
				.Include(a => a.Books) // Include related Books
				.Select(a => new
				{
					a.Id,
					a.Name,
					Books = a.Books.Select(b => new
					{
						b.Id,
						b.Title
					}).ToList() // Project the properties you want from the Books collection
				})
				.ToListAsync();

			return Ok(authors);
		}

		[HttpGet("students-with-courses")]
		public async Task<ActionResult<IEnumerable<object>>> GetStudentsWithCourses()
		{
			var students = await _context.Students
				.Include(s => s.StudentCourses)
				.ThenInclude(sc => sc.Course) // Include the related courses through the join table
				.Select(s => new
				{
					s.Id,
					s.Name,
					Courses = s.StudentCourses.Select(sc => new
					{
						sc.Course.Id,
						sc.Course.CourseName
					}).ToList()
				})
				.ToListAsync();

			return Ok(students);
		}
	}
}
