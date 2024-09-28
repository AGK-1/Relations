public class Author
{
	public int Id { get; set; }
	public string Name { get; set; }

	// Navigation property for the one-to-many relationship
	public List<Book> Books { get; set; }
}

public class Book
{
	public int Id { get; set; }
	public string Title { get; set; }

	// Foreign key
	public int AuthorId { get; set; }

	// Navigation property
	public Author Author { get; set; }
}
