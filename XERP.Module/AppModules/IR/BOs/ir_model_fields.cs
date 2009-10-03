
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
	[DefaultProperty("model")]
    [Persistent("ir_model_fields")]
	public partial class ir_model_fields : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_model_fields_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fmodel;
            [Size(64)]
            [Custom("Caption", "Model")]
            public System.String model {
                get { return fmodel; }
                set { SetPropertyValue("model", ref fmodel, value); }
            }
    
        
            private ir_model fmodel_id;
            //FK FK_ir_model_fields_model_id
            [Custom("Caption", "Model Id")]
            public ir_model model_id {
                get { return fmodel_id; }
                set { SetPropertyValue<ir_model>("model_id", ref fmodel_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String frelation;
            [Size(64)]
            [Custom("Caption", "Relation")]
            public System.String relation {
                get { return frelation; }
                set { SetPropertyValue("relation", ref frelation, value); }
            }
    
            private System.String fselect_level;
            [Size(4)]
            [Custom("Caption", "Select Level")]
            public System.String select_level {
                get { return fselect_level; }
                set { SetPropertyValue("select_level", ref fselect_level, value); }
            }
    
            private System.String ffield_description;
            [Size(256)]
            [Custom("Caption", "Field Description")]
            public System.String field_description {
                get { return ffield_description; }
                set { SetPropertyValue("field_description", ref ffield_description, value); }
            }
    
            private System.String fttype;
            [Size(64)]
            [Custom("Caption", "Ttype")]
            public System.String ttype {
                get { return fttype; }
                set { SetPropertyValue("ttype", ref fttype, value); }
            }
    
            private System.String fstate1;
            [Size(64)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.Boolean fview_load;
            [Custom("Caption", "View Load")]
            public System.Boolean view_load {
                get { return fview_load; }
                set { SetPropertyValue("view_load", ref fview_load, value); }
            }
    
            private System.Boolean frelate;
            [Custom("Caption", "Relate")]
            public System.Boolean relate {
                get { return frelate; }
                set { SetPropertyValue("relate", ref frelate, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_ir_model_fields_create_uid
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
            //FK FK_ir_model_fields_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fdomain;
            [Size(256)]
            [Custom("Caption", "Domain")]
            public System.String domain {
                get { return fdomain; }
                set { SetPropertyValue("domain", ref fdomain, value); }
            }
    
            private System.String fselection;
            [Size(128)]
            [Custom("Caption", "Selection")]
            public System.String selection {
                get { return fselection; }
                set { SetPropertyValue("selection", ref fselection, value); }
            }
    
            private System.String fon_delete;
            [Size(16)]
            [Custom("Caption", "On Delete")]
            public System.String on_delete {
                get { return fon_delete; }
                set { SetPropertyValue("on_delete", ref fon_delete, value); }
            }
    
            private System.Int32 fsize;
            [Custom("Caption", "Size")]
            public System.Int32 size {
                get { return fsize; }
                set { SetPropertyValue("size", ref fsize, value); }
            }
    
            private System.Boolean frequired;
            [Custom("Caption", "Required")]
            public System.Boolean required {
                get { return frequired; }
                set { SetPropertyValue("required", ref frequired, value); }
            }
    
            private System.Boolean freadonly1;
            [Custom("Caption", "Readonly")]
            public System.Boolean readonly1 {
                get { return freadonly1; }
                set { SetPropertyValue("readonly1", ref freadonly1, value); }
            }
    
            private System.String frelation_field;
            [Size(64)]
            [Custom("Caption", "Relation Field")]
            public System.String relation_field {
                get { return frelation_field; }
                set { SetPropertyValue("relation_field", ref frelation_field, value); }
            }
    
            private System.Boolean ftranslate;
            [Custom("Caption", "Translate")]
            public System.Boolean translate {
                get { return ftranslate; }
                set { SetPropertyValue("translate", ref ftranslate, value); }
            }
    
            private System.String fcomplete_name;
            [Size(64)]
            [Custom("Caption", "Complete Name")]
            public System.String complete_name {
                get { return fcomplete_name; }
                set { SetPropertyValue("complete_name", ref fcomplete_name, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_model_fields(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

