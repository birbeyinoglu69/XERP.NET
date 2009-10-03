
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
	[DefaultProperty("name")]
    [Persistent("audittrail_rule")]
	public partial class audittrail_rule : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //audittrail_rule_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_audittrail_rule_create_uid
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
            //FK FK_audittrail_rule_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean flog_read;
            [Custom("Caption", "Log Read")]
            public System.Boolean log_read {
                get { return flog_read; }
                set { SetPropertyValue("log_read", ref flog_read, value); }
            }
    
            private System.Boolean flog_unlink;
            [Custom("Caption", "Log Unlink")]
            public System.Boolean log_unlink {
                get { return flog_unlink; }
                set { SetPropertyValue("log_unlink", ref flog_unlink, value); }
            }
    
            private System.String fname;
            [Size(32)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean flog_write;
            [Custom("Caption", "Log Write")]
            public System.Boolean log_write {
                get { return flog_write; }
                set { SetPropertyValue("log_write", ref flog_write, value); }
            }
    
        
            private ir_model fobject_id;
            //FK FK_audittrail_rule_object_id
            [Custom("Caption", "Object Id")]
            public ir_model object_id {
                get { return fobject_id; }
                set { SetPropertyValue<ir_model>("object_id", ref fobject_id, value); }
            }
    
            private System.Boolean flog_create;
            [Custom("Caption", "Log Create")]
            public System.Boolean log_create {
                get { return flog_create; }
                set { SetPropertyValue("log_create", ref flog_create, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private ir_act_window faction_id;
            //FK FK_audittrail_rule_action_id
            [Custom("Caption", "Action Id")]
            public ir_act_window action_id {
                get { return faction_id; }
                set { SetPropertyValue<ir_act_window>("action_id", ref faction_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public audittrail_rule(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

