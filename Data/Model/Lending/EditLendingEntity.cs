namespace Data.Model.Lending;

public class EditLendingEntity
{
    public virtual BannerLendingEntity BannerLendingEntity { get; set; }
    public virtual List<AboutLendingEntity> AboutLendingEntities { get; set; }
    public virtual List<AtmosphereLendingEntity> AtmosphereLendingEntities { get; set; }
}