﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Degussa.FPASS.Db.DataSets {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class DSUser : DataSet {
        
        private VW_FPASS_USERBYROLEDataTable tableVW_FPASS_USERBYROLE;
        
        public DSUser() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected DSUser(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["VW_FPASS_USERBYROLE"] != null)) {
                    this.Tables.Add(new VW_FPASS_USERBYROLEDataTable(ds.Tables["VW_FPASS_USERBYROLE"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public VW_FPASS_USERBYROLEDataTable VW_FPASS_USERBYROLE {
            get {
                return this.tableVW_FPASS_USERBYROLE;
            }
        }
        
        public override DataSet Clone() {
            DSUser cln = ((DSUser)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["VW_FPASS_USERBYROLE"] != null)) {
                this.Tables.Add(new VW_FPASS_USERBYROLEDataTable(ds.Tables["VW_FPASS_USERBYROLE"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableVW_FPASS_USERBYROLE = ((VW_FPASS_USERBYROLEDataTable)(this.Tables["VW_FPASS_USERBYROLE"]));
            if ((this.tableVW_FPASS_USERBYROLE != null)) {
                this.tableVW_FPASS_USERBYROLE.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "DSUser";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/DSUser.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableVW_FPASS_USERBYROLE = new VW_FPASS_USERBYROLEDataTable();
            this.Tables.Add(this.tableVW_FPASS_USERBYROLE);
        }
        
        private bool ShouldSerializeVW_FPASS_USERBYROLE() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void VW_FPASS_USERBYROLERowChangeEventHandler(object sender, VW_FPASS_USERBYROLERowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VW_FPASS_USERBYROLEDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnUM_USER_ID;
            
            private DataColumn columnFPASS_USER_ID;
            
            private DataColumn columnBOTHNAMESTEL;
            
            private DataColumn columnUM_USERAPPLID;
            
            internal VW_FPASS_USERBYROLEDataTable() : 
                    base("VW_FPASS_USERBYROLE") {
                this.InitClass();
            }
            
            internal VW_FPASS_USERBYROLEDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn UM_USER_IDColumn {
                get {
                    return this.columnUM_USER_ID;
                }
            }
            
            internal DataColumn FPASS_USER_IDColumn {
                get {
                    return this.columnFPASS_USER_ID;
                }
            }
            
            internal DataColumn BOTHNAMESTELColumn {
                get {
                    return this.columnBOTHNAMESTEL;
                }
            }
            
            internal DataColumn UM_USERAPPLIDColumn {
                get {
                    return this.columnUM_USERAPPLID;
                }
            }
            
            public VW_FPASS_USERBYROLERow this[int index] {
                get {
                    return ((VW_FPASS_USERBYROLERow)(this.Rows[index]));
                }
            }
            
            public event VW_FPASS_USERBYROLERowChangeEventHandler VW_FPASS_USERBYROLERowChanged;
            
            public event VW_FPASS_USERBYROLERowChangeEventHandler VW_FPASS_USERBYROLERowChanging;
            
            public event VW_FPASS_USERBYROLERowChangeEventHandler VW_FPASS_USERBYROLERowDeleted;
            
            public event VW_FPASS_USERBYROLERowChangeEventHandler VW_FPASS_USERBYROLERowDeleting;
            
            public void AddVW_FPASS_USERBYROLERow(VW_FPASS_USERBYROLERow row) {
                this.Rows.Add(row);
            }
            
            public VW_FPASS_USERBYROLERow AddVW_FPASS_USERBYROLERow(System.Decimal UM_USER_ID, System.Decimal FPASS_USER_ID, string BOTHNAMESTEL, string UM_USERAPPLID) {
                VW_FPASS_USERBYROLERow rowVW_FPASS_USERBYROLERow = ((VW_FPASS_USERBYROLERow)(this.NewRow()));
                rowVW_FPASS_USERBYROLERow.ItemArray = new object[] {
                        UM_USER_ID,
                        FPASS_USER_ID,
                        BOTHNAMESTEL,
                        UM_USERAPPLID};
                this.Rows.Add(rowVW_FPASS_USERBYROLERow);
                return rowVW_FPASS_USERBYROLERow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                VW_FPASS_USERBYROLEDataTable cln = ((VW_FPASS_USERBYROLEDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new VW_FPASS_USERBYROLEDataTable();
            }
            
            internal void InitVars() {
                this.columnUM_USER_ID = this.Columns["UM_USER_ID"];
                this.columnFPASS_USER_ID = this.Columns["FPASS_USER_ID"];
                this.columnBOTHNAMESTEL = this.Columns["BOTHNAMESTEL"];
                this.columnUM_USERAPPLID = this.Columns["UM_USERAPPLID"];
            }
            
            private void InitClass() {
                this.columnUM_USER_ID = new DataColumn("UM_USER_ID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUM_USER_ID);
                this.columnFPASS_USER_ID = new DataColumn("FPASS_USER_ID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnFPASS_USER_ID);
                this.columnBOTHNAMESTEL = new DataColumn("BOTHNAMESTEL", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnBOTHNAMESTEL);
                this.columnUM_USERAPPLID = new DataColumn("UM_USERAPPLID", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUM_USERAPPLID);
                this.columnUM_USER_ID.AllowDBNull = false;
                this.columnFPASS_USER_ID.AllowDBNull = false;
                this.columnUM_USERAPPLID.AllowDBNull = false;
            }
            
            public VW_FPASS_USERBYROLERow NewVW_FPASS_USERBYROLERow() {
                return ((VW_FPASS_USERBYROLERow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new VW_FPASS_USERBYROLERow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(VW_FPASS_USERBYROLERow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.VW_FPASS_USERBYROLERowChanged != null)) {
                    this.VW_FPASS_USERBYROLERowChanged(this, new VW_FPASS_USERBYROLERowChangeEvent(((VW_FPASS_USERBYROLERow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.VW_FPASS_USERBYROLERowChanging != null)) {
                    this.VW_FPASS_USERBYROLERowChanging(this, new VW_FPASS_USERBYROLERowChangeEvent(((VW_FPASS_USERBYROLERow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.VW_FPASS_USERBYROLERowDeleted != null)) {
                    this.VW_FPASS_USERBYROLERowDeleted(this, new VW_FPASS_USERBYROLERowChangeEvent(((VW_FPASS_USERBYROLERow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.VW_FPASS_USERBYROLERowDeleting != null)) {
                    this.VW_FPASS_USERBYROLERowDeleting(this, new VW_FPASS_USERBYROLERowChangeEvent(((VW_FPASS_USERBYROLERow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveVW_FPASS_USERBYROLERow(VW_FPASS_USERBYROLERow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VW_FPASS_USERBYROLERow : DataRow {
            
            private VW_FPASS_USERBYROLEDataTable tableVW_FPASS_USERBYROLE;
            
            internal VW_FPASS_USERBYROLERow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableVW_FPASS_USERBYROLE = ((VW_FPASS_USERBYROLEDataTable)(this.Table));
            }
            
            public System.Decimal UM_USER_ID {
                get {
                    return ((System.Decimal)(this[this.tableVW_FPASS_USERBYROLE.UM_USER_IDColumn]));
                }
                set {
                    this[this.tableVW_FPASS_USERBYROLE.UM_USER_IDColumn] = value;
                }
            }
            
            public System.Decimal FPASS_USER_ID {
                get {
                    return ((System.Decimal)(this[this.tableVW_FPASS_USERBYROLE.FPASS_USER_IDColumn]));
                }
                set {
                    this[this.tableVW_FPASS_USERBYROLE.FPASS_USER_IDColumn] = value;
                }
            }
            
            public string BOTHNAMESTEL {
                get {
                    try {
                        return ((string)(this[this.tableVW_FPASS_USERBYROLE.BOTHNAMESTELColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVW_FPASS_USERBYROLE.BOTHNAMESTELColumn] = value;
                }
            }
            
            public string UM_USERAPPLID {
                get {
                    return ((string)(this[this.tableVW_FPASS_USERBYROLE.UM_USERAPPLIDColumn]));
                }
                set {
                    this[this.tableVW_FPASS_USERBYROLE.UM_USERAPPLIDColumn] = value;
                }
            }
            
            public bool IsBOTHNAMESTELNull() {
                return this.IsNull(this.tableVW_FPASS_USERBYROLE.BOTHNAMESTELColumn);
            }
            
            public void SetBOTHNAMESTELNull() {
                this[this.tableVW_FPASS_USERBYROLE.BOTHNAMESTELColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VW_FPASS_USERBYROLERowChangeEvent : EventArgs {
            
            private VW_FPASS_USERBYROLERow eventRow;
            
            private DataRowAction eventAction;
            
            public VW_FPASS_USERBYROLERowChangeEvent(VW_FPASS_USERBYROLERow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public VW_FPASS_USERBYROLERow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}