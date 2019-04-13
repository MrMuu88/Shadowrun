using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Shadowrun.DataAccess {
	public class DB_SkillDataService : ISkillDataService  {
		#region fields and Properties #############################################################
		public IMessenger Messenger { get; set; }
		public string ConStr { get; set; }
		public DBType DBType { get; set; }
		#endregion

		#region Methods ###########################################################################
		public IEnumerable<Skill> LoadAllSkills() {
			using (var DB = new ShadowDBContext(ConStr,DBType)) {
				return DB.Skills.Include(s=>s.Specializations).ToList();
			}
		}

		/// <summary>
		/// Loads the Entire Skill object with all related data.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Skill LoadSkillByID(int id) {
			using (var DB = new ShadowDBContext(ConStr,DBType)) {
				return DB.Skills.Include(s => s.Specializations).FirstOrDefault(s => s.ID == id);
			}
		}

		#endregion

		#region Ctors #############################################################################

		public DB_SkillDataService(IMessenger messenger,string constr,DBType dbt=DBType.MariaDB) {
			Messenger = messenger;
			ConStr = constr;
			DBType = dbt;
		}

		#endregion
	}
}
