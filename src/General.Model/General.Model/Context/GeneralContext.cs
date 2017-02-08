using System.Data.Entity;

namespace PoiskIT.Okenit2.General.Context
{
    /// <summary>
    /// Основной контекст.
    /// Содержит все сущности. На его основе создается модель БД и миграции.
    /// Не рекомендуется к прямому использованию.
    /// </summary>
    internal class GeneralContext : DbContext
    {
    }
}
