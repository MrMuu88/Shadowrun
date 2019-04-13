using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using System.Windows;

namespace Shadowrun.DataLoader {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		//"Server = 192.168.1.9; Database=Shadowrun;Uid=BlueFish;Pwd=HoiChummer;"

		public static IMessenger Messenger { get; set; } = new Messenger();

		public App() {}

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			//usnig a bootstrapper at this point would be an overkill

			var DataServ = new ShadowDBDataService(Messenger,"Server = 192.168.1.9; Database=Shadowrun;Uid=BlueFish;Pwd=HoiChummer;");
			var VM = new ViewModels.MainWindow_VM(DataServ,Messenger);
			var window = new Views.MainWindow() { DataContext = VM};
			window.Show();
		}
	}
}
