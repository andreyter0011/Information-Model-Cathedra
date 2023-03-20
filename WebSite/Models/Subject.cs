using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace WebSite.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Название предмета")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Количество часов")]
        public int hours { get; set; }
        [DisplayName("Тип нагрузки")]
        public string loadType { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public Subject()
        {
            Teachers = new List<Teacher>();
        }
        public static int Sum(IEnumerable<Subject> subjects)
        {
            return subjects.Sum(s => s.hours);
        }
    }
}
