namespace Repository
{
    using Repository.Entities;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class QuestionBaseDbContext : DbContext
    {
        public QuestionBaseDbContext()
            : base("QuestionBaseConnectionString")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer<QuestionBaseDbContext>(null);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<QuestionEntity>().HasKey(c => c.Id);
            modelBuilder.Entity<QuestionEntity>().Property(p => p.Title).HasColumnType("NVARCHAR").IsRequired();

            modelBuilder.Entity<QuestionTypeEntity>().HasKey(c => c.Id);
        }


        public DbSet<QuestionEntity> Question { get; set; }
        public DbSet<QuestionTypeEntity> QuestionType { get; set; }

    }
}
