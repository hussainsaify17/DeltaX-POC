﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeltaX.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DeltaXEntities : DbContext
    {
        public DeltaXEntities()
            : base("name=DeltaXEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Movies> Movies1 { get; set; }
        public virtual DbSet<MoviesActors> MoviesActors1 { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
    }
}
