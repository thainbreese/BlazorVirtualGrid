using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.App.Services
{
    public class GridHScrollService
    {
        public int TotalWidth;
        public int ViewWidth;
        public int Track; // Width of horizontal scrollbar track
        public int ThumbSize; // Width of horizontal scrollbar thumb
        public int ThumbPosition; // Left position of scrollbar thumb
        public int StartOffset; // Offset by mouse click point
        public int TableLeftPosition;

        public GridHScrollService()
        {
            ThumbPosition = 18;
        }

        public void SetHScrollbar(int width, int totalWidth)
        {
            TotalWidth = totalWidth;
            Track = width - 18 * 2;
            ViewWidth = width;
            ThumbSize = Math.Min((int)((float)width * Track / totalWidth), Track);
            SetTableLeftPosition();
        }

        public void ScrollStart(int clientX)
        {
            StartOffset = ThumbPosition - clientX;
        }

        public void Scroll(int clientX)
        {
            ThumbPosition = StartOffset + clientX;
            SetTableLeftPosition();
        }

        public void ScrollMove(int clientX)
        {
            ThumbPosition = clientX - ThumbSize / 2;
            SetTableLeftPosition();
        }

        public void ScrollChange(int movement)
        {
            ThumbPosition += (int)((float)movement * (Track - ThumbSize) / (TotalWidth - ViewWidth));
            SetTableLeftPosition();
        }

        public void ScrollTo(int position)
        {
            TableLeftPosition = -position;
            ThumbPosition = (int)((float)position * (Track - ThumbSize) / (TotalWidth - ViewWidth)) + 18;
        }

        public void ScrollLeft(int rowPoint)
        {
            if (rowPoint + TableLeftPosition < 0)
            {
                TableLeftPosition = -rowPoint;
                ThumbPosition = (int)((float)rowPoint * (Track - ThumbSize) / (TotalWidth - ViewWidth)) + 18;
            }
        }

        public void Validate()
        {
            ThumbPosition = Math.Max(18, ThumbPosition);
            ThumbPosition = Math.Min(ThumbPosition, Track - ThumbSize + 18);
        }

        public void SetTableLeftPosition()
        {
            Validate();
            if (Track != ThumbSize)
            {
                TableLeftPosition = -(int)((float)(ThumbPosition - 18) * (TotalWidth - ViewWidth) / (Track - ThumbSize));
            }
        }
    }
}
