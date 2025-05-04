using GoDentalAPP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoDentalAPP.src.GoDentalAPP.APP.Views.ViewsCliente
{
    /// <summary>
    /// Lógica de interacción para ClienteViewPrincipal.xaml
    /// </summary>
    public partial class ClienteViewPrincipal : UserControl
    {
        public ClienteViewPrincipal()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<ClientesViewModel>();
        }
    }
}
