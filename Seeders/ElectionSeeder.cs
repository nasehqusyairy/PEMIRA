using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class ElectionSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(Election);

        public override void Seed()
        {
            if (DBContext.Elections.Any()) return;

            List<Election> items = [
                new(){
                    Name = "Pemira 2021"
                },
                new(){
                    Name = "Pemira 2022"
                },
            ];
            DBContext.Elections.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
