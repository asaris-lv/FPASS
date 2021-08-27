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
    public class DSHistory : DataSet {
        
        private FPASS_HISTDataTable tableFPASS_HIST;
        
        public DSHistory() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected DSHistory(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["FPASS_HIST"] != null)) {
                    this.Tables.Add(new FPASS_HISTDataTable(ds.Tables["FPASS_HIST"]));
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
        public FPASS_HISTDataTable FPASS_HIST {
            get {
                return this.tableFPASS_HIST;
            }
        }
        
        public override DataSet Clone() {
            DSHistory cln = ((DSHistory)(base.Clone()));
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
            if ((ds.Tables["FPASS_HIST"] != null)) {
                this.Tables.Add(new FPASS_HISTDataTable(ds.Tables["FPASS_HIST"]));
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
            this.tableFPASS_HIST = ((FPASS_HISTDataTable)(this.Tables["FPASS_HIST"]));
            if ((this.tableFPASS_HIST != null)) {
                this.tableFPASS_HIST.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "DSHistory";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/DSHistory.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableFPASS_HIST = new FPASS_HISTDataTable();
            this.Tables.Add(this.tableFPASS_HIST);
        }
        
        private bool ShouldSerializeFPASS_HIST() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void FPASS_HISTRowChangeEventHandler(object sender, FPASS_HISTRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class FPASS_HISTDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnHIST_ID;
            
            private DataColumn columnHIST_USER_ID;
            
            private DataColumn columnHIST_CHANGEDATE;
            
            private DataColumn columnHIST_TABLENAME;
            
            private DataColumn columnHIST_COLUMNNAME;
            
            private DataColumn columnHIST_ROWID;
            
            private DataColumn columnHIST_OLDVALUE;
            
            private DataColumn columnHIST_NEWVALUE;
            
            private DataColumn columnHIST_DESCRIPTION;
            
            private DataColumn columnROWID;
            
            internal FPASS_HISTDataTable() : 
                    base("FPASS_HIST") {
                this.InitClass();
            }
            
            internal FPASS_HISTDataTable(DataTable table) : 
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
            
            internal DataColumn HIST_IDColumn {
                get {
                    return this.columnHIST_ID;
                }
            }
            
            internal DataColumn HIST_USER_IDColumn {
                get {
                    return this.columnHIST_USER_ID;
                }
            }
            
            internal DataColumn HIST_CHANGEDATEColumn {
                get {
                    return this.columnHIST_CHANGEDATE;
                }
            }
            
            internal DataColumn HIST_TABLENAMEColumn {
                get {
                    return this.columnHIST_TABLENAME;
                }
            }
            
            internal DataColumn HIST_COLUMNNAMEColumn {
                get {
                    return this.columnHIST_COLUMNNAME;
                }
            }
            
            internal DataColumn HIST_ROWIDColumn {
                get {
                    return this.columnHIST_ROWID;
                }
            }
            
            internal DataColumn HIST_OLDVALUEColumn {
                get {
                    return this.columnHIST_OLDVALUE;
                }
            }
            
            internal DataColumn HIST_NEWVALUEColumn {
                get {
                    return this.columnHIST_NEWVALUE;
                }
            }
            
            internal DataColumn HIST_DESCRIPTIONColumn {
                get {
                    return this.columnHIST_DESCRIPTION;
                }
            }
            
            internal DataColumn ROWIDColumn {
                get {
                    return this.columnROWID;
                }
            }
            
            public FPASS_HISTRow this[int index] {
                get {
                    return ((FPASS_HISTRow)(this.Rows[index]));
                }
            }
            
            public event FPASS_HISTRowChangeEventHandler FPASS_HISTRowChanged;
            
            public event FPASS_HISTRowChangeEventHandler FPASS_HISTRowChanging;
            
            public event FPASS_HISTRowChangeEventHandler FPASS_HISTRowDeleted;
            
            public event FPASS_HISTRowChangeEventHandler FPASS_HISTRowDeleting;
            
            public void AddFPASS_HISTRow(FPASS_HISTRow row) {
                this.Rows.Add(row);
            }
            
            public FPASS_HISTRow AddFPASS_HISTRow(System.Decimal HIST_ID, System.Decimal HIST_USER_ID, System.DateTime HIST_CHANGEDATE, string HIST_TABLENAME, string HIST_COLUMNNAME, System.Decimal HIST_ROWID, string HIST_OLDVALUE, string HIST_NEWVALUE, string HIST_DESCRIPTION, string ROWID) {
                FPASS_HISTRow rowFPASS_HISTRow = ((FPASS_HISTRow)(this.NewRow()));
                rowFPASS_HISTRow.ItemArray = new object[] {
                        HIST_ID,
                        HIST_USER_ID,
                        HIST_CHANGEDATE,
                        HIST_TABLENAME,
                        HIST_COLUMNNAME,
                        HIST_ROWID,
                        HIST_OLDVALUE,
                        HIST_NEWVALUE,
                        HIST_DESCRIPTION,
                        ROWID};
                this.Rows.Add(rowFPASS_HISTRow);
                return rowFPASS_HISTRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                FPASS_HISTDataTable cln = ((FPASS_HISTDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new FPASS_HISTDataTable();
            }
            
            internal void InitVars() {
                this.columnHIST_ID = this.Columns["HIST_ID"];
                this.columnHIST_USER_ID = this.Columns["HIST_USER_ID"];
                this.columnHIST_CHANGEDATE = this.Columns["HIST_CHANGEDATE"];
                this.columnHIST_TABLENAME = this.Columns["HIST_TABLENAME"];
                this.columnHIST_COLUMNNAME = this.Columns["HIST_COLUMNNAME"];
                this.columnHIST_ROWID = this.Columns["HIST_ROWID"];
                this.columnHIST_OLDVALUE = this.Columns["HIST_OLDVALUE"];
                this.columnHIST_NEWVALUE = this.Columns["HIST_NEWVALUE"];
                this.columnHIST_DESCRIPTION = this.Columns["HIST_DESCRIPTION"];
                this.columnROWID = this.Columns["ROWID"];
            }
            
            private void InitClass() {
                this.columnHIST_ID = new DataColumn("HIST_ID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_ID);
                this.columnHIST_USER_ID = new DataColumn("HIST_USER_ID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_USER_ID);
                this.columnHIST_CHANGEDATE = new DataColumn("HIST_CHANGEDATE", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_CHANGEDATE);
                this.columnHIST_TABLENAME = new DataColumn("HIST_TABLENAME", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_TABLENAME);
                this.columnHIST_COLUMNNAME = new DataColumn("HIST_COLUMNNAME", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_COLUMNNAME);
                this.columnHIST_ROWID = new DataColumn("HIST_ROWID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_ROWID);
                this.columnHIST_OLDVALUE = new DataColumn("HIST_OLDVALUE", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_OLDVALUE);
                this.columnHIST_NEWVALUE = new DataColumn("HIST_NEWVALUE", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_NEWVALUE);
                this.columnHIST_DESCRIPTION = new DataColumn("HIST_DESCRIPTION", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHIST_DESCRIPTION);
                this.columnROWID = new DataColumn("ROWID", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnROWID);
                this.columnHIST_ID.AllowDBNull = false;
                this.columnHIST_USER_ID.AllowDBNull = false;
                this.columnHIST_CHANGEDATE.AllowDBNull = false;
                this.columnHIST_TABLENAME.AllowDBNull = false;
                this.columnHIST_COLUMNNAME.AllowDBNull = false;
                this.columnHIST_ROWID.AllowDBNull = false;
                this.columnHIST_OLDVALUE.AllowDBNull = false;
                this.columnHIST_NEWVALUE.AllowDBNull = false;
                this.columnROWID.ReadOnly = true;
            }
            
            public FPASS_HISTRow NewFPASS_HISTRow() {
                return ((FPASS_HISTRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new FPASS_HISTRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(FPASS_HISTRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.FPASS_HISTRowChanged != null)) {
                    this.FPASS_HISTRowChanged(this, new FPASS_HISTRowChangeEvent(((FPASS_HISTRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.FPASS_HISTRowChanging != null)) {
                    this.FPASS_HISTRowChanging(this, new FPASS_HISTRowChangeEvent(((FPASS_HISTRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.FPASS_HISTRowDeleted != null)) {
                    this.FPASS_HISTRowDeleted(this, new FPASS_HISTRowChangeEvent(((FPASS_HISTRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.FPASS_HISTRowDeleting != null)) {
                    this.FPASS_HISTRowDeleting(this, new FPASS_HISTRowChangeEvent(((FPASS_HISTRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveFPASS_HISTRow(FPASS_HISTRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class FPASS_HISTRow : DataRow {
            
            private FPASS_HISTDataTable tableFPASS_HIST;
            
            internal FPASS_HISTRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableFPASS_HIST = ((FPASS_HISTDataTable)(this.Table));
            }
            
            public System.Decimal HIST_ID {
                get {
                    return ((System.Decimal)(this[this.tableFPASS_HIST.HIST_IDColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_IDColumn] = value;
                }
            }
            
            public System.Decimal HIST_USER_ID {
                get {
                    return ((System.Decimal)(this[this.tableFPASS_HIST.HIST_USER_IDColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_USER_IDColumn] = value;
                }
            }
            
            public System.DateTime HIST_CHANGEDATE {
                get {
                    return ((System.DateTime)(this[this.tableFPASS_HIST.HIST_CHANGEDATEColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_CHANGEDATEColumn] = value;
                }
            }
            
            public string HIST_TABLENAME {
                get {
                    return ((string)(this[this.tableFPASS_HIST.HIST_TABLENAMEColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_TABLENAMEColumn] = value;
                }
            }
            
            public string HIST_COLUMNNAME {
                get {
                    return ((string)(this[this.tableFPASS_HIST.HIST_COLUMNNAMEColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_COLUMNNAMEColumn] = value;
                }
            }
            
            public System.Decimal HIST_ROWID {
                get {
                    return ((System.Decimal)(this[this.tableFPASS_HIST.HIST_ROWIDColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_ROWIDColumn] = value;
                }
            }
            
            public string HIST_OLDVALUE {
                get {
                    return ((string)(this[this.tableFPASS_HIST.HIST_OLDVALUEColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_OLDVALUEColumn] = value;
                }
            }
            
            public string HIST_NEWVALUE {
                get {
                    return ((string)(this[this.tableFPASS_HIST.HIST_NEWVALUEColumn]));
                }
                set {
                    this[this.tableFPASS_HIST.HIST_NEWVALUEColumn] = value;
                }
            }
            
            public string HIST_DESCRIPTION {
                get {
                    try {
                        return ((string)(this[this.tableFPASS_HIST.HIST_DESCRIPTIONColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableFPASS_HIST.HIST_DESCRIPTIONColumn] = value;
                }
            }
            
            public string ROWID {
                get {
                    try {
                        return ((string)(this[this.tableFPASS_HIST.ROWIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableFPASS_HIST.ROWIDColumn] = value;
                }
            }
            
            public bool IsHIST_DESCRIPTIONNull() {
                return this.IsNull(this.tableFPASS_HIST.HIST_DESCRIPTIONColumn);
            }
            
            public void SetHIST_DESCRIPTIONNull() {
                this[this.tableFPASS_HIST.HIST_DESCRIPTIONColumn] = System.Convert.DBNull;
            }
            
            public bool IsROWIDNull() {
                return this.IsNull(this.tableFPASS_HIST.ROWIDColumn);
            }
            
            public void SetROWIDNull() {
                this[this.tableFPASS_HIST.ROWIDColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class FPASS_HISTRowChangeEvent : EventArgs {
            
            private FPASS_HISTRow eventRow;
            
            private DataRowAction eventAction;
            
            public FPASS_HISTRowChangeEvent(FPASS_HISTRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public FPASS_HISTRow Row {
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