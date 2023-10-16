using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;

namespace PeopleManager.Core
{
    public class PeopleManagerDbContext : IdentityDbContext
    {

        public PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People => Set<Person>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.ResponsiblePerson)
                .WithMany(p => p.ResponsibleForVehicles)
                .HasForeignKey(v => v.ResponsiblePersonId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {
            var bavoUserEmail = "bavo.ketels@vives.be";
            var bavoUser = new IdentityUser
            {
                Email = bavoUserEmail,
                NormalizedEmail = bavoUserEmail.ToUpper(),
                UserName = bavoUserEmail,
                NormalizedUserName = bavoUserEmail.ToUpper(),
                ConcurrencyStamp = "9d898cd7-2227-44ed-8fb2-3468d2145aae",
                SecurityStamp = "N7URV5X4PI2IFC3I32AVDCDC4Y7XSMAQ",
                PasswordHash =
                    "AQAAAAIAAYagAAAAEL5uZw6smTCysavHMSzc4In30igjGfcG7B6b3rFgDpiTl33+D7dbEjsEevWd2a6rIQ==", //Test123$
            };
            Users.Add(bavoUser);

            var bavoPerson = new Person
            {
                FirstName = "Bavo",
                LastName = "Ketels",
                Email = "bavo.ketels@vives.be",
                Description = "Lector"
            };
            var wimPerson = new Person
            {
                FirstName = "Wim",
                LastName = "Engelen",
                Email = "wim.engelen@vives.be",
                Description = "Opleidingshoofd"
            };

            People.AddRange(new List<Person>
            {
                bavoPerson,
                new Person{FirstName = "Isabelle", LastName = "Vandoorne", Email = "isabelle.vandoorne@vives.be" },
                wimPerson,
                new Person{FirstName = "Ebe", LastName = "Deketelaere", Email = "ebe.deketelaere@vives.be" }
            });

            Vehicles.AddRange(new[] {
                new Vehicle{LicensePlate = "1-ABC-123", ResponsiblePerson = bavoPerson},
                new Vehicle{LicensePlate = "THE_BOSS", Brand= "Ferrari", Type="448", ResponsiblePerson = wimPerson},
                new Vehicle{LicensePlate = "SALES_GUY_1", Brand= "Audi", Type="e-tron", ResponsiblePerson = bavoPerson},
                new Vehicle{LicensePlate = "DESK_1", Brand= "Fiat", Type="Punto", ResponsiblePerson = bavoPerson}});

            SaveChanges();
        }
    }
}
