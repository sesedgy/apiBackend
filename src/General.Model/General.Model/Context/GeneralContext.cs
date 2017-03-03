using System.Data.Entity;

namespace PoiskIT.Okenit2.General.Context
{
    /// <summary>
    /// Основной контекст.
    /// Содержит все сущности. На его основе создается модель БД и миграции.
    /// Не рекомендуется к прямому использованию.
    /// </summary>
    public class GeneralContext : DbContext
    {
        public GeneralContext() : base("DefaultConnection")
        {

        }

        public DbSet<Blog> Blogs { get; set; }

    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
    }

}

