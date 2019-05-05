using Shadowrun.Model;
using System.Collections.Generic;

namespace Shadowrun.DataAccess {
	public interface IDataService<T> {
        IEnumerable<LookupItem> LookupItems();
        //should use the Looukupitems<T>
		//IEnumerable<SkillGroup> LoadSkillGroups();
		T LoadByID(int id);
		void Save(T obj);
        void Delete(T obj);
	}
}
