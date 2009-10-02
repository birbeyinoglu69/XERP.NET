
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
	[DefaultProperty("ref2")]
    [Persistent("crm_case")]
	public partial class crm_case : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //crm_case_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_crm_case_create_uid
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
            //FK FK_crm_case_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private DateTime? fdate_closed;
            [Custom("Caption", "Date Closed")]
            public DateTime? date_closed {
                get { return fdate_closed; }
                set { SetPropertyValue("date_closed", ref fdate_closed, value); }
            }
    
            private System.String fref2;
            [Size(128)]
            [Custom("Caption", "Ref2")]
            public System.String ref2 {
                get { return fref2; }
                set { SetPropertyValue("ref2", ref fref2, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private System.Double fprobability;
            [Custom("Caption", "Probability")]
            public System.Double probability {
                get { return fprobability; }
                set { SetPropertyValue("probability", ref fprobability, value); }
            }
    
        
            private res_partner_canal fcanal_id;
            //FK FK_crm_case_canal_id
            [Custom("Caption", "Canal Id")]
            public res_partner_canal canal_id {
                get { return fcanal_id; }
                set { SetPropertyValue<res_partner_canal>("canal_id", ref fcanal_id, value); }
            }
    
            private DateTime? fdate_action_last;
            [Custom("Caption", "Date Action last")]
            public DateTime? date_action_last {
                get { return fdate_action_last; }
                set { SetPropertyValue("date_action_last", ref fdate_action_last, value); }
            }
    
            private System.Double fplanned_cost;
            [Custom("Caption", "Planned Cost")]
            public System.Double planned_cost {
                get { return fplanned_cost; }
                set { SetPropertyValue("planned_cost", ref fplanned_cost, value); }
            }
    
        
            private res_partner_address fpartner_address_id;
            //FK FK_crm_case_partner_address_id
            [Custom("Caption", "Partner Address id")]
            public res_partner_address partner_address_id {
                get { return fpartner_address_id; }
                set { SetPropertyValue<res_partner_address>("partner_address_id", ref fpartner_address_id, value); }
            }
    
        
            private res_partner_som fsom;
            //FK FK_crm_case_som
            [Custom("Caption", "Som")]
            public res_partner_som som {
                get { return fsom; }
                set { SetPropertyValue<res_partner_som>("som", ref fsom, value); }
            }
    
        
            private crm_case_section fsection_id;
            //FK FK_crm_case_section_id
            [Custom("Caption", "Section Id")]
            public crm_case_section section_id {
                get { return fsection_id; }
                set { SetPropertyValue<crm_case_section>("section_id", ref fsection_id, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_crm_case_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private DateTime? fdate_action_next;
            [Custom("Caption", "Date Action next")]
            public DateTime? date_action_next {
                get { return fdate_action_next; }
                set { SetPropertyValue("date_action_next", ref fdate_action_next, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_crm_case_user_id
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
    
            private DateTime? fdate_deadline;
            [Custom("Caption", "Date Deadline")]
            public DateTime? date_deadline {
                get { return fdate_deadline; }
                set { SetPropertyValue("date_deadline", ref fdate_deadline, value); }
            }
    
            private System.Double fplanned_revenue;
            [Custom("Caption", "Planned Revenue")]
            public System.Double planned_revenue {
                get { return fplanned_revenue; }
                set { SetPropertyValue("planned_revenue", ref fplanned_revenue, value); }
            }
    
        
            private crm_case_categ fcateg_id;
            //FK FK_crm_case_categ_id
            [Custom("Caption", "Categ Id")]
            public crm_case_categ categ_id {
                get { return fcateg_id; }
                set { SetPropertyValue<crm_case_categ>("categ_id", ref fcateg_id, value); }
            }
    
            private System.String fpriority;
            [Size(16)]
            [Custom("Caption", "Priority")]
            public System.String priority {
                get { return fpriority; }
                set { SetPropertyValue("priority", ref fpriority, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String femail_cc;
            [Size(252)]
            [Custom("Caption", "Email Cc")]
            public System.String email_cc {
                get { return femail_cc; }
                set { SetPropertyValue("email_cc", ref femail_cc, value); }
            }
    
            private System.String fref1;
            [Size(128)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
            private System.String femail_from;
            [Size(128)]
            [Custom("Caption", "Email From")]
            public System.String email_from {
                get { return femail_from; }
                set { SetPropertyValue("email_from", ref femail_from, value); }
            }
    
        
            private crm_case_category2 fcategory2_id;
            //FK FK_crm_case_category2_id
            [Custom("Caption", "Category2 Id")]
            public crm_case_category2 category2_id {
                get { return fcategory2_id; }
                set { SetPropertyValue<crm_case_category2>("category2_id", ref fcategory2_id, value); }
            }
    
            private System.Double fduration;
            [Custom("Caption", "Duration")]
            public System.Double duration {
                get { return fduration; }
                set { SetPropertyValue("duration", ref fduration, value); }
            }
    
            private System.String fpartner_name;
            [Size(64)]
            [Custom("Caption", "Partner Name")]
            public System.String partner_name {
                get { return fpartner_name; }
                set { SetPropertyValue("partner_name", ref fpartner_name, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private crm_case fcase_id;
            //FK FK_crm_case_case_id
            [Custom("Caption", "Case Id")]
            public crm_case case_id {
                get { return fcase_id; }
                set { SetPropertyValue<crm_case>("case_id", ref fcase_id, value); }
            }
    
            private System.String fpartner_name2;
            [Size(64)]
            [Custom("Caption", "Partner Name2")]
            public System.String partner_name2 {
                get { return fpartner_name2; }
                set { SetPropertyValue("partner_name2", ref fpartner_name2, value); }
            }
    
            private System.String fpartner_mobile;
            [Size(32)]
            [Custom("Caption", "Partner Mobile")]
            public System.String partner_mobile {
                get { return fpartner_mobile; }
                set { SetPropertyValue("partner_mobile", ref fpartner_mobile, value); }
            }
    
        
            private crm_case_stage fstage_id;
            //FK FK_crm_case_stage_id
            [Custom("Caption", "Stage Id")]
            public crm_case_stage stage_id {
                get { return fstage_id; }
                set { SetPropertyValue<crm_case_stage>("stage_id", ref fstage_id, value); }
            }
    
            private System.String fpartner_phone;
            [Size(32)]
            [Custom("Caption", "Partner Phone")]
            public System.String partner_phone {
                get { return fpartner_phone; }
                set { SetPropertyValue("partner_phone", ref fpartner_phone, value); }
            }
    
            private System.String fcode;
            [Size(64)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public crm_case(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

