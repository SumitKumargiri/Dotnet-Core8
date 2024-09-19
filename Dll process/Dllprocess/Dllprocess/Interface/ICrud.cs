using crudoperation.Model;

namespace Dllprocess.Interface
{
    public interface ICrud
    {
        Task<ResultModel<object>> GetAsync();
    }
}
