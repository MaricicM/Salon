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
    /// Interaction logic for NoviProizvod.xaml
    /// </summary>
    public partial class NoviProizvod : Window
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        public Proizvodi pro = new Proizvodi();
        public int a = 0;
        public NoviProizvod()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (a == 1)
            {
                Osvezi();
            }
            textBoxNaziv.Focus();
        }

        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(textBoxNaziv.Text))
            {
                MessageBox.Show("Niste uneli naziv proizvoda");
                textBoxNaziv.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxCena.Text))
            {
                MessageBox.Show("Niste uneli prodajnu cenu proizvoda");
                textBoxCena.Focus();
                return false;
            }
            if (a == 0 && zDAL.VratiSveProizvode().Any(p => p.Naziv == textBoxNaziv.Text && p.Proizvodjac == textBoxProizvodjac.Text && p.SifraProizvoda == int.Parse(textBoxSifraProizvoda.Text)))
            {
                MessageBox.Show("Proizvod postoji u ponudi!");
                textBoxNaziv.Focus();
                return false;
            }
            return true;
        }
        private void Resetuj ()
        {
            textBoxCena.Clear();
            textBoxSifraProizvoda.Clear();
            textBoxKategorija.Clear();
            textBoxNaziv.Clear();
            textBoxProizvodjac.Clear();
            textBoxStanje.Clear();
        }
        private void Osvezi()
        {
            textBoxCena.Text = pro.Cena.ToString();
            textBoxKategorija.Text = pro.Kategorija;
            textBoxNaziv.Text = pro.Naziv;
            textBoxProizvodjac.Text = pro.Proizvodjac;
            textBoxSifraProizvoda.Text = pro.SifraProizvoda.ToString();
            textBoxStanje.Text = pro.Stanje.ToString();
        }

        private void buttonResetuj_Click(object sender, RoutedEventArgs e)
        {
            if (a == 0)
            {
                Resetuj();
            }
            else
            {
                Osvezi();
            }            
        }
        private void buttonPovratak_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void buttonUbaci_Click(object sender, RoutedEventArgs e)
        {
            if (!Validacija())
            {
                return;
            }
            int st = (string.IsNullOrWhiteSpace(textBoxStanje.Text) ? 0 : int.Parse(textBoxStanje.Text));
            int sifra = (string.IsNullOrWhiteSpace(textBoxSifraProizvoda.Text.Trim())) ? 0 : int.Parse(textBoxSifraProizvoda.Text.Trim());
            pro.Naziv = textBoxNaziv.Text; pro.Proizvodjac = textBoxProizvodjac.Text; pro.Kategorija = textBoxKategorija.Text; pro.SifraProizvoda = sifra;
            pro.Cena = decimal.Parse(textBoxCena.Text); pro.Stanje = st;
            if (a == 0)
            {
                if (zDAL.UbaciProizvod(pro))
                {
                    MessageBox.Show("Proizvod ubacen");
                    Resetuj();
                    textBoxNaziv.Focus();
                }
                else
                {
                    MessageBox.Show("Greska pri cuvanju");
                }
            }
            else
            {
                if (zDAL.IzmeniProizvod(pro))
                {
                    MessageBox.Show("Podaci izmenjeni");
                    Close();
                }
            }
            
        }

        private void textBoxSifraProizvoda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                textBoxCena.Focus();
                return;
            }
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        private void textBoxCena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                textBoxStanje.Focus();
                return;
            }
            textBoxCena.Text = textBoxCena.Text.Trim();
            textBoxCena.CaretIndex = textBoxCena.Text.Length;
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && !textBoxCena.Text.Contains('.'))
                {
                    return;
                }
                e.Handled = true;
            }
        }
        private void textBoxStanje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                buttonUbaci.Focus();
                return;
            }
            textBoxStanje.Text = textBoxStanje.Text.Trim();
            textBoxStanje.CaretIndex = textBoxStanje.Text.Length;
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }

    }
}
