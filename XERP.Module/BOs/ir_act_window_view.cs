
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
	[DefaultProperty("view_mode")]
    [Persistent("ir_act_window_view")]
	public partial class ir_act_window_view : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_act_window_view_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_act_window_view_create_uid
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
            //FK FK_ir_act_window_view_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_act_window fact_window_id;
            //FK FK_ir_act_window_view_act_window_id
            [Custom("Caption", "Act Window id")]
            public ir_act_window act_window_id {
                get { return fact_window_id; }
                set { SetPropertyValue<ir_act_window>("act_window_id", ref fact_window_id, value); }
            }
    
            private System.Boolean fmulti;
            [Custom("Caption", "Multi")]
            public System.Boolean multi {
                get { return fmulti; }
                set { SetPropertyValue("multi", ref fmulti, value); }
            }
    
            private System.String fview_mode;
            [Size(16)]
            [Custom("Caption", "View Mode")]
            public System.String view_mode {
                get { return fview_mode; }
                set { SetPropertyValue("view_mode", ref fview_mode, value); }
            }
    
        
            private ir_ui_view fview_id;
            //FK FK_ir_act_window_view_view_id
            [Custom("Caption", "View Id")]
            public ir_ui_view view_id {
                get { return fview_id; }
                set { SetPropertyValue<ir_ui_view>("view_id", ref fview_id, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_act_window_view(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

