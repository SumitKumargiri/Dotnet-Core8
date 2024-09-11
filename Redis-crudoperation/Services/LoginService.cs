using crudoperation.Interface;
using crudoperation.Model;
using crudoperation.utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace crudoperation.Services
{
    public class LoginService : ILogin
    {
        private readonly DBGateway _DBGateway;
        private readonly IDistributedCache _distributedCache;

        public LoginService(string connection, IDistributedCache distributedCache)
        {
            _DBGateway = new DBGateway(connection);
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        
        public async Task<ResultModel<object>> LoginAsync(Login login)
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                var query = "SELECT * FROM mp_login WHERE Username=@username AND Password=@password";

                var par = new DynamicParameters();
                par.Add("@username", login.Username);
                par.Add("@password", login.Password);

                var user = await _DBGateway.ExeQueryList<Login>(query, par);

                if (user != null && user.Count > 0)  
                {
                    var exitchachedata = await _distributedCache.GetStringAsync(login.Username);

                    if(!String.IsNullOrEmpty(exitchachedata))
                    {
                        var exitchachesession = JsonConvert.DeserializeObject < UserSession > (exitchachedata);

                        if (exitchachedata!=null && exitchachesession.ExpiresAt>DateTime.UtcNow)
                        {
                            result.Data = exitchachesession.Token;
                            result.Success = true;
                            result.Message = "Session already start";
                            return result;
                        }
                        else
                        {
                            await RemoveSession(login.Username);
                        }
                    }
                    var sessionToken = Guid.NewGuid().ToString();
                    var session = new UserSession
                    {
                        Username = login.Username,
                        Token = sessionToken,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                    };
                    var sessionData = JsonConvert.SerializeObject(session);
                    var redisOptions = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    };
                    await _distributedCache.SetStringAsync(sessionToken, sessionData, redisOptions);

                    result.Data = sessionToken;
                    result.Success = true;
                    result.Message = "Successful login";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>

        public async Task<ResultModel<object>> RegisterAsync(Register register)
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                var query = "INSERT INTO mp_login(Firstname,Lastname,Username,Email,MobileNumber,Password) VALUES(@firstname,@lastname,@username,@email,@mobilenumber,@password)";
                var par = new DynamicParameters();
                par.Add("@firstname", register.Firstname);
                par.Add("@lastname", register.Lastname);
                par.Add("@username", register.Username);
                par.Add("@email", register.Email);
                par.Add("@mobilenumber", register.MobileNumber);
                par.Add("@password", register.Password);

                var user = await _DBGateway.ExeQueryList<Register>(query, par);
                if (user != null) {
                    var sessionToken = Guid.NewGuid().ToString();
                    var session = new UserSession
                    {
                        Username = register.Username,
                        Token = sessionToken,
                        ExpiresAt = DateTime.UtcNow.AddSeconds(5)
                    };
                    var sessionData = JsonConvert.SerializeObject(session);
                    var redisOptions = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                    };
                    await _distributedCache.SetStringAsync(sessionToken, sessionData, redisOptions);

                    result.Data = sessionToken;
                    result.Success = true;
                    result.Message = "Successful Register";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Please fill required filled";
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }
            return result;
        }


        public async Task RemoveSession(string Username)
        {
            await _distributedCache.RemoveAsync(Username);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearchache"></param>
        /// <returns></returns>
        /// 

        public async Task <ResultModel<object>> clearchache(string key)
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                await _distributedCache.RemoveAsync(key);
                result.Success = true;
                result.Message = "chache clear Successfully";

            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "error clearing chache";
            }
            return result;
        }
    }
}
