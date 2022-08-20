using Dapper;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositorys
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectString;

        public ContactRepository()
        {
            _connectString = Environment.GetEnvironmentVariable("connectString");
        }

        public async Task<IEnumerable<ContactEntity>> GetByClient(string idClient)
        {
            var sql = @"select * from direct_api.Contact where idClient = @idClient";
            IEnumerable<ContactEntity> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<ContactEntity>(sql, new { idClient = idClient });
                }

                return contactList;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ContactEntity>> GetByClient(string idClient, List<Param> paramaters)
        {

            var sql = @"select * from direct_api.Contact where idClient = @idClient";

            var now = DateTime.Now;
            var listName = GetParam(paramaters, "listName");
         
            if(listName != null)
                sql += string.Concat(" and Classification = '",listName, "';") ;
            
            //var typeList = GetParam(paramaters, "typeList");

            //var inputFilterDays = GetParam(paramaters, "inputFilterDays");


            //var unity = GetParam(paramaters, "unity");
            //var inputMinCountOrders = GetParam(paramaters, "inputMinCountOrders");
            //var inputMaxCountOrders = GetParam(paramaters, "inputMaxCountOrders");
            //var inputMinDay = GetParam(paramaters, "inputMinDays");
            //var inputMaxDay = GetParam(paramaters, "inputMaxDays");

            //var countMessages = GetParam(paramaters, "countMessages");
            //var inputParamCupon = GetParam(paramaters, "inputParamCupon");
            //var inputNameProduct = GetParam(paramaters, "inputNameProduct");
            //var inputData = GetParam(paramaters, "inputData");



            IEnumerable<ContactEntity> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<ContactEntity>(sql, new { idClient = idClient });
                }

                return contactList;
            }
            catch
            {
                throw;
            }
        }
        private static string GetParam(List<Param> paramaters, string nameParam)
        {
            var search = paramaters.Where(p => p.Name == nameParam).Select(pm => pm.Value).ToList();

            if (search.Count() > 0)
                return search[0].ToString();
            return null;
        }

        public Task<IEnumerable<ContactEntity>> GetByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactEntity>> GetContact(string phone, string idClient)
        {
            var sql = @"select * from direct_api.Contact where IdClient = @idClient and Phone = @phone";
            IEnumerable<ContactEntity> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<ContactEntity>(sql, new { IdClient = idClient, Phone = phone }).ConfigureAwait(false);
                }

                return contactList;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateMany(IEnumerable<ContactEntity> contacts)
        {
            foreach (var c in contacts)
            {
               await Update(c);
            }
        }

        private async Task Update(ContactEntity entity)
        {
            var sql = @"update direct_api.Contact
                           set Unity  = @unity
                             , Status = @status
                             , Classification = @classification
                       	 where Phone = @phone 
                           and IdClient = @idClient
                       ";

            using (var connection = new MySqlConnection(_connectString))
            {
                await connection.ExecuteScalarAsync(sql, entity);
            }

        }
    }
}
