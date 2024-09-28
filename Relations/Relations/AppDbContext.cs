using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<UserProfile> UserProfiles { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<Book> Books { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<StudentCourse> StudentCourses { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// One-to-One relationship
		modelBuilder.Entity<User>().HasData(
			new User { Id = 1, Name = "John Doe" },
			new User { Id = 2, Name = "Jane Doe" }
		);

		modelBuilder.Entity<UserProfile>().HasData(
			new UserProfile { Id = 1, Address = "123 Main St 52", UserId = 1 },
			new UserProfile { Id = 2, Address = "456 Elm St", UserId = 2 }
		);
		
		modelBuilder.Entity<User>()
			.HasOne(u => u.UserProfile)
			.WithOne(up => up.User)
			.HasForeignKey<UserProfile>(up => up.UserId);


		///////////////////////////
		// one-to-many relationship
		modelBuilder.Entity<Author>().HasData(
             new Author { Id = 1, Name = "J.K. Rowling" },
             new Author { Id = 2, Name = "George R.R. Martin" }
        );

		modelBuilder.Entity<Book>().HasData(
			new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", AuthorId = 1 },
			new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", AuthorId = 1 },
			new Book { Id = 3, Title = "A Game of Thrones", AuthorId = 1 },
			new Book { Id = 4, Title = "A Clash of Kings", AuthorId = 1 }
		);

		modelBuilder.Entity<Author>()
			.HasMany(a => a.Books)
			.WithOne(b => b.Author)
			.HasForeignKey(b => b.AuthorId);



		// many-to-many relationship
		modelBuilder.Entity<Student>().HasData(
		   new Student { Id = 1, Name = "John Doe" },
		   new Student { Id = 2, Name = "Jane Smith" }
		 );

		// Seeding Courses
		modelBuilder.Entity<Course>().HasData(
			new Course { Id = 1, CourseName = "Math" },
			new Course { Id = 2, CourseName = "Science" }
		);

		// Seeding StudentCourse (Join Table)
		modelBuilder.Entity<StudentCourse>().HasData(
			new StudentCourse { StudentId = 1, CourseId = 1 }, // John Doe enrolled in Math
			new StudentCourse { StudentId = 1, CourseId = 2 }, // John Doe enrolled in Science
			new StudentCourse { StudentId = 2, CourseId = 1 }  // Jane Smith enrolled in Math
		);


		modelBuilder.Entity<StudentCourse>()
		   .HasKey(sc => new { sc.StudentId, sc.CourseId }); // Composite key

		modelBuilder.Entity<StudentCourse>()
			.HasOne(sc => sc.Student)
			.WithMany(s => s.StudentCourses)
			.HasForeignKey(sc => sc.StudentId);

		modelBuilder.Entity<StudentCourse>()
			.HasOne(sc => sc.Course)
			.WithMany(c => c.StudentCourses)
			.HasForeignKey(sc => sc.CourseId);
	}
}
