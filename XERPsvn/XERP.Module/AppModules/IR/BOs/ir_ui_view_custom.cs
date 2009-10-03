
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
	[DefaultProperty("arch")]
    [Persistent("ir_ui_view_custom")]
	public partial class ir_ui_view_custom : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_ui_view_custom_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_ui_view_custom_create_uid
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
            //FK FK_ir_ui_view_custom_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_ir_ui_view_custom_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String farch;
            [Size(-1)]
            [Custom("Caption", "Arch")]
            public System.String arch {
                get { return farch; }
                set { SetPropertyValue("arch", ref farch, value); }
            }
    
        
            private ir_ui_view fref_id;
            //FK FK_ir_ui_view_custom_ref_id
            [Custom("Caption", "Ref Id")]
            public ir_ui_view ref_id {
                get { return fref_id; }
                set { SetPropertyValue<ir_ui_view>("ref_id", ref fref_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_ui_view_custom(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

