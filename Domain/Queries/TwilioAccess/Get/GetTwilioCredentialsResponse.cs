using Domain.Models;

namespace Domain.Queries.TwilioAccess.Get
{
    public class GetTwilioCredentialsResponse : CommandResponse
    {
        public TwilioCredentials Credentials{ get; set; }  

    }
}
