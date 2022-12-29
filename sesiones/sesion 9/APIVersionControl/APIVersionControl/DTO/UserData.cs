namespace APIVersionControl.DTO
{
	public class User
	{
		public string? Id { get; set; }
		public string? Name { get; set; }

		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Picture { get; set; }

	}

	public class UsersResponseData
	{
		public User[] data { get; set; }
		public int total { get; set; }
		public int page { get; set; }
		public int limit { get; set; }
	}
}
