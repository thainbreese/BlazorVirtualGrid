using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.App.Services
{
    public static class GridJSInterop
    {
        public static Task<int> GetWidth(this ElementRef elementRef)
        {
            return JSRuntime.Current.InvokeAsync<int>("gridInterop.getWidth", elementRef);
        }

        public static Task<int> GetHeight(this ElementRef elementRef)
        {
            return JSRuntime.Current.InvokeAsync<int>("gridInterop.getHeight", elementRef);
        }

        public static Task<int> GetTop(this ElementRef elementRef)
        {
            return JSRuntime.Current.InvokeAsync<int>("gridInterop.getTop", elementRef);
        }

        public static Task<int> GetLeft(this ElementRef elementRef)
        {
            return JSRuntime.Current.InvokeAsync<int>("gridInterop.getLeft", elementRef);
        }
    }
}
