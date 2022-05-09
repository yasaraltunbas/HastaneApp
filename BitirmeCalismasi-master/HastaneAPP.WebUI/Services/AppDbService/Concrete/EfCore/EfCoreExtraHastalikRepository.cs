using HastaneAPP.WebUI.Models.Entity;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;

namespace HastaneAPP.WebUI.Services.AppDbService.Concrete.EfCore
{
    public class EfCoreExtraHastalikRepository :  EfCoreGenericRepository<ExtraHastalik, HastaneDbContex>, IExtraHastalikRepository
    {
        
    }
}