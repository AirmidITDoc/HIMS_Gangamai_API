using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Users
{
    public class FavouriteModel
    {
        public Int64 UserId { get; set; }
        public string LinkName { get; set; }
        public int MenuId { get; set; }
        public string LinkAction { get; set; }
        public string Icon { get; set; }
        public bool IsFavourite { get; set; }
    }
}
