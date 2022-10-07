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

namespace ECF_AutoEcole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new LeconsPage();
        }

        private void btnClickLecon(object sender, RoutedEventArgs e)
        {
            Main.Content = new LeconsPage();
        }

        private void btnClickEleve(object sender, RoutedEventArgs e)
        {
            Main.Content = new ElevesPage();
        }

        private void btnClickMoniteur(object sender, RoutedEventArgs e)
        {
            Main.Content = new MoniteursPage();
        }

        private void btnClickModele(object sender, RoutedEventArgs e)
        {
            Main.Content = new ModelesPage();
        }
        private void btnClickVehicule(object sender, RoutedEventArgs e)
        {
            Main.Content = new VehiculesPage();
        }
        private void btnClickCalendrier(object sender, RoutedEventArgs e)
        {
            Main.Content = new CalendrierPage();
        }

        private void main_frame(object sender, EventArgs e)
        {
            Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }

        
    }
}
