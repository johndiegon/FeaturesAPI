using Dapper;
using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
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
                        `params`)
                        VALUES
                        (@clientId,
                         @dateTime,
                         @status,
                         @sent,
                         @count,
                         @template,
                         @params);
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
    }
}
