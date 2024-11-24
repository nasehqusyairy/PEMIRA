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
                    Id = 70536,
                    Name = "Pemira 2021"
                },
                new(){
                    Id=70537,
                    Name = "Pemira 2022"
                },
            ];

            // looping untuk menambahkan item dengan id urut
            // int count = 100;
            // long lastId = items.Last().Id;
            // for (long i = lastId; i < lastId + count; i++)
            // {
            //     items.Add(new Election
            //     {
            //         Id = i + 1,
            //         Name = $"Pemira {i}"
            //     });
            // }

            DBContext.Elections.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
