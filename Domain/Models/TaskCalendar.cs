using Domain.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class TaskCalendar : Validate
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public bool Sent { get; set; }
        public int Count { get; set; }
        public string Template { get; set; }
        public string Params { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new TaskCalendarValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
