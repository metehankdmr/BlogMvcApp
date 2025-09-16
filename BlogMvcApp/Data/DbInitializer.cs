using BlogMvcApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace BlogMvcApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogContext context)
        {
            // Veritabanının oluşturulduğundan emin ol
            context.Database.EnsureCreated();

            // Users tablosu var mı kontrol et
            try
            {
                // Kullanıcılar zaten varsa seed yapma
                if (context.Users.Any())
                {
                    return;
                }
            }
            catch (Exception)
            {
                // Users tablosu yoksa, migration gerekli
                Console.WriteLine("Users tablosu bulunamadı. Lütfen migration oluşturun: Add-Migration AddUserTable");
                return;
            }

            // Resul ve Mete kullanıcılarını ekle
            var users = new User[]
            {
                new User
                {
                    Username = "resul",
                    Password = HashPassword("resul123"),
                    Name = "Resul",
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Username = "mete",
                    Password = HashPassword("mete123"),
                    Name = "Mete",
                    CreatedAt = DateTime.Now
                }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
