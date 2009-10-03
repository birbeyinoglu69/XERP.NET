
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
	[DefaultProperty("print_format")]
    [Persistent("ir_report_custom")]
	public partial class ir_report_custom : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_report_custom_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_report_custom_create_uid
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
            //FK FK_ir_report_custom_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_ui_menu fmenu_id;
            //FK FK_ir_report_custom_menu_id
            [Custom("Caption", "Menu Id")]
            public ir_ui_menu menu_id {
                get { return fmenu_id; }
                set { SetPropertyValue<ir_ui_menu>("menu_id", ref fmenu_id, value); }
            }
    
        
            private ir_model fmodel_id;
            //FK FK_ir_report_custom_model_id
            [Custom("Caption", "Model Id")]
            public ir_model model_id {
                get { return fmodel_id; }
                set { SetPropertyValue<ir_model>("model_id", ref fmodel_id, value); }
            }
    
            private System.String fprint_format;
            [Size(16)]
            [Custom("Caption", "Print Format")]
            public System.String print_format {
                get { return fprint_format; }
                set { SetPropertyValue("print_format", ref fprint_format, value); }
            }
    
            private System.String flimitt;
            [Size(9)]
            [Custom("Caption", "Limitt")]
            public System.String limitt {
                get { return flimitt; }
                set { SetPropertyValue("limitt", ref flimitt, value); }
            }
    
            private System.Boolean frepeat_header;
            [Custom("Caption", "Repeat Header")]
            public System.Boolean repeat_header {
                get { return frepeat_header; }
                set { SetPropertyValue("repeat_header", ref frepeat_header, value); }
            }
    
            private System.String ftitle;
            [Size(64)]
            [Custom("Caption", "Title")]
            public System.String title {
                get { return ftitle; }
                set { SetPropertyValue("title", ref ftitle, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fstate1;
            [Size(64)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String ffrequency;
            [Size(64)]
            [Custom("Caption", "Frequency")]
            public System.String frequency {
                get { return ffrequency; }
                set { SetPropertyValue("frequency", ref ffrequency, value); }
            }
    
            private System.String fsortby;
            [Size(64)]
            [Custom("Caption", "Sortby")]
            public System.String sortby {
                get { return fsortby; }
                set { SetPropertyValue("sortby", ref fsortby, value); }
            }
    
            private System.String fprint_orientation;
            [Size(16)]
            [Custom("Caption", "Print Orientation")]
            public System.String print_orientation {
                get { return fprint_orientation; }
                set { SetPropertyValue("print_orientation", ref fprint_orientation, value); }
            }
    
            private System.String ffooter;
            [Size(64)]
            [Custom("Caption", "Footer")]
            public System.String footer {
                get { return ffooter; }
                set { SetPropertyValue("footer", ref ffooter, value); }
            }
    
            private System.String ftype;
            [Size(64)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
        
            private ir_model_fields ffield_parent;
            //FK FK_ir_report_custom_field_parent
            [Custom("Caption", "Field Parent")]
            public ir_model_fields field_parent {
                get { return ffield_parent; }
                set { SetPropertyValue<ir_model_fields>("field_parent", ref ffield_parent, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_report_custom(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

