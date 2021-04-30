using System;
using System.Collections.Generic;

#nullable disable

namespace LoLiAnProject.Models.Entities
{
    public partial class StatsTable
    {
        public int Id { get; set; }
        public int Mood { get; set; }
        public int PhysicalActivity { get; set; }
        public int Stress { get; set; }
        public int Sleep { get; set; }
        public int MentalHealth { get; set; }
        public int PhysicalHealth { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
        public bool? HasImage { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
