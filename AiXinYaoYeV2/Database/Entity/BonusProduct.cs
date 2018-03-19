using System.ComponentModel.DataAnnotations;

namespace AiXinYaoYeV2.Database.Entity
{
    public partial class BonusProduct
    {
        public BonusProduct()
        {
            this.Bonus = 0m;
        }

        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "简介")]
        public string Desc { get; set; }
        [Display(Name = "兑换积分")]
        public decimal Bonus { get; set; }
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