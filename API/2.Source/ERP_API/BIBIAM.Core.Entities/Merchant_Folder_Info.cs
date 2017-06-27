using ServiceStack.DataAnnotations;
using System;


namespace BIBIAM.Core.Entities
{
    public class Merchant_Folder_Info: BaseEntity
    {
        public string ma_gian_hang { get; set; }
        public string ten_thu_muc { get; set; }
        public string trang_thai { get; set; }
    }
}
