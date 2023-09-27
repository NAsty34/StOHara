using Data.Model;
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

    /*public async Task<List<TablesEntity>> GetTablesBar()
    {
        return await _tablesRepository.GetTablseBar();
    }*/
    public async Task<List<TablesEntity>> GetTablesHall()
    {
        return await _tablesRepository.GetTablseHall();
    }
    /*public async Task<List<TablesEntity>> GetTablesStreet()
    {
        return await _tablesRepository.GetTablseStreet();
    }*/
    public async Task<List<TablesEntity>> GetTablesLaunge()
    {
        return await _tablesRepository.GetTablseLaunge();
    }
    
    public async Task Edit(TablesEntity t)
    {
        await _tablesRepository.Edit(t);
    }
    
    public TablesEntity GetById(string id)
    {
        return _tablesRepository.GetById(id);
    }
    public List<TablesEntity> GetByAll()
    {
        return _tablesRepository.GetByAll();
    }
    public IEnumerable<TablesEntity> GetByIds(IEnumerable<string> id)
    {
        return _tablesRepository.GetByIds(id);
    }

    public async Task<List<TablesEntity>> Create(List<TablesEntity> t)
    {
        await _tablesRepository.Create(t);
        return t;
    }

    public async Task Edit(List<TablesEntity> t)
    {
        await _tablesRepository.Edit(t);
    }

    public async Task Delete()
    {
        await _tablesRepository.Delete();
    }
}