using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using BorderStyle = NPOI.SS.UserModel.BorderStyle;
using DataGrid = System.Windows.Controls.DataGrid;
using DataGridCell = System.Windows.Controls.DataGridCell;

namespace HistorydataPolling.Common.FileCtrol.Common
{
   
    public static class ExportToExcel2
    {
        public static void EE(DataGrid dg, string Title, string date = "")
        {
            int rowCount = dg.Items.Count;//行数
            int columnCount = dg.Columns.Count;//列数

            HSSFWorkbook workbook = new HSSFWorkbook();//创建workbook对象
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
            sheet.DefaultColumnWidth = 15;//设置默认的宽度

            //定义title的font和cell样式
            HSSFFont fontTitle = SheetStyle.GetFont(workbook, 24, FontBoldWeight.BOLD);
            HSSFCellStyle styleTitle = SheetStyle.GetStyle(workbook);
            styleTitle.SetFont(fontTitle);

            //定义date的font和cell样式
            HSSFFont fontDate = SheetStyle.GetFont(workbook, 12);
            HSSFCellStyle styleDate = SheetStyle.GetStyle(workbook);
            styleDate.SetFont(fontDate);

            //定义header的font和cell样式
            HSSFFont fontHeader = SheetStyle.GetFont(workbook, 12, FontBoldWeight.BOLD);
            HSSFCellStyle styleHeaderLT = SheetStyle.GetStyle(workbook, CellPosition.LeftTop);//左上
            styleHeaderLT.SetFont(fontHeader);
            HSSFCellStyle styleHeaderRT = SheetStyle.GetStyle(workbook, CellPosition.RightTop);//右上
            styleHeaderRT.SetFont(fontHeader);
            HSSFCellStyle styleHeaderT = SheetStyle.GetStyle(workbook, CellPosition.Top);//上
            styleHeaderT.SetFont(fontHeader);

            //定义content的font和cell样式
            HSSFFont fontContent = SheetStyle.GetFont(workbook, 12);
            HSSFCellStyle styleContentL = SheetStyle.GetStyle(workbook, CellPosition.Left);//左
            styleContentL.SetFont(fontContent);
            HSSFCellStyle styleContentR = SheetStyle.GetStyle(workbook, CellPosition.Right);//右
            styleContentR.SetFont(fontContent);
            HSSFCellStyle styleContentC = SheetStyle.GetStyle(workbook, CellPosition.Center);//中
            styleContentC.SetFont(fontContent);
            HSSFCellStyle styleContentLB = SheetStyle.GetStyle(workbook, CellPosition.LeftBottom);//左下
            styleContentLB.SetFont(fontContent);
            HSSFCellStyle styleContentRB = SheetStyle.GetStyle(workbook, CellPosition.RightBottom);//右下
            styleContentRB.SetFont(fontContent);
            HSSFCellStyle styleContentB = SheetStyle.GetStyle(workbook, CellPosition.Bottom);//下
            styleContentB.SetFont(fontContent);

            //标题
            HSSFRow rowTitle = (HSSFRow)sheet.CreateRow(0);
            HSSFCell cellTitle = (HSSFCell)rowTitle.CreateCell(0);
            rowTitle.HeightInPoints = 46.5F;//设置行高
            cellTitle.CellStyle = styleTitle;//设置标题样式
            cellTitle.SetCellValue(Title);//给标题位置赋值
            cellTitle.CellStyle = styleTitle;
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, columnCount - 1));//合并单元格，行为一行，列为DataGrid的列数

            int num_date = 1;//判断是否有日期这一行
                             //制表时间
            if (!string.IsNullOrEmpty(date))
            {
                HSSFRow rowDate = (HSSFRow)sheet.CreateRow(num_date);
                rowDate.HeightInPoints = 19.5F;
                HSSFCell cellDate = (HSSFCell)rowDate.CreateCell(columnCount - 2);//位置在列数减2的格上
                cellDate.CellStyle = styleDate;
                cellDate.SetCellValue(date);
                sheet.AddMergedRegion(new CellRangeAddress(1, 1, columnCount - 2, columnCount - 1));
                num_date++;
            }

            //添加表头
            HSSFRow rowHeader = (HSSFRow)sheet.CreateRow(num_date);
            rowHeader.HeightInPoints = 30;
            for (int i = 0; i < columnCount; i++)
            {
                HSSFCell cellHeader = (HSSFCell)rowHeader.CreateCell(i);
                if (i == 0)
                {
                    cellHeader.CellStyle = styleHeaderLT;
                }
                else if (i == columnCount - 1)
                {
                    cellHeader.CellStyle = styleHeaderRT;
                }
                else
                {
                    cellHeader.CellStyle = styleHeaderT;
                }
                cellHeader.SetCellValue(dg.Columns[i].Header.ToString());//给表头位置赋值
            }

            //添加内容
            for (int i = 0; i < rowCount; i++)
            {
                HSSFRow rowContent = (HSSFRow)sheet.CreateRow(num_date + i + 1);
                rowContent.HeightInPoints = 37;
                for (int j = 0; j < columnCount; j++)
                {
                    HSSFCell cellContent = (HSSFCell)rowContent.CreateCell(j);
                    if (j == 0)
                    {
                        if (i == rowCount - 1)
                            cellContent.CellStyle = styleContentLB;
                        else
                            cellContent.CellStyle = styleContentL;
                    }
                    else if (j == columnCount - 1)
                    {
                        if (i == rowCount - 1)
                            cellContent.CellStyle = styleContentRB;
                        else
                            cellContent.CellStyle = styleContentR;
                    }
                    else
                    {
                        if (i == rowCount - 1)
                            cellContent.CellStyle = styleContentB;
                        else
                            cellContent.CellStyle = styleContentC;
                    }
                    DataGridCell cell = dg.GetCell(i, j);
                    cellContent.SetCellValue((dg.GetCell(i, j).Content as TextBlock).Text);
                }
            }

            //string outputPath = @"E://abc.xls";
            try
            {
                SaveFileDialog sfd = new SaveFileDialog()
                {
                    DefaultExt = "xls",
                    Filter = "xls Files (*.xls)|*.xls|All files (*.*)|*.*",
                    FilterIndex = 1
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // using (Stream stream = sfd.OpenFile())
                    {
                        FileStream outStream = new FileStream(sfd.FileName, FileMode.Create);
                        workbook.Write(outStream);
                        outStream.Close();

                    }
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("保存失败！");
                Console.WriteLine(e.Message); 
            }

        }

    }
    public static class DataGridPlus
    {
        /// <summary>   
        /// 获取DataGrid的行   
        /// </summary>   
        /// <param name="dataGrid">DataGrid控件</param>   
        /// <param name="rowIndex">DataGrid行号</param>   
        /// <returns>指定的行号</returns>
        public static DataGridRow GetRow(this DataGrid dataGrid, int rowIndex)
        {

            DataGridRow rowContainer = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (rowContainer == null)
            {
                dataGrid.ScrollIntoView(dataGrid.Items[rowIndex]);
                dataGrid.UpdateLayout();
                rowContainer = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            }
            return rowContainer;
        }
        /// <summary>   
        /// 获取父可视对象中第一个指定类型的子可视对象   
        /// </summary>   
        /// <typeparam name="T">可视对象类型</typeparam>   
        /// <param name="parent">父可视对象</param>   
        /// <returns>第一个指定类型的子可视对象</returns>   
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        /// <summary>   
        /// 获取DataGrid控件单元格   
        /// </summary>   
        /// <param name="dataGrid">DataGrid控件</param>   
        /// <param name="rowIndex">单元格所在的行号</param>   
        /// <param name="columnIndex">单元格所在的列号</param>   
        /// <returns>指定的单元格</returns>   
        public static DataGridCell GetCell(this DataGrid dataGrid, int rowIndex, int columnIndex)
        {
            DataGridRow rowContainer = dataGrid.GetRow(rowIndex);
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                if (cell == null)
                {
                    dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[columnIndex]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                }
                return cell;
            }
            return null;
        }
    }

    public static class SheetStyle
    {
        /// <summary>
        /// 字体
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="size"></param>
        /// <param name="weight"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HSSFFont GetFont(HSSFWorkbook workbook, short size, FontBoldWeight weight = FontBoldWeight.NORMAL, string name = "宋体")
        {
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontName = name;//设置字体
            font.Boldweight = (short)weight;//设置加粗
            font.FontHeightInPoints = size;//设置字体大小
            return font;
        }
        /// <summary>
        /// 样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static HSSFCellStyle GetStyle(HSSFWorkbook workbook, CellPosition position = CellPosition.None)
        {
            HSSFCellStyle style = (HSSFCellStyle)workbook.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;//水平居中
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;//垂直居中
            style.WrapText = true;//设置换行
            SetBorder(style, position);
            return style;
        }
        /// <summary>
        /// 设置边框线
        /// </summary>
        /// <param name="style"></param>
        /// <param name="position"></param>
        private static void SetBorder(HSSFCellStyle style, CellPosition position)
        {
            switch (position)
            {
                case CellPosition.LeftTop:
                    SetBorder(style, BorderStyle.THICK, BorderStyle.THIN, BorderStyle.THICK, BorderStyle.THIN);
                    break;
                case CellPosition.Top:
                    SetBorder(style, BorderStyle.THICK, BorderStyle.THIN, BorderStyle.THIN, BorderStyle.THIN);
                    break;
                case CellPosition.RightTop:
                    SetBorder(style, BorderStyle.THICK, BorderStyle.THIN, BorderStyle.THIN, BorderStyle.THICK);
                    break;
                case CellPosition.Left:
                    SetBorder(style, BorderStyle.THIN, BorderStyle.THIN, BorderStyle.THICK, BorderStyle.THIN);
                    break;
                case CellPosition.Center:
                    SetBorder(style, BorderStyle.THIN, BorderStyle.THIN, BorderStyle.THIN, BorderStyle.THIN);
                    break;
                case CellPosition.Right:
                    SetBorder(style, BorderStyle.THIN, BorderStyle.THIN, BorderStyle.THIN, BorderStyle.THICK);
                    break;
                case CellPosition.LeftBottom:
                    SetBorder(style, BorderStyle.THIN, BorderStyle.THICK, BorderStyle.THICK, BorderStyle.THIN);
                    break;
                case CellPosition.Bottom:
                    SetBorder(style, BorderStyle.THIN, BorderStyle.THICK, BorderStyle.THIN, BorderStyle.THIN);
                    break;
                case CellPosition.RightBottom:
                    SetBorder(style, BorderStyle.THIN, BorderStyle.THICK, BorderStyle.THIN, BorderStyle.THICK);
                    break;
                case CellPosition.None:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设置边框线
        /// </summary>
        /// <param name="style"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void SetBorder(HSSFCellStyle style, BorderStyle top, BorderStyle bottom, BorderStyle left, BorderStyle right)
        {
            style.BorderTop = top;
            style.BorderBottom = bottom;
            style.BorderLeft = left;
            style.BorderRight = right;
        }
    }

    public enum CellPosition
    {
        LeftTop,
        Top,
        RightTop,
        Left,
        Center,
        Right,
        LeftBottom,
        Bottom,
        RightBottom,
        None
    }
}
