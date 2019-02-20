using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.App.Services
{
    public class GridService
    {
        public int[] ColumnWidths; // Width array of grid columns.
        public GridItem[] Data; // Records data
        public int TotalCount; // Total records count

        public int RowHeight; // Height of each row.
        public int Width; // Grid width
        public int Height; // Grid height
        public int RowsCount; // Grid rendering rows count

        public int StartRecord; // Grid rendering start row index / 0 to TotalCount-RowsCount
        public int StartRecordOffset; // Grid rendering start row offset / less than or equal to zero

        public GridHScrollService HScrollService; // Service for horizontal scroll
        public GridVScrollService VScrollService; // Service for vertical scroll

        public bool IsResizing; // Status of resizing
        public int ResizingColumn; // Resizing column index
        public int ResizingOffset; // Resizing column offset by mouse click point

        public bool IsHScrolling;
        public bool IsVScrolling;

        public int SelectedRow;
        public int SelectedColumn;

        public GridService()
        {
            StartRecord = 0;
            RowHeight = 31;
            HScrollService = new GridHScrollService();
            VScrollService = new GridVScrollService();
            SelectedRow = -1;
            SelectedColumn = -1;
        }        

        /// <summary>
        /// Table set
        /// </summary>
        /// <param name="data"></param>
        /// 

        public void SetColumnWidths(int[] columnWidths)
        {
            ColumnWidths = new int[columnWidths.Length];
            Array.Copy(columnWidths, ColumnWidths, columnWidths.Length);
        }
        public void SetData(GridItem[] data)
        {
            Data = new GridItem[data.Length];
            Array.Copy(data, Data, data.Length);
            TotalCount = data.Count();
        }
        public GridItem[] GetRenderingData()
        {
            return Data.Skip(StartRecord).Take(RowsCount).ToArray();
        }
        public void SetDimension(int width, int height)
        {
            Width = width;
            Height = height;
            RowsCount = (int)Math.Ceiling((float)height / RowHeight) - 1;

            SetHScroll();
            SetVScroll();
        }

        /// <summary>
        /// Keyboard navigation
        /// </summary>
        public void CursorUp()
        {
            if (SelectedRow > 0)
            {
                SelectedRow--;
                if (SelectedRow <= StartRecord)
                {
                    StartRecordOffset = 0;
                    StartRecord = SelectedRow;
                }
                else
                {
                    int RowYPoint = RowHeight * (SelectedRow - StartRecord + 1) + StartRecordOffset;
                    int ViewHeight = Height - RowHeight;
                    if (RowYPoint > ViewHeight)
                    {
                        StartRecord = SelectedRow - RowsCount + 1;
                        StartRecordOffset = ViewHeight - RowHeight * RowsCount;
                    }
                }
                VScrollService.ScrollTo(StartRecord, StartRecordOffset);
            }
        }
        public void CursorDown()
        {
            if (SelectedRow < TotalCount - 1)
            {
                SelectedRow++;
                if (SelectedRow > StartRecord)
                {
                    int RowYPoint = RowHeight * (SelectedRow - StartRecord + 1) + StartRecordOffset;
                    int ViewHeight = Height - RowHeight;
                    if (RowYPoint > ViewHeight)
                    {
                        StartRecord = SelectedRow - RowsCount + 1;
                        StartRecordOffset = ViewHeight - RowHeight * RowsCount;
                    }
                }
                if (SelectedRow < StartRecord)
                {
                    StartRecordOffset = 0;
                    StartRecord = SelectedRow;
                }
                VScrollService.ScrollTo(StartRecord, StartRecordOffset);
            }
        }
        public void CursorLeft()
        {
            if (SelectedColumn > 0)
            {
                SelectedColumn--;
                int RowXPoint = ColumnWidths.Skip(2).Take(SelectedColumn - 1).Sum();
                HScrollService.ScrollLeft(RowXPoint);
            }
        }
        public void CursorRight()
        {
            if (SelectedColumn < typeof(GridItem).GetProperties().Count() - 1)
            {
                SelectedColumn++;
                int RowXPoint = ColumnWidths.Take(SelectedColumn + 2).Sum();
                if (RowXPoint > Width)
                {
                    HScrollService.ScrollTo(RowXPoint - Width);
                }
            }
        }

        /// <summary>
        /// Hscroll events
        /// </summary>
        public int GetTableLeftPosition()
        {
            return HScrollService.TableLeftPosition;
        }
        public bool HScrolling()
        {
            return IsHScrolling;
        }
        public void SetHScroll()
        {
            int totalWidth = ColumnWidths.Sum();
            HScrollService.SetHScrollbar(Width, totalWidth);
        }
        public int GetHScrollbarThumbSize()
        {
            return HScrollService.ThumbSize;
        }
        public int GetHScrollbarThumbPosition()
        {
            return HScrollService.ThumbPosition;
        }
        public bool IsHScrollbar()
        {
            return HScrollService.ThumbSize != HScrollService.Track;
        }
        public void HScrollStart(int clientX)
        {
            IsHScrolling = true;
            HScrollService.ScrollStart(clientX);
        }
        public void HScroll(int clientX)
        {
            HScrollService.Scroll(clientX);
        }
        public void HScrollMove(int clientX)
        {
            HScrollService.ScrollMove(clientX);
        }
        public void HScrollByWheel(int deltaY)
        {
            int movement = deltaY / 100 * 50;
            HScrollService.ScrollChange(movement);
        }
        public void HScrollLeft()
        {
            HScrollService.ScrollChange(-40);
        }
        public void HScrollRight()
        {
            HScrollService.ScrollChange(40);
        }


        /// <summary>
        /// VScroll events
        /// </summary>
        public bool VScrolling()
        {
            return IsVScrolling;
        }
        public void SetVScroll()
        {
            int totalHeight = RowHeight * TotalCount;
            VScrollService.SetVScrollbar(Height, totalHeight, RowHeight);
        }
        public int GetVScrollbarThumbSize()
        {
            return VScrollService.ThumbSize;
        }
        public int GetVScrollbarThumbPosition()
        {
            return VScrollService.ThumbPosition;
        }
        public bool IsVScrollbar()
        {
            return VScrollService.ThumbSize != VScrollService.Track;
        }
        public void VScrollStart(int clientY)
        {
            IsVScrolling = true;
            VScrollService.ScrollStart(clientY);
        }
        public void VScroll(int clientY)
        {
            VScrollService.Scroll(clientY);
            SetStartRecord();
        }
        public void VScrollMove(int clientY)
        {
            VScrollService.ScrollMove(clientY);
            SetStartRecord();
        }
        public void VScrollByWeel(int deltaY)
        {
            int movement = deltaY / 100 * 50;
            VScrollService.ScrollChange(movement);
            SetStartRecord();
        }
        public void VScrollUp()
        {
            VScrollService.ScrollChange(-40);
            SetStartRecord();
        }
        public void VScrollDown()
        {
            VScrollService.ScrollChange(40);
            SetStartRecord();
        }
        public void SetStartRecord()
        {
            StartRecord = VScrollService.StartRecord;
            StartRecordOffset = VScrollService.StartRecordOffset;
        }

        /// <summary>
        /// Resize events
        /// </summary>
        public void ResizeStart(int column, int clientX)
        {
            ResizingColumn = column;
            ResizingOffset = ColumnWidths[column] - clientX;
            IsResizing = true;
        }

        public void Resize(int clientX)
        {
            int newWidth = Math.Max(ResizingOffset + clientX, 35);
            ColumnWidths[ResizingColumn] = newWidth;
            SetHScroll();
        }

        /// <summary>
        /// Table item select
        /// </summary>
        public void SelectItem(int row, int column)
        {
            SelectedRow = StartRecord + row;
            SelectedColumn = column;
        }
    }
}
    