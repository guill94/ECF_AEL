using ECF_AutoEcole.Data;
using ECF_AutoEcole.Models;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECF_AutoEcole
{
    /// <summary>
    /// Logique d'interaction pour CalendrierPage.xaml
    /// </summary>
    public partial class CalendrierPage : Page
    {

        CalendrierRepository data = new CalendrierRepository();

        public CalendrierPage()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            List<Calendrier> cal = data.GetAllCalendrier();

            datagrid.ItemsSource = cal;
        }

        public bool isValid()
        {
            DateTime date = date_picker.SelectedDate.Value;
            string[] dateTab = Convert.ToString(date).Split(" ");
            string dateString = dateTab[0];
            string[] dateStringTab = dateString.Split("/");
            string dateCompare = dateStringTab[0] + "/" + dateStringTab[1];

            if (date_picker.Text == String.Empty)
            {
                MessageBox.Show("La date est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if ((int)date.DayOfWeek == 0)
            {
                MessageBox.Show("Ce jour est un dimanche", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dateCompare == "01/01" || dateCompare == "01/05" || dateCompare == "08/05" || dateCompare == "14/07" || dateCompare == "15/08" || dateCompare == "01/11" || dateCompare == "11/05" || dateCompare == "25/12")
            {
                MessageBox.Show("Ce jour est férié", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void DataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            insert_btn.IsEnabled = false;
            delete_btn.IsEnabled = true;

            var row = sender as DataGridRow;
            Calendrier cal = row.DataContext as Calendrier;
            date_picker.SelectedDate = cal.DateHeure;
            string dateTime = Convert.ToString(cal.DateHeure);
            string[] tab = dateTime.Split(" ");
            string[] tab2 = tab[1].Split(":");
            string test = tab2[0] + ":" + tab2[1];
            time_combobox.SelectedValue = tab2[0] + ":" + tab2[1];
        }

        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Calendrier cal = new Calendrier();

                string date = Convert.ToString(date_picker.SelectedDate.Value);
                ComboBoxItem item = time_combobox.SelectedItem as ComboBoxItem;
                string time = item.Content.ToString();

                string[] newDateTime = date.Split(" ");
                newDateTime[1] = time;
                string rebuiltDate = newDateTime[0]+" "+ newDateTime[1];

                cal.DateHeure = Convert.ToDateTime(rebuiltDate);

                if (data.CheckExists(Convert.ToString(cal.DateHeure)).Count() == 0)
                {
                    data.CreateCalendrier(cal);
                }
                else
                {
                    MessageBox.Show("La date existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                LoadGrid();
                date_picker.SelectedDate = null;
                time_combobox.SelectedItem = null;

            }
        }


        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Voulez-vous vraiment supprimer cette entrée?";
            string cpt = "Suppression";
            var res = MessageBox.Show(msg, cpt, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                Calendrier cal = new Calendrier();
                cal.DateHeure = date_picker.SelectedDate.Value;

                data.DeleteCalendrier(cal);
            }

            LoadGrid();
            date_picker.SelectedDate = null;

            insert_btn.IsEnabled = true;
            delete_btn.IsEnabled = false;
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            date_picker.SelectedDate = null;
            search_txt.Clear();

            insert_btn.IsEnabled = true;
            delete_btn.IsEnabled = false;
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            string search = search_txt.Text;
            List<Calendrier> cal = data.FilterCalendrier(search);
            datagrid.ItemsSource = cal;
        }

        private void search_cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            search_txt.Clear();
            LoadGrid();
        }
    }
}
