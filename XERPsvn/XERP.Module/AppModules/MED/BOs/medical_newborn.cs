
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
	[DefaultProperty("code")]
    [Persistent("medical_newborn")]
	public partial class medical_newborn : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_newborn_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_newborn_create_uid
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
            //FK FK_medical_newborn_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fcode;
            [Size(64)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.Int32 fweight;
            [Custom("Caption", "Weight")]
            public System.Int32 weight {
                get { return fweight; }
                set { SetPropertyValue("weight", ref fweight, value); }
            }
    
            private System.Byte[] fphoto;
            [Custom("Caption", "Photo")]
            public System.Byte[] photo {
                get { return fphoto; }
                set { SetPropertyValue("photo", ref fphoto, value); }
            }
    
            private System.String fsex;
            [Size(16)]
            [Custom("Caption", "Sex")]
            public System.String sex {
                get { return fsex; }
                set { SetPropertyValue("sex", ref fsex, value); }
            }
    
            private System.Boolean freanimation_aspiration;
            [Custom("Caption", "Reanimation Aspiration")]
            public System.Boolean reanimation_aspiration {
                get { return freanimation_aspiration; }
                set { SetPropertyValue("reanimation_aspiration", ref freanimation_aspiration, value); }
            }
    
            private System.Int32 fcephalic_perimeter;
            [Custom("Caption", "Cephalic Perimeter")]
            public System.Int32 cephalic_perimeter {
                get { return fcephalic_perimeter; }
                set { SetPropertyValue("cephalic_perimeter", ref fcephalic_perimeter, value); }
            }
    
            private System.Boolean ftest_toxo;
            [Custom("Caption", "Test Toxo")]
            public System.Boolean test_toxo {
                get { return ftest_toxo; }
                set { SetPropertyValue("test_toxo", ref ftest_toxo, value); }
            }
    
            private System.Boolean freanimation_mask;
            [Custom("Caption", "Reanimation Mask")]
            public System.Boolean reanimation_mask {
                get { return freanimation_mask; }
                set { SetPropertyValue("reanimation_mask", ref freanimation_mask, value); }
            }
    
        
            private medical_physician fresponsible;
            //FK FK_medical_newborn_responsible
            [Custom("Caption", "Responsible")]
            public medical_physician responsible {
                get { return fresponsible; }
                set { SetPropertyValue<medical_physician>("responsible", ref fresponsible, value); }
            }
    
            private DateTime? fdismissed;
            [Custom("Caption", "Dismissed")]
            public DateTime? dismissed {
                get { return fdismissed; }
                set { SetPropertyValue("dismissed", ref fdismissed, value); }
            }
    
            private System.Boolean freanimation_stimulation;
            [Custom("Caption", "Reanimation Stimulation")]
            public System.Boolean reanimation_stimulation {
                get { return freanimation_stimulation; }
                set { SetPropertyValue("reanimation_stimulation", ref freanimation_stimulation, value); }
            }
    
            private System.Boolean freanimation_intubation;
            [Custom("Caption", "Reanimation Intubation")]
            public System.Boolean reanimation_intubation {
                get { return freanimation_intubation; }
                set { SetPropertyValue("reanimation_intubation", ref freanimation_intubation, value); }
            }
    
            private System.Boolean freanimation_oxygen;
            [Custom("Caption", "Reanimation Oxygen")]
            public System.Boolean reanimation_oxygen {
                get { return freanimation_oxygen; }
                set { SetPropertyValue("reanimation_oxygen", ref freanimation_oxygen, value); }
            }
    
            private DateTime? ftod;
            [Custom("Caption", "Tod")]
            public DateTime? tod {
                get { return ftod; }
                set { SetPropertyValue("tod", ref ftod, value); }
            }
    
            private System.Boolean fbd;
            [Custom("Caption", "Bd")]
            public System.Boolean bd {
                get { return fbd; }
                set { SetPropertyValue("bd", ref fbd, value); }
            }
    
            private System.Boolean fmeconium;
            [Custom("Caption", "Meconium")]
            public System.Boolean meconium {
                get { return fmeconium; }
                set { SetPropertyValue("meconium", ref fmeconium, value); }
            }
    
            private System.Boolean ftest_billirubin;
            [Custom("Caption", "Test Billirubin")]
            public System.Boolean test_billirubin {
                get { return ftest_billirubin; }
                set { SetPropertyValue("test_billirubin", ref ftest_billirubin, value); }
            }
    
            private System.Boolean ftest_chagas;
            [Custom("Caption", "Test Chagas")]
            public System.Boolean test_chagas {
                get { return ftest_chagas; }
                set { SetPropertyValue("test_chagas", ref ftest_chagas, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.Boolean ftest_audition;
            [Custom("Caption", "Test Audition")]
            public System.Boolean test_audition {
                get { return ftest_audition; }
                set { SetPropertyValue("test_audition", ref ftest_audition, value); }
            }
    
            private System.Boolean ftest_metabolic;
            [Custom("Caption", "Test Metabolic")]
            public System.Boolean test_metabolic {
                get { return ftest_metabolic; }
                set { SetPropertyValue("test_metabolic", ref ftest_metabolic, value); }
            }
    
            private System.Boolean fdied_at_delivery;
            [Custom("Caption", "Died At delivery")]
            public System.Boolean died_at_delivery {
                get { return fdied_at_delivery; }
                set { SetPropertyValue("died_at_delivery", ref fdied_at_delivery, value); }
            }
    
            private System.Int32 flength;
            [Custom("Caption", "Length")]
            public System.Int32 length {
                get { return flength; }
                set { SetPropertyValue("length", ref flength, value); }
            }
    
            private System.Boolean fdied_being_transferred;
            [Custom("Caption", "Died Being transferred")]
            public System.Boolean died_being_transferred {
                get { return fdied_being_transferred; }
                set { SetPropertyValue("died_being_transferred", ref fdied_being_transferred, value); }
            }
    
        
            private medical_pathology fcod;
            //FK FK_medical_newborn_cod
            [Custom("Caption", "Cod")]
            public medical_pathology cod {
                get { return fcod; }
                set { SetPropertyValue<medical_pathology>("cod", ref fcod, value); }
            }
    
            private System.Boolean ftest_vdrl;
            [Custom("Caption", "Test Vdrl")]
            public System.Boolean test_vdrl {
                get { return ftest_vdrl; }
                set { SetPropertyValue("test_vdrl", ref ftest_vdrl, value); }
            }
    
            private System.Boolean fdied_at_the_hospital;
            [Custom("Caption", "Died At the hospital")]
            public System.Boolean died_at_the_hospital {
                get { return fdied_at_the_hospital; }
                set { SetPropertyValue("died_at_the_hospital", ref fdied_at_the_hospital, value); }
            }
    
            private DateTime? fbirth_date;
            [Custom("Caption", "Birth Date")]
            public DateTime? birth_date {
                get { return fbirth_date; }
                set { SetPropertyValue("birth_date", ref fbirth_date, value); }
            }
    
            private System.Int32 fapgar5;
            [Custom("Caption", "Apgar5")]
            public System.Int32 apgar5 {
                get { return fapgar5; }
                set { SetPropertyValue("apgar5", ref fapgar5, value); }
            }
    
            private System.Int32 fapgar1;
            [Custom("Caption", "Apgar1")]
            public System.Int32 apgar1 {
                get { return fapgar1; }
                set { SetPropertyValue("apgar1", ref fapgar1, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_newborn(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

