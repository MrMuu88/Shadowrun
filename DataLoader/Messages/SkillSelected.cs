namespace Shadowrun.DataLoader.Messages {

    public class SkillSelected {
        #region Fields,Properties,Events ##############################################################
        public int SkillID { get; private set; }


        #endregion

        #region Methods, Tasks, commands ##############################################################



        #endregion

        #region Ctors #################################################################################

        public SkillSelected() { }
        public SkillSelected(int id) {
            SkillID = id;
        }

        #endregion
    }//clss

}//ns
