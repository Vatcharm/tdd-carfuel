using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.Models;

namespace CarFuel.DataAccess.Context {
    public class CarDb : DbContext {
        public DbSet<Car> Cars { get; set; }
        public DbSet<FillUp> FillUps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        modelBuilder.Entity<FillUp>()
            .HasOptional(f => f.NextFillUp)
            .WithOptionalPrincipal(x => x.PreviousFillUp);
            }
        }
    }
