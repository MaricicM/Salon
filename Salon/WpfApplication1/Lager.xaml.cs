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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Lager.xaml
    /// </summary>
    public partial class Lager : Window
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        private Proizvodi pro = null;
        private List<Proizvodi> listaProizvoda = new List<Proizvodi>();
        public Lager()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaProizvoda = zDAL.VratiSveProizvode();
            dataGridProizvodi.ItemsSource = listaProizvoda;
        }

        private void textBoxPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPretraga.Text))
            {
                dataGridProizvodi.ItemsSource = listaProizvoda;
                return;
            }
            dataGridProizvodi.SelectedIndex = -1;
            int sifra = 0;
            if (int.TryParse(textBoxPretraga.Text.Trim(), out sifra))
            {
                dataGridProizvodi.ItemsSource = listaProizvoda.Where(k => k.SifraProizvoda.ToString().StartsWith(sifra.ToString()));
                return;
            }
            string ulaz = textBoxPretraga.Text.ToLower().Trim();
            string st1 = "";
            string st2 = "";
            if (ulaz.Contains(" "))
            {
                string[] st = ulaz.Split(' ');
                st1 = st[0];
                for (int i = 1; i < st.Length; i++)
                {
                    if (st[i] != "")
                    {
                        st2 = st[i];
                        break;
                    }
                }
            }
            else
            {
                st2 = "";
            }
            if (!string.IsNullOrWhiteSpace(st2))
            {
                dataGridProizvodi.ItemsSource = listaProizvoda.Where(k => (k.Naziv.ToLower().Contains(st1) || (k.Proizvodjac != null && k.Proizvodjac.ToLower().Contains(st1))) && (k.Naziv.ToLower().Contains(st2) || (k.Proizvodjac != null && k.Proizvodjac.ToLower().Contains(st2))));
            }
            else
            {
                dataGridProizvodi.ItemsSource = listaProizvoda.Where(k => k.Naziv.ToLower().Contains(ulaz) || (k.Proizvodjac != null && k.Proizvodjac.ToLower().Contains(ulaz)));
            }
        }
        private void buttonResetujPretragu_Click(object sender, RoutedEventArgs e)
        {
            textBoxPretraga.Clear();
        }
        private void buttonNoviProizvod_Click(object sender, RoutedEventArgs e)
        {
            NoviProizvod np = new WpfApplication1.NoviProizvod();
            np.Left = this.Left + 300;
            np.Top = this.Top;
            np.ShowDialog();
            listaProizvoda = zDAL.VratiSveProizvode();
            dataGridProizvodi.ItemsSource = listaProizvoda;
        }

        private void dataGridProizvodi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridProizvodi.SelectedIndex == -1)
            {
                labelProizvod.Content = "Proizvod";
                labelStanje.Content = "Trenutno stanje:";
                buttonIzbaci.IsEnabled = false;
                buttonIzmeni.IsEnabled = false;
            }
            else
            {
                pro = (Proizvodi)dataGridProizvodi.SelectedItem;
                labelProizvod.Content = pro.Naziv + " - " + pro.Proizvodjac;
                labelStanje.Content = $"Trenutno stanje: {pro.Stanje}";
                buttonIzmeni.IsEnabled = true;
                buttonIzbaci.IsEnabled = (pro.Stanje < 1) ? true : false;
            }            
        }

        private void textBoxUnos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                buttonUnesi.Focus();
                return;
            }
            textBoxUnos.Text = textBoxUnos.Text.Trim();
            textBoxUnos.CaretIndex = textBoxUnos.Text.Length;
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }            
        }
        private void buttonUnesi_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUnos.Text) || int.Parse(textBoxUnos.Text) == 0)
            {
                MessageBox.Show("Niste uneli kolicinu");
                textBoxUnos.Focus();
                return;
            }
            if (!zDAL.UnesiKolicinu(pro, int.Parse(textBoxUnos.Text)))
            {
                MessageBox.Show("Greska pri cuvanju");
            }
            else
            {
                textBoxUnos.Clear();
                labelStanje.Content = $"Trenutno stanje: {pro.Stanje}";
                listaProizvoda = zDAL.VratiSveProizvode();
                dataGridProizvodi.ItemsSource = listaProizvoda;
            }
        }

        private void buttonPovratak_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonIzbaci_Click(object sender, RoutedEventArgs e)
        {
            Proizvodi pro = (Proizvodi)dataGridProizvodi.SelectedItem;
            PopUp pu = new PopUp();
            pu.label1.Content = "Proizvod ce biti izbacen iz ponude:";
            pu.label2.Content = pro.Naziv;
            pu.label3.Content = "Da li ste sigurni?";
            pu.Left = this.Left + 300;
            pu.Top = this.Top + 300;
            if (pu.ShowDialog() == true)
            {
                if (zDAL.IzbaciProizvod(pro.ProizvodID))
                {
                    MessageBox.Show("Proizvod izbacen iz ponude");
                    listaProizvoda = zDAL.VratiSveProizvode();
                    dataGridProizvodi.ItemsSource = listaProizvoda;
                }
                else
                {
                    MessageBox.Show("Greska pri povezivanju sa bazom");
                }
            }
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            Proizvodi pro = (Proizvodi)dataGridProizvodi.SelectedItem;
            NoviProizvod np = new NoviProizvod();
            np.Title = "Izmena Podataka";
            np.buttonUbaci.Content = "Izmeni podatke";
            np.labelStanje.Content = "Trenutno stanje";
            np.pro = pro;
            np.a = 1;
            np.ShowDialog();
            ZakazivanjeDAL zd = new ZakazivanjeDAL();
            listaProizvoda = zd.VratiSveProizvode();
            dataGridProizvodi.ItemsSource = listaProizvoda;
        }
    }
}
