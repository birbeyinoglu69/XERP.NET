
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
	[DefaultProperty("name")]
    [Persistent("audittrail_log")]
	public partial class audittrail_log : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //audittrail_log_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_audittrail_log_create_uid
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
            //FK FK_audittrail_log_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_audittrail_log_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(32)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private DateTime? ftimestamp;
            [Custom("Caption", "Timestamp")]
            public DateTime? timestamp {
                get { return ftimestamp; }
                set { SetPropertyValue("timestamp", ref ftimestamp, value); }
            }
    
            private System.Int32 fres_id;
            [Custom("Caption", "Res Id")]
            public System.Int32 res_id {
                get { return fres_id; }
                set { SetPropertyValue("res_id", ref fres_id, value); }
            }
    
            private System.String fmethod;
            [Size(16)]
            [Custom("Caption", "Method")]
            public System.String method {
                get { return fmethod; }
                set { SetPropertyValue("method", ref fmethod, value); }
            }
    
        
            private ir_model fobject_id;
            //FK FK_audittrail_log_object_id
            [Custom("Caption", "Object Id")]
            public ir_model object_id {
                get { return fobject_id; }
                set { SetPropertyValue<ir_model>("object_id", ref fobject_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public audittrail_log(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

