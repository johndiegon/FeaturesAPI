using Domain.Models;

namespace Domain.Commands.List.Put
{
    public class PutContactListCommandResponse : CommandResponse
    {
        public ContactList ContactList { get; set; }
    }
}
