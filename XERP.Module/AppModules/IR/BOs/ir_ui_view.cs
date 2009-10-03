
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
    [Persistent("ir_ui_view")]
	public partial class ir_ui_view : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_ui_view_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fmodel;
            [Size(64)]
            [Custom("Caption", "Model")]
            public System.String model {
                get { return fmodel; }
                set { SetPropertyValue("model", ref fmodel, value); }
            }
    
            private System.String ftype;
            [Size(64)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String farch;
            [Size(-1)]
            [Custom("Caption", "Arch")]
            public System.String arch {
                get { return farch; }
                set { SetPropertyValue("arch", ref farch, value); }
            }
    
            private System.String ffield_parent;
            [Size(64)]
            [Custom("Caption", "Field Parent")]
            public System.String field_parent {
                get { return ffield_parent; }
                set { SetPropertyValue("field_parent", ref ffield_parent, value); }
            }
    
            private System.Int32 fpriority;
            [Custom("Caption", "Priority")]
            public System.Int32 priority {
                get { return fpriority; }
                set { SetPropertyValue("priority", ref fpriority, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_ir_ui_view_create_uid
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
            //FK FK_ir_ui_view_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_ui_view finherit_id;
            //FK FK_ir_ui_view_inherit_id
            [Custom("Caption", "Inherit Id")]
            public ir_ui_view inherit_id {
                get { return finherit_id; }
                set { SetPropertyValue<ir_ui_view>("inherit_id", ref finherit_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_ui_view(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

