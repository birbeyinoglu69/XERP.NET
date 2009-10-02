
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
	[DefaultProperty("fc3_op")]
    [Persistent("ir_report_custom_fields")]
	public partial class ir_report_custom_fields : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_report_custom_fields_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_report_custom_fields_create_uid
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
            //FK FK_ir_report_custom_fields_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String ffc3_op;
            [Size(16)]
            [Custom("Caption", "Fc3 Op")]
            public System.String fc3_op {
                get { return ffc3_op; }
                set { SetPropertyValue("fc3_op", ref ffc3_op, value); }
            }
    
            private System.String ffc2_op;
            [Size(16)]
            [Custom("Caption", "Fc2 Op")]
            public System.String fc2_op {
                get { return ffc2_op; }
                set { SetPropertyValue("fc2_op", ref ffc2_op, value); }
            }
    
            private System.Boolean fgroupby;
            [Custom("Caption", "Groupby")]
            public System.Boolean groupby {
                get { return fgroupby; }
                set { SetPropertyValue("groupby", ref fgroupby, value); }
            }
    
        
            private ir_model_fields ffield_child1;
            //FK FK_ir_report_custom_fields_field_child1
            [Custom("Caption", "Field Child1")]
            public ir_model_fields field_child1 {
                get { return ffield_child1; }
                set { SetPropertyValue<ir_model_fields>("field_child1", ref ffield_child1, value); }
            }
    
        
            private ir_model_fields ffield_child0;
            //FK FK_ir_report_custom_fields_field_child0
            [Custom("Caption", "Field Child0")]
            public ir_model_fields field_child0 {
                get { return ffield_child0; }
                set { SetPropertyValue<ir_model_fields>("field_child0", ref ffield_child0, value); }
            }
    
        
            private ir_model_fields ffield_child3;
            //FK FK_ir_report_custom_fields_field_child3
            [Custom("Caption", "Field Child3")]
            public ir_model_fields field_child3 {
                get { return ffield_child3; }
                set { SetPropertyValue<ir_model_fields>("field_child3", ref ffield_child3, value); }
            }
    
        
            private ir_model_fields ffield_child2;
            //FK FK_ir_report_custom_fields_field_child2
            [Custom("Caption", "Field Child2")]
            public ir_model_fields field_child2 {
                get { return ffield_child2; }
                set { SetPropertyValue<ir_model_fields>("field_child2", ref ffield_child2, value); }
            }
    
            private System.String ffc1_condition;
            [Size(64)]
            [Custom("Caption", "Fc1 Condition")]
            public System.String fc1_condition {
                get { return ffc1_condition; }
                set { SetPropertyValue("fc1_condition", ref ffc1_condition, value); }
            }
    
            private System.String foperation;
            [Size(16)]
            [Custom("Caption", "Operation")]
            public System.String operation {
                get { return foperation; }
                set { SetPropertyValue("operation", ref foperation, value); }
            }
    
            private System.String ffc3_condition;
            [Size(64)]
            [Custom("Caption", "Fc3 Condition")]
            public System.String fc3_condition {
                get { return ffc3_condition; }
                set { SetPropertyValue("fc3_condition", ref ffc3_condition, value); }
            }
    
            private System.String ffc1_op;
            [Size(16)]
            [Custom("Caption", "Fc1 Op")]
            public System.String fc1_op {
                get { return ffc1_op; }
                set { SetPropertyValue("fc1_op", ref ffc1_op, value); }
            }
    
            private System.String falignment;
            [Size(16)]
            [Custom("Caption", "Alignment")]
            public System.String alignment {
                get { return falignment; }
                set { SetPropertyValue("alignment", ref falignment, value); }
            }
    
        
            private ir_report_custom freport_id;
            //FK FK_ir_report_custom_fields_report_id
            [Custom("Caption", "Report Id")]
            public ir_report_custom report_id {
                get { return freport_id; }
                set { SetPropertyValue<ir_report_custom>("report_id", ref freport_id, value); }
            }
    
        
            private ir_model_fields ffc2_operande;
            //FK FK_ir_report_custom_fields_fc2_operande
            [Custom("Caption", "Fc2 Operande")]
            public ir_model_fields fc2_operande {
                get { return ffc2_operande; }
                set { SetPropertyValue<ir_model_fields>("fc2_operande", ref ffc2_operande, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ffc2_condition;
            [Size(64)]
            [Custom("Caption", "Fc2 Condition")]
            public System.String fc2_condition {
                get { return ffc2_condition; }
                set { SetPropertyValue("fc2_condition", ref ffc2_condition, value); }
            }
    
            private System.String ffc0_op;
            [Size(16)]
            [Custom("Caption", "Fc0 Op")]
            public System.String fc0_op {
                get { return ffc0_op; }
                set { SetPropertyValue("fc0_op", ref ffc0_op, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.String fbgcolor;
            [Size(64)]
            [Custom("Caption", "Bgcolor")]
            public System.String bgcolor {
                get { return fbgcolor; }
                set { SetPropertyValue("bgcolor", ref fbgcolor, value); }
            }
    
        
            private ir_model_fields ffc3_operande;
            //FK FK_ir_report_custom_fields_fc3_operande
            [Custom("Caption", "Fc3 Operande")]
            public ir_model_fields fc3_operande {
                get { return ffc3_operande; }
                set { SetPropertyValue<ir_model_fields>("fc3_operande", ref ffc3_operande, value); }
            }
    
            private System.String ffc0_condition;
            [Size(64)]
            [Custom("Caption", "Fc0 Condition")]
            public System.String fc0_condition {
                get { return ffc0_condition; }
                set { SetPropertyValue("fc0_condition", ref ffc0_condition, value); }
            }
    
            private System.Int32 fwidth;
            [Custom("Caption", "Width")]
            public System.Int32 width {
                get { return fwidth; }
                set { SetPropertyValue("width", ref fwidth, value); }
            }
    
            private System.Boolean fcumulate;
            [Custom("Caption", "Cumulate")]
            public System.Boolean cumulate {
                get { return fcumulate; }
                set { SetPropertyValue("cumulate", ref fcumulate, value); }
            }
    
            private System.String ffontcolor;
            [Size(64)]
            [Custom("Caption", "Fontcolor")]
            public System.String fontcolor {
                get { return ffontcolor; }
                set { SetPropertyValue("fontcolor", ref ffontcolor, value); }
            }
    
        
            private ir_model_fields ffc0_operande;
            //FK FK_ir_report_custom_fields_fc0_operande
            [Custom("Caption", "Fc0 Operande")]
            public ir_model_fields fc0_operande {
                get { return ffc0_operande; }
                set { SetPropertyValue<ir_model_fields>("fc0_operande", ref ffc0_operande, value); }
            }
    
        
            private ir_model_fields ffc1_operande;
            //FK FK_ir_report_custom_fields_fc1_operande
            [Custom("Caption", "Fc1 Operande")]
            public ir_model_fields fc1_operande {
                get { return ffc1_operande; }
                set { SetPropertyValue<ir_model_fields>("fc1_operande", ref ffc1_operande, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_report_custom_fields(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

