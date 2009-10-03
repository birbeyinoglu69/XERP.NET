
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
    [Persistent("medical_pathology")]
	public partial class medical_pathology : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_pathology_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_pathology_create_uid
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
            //FK FK_medical_pathology_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private medical_pathology_category fcategory;
            //FK FK_medical_pathology_category
            [Custom("Caption", "Category")]
            public medical_pathology_category category {
                get { return fcategory; }
                set { SetPropertyValue<medical_pathology_category>("category", ref fcategory, value); }
            }
    
            private System.String finfo;
            [Size(-1)]
            [Custom("Caption", "Info")]
            public System.String info {
                get { return finfo; }
                set { SetPropertyValue("info", ref finfo, value); }
            }
    
            private System.String fcode;
            [Size(5)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fprotein;
            [Size(128)]
            [Custom("Caption", "Protein")]
            public System.String protein {
                get { return fprotein; }
                set { SetPropertyValue("protein", ref fprotein, value); }
            }
    
            private System.String fgene;
            [Size(128)]
            [Custom("Caption", "Gene")]
            public System.String gene {
                get { return fgene; }
                set { SetPropertyValue("gene", ref fgene, value); }
            }
    
            private System.String fchromosome;
            [Size(128)]
            [Custom("Caption", "Chromosome")]
            public System.String chromosome {
                get { return fchromosome; }
                set { SetPropertyValue("chromosome", ref fchromosome, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_pathology(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

