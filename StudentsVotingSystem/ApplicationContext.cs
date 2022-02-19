using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Models
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user=root;database=StudentsVotingSystem;port=3306;password=Olaitan1991*");
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateVoter> CandidateVoters { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Voter> Voters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData
             (
                new Admin
                {
                    Id = 3,
                    FirstName = "Jaspa",
                    LastName = "Dembaba",
                    MatricNum = "ST003",
                    EmailAddress = "Dembaba@gmail.com",
                    PhoneNUmber = "08098987654"
                }
             );
        }

        public override DatabaseFacade Database => base.Database;
        public override ChangeTracker ChangeTracker => base.ChangeTracker;
        public override IModel Model => base.Model; 
        public override DbContextId ContextId => base.ContextId;
    }
}
