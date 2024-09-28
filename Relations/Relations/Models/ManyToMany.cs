public class Student
{
	public int Id { get; set; }
	public string Name { get; set; }

	// Navigation property for many-to-many relationship
	public List<StudentCourse> StudentCourses { get; set; }
}

public class Course
{
	public int Id { get; set; }
	public string CourseName { get; set; }

	// Navigation property for many-to-many relationship
	public List<StudentCourse> StudentCourses { get; set; }
}

// Join table
public class StudentCourse
{
	public int StudentId { get; set; }
	public Student Student { get; set; }

	public int CourseId { get; set; }
	public Course Course { get; set; }
}
