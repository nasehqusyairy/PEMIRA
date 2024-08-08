using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class MenuSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(Menu);

        public override void Seed()
        {
            if (DBContext.Menus.Any()) return;

            List<Menusegment> menusegments = [.. DBContext.Menusegments];
            List<Menu> menus =
            [
                new()
                {
                    Name = "Beranda",
                    Icon = "house",
                    MenusegmentId = menusegments[0].Id
                },
                new()
                {
                    Name = "Voting",
                    Icon = "person-check",
                    Url = "/Vote/",
                    MenusegmentId = menusegments[0].Id
                },
                new() {
                    Name = "Monitoring",
                    Icon = "display",
                    Url = "/Monitoring/",
                    MenusegmentId = menusegments[1].Id
                },
                new() {
                    Name = "Kandidat",
                    Icon = "person-badge",
                    Url = "/Candidate/",
                    MenusegmentId = menusegments[1].Id
                },
                new() {
                    Name = "Organisasi",
                    Icon = "buildings",
                    Url = "/Organization/",
                    MenusegmentId = menusegments[1].Id
                },
                new() {
                    Name = "Peserta",
                    Icon = "person-gear",
                    Url = "/User/",
                    MenusegmentId = menusegments[1].Id
                },
                new() {
                    Name = "Token",
                    Icon = "shield-exclamation",
<<<<<<< Updated upstream
                    Url = "/Token/",
                    MenusegmentId = menusegments[1].Id
=======
                    Url = "/Token/Index",
                    MenusegmentId = menusegments[1].Id 
                },
                new() {
                    Name = "Menu Setting",
                    Icon = "gear",
                    Url = "/MenuSetting/Index",
                    MenusegmentId = menusegments[2].Id 
>>>>>>> Stashed changes
                }
            ];
            DBContext.Menus.AddRange(menus);
            DBContext.SaveChanges();
        }
    }
}