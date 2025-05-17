using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class Item
    {
        public Item()
        {
            SelectedItems = new HashSet<SelectedItem>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public bool? ItemInUse { get; set; }

        public virtual ICollection<SelectedItem> SelectedItems { get; set; }
    }
}
