using Data.Model.Entities;
using Data.Model.Lending;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data;

public class MaxOHaraContext:DbContext
{
    public MaxOHaraContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<UserEntity>(a =>
        {
            a.Property(d => d.RoleEntity).HasConversion(new EnumToStringConverter<RoleEntity>());
        });
        
        modelBuilder.Entity<ReserveEntity>(a =>
        {
            a.HasMany(e => e.Tables)
                .WithMany(e => e.Reserves);
        });
        
        base.OnModelCreating(modelBuilder);
        
    }
    
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<FileEntity> Files { get; set; } = null!;
    public DbSet<MenuEntity> Menu { get; set; } = null!;
    public DbSet<GalleryEntity> Gallery { get; set; } = null!;
    public DbSet<AboutLendingEntity> AboutLending { get; set; }
    public DbSet<BannerLendingEntity> BannerLending { get; set; }
    public DbSet<SliderLendingEntity> SliderLending { get; set; }
    public DbSet<AtmosphereLendingEntity> AtmosphereLending { get; set; }
    public DbSet<ClientEnity> Clients { get; set; }
    public DbSet<TableEntity> Tables { get; set; }
    public DbSet<ReserveEntity> Reserves { get; set; }
    public DbSet<FeatureEntity> Feature { get; set; }
    
}