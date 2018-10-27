using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using CyberPushkin.model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CyberPushkin.root
{
    class ExcelFileReader
    {

        List<Verse> xlsxToVerse(String xlsxFile, int sheetNumber) {

    if (xlsxFile == null ||
        sheetNumber< 1) {
      throw new ArgumentException();
    }

    try {
                HSSFWorkbook hssfwb;
                using (FileStream file = new FileStream(@xlsxFile, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new HSSFWorkbook(file);
                }
                ISheet sheet = hssfwb.GetSheet(hssfwb.GetSheetName(sheetNumber - 1));
                
      // at least two rows
      if (sheet.LastRowNum > 0) {
        return getVerses((HSSFSheet) sheet);
} else {
        throw new EmptySheetException("Excel sheet (#" + sheetNumber + ") is empty");
      }
    } catch (ArgumentException e) {
      throw new EmptySheetException("Excel sheet (#" + sheetNumber + ") is not found");
    }
  }

  private List<Verse> getVerses(HSSFSheet sheet)
{
    List<Position> positions = getPositions(sheet);
    List<Verse> verses = new  List<Verse>();

    // start from second row
    for (int i = 1; i <= sheet.LastRowNum; i++)
    {
        Dictionary<Position, PositionValue> positionsValues = getPositionsValues(sheet.GetRow(i), positions);

        // exclude empty rows
        if (!sheet.GetRow(i).GetCell(0).StringCellValue.Equals(""))
        {
            verses.Add(new Verse(sheet.GetRow(i).GetCell(0).StringCellValue, positionsValues));
        }
    }

    return verses;
}

private Dictionary<Position, PositionValue> getPositionsValues(IRow row, List<Position> positions)
{
    Dictionary<Position, PositionValue> positionsValues = new Dictionary<Position, PositionValue>();

    for (int i = 1; i < row.LastCellNum; i++)
    {
        positionsValues[positions[(i - 1)]] = new PositionValue(row.GetCell(i).StringCellValue);
    }

    return positionsValues;
}

private List<Position> getPositions(HSSFSheet sheet)
{
    List<Position> positions = new List<Position>();

    // first row from second to last columns contains Positions names
    for (int i = 1; i < sheet.GetRow(0).LastCellNum; i++)
    {
        positions.Add(new Position(sheet.GetRow(0).GetCell(i).StringCellValue));
    }

    return positions;
}
}
    }

