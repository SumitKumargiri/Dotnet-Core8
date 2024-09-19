using crudoperation.Model;
using crudoperation.utility;
using Dllprocess.Interface;
using Dllprocess.Model;
namespace Dllprocess.Services
{
    public class CrudService : ICrud
    {
        private readonly DBGateway _dBGateway;

        public CrudService(string connectionString)
        {
            _dBGateway = new DBGateway(connectionString);
        }

        public async Task<ResultModel<object>> GetAsync()
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                var data = await _dBGateway.ExeQueryList<Model.Crud>("select *from crud");
                if (data != null)
                {
                    result.Message = "Data received";
                    result.Data = data;
                }
                else
                {
                    result.Message = "Error";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
