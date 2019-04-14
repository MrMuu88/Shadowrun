using System;
using System.Globalization;
using System.Windows.Data;

namespace Shadowrun.DataLoader.Converters {
	public class EnumToString:IValueConverter{
		#region fields and Properties #############################################################
		
		#endregion
		
		#region Methods ###########################################################################

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			var strValue = (string)value;
			return Enum.Parse(targetType, strValue);
		}
		
		#endregion
		
		#region Ctors #############################################################################
		
		public EnumToString(){}
				
		#endregion
	}
}
