using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Shadowrun.Database;

namespace Tester {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
	 public static ShadowDBContext DB = new ShadowDBContext("Server = 192.168.1.9; Database=Shadowrun;Uid=BlueFish;Pwd=HoiChummer;");
    }
}
