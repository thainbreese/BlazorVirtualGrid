using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGrid.App.Services
{
    public class GridColumnProperty
    {
        public int Width { get; set; }
        public bool StickToLeft { get; set; }
        public bool StickToRight { get; set; }
        public string Field { get; set; }
    }
}
