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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        private Radnici odabraniRadnik = null;
        private SviRadnici odabraniTerminSvi = null;
        private Termini odabraniTerminJedan = null;
        private Termini terminZaOtkaz = null;
        public List<Korisnici> pretragaKorisnika = new List<Korisnici>();
        public List<Usluge> pretragaUsluga = new List<Usluge>();
        private List<Termini> terminiJedanRadnik = new List<Termini>();
        private List<SviRadnici> terminiSviRadnici = new List<SviRadnici>();
        private List<TempOdabir> tempOdabrani = new List<TempOdabir>();
        private Border bord = null; 
        private int brojDana = 1;
        private int SlobZak = 3;
        private int trajanje = 1;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
            pretragaKorisnika = zDAL.VratiSveKorisnike();
            pretragaKorisnika.Insert(0, new Korisnici { Ime = "...novi ", Prezime = "korisnik...", KorisnikID = -1 });
            comboBoxKorisnik.ItemsSource = pretragaKorisnika;
            pretragaUsluga = zDAL.VratiSveUsluge();
            comboBoxUsluga.ItemsSource = pretragaUsluga;
            List<Radnici> lr = zDAL.VratiSveRadnike();
            comboBoxAdmin.ItemsSource = lr.Where(r=> r.Pozicija == 2);
            comboBoxAdmin.SelectedIndex = 0;
            comboBoxSpec.ItemsSource = lr.Where(r => r.Pozicija == 1);

            radio1Dan.Checked += radioDani_Click;
            radioSviRadnici.Checked += radioSviRadnici_Checked;
            radioSvi.Checked += radioSlobZakSvi_Checked;
            radioZakazivanje.Checked += radioZakazivanje_Checked;
            pickerPocetniDatum.SelectedDateChanged += pickerPocetniDatum_SelectedDateChanged;
            SviRadnici();
        }
        private void Resetuj ()
        {
            comboBoxAdmin.SelectedIndex = 0;
            comboBoxUsluga.SelectedIndex = -1;
            comboBoxKorisnik.SelectedIndex = -1;
            comboBoxSpec.SelectedIndex = -1;
            pickerZakazivanje.SelectedDate = DateTime.Today;
            buttonMinut.Content = 00.ToString();
            textBoxSat.Text = "7";
            trajanje = 1;
            ResetujRasponZaSve();
        }
        private void ResetujOtkazivanje ()
        {
            textBoxAdministrator.Clear();
            textBoxDatum.Clear();
            textBoxKorisnik.Clear();
            textBoxRadnik.Clear();
            textBoxUsluga.Clear();
            textBoxVreme.Clear();
            buttonOtkazi.IsEnabled = false;
        }
        private bool Verifikacija()
        {
            if (textBoxSat.IsFocused)
            {
                pickerZakazivanje.Focus();
            }
            if (textBoxSlobodno.Text.Contains("x"))
            {
                MessageBox.Show("Odabrani termin nije slobodan");
                return false;
            }
            if (comboBoxKorisnik.SelectedIndex < 1)
            {
                MessageBox.Show("Niste odabrali korisnika");
                comboBoxKorisnik.IsDropDownOpen = true;
                return false;
            }
            if (pickerZakazivanje.SelectedDate < DateTime.Today)
            {
                PopUp pu = new PopUp();
                pu.label1.Content = "Odabrani datum je u proslosti";
                pu.label2.Content = pickerZakazivanje.SelectedDate.Value.ToShortDateString();
                pu.label3.Content = "Da li ste zeleli da zakazete retroaktivno?";
                pu.Left = this.Left + 980;
                pu.Top = this.Top + 350;
                if (pu.ShowDialog() == false)
                {
                    pickerZakazivanje.Focus();
                    return false;
                }
            }
            if (comboBoxUsluga.SelectedIndex == -1)
            {
                MessageBox.Show("Niste odabrali uslugu");
                comboBoxUsluga.IsDropDownOpen = true;
                return false;
            }
            if (comboBoxSpec.SelectedIndex == -1)
            {
                MessageBox.Show("Niste odabrali radnika");
                comboBoxSpec.IsDropDownOpen = true;
                return false;
            }
            return true;
        }

        private void OdaberiRasponZaSve()
        {
            if (odabraniTerminSvi == null || bord == null)
            {
                return;
            }
            int indeks = terminiSviRadnici.IndexOf(odabraniTerminSvi) - 1;
            int tr = trajanje;
            int slucaj = int.Parse(bord.Uid);
            bool bul = true;
            while (tr > 0)
            {
                int c = indeks + tr;
                if (c >= terminiSviRadnici.Count)
                {
                    tr -= c - terminiSviRadnici.Count + 1;
                    c = indeks + tr;
                    bul = false;
                }
                if (terminiSviRadnici[c - tr].DatumVreme.Hour - terminiSviRadnici[c].DatumVreme.Hour > 2)
                {
                    int e = (c % 32 == 0) ? 1 : ((c - 1) % 32 == 0) ? 2 : ((c - 2) % 32 == 0) ? 3 : ((c - 3) % 32 == 0)? 4 : ((c - 4) % 32 == 0) ? 5 : ((c - 5) % 32 == 0) ? 6 : ((c - 6) % 32 == 0) ? 7 : 0;
                    bul = false;
                    c -= e;
                    tr -= e;
                }
                int d = 0;
                switch (slucaj)
                {
                    case 1:
                        d = terminiSviRadnici[c].Bojana;
                        terminiSviRadnici[c].Bojana = (d == 0) ? 2000 : 2000 + d;
                        break;
                    case 2:
                        d = terminiSviRadnici[c].Jovana;
                        terminiSviRadnici[c].Jovana = (d == 0) ? 2000 : 2000 + d;
                        break;
                    case 3:
                        d = terminiSviRadnici[c].Ivana;
                        terminiSviRadnici[c].Ivana = (d == 0) ? 2000 : 2000 + d;
                        break;
                }
                tempOdabrani.Add(new TempOdabir { Indeks = c, Slucaj = slucaj, Vrednost = d });
                tr--;
            }
            listViewTermini.Items.Refresh();
            foreach (TempOdabir to in tempOdabrani)
            {
                if (to.Vrednost != 0)
                {
                    bul = false;
                }
            }
            if (bul)
            {
                textBoxSlobodno.Text = "Slobodno √";
                textBoxSlobodno.Background = Brushes.LightGreen;
                buttonDozvoli.IsEnabled = false;
            }
            else
            {
                textBoxSlobodno.Text = "Zauzeto x";
                textBoxSlobodno.Background = Brushes.LightCoral;
                int a = (slucaj == 1) ? odabraniTerminSvi.Bojana : (slucaj == 2) ? odabraniTerminSvi.Jovana : odabraniTerminSvi.Ivana;
                buttonDozvoli.IsEnabled = (a > 2000 && a < 2665) ? false : true;
            }
        }
        private void ResetujRasponZaSve()
        {
            foreach (TempOdabir to in tempOdabrani)
            {
                switch (to.Slucaj)
                {
                    case 1:
                        terminiSviRadnici[to.Indeks].Bojana = to.Vrednost;
                        break;
                    case 2:
                        terminiSviRadnici[to.Indeks].Jovana = to.Vrednost;
                        break;
                    case 3:
                        terminiSviRadnici[to.Indeks].Ivana = to.Vrednost;
                        break;
                }                
            }
            tempOdabrani.Clear();
        }
        private void OdaberiRasponZaJednog()
        {
            foreach (Termini ter in terminiJedanRadnik.Where(t => t.AdministratorID == -2))
            {
                ter.AdministratorID = 0;
            }
            if (odabraniTerminJedan == null || odabraniTerminJedan.DatumVreme.Hour == 0)
            {
                dataGridTermini.Items.Refresh();
                return;
            }
            int tra = trajanje - 1;
            bool bul = true;
            int ind = terminiJedanRadnik.IndexOf(odabraniTerminJedan);
            while (ind + tra >= terminiJedanRadnik.Count)
            {
                tra--;
                bul = false;
            }
            while (terminiJedanRadnik[ind].DatumVreme.Hour - terminiJedanRadnik[ind + tra].DatumVreme.Hour > 0)
            {
                tra--;
                bul = false;
            }
            for (int i = tra; i >= 0; i--)
            {
                terminiJedanRadnik[ind + i].AdministratorID = -2;
                if (terminiJedanRadnik[ind + i].KorisnikID != 0)
                {
                    bul = false;
                }
            }
            if (bul)
            {
                textBoxSlobodno.Text = "Slobodno √";
                textBoxSlobodno.Background = Brushes.LightGreen;
                buttonDozvoli.IsEnabled = false;
            }
            else
            {
                textBoxSlobodno.Text = "Zauzeto x";
                textBoxSlobodno.Background = Brushes.LightCoral;
                buttonDozvoli.IsEnabled = (odabraniTerminJedan.RadnikID == 11) ? false : true;
            }
            
            dataGridTermini.Items.Refresh();
        }
        private void JedanRadnik(int RadnikID)
        {
            terminiJedanRadnik.Clear();
            DateTime datumZakazivanja = (DateTime)pickerPocetniDatum.SelectedDate;
            int b = -1;
            int bd = brojDana;
            ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
            List<Termini> sviTermini = zDAL.VratiSveTermine().Where(t => t.DatumVreme >= pickerPocetniDatum.SelectedDate).ToList();
            while (bd > 0)
            {
                List<Termini> tempTermini = sviTermini.Where(t => t.DatumVreme.Date == datumZakazivanja && t.RadnikID == RadnikID).OrderBy(te=> te.DatumVreme.Hour).ToList();
                DateTime dt = (DateTime)pickerPocetniDatum.SelectedDate + new TimeSpan(-1 - b, 7, 0, 0);
                DateTime ft = (DateTime)pickerPocetniDatum.SelectedDate + new TimeSpan(-1 - b, 22, 0, 0);
                terminiJedanRadnik.Add(new Termini { KorisnikID = b, UslugaID = b, AdministratorID = -1, DatumVreme = dt.Date + new TimeSpan(0, 0, 0) });
                while (dt <= ft)
                {
                    Termini t = tempTermini.FirstOrDefault(zat => zat.DatumVreme.TimeOfDay == dt.TimeOfDay);
                    if (t != null)
                    {
                        int a = t.Usluge.Trajanje - 1;
                        TimeSpan ts = new TimeSpan(0, 0, 0);
                        terminiJedanRadnik.Add(new Termini { DatumVreme = t.DatumVreme + ts, KorisnikID = t.KorisnikID, UslugaID = t.UslugaID, RadnikID = 11 });
                        dt += new TimeSpan(0, 30, 0);
                        while (a >= 1)
                        {
                            if (tempTermini.Any(tt => tt.DatumVreme.TimeOfDay == dt.TimeOfDay)) 
                            {
                                break;
                            }
                            ts += new TimeSpan(0, 30, 0);
                            terminiJedanRadnik.Add(new Termini { DatumVreme = t.DatumVreme + ts, KorisnikID = t.KorisnikID, UslugaID = t.UslugaID });
                            dt += new TimeSpan(0, 30, 0);
                            a--;
                        }
                    }
                    else
                    {
                        terminiJedanRadnik.Add(new Termini { DatumVreme = dt, KorisnikID = 0 });
                        dt += new TimeSpan(0, 30, 0);
                    }
                }
                bd--;
                b--;
                datumZakazivanja += new TimeSpan(1, 0, 0, 0);
            }

            if (SlobZak == 1)
            {
                terminiJedanRadnik.RemoveAll(zt => zt.KorisnikID > 0);
            }
            if (SlobZak == 2)
            {
                terminiJedanRadnik.RemoveAll(zt => zt.KorisnikID == 0);
            }
            dataGridTermini.ItemsSource = terminiJedanRadnik.Where(t => t.DatumVreme.Hour >= (int)sliderOd.Value && t.DatumVreme.Hour <= (int)sliderDo.Value || t.KorisnikID < 0);
            dataGridTermini.Items.Refresh();
            
        }
        private void SviRadnici()
        {
            terminiSviRadnici.Clear();
            listViewNaslov.Items.Clear();
            listViewNaslov.Items.Add(new SviRadnici { DatumVreme = DateTime.Today + new TimeSpan(0, 0, 0), Bojana = 666, Jovana = 777, Ivana = 888 });
            int b = 1000;
            int bd = brojDana;
            ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
            List<Termini> sviTermini = zDAL.VratiSveTermine().Where(t => t.DatumVreme >= pickerPocetniDatum.SelectedDate).ToList();
            while (bd > 0)
            {
                DateTime dt = (DateTime)pickerPocetniDatum.SelectedDate + new TimeSpan(b - 1000, 7, 0, 0);
                DateTime ft = (DateTime)pickerPocetniDatum.SelectedDate + new TimeSpan(b - 1000, 22, 0, 0);
                terminiSviRadnici.Add(new SviRadnici { DatumVreme = dt.Date + new TimeSpan(0, 0, 0), Bojana = b + 10, Jovana = b, Ivana = b + 10 });
                while (dt <= ft)
                {
                    terminiSviRadnici.Add(new SviRadnici { DatumVreme = dt });
                    dt += new TimeSpan(0, 30, 0);
                }
                List<Termini> lt = sviTermini.Where(tr => tr.DatumVreme.Date == dt.Date).OrderBy(t=> t.DatumVreme).ToList();
                foreach (Termini t in lt)
                {
                    int trajanje = t.Usluge.Trajanje;
                    SviRadnici rad = terminiSviRadnici.FirstOrDefault(s => s.DatumVreme == t.DatumVreme);
                    int a = terminiSviRadnici.IndexOf(rad);
                    int p = 2;
                    switch (t.RadnikID)
                    {
                        case 1:
                            rad.Bojana = t.UslugaID;
                            terminiSviRadnici[a + 1].Bojana = (trajanje > 1) ? -t.KorisnikID : 0;
                            while (p < 8 && a + p < 32)
                            {
                                terminiSviRadnici[a + p].Bojana = (trajanje > p) ? 999 : 0;
                                p++;
                            }
                            break;
                        case 2:
                            rad.Jovana = t.UslugaID;
                            terminiSviRadnici[a + 1].Jovana = (trajanje > 1) ? -t.KorisnikID : 0;
                            while (p < 8 && a + p < 32)
                            {
                                terminiSviRadnici[a + p].Jovana = (trajanje > p) ? 999 : 0;
                                p++;
                            }
                            break;
                        case 3:
                            rad.Ivana = t.UslugaID;
                            terminiSviRadnici[a + 1].Ivana = (trajanje > 1) ? -t.KorisnikID : 0;
                            while (p < 8 && a + p < 32)
                            {
                                terminiSviRadnici[a + p].Ivana = (trajanje > p) ? 999 : 0;
                                p++;
                            }
                            break;
                    }
                }
                b++;
                bd--;
            }
            listViewTermini.ItemsSource = terminiSviRadnici.Where(t => t.DatumVreme.Hour >= (int)sliderOd.Value && t.DatumVreme.Hour <= (int)sliderDo.Value || t.Jovana > 999);
            listViewTermini.Items.Refresh();
        }

        private void menKor_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            NoviKorisnik nk = new NoviKorisnik();
            nk.a = int.Parse(mi.Uid);
            nk.ShowDialog();
            ZakazivanjeDAL zd = new ZakazivanjeDAL();
            pretragaKorisnika = zd.VratiSveKorisnike();
            pretragaKorisnika.Insert(0, new Korisnici { Ime = "...novi ", Prezime = "korisnik...", KorisnikID = -1 });
            comboBoxKorisnik.ItemsSource = pretragaKorisnika;
            listViewTermini.Items.Refresh();
            if (radioSviRadnici.IsChecked == true)
            {
                SviRadnici();
            }
        }
        private void menZap_Click(object sender, RoutedEventArgs e)
        {
            NoviKorisnik nk = new NoviKorisnik();
            nk.Title = "Novi Radnik";
            nk.labelPol.Content = "Radno mesto"; nk.radioMuski.Visibility = Visibility.Collapsed; nk.radioZenski.Visibility = Visibility.Collapsed; nk.labelRadnoMesto.Visibility = Visibility.Visible;
            nk.labelDatum.Content = "Datum zaposlenja:"; nk.textBoxDan.Text = DateTime.Today.Day.ToString(); nk.textBoxMesec.Text = DateTime.Today.Month.ToString(); nk.textBoxGodina.Text = DateTime.Today.Year.ToString();
            nk.labelAlergeni.Visibility = Visibility.Collapsed; nk.textBoxAlergeni.Visibility = Visibility.Collapsed;
            nk.labelZabeleske.Visibility = Visibility.Collapsed; nk.textBoxZabeleske.Visibility = Visibility.Collapsed;
            nk.buttonUbaci.Content = "Unesi novog radnika";
            MenuItem mi = (MenuItem)sender;
            nk.a = int.Parse(mi.Uid); 
            nk.ShowDialog();
            ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
            comboBoxAdmin.ItemsSource = zDAL.VratiSveRadnike().Where(r => r.Pozicija == 2);
            comboBoxAdmin.SelectedIndex = 0;
            SviRadnici();
        }
        private void menInt_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            NovaUsluga ni = new NovaUsluga();
            ni.a = int.Parse(mi.Uid);
            ni.ShowDialog();
            ZakazivanjeDAL zd = new ZakazivanjeDAL();
            pretragaUsluga = zd.VratiSveUsluge();
            comboBoxUsluga.ItemsSource = pretragaUsluga;
            trajanje = 1;
            SviRadnici();
        }
        private void menPro1_Click(object sender, RoutedEventArgs e)
        {
            Prodaja pr = new WpfApplication1.Prodaja();
            pr.Show();
        }
        private void menPro2_Click(object sender, RoutedEventArgs e)
        {
            Lager la = new Lager();
            la.Title = "Unos Robe";
            la.buttonIzbaci.Visibility = Visibility.Collapsed;
            la.buttonIzmeni.Visibility = Visibility.Collapsed;
            la.ShowDialog();
        }
        private void menPro3_Click(object sender, RoutedEventArgs e)
        {
            Lager la = new Lager();
            la.Width = 575;
            la.buttonNoviProizvod.Visibility = Visibility.Collapsed;
            la.ShowDialog();
        }

        private void pickerPocetniDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (radioSviRadnici.IsChecked == true)
            {
                SviRadnici();
            }
            else
            {
                JedanRadnik(odabraniRadnik.RadnikID);
            }
        }
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (terminiSviRadnici.Count == 0)
            {
                return;
            }
            if (radioSviRadnici.IsChecked == true)
            {
                listViewTermini.ItemsSource = terminiSviRadnici.Where(t => t.DatumVreme.Hour >= (int)sliderOd.Value && t.DatumVreme.Hour <= (int)sliderDo.Value || t.Jovana > 999);
                listViewTermini.Items.Refresh();
            }
            else
            {
                dataGridTermini.ItemsSource = terminiJedanRadnik.Where(t => t.DatumVreme.Hour >= (int)sliderOd.Value && t.DatumVreme.Hour <= (int)sliderDo.Value || t.KorisnikID < 0);
                dataGridTermini.Items.Refresh();
            }
        }
        private void buttonResetujDatumiVreme_Click(object sender, RoutedEventArgs e)
        {
            pickerPocetniDatum.SelectedDate = DateTime.Today;
            sliderOd.Value = 7;
            sliderDo.Value = 22;
        }

        private void radioDani_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            brojDana = int.Parse((rb.Content as string).Substring(0, 1));
            if (radioSviRadnici.IsChecked == true)
            {
                SviRadnici();
            }
            else
            {
                JedanRadnik(odabraniRadnik.RadnikID);
            }
        }
        private void radioRadnik_Checked(object sender, RoutedEventArgs e)
        {
            radioSlobodni.IsEnabled = true;
            radioZakazani.IsEnabled = true;
            dataGridTermini.Visibility = Visibility.Visible;
            listViewTermini.Visibility = Visibility.Collapsed;
            listViewNaslov.Visibility = Visibility.Collapsed;

            RadioButton rb = (RadioButton)sender;
            odabraniRadnik = zDAL.VratiSveRadnike().First(r => r.Ime == rb.Content.ToString());
            comboBoxSpec.SelectedIndex = int.Parse(rb.Uid) - 1;
            comboBoxSpec.IsEnabled = false;
            JedanRadnik(odabraniRadnik.RadnikID);
        }
        private void radioSviRadnici_Checked(object sender, RoutedEventArgs e)
        {
            dataGridTermini.Visibility = Visibility.Collapsed;
            listViewTermini.Visibility = Visibility.Visible;
            listViewNaslov.Visibility = Visibility.Visible;
            radioSvi.IsChecked = true;
            radioSlobodni.IsEnabled = false;
            radioZakazani.IsEnabled = false;
            comboBoxSpec.IsEnabled = true;
            SviRadnici();
        }
        private void radioSlobZakSvi_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            SlobZak = int.Parse(rb.Uid);
            JedanRadnik(odabraniRadnik.RadnikID);
        }

        private void listViewTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            odabraniTerminSvi = (SviRadnici)listViewTermini.SelectedItem;
            if (odabraniTerminSvi != null)
            {
                if (odabraniTerminSvi.DatumVreme.Hour == 0)
                {
                    buttonDozvoli.IsEnabled = false;
                    ResetujRasponZaSve();
                }
                if (radioZakazivanje.IsChecked == true)
                {
                    textBoxSat.Text = odabraniTerminSvi.DatumVreme.Hour.ToString();
                    buttonMinut.Content = odabraniTerminSvi.DatumVreme.Minute.ToString();
                    pickerZakazivanje.SelectedDate = odabraniTerminSvi.DatumVreme.Date;                    
                }                             
            }
        }
        private void listViewTermini_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (odabraniTerminSvi == null)
            {
                return;
            }
            if (odabraniTerminSvi.DatumVreme.Hour != 0)
            {
                ResetujRasponZaSve();
                OdaberiRasponZaSve();
            }
            else
            {
                listViewTermini.Items.Refresh();
            }
            if (radioOtkazivanje.IsChecked == true)
            {
                if (odabraniTerminSvi.DatumVreme.Hour == 0)
                {
                    return;
                }
                int rad = int.Parse(bord.Uid);
                switch (rad)
                {
                    case 1:
                        if (odabraniTerminSvi.Bojana == 2000)
                        {
                            ResetujOtkazivanje();
                            return;
                        }
                        break;
                    case 2:
                        if (odabraniTerminSvi.Jovana == 2000)
                        {
                            ResetujOtkazivanje();
                            return;
                        }
                        break;
                    case 3:
                        if (odabraniTerminSvi.Ivana == 2000)
                        {
                            ResetujOtkazivanje();
                            return;
                        }
                        break;
                    default:
                        break;
                }
                List<Termini> sviTermini = zDAL.VratiSveTermine().Where(t => t.Radnici1.RadnikID == rad && t.DatumVreme.Date == odabraniTerminSvi.DatumVreme.Date).ToList();
                terminZaOtkaz = null;
                DateTime dt = odabraniTerminSvi.DatumVreme;
                while (terminZaOtkaz == null)
                {
                    terminZaOtkaz = sviTermini.FirstOrDefault(t => t.DatumVreme == dt);
                    dt -= new TimeSpan(0, 30, 0);
                }
                Radnici radnik = terminZaOtkaz.Radnici1;
                Korisnici kor = terminZaOtkaz.Korisnici;
                Usluge usl = terminZaOtkaz.Usluge;
                Radnici admin = terminZaOtkaz.Radnici;
                textBoxAdministrator.Text = $"{admin.Ime} {admin.Prezime}";
                textBoxDatum.Text = terminZaOtkaz.DatumVreme.ToShortDateString();
                textBoxKorisnik.Text = kor.Ime + " " + kor.Prezime;
                textBoxUsluga.Text = usl.Naziv + " " + usl.Tip;
                textBoxRadnik.Text = radnik.Ime + " " + radnik.Prezime;
                textBoxVreme.Text = terminZaOtkaz.DatumVreme.ToShortTimeString();
                buttonOtkazi.IsEnabled = true;
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bord = sender as Border;
            if (radioZakazivanje.IsChecked == true)
            {
                comboBoxSpec.SelectedIndex = int.Parse(bord.Uid) - 1;
            }            
        }
        private void dataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            odabraniTerminJedan = (Termini)dataGridTermini.SelectedItem;
            if (odabraniTerminJedan == null)
            {
                return;
            }
            textBoxSat.Text = odabraniTerminJedan.DatumVreme.Hour.ToString();
            buttonMinut.Content = odabraniTerminJedan.DatumVreme.Minute.ToString();
            pickerZakazivanje.SelectedDate = odabraniTerminJedan.DatumVreme.Date;
            if (odabraniTerminJedan.DatumVreme.Hour != 0)
            {
                OdaberiRasponZaJednog();
            }
            else
            {
                foreach (Termini ter in terminiJedanRadnik.Where(t => t.AdministratorID == -2))
                {
                    ter.AdministratorID = 0;
                }
                buttonDozvoli.IsEnabled = false;
                dataGridTermini.Items.Refresh();
            }
            if (radioOtkazivanje.IsChecked == true)
            {
                if (odabraniTerminJedan.KorisnikID == 0 || odabraniTerminJedan.DatumVreme.Hour == 0)
                {
                    ResetujOtkazivanje();
                    return;
                }
                int radID = odabraniRadnik.RadnikID;
                List<Termini> sviTermini = zDAL.VratiSveTermine().Where(t => t.RadnikID == radID && t.DatumVreme.Date == odabraniTerminJedan.DatumVreme.Date).ToList();
                terminZaOtkaz = null;
                DateTime dt = odabraniTerminJedan.DatumVreme;
                while (terminZaOtkaz == null)
                {
                    terminZaOtkaz = sviTermini.FirstOrDefault(t => t.DatumVreme == dt);
                    dt -= new TimeSpan(0, 30, 0);
                }
                Radnici radnik = terminZaOtkaz.Radnici1;
                Korisnici kor = terminZaOtkaz.Korisnici;
                Usluge usl = terminZaOtkaz.Usluge;
                Radnici admin = terminZaOtkaz.Radnici;
                textBoxAdministrator.Text = $"{admin.Ime} {admin.Prezime}";
                textBoxDatum.Text = terminZaOtkaz.DatumVreme.ToShortDateString();
                textBoxKorisnik.Text = kor.Ime + " " + kor.Prezime;
                textBoxUsluga.Text = usl.Naziv + " " + usl.Tip;
                textBoxRadnik.Text = radnik.Ime + " " + radnik.Prezime;
                textBoxVreme.Text = terminZaOtkaz.DatumVreme.ToShortTimeString();
                buttonOtkazi.IsEnabled = true;
            }
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
                    textBoxPretragaUsluga.Focus();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        private void comboBoxKorisnik_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBoxKorisnik.SelectedIndex == -1)
            {
                return;
            }
            if ((comboBoxKorisnik.SelectedItem as Korisnici).KorisnikID == -1)
            {
                NoviKorisnik nk = new NoviKorisnik();
                nk.a = 2;
                if (nk.ShowDialog() == true)
                {
                    ZakazivanjeDAL zd = new ZakazivanjeDAL();
                    pretragaKorisnika = zd.VratiSveKorisnike();
                    pretragaKorisnika.Insert(0, new Korisnici { Ime = "...novi ", Prezime = "korisnik...", KorisnikID = -1 });
                    comboBoxKorisnik.ItemsSource = pretragaKorisnika;
                    comboBoxKorisnik.SelectedIndex = pretragaKorisnika.Count - 1;
                }
                else
                {
                    comboBoxKorisnik.SelectedIndex = -1;
                }
            }
            textBoxPretragaKorisnika.Clear();
        }
        private void textBoxPretragaUsluga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPretragaUsluga.Text))
            {
                comboBoxUsluga.ItemsSource = pretragaUsluga;
                return;
            }
            comboBoxUsluga.SelectedIndex = -1;
            int sifra = 0;
            if (int.TryParse(textBoxPretragaUsluga.Text.Trim(), out sifra))
            {
                comboBoxUsluga.ItemsSource = pretragaUsluga.Where(k => k.SifraUsluge.ToString().StartsWith(sifra.ToString()));
                comboBoxUsluga.IsDropDownOpen = true;
                return;
            }
            string ulaz = textBoxPretragaUsluga.Text.ToLower().Trim();
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
                comboBoxUsluga.ItemsSource = pretragaUsluga.Where(k => (k.Naziv.ToLower().Contains(st1) || (k.Tip != null && k.Tip.ToLower().Contains(st1))) && (k.Naziv.ToLower().Contains(st2) || (k.Tip != null && k.Tip.ToLower().Contains(st2))));
            }
            else
            {
                comboBoxUsluga.ItemsSource = pretragaUsluga.Where(k => k.Naziv.ToLower().Contains(ulaz) || (k.Tip != null && k.Tip.ToLower().Contains(ulaz)));
            }
            comboBoxUsluga.IsDropDownOpen = true;
        }
        private void textBoxPretragaUsluga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (comboBoxUsluga.Items.Count == 1)
                {
                    comboBoxUsluga.SelectedIndex = 0;
                    comboBoxUsluga.IsDropDownOpen = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        private void comboBoxUsluga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxUsluga.SelectedItem != null)
            {
                trajanje = (comboBoxUsluga.SelectedItem as Usluge).Trajanje;
            }
            if (odabraniTerminSvi != null && odabraniTerminSvi.DatumVreme.Hour != 0 && radioSviRadnici.IsChecked == true)
            {
                ResetujRasponZaSve();
                OdaberiRasponZaSve();
            }
            if (odabraniTerminJedan != null && odabraniTerminJedan.DatumVreme.Hour != 0 && radioSviRadnici.IsChecked != true)
            {
                OdaberiRasponZaJednog();
            }
            textBoxPretragaUsluga.Clear();
        }

        private void comboBoxSpec_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBoxSpec.SelectedIndex < 0)
            {
                return;
            }
            Radnici spec = (Radnici)comboBoxSpec.SelectedItem;
            bord = new Border { Uid = spec.RadnikID.ToString() };
            ResetujRasponZaSve();
            OdaberiRasponZaSve();
        }
        private void buttonZakazi_Click(object sender, RoutedEventArgs e)
        {
            if (!Verifikacija())
            {
                return;
            }
            DateTime dt = pickerZakazivanje.SelectedDate.Value + new TimeSpan(int.Parse(textBoxSat.Text), int.Parse(buttonMinut.Content.ToString()), 0);
            int adminId = (comboBoxAdmin.SelectedItem as Radnici).RadnikID;
            Termini ter = new Termini { DatumVreme = dt, KorisnikID = (comboBoxKorisnik.SelectedItem as Korisnici).KorisnikID, RadnikID = (comboBoxSpec.SelectedItem as Radnici).RadnikID, AdministratorID = adminId, UslugaID = (comboBoxUsluga.SelectedItem as Usluge).UslugaID };
            ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
            if (zDAL.UbaciTermin(ter))
            {
                if (radioSviRadnici.IsChecked == true)
                {
                    ResetujRasponZaSve();
                    SviRadnici();
                }
                else
                {
                    JedanRadnik(odabraniRadnik.RadnikID);
                }
                
            }
            else
            {
                MessageBox.Show("Problem u cuvanju! Probajte ponovo ili kontaktirajte administaratora");
            }

        }
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            Resetuj();
        }
        private void buttonDozvoli_Click(object sender, RoutedEventArgs e)
        {
            textBoxSlobodno.Text = "Dozvoljeno √";
            textBoxSlobodno.Background = Brushes.LightBlue;
            buttonDozvoli.IsEnabled = false;
        }

        private void pickerZakazivanje_CalendarClosed(object sender, RoutedEventArgs e)
        {
            pickerPocetniDatum.SelectedDate = pickerZakazivanje.SelectedDate;
            TekstBoksIzgubljenFokus();
        }
        private void textBoxSat_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                TekstBoksIzgubljenFokus();
            }
        }
        private void textBoxSat_LostFocus(object sender, RoutedEventArgs e)
        {
            TekstBoksIzgubljenFokus();
        }
        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            int sat;
            int.TryParse(textBoxSat.Text, out sat);
            sat = (sat < 6) ? 6 : (sat > 21) ? 21 : sat;
            textBoxSat.Text = (sat + 1).ToString();
            TekstBoksIzgubljenFokus();
        }
        private void buttonDown_Click(object sender, RoutedEventArgs e)
        {
            int sat;
            int.TryParse(textBoxSat.Text, out sat);
            sat = (sat < 8) ? 8 : (sat > 22) ? 22 : sat;
            textBoxSat.Text = (sat - 1).ToString();
            TekstBoksIzgubljenFokus();
        }
        private void buttonMinut_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            but.Content = ((string)but.Content == "0") ? "30" : "0";
            TekstBoksIzgubljenFokus();
        }
        private void TekstBoksIzgubljenFokus()
        {
            int sat = 7;
            int.TryParse(textBoxSat.Text, out sat);
            sat = (sat < 7) ? 7 : (sat > 22) ? 22 : sat;
            textBoxSat.Text = sat.ToString();
            if (sat == 22)
            {
                buttonMinut.Content = "0";
            }
            int minut = int.Parse(buttonMinut.Content.ToString());
            if (radioSviRadnici.IsChecked == true)
            {
                odabraniTerminSvi = terminiSviRadnici.FirstOrDefault(t => t.DatumVreme.Hour == sat && t.DatumVreme.Minute == minut && t.DatumVreme.Date == pickerZakazivanje.SelectedDate);
                ResetujRasponZaSve();
                OdaberiRasponZaSve();
                listViewTermini.ScrollIntoView(odabraniTerminSvi);
            }
            else
            {
                odabraniTerminJedan = terminiJedanRadnik.FirstOrDefault(t => t.DatumVreme.Hour == sat && t.DatumVreme.Minute == minut && t.DatumVreme.Date == pickerZakazivanje.SelectedDate);
                OdaberiRasponZaJednog();
                dataGridTermini.ScrollIntoView(odabraniTerminJedan);
            }            
        }

        private void radioOtkazivanje_Checked(object sender, RoutedEventArgs e)
        {
            trajanje = 1;
            Resetuj();
            listViewTermini.Items.Refresh();
            dataGridTermini.SelectedIndex = 0;
            radioZakazivanje.Content = "Zakazivanje";
            groupBoxOtkazivanje.Visibility = Visibility.Visible;
            groupBoxZakazivanje.Visibility = Visibility.Collapsed;
            radioOtkazivanje.Content = "";
        }
        private void radioZakazivanje_Checked(object sender, RoutedEventArgs e)
        {
            ResetujOtkazivanje();
            ResetujRasponZaSve();
            listViewTermini.Items.Refresh();
            dataGridTermini.SelectedIndex = 0;
            radioZakazivanje.Content = "";
            groupBoxOtkazivanje.Visibility = Visibility.Collapsed;
            groupBoxZakazivanje.Visibility = Visibility.Visible;
            radioOtkazivanje.Content = "Otkazivanje";
            SviRadnici();
        }
        private void buttonOtkazi_Click(object sender, RoutedEventArgs e)
        {
            PopUp pu = new PopUp();
            Korisnici kor = terminZaOtkaz.Korisnici;
            pu.label1.Content = "Da li zelite da otkazete termin:";
            pu.label2.Content = kor.Ime + " " + kor.Prezime;
            pu.label3.Content = terminZaOtkaz.DatumVreme.ToShortDateString() + " u " + terminZaOtkaz.DatumVreme.ToShortTimeString();
            pu.Left = this.Left + 750;
            pu.Top = this.Top + 340; 
            if (pu.ShowDialog() == true)
            {
                if (zDAL.IzbaciTermin(terminZaOtkaz.TerminID))
                {
                    tempOdabrani.Clear();
                    ResetujOtkazivanje();
                    if (radioSviRadnici.IsChecked == true)
                    {
                        SviRadnici();
                    }
                    else
                    {
                        JedanRadnik(odabraniRadnik.RadnikID);
                    }            
                }
            }
        }

    }
}
