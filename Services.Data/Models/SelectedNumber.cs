using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class SelectedNumber
    {
        public int SelectedNumberId { get; set; }
        public int MasterDataId { get; set; }
        public int ColumnsNumber { get; set; }

        public virtual MasterDatum MasterData { get; set; } = null!;
    }
}
