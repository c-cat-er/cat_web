namespace modpackApi.DTO
{
    public class CustomizedDTO
    {
        public int CustomizedId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public float SalePrice { get; set; }
        public List<CustomizedSpec> CustomizedSpecs { get; set; }
        public class CustomizedSpec
        {
            public int CustomizedSpecificationId { get; set; }
            public int ComponentId { get; set; }
            public int? MaterialId { get; set; }
            public int? ColorId { get; set; }
            public int? Location { get; set; }
            public int? SizeX { get; set; }
            public int? SizeY { get; set; }
        }
        public List<CustomizedComponent> CustomizedComponents { get; set; }
        public class CustomizedComponent
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
