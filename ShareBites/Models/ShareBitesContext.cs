using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShareBites.Models
{
    public partial class ShareBitesContext : DbContext
    {
        public ShareBitesContext()
        {
        }

        public ShareBitesContext(DbContextOptions<ShareBitesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExcessFoodOrder> ExcessFoodOrders { get; set; } = null!;
        public virtual DbSet<Helper> Helpers { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<ModeOfhelpMaster> ModeOfhelpMasters { get; set; } = null!;
        public virtual DbSet<RegionMaster> RegionMasters { get; set; } = null!;
        public virtual DbSet<ResFoodHandler> ResFoodHandlers { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Shelter> Shelters { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;
        public virtual DbSet<SponsoredFood> SponsoredFoods { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<UserTypeMaster> UserTypeMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=ShareBites;Integrated Security=SSPI;trustServerCertificate=yes; user id=LAPTOP-HT5M2911\\\\\\\\sarat;Trusted_Connection=True; MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExcessFoodOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__ExcessFo__C3905BAF1AEDD79B");

                entity.HasIndex(e => e.FoodId, "Uniquefood")
                    .IsUnique();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.HelperId).HasColumnName("Helper_ID");

                entity.Property(e => e.ModeOfdelivery)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ShelterId).HasColumnName("ShelterID");

                entity.HasOne(d => d.Food)
                    .WithOne(p => p.ExcessFoodOrder)
                    .HasForeignKey<ExcessFoodOrder>(d => d.FoodId)
                    .HasConstraintName("FK__ExcessFoo__FoodI__440B1D61");

                entity.HasOne(d => d.Helper)
                    .WithMany(p => p.ExcessFoodOrders)
                    .HasForeignKey(d => d.HelperId)
                    .HasConstraintName("FK__ExcessFoo__Helpe__45F365D3");

                entity.HasOne(d => d.Shelter)
                    .WithMany(p => p.ExcessFoodOrders)
                    .HasForeignKey(d => d.ShelterId)
                    .HasConstraintName("FK__ExcessFoo__Shelt__44FF419A");
            });

            modelBuilder.Entity<Helper>(entity =>
            {
                entity.Property(e => e.HelperId).HasColumnName("Helper_ID");

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsUnicode(false)
                    .HasColumnName("Email_ID");

                entity.Property(e => e.LoginId).HasColumnName("login_ID");

                entity.Property(e => e.ModeOfHelp)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Mode_of_help");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RegionID");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Zip_code");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Helpers)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Helpers__login_I__37A5467C");

                entity.HasOne(d => d.ModeOfHelpNavigation)
                    .WithMany(p => p.Helpers)
                    .HasForeignKey(d => d.ModeOfHelp)
                    .HasConstraintName("FK__Helpers__Mode_of__38996AB5");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Helpers)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK__Helpers__login_I__36B12243");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.Property(e => e.MessageId).HasColumnName("messageID");

                entity.Property(e => e.FromUserId).HasColumnName("FromUserID");

                entity.Property(e => e.Message1)
                    .IsUnicode(false)
                    .HasColumnName("Message");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.MessageFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("FK__messages__IsAck__4E88ABD4");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.MessageToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("FK__messages__ToUser__4F7CD00D");
            });

            modelBuilder.Entity<ModeOfhelpMaster>(entity =>
            {
                entity.HasKey(e => e.HelpId)
                    .HasName("PK__ModeOFHe__90E3232E259155CB");

                entity.ToTable("ModeOFHelp_Master");

                entity.Property(e => e.HelpId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("HelpID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ModeOfHelp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Mode_of_help");
            });

            modelBuilder.Entity<RegionMaster>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("PK__Region_M__ACD844431E41566C");

                entity.ToTable("Region_Master");

                entity.Property(e => e.RegionId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RegionID");

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Province)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ResFoodHandler>(entity =>
            {
                entity.HasKey(e => e.FoodId)
                    .HasName("PK__Res_Food__856DB3CBAEEFA8A8");

                entity.ToTable("Res_FoodHandler");

                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.DateAndTime).HasColumnType("datetime");

                entity.Property(e => e.FoodDesc).IsUnicode(false);

                entity.Property(e => e.FoodStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Food_status");

                entity.Property(e => e.IsExcess).HasColumnName("isExcess");

                entity.Property(e => e.ResId).HasColumnName("Res_ID");

                entity.Property(e => e.WaitingTime).HasColumnName("Waiting_time");

                entity.HasOne(d => d.Res)
                    .WithMany(p => p.ResFoodHandlers)
                    .HasForeignKey(d => d.ResId)
                    .HasConstraintName("FK__Res_FoodH__isExc__403A8C7D");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.ResId)
                    .HasName("PK__Restaura__11B93545957432E9");

                entity.Property(e => e.ResId).HasColumnName("Res_ID");

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.ClosingTime).HasColumnName("Closing_time");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsUnicode(false)
                    .HasColumnName("Email_ID");

                entity.Property(e => e.LoginId).HasColumnName("login_ID");

                entity.Property(e => e.ModeOfHelp)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Mode_of_help");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RegionID");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Registration_Date");

                entity.Property(e => e.WaitTime).HasColumnName("Wait_time");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Restauran__login__32E0915F");

                entity.HasOne(d => d.ModeOfHelpNavigation)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.ModeOfHelp)
                    .HasConstraintName("FK__Restauran__Mode___33D4B598");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK__Restauran__login__31EC6D26");
            });

            modelBuilder.Entity<Shelter>(entity =>
            {
                entity.ToTable("Shelter");

                entity.Property(e => e.ShelterId).HasColumnName("ShelterID");

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.LoginId).HasColumnName("login_ID");

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

                entity.Property(e => e.RegionId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RegionID");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Registration_Date");

                entity.Property(e => e.ShelterName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_code");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Shelters)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Shelter__login_I__2F10007B");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Shelters)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK__Shelter__login_I__2E1BDC42");
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.ToTable("sponsors");

                entity.Property(e => e.SponsorId).HasColumnName("Sponsor_ID");

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsUnicode(false)
                    .HasColumnName("Email_ID");

                entity.Property(e => e.LoginId).HasColumnName("login_ID");

                entity.Property(e => e.ModeOfHelp1)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Mode_of_help");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RegionID");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Sponsors)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__sponsors__login___3C69FB99");

                entity.HasOne(d => d.ModeOfHelp1Navigation)
                    .WithMany(p => p.Sponsors)
                    .HasForeignKey(d => d.ModeOfHelp1)
                    .HasConstraintName("FK__sponsors__Mode_o__3D5E1FD2");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Sponsors)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK__sponsors__login___3B75D760");
            });

            modelBuilder.Entity<SponsoredFood>(entity =>
            {
                entity.HasKey(e => e.SfId)
                    .HasName("PK__Sponsore__BF2A24D0FA2D84C5");

                entity.ToTable("SponsoredFood");

                entity.HasIndex(e => e.FoodId, "UniqueSponsorfood")
                    .IsUnique();

                entity.Property(e => e.SfId).HasColumnName("SF_ID");

                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.ShelterId).HasColumnName("ShelterID");

                entity.Property(e => e.SponsorId).HasColumnName("Sponsor_ID");

                entity.HasOne(d => d.Food)
                    .WithOne(p => p.SponsoredFood)
                    .HasForeignKey<SponsoredFood>(d => d.FoodId)
                    .HasConstraintName("FK__Sponsored__FoodI__49C3F6B7");

                entity.HasOne(d => d.Shelter)
                    .WithMany(p => p.SponsoredFoods)
                    .HasForeignKey(d => d.ShelterId)
                    .HasConstraintName("FK__Sponsored__Shelt__4BAC3F29");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.SponsoredFoods)
                    .HasForeignKey(d => d.SponsorId)
                    .HasConstraintName("FK__Sponsored__Spons__4AB81AF0");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PK__userLogi__D7886867F83FDE3F");

                entity.ToTable("userLogin");

                entity.Property(e => e.LoginId).HasColumnName("Login_ID");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserTypeId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("UserType_id");

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__userLogin__IsVer__267ABA7A");
            });

            modelBuilder.Entity<UserTypeMaster>(entity =>
            {
                entity.HasKey(e => e.UserTypeId)
                    .HasName("PK__UserType__88F98EDB2B99832E");

                entity.ToTable("UserType_Master");

                entity.Property(e => e.UserTypeId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("UserType_id");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
