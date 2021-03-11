using System;

using CassaAssistenzaSanitaria.Models;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
