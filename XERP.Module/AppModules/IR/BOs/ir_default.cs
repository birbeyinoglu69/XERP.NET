
using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace XERP
{



    [DefaultClassOptions]
    [DeferredDeletion(true)]
	[DefaultProperty("ref_table")]
    [Persistent("ir_default")]
	public partial class ir_default : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_default_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_default_create_uid
            [Custom("Caption", "Create Uid")]
            public res_users create_uid {
                get { return fcreate_uid; }
                set { SetPropertyValue<res_users>("create_uid", ref fcreate_uid, value); }
            }
    
            private DateTime? fcreate_date;
            [Custom("Caption", "Create Date")]
            public DateTime? create_date {
                get { return fcreate_date; }
                set { SetPropertyValue("create_date", ref fcreate_date, value); }
            }
    
            private DateTime? fwrite_date;
            [Custom("Caption", "Write Date")]
            public DateTime? write_date {
                get { return fwrite_date; }
                set { SetPropertyValue("write_date", ref fwrite_date, value); }
            }
    
        
            private res_users fwrite_uid;
            //FK FK_ir_default_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_users fuid;
            //FK FK_ir_default_uid
            [Custom("Caption", "Uid")]
            public res_users uid {
                get { return fuid; }
                set { SetPropertyValue<res_users>("uid", ref fuid, value); }
            }
    
            private System.String fref_table;
            [Size(64)]
            [Custom("Caption", "Ref Table")]
            public System.String ref_table {
                get { return fref_table; }
                set { SetPropertyValue("ref_table", ref fref_table, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_ir_default_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
            private System.String fvalue;
            [Size(64)]
            [Custom("Caption", "Value")]
            public System.String value {
                get { return fvalue; }
                set { SetPropertyValue("value", ref fvalue, value); }
            }
    
            private System.Int32 fref_id;
            [Custom("Caption", "Ref Id")]
            public System.Int32 ref_id {
                get { return fref_id; }
                set { SetPropertyValue("ref_id", ref fref_id, value); }
            }
    
            private System.String ffield_tbl;
            [Size(64)]
            [Custom("Caption", "Field Tbl")]
            public System.String field_tbl {
                get { return ffield_tbl; }
                set { SetPropertyValue("field_tbl", ref ffield_tbl, value); }
            }
    
            private System.String ffield_name;
            [Size(64)]
            [Custom("Caption", "Field Name")]
            public System.String field_name {
                get { return ffield_name; }
                set { SetPropertyValue("field_name", ref ffield_name, value); }
            }
    
            private System.String fpage;
            [Size(64)]
            [Custom("Caption", "Page")]
            public System.String page {
                get { return fpage; }
                set { SetPropertyValue("page", ref fpage, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_default(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

