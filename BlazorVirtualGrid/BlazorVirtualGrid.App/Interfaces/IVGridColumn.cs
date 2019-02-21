using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGrid.App.Interfaces
{
    public interface IVGridColumn
    {
        RenderFragment GridColumn { get; }
    }
}
