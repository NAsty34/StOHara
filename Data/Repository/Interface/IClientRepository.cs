using Data.Model;
using Data.Model.Entities;

namespace Data.Repository.Interface;

public interface IClientRepository
{
    public Task<ClientEnity?> GetByFio(string name, string surname, string patronymic);
    public Task<PageModel<ClientEnity>> GetPage(int? page, int? size);
    public Task<ClientEnity?> GetById(Guid id);
    public Task Create(ClientEnity t);
    public Task Delete();
    
}