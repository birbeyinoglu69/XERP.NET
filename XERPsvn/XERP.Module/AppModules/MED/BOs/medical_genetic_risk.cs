
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
	[DefaultProperty("info")]
    [Persistent("medical_genetic_risk")]
	public partial class medical_genetic_risk : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_genetic_risk_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_genetic_risk_create_uid
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
            //FK FK_medical_genetic_risk_write_uid
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
    
            private System.String flong_name;
            [Size(256)]
            [Custom("Caption", "Long Name")]
            public System.String long_name {
                get { return flong_name; }
                set { SetPropertyValue("long_name", ref flong_name, value); }
            }
    
            private System.String fname;
            [Size(16)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fchromosome;
            [Size(2)]
            [Custom("Caption", "Chromosome")]
            public System.String chromosome {
                get { return fchromosome; }
                set { SetPropertyValue("chromosome", ref fchromosome, value); }
            }
    
            private System.String fdominance;
            [Size(16)]
            [Custom("Caption", "Dominance")]
            public System.String dominance {
                get { return fdominance; }
                set { SetPropertyValue("dominance", ref fdominance, value); }
            }
    
            private System.String fgene_id;
            [Size(8)]
            [Custom("Caption", "Gene Id")]
            public System.String gene_id {
                get { return fgene_id; }
                set { SetPropertyValue("gene_id", ref fgene_id, value); }
            }
    
            private System.String flocation;
            [Size(32)]
            [Custom("Caption", "Location")]
            public System.String location {
                get { return flocation; }
                set { SetPropertyValue("location", ref flocation, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_genetic_risk(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

