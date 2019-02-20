using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.App.Services
{
    public class GridVScrollService
    {
        public int TotalHeight; // Height of all records
        public int RowHeight; // Height of each row
        public int ViewHeight;
        public int Track; // Height of vertical scrollbar track
        public int ThumbSize; // Height of vertical scrollbar thumb
        public int ThumbPosition; // Top position of scrollbar thumb
        public int StartOffset; // Offset by mouse click point

        public int StartRecord;
        public int StartRecordOffset;

        public GridVScrollService()
        {
            ThumbPosition = 18;
        }

        public void SetVScrollbar(int height, int totalHeight, int rowHeight)
        {
            TotalHeight = totalHeight;
            RowHeight = rowHeight;
            Track = height - 18 * 2;
            ViewHeight = height - RowHeight;
            ThumbSize = (int)((float)height * Track / totalHeight);
        }

        public void ScrollStart(int clientY)
        {
            StartOffset = ThumbPosition - clientY;
        }

        public void Scroll(int clientY)
        {
            ThumbPosition = StartOffset + clientY;
            SetTableTopPosition();
        }

        public void ScrollMove(int clientY)
        {
            ThumbPosition = clientY - ThumbSize / 2;
            SetTableTopPosition();
        }

        public void ScrollChange(int movement)
        {
            ThumbPosition += (int)((float)movement * (Track - ThumbSize) / (TotalHeight - ViewHeight));
            SetTableTopPosition();
        }

        public void ScrollTo(int startRecord, int startRecordOffset)
        {
            int totalMovement = RowHeight * startRecord - startRecordOffset;
            ThumbPosition = (int)((float)(Track - ThumbSize) * totalMovement / (TotalHeight - ViewHeight)) + 18;
            Validate();
        }

        public void SetTableTopPosition()
        {
            Validate();
            int tableTopPosition = (int)((float)(ThumbPosition - 18) * (TotalHeight - ViewHeight) / (Track - ThumbSize));
            StartRecord = tableTopPosition / RowHeight;
            StartRecordOffset = -(tableTopPosition % RowHeight);
        }
        public void Validate()
        {
            ThumbPosition = Math.Max(18, ThumbPosition);
            ThumbPosition = Math.Min(ThumbPosition, Track - ThumbSize + 18);
        }
    }
}
