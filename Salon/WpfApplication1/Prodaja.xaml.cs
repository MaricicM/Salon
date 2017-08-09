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
    /// Interaction logic for Prodaja.xaml
    /// </summary>
    public partial class Prodaja : Window
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        private Proizvodi odabraniProizvod = null;
        private Artikal odabraniArtikal = null;
        private List<Korisnici> pretragaKorisnika;
        private List<Proizvodi> listaProizvoda = new List<Proizvodi>();
        private List<Artikal> korpa = new List<Artikal>();
        private decimal total = 0;
        public Prodaja()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pretragaKorisnika = zDAL.VratiSveKorisnike();
            comboBoxKorisnik.ItemsSource = pretragaKorisnika;
            radioButtonSve.IsChecked = true;
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
            dataGridProizvodi.ItemsSource = listaProizvoda;
        }
        private void radioButtonSve_Checked(object sender, RoutedEventArgs e)
        {
            listaProizvoda = zDAL.VratiSveProizvode();
            dataGridProizvodi.ItemsSource = listaProizvoda;
        }
        private void radioButtonNaStanju_Checked(object sender, RoutedEventArgs e)
        {
            listaProizvoda = zDAL.VratiSveProizvode().Where(p => p.Stanje != 0).ToList();
            dataGridProizvodi.ItemsSource = listaProizvoda;
        }
        private void dataGridProizvodi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridProizvodi.SelectedIndex == -1)
            {
                odabraniProizvod = null;
                buttonDodaj.IsEnabled = false;
            }
            else
            {
                odabraniProizvod = (Proizvodi)dataGridProizvodi.SelectedItem;
                textBoxKolicina.Text = "1";
                buttonDodaj.IsEnabled = true;
                dataGridKorpa.SelectedIndex = -1;
            }            
        }
        private void dataGridProizvodi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (odabraniProizvod.Stanje == 0)
            {
                MessageBox.Show("Proizvoda nema na stanju");
                return;
            }
            Artikal art1 = korpa.FirstOrDefault(a => a.ArtikalID == odabraniProizvod.ProizvodID);
            if (art1 != null)
            {
                if (art1.Kolicina >= odabraniProizvod.Stanje)
                {
                    MessageBox.Show($"Nema dovoljno proizvoda na stanju. Trenutno stanje: {odabraniProizvod.Stanje}");
                    return;
                }
                art1.Kolicina++;
                art1.Ukupno += odabraniProizvod.Cena;
            }
            else
            {
                Artikal art = new Artikal { Naziv = odabraniProizvod.Naziv, ArtikalID = odabraniProizvod.ProizvodID, SifraProizvoda = odabraniProizvod.SifraProizvoda, Kolicina = 1, Proizvodjac = odabraniProizvod.Proizvodjac, Cena = odabraniProizvod.Cena, Ukupno = odabraniProizvod.Cena };
                korpa.Add(art);
            }
            total += odabraniProizvod.Cena;
            textBoxTotal.Text = total.ToString("#.##");
            dataGridKorpa.ItemsSource = korpa;
            dataGridKorpa.Items.Refresh();
            buttonIsprazniKorpu.IsEnabled = true;
        }

        private void buttonVise_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniProizvod != null && odabraniProizvod.Stanje == 0)
            {
                MessageBox.Show("Proizvoda nema na stanju");
                return;
            }
            int a = int.Parse(textBoxKolicina.Text);
            a++;
            if (dataGridKorpa.SelectedIndex != -1)
            {
                int stanje = listaProizvoda.FirstOrDefault(p => p.ProizvodID == odabraniArtikal.ArtikalID).Stanje;
                if (a > stanje)
                {
                    MessageBox.Show($"Nema dovoljno proizvoda na stanju. Trenutno stanje: {stanje}");
                    return;
                }
                odabraniArtikal.Kolicina = a;
                odabraniArtikal.Ukupno = odabraniArtikal.Cena * a;
                dataGridKorpa.Items.Refresh();
                total += odabraniArtikal.Cena;
                textBoxTotal.Text = total.ToString("#.##");
            }
            textBoxKolicina.Text = a.ToString();
        }
        private void textBoxKolicina_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxKolicina.Text = textBoxKolicina.Text.Trim();
            textBoxKolicina.CaretIndex = textBoxKolicina.Text.Length;
            if (!(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && !(e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (odabraniProizvod != null && odabraniProizvod.Stanje == 0)
                {
                    MessageBox.Show("Proizvoda nema na stanju");
                    return;
                }
                int a = int.Parse(textBoxKolicina.Text);
                if (dataGridKorpa.SelectedIndex == -1)
                {
                    if (a == 0)
                    {
                        textBoxKolicina.Text = "1";
                    }
                    buttonDodaj.Focus();
                }
                else
                {
                    if (a == 0)
                    {
                        korpa.Remove(odabraniArtikal);
                        if (korpa.Count == 0)
                        {
                            buttonIsprazniKorpu.IsEnabled = false;
                        }
                    }
                    else
                    {
                        odabraniArtikal.Kolicina = a;
                        odabraniArtikal.Ukupno = odabraniArtikal.Cena * a;
                    }
                    dataGridKorpa.Items.Refresh();
                }
            }
        }
        private void buttonManje_Click(object sender, RoutedEventArgs e)
        {
            int a = int.Parse(textBoxKolicina.Text);
            a--;
            if (dataGridKorpa.SelectedIndex == -1 && a < 1)
            {
                a = 1;
            }
            textBoxKolicina.Text = a.ToString();
            if (dataGridKorpa.SelectedIndex != -1)
            {
                if (a < 1)
                {
                    a = 0;
                    korpa.Remove(odabraniArtikal);
                }
                else
                {
                    odabraniArtikal.Kolicina = a;
                    odabraniArtikal.Ukupno = odabraniArtikal.Cena * a;
                }
                total -= odabraniArtikal.Cena;
                textBoxTotal.Text = total.ToString("#.##");                
                dataGridKorpa.Items.Refresh();
            }
            if (korpa.Count == 0)
            {
                buttonIsprazniKorpu.IsEnabled = false;
            }
        }
        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniProizvod.Stanje == 0)
            {
                MessageBox.Show("Proizvoda nema na stanju");
                return;
            }
            int kol = int.Parse(textBoxKolicina.Text);
            if (kol > odabraniProizvod.Stanje)
            {
                MessageBox.Show($"Nema dovoljno proizvoda na stanju. Trenutno stanje: {odabraniProizvod.Stanje}");
                return;
            }
            Artikal art1 = korpa.FirstOrDefault(a => a.ArtikalID == odabraniProizvod.ProizvodID);
            if (art1 != null)
            {
                if (kol + art1.Kolicina > odabraniProizvod.Stanje)
                {
                    MessageBox.Show($"Nema dovoljno proizvoda na stanju. Trenutno stanje: {odabraniProizvod.Stanje}");
                    return;
                }
                art1.Kolicina += kol;
                art1.Ukupno = odabraniProizvod.Cena * art1.Kolicina;
            }
            else
            {
                Artikal art = new Artikal { Naziv = odabraniProizvod.Naziv, ArtikalID = odabraniProizvod.ProizvodID, SifraProizvoda = odabraniProizvod.SifraProizvoda, Kolicina = kol, Proizvodjac = odabraniProizvod.Proizvodjac, Cena = odabraniProizvod.Cena, Ukupno = odabraniProizvod.Cena * kol };
                korpa.Add(art);
            }
            total += odabraniProizvod.Cena * kol;
            textBoxTotal.Text = total.ToString("#.##");
            dataGridKorpa.ItemsSource = korpa;
            dataGridKorpa.Items.Refresh();
            buttonIsprazniKorpu.IsEnabled = true;
        }
        private void buttonIzbaci_Click(object sender, RoutedEventArgs e)
        {
            total -= odabraniArtikal.Ukupno;
            textBoxTotal.Text = total.ToString("#.##");
            korpa.Remove(odabraniArtikal);
            dataGridKorpa.Items.Refresh();
            if (korpa.Count == 0)
            {
                buttonIsprazniKorpu.IsEnabled = false;
            }
        }
        private void buttonIsprazniKorpu_Click(object sender, RoutedEventArgs e)
        {
            korpa.Clear();
            dataGridKorpa.Items.Refresh();
            buttonIsprazniKorpu.IsEnabled = false;
            total = 0;
            textBoxTotal.Clear();
        }

        private void textBoxPretragaKorisnika_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPretragaKorisnika.Text))
            {
                comboBoxKorisnik.ItemsSource = pretragaKorisnika;
                return;
            }
            comboBoxKorisnik.SelectedIndex = -1;
            string ulaz = textBoxPretragaKorisnika.Text.ToLower().Trim();
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
                comboBoxKorisnik.ItemsSource = pretragaKorisnika.Where(k => (k.Ime.ToLower().Contains(st1) || k.Prezime.ToLower().Contains(st1)) && (k.Ime.ToLower().Contains(st2) || k.Prezime.ToLower().Contains(st2)));
            }
            else
            {
                comboBoxKorisnik.ItemsSource = pretragaKorisnika.Where(k => k.Ime.ToLower().Contains(ulaz) || k.Prezime.ToLower().Contains(ulaz));
            }
            if (!comboBoxKorisnik.IsDropDownOpen)
            {
                comboBoxKorisnik.IsDropDownOpen = true;
            }
        }
        private void textBoxPretragaKorisnika_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (comboBoxKorisnik.Items.Count == 1)
                {
                    comboBoxKorisnik.SelectedIndex = 0;
                    comboBoxKorisnik.IsDropDownOpen = false;
                    textBoxPretraga.Focus();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        private void comboBoxKorisnik_DropDownClosed(object sender, EventArgs e)
        {
            textBoxPretragaKorisnika.Clear();
        }
        private void dataGridKorpa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridKorpa.SelectedIndex == -1)
            {
                odabraniArtikal = null;
                buttonIzbaci.IsEnabled = false;
            }
            else
            {
                odabraniArtikal = (Artikal)dataGridKorpa.SelectedItem;
                textBoxKolicina.Text = odabraniArtikal.Kolicina.ToString();
                buttonIzbaci.IsEnabled = true;
                dataGridProizvodi.SelectedIndex = -1;
            }
        }
        private void dataGridKorpa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (odabraniArtikal.Kolicina > 1)
            {
                odabraniArtikal.Ukupno -= odabraniArtikal.Cena;
                odabraniArtikal.Kolicina--;
                textBoxKolicina.Text = odabraniArtikal.Kolicina.ToString();
            }
            else
            {
                korpa.Remove(odabraniArtikal);
            }
            total -= odabraniArtikal.Cena;
            textBoxTotal.Text = total.ToString("#.##");
            dataGridKorpa.Items.Refresh();
            if (korpa.Count == 0)
            {
                buttonIsprazniKorpu.IsEnabled = false;
            }            
        }

        private void buttonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (korpa.Count == 0)
            {
                MessageBox.Show("Korpa je prazna");
                return;
            }
            Korisnici kor = (Korisnici)comboBoxKorisnik.SelectedItem;
            int? korId = null;
            if (kor == null)
            {
                PopUp pu = new PopUp();
                pu.label1.Content = "Niste odabrali korisnika";
                pu.label2.Content = "";
                pu.label3.Content = "Prodati anonimnom kupcu?";
                pu.Left = this.Left + 880;
                pu.Top = this.Top + 150;
                if (pu.ShowDialog() != true)
                {
                    comboBoxKorisnik.IsDropDownOpen = true;
                    textBoxPretragaKorisnika.Focus();
                    return;
                }
            }
            else
            {
                korId = kor.KorisnikID;
            }
            Porudzbine por = new Porudzbine { DatumPorudzbine = DateTime.Today, KorisnikID = korId, Total = total };
            if (!zDAL.UbaciPorudzbinu(por))
            {
                MessageBox.Show("Problem u cuvanju");
                return;
            }

            List<DetaljiPorudzbine> listaDetalja = new List<DetaljiPorudzbine>();
            foreach (Artikal art in korpa)
            {
                listaDetalja.Add(new DetaljiPorudzbine { ProizvodID = art.ArtikalID, Kolicina = art.Kolicina, PorudzbinaID = por.PorudzbinaID });
            }
            if (zDAL.UbaciDetaljePorudzbine(listaDetalja))
            {
                listaProizvoda = zDAL.VratiSveProizvode();
                dataGridProizvodi.Items.Refresh();
                textBoxPretraga.Clear();
                comboBoxKorisnik.SelectedIndex = -1;
                korpa.Clear();
                dataGridKorpa.Items.Refresh();
                total = 0;
                textBoxTotal.Clear();
                MessageBox.Show("Prodato!");
            }
            else
            {
                MessageBox.Show("Problem u cuvanju");
            }            
        }
        private void buttonPovratak_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
