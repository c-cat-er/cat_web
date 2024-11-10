namespace modpackApi.DTO
{
    public class ComponentsDTO
    {
        public int ComponentID { get; set; }
        public int MaterialID { get; set; }
        public string MateriaName { get; set; }
        public string MateriaFileName { get; set; }
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public string ColorRGB { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public float OriginalPrice { get; set; }
        public string FBXFileName { get; set; }
        public string ImageFileName { get; set; }
        public bool IsCustomized { get; set; }
        public List<ProductCategory> productCategories { get; set; }
        public class ProductCategory
        {
            public int CategoryID { get; set; }
            public string Name { get; set; }
        }
    }
}
