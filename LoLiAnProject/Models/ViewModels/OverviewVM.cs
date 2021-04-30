using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Models.ViewModels
{
    public class OverviewVM
    {

        public DateTime Date { get; set; }

        public bool DateExist { get; set; }
    }
}
