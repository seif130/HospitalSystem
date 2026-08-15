using HospitalSystem.Domain.Modules.Engagement.Complaint;
using HospitalSystem.Domain.Modules.Engagement.DoctorReview;
using HospitalSystem.Domain.Modules.Engagement.PatientFeedback;
using HospitalSystem.Domain.Modules.Engagement.Referral;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Contexts.DbContextsExtended
{
    public sealed class EngagementDbContext : DbContext
    {
        public EngagementDbContext(DbContextOptions<EngagementDbContext> options) : base(options) { }

        public DbSet<PatientFeedback> PatientFeedback => Set<PatientFeedback>();
        public DbSet<Complaint> Complaints => Set<Complaint>();
        public DbSet<Referral> Referrals => Set<Referral>();
        public DbSet<DoctorReview> DoctorReviews => Set<DoctorReview>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EngagementDbContext).Assembly);
    }
}
