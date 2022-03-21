using Domain.Helpers;
using Domain.Validators;
using MediatR;

namespace Domain.Queries.TwilioAccess.Get
{
    public class GetTwilioCredentials : Validate, IRequest<GetTwilioCredentialsResponse>
    {
        private string _phone;
        public string PhoneFrom
        {
            get { return _phone; }
            set { _phone = PhoneValid.TakeAValidNumber(value); }
        }
        public string IdUser { get; set; }
        public string IdClient { get; set; }
        public override bool IsValid()
        {
            if(string.IsNullOrEmpty(IdClient) || string.IsNullOrEmpty(IdUser))
                return false;

            if(string.IsNullOrEmpty(PhoneFrom))
                return false;
            else
                return true;
        }
    }
}
