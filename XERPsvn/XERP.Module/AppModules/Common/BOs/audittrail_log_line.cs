
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
    [DeferredDeletion(false)]
	[DefaultProperty("old_value")]
    [Persistent("audittrail_log_line")]
	public partial class audittrail_log_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //audittrail_log_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_audittrail_log_line_create_uid
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
            //FK FK_audittrail_log_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 flog;
            [Custom("Caption", "Log")]
            public System.Int32 log {
                get { return flog; }
                set { SetPropertyValue("log", ref flog, value); }
            }
    
        
            private audittrail_log flog_id;
            //FK FK_audittrail_log_line_log_id
            [Custom("Caption", "Log Id")]
            public audittrail_log log_id {
                get { return flog_id; }
                set { SetPropertyValue<audittrail_log>("log_id", ref flog_id, value); }
            }
    
            private System.String fold_value;
            [Size(-1)]
            [Custom("Caption", "Old Value")]
            public System.String old_value {
                get { return fold_value; }
                set { SetPropertyValue("old_value", ref fold_value, value); }
            }
    
        
            private ir_model_fields ffield_id;
            //FK FK_audittrail_log_line_field_id
            [Custom("Caption", "Field Id")]
            public ir_model_fields field_id {
                get { return ffield_id; }
                set { SetPropertyValue<ir_model_fields>("field_id", ref ffield_id, value); }
            }
    
            private System.String fnew_value_text;
            [Size(-1)]
            [Custom("Caption", "New Value text")]
            public System.String new_value_text {
                get { return fnew_value_text; }
                set { SetPropertyValue("new_value_text", ref fnew_value_text, value); }
            }
    
            private System.String fold_value_text;
            [Size(-1)]
            [Custom("Caption", "Old Value text")]
            public System.String old_value_text {
                get { return fold_value_text; }
                set { SetPropertyValue("old_value_text", ref fold_value_text, value); }
            }
    
            private System.String fnew_value;
            [Size(-1)]
            [Custom("Caption", "New Value")]
            public System.String new_value {
                get { return fnew_value; }
                set { SetPropertyValue("new_value", ref fnew_value, value); }
            }
    
            private System.String ffield_description;
            [Size(64)]
            [Custom("Caption", "Field Description")]
            public System.String field_description {
                get { return ffield_description; }
                set { SetPropertyValue("field_description", ref ffield_description, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public audittrail_log_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

