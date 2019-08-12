using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HistorydataPolling.fileCtrol
{
    class SaveFile
    {
        public void saveFile(string s1)
        {
           



            // HSSFWorkbook workbook2003 = new HSSFWorkbook(); //新建工作簿
            // workbook2003.CreateSheet("Sheet1");  //新建1个Sheet工作表            
            // HSSFSheet SheetOne = (HSSFSheet)workbook2003.GetSheet("Sheet1"); //获取名称为Sheet1的工作表
            // //对工作表先添加行，下标从0开始
            // for (int i = 0; i < 10; i++)
            // {
            //     SheetOne.CreateRow(i);   //创建10行
            // }
            // //对每一行创建10个单元格
            // HSSFRow SheetRow = (HSSFRow)SheetOne.GetRow(0);  //获取Sheet1工作表的首行
            // HSSFCell[] SheetCell = new HSSFCell[3];
            // for (int i = 0; i <3 ; i++)
            // {
            //     SheetCell[i] = (HSSFCell)SheetRow.CreateCell(i);  //为第一行创建10个单元格
            // }
            // //创建之后就可以赋值了
            // SheetCell[0].SetCellValue(true); //赋值为bool型         
            // SheetCell[1].SetCellValue(0.000001); //赋值为浮点型
            // SheetCell[2].SetCellValue("Excel2003"); //赋值为字符串
            //// SheetCell[3].SetCellValue("123456789987654321");//赋值为长字符串
            // //for (int i = 4; i < 10; i++)
            // //{
            // //    SheetCell[i].SetCellValue(i);    //循环赋值为整形
            // //}

            // //@"g:\Excel2gggggg003.xls"
            // FileStream file2003 = new FileStream(s1, FileMode.Create);
            // workbook2003.Write(file2003);
            // file2003.Close();
            // workbook2003.Close();
        }
    }
}
