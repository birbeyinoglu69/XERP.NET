
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
	[DefaultProperty("expression")]
    [Persistent("base_report_creator_report_filter")]
	public partial class base_report_creator_report_filter : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //base_report_creator_report_filter_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_base_report_creator_report_filter_create_uid
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
            //FK FK_base_report_creator_report_filter_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fexpression;
            [Size(-1)]
            [Custom("Caption", "Expression")]
            public System.String expression {
                get { return fexpression; }
                set { SetPropertyValue("expression", ref fexpression, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fcondition;
            [Size(16)]
            [Custom("Caption", "Condition")]
            public System.String condition {
                get { return fcondition; }
                set { SetPropertyValue("condition", ref fcondition, value); }
            }
    
        
            private base_report_creator_report freport_id;
            //FK FK_base_report_creator_report_filter_report_id
            [Custom("Caption", "Report Id")]
            public base_report_creator_report report_id {
                get { return freport_id; }
                set { SetPropertyValue<base_report_creator_report>("report_id", ref freport_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public base_report_creator_report_filter(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

