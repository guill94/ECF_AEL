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
    /// Logique d'interaction pour MoniteursPage.xaml
    /// </summary>
    public partial class MoniteursPage : Page
    {
        MoniteursRepository data = new MoniteursRepository();

        public MoniteursPage()
        {
            InitializeComponent();
            LoadGrid();
        }

        /// <summary>
        /// Charge la grille avec toutes les données 
        /// </summary>
        public void LoadGrid()
        {
            List<Moniteur> monos = data.GetAllMoniteurs();

            datagrid.ItemsSource = monos;
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
            if (date_picker_birth.Text == String.Empty)
            {
                MessageBox.Show("La date de naissance est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (date_picker_hire.Text == String.Empty)
            {
                MessageBox.Show("La date d'embauche est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
            Moniteur mono = row.DataContext as Moniteur;
            id_txt.Text = Convert.ToString(mono.IdMoniteur);
            nom_txt.Text = mono.NomMoniteur;
            prenom_txt.Text = mono.PrenomMoniteur;
            date_picker_birth.SelectedDate = mono.DateNaissance;
            date_picker_hire.SelectedDate = mono.DateEmbauche;
            activite_chk.IsChecked = mono.Activite;
        }


        /// <summary>
        /// Insertion nouveau moniteur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Moniteur mono = new Moniteur();
                mono.NomMoniteur = nom_txt.Text;
                mono.PrenomMoniteur = prenom_txt.Text;
                mono.DateNaissance = date_picker_birth.SelectedDate.Value;
                mono.DateEmbauche = date_picker_hire.SelectedDate.Value;
                mono.Activite = activite_chk.IsChecked;

                mono.IdMoniteur = data.GetLastId();
                data.CreateMoniteur(mono);

                LoadGrid();

                id_txt.Clear();
                nom_txt.Clear();
                prenom_txt.Clear();
                date_picker_birth.SelectedDate = null;
                date_picker_hire.SelectedDate = null;
                activite_chk.IsChecked = false;

            }
        }

        /// <summary>
        /// Mise à jour moniteur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Moniteur mono = new Moniteur();
                mono.IdMoniteur = Convert.ToInt32(id_txt.Text);
                mono.NomMoniteur = nom_txt.Text;
                mono.PrenomMoniteur = prenom_txt.Text;
                mono.DateNaissance = date_picker_birth.SelectedDate.Value;
                mono.DateEmbauche = date_picker_hire.SelectedDate.Value;
                mono.Activite = activite_chk.IsChecked;

                data.UpdateMoniteur(mono);

                LoadGrid();

                id_txt.Clear();
                nom_txt.Clear();
                prenom_txt.Clear();
                date_picker_birth.SelectedDate = null;
                date_picker_hire.SelectedDate = null;
                activite_chk.IsChecked = false;

                insert_btn.IsEnabled = true;
                edit_btn.IsEnabled = delete_btn.IsEnabled = false;

            }

        }

        /// <summary>
        /// Suppression moniteur
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
                data.DeleteMoniteur(id);
            }

            LoadGrid();

            id_txt.Clear();
            nom_txt.Clear();
            prenom_txt.Clear();
            date_picker_birth.SelectedDate = null;
            date_picker_hire.SelectedDate = null;
            activite_chk.IsChecked = false;


            insert_btn.IsEnabled = true;
            edit_btn.IsEnabled = delete_btn.IsEnabled = false;
        }

        /// <summary>
        /// Permet de réinitialiser la page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            id_txt.Clear();
            nom_txt.Clear();
            prenom_txt.Clear();
            date_picker_birth.SelectedDate = null;
            date_picker_hire.SelectedDate = null;
            activite_chk.IsChecked = false;
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
            List<Moniteur> monos = data.FilterMoniteur(search);
            datagrid.ItemsSource = monos;
        }

        /// <summary>
        /// Annuler la recherche
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
