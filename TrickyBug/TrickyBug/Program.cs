using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace TrickyBug
{
	public class Program
	{
		public static void Main()
		{
			Configuration configuration = new Configuration().Configure("nhibernate.config");
			ISessionFactory sessionFactory = configuration.BuildSessionFactory();
			new SchemaExport(configuration).Create(true, true);


			using (ISession s = sessionFactory.OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				s.Save(new User
				{
					Username = "ayende",
					AllowedPaths = {"/users", "/meetings"}
				});

				tx.Commit();
			}

			using (ISession s = sessionFactory.OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				var user = s.Get<User>("Ayende");
				Console.WriteLine("Found user {0}", user.Username);
				Console.WriteLine("Allowed paths:");
				foreach (string allowedPath in user.AllowedPaths)
				{
					Console.WriteLine("\t{0}", allowedPath);
				}

				tx.Commit();
			}
		}
	}
}