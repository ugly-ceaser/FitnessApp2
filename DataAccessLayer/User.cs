//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.FitnessGoals = new HashSet<FitnessGoal>();
            this.FitnessRecords = new HashSet<FitnessRecord>();
            this.Metrics = new HashSet<Metric>();
            this.UserWorkouts = new HashSet<UserWorkout>();
        }
    
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<FitnessGoal> FitnessGoals { get; set; }
        public virtual ICollection<FitnessRecord> FitnessRecords { get; set; }
        public virtual ICollection<Metric> Metrics { get; set; }
        public virtual ICollection<UserWorkout> UserWorkouts { get; set; }
    }
}
