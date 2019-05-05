using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Shadowrun.DataLoader.Views {
	/// <summary>
	/// Interaction logic for SkillDetailView.xaml
	/// </summary>
	public partial class SkillDetailView : UserControl
    {
        public SkillDetailView()
        {
            InitializeComponent();
        }

		private void TextBox_LostFocus(object sender, RoutedEventArgs e) {
			Debug.WriteLine($"Testing {DataContext.GetType().Name}");
		}

       
    }
}
