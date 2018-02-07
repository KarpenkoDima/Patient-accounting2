using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SOPB.GUI.UserControls
{
    class BoundCheckedListBox : CheckedListBox
    {
        /// <summary>
        /// helper class. makes it possible to hold display and values information in an item of listbox which makes identification easier
        /// </summary>
        internal class ListBoxItem
        {
            object _val, _display;

            public object Value
            {
                get { return _val; }
                set { _val = value; }
            }

            public object Display
            {
                get { return _display; }
                set { _display = value; }
            }

            public override string ToString()
            {
                return _display.ToString();
            }

            public ListBoxItem(object value, object display)
            {
                this._display = display;
                this._val = value;
            }
        }

        private object parentDataSource, childDataSource, relationDataSource;
        private string parentIDMember, childIDMember, childDisplayMember, parentValueMember, childValueMember;

        private ListChangedEventHandler childListChangedHandler;
        private ListChangedEventHandler parentListChangedHandler;
        private ListChangedEventHandler relationListChangedHandler;
        private ItemCheckEventHandler itemCheckChangesHandler;
        private EventHandler selectionChangedHandler;

        private EventHandler childPositionChangedHandler;
        private EventHandler parentPositionChangedHandler;

        private CurrencyManager childDataManager;
        private CurrencyManager parentDataManager;
        private CurrencyManager relationDataManager;
        //private CurrencyManager myRelationDataManager;

        bool ready = false;

        #region Properties
        #region DataSource
        /// <summary>
        /// Returns or sets the datasource used as child datasource
        /// </summary>
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Category("Data")]
        [Description("Gibt die Datenquelle (child) fьr das Control an.")]
        [DefaultValue(null)]
        public object ChildDataSource
        {
            get
            {
                return this.childDataSource;
            }
            set
            {
                if (this.childDataSource != value)
                {
                    this.childDataSource = value;
                    tryDataBinding();
                }
            }
        }
        /// <summary>
        /// Returns or sets the datasource used as parent datasource
        /// </summary>
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Category("Data")]
        [Description("Gibt die Datenquelle (parent) fьr das Control an.")]
        [DefaultValue(null)]
        public object ParentDataSource
        {
            get
            {
                return this.parentDataSource;
            }
            set
            {
                if (this.parentDataSource != value)
                {
                    this.parentDataSource = value;
                    tryDataBinding();
                }
            }
        }
        /// <summary>
        /// Returns or sets the datasource used as relation datasource
        /// </summary>
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Category("Data")]
        [Description("Gibt die Datenquelle (relation) fьr das Control an.")]
        [DefaultValue(null)]
        public object RelationDataSource
        {
            get
            {
                return this.relationDataSource;
            }
            set
            {
                if (this.relationDataSource != value)
                {
                    this.relationDataSource = value;
                    tryDataBinding();
                }
            }
        }
        #endregion

        #region DataMember
        /// <summary>
        /// Returns or sets the ID column name of the parent datasource
        /// </summary>
        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
             "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Description("Gibt eine untergeordnete Liste von DataSource an, um sie anzuzeigen. - parent ID column")]
        [DefaultValue(null)]
        public string ParentIDMember
        {
            get
            {
                return this.parentIDMember;
            }
            set
            {
                if (this.parentIDMember != value)
                {
                    this.parentIDMember = value;
                    tryDataBinding();
                }
            }
        }
        /// <summary>
        /// Returns or sets the ID column name of the child datasource
        /// </summary>
        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
             "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Description("Gibt eine untergeordnete Liste von DataSource an, um sie anzuzeigen. - child ID column")]
        [DefaultValue(null)]
        public string ChildIDMember
        {
            get
            {
                return this.childIDMember;
            }
            set
            {
                if (this.childIDMember != value)
                {
                    this.childIDMember = value;
                    tryDataBinding();
                }
            }
        }
        /// <summary>
        /// Returns or sets the column name of the column which holds the informations used as names in the listbox
        /// of the child datasource
        /// </summary>
        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
             "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Description("Gibt eine untergeordnete Liste von DataSource an, um sie anzuzeigen. - child display column")]
        [DefaultValue(null)]
        public string ChildDisplayMember
        {
            get
            {
                return this.childDisplayMember;
            }
            set
            {
                if (this.childDisplayMember != value)
                {
                    this.childDisplayMember = value;
                    tryDataBinding();
                }
            }
        }
        /// <summary>
        /// Returns or sets the column name of the relation table column which holds the foreign keys for the parent table
        /// </summary>
        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
             "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Description("Gibt eine untergeordnete Liste von DataSource an, um sie anzuzeigen. - parent foreign key column in relation")]
        [DefaultValue(null)]
        public string ParentValueMember
        {
            get
            {
                return this.parentValueMember;
            }
            set
            {
                if (this.parentValueMember != value)
                {
                    this.parentValueMember = value;
                    tryDataBinding();
                }
            }
        }
        /// <summary>
        /// Returns or sets the column name of the relation table column which holds the foreign keys for the child table
        /// </summary>
        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
             "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Description("Gibt eine untergeordnete Liste von DataSource an, um sie anzuzeigen. - child foreign key column in relation")]
        [DefaultValue(null)]
        public string ChildValueMember
        {
            get
            {
                return this.childValueMember;
            }
            set
            {
                if (this.childValueMember != value)
                {
                    this.childValueMember = value;
                    tryDataBinding();
                }
            }
        }
        #endregion

        #region CurrencyManager
        /// <summary>
        /// Ruft den CurrencyManager der gebundenen Liste ab.
        /// </summary>
        protected CurrencyManager ParentCurrencyManager
        {
            get
            {
                return this.parentDataManager;
            }
        }/// <summary>
         /// Ruft den CurrencyManager der gebundenen Liste ab.
         /// </summary>
        protected CurrencyManager ChildCurrencyManager
        {
            get
            {
                return this.childDataManager;
            }
        }
        /// <summary>
        /// Ruft den CurrencyManager der gebundenen Liste ab.
        /// </summary>
        protected CurrencyManager RelationCurrencyManager
        {
            get
            {
                return this.relationDataManager;
            }
        }

        #endregion
        #endregion

        /// <summary>
        /// Renew the Databinding. BindingContext is changed, if you change the Parent.
        /// </summary>
        protected override void OnBindingContextChanged(EventArgs e)
        {
            this.tryDataBinding();
            base.OnBindingContextChanged(e);
        }

        /// <summary>
        /// Constructor. creates event handlers
        /// </summary>
        public BoundCheckedListBox()
        {
            childListChangedHandler = new ListChangedEventHandler(dataManager_childListChanged);
            childPositionChangedHandler = new EventHandler(dataManager_childPositionChanged);

            parentListChangedHandler = new ListChangedEventHandler(dataManager_parentListChanged);
            parentPositionChangedHandler = new EventHandler(dataManager_parentPositionChanged);

            relationListChangedHandler = new ListChangedEventHandler(dataManager_relationListChanged);
            //childPositionChangedHandler = new EventHandler(dataManager_childPositionChanged);

            itemCheckChangesHandler = new ItemCheckEventHandler(BoundCheckedListBox_ItemCheck);
            this.ItemCheck += itemCheckChangesHandler;
            selectionChangedHandler = new EventHandler(BoundCheckedListBox_SelectedIndexChanged);
            this.SelectedIndexChanged += selectionChangedHandler;
        }

        #region events
        #region listbox-fired events   
        /// <summary>
        /// Is called when (un)checking items of the listbox. updates the datasource relation table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoundCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!ready) return;

            if (e.Index < 0 || e.Index >= this.Items.Count)
                return;

            //just in case someone adds items manually
            //do not do that!
            if (this.Items[e.Index].GetType() != typeof(ListBoxItem))
                return;

            //find the currently selected ID in the parent table            
            object parentID = null;
            object childID = ((ListBoxItem)this.Items[e.Index]).Value;
            try
            {
                //do we always have type datarowview?!
                //another solution is parentDataManager.GetItemProperties().Find(parentIDMember, false).GetValue(parentDataManager.List[parentDataManager.Position]);
                //currencymanager.current shows buggy behavior sometimes when used with tostring()
                DataRowView drv = (DataRowView)this.parentDataManager.Current;
                parentID = drv[this.parentIDMember];
            }
            catch (Exception) { }

            if (parentID == null || childID == null)
                return;
            ArrayList rows = new ArrayList();
            PropertyDescriptorCollection relationProps = relationDataManager.GetItemProperties();

            PropertyDescriptor parentValue = relationProps.Find(parentValueMember, false);
            PropertyDescriptor childValue = relationProps.Find(childValueMember, false);

            //find row(s) with current parentID and childId of item being (un)checked
            for (int i = 0; i < relationDataManager.Count; i++)
            {
                object row = relationDataManager.List[i];
                object val = parentValue.GetValue(row);
                if (!parentID.Equals(val))
                    continue;

                val = childValue.GetValue(row);

                if (childID.Equals(val))
                    rows.Add(row);
            }

            if (e.NewValue == CheckState.Checked)
            {
                //case: changing to checked state
                bool exists = false;
                foreach (DataRowView curRow in rows)
                {
                    //just in case of more than one row with same IDs, keep only one
                    if (exists)
                        curRow.Delete();

                    //TODO: we should rather undelete deleted rows than always create new ones and delete them
                    //means we might end in having several deleted rows with same ids
                    //but how to get hands on 'em?! maybe through dataview or sth.
                    /*if (curRow.Row.RowState == DataRowState.Deleted)
                        curRow.Row.
                        continue;*/

                    if (curRow.Row.RowState != DataRowState.Deleted &&
                        curRow.Row.RowState != DataRowState.Detached)
                    {
                        exists = true;
                    }
                }
                //if none was found, which should be the normal case, create one
                //attention! assign allownull to your colums in your dataset or assign proper default values
                if (!exists)
                {
                    relationDataManager.AddNew();
                    ((DataRowView)relationDataManager.Current)[parentValueMember] = parentID;
                    ((DataRowView)relationDataManager.Current)[childValueMember] = childID;
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                //case: changing to unchecked state
                //delete all concerned rows
                foreach (DataRowView curRow in rows)
                {
                    if (curRow.Row.RowState == DataRowState.Deleted)
                        continue;
                    else
                        curRow.Delete();
                }
            }
        }
        /// <summary>
        /// Is called when selcted item of listbox changed. updates position of the child table 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoundCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.ready || this.SelectedItem == null || this.SelectedItem.GetType() != typeof(ListBoxItem))
                    return;

            }
            catch
            {
                return; //if selecteditem throws array out of bounds exception 
            }

            object val = ((ListBoxItem)this.SelectedItem).Value;

            PropertyDescriptorCollection childProps = childDataManager.GetItemProperties();
            PropertyDescriptor childID = childProps.Find(childIDMember, false);
            for (int i = 0; i < childDataManager.Count; i++)
            {
                object row = childDataManager.List[i];
                object val2 = childID.GetValue(row);
                if (val.Equals(val2))
                {
                    childDataManager.PositionChanged -= this.childPositionChangedHandler;
                    childDataManager.Position = i;
                    childDataManager.PositionChanged += this.childPositionChangedHandler;
                    return;
                }
            }
        }
        #endregion
        #region currencymanager-fired events
        #region from parentdatamanger
        /// <summary>
        /// Is called when list of parentdatasource changes. updates checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataManager_parentListChanged(object sender, ListChangedEventArgs e)
        {
            if (!ready) return;
            setHaken();
        }
        /// <summary>
        /// Is called when position of parent list changes. updates checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataManager_parentPositionChanged(object sender, EventArgs e)
        {
            if (!ready) return;
            setHaken();
        }
        #endregion
        #region from childdatamanager        
        /// <summary>
        /// Is called when list of child datasource changes. updates listboxitems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataManager_childListChanged(object sender, ListChangedEventArgs e)
        {
            if (!ready) return;
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    this.updateView();
                    //TODO: we could do this more precisely
                    break;
                case ListChangedType.ItemChanged:
                    this.updateView();
                    //TODO: we could do this more precisely
                    break;
                case ListChangedType.ItemDeleted:
                    this.updateView();
                    //TODO: we could do this more precisely
                    break;
                case ListChangedType.ItemMoved:
                    this.updateView();
                    break;
                case ListChangedType.PropertyDescriptorAdded:
                    break;
                case ListChangedType.PropertyDescriptorChanged:
                    if (!this.checkManagers()) tryDataBinding();
                    break;
                case ListChangedType.PropertyDescriptorDeleted:
                    if (!this.checkManagers()) tryDataBinding();
                    break;
                case ListChangedType.Reset:
                    tryDataBinding();
                    break;
            }
        }
        /// <summary>
        /// Is called when position of child list changes. updates selected item in listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataManager_childPositionChanged(object sender, EventArgs e)
        {
            if (!ready) return;
            this.selectItem();
        }
        #endregion
        #region from relationdatamanager
        /// <summary>
        /// Is called when list of relation datasource changes. updates checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataManager_relationListChanged(object sender, ListChangedEventArgs e)
        {
            if (!ready) return;
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    setHaken();
                    break;
                case ListChangedType.ItemChanged:
                    setHaken();
                    break;
                case ListChangedType.ItemDeleted:
                    setHaken();
                    break;
                case ListChangedType.ItemMoved:
                    break;
                case ListChangedType.PropertyDescriptorAdded:
                    break;
                case ListChangedType.PropertyDescriptorChanged:
                    if (!this.checkManagers()) tryDataBinding();
                    break;
                case ListChangedType.PropertyDescriptorDeleted:
                    if (!this.checkManagers()) tryDataBinding();
                    break;
                case ListChangedType.Reset:
                    tryDataBinding();
                    break;
            }
        }
        #endregion
        #endregion
        #endregion

        #region trydatabinding functions
        /// <summary>
        /// Main function for binding to currencymanagers and calling update functions for listbox.
        /// Checks if everything is valid.
        /// </summary>
        private void tryDataBinding()
        {
            if (!(tryRelationDataBinding() &&
            tryParentDataBinding() &&
            tryChildDataBinding() &&
            checkManagers()))
            {
                this.Enabled = false;
                this.ready = false;
            }
            else
            {
                this.Enabled = true;
                this.ready = true;
                this.updateView();
            }
        }
        /// <summary>
        /// tries to get currencymanager of parent datasource and binds to it
        /// </summary>
        /// <returns>success or fail</returns>
        private bool tryParentDataBinding()
        {
            if (this.parentDataSource == null ||
                base.BindingContext == null)
                return false;

            CurrencyManager cm;
            try
            {
                cm = (CurrencyManager)base.BindingContext[this.parentDataSource];
            }
            catch (System.ArgumentException)
            {
                // If no CurrencyManager was found
                return false;
            }
            if (this.parentDataManager != cm)
            {
                // Unwire the old CurrencyManager
                if (this.parentDataManager != null)
                {
                    this.parentDataManager.ListChanged -= parentListChangedHandler;
                    this.parentDataManager.PositionChanged -= parentPositionChangedHandler;
                }
                this.parentDataManager = cm;
                // Wire the new CurrencyManager
                if (this.parentDataManager != null)
                {
                    this.parentDataManager.ListChanged += parentListChangedHandler;
                    this.parentDataManager.PositionChanged += parentPositionChangedHandler;
                }
            }
            return true;
        }
        /// <summary>
        /// tries to get currencymanager of child datasource and binds to it
        /// </summary>
        /// <returns>success or fail</returns>
        private bool tryChildDataBinding()
        {
            if (this.childDataSource == null ||
                base.BindingContext == null)
                return false;

            CurrencyManager cm;
            try
            {
                cm = (CurrencyManager)base.BindingContext[this.childDataSource];
            }
            catch (System.ArgumentException)
            {
                // If no CurrencyManager was found
                return false;
            }
            if (this.childDataManager != cm)
            {
                // Unwire the old CurrencyManager
                if (this.childDataManager != null)
                {
                    this.childDataManager.ListChanged -= childListChangedHandler;
                    this.childDataManager.PositionChanged -= childPositionChangedHandler;
                }
                this.childDataManager = cm;
                // Wire the new CurrencyManager
                if (this.childDataManager != null)
                {
                    this.childDataManager.ListChanged += childListChangedHandler;
                    this.childDataManager.PositionChanged += childPositionChangedHandler;
                }
            }
            return true;
        }
        /// <summary>
        /// tries to get currencymanager of relation datasource and binds to it
        /// </summary>
        /// <returns>success or fail</returns>
        private bool tryRelationDataBinding()
        {
            if (this.relationDataSource == null ||
                base.BindingContext == null)
                return false;

            CurrencyManager cm;
            try
            {
                cm = (CurrencyManager)base.BindingContext[this.RelationDataSource];
            }
            catch (System.ArgumentException)
            {
                // If no CurrencyManager was found
                return false;
            }
            if (this.relationDataManager != cm)
            {
                // Unwire the old CurrencyManager
                if (this.relationDataManager != null)
                {
                    this.relationDataManager.ListChanged -= relationListChangedHandler;
                    //this.relationDataManager.PositionChanged -= relationPositionChangedHandler;
                }
                this.relationDataManager = cm;
                // Wire the new CurrencyManager
                if (this.relationDataManager != null)
                {
                    this.relationDataManager.ListChanged += relationListChangedHandler;
                    //this.relationDataManager.PositionChanged += relationPositionChangedHandler;
                }
            }
            return true;
        }

        #endregion

        #region listbox sync functions
        /// <summary>
        /// checks if all managers and column names = member are set and if columns exist in datasources 
        /// </summary>
        /// <returns></returns>
        private bool checkManagers()
        {
            if (parentDataManager == null ||
                childDataManager == null ||
                relationDataManager == null)
                return false;
            if (childDisplayMember == null ||
                childValueMember == null ||
                childIDMember == null ||
                parentValueMember == null ||
                parentIDMember == null)
                return false;

            if (childDataManager.GetItemProperties().Find(childDisplayMember, false) == null ||
                childDataManager.GetItemProperties().Find(childIDMember, false) == null ||
                parentDataManager.GetItemProperties().Find(parentIDMember, false) == null ||
                relationDataManager.GetItemProperties().Find(childValueMember, false) == null ||
                relationDataManager.GetItemProperties().Find(parentValueMember, false) == null)
                return false;

            return true;
        }

        /// <summary>
        /// checks the checkboxes according to datasource
        /// </summary>
        private void setHaken()
        {
            if (!ready) return;

            this.ItemCheck -= itemCheckChangesHandler;

            //find the currently selected ID in the parent table            
            object parentID = null;
            try
            {
                //do we always have type datarowview?!
                //another solution is parentDataManager.GetItemProperties().Find(parentIDMember, false).GetValue(parentDataManager.List[parentDataManager.Position]);
                //currencymanager.current shows buggy behavior sometimes
                DataRowView drv = (DataRowView)this.parentDataManager.Current;
                parentID = drv[this.parentIDMember];
            }
            catch (Exception) { }
            if (parentID == null)
                return;

            ArrayList childValues = new ArrayList();
            PropertyDescriptorCollection relationProps = relationDataManager.GetItemProperties();

            PropertyDescriptor parentValue = relationProps.Find(parentValueMember, false);
            PropertyDescriptor childValue = relationProps.Find(childValueMember, false);
            for (int i = 0; i < relationDataManager.Count; i++)
            {
                object row = relationDataManager.List[i];
                object val = parentValue.GetValue(row);
                if (parentID.Equals(val))
                    childValues.Add(childValue.GetValue(row));
            }

            for (int i = 0; i < this.Items.Count; i++)
            {

                if (this.Items[i].GetType() != typeof(ListBoxItem))
                {
                    this.SetItemChecked(i, false);
                    continue;
                }

                if (childValues.Contains(((ListBoxItem)this.Items[i]).Value))
                {
                    this.SetItemChecked(i, true);
                }
                else
                    this.SetItemChecked(i, false);
            }
            this.ItemCheck += itemCheckChangesHandler;
        }

        /// <summary>
        /// fills the listbox, checks and selects items
        /// </summary>
        private void updateView()
        {
            if (!ready) return;
            //liste fьllen
            this.Items.Clear();
            for (int i = 0; i < childDataManager.Count; i++)
            {
                this.Items.Add(getItem(i));
            }
            //haken setzen
            setHaken();
            selectItem();
        }

        /// <summary>
        /// Returns a <see cref="ListBoxItem"/> which holds ID and displaymember information of child datasource at given index.
        /// </summary>
        /// <param name="index">The index of the row.</param>
        /// <returns>A item wich contains the data.</returns>
        private ListBoxItem getItem(int index)
        {
            object row = childDataManager.List[index];
            PropertyDescriptorCollection propColl = childDataManager.GetItemProperties();

            //is this dangerous?!, != null?
            return new ListBoxItem(propColl.Find(ChildIDMember, false).GetValue(row),
                                   propColl.Find(childDisplayMember, false).GetValue(row));
        }


        /// <summary>
        /// selects the item according to position of currencymanager of child datasource
        /// </summary>
        private void selectItem()
        {
            if (!ready) return;

            this.ClearSelected();
            this.SelectedIndexChanged -= selectionChangedHandler;

            try
            {
                //do we always have type datarowview?!
                //another solution is childDataManager.GetItemProperties().Find(childIDMember, false).GetValue(childDataManager.List[childDataManager.Position]);
                //currencymanager.current shows buggy behavior sometimes when used with tostring()
                DataRowView drv = (DataRowView)this.childDataManager.Current;
                object childID = drv[this.childIDMember];

                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i].GetType() != typeof(ListBoxItem))
                        continue;

                    if (((ListBoxItem)this.Items[i]).Value.Equals(childID))
                    {
                        this.SetSelected(i, true);
                        break;
                    }
                }

            }
            catch (Exception) { }

            this.SelectedIndexChanged += selectionChangedHandler;
        }

        #endregion

    }
}
