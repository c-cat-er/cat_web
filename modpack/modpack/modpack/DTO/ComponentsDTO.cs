using System.ComponentModel;

namespace modpack.DTO
{
    public class ComponentsDTO
    {

        [DisplayName("編號")]
        public int ComponentID { get; set; }
        [DisplayName("材質")]
        public int MaterialID { get; set; }
        [DisplayName("材質")]
        public string MateriaName { get; set; }
        public string MateriaFileName { get; set; }
        [DisplayName("顏色")]
        public int ColorID { get; set; }

        [DisplayName("顏色")]
        public string ColorName { get; set; }
        public string ColorRGB { get; set; }
        [DisplayName("狀態")]
        public int StatusID { get; set; }
        [DisplayName("狀態")]
        public string StatusName { get; set; }
        [DisplayName("名稱")]
        public string Name { get; set; }
        [DisplayName("元件分類")]
        public string Category { get; set; }
        [DisplayName("原價")]
        public float OriginalPrice { get; set; }
        [DisplayName("FBX檔名")]
        public string FBXFileName { get; set; }
        [DisplayName("元件圖片")]
        public string ImageFileName { get; set; }
        
        [DisplayName("能否客製化")]
        public bool IsCustomized { get; set; }

        public List<ProductCategory> productCategories { get; set; }
        public class ProductCategory
        {
            public int CategoryID { get; set; }
            [DisplayName("產品分類")]
            public string Name { get; set; }
        }
        public string productCategoriesjson { get; set; }
    }
}
