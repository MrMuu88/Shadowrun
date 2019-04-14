using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using Shadowrun.Model;
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
			//using (var DB = new ShadowDBContext(ConStr,DBType)) {
			//	return DB.Skills.Include(s=>s.Specializations).ToList();
			//}
			var sg = new SkillGroup() { ID = 0, Name = "group Zero" };

			var Specs1 = new List<Specialization>();
			Specs1.Add(new Specialization("Spec1") { ID=0});
			Specs1.Add(new Specialization("Spec2") { ID=1});

			var Specs2 = new List<Specialization>();
			Specs2.Add(new Specialization("Spec3") { ID=2});
			Specs2.Add(new Specialization("Spec4") { ID=3});

			yield return new Skill() {
				ID = 0,
				Name = "Test Skill 1",
				Group = sg,
				LinkedTo = Attribute.Body,
				Type = SkillType.Active,
				CanDefault = true,
				Description = "the first test skill",
				Specializations = Specs1
			};

			yield return new Skill() {
				ID = 1,
				Name = "Test Skill 2",
				Group = sg,
				LinkedTo = Attribute.Logic,
				Type = SkillType.Knowledge,
				CanDefault = false,
				Description = "the Second test skill",
				Specializations = Specs2
			};
		}

		public IEnumerable<SkillGroup> LoadAllSkillGroups() {
			//using (var DB = new ShadowDBContext(ConStr, DBType)) {
			//	return DB.SkillGroups.ToList();
			//}
			yield return new SkillGroup() { ID = 0, Name = "group Zero" };
			yield return new SkillGroup() { ID = 1, Name = "first group" };
			yield return new SkillGroup() { ID = 2, Name = "second group" };
			yield return new SkillGroup() { ID = 3, Name = "third group" };
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

		public void SaveSkill(Skill skill) {
			using (var DB = new ShadowDBContext(ConStr, DBType)) {
				DB.Skills.Update(skill);
				DB.SaveChanges();
			}
		}

		public void SaveSkills(IEnumerable<Skill> Skills) {
			using (var DB = new ShadowDBContext(ConStr, DBType)) {
				DB.Skills.UpdateRange(Skills);
				DB.SaveChanges();
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
