using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace OfficeSuppliesManagementSystem.Models
{
    public class Supply
    {
        #region 枚举类型定义
        public enum ESupplyType
        {
            [Display(Name = "纸张")]
            Paper = 0x0,

            [Display(Name = "文具")]
            Stationery = 0x1,

            [Display(Name = "刀具")]
            Knife = 0x2,

            [Display(Name = "单据")]
            Bill = 0x4,

            [Display(Name = "礼品")]
            Gift = 0x8,

            [Display(Name = "其它")]
            Others = 0x10
        }
        public enum EProvince
        {
            [Display(Name = "")]
            Unset = 0,
            [Display(Name = "北京市")]
            Beijing = 1,
            [Display(Name = "天津市")]
            Tianjing = 2,
            [Display(Name = "重庆市")]
            Chongqing = 3,
            [Display(Name = "上海市")]
            Shanghai = 4,
            [Display(Name = "河北省")]
            Hebei = 5,
            [Display(Name = "山西省")]
            Shanxi = 6,
            [Display(Name = "辽宁省")]
            Liaoning = 7,
            [Display(Name = "吉林省")]
            Jilin = 8,
            [Display(Name = "黑龙江省")]
            Heilongjiang = 9,
            [Display(Name = "江苏省")]
            Jiangsu = 10,
            [Display(Name = "浙江省")]
            Zhejiang = 11,
            [Display(Name = "安徽省")]
            Anhui = 12,
            [Display(Name = "福建省")]
            Fujian = 13,
            [Display(Name = "江西省")]
            Jiangxi = 14,
            [Display(Name = "山东省")]
            Shandong = 15,
            [Display(Name = "河南省")]
            Henan = 16,
            [Display(Name = "湖北省")]
            Hubei = 17,
            [Display(Name = "湖南省")]
            Hunan = 18,
            [Display(Name = "广东省")]
            Guangdong = 19,
            [Display(Name = "海南省")]
            Hainan = 20,
            [Display(Name = "四川省")]
            Sichuan = 21,
            [Display(Name = "贵州省")]
            Guizhou = 22,
            [Display(Name = "云南省")]
            Yunnan = 23,
            [Display(Name = "陕西省")]
            Shaanxi = 24,
            [Display(Name = "甘肃省")]
            Gansu = 25,
            [Display(Name = "青海省")]
            Qinghai = 26,
            [Display(Name = "台湾省")]
            Taiwan = 27,
            [Display(Name = "内蒙古自治区")]
            Neimenggu = 28,
            [Display(Name = "广西壮族自治区")]
            Guangxi = 29,
            [Display(Name = "西藏自治区")]
            Xizang = 30,
            [Display(Name = "宁夏回族自治区")]
            Ningxia = 31,
            [Display(Name = "新疆维吾尔自治区")]
            Xinjiang = 32,
            [Display(Name = "香港特别行政区")]
            Xianggang = 33,
            [Display(Name = "澳门特别行政区")]
            Aomen = 34
        }

        #endregion 枚举类型定义

        #region 字段
        [Display(Name = "物品编码")]
        public int Id { set; get; }

        [Display(Name = "物品名称")]
        public string Name { set; get; }

        [Display(Name = "物品类别")]
        public ESupplyType Type { set; get; }

        [Display(Name = "产地")]
        public EProvince Province { set; get; }

        [Display(Name = "规格")]
        public string Specification { set; get; }

        [Display(Name = "型号")]
        public string Serial { set; get; }
        
        [Display(Name = "照片")]
        public string PhotoUrl { get; set; } = "\\wwwroot\\images\\Supply\\default.jpg";

        #endregion 字段
    }
}
