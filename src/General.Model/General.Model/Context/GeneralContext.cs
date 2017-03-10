using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PoiskIT.Okenit2.General.Context
{
    /// <summary>
    /// Основной контекст.
    /// Содержит все сущности. На его основе создается модель БД и миграции.
    /// Не рекомендуется к прямому использованию.
    /// </summary>
    internal sealed class GeneralContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
