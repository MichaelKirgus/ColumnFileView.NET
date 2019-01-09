using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewControls
{
    partial class ExtendedDataGridView
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == '`')
                ShowSearch();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Control)
                foreach (DataGridViewColumn col in Columns)
                    if (!col.HeaderText.StartsWith((col.Index + 1) + ". "))
                        col.HeaderText = string.Format("{0}. {1}", col.Index + 1, col.HeaderText);

            if ((char)e.KeyValue >= '1' && (char)e.KeyValue <= '9')
            {
                if (e.Modifiers == (Keys.Alt | Keys.Control))
                    foreach (ToolStripItem item in menuColumnHeader.Items)
                    {
                        ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                        if (menuItem != null && menuItem.ShortcutKeyDisplayString == "Ctrl + Alt + " + (char)e.KeyValue)
                            MenuItemClicked(this, new ToolStripItemClickedEventArgs(item));
                    }
                else if (e.Modifiers == Keys.Control)
                {
                    DataGridViewColumn column              = Columns[int.Parse(((char)e.KeyValue).ToString()) - 1];
                    MouseEventArgs mouseArg                = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 0);
                    DataGridViewCellMouseEventArgs gridArg = new DataGridViewCellMouseEventArgs(column.Index, -1, 1, 1, mouseArg);
                    OnColumnHeaderMouseClick(gridArg);

                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (!e.Control)
                foreach (DataGridViewColumn col in Columns)
                    if (col.HeaderText.StartsWith((col.Index + 1) + ". "))
                        col.HeaderText = col.HeaderText.Replace((col.Index + 1) + ". ", "");
        }
    }
}
