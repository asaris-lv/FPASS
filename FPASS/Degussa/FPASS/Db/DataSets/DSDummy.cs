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
    public class DSDummy : DataSet {
        
        private FPASS_DUMMYDataTable tableFPASS_DUMMY;
        
        public DSDummy() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected DSDummy(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["FPASS_DUMMY"] != null)) {
                    this.Tables.Add(new FPASS_DUMMYDataTable(ds.Tables["FPASS_DUMMY"]));
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
        public FPASS_DUMMYDataTable FPASS_DUMMY {
            get {
                return this.tableFPASS_DUMMY;
            }
        }
        
        public override DataSet Clone() {
            DSDummy cln = ((DSDummy)(base.Clone()));
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
            if ((ds.Tables["FPASS_DUMMY"] != null)) {
                this.Tables.Add(new FPASS_DUMMYDataTable(ds.Tables["FPASS_DUMMY"]));
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
            this.tableFPASS_DUMMY = ((FPASS_DUMMYDataTable)(this.Tables["FPASS_DUMMY"]));
            if ((this.tableFPASS_DUMMY != null)) {
                this.tableFPASS_DUMMY.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "DSDummy";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/DSDummy.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableFPASS_DUMMY = new FPASS_DUMMYDataTable();
            this.Tables.Add(this.tableFPASS_DUMMY);
        }
        
        private bool ShouldSerializeFPASS_DUMMY() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void FPASS_DUMMYRowChangeEventHandler(object sender, FPASS_DUMMYRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class FPASS_DUMMYDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnDUMMY_ID;
            
            private DataColumn columnSURNAME;
            
            private DataColumn columnFIRSTNAME;
            
            private DataColumn columnCOORD_ID;
            
            private DataColumn columnCONTRACTOR_ID;
            
            internal FPASS_DUMMYDataTable() : 
                    base("FPASS_DUMMY") {
                this.InitClass();
            }
            
            internal FPASS_DUMMYDataTable(DataTable table) : 
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
            
            internal DataColumn DUMMY_IDColumn {
                get {
                    return this.columnDUMMY_ID;
                }
            }
            
            internal DataColumn SURNAMEColumn {
                get {
                    return this.columnSURNAME;
                }
            }
            
            internal DataColumn FIRSTNAMEColumn {
                get {
                    return this.columnFIRSTNAME;
                }
            }
            
            internal DataColumn COORD_IDColumn {
                get {
                    return this.columnCOORD_ID;
                }
            }
            
            internal DataColumn CONTRACTOR_IDColumn {
                get {
                    return this.columnCONTRACTOR_ID;
                }
            }
            
            public FPASS_DUMMYRow this[int index] {
                get {
                    return ((FPASS_DUMMYRow)(this.Rows[index]));
                }
            }
            
            public event FPASS_DUMMYRowChangeEventHandler FPASS_DUMMYRowChanged;
            
            public event FPASS_DUMMYRowChangeEventHandler FPASS_DUMMYRowChanging;
            
            public event FPASS_DUMMYRowChangeEventHandler FPASS_DUMMYRowDeleted;
            
            public event FPASS_DUMMYRowChangeEventHandler FPASS_DUMMYRowDeleting;
            
            public void AddFPASS_DUMMYRow(FPASS_DUMMYRow row) {
                this.Rows.Add(row);
            }
            
            public FPASS_DUMMYRow AddFPASS_DUMMYRow(System.Decimal DUMMY_ID, string SURNAME, string FIRSTNAME, System.Decimal COORD_ID, System.Decimal CONTRACTOR_ID) {
                FPASS_DUMMYRow rowFPASS_DUMMYRow = ((FPASS_DUMMYRow)(this.NewRow()));
                rowFPASS_DUMMYRow.ItemArray = new object[] {
                        DUMMY_ID,
                        SURNAME,
                        FIRSTNAME,
                        COORD_ID,
                        CONTRACTOR_ID};
                this.Rows.Add(rowFPASS_DUMMYRow);
                return rowFPASS_DUMMYRow;
            }
            
            public FPASS_DUMMYRow FindByDUMMY_ID(System.Decimal DUMMY_ID) {
                return ((FPASS_DUMMYRow)(this.Rows.Find(new object[] {
                            DUMMY_ID})));
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                FPASS_DUMMYDataTable cln = ((FPASS_DUMMYDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new FPASS_DUMMYDataTable();
            }
            
            internal void InitVars() {
                this.columnDUMMY_ID = this.Columns["DUMMY_ID"];
                this.columnSURNAME = this.Columns["SURNAME"];
                this.columnFIRSTNAME = this.Columns["FIRSTNAME"];
                this.columnCOORD_ID = this.Columns["COORD_ID"];
                this.columnCONTRACTOR_ID = this.Columns["CONTRACTOR_ID"];
            }
            
            private void InitClass() {
                this.columnDUMMY_ID = new DataColumn("DUMMY_ID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDUMMY_ID);
                this.columnSURNAME = new DataColumn("SURNAME", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnSURNAME);
                this.columnFIRSTNAME = new DataColumn("FIRSTNAME", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnFIRSTNAME);
                this.columnCOORD_ID = new DataColumn("COORD_ID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCOORD_ID);
                this.columnCONTRACTOR_ID = new DataColumn("CONTRACTOR_ID", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCONTRACTOR_ID);
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnDUMMY_ID}, true));
                this.columnDUMMY_ID.AllowDBNull = false;
                this.columnDUMMY_ID.Unique = true;
                this.columnSURNAME.AllowDBNull = false;
                this.columnCONTRACTOR_ID.AllowDBNull = false;
            }
            
            public FPASS_DUMMYRow NewFPASS_DUMMYRow() {
                return ((FPASS_DUMMYRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new FPASS_DUMMYRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(FPASS_DUMMYRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.FPASS_DUMMYRowChanged != null)) {
                    this.FPASS_DUMMYRowChanged(this, new FPASS_DUMMYRowChangeEvent(((FPASS_DUMMYRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.FPASS_DUMMYRowChanging != null)) {
                    this.FPASS_DUMMYRowChanging(this, new FPASS_DUMMYRowChangeEvent(((FPASS_DUMMYRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.FPASS_DUMMYRowDeleted != null)) {
                    this.FPASS_DUMMYRowDeleted(this, new FPASS_DUMMYRowChangeEvent(((FPASS_DUMMYRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.FPASS_DUMMYRowDeleting != null)) {
                    this.FPASS_DUMMYRowDeleting(this, new FPASS_DUMMYRowChangeEvent(((FPASS_DUMMYRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveFPASS_DUMMYRow(FPASS_DUMMYRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class FPASS_DUMMYRow : DataRow {
            
            private FPASS_DUMMYDataTable tableFPASS_DUMMY;
            
            internal FPASS_DUMMYRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableFPASS_DUMMY = ((FPASS_DUMMYDataTable)(this.Table));
            }
            
            public System.Decimal DUMMY_ID {
                get {
                    return ((System.Decimal)(this[this.tableFPASS_DUMMY.DUMMY_IDColumn]));
                }
                set {
                    this[this.tableFPASS_DUMMY.DUMMY_IDColumn] = value;
                }
            }
            
            public string SURNAME {
                get {
                    return ((string)(this[this.tableFPASS_DUMMY.SURNAMEColumn]));
                }
                set {
                    this[this.tableFPASS_DUMMY.SURNAMEColumn] = value;
                }
            }
            
            public string FIRSTNAME {
                get {
                    try {
                        return ((string)(this[this.tableFPASS_DUMMY.FIRSTNAMEColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableFPASS_DUMMY.FIRSTNAMEColumn] = value;
                }
            }
            
            public System.Decimal COORD_ID {
                get {
                    try {
                        return ((System.Decimal)(this[this.tableFPASS_DUMMY.COORD_IDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableFPASS_DUMMY.COORD_IDColumn] = value;
                }
            }
            
            public System.Decimal CONTRACTOR_ID {
                get {
                    return ((System.Decimal)(this[this.tableFPASS_DUMMY.CONTRACTOR_IDColumn]));
                }
                set {
                    this[this.tableFPASS_DUMMY.CONTRACTOR_IDColumn] = value;
                }
            }
            
            public bool IsFIRSTNAMENull() {
                return this.IsNull(this.tableFPASS_DUMMY.FIRSTNAMEColumn);
            }
            
            public void SetFIRSTNAMENull() {
                this[this.tableFPASS_DUMMY.FIRSTNAMEColumn] = System.Convert.DBNull;
            }
            
            public bool IsCOORD_IDNull() {
                return this.IsNull(this.tableFPASS_DUMMY.COORD_IDColumn);
            }
            
            public void SetCOORD_IDNull() {
                this[this.tableFPASS_DUMMY.COORD_IDColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class FPASS_DUMMYRowChangeEvent : EventArgs {
            
            private FPASS_DUMMYRow eventRow;
            
            private DataRowAction eventAction;
            
            public FPASS_DUMMYRowChangeEvent(FPASS_DUMMYRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public FPASS_DUMMYRow Row {
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
