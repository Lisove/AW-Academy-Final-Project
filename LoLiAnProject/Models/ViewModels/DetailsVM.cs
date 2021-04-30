using LoLiAnProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Models.ViewModels
{
    public class DetailsVM
    {
        public int Id { get; set; }

        [Display(Name = "Humör")]
        public int Mood { get; set; }

        [Display(Name = "Fysisk aktivitet")]
        public int PhysicalActivity { get; set; }

        [Display(Name = "Stress")]
        public int Stress { get; set; }

        [Display(Name = "Sömn")]
        public int Sleep { get; set; }

        [Display(Name = "Psykiskt mående")]
        public int MentalHealth { get; set; }

        [Display(Name = "Fysiskt mående")]
        public int PhysicalHealth { get; set; }

        [Display(Name = "Anteckningar")]
        public string Notes { get; set; }

        [Display(Name = "Dagens bild")]
        public IFormFile ImageUpload { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser User { get; set; }
        public string ImagePath { get; internal set; }
    }
}
