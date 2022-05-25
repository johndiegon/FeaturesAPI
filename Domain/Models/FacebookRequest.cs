using System.Collections.Generic;

namespace Domain.Models
{
    public class FacebookRequest
    {
        public string Object { get; set; }
        public Entry Entry { get; set; }
    }

    public class Entry
    {
        public string Id { get; set; }
        public List<Changes> Changes{ get; set; }
    }
    public class Changes
    {
        public Value Value { get; set; }
        public string Field { get; set; }
    }
    public class Value
    {
        public string Messaging_product { get; set; }
        public Metadata Metadata { get; set; }
        public List<ContactFacebook> Contacts { get; set; }
        public List<FacebookMessage> Messages { get; set; }
        public List<FacebookStatus> Statuses { get; set; }
    }

    public class FacebookProfile
    {
        public string Name { get; set; }
    }

    public class Metadata
    {
        public string Display_phone_number { get; set; }
        public string Phone_number_id { get; set; }
    }
    
    public class Text
    {
        public string Body { get; set; }
    }

    public class FacebookStatus
    {
        public string Id { get; set; }
        public string Recipient_id { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }
    }
    public class Conversation
    {
        public string Id { get; set; }
        public string Expiration_timestamp { get; set; }    
        public Origin Origin { get; set; }  
    }
    public class Origin
    {
        public string Type { get; set; }
    }

    public class FacebookMessage
    {
        public string From { get; set; }
        public string Id { get; set; }
        public Text Text { get; set; }
        public string Timestamp { get; set; }
        public string Type { get; set; }
        public Image Image { get; set; }
        public Stiker Stiker { get; set; }
        public List<Error> Errors { get; set; }
        public Location Location { get; set; }
        public List<ContactFacebook> contacts { get; set; }
        public Button Button { get; set; }
        public Referral Referral { get; set; }
        public Interactive interactive { get; set; }

    }
    public class Interactive
    {
        public List<Reply> List_Reply { get; set; }
        public Reply ButtonReply { get; set; }
        public string Type { get; set; }

    }
    public class Referral
    {
        public string source_url { get; set; }
        public string source_id { get; set; }
        public string source_type { get; set; }
        public string headline { get; set; }
        public string body { get; set; }
        public string media_type { get; set; }
        public string image_url { get; set; }
        public string video_url { get; set; }
        public string thumbnail_url { get; set; }
    }

    public class Reply
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class Button
    {
        public string Text { get; set; }
        public string Payload { get; set; }
    }
    public class Location
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }   
        public string Name { get; set; }
        public string Adrress { get; set; }
    }


    public class Address
    {
        public string city { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string type { get; set; }
        public string zip { get; set; }
    }

    public class ContactFacebook
    {
        public FacebookProfile Profile { get; set; }
        public string Wa_id { get; set; }
        public List<Address> addresses { get; set; }
        public string birthday { get; set; }
        public List<Email> emails { get; set; }
        public Name name { get; set; }
        public Org org { get; set; }
        public List<Phone> phones { get; set; }
        public List<Url> urls { get; set; }
    }

    public class Email
    {
        public string email { get; set; }
        public string type { get; set; }
    }

    public class Name
    {
        public string formatted_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string suffix { get; set; }
        public string prefix { get; set; }
    }

    public class Org
    {
        public string company { get; set; }
        public string department { get; set; }
        public string title { get; set; }
    }

    public class Phone
    {
        public string phone { get; set; }
        public string wa_id { get; set; }
        public string type { get; set; }
    }

    public class Url
    {
        public string url { get; set; }
        public string type { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }
    }

    public class Image
    {
        public string Caption { get; set; }
        public string Mime_type { get; set; }
        public string Sha256 { get; set; }
        public string Id { get; set; }
    }

    public class Stiker
    {
        public string Mime_type { get; set; }
        public string Sha256 { get; set; }
        public string Id { get; set; }

    }
}
