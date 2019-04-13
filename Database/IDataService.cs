using System.Collections.Generic;

namespace Shadowrun.DataAccess {
	public interface IDataService {
		IEnumerable<Skill> LoadAllSkills();
		Skill LoadSkillByID(int id);
	}
}
