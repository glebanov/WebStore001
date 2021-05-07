namespace WebStore.Domain
{
    public class ProductFilter
    {
        public int? SectionId { get; init; }

        public int? BrandId { get; init; }

        public int[] Ids { get; set; }

        public int Page { get; set; }

        public int? PageSize { get; set; }
    }
}
