using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fruits.App.ViewModels
{
    public class FruitViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }
        
        [DisplayName("Image")]
        public IFormFile ImageUpload { get; set; }
        public String Image { get; set; }
    }
}