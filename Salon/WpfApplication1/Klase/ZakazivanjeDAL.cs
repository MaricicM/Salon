using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;

namespace WpfApplication1
{
    class ZakazivanjeDAL
    {
        Model1 db = new Model1();

        public bool UbaciKorisnika (Korisnici k)
        {            
            try
            {
                db.Korisnicis.Add(k);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(k).State = EntityState.Detached;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool UbaciTermin (Termini t)
        {
            try
            {
                db.Terminis.Add(t);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(t).State = EntityState.Detached;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool UbaciRadnika (Radnici r)
        {
            try
            {
                db.Radnicis.Add(r);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(r).State = EntityState.Detached;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool UbaciUslugu (Usluge i)
        {
            try
            {
                db.Usluges.Add(i);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(i).State = EntityState.Detached;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool UbaciPorudzbinu (Porudzbine p)
        {
            try
            {
                db.Porudzbines.Add(p);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(p).State = EntityState.Detached;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool UbaciDetaljePorudzbine (List<DetaljiPorudzbine> ldp)
        {
            try
            {
                foreach (DetaljiPorudzbine  dp in ldp)
                {
                    db.DetaljiPorudzbines.Add(dp);
                    db.Proizvodis.Find(dp.ProizvodID).Stanje -= dp.Kolicina;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                foreach (DetaljiPorudzbine dp in ldp)
                {
                    db.Entry(dp).State = EntityState.Detached;
                }
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool UbaciProizvod (Proizvodi p)
        {
            try
            {
                db.Proizvodis.Add(p);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(p).State = EntityState.Detached;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }

        public bool UnesiKolicinu (Proizvodi pro, int kolicina)
        {
            try
            {
                pro.Stanje += kolicina;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(pro).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }            
        }

        public bool IzmeniKorisnika (Korisnici k)
        {
            Korisnici kor = db.Korisnicis.Find(k.KorisnikID);
            try
            {
                kor.Ime = k.Ime; kor.Prezime = k.Prezime; kor.DatumRodjenja = k.DatumRodjenja; kor.Email = k.Email; kor.Pol = k.Pol;
                kor.PoznatiAlergeni = k.PoznatiAlergeni; kor.Telefon = k.Telefon; kor.Zabeleske = k.Zabeleske;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(kor).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool IzmeniRadnika (Radnici r)
        {
            Radnici rad = db.Radnicis.Find(r.RadnikID);
            try
            {
                rad.Ime = r.Ime; rad.Prezime = r.Prezime; rad.Pozicija = r.Pozicija; rad.Telefon = r.Telefon; rad.Email = r.Email; rad.DatumZaposlenja = r.DatumZaposlenja;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(rad).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool IzmeniUslugu(Usluge us)
        {
            Usluge i = db.Usluges.Find(us.UslugaID);
            try
            {
                i.Naziv = us.Naziv; i.Tip = us.Tip; i.Cena = us.Cena; i.Trajanje = us.Trajanje; i.SifraUsluge = us.SifraUsluge; i.Opis = us.Opis; i.Primedbe = us.Primedbe;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(i).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool IzmeniProizvod (Proizvodi pro)
        {
            Proizvodi p = db.Proizvodis.Find(pro.ProizvodID);
            try
            {
                p.Cena = pro.Cena; p.Kategorija = pro.Kategorija; p.Naziv = pro.Naziv; p.Proizvodjac = pro.Proizvodjac;
                p.SifraProizvoda = pro.SifraProizvoda; p.Stanje = pro.Stanje;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(pro).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }

        public bool IzbaciKorisnika(int id)
        {
            List<Porudzbine> listaPorudzbina = new List<Porudzbine>();
            Korisnici kor = db.Korisnicis.Find(id);
            try
            {
                listaPorudzbina = kor.Porudzbines.ToList();
                foreach (Porudzbine por in listaPorudzbina)
                {
                    db.DetaljiPorudzbines.RemoveRange(por.DetaljiPorudzbines);
                }
                db.Porudzbines.RemoveRange(listaPorudzbina);
                db.Terminis.RemoveRange(kor.Terminis);
                db.Korisnicis.Remove(kor);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                foreach (Termini t in kor.Terminis.ToList())
                {
                    db.Entry(t).State = EntityState.Unchanged;
                }
                foreach (Porudzbine p in listaPorudzbina)
                {
                    foreach (DetaljiPorudzbine dp in p.DetaljiPorudzbines.ToList())
                    {
                        db.Entry(dp).State = EntityState.Unchanged;
                    }
                    db.Entry(p).State = EntityState.Unchanged;
                }
                db.Entry(kor).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool IzbaciRadnika(int id)
        {
            List<Termini> lt = new List<Termini>();
            Radnici rad = db.Radnicis.Find(id);
            try
            {
                lt = db.Terminis.Where(t => t.AdministratorID == rad.RadnikID).ToList();
                foreach (Termini ter in lt)
                {
                    ter.AdministratorID = 4;
                }
                db.Radnicis.Remove(rad);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                foreach (Termini ter in lt)
                {
                    db.Entry(ter).State = EntityState.Unchanged;
                }
                db.Entry(rad).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool IzbaciUslugu(int id)
        {
            Usluge us = db.Usluges.Find(id);
            try
            {
                foreach (Termini ter in us.Terminis.ToList())
                {
                    db.Terminis.Remove(ter);
                }
                db.Usluges.Remove(us);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                foreach (Termini ter in us.Terminis.ToList())
                {
                    db.Entry(ter).State = EntityState.Unchanged;
                }
                db.Entry(us).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool IzbaciProizvod (int id)
        {
            Proizvodi p = new Proizvodi();
            try
            {
                p = db.Proizvodis.Find(id);
                foreach (DetaljiPorudzbine dp in p.DetaljiPorudzbines.ToList())
                {
                    db.DetaljiPorudzbines.Remove(dp);
                }
                db.Proizvodis.Remove(p);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                foreach (DetaljiPorudzbine dp in p.DetaljiPorudzbines.ToList())
                {
                    db.Entry(dp).State = EntityState.Unchanged;
                }
                db.Entry(p).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }
        public bool IzbaciTermin (int id)
        {
            Termini ter = new Termini();
            try
            {
                ter = db.Terminis.Find(id);
                db.Terminis.Remove(ter);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.Entry(ter).State = EntityState.Unchanged;
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return false;
            }
        }

        public List<Termini> VratiSveTermine()
        {
            try
            {
                return db.Terminis.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }
        }
        public List<Usluge> VratiSveUsluge()
        {
            try
            {
                return db.Usluges.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }            
        }
        public List<Korisnici> VratiSveKorisnike()
        {
            try
            {
                return db.Korisnicis.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }
        }
        public List<Radnici> VratiSveRadnike ()
        {
            try
            {
                return db.Radnicis.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }
        }
        public List<Proizvodi> VratiSveProizvode()
        {
            try
            {
                return db.Proizvodis.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }
        }

        public Korisnici VratiKorisnika(int id)
        {
            try
            {
                return db.Korisnicis.Find(id);
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }
        }
        public Usluge VratiUslugu(int id)
        {
            try
            {
                return db.Usluges.Find(id);
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }
        }

        public List<Termini> VratiTermine(int uslID)
        {
            try
            {
                return db.Terminis.Where(t => t.UslugaID == uslID).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Greska pri povezivanju sa bazom. Pokusajte ponovo ili kontaktirajte administartora.");
                return null;
            }
        }

    }
}
