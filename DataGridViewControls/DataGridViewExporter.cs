// Copyright (C) 2018-2019 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// This source code was initial published by "Chris_McGrath" under the "The Code Project Open License (CPOL) 1.022" license. You can find a copy under \License\License_CPOL.txt or under https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace DataGridViewControls
{
    #region Farthest side of the western ocean
    //A Gorgon class - For the love of Zeus don't look directly at it!
    //Hides reflection used to call Excel. Reflection is used so Excel doesn't have to be on the dev machine
    public class Excel
    {
        const string ASSEMBLY2003 = "Microsoft.Office.Interop.Excel, Version=11.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c";

        #region Medusa
        public class Worksheet
        {
            object       m_worksheet;
            Type         m_type;
            object       m_cells;
            PropertyInfo m_cellIndex;

            public Worksheet()
            {
                object excel      = s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel.ApplicationClass").GetConstructor(new Type[] { }).Invoke(new object[] { });
                object workbooks  = excel.GetType().GetProperty("Workbooks").GetValue(excel, null);
                object workbook   = s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel.Workbooks").GetMethod("Add").Invoke(workbooks, new object[] { Type.Missing });
                object worksheets = workbook.GetType().GetProperty("Worksheets").GetValue(workbook, null);
                object worksheet  = s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel.Sheets").GetMethod("get_Item").Invoke(worksheets, new object[] { 1 });

                m_worksheet = worksheet;
                m_type      = Excel.s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel._Worksheet");
                m_cells     = m_type.GetProperty("Cells").GetValue(m_worksheet, null);
                m_cellIndex = Excel.s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel.Range").GetProperty("Item", new Type[] { typeof(int), typeof(int) });
            }

            public string Name
            {
                get { return (string)m_type.GetProperty("Name").GetValue(m_worksheet, null); }
                set { m_type.GetProperty("Name").SetValue(m_worksheet, value, null); }
            }

            public Cell this[object row, object column]
            {
                get { return new Cell(m_cellIndex.GetValue(m_cells, new object[] {row, column})); }
            }

            public bool IsApplicationVisible
            {
                get
                {
                    object app = m_type.GetProperty("Application").GetValue(m_worksheet, null);
                    return (bool)s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel._Application").GetProperty("Visible").GetValue(app, null);
                }
                set
                {
                    object app = m_type.GetProperty("Application").GetValue(m_worksheet, null);
                    s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel._Application").GetProperty("Visible").SetValue(app, value, null);
                }
            }

            public void Close()
            {
                object app = m_type.GetProperty("Application").GetValue(m_worksheet, null);
                s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel._Application").GetMethod("Quit").Invoke(app, null);
            }
        }
        #endregion

        #region Stheno
        public class Cell
        {
            static Type         s_type;
            static PropertyInfo s_value,
                                s_numberFormat,
                                s_font,
                                s_entireColumn;
            static MethodInfo   s_autoFit;
            object              m_range;

            static Cell()
            {
                s_type         = s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel.Range");

                s_value        = s_type.GetProperty("Value2");
                s_numberFormat = s_type.GetProperty("NumberFormat");
                s_font         = s_type.GetProperty("Font");
                s_entireColumn = s_type.GetProperty("EntireColumn");

                s_autoFit      = s_type.GetMethod("AutoFit");
            }

            public Cell(object cell)
            {
                m_range = cell;
            }

            public object Value
            {
                get { return s_value.GetValue(m_range, null); }
                set { s_value.SetValue(m_range, value, null); }
            }

            public object NumberFormat
            {
                get { return s_numberFormat.GetValue(m_range, null); }
                set { s_numberFormat.SetValue(m_range, value, null); }
            }

            public Font Font
            {
                get { return new Font(s_font.GetValue(m_range, null)); }
            }

            public void AutoFitEntireColumn()
            {
                object entireColumn = s_entireColumn.GetValue(m_range, null);
                s_autoFit.Invoke(entireColumn, null);
            }
        }
        #endregion

        #region Euryale
        public class Font
        {
            static Type         s_type;
            static PropertyInfo s_bold;
            object              m_font;

            static Font()
            {
                s_type = s_assemblyExcel.GetType("Microsoft.Office.Interop.Excel.Font");
                s_bold = s_type.GetProperty("Bold");
            }

            public Font(object font)
            {
                m_font = font;
            }

            public bool Bold
            {
                get { return (bool)s_bold.GetValue(m_font, null); }
                set { s_bold.SetValue(m_font, value, null); }
            }
        }
        #endregion

        static bool?    s_isExcelInstalled;
        static Assembly s_assemblyExcel;
        public static bool IsExcelInstalled
        {
            get
            {
                if (s_isExcelInstalled == null)
                    s_isExcelInstalled = IsAssemblyInstalled(ASSEMBLY2003);

                return (bool)s_isExcelInstalled;
            }
        }

        static bool IsAssemblyInstalled(string assembly)
        {
            try
            {
                s_assemblyExcel = Assembly.Load(assembly);
                return true;
            } catch
            {
                return false;
            }
        }
    }
    #endregion

    public abstract class AbstractDataGridViewExporter
    {
        DataGridView m_grid;
        bool         m_exportVisibleOnly;

        protected AbstractDataGridViewExporter(DataGridView grid, bool exportVisibleColumnsOnly)
        {
            m_grid            = grid;
            ExportVisibleOnly = exportVisibleColumnsOnly;
        }

        public bool ExportVisibleOnly
        {
            get { return m_exportVisibleOnly; }
            set { m_exportVisibleOnly = value;}
        }

        #region CSV
        public string ToCSV(bool includeColumnHeaders)
        {
            StringBuilder csv = new StringBuilder();

            if (includeColumnHeaders)
                ExportHeaders(csv);

            ExportCells(csv);

            return csv.ToString();
        }

        void ExportHeaders(StringBuilder csv)
        {
            string line = "";
            for (int i = 0; i < m_grid.ColumnCount; i++)
                if (IsExportableColumn(i))
                    line += "," + m_grid.Columns[i].HeaderText;

            csv.AppendLine(line.Substring(1));
        }

        void ExportCells(StringBuilder csv)
        {
            for (int row = 0; row < m_grid.RowCount; row++)
            {
                string line = "";
                for (int column = 0; column < m_grid.ColumnCount; column++)
                {
                    if (IsExportableColumn(column))
                        line += "," + CellValue(m_grid[column, row]).ToString();
                }

                csv.AppendLine(line.Substring(1));
            }
        }

        public void SaveAsCSV(bool incluedColumnHeaders)
        {
            SaveAsCSV(incluedColumnHeaders, m_grid.Name);
        }

        public void SaveAsCSV(bool incluedColumnHeaders, string filename)
        {
            m_grid.Invoke(new MethodInvoker(delegate
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter         = "Comma Separated Value (*.csv)|*.csv";
                dialog.FileName       = filename;
                dialog.ValidateNames  = true;
                
                if (dialog.ShowDialog(m_grid) == DialogResult.Cancel)
                    filename = null;
                else
                    filename = dialog.FileName;
            }));

            if (filename == null)
                return;

            TextWriter writer = new StreamWriter(filename);
            writer.Write(ToCSV(incluedColumnHeaders));
            writer.Close();
        }
        #endregion

        #region Excel
        public Excel.Worksheet ToExcel()
        {
            bool gridEnabled = m_grid.Enabled;

            m_grid.Invoke(new MethodInvoker(delegate() { m_grid.Enabled = false; }));

            Excel.Worksheet  worksheet = new Excel.Worksheet();

            SetWorksheetName(worksheet);
            ExportCells(worksheet);
            ExportHeaders(worksheet);

            try
            {
                if (gridEnabled)
                    m_grid.Invoke(new MethodInvoker(delegate() { m_grid.Enabled = true; }));
            } catch
            {
            }

            return worksheet;
        }

        void SetWorksheetName(Excel.Worksheet worksheet)
        {
            if (m_grid.Parent != null)
            {
                string name = m_grid.Parent.Text;

                foreach (char c in ":\\/?*[]")
                    name = name.Replace(c.ToString(), "");

                if (name.Length > 31)
                    name = name.Substring(0, 28) + "...";

                if (name != "")
                    worksheet.Name = name;
            }
        }

        void ExportCells(Excel.Worksheet worksheet)
        {
            for (int column = 0, worksheetColumn = 1; column < m_grid.ColumnCount; column++)
                if (IsExportableColumn(column))
                {
                    for (int row = 0; row < m_grid.RowCount; row++)
                    {
                        Excel.Cell currentCell = worksheet[row + 2, worksheetColumn];
                        currentCell.Value      = CellValue(m_grid[column, row]);
                        SetCellFormat(currentCell, m_grid[column, row]);
                    }

                    worksheetColumn++;
                }
        }

        void ExportHeaders(Excel.Worksheet worksheet)
        {
            for (int column = 0, worksheetColumn = 1; column < m_grid.ColumnCount; column++)
                if (IsExportableColumn(column))
                {
                    Excel.Cell currentHeader = worksheet[1, worksheetColumn++];
                    currentHeader.Value      = m_grid.Columns[column].HeaderText;
                    currentHeader.Font.Bold  = true;
                    currentHeader.AutoFitEntireColumn();
                }
        }
        #endregion

        public static bool IsExcelInstalled
        {
            get { return Excel.IsExcelInstalled; }
        }

        bool IsExportableColumn(int columnIndex)
        {
            return IsExportableColumn(m_grid.Columns[columnIndex]);
        }

        public virtual bool IsExportableColumn(DataGridViewColumn column)
        {
            return (!ExportVisibleOnly || column.Visible);
        }

        void SetCellFormat(Excel.Cell excelCell, DataGridViewCell gridCell)
        {
            try
            {
                excelCell.NumberFormat = CellFormat(gridCell);
            } catch
            {
            }
        }

        public abstract object CellFormat(DataGridViewCell cell);

        public abstract object CellValue(DataGridViewCell cell);
    }

    public class DataGridViewExporter : AbstractDataGridViewExporter
    {
        #region Constructors
        public DataGridViewExporter(DataGridView grid) : base(grid, true)
        {
        }

        public DataGridViewExporter(DataGridView grid, bool exportVisibleColumnsOnly) : base(grid, exportVisibleColumnsOnly)
        {
        }
        #endregion

        public override bool IsExportableColumn(DataGridViewColumn column)
        {
            if (column is DataGridViewButtonColumn ||
                column is DataGridViewImageColumn)
                return false;

            return base.IsExportableColumn(column);
        }

        public override object CellFormat(DataGridViewCell cell)
        {
            string format = string.IsNullOrEmpty(cell.Style.Format)
                          ? cell.OwningColumn.DefaultCellStyle.Format
                          : cell.Style.Format;

            if (format.ToUpper() == "C" || format.ToUpper() == "C2")
                format = "$0.00";

            if (cell.Value is DateTime && format == "")
                format = "d/mm/yyyy";

            return format;
        }

        public override object CellValue(DataGridViewCell cell)
        {
            if (cell is DataGridViewCheckBoxCell)
            {
                if (cell.Value is DBNull)
                    return "Unknown";

                if (cell.FormattedValue is bool)
                    return ((bool)cell.FormattedValue) ? "Yes" : "No";
                else
                {
                    object falseValue = ((DataGridViewCheckBoxColumn)cell.DataGridView.Columns[cell.ColumnIndex]).FalseValue;
                    if (falseValue != null)
                        return falseValue.Equals(cell.Value.ToString()) ? "No" : "Yes";
                }
            }
            else if (IsNumeric(cell.Value) || cell.Value is DateTime)
                return cell.Value;

            return cell.FormattedValue;
        }

        static bool IsNumeric(object value)
        {
            return value is int || value is double || value is float || value is decimal;
        }
    }
}
