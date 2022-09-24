using Dapper;
using FeaturesAPI.Infrastructure.Data.Entities;
using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositorys
{
    public class ChatRepository : IChatRepository
    {
        private readonly string _connectString;

        public ChatRepository(IDatabaseSettings settings)
        {
            _connectString = settings.ConnectionStringsMysql;
        }

        public async Task Create(MessageOnChatEntity chat, ClientEntity client, ContactEntity contact)
        {
            Insert(contact.Id, chat.PhoneFrom == contact.Phone ? Sender.Contact : Sender.Client, chat.Message);
        }

        public async Task<IEnumerable<MessageOnChatEntity>> GetByClientId(ClientEntity client, string phone)
        {
            var sql = @"SELECT 
                              chat.*
                          FROM
                              direct_api.Chat chat
                                  JOIN
                               direct_api.Contact contact on contact.id = chat.idContact
                          WHERE
                              contact.Phone = @phone
                          AND contact.IdClient = @idClient
                        ORDER BY chat.DateInclude
                        LIMIT 60;";

            IEnumerable<Chat> chatMessage;
            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    chatMessage = await connection.QueryAsync<Chat>(sql, new { idClient = client.Id, phone = phone });
                }

                var response = from chat in chatMessage.ToList()
                       select new MessageOnChatEntity
                       {
                           DateTime = chat.DateInclude,
                           Message = chat.Message,
                           PhoneFrom = chat.Sender == Sender.Contact ? phone : client.Phone.FirstOrDefault(),
                           PhoneTo = chat.Sender != Sender.Contact ? phone : client.Phone.FirstOrDefault(),
                           WasVisible = true,
                       };

                return response;
            }
            catch
            {
                throw;
            }
        }

        private void Insert(int idContact, Sender sender, string message)
        {
            var sql = @"INSERT INTO `direct_api`.`Chat` (IdContact, Sender, Message) VALUES( @idContact, @sender, @Message)";

            try
            {
                using (var connection = new MySqlConnection(_connectString))
                {
                    var idInserted = connection.ExecuteScalar(sql, new { idContact = idContact, sender = sender, message = message });
                }

            }
            catch
            {
                throw;
            }
        }

        public enum Sender
        {
            Contact = 1,
            Client = 2,
            Automatic = 3,
        }
    }
}
