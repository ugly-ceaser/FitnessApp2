﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FitnessTrackerEntities : DbContext
    {
        public FitnessTrackerEntities()
            : base("name=FitnessTrackerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Activity> Activities { get; set; }
        public DbSet<FitnessGoal> FitnessGoals { get; set; }
        public DbSet<FitnessRecord> FitnessRecords { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
    }
}
