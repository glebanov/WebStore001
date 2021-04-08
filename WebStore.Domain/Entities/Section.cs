using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))] // Можно указать вручную внешний ключ
        public Section Parent { get; set; } // Свойство независимостей

        public ICollection<Product> Products { get; set; } 
    }
}
