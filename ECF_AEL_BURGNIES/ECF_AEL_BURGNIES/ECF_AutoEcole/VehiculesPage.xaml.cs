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
    /// Logique d'interaction pour VehiculesPage.xaml
    /// </summary>
    public partial class VehiculesPage : Page
    {
        VehiculesRepository data = new VehiculesRepository();
        ModelesRepository dataModele = new ModelesRepository();

        public Modele modeleSelected { get; set; }

        public VehiculesPage()
        {
            InitializeComponent();
            LoadGrid();
            FillComboBox();
        }

        /// <summary>
        /// Charge la grille avec toutes les données
        /// </summary>
        public void LoadGrid()
        {
            List<Vehicule> vehicules = data.GetAllVehicules();

            datagrid.ItemsSource = vehicules;
        }

        /// <summary>
        /// Rempli le combo box avec tous les modèles de véhicule
        /// </summary>
        public void FillComboBox()
        {
            List<Modele> modeles = dataModele.GetAllModeles();
            //modeleCombo = modeles;
            combo_box.ItemsSource = modeles;
            //DataContext = modeles;
        }

        /// <summary>
        /// Suit le changement de la selection du combo box des modèles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Change(object sender, SelectionChangedEventArgs e)
        {
            this.modeleSelected = combo_box.SelectedItem as Modele;
        }

        /// <summary>
        /// Verifications
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            if (immat_txt.Text == String.Empty)
            {
                MessageBox.Show("L'immatriculation est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (combo_box.Text == String.Empty)
            {
                MessageBox.Show("Le modèle est requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
            Vehicule vehicule = row.DataContext as Vehicule;
            immat_txt.Text = vehicule.NImmatriculation;
            combo_box.SelectedValue = vehicule.ModeleVehicule;
            etat_chk.IsChecked = vehicule.Etat;
        
        }

        /// <summary>
        /// Insertion d'un nouveau véhicule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Vehicule vehicule = new Vehicule();
                vehicule.NImmatriculation = immat_txt.Text;
                vehicule.ModeleVehicule = modeleSelected.ModeleVehicule;
                vehicule.Etat = etat_chk.IsChecked;

                if (data.CheckExists(vehicule.NImmatriculation).Count() == 0)
                {
                    data.CreateVehicule(vehicule);
                }
                else
                {
                    MessageBox.Show("Le véhicule existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                LoadGrid();

                immat_txt.Clear();
                combo_box.SelectedItem = null;
                etat_chk.IsChecked = false;

            }
        }

        /// <summary>
        /// Mise à jour d'un véhicule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                Vehicule vehicule = new Vehicule();
                vehicule.NImmatriculation = immat_txt.Text;
                vehicule.ModeleVehicule = modeleSelected.ModeleVehicule;
                vehicule.Etat = etat_chk.IsChecked;

                data.UpdateVehicule(vehicule);

                LoadGrid();

                immat_txt.Clear();
                combo_box.SelectedItem = null;
                etat_chk.IsChecked = false;

                insert_btn.IsEnabled = true;
                edit_btn.IsEnabled = delete_btn.IsEnabled = false;

            }
        }
        /// <summary>
        /// Suppression d'un véhicule
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
                string immat = immat_txt.Text;
                data.DeleteVehicule(immat);
            }

            LoadGrid();

            immat_txt.Clear();
            combo_box.SelectedItem = null;
            etat_chk.IsChecked = false;


            insert_btn.IsEnabled = true;
            edit_btn.IsEnabled = delete_btn.IsEnabled = false;
        }

        /// <summary>
        /// Permet de réinitialiser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            immat_txt.Clear();
            combo_box.SelectedItem = null;
            etat_chk.IsChecked = false;
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
            List<Vehicule> vehicules = data.FilterVehicule(search);
            datagrid.ItemsSource = vehicules;
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
