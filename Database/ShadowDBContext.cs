using Microsoft.EntityFrameworkCore;

namespace Shadowrun.Database {
	public class ShadowDBContext : DbContext {
		#region fields and Properties #############################################################
		private string ConnectionString;
		private DBType DBType;

		public DbSet<Skill> Skills { get; set; }
		public DbSet<Specialization> Specializations { get; set; }

		public DbSet<Meele> MeeleWeapons { get; set; }
		public DbSet<Projectile> ProjectileWeapons{ get; set; }
		public DbSet<FireArm> FireArms{ get; set; }
		public DbSet<Explosive> Explosives { get; set; }

		#endregion

		#region Methods ###########################################################################

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			switch (DBType) {
				case DBType.MariaDB:
					optionsBuilder.UseMySql(ConnectionString);
					break;
				case DBType.SqLite:
					optionsBuilder.UseSqlite(ConnectionString);
					break;
				default:
					throw new System.Exception("Unkown DBType");
					break;
			}
			base.OnConfiguring(optionsBuilder);	
		}

		#endregion

		#region Ctors #############################################################################

		public ShadowDBContext(string Constr,DBType dbt = DBType.MariaDB) {
			ConnectionString = Constr;
			DBType = dbt;
			Database.EnsureCreated();
		}
		#endregion
	}

	public enum DBType{MariaDB,SqLite}
}
