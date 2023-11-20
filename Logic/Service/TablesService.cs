using Data.Model;
using Data.Model.Entities;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class TablesService:ITablesService
{
    private readonly ITablesRepository _tablesRepository;

    public TablesService(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async Task<List<TableEntity>> GetTablesHall()
    {
        return await _tablesRepository.GetTableHall();
    }
   
    public async Task<List<TableEntity>> GetTablesLounge()
    {
        return await _tablesRepository.GetTableLounge();
    }
    
    public async Task Edit(TableEntity t)
    {
        await _tablesRepository.Edit(t);
    }
    
    public TableEntity GetById(string id)
    {
        return _tablesRepository.GetById(id);
    }
    public IEnumerable<TableEntity> GetByAll()
    {
        return _tablesRepository.GetByAll();
    }
    public List<TableEntity> GetByIds(List<string> id)
    {
        return _tablesRepository.GetByIds(id);
    }

    public async Task<List<TableEntity>> Create(List<TableEntity> t)
    {
        await _tablesRepository.Create(t);
        return t;
    }
    public async Task<TableEntity> Create(TableEntity t)
    {
        await _tablesRepository.Create(t);
        return t;
    }

    public async Task Edit(List<TableEntity> t)
    {
        await _tablesRepository.Edit(t);
    }

    public async Task Delete()
    {
        await _tablesRepository.Delete();
    }
}