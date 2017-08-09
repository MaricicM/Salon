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
    public partial class NovaUsluga : Window
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        private List<Usluge> prikaznaLista = new List<Usluge>();
        public int a = 1;
        private int trajanje = 1;
        public NovaUsluga()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (a == 1) 
            {
                prikaznaLista = zDAL.VratiSveUsluge();
                dataGridUsluge.ItemsSource = prikaznaLista;
            }
            else
            {
                this.Width = 375;
                dataGridUsluge.Visibility = Visibility.Collapsed;
                buttonReset.Content = "Ocisti podatke";
                buttonPotvrdi.Content = "Potvrdi Unos";
                this.Title = "Nova Usluga";
                textBoxNaziv.Focus();
            }
        }

        private bool Verifikacija ()
        {
            if (string.IsNullOrWhiteSpace(textBoxNaziv.Text))
            {
                MessageBox.Show("Niste uneli naziv usluge");
                textBoxNaziv.Focus();
                return false;
            }
            if (textBoxNaziv.Text.Length > 100)
            {
                MessageBox.Show("Naziv ne sme imati preko 100 karaktera");
                textBoxNaziv.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxCena.Text) || textBoxCena.Text == ".")
            {
                MessageBox.Show("Niste uneli cenu usluge");
                textBoxCena.Clear();
                textBoxCena.Focus();
                return false;
            }
            if (zDAL.VratiSveUsluge().Any(u => u.Naziv == textBoxNaziv.Text && u.Tip == textBoxTip.Text))
            {
                MessageBox.Show("Postoji usluga istog naziva i tipa!");
                textBoxTip.Focus();
                return false;
            }
            return true;
        }
        private void Resetuj()
        {
            textBoxNaziv.Clear(); textBoxTip.Clear(); textBoxCena.Clear(); labelTrajanje.Content = "0h : 30min";
            trajanje = 1; textBoxOpis.Clear(); textBoxPrimedbe.Clear(); textBoxNaziv.Focus();
        }
        private void IspisiLabelu()
        {
            if (trajanje % 2 == 0)
            {
                int b = trajanje / 2;
                labelTrajanje.Content = $"{b}h : 00min";
            }
            else
            {
                int b = (trajanje - 1) / 2;
                labelTrajanje.Content = $"{b}h : 30min";
            }
        }
        private void OsveziPodatke()
        {
            Usluge us = (Usluge)dataGridUsluge.SelectedItem;
            if (us == null)
            {
                return;
            }
            textBoxNaziv.Text = us.Naziv; textBoxTip.Text = us.Tip; textBoxCena.Text = us.Cena.ToString(); trajanje = us.Trajanje;
            IspisiLabelu(); textBoxSifraUsluge.Text = us.SifraUsluge.ToString(); textBoxOpis.Text = us.Opis; textBoxPrimedbe.Text = us.Primedbe;
        }

        private void textBoxCena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
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
        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            trajanje++;
            IspisiLabelu();
        }
        private void buttonDown_Click(object sender, RoutedEventArgs e)
        {
            if (trajanje < 2)
            {
                return;
            }
            trajanje--;
            IspisiLabelu();
        }
        private void textBoxSifraUsluge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                textBoxOpis.Focus();
                return;
            }
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            if (a == 1)
            {
                OsveziPodatke();
            }
            else
            {
                Resetuj();
            }
            
        }
        private void buttonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (a == 1 && dataGridUsluge.SelectedIndex == -1)
            {
                MessageBox.Show("Niste odabrali uslugu");
                return;
            }
            if (!Verifikacija())
            {
                return;
            }
            int sifra = (string.IsNullOrWhiteSpace(textBoxSifraUsluge.Text)) ? 0 : int.Parse(textBoxSifraUsluge.Text);
            if (a == 1)
            {
                Usluge us = (Usluge)dataGridUsluge.SelectedItem;
                Usluge usl = new Usluge();
                usl.UslugaID = us.UslugaID; usl.Naziv = textBoxNaziv.Text; usl.Tip = textBoxTip.Text; usl.Cena = decimal.Parse(textBoxCena.Text);
                usl.Trajanje = trajanje; usl.SifraUsluge = sifra; usl.Opis = textBoxOpis.Text;usl.Primedbe = textBoxPrimedbe.Text;
                if (zDAL.IzmeniUslugu(usl))
                {
                    MessageBox.Show("Podaci o usluzi izmenjeni");
                    OsveziPodatke();
                    dataGridUsluge.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Gredka pri cuvanju");
                }
            }
            else
            {
                Usluge inter = new Usluge { Naziv = textBoxNaziv.Text, Tip = textBoxTip.Text, Cena = decimal.Parse(textBoxCena.Text), Trajanje = trajanje, SifraUsluge = sifra, Opis = textBoxOpis.Text, Primedbe = textBoxPrimedbe.Text };
                if (zDAL.UbaciUslugu(inter))
                {
                    MessageBox.Show("Usluga Sacuvana");
                    Resetuj();
                }
                else
                {
                    MessageBox.Show("Greska pri cuvanju");
                }
            }
        }

        private void textBoxPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPretraga.Text))
            {
                dataGridUsluge.ItemsSource = prikaznaLista;
                return;
            }
            dataGridUsluge.SelectedIndex = -1;
            int sifra = 0;
            if (int.TryParse(textBoxPretraga.Text.Trim(), out sifra)) 
            {
                dataGridUsluge.ItemsSource = prikaznaLista.Where(k => k.SifraUsluge.ToString().StartsWith(sifra.ToString()));
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
                dataGridUsluge.ItemsSource = prikaznaLista.Where(k => (k.Naziv.ToLower().Contains(st1) || (k.Tip != null && k.Tip.ToLower().Contains(st1))) && (k.Naziv.ToLower().Contains(st2) || (k.Tip != null && k.Tip.ToLower().Contains(st2))));
            }
            else
            {
                dataGridUsluge.ItemsSource = prikaznaLista.Where(k => k.Naziv.ToLower().Contains(ulaz) || (k.Tip != null && k.Tip.ToLower().Contains(ulaz)));
            }
        }
        private void buttonNovaUsluga_Click(object sender, RoutedEventArgs e)
        {
            NovaUsluga ni = new NovaUsluga();
            ni.a = 0;
            ni.ShowDialog();
            dataGridUsluge.ItemsSource = zDAL.VratiSveUsluge();
        }
        private void dataGridUsluge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OsveziPodatke();
            buttonIzbaci.IsEnabled = (dataGridUsluge.SelectedIndex == -1) ? false : true;
        }

        private void buttonIzbaci_Click(object sender, RoutedEventArgs e)
        {
            Usluge us = (Usluge)dataGridUsluge.SelectedItem;
            List<Termini> lt = zDAL.VratiTermine(us.UslugaID);
            PopUp pu = new PopUp();
            pu.label1.Content = "Da li zelite da izbacite uslugu:";
            pu.label2.Content = us.Naziv + " " + us.Tip;
            pu.label3.Content = "";
            if (lt.Count > 0)
            {
                pu.label1.Content = (lt.Count % 10 == 1) ? $"Postoji {lt.Count} rezervisani termin za ovu uslugu." : $"Postoje {lt.Count} rezervisana termina za ovu uslugu.";
                pu.label3.Content = "Da li zelite da izbrisete USLUGU i TERMINE?";
                pu.Width = 350;
            }
            pu.Left = this.Left + 280;
            pu.Top = this.Top + 340;
            if (pu.ShowDialog() == true)
            {
                if (zDAL.IzbaciUslugu(us.UslugaID))
                {
                    MessageBox.Show("Usluga izbacena iz ponude");
                    Resetuj();
                    prikaznaLista = zDAL.VratiSveUsluge();
                    dataGridUsluge.ItemsSource = prikaznaLista;
                }
                else
                {
                    MessageBox.Show("Greska pri povezivanju sa bazom");
                }
            }
        }
        private void buttonPovratak_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
