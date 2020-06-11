using System;
using EFSemMigrations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSemMigrations.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
            
        public DbSet<Usuario> Usuario { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("Id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);                

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("date");
                    
                entity.Property(e => e.Ativo)
                    .HasDefaultValue(true);
                
                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Altura);
                   
            }); 
        }
    }
}