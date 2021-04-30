using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Models.ViewModels
{
    public class ChartsVM
    {
       
        public int Mood { get; set; }
        public int PhysicalActivity { get; set; }
        public int Stress { get; set; }
        public int Sleep { get; set; }
        public int MentalHealth { get; set; }
        public int PhysicalHealth { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }
}
