
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
	[DefaultProperty("name")]
    [Persistent("res_company")]
	public partial class res_company : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_company_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private res_company fparent_id;
            //FK FK_res_company_parent_id
            [Custom("Caption", "Parent Id")]
            public res_company parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<res_company>("parent_id", ref fparent_id, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_res_company_create_uid
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
            //FK FK_res_company_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String frml_footer1;
            [Size(200)]
            [Custom("Caption", "Rml Footer1")]
            public System.String rml_footer1 {
                get { return frml_footer1; }
                set { SetPropertyValue("rml_footer1", ref frml_footer1, value); }
            }
    
            private System.String frml_footer2;
            [Size(200)]
            [Custom("Caption", "Rml Footer2")]
            public System.String rml_footer2 {
                get { return frml_footer2; }
                set { SetPropertyValue("rml_footer2", ref frml_footer2, value); }
            }
    
        
            private res_currency fcurrency_id;
            //FK FK_res_company_currency_id
            [Custom("Caption", "Currency Id")]
            public res_currency currency_id {
                get { return fcurrency_id; }
                set { SetPropertyValue<res_currency>("currency_id", ref fcurrency_id, value); }
            }
    
            private System.String frml_header2;
            [Size(-1)]
            [Custom("Caption", "Rml Header2")]
            public System.String rml_header2 {
                get { return frml_header2; }
                set { SetPropertyValue("rml_header2", ref frml_header2, value); }
            }
    
            private System.String frml_header1;
            [Size(200)]
            [Custom("Caption", "Rml Header1")]
            public System.String rml_header1 {
                get { return frml_header1; }
                set { SetPropertyValue("rml_header1", ref frml_header1, value); }
            }
    
            private System.Byte[] flogo;
            [Custom("Caption", "Logo")]
            public System.Byte[] logo {
                get { return flogo; }
                set { SetPropertyValue("logo", ref flogo, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_res_company_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String frml_header;
            [Size(-1)]
            [Custom("Caption", "Rml Header")]
            public System.String rml_header {
                get { return frml_header; }
                set { SetPropertyValue("rml_header", ref frml_header, value); }
            }
    
            private System.String foverdue_msg;
            [Size(-1)]
            [Custom("Caption", "Overdue Msg")]
            public System.String overdue_msg {
                get { return foverdue_msg; }
                set { SetPropertyValue("overdue_msg", ref foverdue_msg, value); }
            }
    
            private System.Double fmanufacturing_lead;
            [Custom("Caption", "Manufacturing Lead")]
            public System.Double manufacturing_lead {
                get { return fmanufacturing_lead; }
                set { SetPropertyValue("manufacturing_lead", ref fmanufacturing_lead, value); }
            }
    
            private System.Double fsecurity_lead;
            [Custom("Caption", "Security Lead")]
            public System.Double security_lead {
                get { return fsecurity_lead; }
                set { SetPropertyValue("security_lead", ref fsecurity_lead, value); }
            }
    
            private System.Double fpo_lead;
            [Custom("Caption", "Po Lead")]
            public System.Double po_lead {
                get { return fpo_lead; }
                set { SetPropertyValue("po_lead", ref fpo_lead, value); }
            }
    
            private System.Double fschedule_range;
            [Custom("Caption", "Schedule Range")]
            public System.Double schedule_range {
                get { return fschedule_range; }
                set { SetPropertyValue("schedule_range", ref fschedule_range, value); }
            }
    
            private System.Double fbvr_delta_vert;
            [Custom("Caption", "Bvr Delta vert")]
            public System.Double bvr_delta_vert {
                get { return fbvr_delta_vert; }
                set { SetPropertyValue("bvr_delta_vert", ref fbvr_delta_vert, value); }
            }
    
            private System.Double fbvr_delta_horz;
            [Custom("Caption", "Bvr Delta horz")]
            public System.Double bvr_delta_horz {
                get { return fbvr_delta_horz; }
                set { SetPropertyValue("bvr_delta_horz", ref fbvr_delta_horz, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_company(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

