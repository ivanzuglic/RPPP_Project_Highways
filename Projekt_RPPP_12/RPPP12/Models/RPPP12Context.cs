using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RPPP12.Models
{
    public partial class RPPP12Context : DbContext
    {
        public RPPP12Context()
        {
        }

        public RPPP12Context(DbContextOptions<RPPP12Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Alarm> Alarm { get; set; }
        public virtual DbSet<Autocesta> Autocesta { get; set; }
        public virtual DbSet<Cjenik> Cjenik { get; set; }
        public virtual DbSet<Dionica> Dionica { get; set; }
        public virtual DbSet<Dogadaj> Dogadaj { get; set; }
        public virtual DbSet<KategorijaScenarija> KategorijaScenarija { get; set; }
        public virtual DbSet<KategorijaVozila> KategorijaVozila { get; set; }
        public virtual DbSet<LokacijaAutoceste> LokacijaAutoceste { get; set; }
        public virtual DbSet<LokacijaPostaje> LokacijaPostaje { get; set; }
        public virtual DbSet<NacinPlacanja> NacinPlacanja { get; set; }
        public virtual DbSet<NaplatnaKucica> NaplatnaKucica { get; set; }
        public virtual DbSet<NaplatnaPostaja> NaplatnaPostaja { get; set; }
        public virtual DbSet<Objekt> Objekt { get; set; }
        public virtual DbSet<Racun> Racun { get; set; }
        public virtual DbSet<RazinaOpasnosti> RazinaOpasnosti { get; set; }
        public virtual DbSet<Scenarij> Scenarij { get; set; }
        public virtual DbSet<Sjediste> Sjediste { get; set; }
        public virtual DbSet<Stanje> Stanje { get; set; }
        public virtual DbSet<SustavNaplate> SustavNaplate { get; set; }
        public virtual DbSet<Upravitelj> Upravitelj { get; set; }
        public virtual DbSet<Uredaj> Uredaj { get; set; }
        public virtual DbSet<VrstaNaplatneKucice> VrstaNaplatneKucice { get; set; }
        public virtual DbSet<VrstaObjekta> VrstaObjekta { get; set; }
        public virtual DbSet<VrstaUredaja> VrstaUredaja { get; set; }
        public virtual DbSet<VrstaZaposlenika> VrstaZaposlenika { get; set; }
        public virtual DbSet<Zabrana> Zabrana { get; set; }
        public virtual DbSet<Zaposlenik> Zaposlenik { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=rppp.fer.hr,3000;Database=RPPP12;User Id=rppp12;Password=snoopy#012");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alarm>(entity =>
            {
                entity.HasKey(e => new { e.SifraUredaja, e.SifraDogadaja });

                entity.Property(e => e.SifraUredaja).HasColumnName("sifraUredaja");

                entity.Property(e => e.SifraDogadaja).HasColumnName("sifraDogadaja");

                entity.Property(e => e.SifraOperatera).HasColumnName("sifraOperatera");

                entity.Property(e => e.SifraScenarija).HasColumnName("sifraScenarija");

                entity.HasOne(d => d.SifraDogadajaNavigation)
                    .WithMany(p => p.Alarm)
                    .HasForeignKey(d => d.SifraDogadaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alarm_Dogadaj");

                entity.HasOne(d => d.SifraScenarijaNavigation)
                    .WithMany(p => p.Alarm)
                    .HasForeignKey(d => d.SifraScenarija)
                    .HasConstraintName("FK_Alarm_Scenarij");

                entity.HasOne(d => d.SifraUredajaNavigation)
                    .WithMany(p => p.Alarm)
                    .HasForeignKey(d => d.SifraUredaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alarm_Uredaj");
            });

            modelBuilder.Entity<Autocesta>(entity =>
            {
                entity.HasKey(e => e.SifraAutoceste);

                entity.HasIndex(e => e.ImeAutoceste)
                    .HasName("IX_Autocesta")
                    .IsUnique();

                entity.Property(e => e.SifraAutoceste)
                    .HasColumnName("sifraAutoceste")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImeAutoceste)
                    .IsRequired()
                    .HasColumnName("imeAutoceste")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Kilometraza).HasColumnName("kilometraza");

                entity.Property(e => e.Nadimak)
                    .HasColumnName("nadimak")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SifraNacinaPlacanja).HasColumnName("sifraNacinaPlacanja");

                entity.Property(e => e.SifraPocetka).HasColumnName("sifraPocetka");

                entity.Property(e => e.SifraUpravitelja).HasColumnName("sifraUpravitelja");

                entity.Property(e => e.SifraZavrsetka).HasColumnName("sifraZavrsetka");

                entity.HasOne(d => d.SifraNacinaPlacanjaNavigation)
                    .WithMany(p => p.Autocesta)
                    .HasForeignKey(d => d.SifraNacinaPlacanja)
                    .HasConstraintName("FK_Autocesta_SustavNaplate");

                entity.HasOne(d => d.SifraPocetkaNavigation)
                    .WithMany(p => p.AutocestaSifraPocetkaNavigation)
                    .HasForeignKey(d => d.SifraPocetka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autocesta_LokacijaAutoceste");

                entity.HasOne(d => d.SifraUpraviteljaNavigation)
                    .WithMany(p => p.Autocesta)
                    .HasForeignKey(d => d.SifraUpravitelja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autocesta_Upravitelj");

                entity.HasOne(d => d.SifraZavrsetkaNavigation)
                    .WithMany(p => p.AutocestaSifraZavrsetkaNavigation)
                    .HasForeignKey(d => d.SifraZavrsetka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autocesta_LokacijaAutoceste1");
            });

            modelBuilder.Entity<Cjenik>(entity =>
            {
                entity.HasKey(e => new { e.SifraKucica, e.SifraKategorijaVozila });

                entity.Property(e => e.SifraKucica).HasColumnName("sifraKucica");

                entity.Property(e => e.SifraKategorijaVozila).HasColumnName("sifraKategorijaVozila");

                entity.Property(e => e.Cijena).HasColumnName("cijena");

                entity.HasOne(d => d.SifraKategorijaVozilaNavigation)
                    .WithMany(p => p.Cjenik)
                    .HasForeignKey(d => d.SifraKategorijaVozila)
                    .HasConstraintName("FK_Cjenik_KategorijaVozila");

                entity.HasOne(d => d.SifraKucicaNavigation)
                    .WithMany(p => p.Cjenik)
                    .HasForeignKey(d => d.SifraKucica)
                    .HasConstraintName("FK_Cjenik_NaplatnaKucica");
            });

            modelBuilder.Entity<Dionica>(entity =>
            {
                entity.HasKey(e => e.SifraDionice);

                entity.Property(e => e.SifraDionice).HasColumnName("sifraDionice");

                entity.Property(e => e.Duljina).HasColumnName("duljina");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnName("naziv")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SifraAutoceste).HasColumnName("sifraAutoceste");

                entity.Property(e => e.SifraKraja).HasColumnName("sifraKraja");

                entity.Property(e => e.SifraPocetka).HasColumnName("sifraPocetka");

                entity.HasOne(d => d.SifraAutocesteNavigation)
                    .WithMany(p => p.Dionica)
                    .HasForeignKey(d => d.SifraAutoceste)
                    .HasConstraintName("FK_Dionica_Autocesta");

                entity.HasOne(d => d.SifraKrajaNavigation)
                    .WithMany(p => p.DionicaSifraKrajaNavigation)
                    .HasForeignKey(d => d.SifraKraja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dionica_LokacijaPostaje1");

                entity.HasOne(d => d.SifraPocetkaNavigation)
                    .WithMany(p => p.DionicaSifraPocetkaNavigation)
                    .HasForeignKey(d => d.SifraPocetka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dionica_LokacijaPostaje");
            });

            modelBuilder.Entity<Dogadaj>(entity =>
            {
                entity.HasKey(e => e.SifraDogadaj);

                entity.Property(e => e.SifraDogadaj).HasColumnName("sifraDogadaj");

                entity.Property(e => e.DatumVrijeme)
                    .IsRequired()
                    .HasColumnName("datumVrijeme")
                    .IsRowVersion();

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SifraDionica).HasColumnName("sifraDionica");

                entity.Property(e => e.SifraRazinaOpasnosti).HasColumnName("sifraRazinaOpasnosti");

                entity.HasOne(d => d.SifraDionicaNavigation)
                    .WithMany(p => p.Dogadaj)
                    .HasForeignKey(d => d.SifraDionica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dogadaj_Dionica");

                entity.HasOne(d => d.SifraRazinaOpasnostiNavigation)
                    .WithMany(p => p.Dogadaj)
                    .HasForeignKey(d => d.SifraRazinaOpasnosti)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dogadaj_RazinaOpasnosti");
            });

            modelBuilder.Entity<KategorijaScenarija>(entity =>
            {
                entity.HasKey(e => e.SifraKategorijeScenarija);

                entity.Property(e => e.SifraKategorijeScenarija).HasColumnName("sifraKategorijeScenarija");

                entity.Property(e => e.NazivKategorijeScenarija)
                    .IsRequired()
                    .HasColumnName("nazivKategorijeScenarija")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KategorijaVozila>(entity =>
            {
                entity.HasKey(e => e.SifraKategorijaVozila);

                entity.Property(e => e.SifraKategorijaVozila).HasColumnName("sifraKategorijaVozila");

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasColumnName("opis")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Oznaka).HasColumnName("oznaka");
            });

            modelBuilder.Entity<LokacijaAutoceste>(entity =>
            {
                entity.HasKey(e => e.SifraLokacije);

                entity.Property(e => e.SifraLokacije)
                    .HasColumnName("sifraLokacije")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImeLokacije)
                    .IsRequired()
                    .HasColumnName("imeLokacije")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LokacijaPostaje>(entity =>
            {
                entity.HasKey(e => e.SifraLokacije);

                entity.Property(e => e.SifraLokacije).HasColumnName("sifraLokacije");

                entity.Property(e => e.NazivLokacije)
                    .IsRequired()
                    .HasColumnName("nazivLokacije")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NacinPlacanja>(entity =>
            {
                entity.HasKey(e => e.SifraNacinPlacanja);

                entity.Property(e => e.SifraNacinPlacanja).HasColumnName("sifraNacinPlacanja");

                entity.Property(e => e.NacinPlacanja1)
                    .IsRequired()
                    .HasColumnName("nacinPlacanja")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NaplatnaKucica>(entity =>
            {
                entity.HasKey(e => e.SifraKucica);

                entity.Property(e => e.SifraKucica).HasColumnName("sifraKucica");

                entity.Property(e => e.SifraBlagajnika).HasColumnName("sifraBlagajnika");

                entity.Property(e => e.SifraPostaja).HasColumnName("sifraPostaja");

                entity.Property(e => e.VrstaNaplatneKucice)
                    .HasColumnName("vrstaNaplatneKucice")
                    .HasMaxLength(50);

                entity.HasOne(d => d.SifraBlagajnikaNavigation)
                    .WithMany(p => p.NaplatnaKucica)
                    .HasForeignKey(d => d.SifraBlagajnika)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_NaplatnaKucica_Zaposlenik");

                entity.HasOne(d => d.SifraPostajaNavigation)
                    .WithMany(p => p.NaplatnaKucica)
                    .HasForeignKey(d => d.SifraPostaja)
                    .HasConstraintName("FK_NaplatnaKucica_NaplatnaPostaja");

                entity.HasOne(d => d.VrstaNaplatneKuciceNavigation)
                    .WithMany(p => p.NaplatnaKucica)
                    .HasForeignKey(d => d.VrstaNaplatneKucice)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_NaplatnaKucica_VrstaNaplatneKucice");
            });

            modelBuilder.Entity<NaplatnaPostaja>(entity =>
            {
                entity.HasKey(e => e.SifraPostaje);

                entity.Property(e => e.SifraPostaje).HasColumnName("sifraPostaje");

                entity.Property(e => e.ImePostaje)
                    .HasColumnName("imePostaje")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SifraDionice).HasColumnName("sifraDionice");

                entity.Property(e => e.SifraLokacijePostaje).HasColumnName("sifraLokacijePostaje");

                entity.HasOne(d => d.SifraDioniceNavigation)
                    .WithMany(p => p.NaplatnaPostaja)
                    .HasForeignKey(d => d.SifraDionice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NaplatnaPostaja_Dionica");

                entity.HasOne(d => d.SifraLokacijePostajeNavigation)
                    .WithMany(p => p.NaplatnaPostaja)
                    .HasForeignKey(d => d.SifraLokacijePostaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NaplatnaPostaja_LokacijaPostaje");
            });

            modelBuilder.Entity<Objekt>(entity =>
            {
                entity.HasKey(e => e.SifraObjekta);

                entity.Property(e => e.SifraObjekta).HasColumnName("sifraObjekta");

                entity.Property(e => e.SifraDionice).HasColumnName("sifraDionice");

                entity.Property(e => e.SifraVrstaObjekta).HasColumnName("sifraVrstaObjekta");

                entity.HasOne(d => d.SifraDioniceNavigation)
                    .WithMany(p => p.Objekt)
                    .HasForeignKey(d => d.SifraDionice)
                    .HasConstraintName("FK_Objekt_Dionica");

                entity.HasOne(d => d.SifraVrstaObjektaNavigation)
                    .WithMany(p => p.Objekt)
                    .HasForeignKey(d => d.SifraVrstaObjekta)
                    .HasConstraintName("FK_Objekt_VrstaObjekta");
            });

            modelBuilder.Entity<Racun>(entity =>
            {
                entity.HasKey(e => e.SifraRacun);

                entity.Property(e => e.SifraRacun).HasColumnName("sifraRacun");

                entity.Property(e => e.DatumVrijeme)
                    .IsRequired()
                    .HasColumnName("datumVrijeme")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistarskaOznaka)
                    .IsRequired()
                    .HasColumnName("registarskaOznaka")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SifraKategorijaVozila).HasColumnName("sifraKategorijaVozila");

                entity.Property(e => e.SifraKucica).HasColumnName("sifraKucica");

                entity.Property(e => e.SifraNacinPlacanja).HasColumnName("sifraNacinPlacanja");

                entity.HasOne(d => d.SifraKategorijaVozilaNavigation)
                    .WithMany(p => p.Racun)
                    .HasForeignKey(d => d.SifraKategorijaVozila)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Racun_KategorijaVozila");

                entity.HasOne(d => d.SifraKucicaNavigation)
                    .WithMany(p => p.Racun)
                    .HasForeignKey(d => d.SifraKucica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Racun_NaplatnaKucica");

                entity.HasOne(d => d.SifraNacinPlacanjaNavigation)
                    .WithMany(p => p.Racun)
                    .HasForeignKey(d => d.SifraNacinPlacanja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Racun_NacinPlacanja");
            });

            modelBuilder.Entity<RazinaOpasnosti>(entity =>
            {
                entity.HasKey(e => e.SifraRazinaOpasnosti);

                entity.Property(e => e.SifraRazinaOpasnosti).HasColumnName("sifraRazinaOpasnosti");

                entity.Property(e => e.NazivRazinaOpasnosti)
                    .IsRequired()
                    .HasColumnName("nazivRazinaOpasnosti")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Scenarij>(entity =>
            {
                entity.HasKey(e => e.SifraScenarija);

                entity.Property(e => e.SifraScenarija).HasColumnName("sifraScenarija");

                entity.Property(e => e.NazivScenarija)
                    .IsRequired()
                    .HasColumnName("nazivScenarija")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Procedura)
                    .HasColumnName("procedura")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SifraVrsteObjekta).HasColumnName("sifraVrsteObjekta");

                entity.Property(e => e.SifraVrsteScenarija).HasColumnName("sifraVrsteScenarija");

                entity.HasOne(d => d.SifraVrsteScenarijaNavigation)
                    .WithMany(p => p.Scenarij)
                    .HasForeignKey(d => d.SifraVrsteScenarija)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Scenarij_KategorijaScenarija");
            });

            modelBuilder.Entity<Sjediste>(entity =>
            {
                entity.HasKey(e => e.SifraSjedista);

                entity.Property(e => e.SifraSjedista).HasColumnName("sifraSjedista");

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasColumnName("adresa")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImeSjedista)
                    .IsRequired()
                    .HasColumnName("imeSjedista")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stanje>(entity =>
            {
                entity.HasKey(e => e.SifraStanje);

                entity.Property(e => e.SifraStanje).HasColumnName("sifraStanje");

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SifraDogadaj).HasColumnName("sifraDogadaj");

                entity.Property(e => e.SifraZabrana).HasColumnName("sifraZabrana");

                entity.Property(e => e.VrijemePocetka)
                    .HasColumnName("vrijemePocetka")
                    .IsRowVersion();

                entity.Property(e => e.VrijemeZavrsetka).HasColumnName("vrijemeZavrsetka");

                entity.HasOne(d => d.SifraDogadajNavigation)
                    .WithMany(p => p.Stanje)
                    .HasForeignKey(d => d.SifraDogadaj)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Stanje_Dogadaj");

                entity.HasOne(d => d.SifraZabranaNavigation)
                    .WithMany(p => p.Stanje)
                    .HasForeignKey(d => d.SifraZabrana)
                    .HasConstraintName("FK_Stanje_Zabrana");
            });

            modelBuilder.Entity<SustavNaplate>(entity =>
            {
                entity.HasKey(e => e.SifraNacinaPlacanja);

                entity.Property(e => e.SifraNacinaPlacanja).HasColumnName("sifraNacinaPlacanja");

                entity.Property(e => e.NacinPlacanja)
                    .IsRequired()
                    .HasColumnName("nacinPlacanja")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Upravitelj>(entity =>
            {
                entity.HasKey(e => e.SifraUpravitelja);

                entity.Property(e => e.SifraUpravitelja).HasColumnName("sifraUpravitelja");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasColumnName("ime")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Oib).HasColumnName("OIB");

                entity.Property(e => e.SifraSjedista).HasColumnName("sifraSjedista");

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnName("telefon")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SifraSjedistaNavigation)
                    .WithMany(p => p.Upravitelj)
                    .HasForeignKey(d => d.SifraSjedista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Upravitelj_Sjediste");
            });

            modelBuilder.Entity<Uredaj>(entity =>
            {
                entity.HasKey(e => e.SifraUredaja);

                entity.Property(e => e.SifraUredaja).HasColumnName("sifraUredaja");

                entity.Property(e => e.SifraObjekta).HasColumnName("sifraObjekta");

                entity.Property(e => e.SifraVrsteUredaja).HasColumnName("sifraVrsteUredaja");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SifraObjektaNavigation)
                    .WithMany(p => p.Uredaj)
                    .HasForeignKey(d => d.SifraObjekta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Uredaj_Objekt");

                entity.HasOne(d => d.SifraVrsteUredajaNavigation)
                    .WithMany(p => p.Uredaj)
                    .HasForeignKey(d => d.SifraVrsteUredaja)
                    .HasConstraintName("FK_Uredaj_VrstaUredaja");
            });

            modelBuilder.Entity<VrstaNaplatneKucice>(entity =>
            {
                entity.HasKey(e => e.VrstaNaplatneKucice1);

                entity.Property(e => e.VrstaNaplatneKucice1)
                    .HasColumnName("vrstaNaplatneKucice")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<VrstaObjekta>(entity =>
            {
                entity.HasKey(e => e.SifraVrsteObjekta);

                entity.Property(e => e.SifraVrsteObjekta).HasColumnName("sifraVrsteObjekta");

                entity.Property(e => e.NazivObjekta)
                    .IsRequired()
                    .HasColumnName("nazivObjekta")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VrstaUredaja>(entity =>
            {
                entity.HasKey(e => e.SifraVrsteUredaja);

                entity.Property(e => e.SifraVrsteUredaja).HasColumnName("sifraVrsteUredaja");

                entity.Property(e => e.NazivVrsteUredaja)
                    .HasColumnName("nazivVrsteUredaja")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VrstaZaposlenika>(entity =>
            {
                entity.HasKey(e => e.SifraVrsteZaposlenika);

                entity.Property(e => e.SifraVrsteZaposlenika).HasColumnName("sifraVrsteZaposlenika");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnName("naziv")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Zabrana>(entity =>
            {
                entity.HasKey(e => e.SifraZabrana);

                entity.Property(e => e.SifraZabrana).HasColumnName("sifraZabrana");

                entity.Property(e => e.VrstaZabrane).HasColumnName("vrstaZabrane");
            });

            modelBuilder.Entity<Zaposlenik>(entity =>
            {
                entity.HasKey(e => e.SifraZaposlenika);

                entity.Property(e => e.SifraZaposlenika).HasColumnName("sifraZaposlenika");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasColumnName("ime")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasColumnName("prezime")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SifraPostaje).HasColumnName("sifraPostaje");

                entity.Property(e => e.SifraVrsteZaposlenika).HasColumnName("sifraVrsteZaposlenika");

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnName("telefon")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SifraPostajeNavigation)
                    .WithMany(p => p.Zaposlenik)
                    .HasForeignKey(d => d.SifraPostaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zaposlenik_NaplatnaPostaja");

                entity.HasOne(d => d.SifraVrsteZaposlenikaNavigation)
                    .WithMany(p => p.Zaposlenik)
                    .HasForeignKey(d => d.SifraVrsteZaposlenika)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zaposlenik_VrstaZaposlenika");
            });
        }
    }
}
