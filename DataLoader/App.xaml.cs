using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Shadowrun.DataLoader {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		public static Database.ShadowDBContext DB = new Database.ShadowDBContext("Server = 192.168.1.9; Database=Shadowrun;Uid=BlueFish;Pwd=HoiChummer;");

		public App() {
			
		}
	}
}
