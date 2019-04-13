using Microsoft.EntityFrameworkCore;
using Shadowrun.Model;

namespace Shadowrun.DataAccess {
	public class ShadowDBContext : DbContext {
		#region fields and Properties #############################################################
		private string ConnectionString;
		private DBType DBType;

		public DbSet<Skill> Skills { get; set; }
		public DbSet<Specialization> Specializations { get; set; }

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
			}
			base.OnConfiguring(optionsBuilder);	
		}

		#endregion

		#region Ctors #############################################################################

		public ShadowDBContext() {
			ConnectionString = "Server = 192.168.1.9; Database = Shadowrun; Uid = BlueFish; Pwd = HoiChummer;";
			DBType = DBType.MariaDB;
		}

		public ShadowDBContext(string Constr,DBType dbt = DBType.MariaDB) {
			ConnectionString = Constr;
			DBType = dbt;
			Database.EnsureCreated();
		}
		#endregion
	}

	public enum DBType{MariaDB,SqLite}
}
