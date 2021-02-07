using Coelsa.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Coelsa.Repositories
{
    public class ContactRepository :  IContactRepository
    {
        private readonly IConfiguration _Configuration;
        private readonly string _Connection = "Data Source=192.168.0.10,1434;Initial Catalog=CoelsaDB;User ID=coelsauser; Password=coelsauser";
        public ContactRepository()
        {
            // TODO: Get connection from config file
            //this._Configuration = configuration;
            //this._Connection = _Configuration.GetConnectionString("SqlConnection");
        }
        public async Task<int> InsertAsync(ContactModel entity)
        {
            entity.CreatedDate = DateTime.Now;
            var sql =   " insert into Contacts " +
                        " (FirstName,LastName,Company,PhoneNumber,CreatedDate) " +
                        " output inserted.Id" +
                        " VALUES (@FirstName,@LastName,@Company,@PhoneNumber,@CreatedDate)";
            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql, entity);
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Contacts WHERE Id = @Id";
            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IEnumerable<ContactModel>> GetAllAsync()
        {
            var sql = "SELECT * FROM Contacts";
            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.QueryAsync<ContactModel>(sql);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<ContactModel>> GetContactsByCompanyAsync(string company, int pageNumber, int pageSize)
        {
            var sql =   @"WITH Results_CTE AS
                        (
                            SELECT *,
                                ROW_NUMBER() OVER(ORDER BY CreatedDate) AS RowNum
                            FROM Contacts
                            WHERE Company LIKE @Company
                        )
                        SELECT *
                        FROM Results_CTE
                        WHERE RowNum >= @Offset
                        AND RowNum < @Offset + @Limit";

            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.QueryAsync<ContactModel>(sql, new { Company = string.Concat("%",company,"%"), Offset = pageNumber, Limit = pageSize });
                return result.ToList();
            }
        }
                
        public async Task<ContactModel> GetAsync(int id)
        {
            var sql = "SELECT * FROM Contacts WHERE Id = @Id";
            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<ContactModel>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> UpdateAsync(ContactModel entity)
        {
            entity.ModifiedDate = DateTime.Now;
            var sql =   "UPDATE Contacts "                  +
                        " SET "                             +
                        " FirstName     = @FirstName,"      +
                        " LastName      = @LastName,"       +
                        " Company       = @Company,"        +
                        " PhoneNumber   = @PhoneNumber,"    +
                        " ModifiedDate  = @ModifiedDate"    +
                        " WHERE Id      = @Id";
            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var sql = "SELECT count(1) FROM Contacts WHERE Id = @Id";
            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
                return result;
            }
        }
    }
}
