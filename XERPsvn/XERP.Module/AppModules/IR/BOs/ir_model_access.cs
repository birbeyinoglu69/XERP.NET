
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
    [Persistent("ir_model_access")]
	public partial class ir_model_access : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_model_access_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_model_access_create_uid
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
            //FK FK_ir_model_access_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_model fmodel_id;
            //FK FK_ir_model_access_model_id
            [Custom("Caption", "Model Id")]
            public ir_model model_id {
                get { return fmodel_id; }
                set { SetPropertyValue<ir_model>("model_id", ref fmodel_id, value); }
            }
    
            private System.Boolean fperm_read;
            [Custom("Caption", "Perm Read")]
            public System.Boolean perm_read {
                get { return fperm_read; }
                set { SetPropertyValue("perm_read", ref fperm_read, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean fperm_unlink;
            [Custom("Caption", "Perm Unlink")]
            public System.Boolean perm_unlink {
                get { return fperm_unlink; }
                set { SetPropertyValue("perm_unlink", ref fperm_unlink, value); }
            }
    
            private System.Boolean fperm_write;
            [Custom("Caption", "Perm Write")]
            public System.Boolean perm_write {
                get { return fperm_write; }
                set { SetPropertyValue("perm_write", ref fperm_write, value); }
            }
    
            private System.Boolean fperm_create;
            [Custom("Caption", "Perm Create")]
            public System.Boolean perm_create {
                get { return fperm_create; }
                set { SetPropertyValue("perm_create", ref fperm_create, value); }
            }
    
        
            private res_groups fgroup_id;
            //FK FK_ir_model_access_group_id
            [Custom("Caption", "Group Id")]
            public res_groups group_id {
                get { return fgroup_id; }
                set { SetPropertyValue<res_groups>("group_id", ref fgroup_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_model_access(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

