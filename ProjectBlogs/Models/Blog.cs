using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogs.Models
{
    public class Blog
    {
        [Key]
        [Display(Name="ID")]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        [Display(Name="Назва")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Заговолок")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Опис")]
        public string Content { get; set; }
        [Display(Name = "Час")]
        public DateTime? Date { get; set; } = DateTime.Now;
        [Display(Name = "Адрес фото")]
        [Required]
        public string PhotoUrl { get; set; }
        [Display(Name = "Автор")]
        public string Author { get; set; }
    }
}
