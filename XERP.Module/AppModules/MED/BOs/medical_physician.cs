
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
	[DefaultProperty("info")]
    [Persistent("medical_physician")]
	public partial class medical_physician : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_physician_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_physician_create_uid
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
            //FK FK_medical_physician_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String finfo;
            [Size(-1)]
            [Custom("Caption", "Info")]
            public System.String info {
                get { return finfo; }
                set { SetPropertyValue("info", ref finfo, value); }
            }
    
            private System.String fcode;
            [Size(128)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
        
            private res_partner fname;
            //FK FK_medical_physician_name
            [Custom("Caption", "Name")]
            public res_partner name {
                get { return fname; }
                set { SetPropertyValue<res_partner>("name", ref fname, value); }
            }
    
        
            private res_partner finstitution;
            //FK FK_medical_physician_institution
            [Custom("Caption", "Institution")]
            public res_partner institution {
                get { return finstitution; }
                set { SetPropertyValue<res_partner>("institution", ref finstitution, value); }
            }
    
        
            private medical_specialty fspecialty;
            //FK FK_medical_physician_specialty
            [Custom("Caption", "Specialty")]
            public medical_specialty specialty {
                get { return fspecialty; }
                set { SetPropertyValue<medical_specialty>("specialty", ref fspecialty, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_physician(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

