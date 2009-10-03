
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
	[DefaultProperty("name")]
    [Persistent("module_quality_detail")]
	public partial class module_quality_detail : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //module_quality_detail_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_module_quality_detail_create_uid
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
            //FK FK_module_quality_detail_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fdetail;
            [Size(-1)]
            [Custom("Caption", "Detail")]
            public System.String detail {
                get { return fdetail; }
                set { SetPropertyValue("detail", ref fdetail, value); }
            }
    
            private System.String fsummary;
            [Size(-1)]
            [Custom("Caption", "Summary")]
            public System.String summary {
                get { return fsummary; }
                set { SetPropertyValue("summary", ref fsummary, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
            private System.String fstate1;
            [Size(7)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.Double fscore;
            [Custom("Caption", "Score")]
            public System.Double score {
                get { return fscore; }
                set { SetPropertyValue("score", ref fscore, value); }
            }
    
        
            private module_quality_check fquality_check_id;
            //FK FK_module_quality_detail_quality_check_id
            [Custom("Caption", "Quality Check id")]
            public module_quality_check quality_check_id {
                get { return fquality_check_id; }
                set { SetPropertyValue<module_quality_check>("quality_check_id", ref fquality_check_id, value); }
            }
    
            private System.Double fponderation;
            [Custom("Caption", "Ponderation")]
            public System.Double ponderation {
                get { return fponderation; }
                set { SetPropertyValue("ponderation", ref fponderation, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public module_quality_detail(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

