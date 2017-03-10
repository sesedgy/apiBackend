using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PoiskIT.Okenit2.General.Context
{
    public abstract class BoundedContextBase<TContext> : DbContext
        where TContext : DbContext
    {
        static BoundedContextBase()
        {
            // Запрещаем любую инициализацию БД, дабы связанные контексты ее не повредили случайно
            Database.SetInitializer<TContext>(null);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionStringOrName">Строка соединения или ее название в конфигурационном файле</param>
        protected BoundedContextBase(string connectionStringOrName)
            : base(connectionStringOrName)
        {
        }

        /// <summary>
        /// Устанавливает базовые настройки модели
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
