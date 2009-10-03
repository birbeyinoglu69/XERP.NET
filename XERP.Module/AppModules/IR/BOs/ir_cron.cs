
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
	[DefaultProperty("function")]
    [Persistent("ir_cron")]
	public partial class ir_cron : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_cron_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_cron_create_uid
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
            //FK FK_ir_cron_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String ffunction;
            [Size(64)]
            [Custom("Caption", "Function")]
            public System.String function {
                get { return ffunction; }
                set { SetPropertyValue("function", ref ffunction, value); }
            }
    
            private System.String finterval_type;
            [Size(16)]
            [Custom("Caption", "Interval Type")]
            public System.String interval_type {
                get { return finterval_type; }
                set { SetPropertyValue("interval_type", ref finterval_type, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_ir_cron_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(60)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fargs;
            [Size(-1)]
            [Custom("Caption", "Args")]
            public System.String args {
                get { return fargs; }
                set { SetPropertyValue("args", ref fargs, value); }
            }
    
            private System.Int32 fnumbercall;
            [Custom("Caption", "Numbercall")]
            public System.Int32 numbercall {
                get { return fnumbercall; }
                set { SetPropertyValue("numbercall", ref fnumbercall, value); }
            }
    
            private DateTime? fnextcall;
            [Custom("Caption", "Nextcall")]
            public DateTime? nextcall {
                get { return fnextcall; }
                set { SetPropertyValue("nextcall", ref fnextcall, value); }
            }
    
            private System.Int32 fpriority;
            [Custom("Caption", "Priority")]
            public System.Int32 priority {
                get { return fpriority; }
                set { SetPropertyValue("priority", ref fpriority, value); }
            }
    
            private System.Boolean fdoall;
            [Custom("Caption", "Doall")]
            public System.Boolean doall {
                get { return fdoall; }
                set { SetPropertyValue("doall", ref fdoall, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Int32 finterval_number;
            [Custom("Caption", "Interval Number")]
            public System.Int32 interval_number {
                get { return finterval_number; }
                set { SetPropertyValue("interval_number", ref finterval_number, value); }
            }
    
            private System.String fmodel;
            [Size(64)]
            [Custom("Caption", "Model")]
            public System.String model {
                get { return fmodel; }
                set { SetPropertyValue("model", ref fmodel, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_cron(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

