using Microsoft.EntityFrameworkCore;
using Shadowrun.Model;
using System.Collections.Generic;
using System.Linq;

namespace Shadowrun.DataAccess {
    public class DB_SkillDataService : IDataService<Skill> {
        #region fields and Properties #############################################################
        public string ConStr { get; set; }
        public DBType DBType { get; set; }
        #endregion

        #region Methods ###########################################################################

        /// <summary>
        /// Loads the skill groups from the database
        /// </summary>
        /// <returns>Collection of SkillGroup</returns>
        public IEnumerable<SkillGroup> LoadSkillGroups() {
            using (var DB = new ShadowDBContext(ConStr, DBType)) {
            	return DB.SkillGroups.ToList();
            }
        }

        /// <summary>
        /// Loads the Entire Skill object with all related data.
        /// </summary>
        /// <param name="id">the ID of the Skill</param>
        /// <returns>a Skill</returns>
        public Skill LoadByID(int id) {
            using (var DB = new ShadowDBContext(ConStr, DBType)) {
                return DB.Skills.Include(s => s.Specializations)
                                .Include(s=>s.Group)
                                .FirstOrDefault(s => s.ID == id);
            }
        }

        /// <summary>
        /// Generate LookupItems from all the skills in the database
        /// </summary>
        /// <returns>Collection of LookupItems</returns>
        public IEnumerable<LookupItem> LookupItems() {
            using (var DB = new ShadowDBContext(ConStr, DBType)) {
                return DB.Skills.Select(s => new LookupItem(s.ID, s.Name)).ToArray();
            }
        }

        /// <summary>
        /// Saves a Skill to the database
        /// </summary>
        /// <param name="skill">the skill to be saved</param>
        public void Save(Skill skill) {
            using (var DB = new ShadowDBContext(ConStr, DBType)) {
                var specs = DB.Specializations
                    .Where(sp => (sp.Skill == skill) && (!skill.Specializations.Contains(sp)));
                DB.RemoveRange(specs);
                DB.Skills.Update(skill);
                DB.SaveChanges();
            }
        }

        public void Delete(Skill skill) {
            using (var DB = new ShadowDBContext(ConStr, DBType)) {
                var specs = DB.Specializations.Where(sp => sp.Skill == skill);
                DB.Specializations.RemoveRange(specs);
                DB.Skills.Remove(skill);
                DB.SaveChanges();
            }
        }
        
        #endregion

        #region Ctors #############################################################################

        public DB_SkillDataService(string constr, DBType dbt = DBType.MariaDB) {
            ConStr = constr;
            DBType = dbt;
        }

        #endregion
    }
}
