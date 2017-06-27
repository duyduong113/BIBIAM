namespace ERPAPD.Models
{
    public class DropDownListData
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public DropDownListData() {  }

        public DropDownListData(int Code, string Name)
        {
            this.Code = Code;
            this.Name = Name;
        }
    }

    public class DropDownListDataList
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public DropDownListDataList() { }

        public DropDownListDataList(string Code, string Name)
        {
            this.Code = Code;
            this.Name = Name;
        }
    }

    public class DropDownListValue
    {
        public int Value { get; set; }
        public string Text { get; set; }

        public DropDownListValue() { }

        public DropDownListValue(int Value, string Text)
        {
            this.Value = Value;
            this.Text = Text;
        }
    }
}