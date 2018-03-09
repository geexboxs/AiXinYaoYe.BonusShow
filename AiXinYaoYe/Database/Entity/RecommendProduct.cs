using System.ComponentModel.DataAnnotations;

namespace AiXinYaoYe.Database.Entity
{
    public partial class RecommandProduct
    {
        public RecommandProduct()
        {
            this.Price = 0m;
        }

        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "简介")]
        public string Desc { get; set; }
        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "详情长图")]
        [DataType(DataType.ImageUrl)]
        [UIHint("ImageUrl")]
        public string DetailPics { get; set; }
        [Display(Name = "封面图")]
        [DataType(DataType.ImageUrl)]
        [UIHint("ImageUrl")]
        public string CoverImage { get; set; }
    }
}
