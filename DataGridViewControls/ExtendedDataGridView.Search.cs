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
