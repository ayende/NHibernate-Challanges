using System.Collections.Generic;

namespace TrickyBug
{
	public class User
	{
		public virtual string Username { get; set; }
		public virtual ICollection<string> AllowedPaths { get; set; }

		public User()
		{
			AllowedPaths = new HashSet<string>();
		}
	}
}