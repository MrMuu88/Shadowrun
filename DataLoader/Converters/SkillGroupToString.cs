using Shadowrun.DataLoader.ViewModels;
using Shadowrun.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Shadowrun.DataLoader.Converters {
	public class SkillGroupToString:IValueConverter{
		#region fields and Properties #############################################################
		
		#endregion
		
		#region Methods ###########################################################################
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			var groupname = (string)value;

			var exists = SkillWrapperVM.Groups.Any(sg => sg.Name.ToUpper() == groupname.ToUpper());
			if (exists) {
				return SkillWrapperVM.Groups.FirstOrDefault(sg => sg.Name.ToUpper() == groupname.ToUpper());
			} else { 
				var nsg = new SkillGroup((string)value);
				SkillWrapperVM.Groups.Add(nsg);
				return nsg;
			}
		}
		
		#endregion

		#region Ctors #############################################################################

		public SkillGroupToString(){}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value != null) {
				return ((SkillGroup)value).Name;
			} else {
				return null;
			}
		}


		#endregion
	}
}
