using System.Configuration;
using System.Data;
using System.Windows;

namespace GoDentalAPP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static object ServiceProvider { get; internal set; }
    }

}
