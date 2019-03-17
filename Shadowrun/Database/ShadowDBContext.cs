using Microsoft.EntityFrameworkCore;

namespace Shadowrun.Database {
	public class ShadowDBContext : DbContext {
		#region fields and Properties #############################################################
		private string ConnectionString;

		DbSet<Skill> Skills { get; set; }
		DbSet<Specialization> Specializations { get; set; }

		DbSet<Meele> MeeleWeapons { get; set; }
		DbSet<Projectile> ProjectileWeapons{ get; set; }
		DbSet<FireArm> FireArms{ get; set; }
		DbSet<Explosive> Explosives { get; set; }

		#endregion

		#region Methods ###########################################################################

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseMySql(ConnectionString);
			base.OnConfiguring(optionsBuilder);	
		}

		#endregion

		#region Ctors #############################################################################

		public ShadowDBContext(string Constr) {
			ConnectionString = Constr;
			Database.EnsureCreated();
		}
		#endregion
	}
}
