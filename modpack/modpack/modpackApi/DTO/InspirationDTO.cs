namespace modpackApi.DTO
{
    public class InspirationDTO
    {
        public int InspirationId { get; set; }
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public float SalePrice { get; set; }
        public List<InspirationSpec> InspirationSpecs { get; set; }
        public class InspirationSpec
        {
            public int InspirationSpecificationId { get; set; }
            public int ComponentId { get; set; }
            public int? MaterialId { get; set; }
            public int? ColorId { get; set; }
            public int? Location { get; set; }
            public int? SizeX { get; set; }
            public int? SizeY { get; set; }
        }
        public List<InspirationComponent> InspirationComponents { get; set; }
        public class InspirationComponent
        {
            public int ComponentId { get; set; }
            public int MaterialId { get; set; }
            public int ColorId { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public float OriginalPrice { get; set; }
            public string FBXFileName { get; set; }
            public string ImageFileName { get; set; }
            public bool IsCustomized { get; set; }
        }
    }
}
