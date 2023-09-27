using Data.Model;
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
    /*public DbSet<ClientEnity> Client { get; set; }
    public DbSet<TablesEntity> Tables { get; set; }
    public DbSet<ReservesEntity> Reserves { get; set; }
    public DbSet<FeatureEntity> Feature { get; set; }*/
}