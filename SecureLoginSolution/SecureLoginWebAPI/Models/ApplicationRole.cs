namespace SecureLoginWebAPI.Models
{
    public class ApplicationRole
    {
		public int id { get; set; }
		public string name { get; set; }
		public string normalizedname { get; set; }

		public ApplicationRole(int id_, string name_, string normalizedname_)
		{
			this.id = id_;
			this.name = name_;
			this.normalizedname = normalizedname_;
		}
	}
}
