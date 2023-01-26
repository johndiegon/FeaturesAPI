using Dapper;
using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Enum;
using Infrastructure.Data.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositorys
{
    public class RoutineRepository : IRoutineRepository
    {

        private readonly string _connectString;

        public RoutineRepository(IDatabaseSettings settings)
        {
            _connectString = settings.ConnectionStringsMysql;
        }

        public RoutineEntity Create(RoutineEntity entity)
        {
            var sql = @"INSERT INTO `direct_api`.`Routine`
                       (`id`,
                       `clientId`,
                       `dateTimeBegin`,
                       `dateTimeEnd`,
                       `monday`,
                       `tuesday`,
                       `wednesday`,
                       `thursday`,
                       `friday`,
                       `saturday`,
                       `saunday`,
                       `timeToSend`,
                       `status`,
                       `sent`,
                       `count`,
                       `template`,
                       `params`,
                       `update`)
                       VALUES
                       (@id,
                        @clientId,
                        @dateTimeBegin,
                        @dateTimeEnd,
                        @monday,
                        @tuesday,
                        @wednesday,
                        @thursday,
                        @friday,
                        @saturday,
                        @saunday,
                        @timeToSend,
                        @status,
                        @sent,
                        @count,
                        @template,
                        @params,
                        @update`);
                        SELECT LAST_INSERT_ID();                    
                       "
                      ;

            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    var idInserted = connection.ExecuteScalar(sql, entity);
                    entity.Id = Convert.ToInt32(idInserted);
                }
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public void Delete(string id)
        {
            var sql = @"DELETE FROM `direct_api`.`Routine`
                        WHERE id = @id ;"
                        ;

            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    connection.ExecuteScalar(sql, new { id = id });
                }
            }
            catch
            {
                throw;
            }
        }

        public List<RoutineEntity> Get(string idClient, int month, int year)
        {
            var sql = @"SELECT `Routine`.`id`,
                               `Routine`.`clientId`,
                               `Routine`.`dateTimeBegin`,
                               `Routine`.`dateTimeEnd`,
                               `Routine`.`monday`,
                               `Routine`.`tuesday`,
                               `Routine`.`wednesday`,
                               `Routine`.`thursday`,
                               `Routine`.`friday`,
                               `Routine`.`saturday`,
                               `Routine`.`saunday`,
                               `Routine`.`timeToSend`,
                               `Routine`.`status`,
                               `Routine`.`sent`,
                               `Routine`.`count`,
                               `Routine`.`template`,
                               `Routine`.`params`,
                               `Routine`.`update`
                           FROM `direct_api`.`Routine`
                           where  Month(`Routine`.`dateTime`) = @month
                       and Year(`Routine`.`dateTime`) = @year
                       and `Routine`.`clientId` = @clientId";
            ;

            try
            {
                var routines = new List<RoutineEntity>();
                using (var connection = new MySqlConnection(_connectString))
                {
                    routines = connection.Query<RoutineEntity>(sql, new
                    {
                        clientId = idClient,
                        month = month,
                        year = year
                    }).AsList();
                }

                return routines;
            }
            catch
            {
                throw;
            }
        }

        public RoutineEntity Get(string id)
        {
            var sql = @"SELECT `Routine`.`id`,
                               `Routine`.`clientId`,
                               `Routine`.`dateTimeBegin`,
                               `Routine`.`dateTimeEnd`,
                               `Routine`.`monday`,
                               `Routine`.`tuesday`,
                               `Routine`.`wednesday`,
                               `Routine`.`thursday`,
                               `Routine`.`friday`,
                               `Routine`.`saturday`,
                               `Routine`.`saunday`,
                               `Routine`.`timeToSend`,
                               `Routine`.`status`,
                               `Routine`.`sent`,
                               `Routine`.`count`,
                               `Routine`.`template`,
                               `Routine`.`params`,
                               `Routine`.`update`
                           FROM `direct_api`.`Routine`
                        WHERE id = @id ;"
                   ;

            try
            {
                var routine = new RoutineEntity();
                using (var connection = new MySqlConnection(_connectString))
                {
                    routine = connection.QueryFirst<RoutineEntity>(sql, new { id = id });
                }

                return routine;
            }
            catch
            {
                throw;
            }
        }

        public List<RoutineEntity> GetAutomaticMessage(string idClient, int beginMinute, int endMintue, StatusTask status)
        {
            var sql = @"SELECT `Routine`.`id`,
                               `Routine`.`clientId`,
                               `Routine`.`dateTimeBegin`,
                               `Routine`.`dateTimeEnd`,
                               `Routine`.`monday`,
                               `Routine`.`tuesday`,
                               `Routine`.`wednesday`,
                               `Routine`.`thursday`,
                               `Routine`.`friday`,
                               `Routine`.`saturday`,
                               `Routine`.`saunday`,
                               `Routine`.`timeToSend`,
                               `Routine`.`status`,
                               `Routine`.`sent`,
                               `Routine`.`count`,
                               `Routine`.`template`,
                               `Routine`.`params`,
                               `Routine`.`update`
                           FROM `direct_api`.`Routine`
                          where  Month(`Routine`.`dateTime`) = @month
                    and Year(`Routine`.`dateTime`) = @year
                    and minute(`Routine`.`dateTime`) between @beginMinute and @endMintue
                    and `Routine`.`clientId` = @clientId
                    and `Routine`.`status` = @status";
            try
            {
                var calendar = new List<RoutineEntity>();
                using (var connection = new MySqlConnection(_connectString))
                {
                    calendar = connection.Query<RoutineEntity>(sql, new
                    {
                        beginMinute = beginMinute,
                        endMintue = endMintue,
                        month = DateTime.Now.Month,
                        year = DateTime.Now.Year,
                        clientId = idClient,
                        status = status
                    }).AsList();
                }
                return calendar;
            }
            catch
            {
                throw;
            }
        }

        public List<string> GetClientsProcessAutomaticMessage(int beginMinute, int endMintue, StatusTask status)
        {
            var sql = @"SELECT  `Routine`.`clientId`
			              FROM `direct_api`.`Routine`
                         where  Month(`Routine`.`dateTime`) = @month
                           and Year(`Routine`.`dateTime`) = @year
                           and `Routine`.`status` = @status
                           and minute(`Routine`.`dateTime`) between @beginMinute and @endMintue
                           ;";
            try
            {
                var clients = new List<string>();
                using (var connection = new MySqlConnection(_connectString))
                {
                    clients = connection.Query<string>(sql, new
                    {
                        beginMinute = beginMinute,
                        endMintue = endMintue,
                        month = DateTime.Now.Month,
                        year = DateTime.Now.Year,
                        status = status
                    }).AsList();
                }

                return clients;
            }
            catch
            {
                throw;
            }
        }

        public RoutineEntity Update(RoutineEntity entity)
        {
            var sql = @"UPDATE `direct_api`.`Routine`
                         SET
                         `clientId` = @clientId,
                         `dateTimeBegin` = @dateTimeBegin,
                         `dateTimeEnd` = @dateTimeEnd,
                         `monday` = @monday,
                         `tuesday` = @tuesday,
                         `wednesday` = @wednesday,
                         `thursday` = @thursday,
                         `friday` = @friday,
                         `saturday` = @saturday,
                         `saunday` = @saunday,
                         `timeToSend` = @timeToSend,
                         `status` = @status,
                         `sent` = @sent,
                         `count` = @count,
                         `template` = @template,
                         `params` = @params,
                         WHERE `id` = @id;
                         ";

            try
            {
                var routine = new RoutineEntity();
                using (var connection = new MySqlConnection(_connectString))
                {
                    connection.ExecuteScalar(sql, entity);
                }

                return routine;
            }
            catch
            {
                throw;
            }
        }
    }
}
