
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
	[DefaultProperty("expr_name")]
    [Persistent("crm_segmentation_line")]
	public partial class crm_segmentation_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //crm_segmentation_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_crm_segmentation_line_create_uid
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
            //FK FK_crm_segmentation_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fexpr_name;
            [Size(64)]
            [Custom("Caption", "Expr Name")]
            public System.String expr_name {
                get { return fexpr_name; }
                set { SetPropertyValue("expr_name", ref fexpr_name, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Double fexpr_value;
            [Custom("Caption", "Expr Value")]
            public System.Double expr_value {
                get { return fexpr_value; }
                set { SetPropertyValue("expr_value", ref fexpr_value, value); }
            }
    
            private System.String foperator1;
            [Size(16)]
            [Custom("Caption", "Operator")]
            public System.String operator1 {
                get { return foperator1; }
                set { SetPropertyValue("operator1", ref foperator1, value); }
            }
    
        
            private crm_segmentation fsegmentation_id;
            //FK FK_crm_segmentation_line_segmentation_id
            [Custom("Caption", "Segmentation Id")]
            public crm_segmentation segmentation_id {
                get { return fsegmentation_id; }
                set { SetPropertyValue<crm_segmentation>("segmentation_id", ref fsegmentation_id, value); }
            }
    
            private System.String fexpr_operator;
            [Size(16)]
            [Custom("Caption", "Expr Operator")]
            public System.String expr_operator {
                get { return fexpr_operator; }
                set { SetPropertyValue("expr_operator", ref fexpr_operator, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public crm_segmentation_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

