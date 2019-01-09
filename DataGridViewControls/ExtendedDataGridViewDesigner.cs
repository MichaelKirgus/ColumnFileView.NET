using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Reflection;
using System.ComponentModel;

namespace DataGridViewControls
{
    class ExtendedDataGridViewDesigner : ControlDesigner
    {
        DesignerActionListCollection m_actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (m_actionLists == null)
                    BuildActionLists();
                
                return m_actionLists;
            }
        }

        void BuildActionLists()
        {
            m_actionLists = new DesignerActionListCollection();
            m_actionLists.Add(new DataGridViewChooseDataSourceActionList(this));
            m_actionLists.Add(new DataGridViewColumnEditingActionList(this));
            m_actionLists.Add(new DataGridViewPropertiesActionList(this));
        }

        public static Form CreateColumnAdd(DataGridView grid)
        {
            Type dlgColumnAdd = Assembly.Load("System.Design").GetType("System.Windows.Forms.Design.DataGridViewAddColumnDialog");

            var constructor = dlgColumnAdd.GetConstructors()[0];
            var form = (Form)constructor.Invoke(new object[] { grid.Columns, grid });

            dlgColumnAdd.GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(form, new object[] { grid.Columns.Count, true});

            return form;
        }

        public void OnAddColumn(object sender, EventArgs e)
        {
            DesignerTransaction transaction = (base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost).CreateTransaction("DataGridViewAddColumnTransactionString");
            DialogResult cancel = DialogResult.Cancel;
            Form dialog = CreateColumnAdd((DataGridView)base.Component);
            try
            {
                cancel = this.ShowDialog(dialog);
            }
            finally
            {
                if (cancel == DialogResult.OK)
                    transaction.Commit();
                else
                    transaction.Cancel();
            }
        }

        public void OnEditColumns(object sender, EventArgs e)
        {
            IDesignerHost service = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
            Form dialog = ExtendedDataGridViewColumnCollectionEditor.CreateColumnEditor();
            ExtendedDataGridViewColumnCollectionEditor.SetGrid(dialog, (DataGridView)base.Component);
            DesignerTransaction transaction = service.CreateTransaction("DataGridViewEditColumnsTransactionString");
            DialogResult cancel = DialogResult.Cancel;
            try
            {
                cancel = this.ShowDialog(dialog);
            }
            finally
            {
                if (cancel == DialogResult.OK)
                    transaction.Commit();
                else
                    transaction.Cancel();
                ExtendedDataGridViewColumnCollectionEditor.SaveData(dialog);
            }
        }

        private DialogResult ShowDialog(Form dialog)
        {
            IUIService service = base.Component.Site.GetService(typeof(IUIService)) as IUIService;
            if (service != null)
            {
                return service.ShowDialog(dialog);
            }
            return dialog.ShowDialog(base.Component as IWin32Window);
        }

        public object DataSource
        {
            get { return ((DataGridView)base.Component).DataSource; }
            set
            {
                DataGridView component = base.Component as DataGridView;
                if ((component.AutoGenerateColumns && (component.DataSource == null)) && (value != null))
                    component.AutoGenerateColumns = false;

                component.DataSource = value;
            }
        }

        private class DataGridViewColumnEditingActionList : DesignerActionList
        {
            ExtendedDataGridViewDesigner m_owner;

            public DataGridViewColumnEditingActionList(ExtendedDataGridViewDesigner owner)
                : base(owner.Component)
            {
                m_owner = owner;
            }

            public void AddColumn()
            {
                m_owner.OnAddColumn(this, EventArgs.Empty);
            }

            public void EditColumns()
            {
                m_owner.OnEditColumns(this, EventArgs.Empty);
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                var items = new DesignerActionItemCollection();
                items.Add(new DesignerActionMethodItem(this, "EditColumns", "Edit Columns...", true));
                items.Add(new DesignerActionMethodItem(this, "AddColumn",   "Add Column...", true));
                return items;
            }
        }

        private class DataGridViewPropertiesActionList : DesignerActionList
        {
            ExtendedDataGridViewDesigner m_owner;

            public DataGridViewPropertiesActionList(ExtendedDataGridViewDesigner owner)
                : base(owner.Component)
            {
                m_owner = owner;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                DesignerActionItemCollection items = new DesignerActionItemCollection();
                items.Add(new DesignerActionPropertyItem("AllowUserToAddRows",      "Enable Adding"));
                items.Add(new DesignerActionPropertyItem("ReadOnly",                "Enable Editing"));
                items.Add(new DesignerActionPropertyItem("AllowUserToDeleteRows",   "Enable Deleting"));
                items.Add(new DesignerActionPropertyItem("AllowUserToOrderColumns", "Enable Column Reordering"));
                return items;
            }

            public bool AllowUserToAddRows
            {
                get { return ((DataGridView)m_owner.Component).AllowUserToAddRows; }
                set
                {
                    if (value != AllowUserToAddRows)
                        SetProperty("AllowUserToAddRows", "DataGridView{0}AddingTransactionString", value);
                }
            }

            public bool AllowUserToDeleteRows
            {
                get { return ((DataGridView)m_owner.Component).AllowUserToDeleteRows; }
                set
                {
                    if (value != AllowUserToDeleteRows)
                        SetProperty("AllowUserToDeleteRows", "DataGridView{0}DeletingTransactionString", value);
                }
            }

            public bool AllowUserToOrderColumns
            {
                get { return ((DataGridView)m_owner.Component).AllowUserToOrderColumns; }
                set
                {
                    if (value != AllowUserToOrderColumns)
                        SetProperty("AllowUserToOrderColumns", "DataGridView{0}ColumnReorderingTransactionString", value);
                }
            }

            public bool ReadOnly
            {
                get { return !((DataGridView)m_owner.Component).ReadOnly; }
                set
                {
                    if (value != ReadOnly)
                        SetProperty("ReadOnly", "DataGridView{0}EditingTransactionString", !value);
                }
            }

            public void SetProperty(string propertyName, string transactionName, bool value)
            {
                var host = m_owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                var transaction = host.CreateTransaction(string.Format(transactionName, value ? "Enable" : "Disable"));

                try
                {
                    var service = m_owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    var member  = TypeDescriptor.GetProperties(m_owner.Component)[propertyName];
                    service.OnComponentChanging(m_owner.Component, member);
                    member.SetValue(m_owner.Component, value);
                    service.OnComponentChanged(m_owner.Component, member, null, null);
                    transaction.Commit();
                    transaction = null;
                }
                finally
                {
                    if (transaction != null)
                        transaction.Cancel();
                }
            }
        }

        [ComplexBindingProperties("DataSource", "DataMember")]
        private class DataGridViewChooseDataSourceActionList : DesignerActionList
        {
            ExtendedDataGridViewDesigner m_owner;

            public DataGridViewChooseDataSourceActionList(ExtendedDataGridViewDesigner owner)
                : base(owner.Component)
            {
                m_owner = owner;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                var items = new DesignerActionItemCollection();
                var item  = new DesignerActionPropertyItem("DataSource", "Choose Data Source");
                item.RelatedComponent = m_owner.Component;
                items.Add(item);
                return items;
            }

            [AttributeProvider(typeof(IListSource))]
            public object DataSource
            {
                get { return m_owner.DataSource; }
                set
                {
                    var component   = (DataGridView)m_owner.Component;
                    var host        = m_owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    var member      = TypeDescriptor.GetProperties(component)["DataSource"];
                    var service     = m_owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    var transaction = host.CreateTransaction("DataGridViewChooseDataSourceTransactionString");
                    try
                    {
                        service.OnComponentChanging(component, member);
                        m_owner.DataSource = value;
                        service.OnComponentChanged(component, member, null, null);
                        transaction.Commit();
                        transaction = null;
                    }
                    finally
                    {
                        if (transaction != null)
                            transaction.Cancel();
                    }
                }
            }
        }
    }
}
