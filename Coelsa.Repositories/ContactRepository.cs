using Coelsa.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Coelsa.Repositories
{
    public class ContactRepository :  IContactRepository
    {
        private readonly string _Connection;
        public ContactRepository(IOptions<SettingModel> configuration)
        {
            this._Connection = configuration.Value.SqlConnection;
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
            var sql = @"SELECT *
                        FROM Contacts
                        WHERE Company LIKE @Company
                        ORDER BY CreatedDate
                        OFFSET @PageSize * (@PageNumber - 1) ROWS
                        FETCH NEXT @PageSize ROWS ONLY OPTION (RECOMPILE)";

            using (var connection = new SqlConnection(this._Connection))
            {
                connection.Open();
                var result = await connection.QueryAsync<ContactModel>(sql, new { Company = string.Concat("%",company,"%"), PageNumber = pageNumber, PageSize = pageSize });
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
