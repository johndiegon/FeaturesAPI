using Dapper;
using FeaturesAPI.Infrastructure.Data.Interface;
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

        public ContactRepository(IDatabaseSettings settings)
        {
            _connectString = settings.ConnectionStringsMysql;
        }

        public async Task<IEnumerable<ContactEntity>> GetByClient(string idClient)
        {
            var sql = @"select * from direct_api.Contact where idClient = @idClient and Status = 1;";
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

        public async Task<IEnumerable<ContactListEntity>> GetListByClient(string idClient)
        {
            var sql = @"SELECT * 
                              , ROW_NUMBER() OVER (order by listContact.CreationDate) AS Id
                          FROM (
                         SELECT c.IdClient
                                , c.Classification as Name
                                , c.Unity
                                , COUNT(*) as Count
                                , SUM(o.Count) as CountOrders
                                , MAX(c.DateInclude) as CreationDate
                            FROM direct_api.Contact c
                            LEFT JOIN ( select Count(*) as Count
                                             , ContactId
                           		     from direct_api.Order
                                        group by ContactId
                                        ) as o on o.ContactId = c.Id
                           where c.IdClient = @idClient
                             and c.Classification is not null
                             and c.Status = 1
                            GROUP BY c.IDCLIENT 
                                   , c.CLASSIFICATION
                                   , c.Unity
                           union
                           SELECT c.IdClient 
                                , 'Lista de Clientes' as Name
                                , c.Unity
                                , COUNT(*) as Count
                                , SUM(o.Count) as CountOrders
                                , MAX(c.DateInclude) as CreationDate
                            FROM direct_api.Contact c
                            LEFT JOIN ( select Count(*) as Count
                                             , ContactId
                           		     from direct_api.Order
                                        group by ContactId
                                        ) as o on o.ContactId = c.Id
                           WHERE c.IdClient = @idClient
                             AND c.Status = 1
                           GROUP BY c.IDCLIENT 
                                  , c.Unity
                                   ) AS listContact";

            IEnumerable<ContactListEntity> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<ContactListEntity>(sql, new { idClient = idClient });
                }

                return contactList;
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<DateOrder>> GetDateOrderByClient(string idClient)
        {
            var sql = @"SELECT 
                              DATE_FORMAT(o.DateOrder, '%Y-%m-%d') AS OrderDate,
                              DATEDIFF(CURDATE(), o.DateOrder) AS Days,
                              c.Classification,
                              c.Unity,
                              COUNT(*) AS Count
                          FROM
                              direct_api.Contact c
                                  JOIN
                              direct_api.Order o ON o.ContactId = c.id
                          WHERE
                              c.IdClient = @idClient
                          and c.Status = 1
                          GROUP BY DATE_FORMAT(o.DateOrder, '%Y-%m-%d') , c.Classification , c.Unity 
                          UNION SELECT 
                              DATE_FORMAT(o.DateOrder, '%Y-%m-%d') AS OrderDate,
                              DATEDIFF(CURDATE(), o.DateOrder) AS Days,
                              'Lista de Clientes' AS Classification,
                              c.Unity,
                              COUNT(*) AS Count
                          FROM
                              direct_api.Contact c
                                  JOIN
                              direct_api.Order o ON o.ContactId = c.id
                          WHERE
                              c.IdClient = @idClient
                          and c.Status = 1
                          GROUP BY DATE_FORMAT(o.DateOrder, '%Y-%m-%d') , c.Unity;";

            IEnumerable<DateOrder> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<DateOrder>(sql, new { idClient = idClient });
                }

                return contactList;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CountOrder>> GetCountOrderByClient(string idClient)
        {
            var sql = @"SELECT 
                              Orders.CountOrder AS OrderCount,
                              c.IdClient,
                              c.Classification AS Name,
                              c.Unity,
                              COUNT(*) AS Count
                          FROM
                              (SELECT 
                                  c.id AS ContactId, COUNT(*) AS CountOrder
                              FROM
                                  direct_api.Contact c
                              JOIN direct_api.Order o ON o.ContactId = c.id
                              WHERE
                                  c.IdClient = @idClient
                              and c.Status = 1
                              GROUP BY o.ContactId) Orders
                                  JOIN
                              direct_api.Contact c ON c.id = Orders.ContactId
                          GROUP BY Orders.CountOrder , c.IdClient , c.Classification , c.Unity
                          ORDER BY c.Classification , c.Unity , Orders.CountOrder";

            IEnumerable<CountOrder> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<CountOrder>(sql, new { idClient = idClient });
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

            var sql = @"select * from direct_api.Contact c
                        where c.idClient = @idClient
                          and c.Status = 1
                        ";


            if (paramaters != null && paramaters.Count > 0)
            {

                var now = DateTime.Now;
                var listName = GetParam(paramaters, "listName");

                if (listName != null && listName.ToUpper() != "LISTA DE CLIENTES")
                    sql += $" and c.Classification = '{listName}'";

                var unit = GetParam(paramaters, "unity");

                if (unit != null)
                    sql += $" and c.Unity = '{unit}'";

                //var inputFilterDays = GetParam(paramaters, "inputFilterDays");
                //if (inputFilterDays != null)
                //{
                //    var value = 1;
                //    sql += $@"EXISTS( SELECT 
                //                           1
                //                       FROM
                //                           direct_api.Order
                //                       WHERE
                //                           direct_api.Order.ContactId = c.id
                //                               AND WEEKDAY(DateOrder) IN ({value}))";
                //}

                var inputMaxCountOrders = GetParam(paramaters, "inputMaxCountOrders");
                if (inputMaxCountOrders != null)
                {
                    sql += $@"AND (SELECT 
                                        COUNT(*)
                                    FROM
                                        direct_api.Order
                                    WHERE
                                        direct_api.Order.ContactId = c.id) <= {inputMaxCountOrders}";
                }

                var inputMinCountOrders = GetParam(paramaters, "inputMinCountOrders");
                if (inputMinCountOrders != null)
                {
                    sql += $@" AND (SELECT 
                                        COUNT(*)
                                    FROM
                                        direct_api.Order
                                    WHERE
                                        direct_api.Order.ContactId = c.id) >= {inputMinCountOrders}";
                }

                var inputMinDays = GetParam(paramaters, "inputMinDays");

                if (inputMinDays != null)
                {
                    sql += $@" AND (SELECT 
                                          DATEDIFF(CURDATE(), MAX(DateOrder)) AS T
                                      FROM
                                          direct_api.Order
                                      WHERE
                                          direct_api.Order.ContactId = c.id) >= {inputMinDays}";
                }

                var inputMaxDays = GetParam(paramaters, "inputMaxDays");

                if (inputMaxDays != null)
                {
                    sql += $@" AND (SELECT 
                                         DATEDIFF(CURDATE(), MAX(DateOrder)) AS T
                                     FROM
                                         direct_api.Order
                                     WHERE
                                         direct_api.Order.ContactId = c.id) <= {inputMaxDays}";
                }

                sql += ";";

            }

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
                return search[0] == null ? null : search[0].ToString();
            return null;
        }

        public async Task<IEnumerable<ContactEntity>> GetByPhone(string phone, string idClient)
        {
            var sql = @"select * from direct_api.Contact where IdClient = @idClient and Phone = @phone and Status = 1";
            IEnumerable<ContactEntity> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<ContactEntity>(sql, new { IdClient = idClient, Phone = phone });
                }

                return contactList;
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<ContactEntity>> GetContact(string phone, string idClient)
        {
            var sql = @"select * from direct_api.Contact where IdClient = @idClient and Phone = @phone and Status = 1";
            IEnumerable<ContactEntity> contactList;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    contactList = await connection.QueryAsync<ContactEntity>(sql, new { IdClient = idClient, Phone = phone }).ConfigureAwait(false);
                }

                return contactList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateMany(IEnumerable<ContactEntity> contacts)
        {
            foreach (var c in contacts)
            {
                await Update(c);
            }
        }
        public async Task UpdateStatus(int contactId, string status = "0")
        {
            var sql = @"update direct_api.Contact
                           set Status = @status
                       	 where id = @contactId
                       ";

            using (var connection = new MySqlConnection(_connectString))
            {
                await connection.ExecuteScalarAsync(sql, new { contactId = contactId, status = status });
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
                           and c.Status = 1
                       ";

            using (var connection = new MySqlConnection(_connectString))
            {
                await connection.ExecuteScalarAsync(sql, entity);
            }

        }
    }
}
