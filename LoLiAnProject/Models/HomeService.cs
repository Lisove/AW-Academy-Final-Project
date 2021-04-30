using LoLiAnProject.Models.Entities;
using LoLiAnProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoLiAnProject.Controllers;
using System.Runtime.Serialization;

namespace LoLiAnProject.Models
{
    public class HomeService
    {
        MyContext context;
        IWebHostEnvironment webHostEnv;
        private readonly IHttpContextAccessor accessor;
        readonly UserManager<MyIdentityUser> userManager; //används vid registerering


        public HomeService(MyContext context, IWebHostEnvironment webHostEnv, IHttpContextAccessor accessor, UserManager<MyIdentityUser> userManager)
        {
            this.context = context;
            this.webHostEnv = webHostEnv;
            this.accessor = accessor;
            this.userManager = userManager;

        }

        //---------Register our day--------//
        internal void RegisterYourDay(RegisterVM registerVM)
        {

            if (registerVM.ImageUpload != null)
            {
                var filePath = Path.Combine(webHostEnv.WebRootPath, "Uploads", registerVM.ImageUpload.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    registerVM.ImageUpload.CopyTo(fileStream);
                }
            }

            string userId = userManager.GetUserId(
                accessor.HttpContext.User);

            context.StatsTables.Add(new StatsTable
            {
                Id = registerVM.Id,
                Mood = registerVM.Mood,
                Stress = registerVM.Stress,
                Sleep = registerVM.Sleep,
                MentalHealth = registerVM.MentalHealth,
                PhysicalHealth = registerVM.PhysicalHealth,
                PhysicalActivity = registerVM.PhysicalActivity,
                Notes = registerVM.Notes,
                ImagePath = registerVM.ImageUpload?.FileName,
                HasImage = registerVM.ImageUpload?.FileName.Length > 0,
                UserId = userId,
                Date = registerVM.Date
            }); ;


            var dateExist = context.StatsTables
                .Where(x => x.UserId == userId)
                .FirstOrDefault(x => x.Date == registerVM.Date);

            context.SaveChanges();
        }

        //------------ Register Previous days-------------//

        internal void RegisterYourPreviousDay(RegisterPreviousDayVM registerPreviousDayVM)
        {

            if (registerPreviousDayVM.ImageUpload != null)
            {
                var filePath = Path.Combine(webHostEnv.WebRootPath, "Uploads", registerPreviousDayVM.ImageUpload.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    registerPreviousDayVM.ImageUpload.CopyTo(fileStream);
                }
            }

            string userId = userManager.GetUserId(
                accessor.HttpContext.User);

            context.StatsTables.Add(new StatsTable
            {
                Id = registerPreviousDayVM.Id,
                Mood = registerPreviousDayVM.Mood,
                Stress = registerPreviousDayVM.Stress,
                Sleep = registerPreviousDayVM.Sleep,
                MentalHealth = registerPreviousDayVM.MentalHealth,
                PhysicalHealth = registerPreviousDayVM.PhysicalHealth,
                PhysicalActivity = registerPreviousDayVM.PhysicalActivity,
                Notes = registerPreviousDayVM.Notes,
                ImagePath = registerPreviousDayVM.ImageUpload?.FileName,
                HasImage = registerPreviousDayVM.ImageUpload?.FileName.Length > 0,
                UserId = userId,
                Date = registerPreviousDayVM.Date
            }); ;


            var dateExist = context.StatsTables
                .Where(x => x.UserId == userId)
                .FirstOrDefault(x => x.Date == registerPreviousDayVM.Date);

            if (dateExist != null)
            {
                throw new Exception("date already exist!");
            }
            else context.SaveChanges();
        }

        internal OverviewVM GetOverViewVM()
        {
            var details = GetDetailsByDate(DateTime.Now);

            return new OverviewVM
            {
                Date = DateTime.Now,
                DateExist = details != null
            };
        }

        //----------Get details by date-------------//

        public StatsTable GetDetailsByDate(DateTime date)
        {
            string userId = userManager.GetUserId(
                accessor.HttpContext.User);

            var details = context.StatsTables
                .Where(x => x.UserId == userId)
                .FirstOrDefault(x => x.Date == date);

            return details;
        }

        //----------Read details-------------//

        internal DetailsVM GetDetailsVM(StatsTable stats)
        {

            return new DetailsVM
            {
                Id = stats.Id,
                Mood = stats.Mood,
                Stress = stats.Stress,
                Sleep = stats.Sleep,
                MentalHealth = stats.MentalHealth,
                PhysicalHealth = stats.PhysicalHealth,
                PhysicalActivity = stats.PhysicalActivity,
                Notes = stats.Notes,
                ImagePath = stats.ImagePath,
                Date = stats.Date
            };
        }

        //----------Update/Edit details-------------//

        internal void EditDetails(DetailsEditVM editVM, DateTime date)
        {
            if (editVM.ImageUpload != null)
            {
                var filePath = Path.Combine(webHostEnv.WebRootPath, "Uploads", editVM.ImageUpload.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    editVM.ImageUpload.CopyTo(fileStream);
                }
            }

            string userId = userManager.GetUserId(
                accessor.HttpContext.User);

            var dayToUpdate = context.StatsTables
                .Where(x => x.Date == date)
                .SingleOrDefault(x => x.UserId == userId);

            dayToUpdate.Mood = editVM.Mood;
            dayToUpdate.Stress = editVM.Stress;
            dayToUpdate.PhysicalActivity = editVM.PhysicalActivity;
            dayToUpdate.Sleep = editVM.Sleep;
            dayToUpdate.MentalHealth = editVM.MentalHealth;
            dayToUpdate.PhysicalHealth = editVM.PhysicalHealth;
            dayToUpdate.Notes = editVM.Notes;
            if (editVM.ImageUpload != null)
            {
                dayToUpdate.ImagePath = editVM.ImageUpload?.FileName;
                dayToUpdate.HasImage = editVM.ImageUpload?.FileName.Length > 0;
            }

            context.SaveChanges();
        }

        internal DetailsEditVM GetDetailsEditVM(StatsTable stats)
        {
            return new DetailsEditVM
            {
                Id = stats.Id,
                Mood = stats.Mood,
                Stress = stats.Stress,
                Sleep = stats.Sleep,
                MentalHealth = stats.MentalHealth,
                PhysicalHealth = stats.PhysicalHealth,
                PhysicalActivity = stats.PhysicalActivity,
                Notes = stats.Notes,
                ImagePath = stats.ImagePath,
                Date = stats.Date
                

            };
        }

        //----------Delete details-------------//

        public void DeleteDetailsByDateModal(DateTime date)
        {
            string userId = userManager.GetUserId(
                accessor.HttpContext.User);

            var dayToUpdateDelete = context.StatsTables
                .Where(x => x.Date == date)
                .SingleOrDefault(x => x.UserId == userId);

            context.StatsTables.Remove(dayToUpdateDelete);
            context.SaveChanges();
        }


        //-------------------Charts---------------------//

        //DataContract for Serializing Data - required to serve in JSON format
        [DataContract]
        public class DataPoint
        {
            public DataPoint(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "x")]
            public Nullable<double> x = null;

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> y = null;
        }

        internal List<ChartsVM> GetDataToCharts()
        {
            string userId = userManager.GetUserId(
                accessor.HttpContext.User);

            var statsTable = context.StatsTables
                .Where(x => x.UserId == userId)
                .Select(x => new ChartsVM
                {
                    Mood = x.Mood,
                    Sleep = x.Sleep,
                    PhysicalActivity = x.PhysicalActivity,
                    PhysicalHealth = x.PhysicalHealth,
                    Stress = x.Stress,
                    MentalHealth = x.MentalHealth,
                    Date = x.Date
                })
                .OrderBy(x => x.Date)
                .ToList();

            return statsTable;
        }


    }

}
