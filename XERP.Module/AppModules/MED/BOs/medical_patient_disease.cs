
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
	[DefaultProperty("status")]
    [Persistent("medical_patient_disease")]
	public partial class medical_patient_disease : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_patient_disease_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_patient_disease_create_uid
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
            //FK FK_medical_patient_disease_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fstatus;
            [Size(16)]
            [Custom("Caption", "Status")]
            public System.String status {
                get { return fstatus; }
                set { SetPropertyValue("status", ref fstatus, value); }
            }
    
        
            private medical_pathology fname;
            //FK FK_medical_patient_disease_name
            [Custom("Caption", "Name")]
            public medical_pathology name {
                get { return fname; }
                set { SetPropertyValue<medical_pathology>("name", ref fname, value); }
            }
    
        
            private medical_physician fdoctor;
            //FK FK_medical_patient_disease_doctor
            [Custom("Caption", "Doctor")]
            public medical_physician doctor {
                get { return fdoctor; }
                set { SetPropertyValue<medical_physician>("doctor", ref fdoctor, value); }
            }
    
            private System.Boolean fpregnancy_warning;
            [Custom("Caption", "Pregnancy Warning")]
            public System.Boolean pregnancy_warning {
                get { return fpregnancy_warning; }
                set { SetPropertyValue("pregnancy_warning", ref fpregnancy_warning, value); }
            }
    
            private System.String fage;
            [Size(3)]
            [Custom("Caption", "Age")]
            public System.String age {
                get { return fage; }
                set { SetPropertyValue("age", ref fage, value); }
            }
    
            private System.Int32 fweeks_of_pregnancy;
            [Custom("Caption", "Weeks Of pregnancy")]
            public System.Int32 weeks_of_pregnancy {
                get { return fweeks_of_pregnancy; }
                set { SetPropertyValue("weeks_of_pregnancy", ref fweeks_of_pregnancy, value); }
            }
    
            private System.Boolean fis_on_treatment;
            [Custom("Caption", "Is On treatment")]
            public System.Boolean is_on_treatment {
                get { return fis_on_treatment; }
                set { SetPropertyValue("is_on_treatment", ref fis_on_treatment, value); }
            }
    
            private System.String ftreatment_description;
            [Size(128)]
            [Custom("Caption", "Treatment Description")]
            public System.String treatment_description {
                get { return ftreatment_description; }
                set { SetPropertyValue("treatment_description", ref ftreatment_description, value); }
            }
    
        
            private medical_procedure fpcs_code;
            //FK FK_medical_patient_disease_pcs_code
            [Custom("Caption", "Pcs Code")]
            public medical_procedure pcs_code {
                get { return fpcs_code; }
                set { SetPropertyValue<medical_procedure>("pcs_code", ref fpcs_code, value); }
            }
    
            private System.String fdescription;
            [Size(128)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private System.String fextra_info;
            [Size(-1)]
            [Custom("Caption", "Extra Info")]
            public System.String extra_info {
                get { return fextra_info; }
                set { SetPropertyValue("extra_info", ref fextra_info, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_patient_disease(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

