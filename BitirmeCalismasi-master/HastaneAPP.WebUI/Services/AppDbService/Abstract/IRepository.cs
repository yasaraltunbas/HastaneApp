using System.Collections.Generic;

namespace HastaneAPP.WebUI.Services.AppDbService.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);
        
        List<T> GetAll();
        
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}