using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace WebSite.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("ФИО")]
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Дата рождения")]
        public DateTime dateOfBirth { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Номер телефона должен состоять из 10 цифр.")]
        public string? contactInfo { get; set; }
        [DisplayName("Должность")]
        public string? Position { get; set; }
        [Required]
        [DisplayName("Образование")]
        public string education { get; set; }
        [DisplayName("Опыт работы")]
        public string expirience { get; set; }
        [DisplayName("Научный достижения")]
        public string achivments { get; set; }
        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
