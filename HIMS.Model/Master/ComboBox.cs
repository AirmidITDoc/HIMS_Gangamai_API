using System.Collections.Generic;
using System.Data;

namespace HIMS.Model.Master
{
    public class ComboBox
    {
        public ComboBox()
        {
            Items = new List<OptionDetails>();
        }
        public List<OptionDetails> Items { get; set; }
        public int SelectedIndex { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public DataTable DataSource { get; set; }
        public int SelectedValue { get; set; }
    }

    public class OptionDetails
    {
        public int iNameId;
        public string sName;

        public OptionDetails(string someword)
        {
            sName = someword;
        }
        public override string ToString()
        {
            return sName;
        }
    }
}
