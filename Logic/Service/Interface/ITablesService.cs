using Data.Model;

namespace Logic.Service.Interface;

public interface ITablesService
{
    /*public Task<List<TablesEntity>> GetTablesBar();*/
    public Task<List<TablesEntity>> GetTablesHall();
    /*public Task<List<TablesEntity>> GetTablesStreet();*/
    public Task<List<TablesEntity>> GetTablesLaunge();
    public TablesEntity GetById(string id);
    public List<TablesEntity> GetByAll();
    public IEnumerable<TablesEntity> GetByIds(IEnumerable<string> id);
    public Task<List<TablesEntity>> Create(List<TablesEntity> t);
    public Task Edit(List<TablesEntity> t);
    public Task Edit(TablesEntity t);
    public Task Delete();
}