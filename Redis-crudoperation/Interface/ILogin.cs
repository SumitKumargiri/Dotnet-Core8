using crudoperation.Model;

namespace crudoperation.Interface
{
    public interface ILogin
    {
        Task<ResultModel<object>> LoginAsync(Login login);
        Task<ResultModel<object>> RegisterAsync(Register register);
        Task RemoveSession(string Username);
        Task<ResultModel<object>> clearchache(string key);
    }
}
