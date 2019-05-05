using Shadowrun.Model;
using System.Collections.Generic;

namespace Shadowrun.DataAccess {
	public interface ISkillDataService {
        IEnumerable<LookupItem> LookupSkills();
		IEnumerable<SkillGroup> LoadSkillGroups();
		Skill LoadSkillByID(int id);
		void SaveSkill(Skill skill);
        void DeleteSkill(Skill skill);
	}
}
