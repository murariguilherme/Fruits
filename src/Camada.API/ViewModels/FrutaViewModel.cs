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

        [Required(ErrorMessage = "O campo {0} é requerido.")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa conter entre {1} e {2} caracteres.", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(4000, ErrorMessage = "O campo {0} precisa conter até {1} caracteres.")]
        [DisplayName("Descrição")]
        public string Description { get; set; }
        
        
        [DisplayName("Image")]
        public IFormFile ImageUpload { get; set; }
        public String Image { get; set; }
    }
}