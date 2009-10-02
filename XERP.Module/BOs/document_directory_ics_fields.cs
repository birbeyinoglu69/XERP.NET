
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
    [Persistent("document_directory_ics_fields")]
	public partial class document_directory_ics_fields : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //document_directory_ics_fields_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_document_directory_ics_fields_create_uid
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
            //FK FK_document_directory_ics_fields_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private document_directory_content fcontent_id;
            //FK FK_document_directory_ics_fields_content_id
            [Custom("Caption", "Content Id")]
            public document_directory_content content_id {
                get { return fcontent_id; }
                set { SetPropertyValue<document_directory_content>("content_id", ref fcontent_id, value); }
            }
    
        
            private ir_model_fields ffield_id;
            //FK FK_document_directory_ics_fields_field_id
            [Custom("Caption", "Field Id")]
            public ir_model_fields field_id {
                get { return ffield_id; }
                set { SetPropertyValue<ir_model_fields>("field_id", ref ffield_id, value); }
            }
    
            private System.String fname;
            [Size(16)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public document_directory_ics_fields(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

