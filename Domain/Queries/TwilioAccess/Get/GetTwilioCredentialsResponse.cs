using Domain.Models;

namespace Domain.Queries.TwilioAccess.Get
{
    public class GetTwilioCredentialsResponse : CommandResponse
    {
        public Credentials Credentials { get; set; }

    }
}
