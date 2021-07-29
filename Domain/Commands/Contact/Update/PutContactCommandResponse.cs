using Domain.Models;

namespace Domain.Commands.Contact.Update
{
    public  class PutContactCommandResponse : CommandResponse
    {
        public Models.Contact Contact { get; set; }

    }
}
