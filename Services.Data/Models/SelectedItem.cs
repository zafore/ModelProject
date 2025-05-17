using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class SelectedItem
    {
        public int SelectedItemsId { get; set; }
        public int MasterDataId { get; set; }
        public int ItemId { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual MasterDatum MasterData { get; set; } = null!;
    }
}
