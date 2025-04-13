using GoDentalAPP.ViewModels;
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
using System.Windows.Shapes;

namespace GoDentalAPP.src.GoDentalAPP.APP.Views.ViewsProducto
{
    /// <summary>
    /// Lógica de interacción para ProductosMainWindow.xaml
    /// </summary>
    public partial class ProductosMainWindow : Window
    {
        public ProductosMainWindow()
        {
            InitializeComponent();
            this.DataContext = new ProductosViewModel();

            /* Ejecuta el comando para cargar los datos al iniciar
            if (DataContext is ProductosViewModel viewModel)
            {
                viewModel.MostrarProductosCommand.Execute(null);
            }*/
        }
    }
}
