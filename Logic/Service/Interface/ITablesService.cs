using Data.Model.Entities;

namespace Logic.Service.Interface;

public interface ITablesService
{
    public Task<List<TableEntity>> GetTablesHall();
    public Task<List<TableEntity>> GetTablesLounge();
    public TableEntity GetById(string id);
    public IEnumerable<TableEntity> GetByAll();
    public List<TableEntity> GetByIds(List<string> id);
    public Task<List<TableEntity>> Create(List<TableEntity> t);
    public Task<TableEntity> Create(TableEntity t);
    public Task Edit(List<TableEntity> t);
    public Task Edit(TableEntity t);
    public Task Delete();
}