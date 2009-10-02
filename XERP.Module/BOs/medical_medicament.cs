
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
	[DefaultProperty("indications")]
    [Persistent("medical_medicament")]
	public partial class medical_medicament : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_medicament_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_medicament_create_uid
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
            //FK FK_medical_medicament_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String findications;
            [Size(-1)]
            [Custom("Caption", "Indications")]
            public System.String indications {
                get { return findications; }
                set { SetPropertyValue("indications", ref findications, value); }
            }
    
        
            private product_product fname;
            //FK FK_medical_medicament_name
            [Custom("Caption", "Name")]
            public product_product name {
                get { return fname; }
                set { SetPropertyValue<product_product>("name", ref fname, value); }
            }
    
            private System.String foverdosage;
            [Size(-1)]
            [Custom("Caption", "Overdosage")]
            public System.String overdosage {
                get { return foverdosage; }
                set { SetPropertyValue("overdosage", ref foverdosage, value); }
            }
    
            private System.Boolean fpregnancy_warning;
            [Custom("Caption", "Pregnancy Warning")]
            public System.Boolean pregnancy_warning {
                get { return fpregnancy_warning; }
                set { SetPropertyValue("pregnancy_warning", ref fpregnancy_warning, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.String ftherapeutic_action;
            [Size(128)]
            [Custom("Caption", "Therapeutic Action")]
            public System.String therapeutic_action {
                get { return ftherapeutic_action; }
                set { SetPropertyValue("therapeutic_action", ref ftherapeutic_action, value); }
            }
    
            private System.String fstorage;
            [Size(-1)]
            [Custom("Caption", "Storage")]
            public System.String storage {
                get { return fstorage; }
                set { SetPropertyValue("storage", ref fstorage, value); }
            }
    
            private System.String fadverse_reaction;
            [Size(-1)]
            [Custom("Caption", "Adverse Reaction")]
            public System.String adverse_reaction {
                get { return fadverse_reaction; }
                set { SetPropertyValue("adverse_reaction", ref fadverse_reaction, value); }
            }
    
            private System.String fdosage;
            [Size(-1)]
            [Custom("Caption", "Dosage")]
            public System.String dosage {
                get { return fdosage; }
                set { SetPropertyValue("dosage", ref fdosage, value); }
            }
    
            private System.String factive_component;
            [Size(128)]
            [Custom("Caption", "Active Component")]
            public System.String active_component {
                get { return factive_component; }
                set { SetPropertyValue("active_component", ref factive_component, value); }
            }
    
            private System.String fpregnancy;
            [Size(-1)]
            [Custom("Caption", "Pregnancy")]
            public System.String pregnancy {
                get { return fpregnancy; }
                set { SetPropertyValue("pregnancy", ref fpregnancy, value); }
            }
    
            private System.String fpresentation;
            [Size(-1)]
            [Custom("Caption", "Presentation")]
            public System.String presentation {
                get { return fpresentation; }
                set { SetPropertyValue("presentation", ref fpresentation, value); }
            }
    
            private System.String fcomposition;
            [Size(-1)]
            [Custom("Caption", "Composition")]
            public System.String composition {
                get { return fcomposition; }
                set { SetPropertyValue("composition", ref fcomposition, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_medicament(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

