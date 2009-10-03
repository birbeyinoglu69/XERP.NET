
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
	[DefaultProperty("body")]
    [Persistent("res_request")]
	public partial class res_request : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_request_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_request_create_uid
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
            //FK FK_res_request_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fbody;
            [Size(-1)]
            [Custom("Caption", "Body")]
            public System.String body {
                get { return fbody; }
                set { SetPropertyValue("body", ref fbody, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fref_doc2;
            [Size(128)]
            [Custom("Caption", "Ref Doc2")]
            public System.String ref_doc2 {
                get { return fref_doc2; }
                set { SetPropertyValue("ref_doc2", ref fref_doc2, value); }
            }
    
            private System.String fpriority;
            [Size(16)]
            [Custom("Caption", "Priority")]
            public System.String priority {
                get { return fpriority; }
                set { SetPropertyValue("priority", ref fpriority, value); }
            }
    
            private System.String fref_doc1;
            [Size(128)]
            [Custom("Caption", "Ref Doc1")]
            public System.String ref_doc1 {
                get { return fref_doc1; }
                set { SetPropertyValue("ref_doc1", ref fref_doc1, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private res_users fact_from;
            //FK FK_res_request_act_from
            [Custom("Caption", "Act From")]
            public res_users act_from {
                get { return fact_from; }
                set { SetPropertyValue<res_users>("act_from", ref fact_from, value); }
            }
    
        
            private res_partner fref_partner_id;
            //FK FK_res_request_ref_partner_id
            [Custom("Caption", "Ref Partner id")]
            public res_partner ref_partner_id {
                get { return fref_partner_id; }
                set { SetPropertyValue<res_partner>("ref_partner_id", ref fref_partner_id, value); }
            }
    
            private DateTime? fdate_sent;
            [Custom("Caption", "Date Sent")]
            public DateTime? date_sent {
                get { return fdate_sent; }
                set { SetPropertyValue("date_sent", ref fdate_sent, value); }
            }
    
            private DateTime? ftrigger_date;
            [Custom("Caption", "Trigger Date")]
            public DateTime? trigger_date {
                get { return ftrigger_date; }
                set { SetPropertyValue("trigger_date", ref ftrigger_date, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
        
            private res_users fact_to;
            //FK FK_res_request_act_to
            [Custom("Caption", "Act To")]
            public res_users act_to {
                get { return fact_to; }
                set { SetPropertyValue<res_users>("act_to", ref fact_to, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_request(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

