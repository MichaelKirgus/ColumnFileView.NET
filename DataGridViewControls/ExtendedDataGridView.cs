// Copyright (C) 2018-2019 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// This source code was initial published by "Chris_McGrath" under the "The Code Project Open License (CPOL) 1.022" license. You can find a copy under \License\License_CPOL.txt or under https://www.codeproject.com/info/cpol10.aspx
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace DataGridViewControls
{
    [DesignerAttribute(typeof(ExtendedDataGridViewDesigner))]
    public partial class ExtendedDataGridView : DataGridView
    {
        public ExtendedDataGridView()
        {
            SetupContextMenu();
        }

        public event EventHandler WorkStart;
        public event EventHandler WorkFinished;

        public virtual void OnWorkStart(object sender, EventArgs e)
        {
            if (WorkStart != null)
                WorkStart(sender, e);
        }

        public virtual void OnWorkFinished(object sender, EventArgs e)
        {
            if (WorkFinished != null)
                WorkFinished(sender, e);
        }

        public void ToExcel()
        {
            new Thread(delegate()
            {
                OnWorkStart(this, new EventArgs());

                if (DataGridViewExporter.IsExcelInstalled)
                {
                    Excel.Worksheet worksheet = new DataGridViewExporter(this, ExportVisibleColumnsOnly).ToExcel();
                    worksheet.IsApplicationVisible = true;
                }

                OnWorkFinished(this, new EventArgs());
            }).Start();
        }

        public void ToCsv()
        {
            new Thread(delegate()
            {
                OnWorkStart(this, new EventArgs());

                new DataGridViewExporter(this).SaveAsCSV(true);

                OnWorkFinished(this, new EventArgs());
            }).Start();
        }
    }
}
