using Dapper;
using FeaturesAPI.Infrastructure.Data.Entities;
using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Data.Repositorys.ChatRepository;

namespace Infrastructure.Data.Repositorys
{
    public class LastMessageRepository :  ILastMessageRepository
    {
        private readonly string _connectString;

        public LastMessageRepository(IDatabaseSettings settings)
        {
            _connectString = settings.ConnectionStringsMysql;
        }

        public async Task<IEnumerable<LastMessageEntity>> GetByClientId(ClientEntity client)
        {
            var sql = @"SELECT 
                              chat.*,
                              contact.Name,
                              contact.Phone
                         FROM
                              direct_api.Chat chat
                                  JOIN
                              direct_api.Contact contact on contact.id = chat.idContact
                        WHERE contact.IdClient = @idClient
                          and chat.sender != '3'
                          and chat.DateInclude in (SELECT max(DateInclude) as LastDate 
                                                     from  direct_api.Chat chatDate 
                                                    where chatDate.idContact =  chat.idContact
                                                      and chatDate.sender != '3')";

            IEnumerable<Chat> chatMessage;
            try
            {
                var phone = client.Phone.FirstOrDefault();

                using (var connection = new MySqlConnection(_connectString))
                {
                    chatMessage = await connection.QueryAsync<Chat>(sql, new { idClient = client.Id});
                }

                return from chat in chatMessage.ToList()
                       select new LastMessageEntity
                       {
                           DateTime = chat.DateInclude,
                           Message = chat.Message,
                           PhoneFrom = phone,
                           PhoneTo = chat.Phone,
                           IdClient = client.Id,
                           NameFrom = chat.Sender != Sender.Contact ? chat.Name : client.Name,
                           NameTo = chat.Sender == Sender.Contact ? chat.Name : client.Name,
                       };
            }
            catch
            {
                throw;
            }
        }
    }
}
