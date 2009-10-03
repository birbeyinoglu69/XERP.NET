
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
	[DefaultProperty("operator1")]
    [Persistent("ir_rule")]
	public partial class ir_rule : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_rule_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_rule_create_uid
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
            //FK FK_ir_rule_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String foperator1;
            [Size(16)]
            [Custom("Caption", "Operator")]
            public System.String operator1 {
                get { return foperator1; }
                set { SetPropertyValue("operator1", ref foperator1, value); }
            }
    
            private System.String fdomain_force;
            [Size(250)]
            [Custom("Caption", "Domain Force")]
            public System.String domain_force {
                get { return fdomain_force; }
                set { SetPropertyValue("domain_force", ref fdomain_force, value); }
            }
    
            private System.String foperand;
            [Size(64)]
            [Custom("Caption", "Operand")]
            public System.String operand {
                get { return foperand; }
                set { SetPropertyValue("operand", ref foperand, value); }
            }
    
        
            private ir_rule_group frule_group;
            //FK FK_ir_rule_rule_group
            [Custom("Caption", "Rule Group")]
            public ir_rule_group rule_group {
                get { return frule_group; }
                set { SetPropertyValue<ir_rule_group>("rule_group", ref frule_group, value); }
            }
    
        
            private ir_model_fields ffield_id;
            //FK FK_ir_rule_field_id
            [Custom("Caption", "Field Id")]
            public ir_model_fields field_id {
                get { return ffield_id; }
                set { SetPropertyValue<ir_model_fields>("field_id", ref ffield_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_rule(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

