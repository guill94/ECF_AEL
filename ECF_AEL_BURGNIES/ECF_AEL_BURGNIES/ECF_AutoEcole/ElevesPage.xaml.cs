using ECF_AutoEcole.Data;
using ECF_AutoEcole.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour ElevesPage.xaml
    /// </summary>
    public partial class ElevesPage : Page
    {
        ElevesRepository data = new ElevesRepository();
        List<Eleve> eleves = new List<Eleve>();
        public ElevesPage()
        {
            InitializeComponent();
            LoadGrid();
        }

        /// <summary>
        /// Charge la grille avec toutes les données
        /// </summary>
        public void LoadGrid()
        {
            eleves = data.GetAllEleves();
           
            datagrid.ItemsSource = eleves;
        }

        /// <summary>
        /// Verifications
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            if (nom_txt.Text == String.Empty)
            {
                MessageBox.Show("Le nom est requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (prenom_txt.Text == String.Empty)
            {
                MessageBox.Show("Le prénom est requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (date_picker.Text == String.Empty)
            {
                MessageBox.Show("La date de naissance est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Récupère l'objet correspondant à la ligne cliquée dans la datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            insert_btn.IsEnabled = false;
            edit_btn.IsEnabled = delete_btn.IsEnabled = true;

            var row = sender as DataGridRow;
            Eleve eleve = row.DataContext as Eleve;
            id_txt.Text = Convert.ToString(eleve.IdEleve);
            nom_txt.Text = eleve.NomEleve;
            prenom_txt.Text = eleve.PrenomEleve;
            code_chk.IsChecked = eleve.Code;
            conduite_chk.IsChecked = eleve.Conduite;
            date_picker.SelectedDate = eleve.DateNaissance;
        }

        /// <summary>
        /// Insertion nouvel élève
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Eleve eleve = new Eleve();
                eleve.NomEleve = nom_txt.Text;
                eleve.PrenomEleve = prenom_txt.Text;
                eleve.Code = code_chk.IsChecked;
                eleve.Conduite = conduite_chk.IsChecked;
                eleve.DateNaissance = date_picker.SelectedDate.Value;

                eleve.IdEleve = data.GetLastId();
                data.CreateEleve(eleve);

                LoadGrid();

                nom_txt.Text = "";
                prenom_txt.Text = "";
                code_chk.IsChecked = false;
                conduite_chk.IsChecked = false;
                date_picker.SelectedDate = null;

            }
        }


        
        /// <summary>
        /// Mise à jour éléve
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Eleve eleve = new Eleve();
                eleve.IdEleve = Convert.ToInt32(id_txt.Text);
                eleve.NomEleve = nom_txt.Text;
                eleve.PrenomEleve = prenom_txt.Text;
                eleve.Code = code_chk.IsChecked;
                eleve.Conduite = conduite_chk.IsChecked;
                eleve.DateNaissance = date_picker.SelectedDate.Value;

                data.UpdateEleve(eleve);

                LoadGrid();

                id_txt.Text = "";
                nom_txt.Text = "";
                prenom_txt.Text = "";
                code_chk.IsChecked = false;
                conduite_chk.IsChecked = false;
                date_picker.SelectedDate = null;

                insert_btn.IsEnabled = true;
                edit_btn.IsEnabled = delete_btn.IsEnabled = false;

            }

        }

        /// <summary>
        /// Suppression élève
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Voulez-vous vraiment supprimer cette entrée?";
            string cpt = "Suppression";
            var res = MessageBox.Show(msg, cpt, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                int id = Convert.ToInt32(id_txt.Text);
                data.DeleteEleve(id);
            }
            
            LoadGrid();

            id_txt.Text = "";
            nom_txt.Text = "";
            prenom_txt.Text = "";
            code_chk.IsChecked = false;
            conduite_chk.IsChecked = false;
            date_picker.SelectedDate = null;


            insert_btn.IsEnabled = true;
            edit_btn.IsEnabled = delete_btn.IsEnabled = false;

        }

        /// <summary>
        /// Réinitialise la page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            id_txt.Text = "";
            nom_txt.Text = "";
            prenom_txt.Text = "";
            code_chk.IsChecked = false;
            conduite_chk.IsChecked = false;
            date_picker.SelectedDate = null;
            search_txt.Clear();

            insert_btn.IsEnabled = true;
            edit_btn.IsEnabled = delete_btn.IsEnabled = false;
        }

        /// <summary>
        /// Effectuer une recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            string search = search_txt.Text;
            List<Eleve> modeles = data.FilterEleve(search);
            datagrid.ItemsSource = modeles;
        }

        /// <summary>
        /// Annule la recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            search_txt.Clear();
            LoadGrid();
        }
    }
}
