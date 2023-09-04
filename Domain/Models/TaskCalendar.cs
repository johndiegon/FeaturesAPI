using Domain.Validators;
using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TaskCalendar : Validate
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Time { get; set; }
        public TaskStatus Status { get; set; }
        public string Template { get; set; }
        public List<Param> Params { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new TaskCalendarValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
