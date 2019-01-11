// Copyright (C) 2018-2019 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewControls
{
    partial class ExtendedDataGridView
    {
        PanelQuickSearch m_pnlQuickSearch;

        void ShowSearch()
        {
            if (SortedColumn != null)
            {
                if (m_pnlQuickSearch == null)
                {
                    m_pnlQuickSearch = new PanelQuickSearch();
                    Controls.Add(m_pnlQuickSearch);
                    m_pnlQuickSearch.SearchChanged += m_pnlQuickSearch_SearchChanged;
                }

                if (SelectedRows.Count > 0)
                    m_pnlQuickSearch.Search = SelectedRows[0].Cells[SortedColumn.Index].Value.ToString();
                m_pnlQuickSearch.Column = SortedColumn.HeaderText;
                m_pnlQuickSearch.Show();
                m_pnlQuickSearch.Focus();
            }
        }

        void m_pnlQuickSearch_SearchChanged(string search)
        {
            foreach (DataGridViewRow row in SelectedRows)
                row.Selected = false;

            if (SortOrder == SortOrder.Ascending)
                Rows[BinarySearchAsc(search)].Selected = true;
            else
                Rows[BinarySearchDesc(search)].Selected = true;

            FirstDisplayedScrollingRowIndex = SelectedRows[0].Index;
        }

        int BinarySearchAsc(string value)
        {
            int max     = Rows.Count - 1,
                min     = 0,
                current,
                compare;
            
            int sortedColumn = SortedColumn.Index;

            while (max >= min)
            {
                current = (max - min) / 2 + min;

                compare = this[sortedColumn, current].Value.ToString().CompareTo(value);

                if (compare > 0)
                    max = current - 1;
                else if (compare < 0)
                    min = current + 1;
                else
                    return current;
            }

            if (min >= Rows.Count)
                return Rows.Count - 1;

            return min;
        }

        int BinarySearchDesc(string value)
        {
            int max     = Rows.Count - 1,
                min     = 0,
                current,
                compare;

            int sortedColumn = SortedColumn.Index;

            while (max >= min)
            {
                current = (max - min) / 2 + min;

                compare = this[sortedColumn, current].Value.ToString().CompareTo(value);

                if (compare < 0)
                    max = current - 1;
                else if (compare > 0)
                    min = current + 1;
                else
                    return current;
            }

            if (max < 0)
                return 0;

            return max;
        }
    }
}
