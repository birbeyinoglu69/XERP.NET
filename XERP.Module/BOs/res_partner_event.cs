
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
    [Persistent("res_partner_event")]
	public partial class res_partner_event : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_partner_event_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_partner_event_create_uid
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
            //FK FK_res_partner_event_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_res_partner_event_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Double fprobability;
            [Custom("Caption", "Probability")]
            public System.Double probability {
                get { return fprobability; }
                set { SetPropertyValue("probability", ref fprobability, value); }
            }
    
        
            private res_partner_canal fcanal_id;
            //FK FK_res_partner_event_canal_id
            [Custom("Caption", "Canal Id")]
            public res_partner_canal canal_id {
                get { return fcanal_id; }
                set { SetPropertyValue<res_partner_canal>("canal_id", ref fcanal_id, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.Double fplanned_cost;
            [Custom("Caption", "Planned Cost")]
            public System.Double planned_cost {
                get { return fplanned_cost; }
                set { SetPropertyValue("planned_cost", ref fplanned_cost, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
        
            private res_partner_som fsom;
            //FK FK_res_partner_event_som
            [Custom("Caption", "Som")]
            public res_partner_som som {
                get { return fsom; }
                set { SetPropertyValue<res_partner_som>("som", ref fsom, value); }
            }
    
            private System.String fpartner_type;
            [Size(16)]
            [Custom("Caption", "Partner Type")]
            public System.String partner_type {
                get { return fpartner_type; }
                set { SetPropertyValue("partner_type", ref fpartner_type, value); }
            }
    
            private System.Double fplanned_revenue;
            [Custom("Caption", "Planned Revenue")]
            public System.Double planned_revenue {
                get { return fplanned_revenue; }
                set { SetPropertyValue("planned_revenue", ref fplanned_revenue, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
            private System.String fdocument;
            [Size(128)]
            [Custom("Caption", "Document")]
            public System.String document {
                get { return fdocument; }
                set { SetPropertyValue("document", ref fdocument, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_res_partner_event_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String fevent_ical_id;
            [Size(64)]
            [Custom("Caption", "Event Ical id")]
            public System.String event_ical_id {
                get { return fevent_ical_id; }
                set { SetPropertyValue("event_ical_id", ref fevent_ical_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_partner_event(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

