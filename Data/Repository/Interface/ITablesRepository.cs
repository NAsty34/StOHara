using Data.Model.Entities;

namespace Data.Repository.Interface;

public interface ITablesRepository
{
    public Task<List<TableEntity>> GetTableHall();
    public Task<List<TableEntity>> GetTableLounge();
    public TableEntity GetById(string id);
    public IEnumerable<TableEntity> GetByAll();
    public List<TableEntity> GetByIds(List<string> ids);
    public Task Create(List<TableEntity> t);
    public Task Create(TableEntity t);
    public Task Edit(List<TableEntity> t);
    public Task Edit(TableEntity t);
    public Task Delete();
}