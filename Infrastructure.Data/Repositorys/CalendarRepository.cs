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
    public class CalendarRepository : ICalendarRepository
    {

        private readonly string _connectString;

        public CalendarRepository(IDatabaseSettings settings)
        {
            _connectString = settings.ConnectionStringsMysql;
        }

        public CalendarEntity Create(CalendarEntity entity)
        {
            var sql = @"INSERT INTO `direct_api`.`Calendar`
                        (`clientId`,
                        `dateTime`,
                        `status`,
                        `sent`,
                        `count`,
                        `template`,
                        `params`,
                        `filters')
                        VALUES
                        (@clientId,
                         @dateTime,
                         @status,
                         @sent,
                         @count,
                         @template,
                         @params,
                         @filters);
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
            var sql = @"DELETE FROM `direct_api`.`Calendar`
                        WHERE id = @id ;"
                       ;

            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    connection.ExecuteScalar(sql, new { id = id});
                }
            }
            catch
            {
                throw;
            }
        }

        public CalendarEntity Get(string id)
        {
            var sql = @"SELECT `Calendar`.`id`,
                               `Calendar`.`clientId`,
                               `Calendar`.`dateTime`,
                               `Calendar`.`status`,
                               `Calendar`.`sent`,
                               `Calendar`.`count`,
                               `Calendar`.`template`,
                               `Calendar`.`params`,
                               `Calendar`.`filters`,
                               `Calendar`.`update`
                           FROM `direct_api`.`Calendar`
                        WHERE id = @id ;"
                      ;

            try
            {
                var calendar = new CalendarEntity();
                using (var connection = new MySqlConnection(_connectString))
                {
                    calendar = connection.QueryFirst<CalendarEntity>(sql, new { id = id });
                }

                return calendar;
            }
            catch
            {
                throw;
            }
        }

        public CalendarEntity Update(CalendarEntity entity)
        {
            var sql = @"UPDATE `direct_api`.`Calendar`
                           SET
                           `dateTime` = @dateTime,
                           `status` = @status,
                           `sent` = @sent,
                           `count` = @count,
                           `template` = @template,
                           `params` = @params
                           `filters` = @filters
                         WHERE `id` = @id;";

            try
            {
                var calendar = new CalendarEntity();
                using (var connection = new MySqlConnection(_connectString))
                {
                    connection.ExecuteScalar(sql, entity);
                }

                return calendar;
            }
            catch
            {
                throw;
            }
        }

        public List<CalendarEntity> Get(string idClient, int month , int year )
        {
            var sql = @"SELECT `Calendar`.`id`,
                        `Calendar`.`clientId`,
                        `Calendar`.`dateTime`,
                        `Calendar`.`status`,
                        `Calendar`.`sent`,
                        `Calendar`.`count`,
                        `Calendar`.`template`,
                        `Calendar`.`params`,
                        `Calendar`.`filters`,
                        `Calendar`.`update`
                    FROM `direct_api`.`Calendar`
                    where  Month(`Calendar`.`dateTime`) = @month
                    and Year(`Calendar`.`dateTime`) = @year
                    and `Calendar`.`clientId` = @clientId";

            try
            {
                var calendar = new List<CalendarEntity>();
                using (var connection = new MySqlConnection(_connectString))
                {
                    calendar =  connection.Query<CalendarEntity>(sql, new 
                    {
                        clientId = idClient,
                        month = month,
                        year = year
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
            var sql = @"SELECT  `Calendar`.`clientId`
			              FROM `direct_api`.`Calendar`
                         where  Month(`Calendar`.`dateTime`) = @month
                           and Year(`Calendar`.`dateTime`) = @year
                           and `Calendar`.`status` = @status
                           and minute(`Calendar`.`dateTime`) between @beginMinute and @endMintue
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

        public List<CalendarEntity> GetAutomaticMessage(string idClient, int beginMinute, int endMintue, StatusTask status)
        {
            var sql = @"SELECT `Calendar`.`id`,
                        `Calendar`.`clientId`,
                        `Calendar`.`dateTime`,
                        `Calendar`.`status`,
                        `Calendar`.`sent`,
                        `Calendar`.`count`,
                        `Calendar`.`template`,
                        `Calendar`.`params`,
                        `Calendar`.`filters`,
                        `Calendar`.`update`
                    FROM `direct_api`.`Calendar`
                    where  Month(`Calendar`.`dateTime`) = @month
                    and Year(`Calendar`.`dateTime`) = @year
                    and minute(`Calendar`.`dateTime`) between @beginMinute and @endMintue
                    and `Calendar`.`clientId` = @clientId
                    and `Calendar`.`status` = @status";

            try
            {
                var calendar = new List<CalendarEntity>();
                using (var connection = new MySqlConnection(_connectString))
                {
                    calendar = connection.Query<CalendarEntity>(sql, new
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
    }
}
