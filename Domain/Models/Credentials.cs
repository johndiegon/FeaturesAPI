using Domain.Helpers;

namespace Domain.Models
{
    public class Credentials
    {
        private string _phone;
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string PhoneFrom
        {
            get { return _phone; }
            set { _phone = PhoneValid.TakeAValidNumber(value); }
        }
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
    }
}
