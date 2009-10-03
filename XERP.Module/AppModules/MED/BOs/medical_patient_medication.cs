
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
	[DefaultProperty("adverse_reaction")]
    [Persistent("medical_patient_medication")]
	public partial class medical_patient_medication : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_patient_medication_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_patient_medication_create_uid
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
            //FK FK_medical_patient_medication_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private DateTime? fstart_treatment;
            [Custom("Caption", "Start Treatment")]
            public DateTime? start_treatment {
                get { return fstart_treatment; }
                set { SetPropertyValue("start_treatment", ref fstart_treatment, value); }
            }
    
            private System.Boolean fallow_substitution;
            [Custom("Caption", "Allow Substitution")]
            public System.Boolean allow_substitution {
                get { return fallow_substitution; }
                set { SetPropertyValue("allow_substitution", ref fallow_substitution, value); }
            }
    
            private System.String fadverse_reaction;
            [Size(-1)]
            [Custom("Caption", "Adverse Reaction")]
            public System.String adverse_reaction {
                get { return fadverse_reaction; }
                set { SetPropertyValue("adverse_reaction", ref fadverse_reaction, value); }
            }
    
            private DateTime? fend_treatment;
            [Custom("Caption", "End Treatment")]
            public DateTime? end_treatment {
                get { return fend_treatment; }
                set { SetPropertyValue("end_treatment", ref fend_treatment, value); }
            }
    
            private System.Boolean fcourse_completed;
            [Custom("Caption", "Course Completed")]
            public System.Boolean course_completed {
                get { return fcourse_completed; }
                set { SetPropertyValue("course_completed", ref fcourse_completed, value); }
            }
    
            private System.Int32 ffrequency;
            [Custom("Caption", "Frequency")]
            public System.Int32 frequency {
                get { return ffrequency; }
                set { SetPropertyValue("frequency", ref ffrequency, value); }
            }
    
            private DateTime? freview;
            [Custom("Caption", "Review")]
            public DateTime? review {
                get { return freview; }
                set { SetPropertyValue("review", ref freview, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Int32 fdose;
            [Custom("Caption", "Dose")]
            public System.Int32 dose {
                get { return fdose; }
                set { SetPropertyValue("dose", ref fdose, value); }
            }
    
        
            private medical_dose_unit fdose_unit;
            //FK FK_medical_patient_medication_dose_unit
            [Custom("Caption", "Dose Unit")]
            public medical_dose_unit dose_unit {
                get { return fdose_unit; }
                set { SetPropertyValue<medical_dose_unit>("dose_unit", ref fdose_unit, value); }
            }
    
            private System.Int32 frefills;
            [Custom("Caption", "Refills")]
            public System.Int32 refills {
                get { return frefills; }
                set { SetPropertyValue("refills", ref frefills, value); }
            }
    
        
            private medical_medicament fname;
            //FK FK_medical_patient_medication_name
            [Custom("Caption", "Name")]
            public medical_medicament name {
                get { return fname; }
                set { SetPropertyValue<medical_medicament>("name", ref fname, value); }
            }
    
        
            private medical_physician fdoctor;
            //FK FK_medical_patient_medication_doctor
            [Custom("Caption", "Doctor")]
            public medical_physician doctor {
                get { return fdoctor; }
                set { SetPropertyValue<medical_physician>("doctor", ref fdoctor, value); }
            }
    
            private System.String froute;
            [Size(16)]
            [Custom("Caption", "Route")]
            public System.String route {
                get { return froute; }
                set { SetPropertyValue("route", ref froute, value); }
            }
    
            private System.String fprescription_id;
            [Size(64)]
            [Custom("Caption", "Prescription Id")]
            public System.String prescription_id {
                get { return fprescription_id; }
                set { SetPropertyValue("prescription_id", ref fprescription_id, value); }
            }
    
            private System.String ffrequency_unit;
            [Size(16)]
            [Custom("Caption", "Frequency Unit")]
            public System.String frequency_unit {
                get { return ffrequency_unit; }
                set { SetPropertyValue("frequency_unit", ref ffrequency_unit, value); }
            }
    
        
            private medical_pathology findication;
            //FK FK_medical_patient_medication_indication
            [Custom("Caption", "Indication")]
            public medical_pathology indication {
                get { return findication; }
                set { SetPropertyValue<medical_pathology>("indication", ref findication, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.String fdiscontinued_reason;
            [Size(128)]
            [Custom("Caption", "Discontinued Reason")]
            public System.String discontinued_reason {
                get { return fdiscontinued_reason; }
                set { SetPropertyValue("discontinued_reason", ref fdiscontinued_reason, value); }
            }
    
            private System.Boolean fdiscontinued;
            [Custom("Caption", "Discontinued")]
            public System.Boolean discontinued {
                get { return fdiscontinued; }
                set { SetPropertyValue("discontinued", ref fdiscontinued, value); }
            }
    
            private System.Int32 fquantity;
            [Custom("Caption", "Quantity")]
            public System.Int32 quantity {
                get { return fquantity; }
                set { SetPropertyValue("quantity", ref fquantity, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_patient_medication(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

