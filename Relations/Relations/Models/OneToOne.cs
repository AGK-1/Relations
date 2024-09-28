public class User
{
	public int Id { get; set; }
	public string Name { get; set; }

	// Navigation property
	public UserProfile UserProfile { get; set; }
}

public class UserProfile
{
	public int Id { get; set; }
	public string Address { get; set; }

	// Foreign key
	public int UserId { get; set; }

	// Navigation property
	public User User { get; set; }
}

