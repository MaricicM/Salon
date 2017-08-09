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
    /// Interaction logic for NoviKorisnik.xaml
    /// </summary>
    public partial class NoviKorisnik : Window
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        private List<Radnici> listaRadnika = new List<Radnici>();
        private List<Korisnici> listaKorisnika = new List<Korisnici>();
        public int a = 0;

        public NoviKorisnik()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //0 = Novi kor iz menija;   1 = Izmena kor iz menija;   2 = Novi kor iz comboa;   3 = Novi rad;  4 = Izmena rad
            if (a == 1 || a == 4)
            {
                buttonReset.Click += buttonOsvezi_Click;
                buttonUbaci.Click += buttonIzmeni_Click;
                buttonReset.Content = "Resetuj Podatke";
                buttonUbaci.IsEnabled = false;
                buttonReset.IsEnabled = false;

                if (a == 1)
                {
                    this.Title = "Izmena podataka o korisniku";
                    buttonUbaci.Content = "Izmeni podatke o korisniku";
                    listaKorisnika = zDAL.VratiSveKorisnike();
                    dataGridPrikaz.ItemsSource = listaKorisnika;
                }
                else
                {
                    this.Title = "Pregled Zaposlenih";
                    buttonUbaci.Content = "Izmeni podatke o radniku";
                    buttonNoviKorisnik.Content = "Novi radnik";
                    listaRadnika = zDAL.VratiSveRadnike();
                    dataGridPrikaz.ItemsSource = listaRadnika;
                    //comboBoxRadnoMesto.Items.Add("Administrator");
                    //comboBoxRadnoMesto.SelectedIndex = 0;
                    buttonOtkaz.Visibility = Visibility.Visible;
                    buttonIzbrisi.Visibility = Visibility.Collapsed;
                }                
            }
            else if (a == 3)
            {
                //comboBoxRadnoMesto.Items.Add("Administrator");
                //comboBoxRadnoMesto.SelectedIndex = 0;
                buttonReset.Click += buttonReset_Click;
                buttonUbaci.Click += buttonUbaci_Click;
                this.Width = 480;
                textBoxIme.Focus();
            }
            else
            {
                buttonReset.Click += buttonReset_Click;
                buttonUbaci.Click += buttonUbaci_Click;
                this.Width = 480;
                textBoxIme.Focus();
            }
        }

        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(textBoxIme.Text))
            {
                MessageBox.Show("Morate uneti ime");
                textBoxIme.Focus();
                return false;
            }
            if (textBoxIme.Text.Count() > 30)
            {
                MessageBox.Show("Ime ne sme imati preko 30 karaktera");
                textBoxIme.Focus();
                return false;
            }
            if (textBoxPrezime.Text.Count() > 30)
            {
                MessageBox.Show("Prezimeme ne sme imati preko 30 karaktera");
                textBoxPrezime.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxDan.Text) || string.IsNullOrWhiteSpace(textBoxMesec.Text) || string.IsNullOrWhiteSpace(textBoxGodina.Text))
            {
                MessageBox.Show("Neispravno unet datum");
                textBoxDan.Focus();
                return false;
            }
            if (int.Parse(textBoxDan.Text) < 1 || int.Parse(textBoxDan.Text) > 31 || int.Parse(textBoxMesec.Text) < 1 || int.Parse(textBoxMesec.Text) > 12 || int.Parse(textBoxGodina.Text) < 1900 || int.Parse(textBoxGodina.Text) > DateTime.Today.Year)
            {
                MessageBox.Show("Neispravno unet datum");
                textBoxDan.Focus();
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBoxTelefon.Text) && textBoxTelefon.Text.Count() < 5 || textBoxTelefon.Text.Count() > 30) 
            {
                MessageBox.Show("Telefonski broj mora imati izmedju 5 i 30 karaktera");
                textBoxTelefon.Focus();
                return false;
            }
            string mail = textBoxEmail.Text;
            if (!string.IsNullOrWhiteSpace(mail) && !mail.Contains('@') && !mail.Contains('.'))
            {
                MessageBox.Show("Nesipravno unet e-mail");
                textBoxEmail.Focus();
                return false;
            }
            if (textBoxEmail.Text.Count() > 50)
            {
                MessageBox.Show("E-mail ne sme imati preko 50 karaktera");
                textBoxEmail.Focus();
                return false;
            }
            if (a < 3 && a !=1)
            {
                if (zDAL.VratiSveKorisnike().Any(k => k.Ime == textBoxIme.Text && k.Prezime == textBoxPrezime.Text)) 
                {
                    MessageBox.Show("Postoji korisnik sa istim imenom i prezimenom!");
                    textBoxPrezime.Focus();
                    return false;
                }
                try
                {
                    DateTime dt = new DateTime(int.Parse(textBoxGodina.Text), int.Parse(textBoxMesec.Text), int.Parse(textBoxDan.Text));
                    if ((DateTime.Today.Year - dt.Year) < 12)
                    {
                        PopUp pu = new PopUp();
                        pu.Left = this.Left + 530;
                        pu.Top = this.Top + 340;
                        pu.label2.Content = dt.ToShortDateString();
                        if (pu.ShowDialog() == false)
                        {
                            textBoxDan.Focus();
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Neispravno unet datum");
                    textBoxDan.Focus();
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(textBoxTelefon.Text) && zDAL.VratiSveKorisnike().Any(k => k.Telefon == textBoxTelefon.Text))
                {
                    MessageBox.Show("Postoji korisnik sa unetim brojem telefona");
                    textBoxTelefon.Focus();
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(mail) && zDAL.VratiSveKorisnike().Any(k => k.Email == textBoxEmail.Text))
                {
                    MessageBox.Show("Posroji korisnik sa unetim e-mailom");
                    textBoxEmail.Focus();
                    return false;
                }
                if (textBoxAlergeni.Text.Count() > 500)
                {
                    MessageBox.Show("Tekst ne sme imati preko 500 karaktera");
                    textBoxAlergeni.Focus();
                    return false;
                }
                if (textBoxZabeleske.Text.Count() > 500)
                {
                    MessageBox.Show("Tekst ne sme imati preko 500 karaktera");
                    textBoxZabeleske.Focus();
                    return false;
                }
            }
            return true;
        }
        private void Resetuj ()
        {
            textBoxEmail.Clear();
            textBoxIme.Clear();
            textBoxPrezime.Clear();
            textBoxTelefon.Clear();
            if (a < 3)
            {
                textBoxAlergeni.Clear();
                textBoxZabeleske.Clear();
                textBoxDan.Clear();
                textBoxMesec.Clear();
                textBoxGodina.Clear();
                radioZenski.IsChecked = true;
                dataGridPrikaz.SelectedIndex = -1;
            }
            else
            {
                textBoxDan.Text = DateTime.Today.Day.ToString();
                textBoxMesec.Text = DateTime.Today.Month.ToString();
                textBoxGodina.Text = DateTime.Today.Year.ToString();
            }
        }
        private void OsveziPodatke()
        {
            if (a == 1)
            {
                Korisnici k = (Korisnici)dataGridPrikaz.SelectedItem;
                textBoxAlergeni.Text = k.PoznatiAlergeni;
                textBoxEmail.Text = k.Email;
                textBoxIme.Text = k.Ime;
                textBoxPrezime.Text = k.Prezime;
                textBoxTelefon.Text = k.Telefon;
                textBoxZabeleske.Text = k.Zabeleske;
                textBoxDan.Text = k.DatumRodjenja.Value.Day.ToString();
                textBoxMesec.Text = k.DatumRodjenja.Value.Month.ToString();
                textBoxGodina.Text = k.DatumRodjenja.Value.Year.ToString();
                if (k.Pol == 0)
                {
                    radioZenski.IsChecked = true;
                }
                else
                {
                    radioMuski.IsChecked = true;
                }
            }
            else
            {
                Radnici k = (Radnici)dataGridPrikaz.SelectedItem;
                textBoxEmail.Text = k.Email;
                textBoxIme.Text = k.Ime;
                textBoxPrezime.Text = k.Prezime;
                textBoxTelefon.Text = k.Telefon;
                textBoxDan.Text = k.DatumZaposlenja.Value.Day.ToString();
                textBoxMesec.Text = k.DatumZaposlenja.Value.Month.ToString();
                textBoxGodina.Text = k.DatumZaposlenja.Value.Year.ToString();
                labelRadnoMesto.Content = (k.Pozicija == 1) ? "Radnik" : "Administrator";
                //comboBoxRadnoMesto.SelectedIndex = k.Pozicija - 1;
            }
        }

        private void buttonUbaci_Click(object sender, RoutedEventArgs e)
        {
            if (!Validacija())
            {
                return;
            }
            DateTime dt = new DateTime(int.Parse(textBoxGodina.Text), int.Parse(textBoxMesec.Text), int.Parse(textBoxDan.Text));
            if (a == 3)
            {
                Radnici rad = new Radnici { Ime = textBoxIme.Text, Prezime = textBoxPrezime.Text, DatumZaposlenja = dt, Email = textBoxEmail.Text, Telefon = textBoxTelefon.Text, Pozicija = 2 };
                if (zDAL.UbaciRadnika(rad))
                {
                    MessageBox.Show("Radnik ubacen u bazu");
                    Resetuj();
                    textBoxIme.Focus();
                }
            }
            else
            {
                Korisnici k = new Korisnici { Ime = textBoxIme.Text, Prezime = textBoxPrezime.Text, DatumOtvaranjaDosijea = DateTime.Today, DatumRodjenja = dt, Email = textBoxEmail.Text, Pol = (radioZenski.IsChecked == true) ? 0 : 1, PoznatiAlergeni = textBoxAlergeni.Text, Telefon = textBoxTelefon.Text, Zabeleske = textBoxZabeleske.Text };
                if (zDAL.UbaciKorisnika(k))
                {
                    if (a == 2)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Korisnik ubacen u bazu");
                        Resetuj();
                        textBoxIme.Focus();
                    }
                }
            }
            
        }
        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (!Validacija())
            {
                return;
            }
            DateTime dt = new DateTime(int.Parse(textBoxGodina.Text), int.Parse(textBoxMesec.Text), int.Parse(textBoxDan.Text));
            if (a == 1)
            {
                Korisnici k = new Korisnici { KorisnikID = (dataGridPrikaz.SelectedItem as Korisnici).KorisnikID, Ime = textBoxIme.Text, Prezime = textBoxPrezime.Text, DatumOtvaranjaDosijea = DateTime.Today, DatumRodjenja = dt, Email = textBoxEmail.Text, Pol = (radioZenski.IsChecked == true) ? 0 : 1, PoznatiAlergeni = textBoxAlergeni.Text, Telefon = textBoxTelefon.Text, Zabeleske = textBoxZabeleske.Text };
                if (zDAL.IzmeniKorisnika(k))
                {
                    MessageBox.Show("Podaci izmenjeni");
                    OsveziPodatke();
                    textBoxIme.Focus();
                    dataGridPrikaz.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Greska pri cuvanju. Pokusajte ponovo ili kontaktirajte administratora.");
                }
            }
            else
            {
                Radnici r = dataGridPrikaz.SelectedItem as Radnici;
                r.Ime = textBoxIme.Text; r.Prezime = textBoxPrezime.Text; r.DatumZaposlenja = dt; r.Email = textBoxEmail.Text; r.Telefon = textBoxTelefon.Text;
                if (zDAL.IzmeniRadnika(r))
                {
                    MessageBox.Show("Podaci izmenjeni");
                    OsveziPodatke();
                    textBoxIme.Focus();
                    dataGridPrikaz.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Greska pri cuvanju. Pokusajte ponovo ili kontaktirajte administratora.");
                }
            }
        }
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            Resetuj();
        }
        private void buttonOsvezi_Click(object sender, RoutedEventArgs e)
        {
            OsveziPodatke();
        }

        private void textBoxIme_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxIme.Text.Count() > 30)
            {
                MessageBox.Show("Ime ne sme imati preko 30 karaktera");
                textBoxIme.Focus();
            }
        }
        private void textBoxPrezime_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxPrezime.Text.Count() > 30)
            {
                MessageBox.Show("Prezimeme ne sme imati preko 30 karaktera");
                textBoxPrezime.Focus();
            }
        }

        private void textBoxPrezime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                textBoxDan.Focus();
            }
        }
        private void textBoxDan_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBoxDan.Text.Length > 1)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                textBoxMesec.Focus();
            }
            textBoxDan.Text = textBoxDan.Text.Trim();
            textBoxDan.CaretIndex = textBoxDan.Text.Length;
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        private void textBoxMesec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                textBoxGodina.Focus();
            }
            if (textBoxMesec.Text.Length > 1)
            {
                e.Handled = true;
            }
            textBoxMesec.Text = textBoxMesec.Text.Trim();
            textBoxMesec.CaretIndex = textBoxMesec.Text.Length;
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        private void textBoxGodina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                textBoxTelefon.Focus();
            }
            if (textBoxGodina.Text.Length > 3)
            {
                e.Handled = true;
            }
            textBoxGodina.Text = textBoxGodina.Text.Trim();
            textBoxGodina.CaretIndex = textBoxGodina.Text.Length;
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }

        private void textBoxPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            string ulaz = textBoxPretraga.Text.Trim().ToLower();
            if (a == 1)
            {
                dataGridPrikaz.ItemsSource = listaKorisnika.Where(i => i.Ime.ToLower().Contains(ulaz) || i.Prezime.ToLower().Contains(ulaz));
            }
            else
            {
                dataGridPrikaz.ItemsSource = listaRadnika.Where(i => i.Ime.ToLower().Contains(ulaz) || i.Prezime.ToLower().Contains(ulaz));
            }
            
        }
        private void buttonNoviKorisnik_Click(object sender, RoutedEventArgs e)
        {
            NoviKorisnik nk = new NoviKorisnik();
            nk.a = (a == 1) ? 0 : 3;
            if (a==4)
            {
                nk.Title = "Novi Radnik";
                nk.labelPol.Content = "Radno mesto"; nk.radioMuski.Visibility = Visibility.Collapsed; nk.radioZenski.Visibility = Visibility.Collapsed; nk.labelRadnoMesto.Visibility = Visibility.Visible;
                nk.labelDatum.Content = "Datum zaposlenja:"; nk.textBoxDan.Text = DateTime.Today.Day.ToString(); nk.textBoxMesec.Text = DateTime.Today.Month.ToString(); nk.textBoxGodina.Text = DateTime.Today.Year.ToString();
                nk.labelAlergeni.Visibility = Visibility.Collapsed; nk.textBoxAlergeni.Visibility = Visibility.Collapsed;
                nk.labelZabeleske.Visibility = Visibility.Collapsed; nk.textBoxZabeleske.Visibility = Visibility.Collapsed;
                nk.buttonUbaci.Content = "Unesi novog radnika";
            }
            nk.ShowDialog();
            if (a == 1)
            {
                listaKorisnika = zDAL.VratiSveKorisnike();
                dataGridPrikaz.ItemsSource = listaKorisnika;
            }
            else
            {
                listaRadnika = zDAL.VratiSveRadnike();
                dataGridPrikaz.ItemsSource = listaRadnika;
            }
        }
        private void dataGridPrikaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridPrikaz.SelectedIndex > -1)
            {
                OsveziPodatke();
                buttonUbaci.IsEnabled = true;
                buttonReset.IsEnabled = true;
            }
            else
            {
                buttonUbaci.IsEnabled = false;
                buttonReset.IsEnabled = false;
            }
            if (a == 4)
            {
                Radnici ra = (Radnici)dataGridPrikaz.SelectedItem;
                if (ra == null || ra.Pozicija == 1 || ra.Ime == "N.") 
                {
                    buttonOtkaz.IsEnabled = false;
                }
                else
                {
                    buttonOtkaz.IsEnabled = true;
                }
            }
            else
            {
                buttonIzbrisi.IsEnabled = (dataGridPrikaz.SelectedIndex == -1) ? false : true;
            }
        }
        private void buttonPovratak_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonOtkaz_Click(object sender, RoutedEventArgs e)
        {
            Radnici rad = (Radnici)dataGridPrikaz.SelectedItem;
            PopUp pu = new PopUp();
            pu.label1.Content = "Da li zelite da otpustite radnika:";
            pu.label2.Content = rad.Ime + " " + rad.Prezime;
            pu.label3.Content = "";
            pu.Left = this.Left + 100;
            pu.Top = this.Top + 150;
            if (pu.ShowDialog() ==true)
            {
                if (zDAL.IzbaciRadnika(rad.RadnikID))
                {
                    MessageBox.Show("Radnik otpusten");
                    //Resetuj();
                    //listaRadnika = zDAL.VratiSveRadnike();
                    //dataGridPrikaz.ItemsSource = listaRadnika;
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo");
                }
            }
        }
        private void buttonIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            Korisnici kor = (Korisnici)dataGridPrikaz.SelectedItem;
            PopUp pu = new PopUp();
            pu.label1.Content = "Da li zelite da izbrisete korisnika:";
            pu.label2.Content = kor.Ime + " " + kor.Prezime;
            pu.label3.Content = "Bice izbrisani i svi termini ovog korisnika!";
            pu.Left = this.Left + 380;
            pu.Top = this.Top + 340;
            if (pu.ShowDialog() == true)
            {
                if (zDAL.IzbaciKorisnika(kor.KorisnikID))
                {
                    MessageBox.Show("Korisnik izbrisan");
                    Resetuj();
                    listaKorisnika = zDAL.VratiSveKorisnike();
                    dataGridPrikaz.ItemsSource = listaKorisnika;
                }
                else
                {
                    MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo");
                }
            }
        }
    }
}
