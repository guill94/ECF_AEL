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
    /// Logique d'interaction pour ModelesPage.xaml
    /// </summary>
    public partial class ModelesPage : Page
    {

        ModelesRepository data = new ModelesRepository();

        public ModelesPage()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void LoadGrid()
        {
            List<Modele> modeles = data.GetAllModeles();

            datagrid.ItemsSource = modeles;
        }

        public bool isValid()
        {
            if (modele_txt.Text == String.Empty)
            {
                MessageBox.Show("Le modèle est requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (marque_txt.Text == String.Empty)
            {
                MessageBox.Show("La marque est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (annee_txt.Text == String.Empty)
            {
                MessageBox.Show("L'année est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (date_picker.Text == String.Empty)
            {
                MessageBox.Show("La date d'achat est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (annee_txt.Text != String.Empty)
            {
                string annee = annee_txt.Text;
                if (annee.Length != 4)
                {
                    MessageBox.Show("L'année doit contenir 4 chiffres", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    for (int i = 0; i < annee.Length; i++)
                    {
                        if (!(Char.IsDigit(annee[i])))
                        {
                            MessageBox.Show("L'année ne doit contenir que des chiffres", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void DataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            insert_btn.IsEnabled = false;
            edit_btn.IsEnabled = delete_btn.IsEnabled = true;

            var row = sender as DataGridRow;
            Modele modele = row.DataContext as Modele;
            modele_txt.Text = Convert.ToString(modele.ModeleVehicule);
            marque_txt.Text = modele.Marque;
            annee_txt.Text = modele.Annee;
            date_picker.SelectedDate = modele.DateAchat;

            modele_txt.IsEnabled = false;
        }

        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Modele modele = new Modele();
                modele.ModeleVehicule = modele_txt.Text;
                modele.Marque = marque_txt.Text;
                modele.Annee = annee_txt.Text;
                modele.DateAchat = date_picker.SelectedDate.Value;

           
                if (data.CheckExists(modele.ModeleVehicule).Count == 0)
                {
                    data.CreateModele(modele);
                }
                else
                {
                    MessageBox.Show("Ce modèle existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                LoadGrid();

                modele_txt.Clear();
                marque_txt.Clear();
                annee_txt.Clear();
                date_picker.SelectedDate = null;


            }
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            modele_txt.IsEnabled = true;
            if (isValid())
            {
                Modele modele = new Modele();
                modele.ModeleVehicule = modele_txt.Text;
                modele.Marque = marque_txt.Text;
                modele.Annee = annee_txt.Text;
                modele.DateAchat = date_picker.SelectedDate.Value;

                data.UpdateModele(modele);

                LoadGrid();

                modele_txt.Clear();
                marque_txt.Clear();
                annee_txt.Clear();
                date_picker.SelectedDate = null;

                insert_btn.IsEnabled = true;
                edit_btn.IsEnabled = delete_btn.IsEnabled = false;

            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            modele_txt.IsEnabled = true;
            string msg = "Voulez-vous vraiment supprimer cette entrée?";
            string cpt = "Suppression";
            var res = MessageBox.Show(msg, cpt, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                string modele = modele_txt.Text;
                data.DeleteModele(modele);
            }

            LoadGrid();

            modele_txt.Clear();
            marque_txt.Clear();
            annee_txt.Clear();
            date_picker.SelectedDate = null;

            insert_btn.IsEnabled = true;
            edit_btn.IsEnabled = delete_btn.IsEnabled = false;
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            modele_txt.IsEnabled = true;

            modele_txt.Clear();
            marque_txt.Clear();
            annee_txt.Clear();
            date_picker.SelectedDate = null;
            search_txt.Clear();

            insert_btn.IsEnabled = true;
            edit_btn.IsEnabled = delete_btn.IsEnabled = false;
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            string search = search_txt.Text;
            List<Modele> modeles = data.FilterModele(search);
            datagrid.ItemsSource = modeles;
        }

        private void search_cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            search_txt.Clear();
            LoadGrid();
        }
    }
}
