using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext() { }



        #region DbSet<TEntity>
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillStatus> BillStatuses { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CommentBlog> CommentBlogs { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<DoHomework> DoHomeworks { get; set; }
        public virtual DbSet<LanguagePrograming> LanguageProgramings { get; set; }
        public virtual DbSet<LearningProgress> LearningProgresses { get; set; }
        public virtual DbSet<LikeBlog> LikeBlogs { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Practice> Practices { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RegisterStudy> RegisterStudies { get; set; }
        public virtual DbSet<RunTestCase> RunTestCases { get; set; }
        public virtual DbSet<Subject>  Subjects { get; set; }
        public virtual DbSet<SubjectDetail> SubjectsDetails { get; set; }
        public virtual DbSet<TestCase> TestCases { get; set; }

        #endregion


        #region implement interfaces
        public async Task<int> CommitChangeAsync()
        {
            return await SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        #endregion


        #region seed data and avoid multiple cascade paths 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Id = Guid.NewGuid(), RoleCode = "Admin", RoleName = "Administrator" },
            //    new Role { Id = Guid.NewGuid(), RoleCode = "Teacher", RoleName = "Teacher" },
            //    new Role { Id = Guid.NewGuid(), RoleCode = "User", RoleName = "User" });

            // Blog.CreatorId > User.Id
            modelBuilder.Entity<Blog>()
                .HasOne<User>(u => u.Creator) 
                .WithMany(b => b.Blogs)
                .HasForeignKey(b => b.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // CommentBlog.BlogId > Blog.Id
            modelBuilder.Entity<CommentBlog>()
                .HasOne<Blog>(cb => cb.Blog)
                .WithMany(c => c.CommentBlogs)
                .HasForeignKey(cb => cb.BlogId)
                .OnDelete(DeleteBehavior.Restrict);

            // CommentBlog.UserId > User.Id
            modelBuilder.Entity<CommentBlog>()
                .HasOne(cb => cb.User)
                .WithMany(c => c.CommentBlogs)
                .HasForeignKey(cb => cb.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // LikeBlog.BlogId > Blog.Id
            modelBuilder.Entity<LikeBlog>()
                .HasOne(lb => lb.Blog)
                .WithMany(c => c.LikeBlogs)
                .HasForeignKey(lb => lb.BlogId)
                .OnDelete(DeleteBehavior.Restrict);

            // LikeBlog.UserId > User.Id
            modelBuilder.Entity<LikeBlog>()
                .HasOne(lb => lb.User)
                .WithMany(c => c.LikeBlogs)
                .HasForeignKey(lb => lb.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bill>()
                .HasOne<User>(u => u.User)
                .WithMany(b => b.Bills)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterStudy>()
                .HasOne<User>(u => u.User)
                .WithMany(b => b.RegisterStudies)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoHomework>()
                .HasOne<User>(u => u.User)
                .WithMany(b => b.DoHomework)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LearningProgress>()
                    .HasOne<SubjectDetail>(u => u.SubjectDetail)
                    .WithMany(b => b.LearningProgresses)
                    .HasForeignKey(b => b.SubjectDetailId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne<User>(u => u.User)
                .WithMany(b => b.Questions)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .HasOne<User>(u => u.User)
                .WithMany(b => b.Answers)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoHomework>()
                .HasOne<RegisterStudy>(u => u.RegisterStudy)
                .WithMany(b => b.DoHomeworks)
                .HasForeignKey(b => b.RegisterStudyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TestCase>()
                .HasOne<Practice>(u => u.Practice)
                .WithMany(b => b.TestCases)
                .HasForeignKey(b => b.PracticeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RunTestCase>()
                .HasOne<TestCase>(u => u.TestCase)
                .WithMany(b => b.RunTestCases)
                .HasForeignKey(b => b.TestCaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Certificate>()
                .HasOne<User>(u => u.User)
                .WithMany(c => c.Certificates)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
                

        }
        #endregion
    }
}
