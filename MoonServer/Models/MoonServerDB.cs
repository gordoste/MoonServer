namespace MoonServer.Models
{
    using System.Data.Entity;

    public partial class MoonServerDB : DbContext
    {
        public MoonServerDB()
            : base("name=MoonServerDB")
        {
        }

        public virtual DbSet<EndProblemPosition> EndProblemPositions { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<HoldPlacement> HoldPlacements { get; set; }
        public virtual DbSet<Hold> Holds { get; set; }
        public virtual DbSet<HoldSetupHoldPlacement> HoldSetupHoldPlacements { get; set; }
        public virtual DbSet<HoldSetup> HoldSetups { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<ProblemPosition> ProblemPositions { get; set; }
        public virtual DbSet<Problem> Problems { get; set; }
        public virtual DbSet<StartProblemPosition> StartProblemPositions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .HasMany(e => e.Problems)
                .WithRequired(e => e.Grade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoldPlacement>()
                .HasMany(e => e.HoldSetupHoldPlacements)
                .WithRequired(e => e.HoldPlacement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hold>()
                .HasMany(e => e.HoldPlacements)
                .WithRequired(e => e.Hold)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoldSetup>()
                .HasMany(e => e.HoldSetupHoldPlacements)
                .WithRequired(e => e.HoldSetup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoldSetup>()
                .HasMany(e => e.Problems)
                .WithRequired(e => e.HoldSetup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.EndProblemPositions)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.HoldPlacements)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.ProblemPositions)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.StartProblemPositions)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Problem>()
                .HasMany(e => e.EndProblemPositions)
                .WithRequired(e => e.Problem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Problem>()
                .HasMany(e => e.ProblemPositions)
                .WithRequired(e => e.Problem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Problem>()
                .HasMany(e => e.StartProblemPositions)
                .WithRequired(e => e.Problem)
                .WillCascadeOnDelete(false);
        }
    }

    public enum Orientation { N, NE, E, SE, S, SW, W, NW };
    public enum Holdset { Original, SetA, SetB, SetC, Wooden };
}
