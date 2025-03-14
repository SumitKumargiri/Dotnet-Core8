# How to Install NuGet Packages:
    -- Install the following packages:
    -- Dapper
    -- Microsoft.Data.SqlClient
# --------------------------------------------------
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace empcrudoperation.utility
{
    public class DBGateway
    {
        private readonly string _connectionString;

        public DBGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection()
        {
            return new SqlConnection(_connectionString); 
        }

        public async Task<T> ExeScalarQuery<T>(string QueryText, DynamicParameters paras)
        {
            try
            {
                using (IDbConnection conn = Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    return await SqlMapper.QueryFirstOrDefaultAsync<T>(conn, QueryText, paras, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database operation failed", ex);
            }
        }

        public async Task<int> ExeQuery(string QueryText, DynamicParameters paras)
        {
            try
            {
                using (IDbConnection conn = Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    return await conn.ExecuteAsync(QueryText, paras, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database operation failed", ex);
            }
        }

        public async Task<List<T>> ExeQueryList<T>(string QueryText, DynamicParameters paras)
        {
            try
            {
                using (IDbConnection conn = Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var result = await SqlMapper.QueryAsync<T>(conn, QueryText, paras, commandType: CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database operation failed", ex);
            }
        }

        public async Task<T> ExeSPScaler<T>(string QueryText, DynamicParameters paras)
        {
            try
            {
                using (IDbConnection conn = Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    return await SqlMapper.QueryFirstOrDefaultAsync<T>(conn, QueryText, paras, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Stored Procedure operation failed", ex);
            }
        }

        public async Task<List<T>> ExeSPList<T>(string QueryText, DynamicParameters paras)
        {
            try
            {
                using (IDbConnection conn = Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    var result = await SqlMapper.QueryAsync<T>(conn, QueryText, paras, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Stored Procedure operation failed", ex);
            }
        }

        public async Task<int> ExeSP(string QueryText, DynamicParameters paras)
        {
            try
            {
                using (IDbConnection conn = Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    return await conn.ExecuteAsync(QueryText, paras, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Stored Procedure execution failed", ex);
            }
        }
    }
}
