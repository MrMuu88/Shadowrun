using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowrun {
	public sealed class FiringMode {
		#region fields and Properties #############################################################

		public int ID { get; set; }
		public string Mode {get;set;}

		#endregion

		#region Methods ###########################################################################

		#endregion

		#region Ctors #############################################################################

		public FiringMode() {
			Mode = "";
		}

		public FiringMode(string mode):this() {
			Mode = mode;
		}
		#endregion
	}
}
