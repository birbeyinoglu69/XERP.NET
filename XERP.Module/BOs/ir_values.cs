
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
    [Persistent("ir_values")]
	public partial class ir_values : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_values_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fkey;
            [Size(128)]
            [Custom("Caption", "Key")]
            public System.String key {
                get { return fkey; }
                set { SetPropertyValue("key", ref fkey, value); }
            }
    
            private System.String fkey2;
            [Size(256)]
            [Custom("Caption", "Key2")]
            public System.String key2 {
                get { return fkey2; }
                set { SetPropertyValue("key2", ref fkey2, value); }
            }
    
            private System.String fmodel;
            [Size(128)]
            [Custom("Caption", "Model")]
            public System.String model {
                get { return fmodel; }
                set { SetPropertyValue("model", ref fmodel, value); }
            }
    
            private System.String fvalue;
            [Size(-1)]
            [Custom("Caption", "Value")]
            public System.String value {
                get { return fvalue; }
                set { SetPropertyValue("value", ref fvalue, value); }
            }
    
            private System.String fmeta;
            [Size(-1)]
            [Custom("Caption", "Meta")]
            public System.String meta {
                get { return fmeta; }
                set { SetPropertyValue("meta", ref fmeta, value); }
            }
    
            private System.Int32 fres_id;
            [Custom("Caption", "Res Id")]
            public System.Int32 res_id {
                get { return fres_id; }
                set { SetPropertyValue("res_id", ref fres_id, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_ir_values_create_uid
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
            //FK FK_ir_values_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_model fmodel_id;
            //FK FK_ir_values_model_id
            [Custom("Caption", "Model Id")]
            public ir_model model_id {
                get { return fmodel_id; }
                set { SetPropertyValue<ir_model>("model_id", ref fmodel_id, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_ir_values_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.Boolean fobject1;
            [Custom("Caption", "Object")]
            public System.Boolean object1 {
                get { return fobject1; }
                set { SetPropertyValue("object1", ref fobject1, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_ir_values_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
            private System.Int32 faction_id;
            [Custom("Caption", "Action Id")]
            public System.Int32 action_id {
                get { return faction_id; }
                set { SetPropertyValue("action_id", ref faction_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_values(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

