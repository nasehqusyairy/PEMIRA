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
                    Url = "/Home/Index",
                    MenusegmentId = menusegments[0].Id
                },
                new() 
                {
                    Name = "Voting",
                    Icon = "person-check",
                    Url = "/Vote/Index",
                    MenusegmentId = menusegments[0].Id
                },
                new() {
                    Name = "Monitoring",
                    Icon = "display",
                    Url = "/Monitoring/Index",
                    MenusegmentId = menusegments[1].Id 
                },
                new() {
                    Name = "Kandidat",
                    Icon = "person-badge",
                    Url = "/Candidate/Index",
                    MenusegmentId = menusegments[1].Id 
                },
                new() {
                    Name = "Organisasi",
                    Icon = "buildings",
                    Url = "/Organization/Index",
                    MenusegmentId = menusegments[1].Id 
                },
                new() {
                    Name = "Peserta",
                    Icon = "person-gear",
                    Url = "/User/Index",
                    MenusegmentId = menusegments[1].Id 
                },
                new() {
                    Name = "Token",
                    Icon = "shield-exclamation",
                    Url = "/Token/Index",
                    MenusegmentId = menusegments[1].Id 
                }
            ];
            DBContext.Menus.AddRange(menus);
            DBContext.SaveChanges();
        }
    }
}