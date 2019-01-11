// Copyright (C) 2018-2019 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// This source code was initial published by "Chris_McGrath" under the "The Code Project Open License (CPOL) 1.022" license. You can find a copy under \License\License_CPOL.txt or under https://www.codeproject.com/info/cpol10.aspx
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
