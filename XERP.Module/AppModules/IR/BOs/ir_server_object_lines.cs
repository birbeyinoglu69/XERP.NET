
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
	[DefaultProperty("type")]
    [Persistent("ir_server_object_lines")]
	public partial class ir_server_object_lines : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_server_object_lines_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_server_object_lines_create_uid
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
            //FK FK_ir_server_object_lines_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_act_server fserver_id;
            //FK FK_ir_server_object_lines_server_id
            [Custom("Caption", "Server Id")]
            public ir_act_server server_id {
                get { return fserver_id; }
                set { SetPropertyValue<ir_act_server>("server_id", ref fserver_id, value); }
            }
    
            private System.String ftype;
            [Size(32)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String fvalue;
            [Size(-1)]
            [Custom("Caption", "Value")]
            public System.String value {
                get { return fvalue; }
                set { SetPropertyValue("value", ref fvalue, value); }
            }
    
        
            private ir_model_fields fcol1;
            //FK FK_ir_server_object_lines_col1
            [Custom("Caption", "Col1")]
            public ir_model_fields col1 {
                get { return fcol1; }
                set { SetPropertyValue<ir_model_fields>("col1", ref fcol1, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_server_object_lines(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

