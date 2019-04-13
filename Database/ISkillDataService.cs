using System.Collections.Generic;

namespace Shadowrun.DataAccess {
	public interface ISkillDataService {
		IEnumerable<Skill> LoadAllSkills();
		Skill LoadSkillByID(int id);
	}
}
