using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DataGridViewControls
{
    partial class ExtendedDataGridView
    {
        const string EXPORT_TO_EXCEL = "&Export to Excel",
                     EXPORT_TO_CSV   = "&Export to CSV",
                     SEARCH          = "&Search";

        ContextMenuStrip menuColumnHeader;

        void SetupContextMenu()
        {
            menuColumnHeader              = new ContextMenuStrip();
            menuColumnHeader.Opening     += MenuOpening;
            menuColumnHeader.ItemClicked += MenuItemClicked;
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            e.Column.HeaderCell.ContextMenuStrip = menuColumnHeader;
            MenuOpening(null, new CancelEventArgs());
        }

        string GetColumnText(DataGridViewColumn column)
        {
            if (column.HeaderText == "" && column is DataGridViewButtonColumn)
                return ((DataGridViewButtonColumn)column).Text;

            return column.HeaderText;
        }

        void MenuOpening(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            menuColumnHeader.Items.Clear();
            menuColumnHeader.Items.Add(CreateExportMenuItem());
            if (SortedColumn != null)
                menuColumnHeader.Items.Add(new ToolStripMenuItem(SEARCH));

            if (AllowAddRemoveColumns)
            {
                menuColumnHeader.Items.Add(new ToolStripSeparator());

                foreach (DataGridViewColumn column in Columns)
                {
                    ToolStripMenuItem newItem = new ToolStripMenuItem(GetColumnText(column));
                    if (ModifierKeys == Keys.Shift)
                        newItem.Text         += string.Format(" - ({0} px)", column.Width);
                    newItem.Checked           = column.Visible;
                    newItem.Visible           = newItem.Text != "";
                    newItem.Tag               = column;
                    if (column.Index < 9)
                        newItem.ShortcutKeyDisplayString = "Ctrl + Alt + " + (column.Index + 1);
                    menuColumnHeader.Items.Add(newItem);
                }
            }
        }

        ToolStripMenuItem CreateExportMenuItem()
        {
            if (DataGridViewExporter.IsExcelInstalled)
                return new ToolStripMenuItem(EXPORT_TO_EXCEL, Properties.Resources.IconExcel16);

            return new ToolStripMenuItem(EXPORT_TO_CSV, Properties.Resources.IconCSV16);
        }

        private void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem menuItem = e.ClickedItem as ToolStripMenuItem;

            if (menuItem == null)
                return;

            switch (menuItem.Text)
            {
                case EXPORT_TO_EXCEL:
                    ToExcel();
                    break;

                case EXPORT_TO_CSV:
                    ToCsv();
                    break;

                case SEARCH:
                    ShowSearch();
                    break;

                default:
                    if (!menuItem.Checked || Columns.GetColumnCount(DataGridViewElementStates.Visible) > 1)
                    {
                        DataGridViewColumn column = (DataGridViewColumn)menuItem.Tag;
                        column.Visible            = !menuItem.Checked;
                        menuItem.Checked          = column.Visible;
                    }
                    break;
            }
        }

    }
}
