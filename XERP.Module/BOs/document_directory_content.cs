
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
	[DefaultProperty("suffix")]
    [Persistent("document_directory_content")]
	public partial class document_directory_content : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //document_directory_content_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_document_directory_content_create_uid
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
            //FK FK_document_directory_content_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean finclude_name;
            [Custom("Caption", "Include Name")]
            public System.Boolean include_name {
                get { return finclude_name; }
                set { SetPropertyValue("include_name", ref finclude_name, value); }
            }
    
            private System.String fsuffix;
            [Size(16)]
            [Custom("Caption", "Suffix")]
            public System.String suffix {
                get { return fsuffix; }
                set { SetPropertyValue("suffix", ref fsuffix, value); }
            }
    
            private System.String fextension;
            [Size(4)]
            [Custom("Caption", "Extension")]
            public System.String extension {
                get { return fextension; }
                set { SetPropertyValue("extension", ref fextension, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private ir_act_report_xml freport_id;
            //FK FK_document_directory_content_report_id
            [Custom("Caption", "Report Id")]
            public ir_act_report_xml report_id {
                get { return freport_id; }
                set { SetPropertyValue<ir_act_report_xml>("report_id", ref freport_id, value); }
            }
    
        
            private document_directory fdirectory_id;
            //FK FK_document_directory_content_directory_id
            [Custom("Caption", "Directory Id")]
            public document_directory directory_id {
                get { return fdirectory_id; }
                set { SetPropertyValue<document_directory>("directory_id", ref fdirectory_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private ir_model fics_object_id;
            //FK FK_document_directory_content_ics_object_id
            [Custom("Caption", "Ics Object id")]
            public ir_model ics_object_id {
                get { return fics_object_id; }
                set { SetPropertyValue<ir_model>("ics_object_id", ref fics_object_id, value); }
            }
    
            private System.String fics_domain;
            [Size(64)]
            [Custom("Caption", "Ics Domain")]
            public System.String ics_domain {
                get { return fics_domain; }
                set { SetPropertyValue("ics_domain", ref fics_domain, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public document_directory_content(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

