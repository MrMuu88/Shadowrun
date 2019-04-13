using Shadowrun.Model;
using System.Collections.Generic;

namespace Shadowrun.DataAccess {
	public interface ISkillDataService {
		IEnumerable<Skill> LoadAllSkills();
		IEnumerable<SkillGroup> LoadAllSkillGroups();
		Skill LoadSkillByID(int id);
		void SaveSkill(Skill skill);
		void SaveSkills(IEnumerable<Skill> Skills);
	}
}
