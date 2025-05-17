using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class MasterDatum
    {
        public MasterDatum()
        {
            SelectedItems = new HashSet<SelectedItem>();
            SelectedNumbers = new HashSet<SelectedNumber>();
        }

        public int MasterDataId { get; set; }
        public string MasterDataName { get; set; } = null!;
        public int ColumnsNumberCount { get; set; }
        public bool? InUse { get; set; }

        public virtual ICollection<SelectedItem> SelectedItems { get; set; }
        public virtual ICollection<SelectedNumber> SelectedNumbers { get; set; }
    }
}
