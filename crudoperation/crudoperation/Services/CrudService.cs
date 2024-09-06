using crudoperation.Interface;
using crudoperation.Model;
using crudoperation.utility;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace crudoperation.Services
{
    public class CrudService : Icrud
    {
        private readonly DBGateway _DBGateway;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromSeconds(5); 

        public CrudService(string connection, IMemoryCache cache)
        {
            _DBGateway = new DBGateway(connection);
            _cache = cache;
        }

        public async Task<ResultModel<object>> Getcrud()
        {
            ResultModel<object> result = new ResultModel<object>();

            string cacheKey = "dataList";

            if (!_cache.TryGetValue(cacheKey, out List<Crud> employees))
            {
                try
                {
                    employees = await _DBGateway.ExeQueryList<Crud>("SELECT * FROM crud", null);

                    if (employees != null)
                    {
                        _cache.Set(cacheKey, employees, _cacheExpiration);
                        result.Message = "data retrieved from database and cached";
                    }
                    else
                    {
                        result.Message = "No data found";
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Error while retrieving data: {ex.Message}";
                }
            }
            else
            {
                result.Message = "data retrieved from cache";
            }

            result.Data = employees;  
            result.Success = true;
            return result;
        }

        public async Task<ResultModel<object>> InsertCrud(Crud model)  
        {
            string chachekey = "datalist";
            try
            {
                var query = "INSERT INTO Crud (name, email, country) VALUES (@Name, @Email, @Country)";
                var par = new DynamicParameters();
                par.Add("@Name", model.name);
                par.Add("@Email", model.email);
                par.Add("@Country", model.country);

                var result = await _DBGateway.ExecuteAsync(query, par);

                if (result > 0)
                  
                {
                    _cache.Remove(chachekey);
                    return new ResultModel<object>
                    {
                        Success = true,
                        Message = "Record inserted successfully"
                    };
                }
                else
                {
                    return new ResultModel<object>
                    {
                        Success = false,
                        Message = "Failed to insert record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<object>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResultModel<object>> UpdateCrud(Crud model)
        {
            try
            {
                var query = "UPDATE Crud SET name = @Name, email = @Email, country = @Country WHERE id = @Id";
                var par = new DynamicParameters();
                par.Add("@Id", model.id);
                par.Add("@Name", model.name);
                par.Add("@Email", model.email);
                par.Add("@Country", model.country);

                var result = await _DBGateway.ExecuteAsync(query, par);

                if (result > 0)
                {
                    return new ResultModel<object>
                    {
                        Success = true,
                        Message = "Record updated successfully"
                    };
                }
                else
                {
                    return new ResultModel<object>
                    {
                        Success = false,
                        Message = "Failed to update record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<object>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResultModel<object>> DeleteCrud(int id)
        {
            string chachekey = "datalist";
            try
            {
                var query = "DELETE FROM Crud WHERE id = @Id";
                var par = new DynamicParameters();
                par.Add("@Id", id);

                var result = await _DBGateway.ExecuteAsync(query, par);

                if (result > 0)
                {
                    _cache.Remove(chachekey);

                    var updatelist = await _DBGateway.ExeQueryList<Crud>("select *from crud",null);
                    _cache.Set(chachekey, updatelist, TimeSpan.FromSeconds(2));

                    return new ResultModel<object>
                    {
                        Success = true,
                        Message = "Record deleted successfully"
                    };
                    _cache.Remove(chachekey);
                }
                else
                {
                    return new ResultModel<object>
                    {
                        Success = false,
                        Message = "Failed to delete record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<object>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
