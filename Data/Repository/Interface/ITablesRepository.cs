using Data.Model;

namespace Data.Repository.Interface;

public interface ITablesRepository
{
    /*public Task<List<TablesEntity>> GetTablseBar();*/
    /*public Task<List<TablesEntity>> GetTablseStreet();*/
    public Task<List<TablesEntity>> GetTablseHall();
    public Task<List<TablesEntity>> GetTablseLaunge();
    public TablesEntity GetById(string id);
    public List<TablesEntity> GetByAll();
    public IEnumerable<TablesEntity> GetByIds(IEnumerable<string> ids);
    public Task Create(List<TablesEntity> t);
    public Task Edit(List<TablesEntity> t);
    public Task Edit(TablesEntity t);
    public Task Delete();
}