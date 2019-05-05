namespace Shadowrun.DataAccess {

    public class LookupItem {
        #region Fields,Properties,Events ##############################################################

        public int ID { get; set; }
        public string DisplayText { get; set; }

        #endregion

        #region Methods, Tasks, commands ##############################################################



        #endregion

        #region Ctors #########################################################################

        public LookupItem() { }

        public LookupItem(int id,string text) {
            ID = id;
            DisplayText = text;
        }

        #endregion
    }//clss

}//ns
