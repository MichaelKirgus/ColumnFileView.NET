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
