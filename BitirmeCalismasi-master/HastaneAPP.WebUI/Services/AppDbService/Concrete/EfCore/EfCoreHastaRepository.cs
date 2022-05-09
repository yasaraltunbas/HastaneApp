using HastaneAPP.WebUI.Models.Entity;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;

namespace HastaneAPP.WebUI.Services.AppDbService.Concrete.EfCore
{
    public class EfCoreHastaRepository :  EfCoreGenericRepository<Hastalar, HastaneDbContex>, IHastaRepository
    {
        
    }
}