// Copyright (C) 2018-2019 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace DataGridViewControls
{
    partial class ExtendedDataGridView
    {
        bool m_allowAddRemoveColumns    = true,
             m_exportVisibleColumnsOnly = true;

        [DefaultValue(true)]
        public bool AllowAddRemoveColumns
        {
            get { return m_allowAddRemoveColumns; }
            set { m_allowAddRemoveColumns = value; }
        }

        [DefaultValue(true)]
        public bool ExportVisibleColumnsOnly
        {
            get { return m_exportVisibleColumnsOnly; }
            set { m_exportVisibleColumnsOnly = value; }
        }

        [Editor(typeof(ExtendedDataGridViewColumnCollectionEditor), typeof(UITypeEditor))]
        public new DataGridViewColumnCollection Columns
        {
            get { return base.Columns; }
        }
    }
}
