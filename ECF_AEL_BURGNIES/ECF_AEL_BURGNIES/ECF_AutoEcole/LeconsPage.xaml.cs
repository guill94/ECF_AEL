using ECF_AutoEcole.Data;
using ECF_AutoEcole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour LeconsPage.xaml
    /// </summary>
    public partial class LeconsPage : Page
    {

        LeconsRepository data = new LeconsRepository();
        ElevesRepository dataEleve = new ElevesRepository();
        MoniteursRepository dataMono = new MoniteursRepository();
        CalendrierRepository dataCal = new CalendrierRepository();
        ModelesRepository dataModele = new ModelesRepository();
        VehiculesRepository dataVehicule = new VehiculesRepository();

        public Modele modeleSelected { get; set; }
        public Calendrier dateSelected { get; set; }
        public Eleve eleveSelected { get; set; }
        public Moniteur moniteurSelected { get; set; }

        public LeconsPage()
        {
            InitializeComponent();
            LoadGrid();
            FillModeleComboBox();
            FillDateComboBox();
            FillEleveComboBox();
            FillMoniteurComboBox();
        }
        /// <summary>
        /// Charge la grille avec toutes les données
        /// </summary>
        private void LoadGrid()
        {
            List<Lecon> lecons = data.GetAllLecons();
            datagrid.ItemsSource = lecons;
        }

        /// <summary>
        /// Remplir le combo box avec les modèles
        /// </summary>
        public void FillModeleComboBox()
        {
            List<Modele> modeles = dataModele.GetAllModeles();
            combo_box_modele.ItemsSource = modeles;
        }

        /// <summary>
        /// Remplir le combo box avec les Calendriers
        /// </summary>
        public void FillDateComboBox()
        {
            List<Calendrier> dates = dataCal.GetAllCalendrier();
            combo_box_date.ItemsSource = dates;
            combo_box_date.SelectedItem = null;
        }

        /// <summary>
        /// Remplir le combo box avec les élèves
        /// </summary>
        public void FillEleveComboBox()
        {
            List<Eleve> eleves = dataEleve.GetAllElevesWithoutConduite();
            combo_box_eleve.ItemsSource = eleves;
            combo_box_eleve.SelectedItem = null;
        }

        /// <summary>
        /// Remplir le combo box avec les Moniteurs
        /// </summary>
        public void FillMoniteurComboBox()
        {
            List<Moniteur> monos = dataMono.GetAllAvailableMoniteurs();
            combo_box_moniteur.ItemsSource = monos;
            combo_box_moniteur.SelectedItem = null;
        }

        /// <summary>
        /// Verifications
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            
            if (combo_box_modele.Text == String.Empty)
            {
                MessageBox.Show("Le modèle est requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (combo_box_date.Text == String.Empty)
            {
                MessageBox.Show("La date est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (combo_box_eleve.Text == String.Empty)
            {
                MessageBox.Show("L'élève est requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (combo_box_moniteur.Text == String.Empty)
            {
                MessageBox.Show("Le moniteur est requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (duree_txt.Text == String.Empty)
            {
                MessageBox.Show("La durée est requise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (duree_txt.Text != String.Empty)
            {
                string duree = duree_txt.Text;

                for (int i = 0; i < duree.Length; i++)
                {
                    if (!(Char.IsDigit(duree[i])))
                    {
                        MessageBox.Show("La durée doit contenir que des chiffres", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
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
            Lecon lecon = row.DataContext as Lecon;

            combo_box_modele.SelectedValue = lecon.ModeleVehicule;
            combo_box_date.SelectedValue = lecon.DateHeure;
            combo_box_eleve.SelectedValue = lecon.IdEleveNavigation.NomEleve;
            combo_box_moniteur.SelectedValue = lecon.IdMoniteurNavigation.NomMoniteur;
            duree_txt.Text = Convert.ToString(lecon.Duree);

            combo_box_modele.IsEnabled = false;
            combo_box_date.IsEnabled = false;
            combo_box_eleve.IsEnabled = false;
            combo_box_moniteur.IsEnabled = false;

        }

        /// <summary>
        /// Suit le changement de séléction du combo box pour le modèle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxModele_Change(object sender, SelectionChangedEventArgs e)
        {
            this.modeleSelected = combo_box_modele.SelectedItem as Modele;
        }
        /// <summary>
        /// Suit le changement de séléction du combo box pour le calendrier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDate_Change(object sender, SelectionChangedEventArgs e)
        {
            this.dateSelected = combo_box_date.SelectedItem as Calendrier;
        }
        /// <summary>
        /// Suit le changement de séléction du combo box pour l'élève
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEleve_Change(object sender, SelectionChangedEventArgs e)
        {
            this.eleveSelected = combo_box_eleve.SelectedItem as Eleve;
        }
        /// <summary>
        /// Suit le changement de séléction du combo box pour le moniteur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMoniteur_Change(object sender, SelectionChangedEventArgs e)
        {
            this.moniteurSelected = combo_box_moniteur.SelectedItem as Moniteur;
        }



        /// <summary>
        /// Insertion d'une nouvelle leçon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            int idMoniteur = moniteurSelected.IdMoniteur;
            int idEleve = eleveSelected.IdEleve;
            string dateSel = Convert.ToString(dateSelected.DateHeure);
            string[] tab = dateSel.Split(" ");
            string date = tab[0];

            if (isValid())
            {
                Lecon lecon = new Lecon();

                lecon.ModeleVehicule = modeleSelected.ModeleVehicule;
                lecon.DateHeure = dateSelected.DateHeure;
                lecon.IdEleve = eleveSelected.IdEleve;
                lecon.IdMoniteur = moniteurSelected.IdMoniteur;
                lecon.Duree = Convert.ToInt32(duree_txt.Text);

                if (data.CheckExists(lecon).Count == 0)
                {
                    if (CheckVehiculeAvailable(data.GetLeconsByModele(modeleSelected.ModeleVehicule)))
                    {
                        if (CheckMoniteurAvailable(data.GetLeconsByMoniteurAndDate(idMoniteur, date)))
                        {
                            if (CheckEleveAvailable(data.GetLeconsByEleveAndDate(idEleve, date)))
                            {
                                
                                data.CreateLecon(lecon);
                                
                                LoadGrid();

                                combo_box_modele.SelectedItem = null;
                                combo_box_date.SelectedItem = null;
                                combo_box_eleve.SelectedItem = null;
                                combo_box_moniteur.SelectedItem = null;
                                duree_txt.Clear();

                                insert_btn.IsEnabled = false;

                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("La leçon existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        /// <summary>
        /// Mise à jour d'une leçon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {

            int idMoniteur = moniteurSelected.IdMoniteur;
            int idEleve = eleveSelected.IdEleve;
            string dateSel = Convert.ToString(dateSelected.DateHeure);
            string[] tab = dateSel.Split(" ");
            string date = tab[0];

            if (isValid())
            {
                if (CheckVehiculeAvailable(data.GetLeconsByModele(modeleSelected.ModeleVehicule)))
                {
                    if (CheckMoniteurAvailable(data.GetLeconsByMoniteurAndDate(idMoniteur, date)))
                    {
                        if (CheckEleveAvailable(data.GetLeconsByEleveAndDate(idEleve, date)))
                        {

                            Lecon lecon = new Lecon();

                            lecon.ModeleVehicule = modeleSelected.ModeleVehicule;
                            lecon.DateHeure = dateSelected.DateHeure;
                            lecon.IdEleve = eleveSelected.IdEleve;
                            lecon.IdMoniteur = moniteurSelected.IdMoniteur;
                            lecon.Duree = Convert.ToInt32(duree_txt.Text);

                            data.UpdateLecon(lecon);

                            LoadGrid();

                            combo_box_modele.SelectedItem = null;
                            combo_box_date.SelectedItem = null;
                            combo_box_eleve.SelectedItem = null;
                            combo_box_moniteur.SelectedItem = null;
                            duree_txt.Clear();

                            edit_btn.IsEnabled = delete_btn.IsEnabled = false;

                            combo_box_modele.IsEnabled = true;
                            combo_box_date.IsEnabled = true;
                            combo_box_eleve.IsEnabled = true;
                            combo_box_moniteur.IsEnabled = true;


                        }

                    }
                }
            }
            
        }

        /// <summary>
        /// Suppression d'une leçon
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
                Lecon lecon = new Lecon();

                lecon.ModeleVehicule = modeleSelected.ModeleVehicule;
                lecon.DateHeure = dateSelected.DateHeure;
                lecon.IdEleve = eleveSelected.IdEleve;
                lecon.IdMoniteur = moniteurSelected.IdMoniteur;
                lecon.Duree = Convert.ToInt32(duree_txt.Text);

                data.DeleteLecon(lecon);
            }


            LoadGrid();

            combo_box_modele.SelectedItem = null;
            combo_box_date.SelectedItem = null;
            combo_box_eleve.SelectedItem = null;
            combo_box_moniteur.SelectedItem = null;
            duree_txt.Clear();

            edit_btn.IsEnabled = delete_btn.IsEnabled = false;

            combo_box_modele.IsEnabled = true;
            combo_box_date.IsEnabled = true;
            combo_box_eleve.IsEnabled = true;
            combo_box_moniteur.IsEnabled = true;
        }

        /// <summary>
        /// Réinitialise la page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            combo_box_modele.SelectedItem = null;
            combo_box_date.SelectedItem = null;
            combo_box_eleve.SelectedItem = null;
            combo_box_moniteur.SelectedItem = null;
            duree_txt.Clear();
            search_txt.Clear();

            edit_btn.IsEnabled = delete_btn.IsEnabled = false;
            insert_btn.IsEnabled = false;

            combo_box_modele.IsEnabled = true;
            combo_box_date.IsEnabled = true;
            combo_box_eleve.IsEnabled = true;
            combo_box_moniteur.IsEnabled = true;
        }

        /// <summary>
        /// Effectuer une recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            string search = search_txt.Text;
            List<Lecon> lecons = data.FilterLecon(search);
            datagrid.ItemsSource = lecons;
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


        //METHODES VERIFICATIONS DISPOS

        /// <summary>
        /// Verifie si le moniteur est dispo à un crénaux
        /// </summary>
        /// <param name="lecons">liste de leçons</param>
        /// <returns>vrai ou faux</returns>
        private bool CheckMoniteurAvailable(List<Lecon> lecons)
        {
            int dureeNewLecon = Convert.ToInt32(duree_txt.Text);

            foreach (var lecon in lecons)
            {
                var minDate = lecon.DateHeure.AddMinutes(-dureeNewLecon);
                var maxDate = lecon.DateHeure.AddMinutes(lecon.Duree);
                var chosenDate = dateSelected.DateHeure;

                if (!(modeleSelected.ModeleVehicule == lecon.ModeleVehicule && eleveSelected.IdEleve == lecon.IdEleve && dateSelected.DateHeure == lecon.DateHeure))
                {
                    if (minDate < chosenDate && chosenDate < maxDate)
                    {
                        MessageBox.Show("Le moniteur n'est pas disponible à cette date et heure", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Verifie si l'élève est dispo à un crénaux
        /// </summary>
        /// <param name="lecons">liste de leçons</param>
        /// <returns>vrai ou faux</returns>
        private bool CheckEleveAvailable(List<Lecon> lecons)
        {
            int dureeNewLecon = Convert.ToInt32(duree_txt.Text);

            foreach (var lecon in lecons)
            {
                var minDate = lecon.DateHeure.AddMinutes(-dureeNewLecon);
                var maxDate = lecon.DateHeure.AddMinutes(lecon.Duree);
                var chosenDate = dateSelected.DateHeure;

                if (!(modeleSelected.ModeleVehicule == lecon.ModeleVehicule && moniteurSelected.IdMoniteur == lecon.IdMoniteur && dateSelected.DateHeure == lecon.DateHeure))
                {
                    if (minDate < chosenDate && chosenDate < maxDate)
                    {
                        MessageBox.Show("L'élève n'est pas disponible à cette date et heure", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Verifie si un véhicule est dispo à un crénaux
        /// </summary>
        /// <param name="lecons">liste de leçons</param>
        /// <returns>vrai ou faux</returns>
        private bool CheckVehiculeAvailable(List<Lecon> lecons)
        {
            int dureeNewLecon = Convert.ToInt32(duree_txt.Text);
            int count = 0;
            List<Vehicule> vehiculeByModele = dataVehicule.GetVehiculesByModele(modeleSelected.ModeleVehicule);
            int nbrVehiculeTot = vehiculeByModele.Count;

            foreach (var lecon in lecons)
            {
                var minDate = lecon.DateHeure.AddMinutes(-dureeNewLecon); //date et heure début lecon requete - duree nouvelle lecon
                var maxDate = lecon.DateHeure.AddMinutes(lecon.Duree); //date et heure fin
                var chosenDate = dateSelected.DateHeure;

                if (!(moniteurSelected.IdMoniteur == lecon.IdMoniteur && eleveSelected.IdEleve == lecon.IdEleve && dateSelected.DateHeure == lecon.DateHeure))
                {
                    if (minDate < chosenDate && chosenDate < maxDate)
                    {
                        count++;

                    }
                }
            }
            if (count >= nbrVehiculeTot)
            {
                MessageBox.Show("Aucun véhicule de ce modèle n'est disponible à cette date et heure", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
