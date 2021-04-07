using System.Collections.Generic;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    //[Table("Brands")] //- Добавляем таблицу, где будет храниться набор сущностей
    public class Brand : NamedEntity, IOrderedEntity
    {
        //[Column("BrandOrder")] //- Можно указать из какого столбца данные будут загружены
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
