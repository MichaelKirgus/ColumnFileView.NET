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
