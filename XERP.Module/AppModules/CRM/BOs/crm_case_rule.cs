
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
	[DefaultProperty("trg_priority_to")]
    [Persistent("crm_case_rule")]
	public partial class crm_case_rule : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //crm_case_rule_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_crm_case_rule_create_uid
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
            //FK FK_crm_case_rule_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private crm_case_categ ftrg_categ_id;
            //FK FK_crm_case_rule_trg_categ_id
            [Custom("Caption", "Trg Categ id")]
            public crm_case_categ trg_categ_id {
                get { return ftrg_categ_id; }
                set { SetPropertyValue<crm_case_categ>("trg_categ_id", ref ftrg_categ_id, value); }
            }
    
        
            private crm_case_section ftrg_section_id;
            //FK FK_crm_case_rule_trg_section_id
            [Custom("Caption", "Trg Section id")]
            public crm_case_section trg_section_id {
                get { return ftrg_section_id; }
                set { SetPropertyValue<crm_case_section>("trg_section_id", ref ftrg_section_id, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.Boolean fact_mail_to_partner;
            [Custom("Caption", "Act Mail to partner")]
            public System.Boolean act_mail_to_partner {
                get { return fact_mail_to_partner; }
                set { SetPropertyValue("act_mail_to_partner", ref fact_mail_to_partner, value); }
            }
    
            private System.String ftrg_priority_to;
            [Size(16)]
            [Custom("Caption", "Trg Priority to")]
            public System.String trg_priority_to {
                get { return ftrg_priority_to; }
                set { SetPropertyValue("trg_priority_to", ref ftrg_priority_to, value); }
            }
    
            private System.Boolean fact_remind_partner;
            [Custom("Caption", "Act Remind partner")]
            public System.Boolean act_remind_partner {
                get { return fact_remind_partner; }
                set { SetPropertyValue("act_remind_partner", ref fact_remind_partner, value); }
            }
    
            private System.Int32 ftrg_max_history;
            [Custom("Caption", "Trg Max history")]
            public System.Int32 trg_max_history {
                get { return ftrg_max_history; }
                set { SetPropertyValue("trg_max_history", ref ftrg_max_history, value); }
            }
    
            private System.String ftrg_date_range_type;
            [Size(16)]
            [Custom("Caption", "Trg Date range type")]
            public System.String trg_date_range_type {
                get { return ftrg_date_range_type; }
                set { SetPropertyValue("trg_date_range_type", ref ftrg_date_range_type, value); }
            }
    
            private System.String fact_method;
            [Size(64)]
            [Custom("Caption", "Act Method")]
            public System.String act_method {
                get { return fact_method; }
                set { SetPropertyValue("act_method", ref fact_method, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Int32 ftrg_date_range;
            [Custom("Caption", "Trg Date range")]
            public System.Int32 trg_date_range {
                get { return ftrg_date_range; }
                set { SetPropertyValue("trg_date_range", ref ftrg_date_range, value); }
            }
    
            private System.Boolean fact_remind_user;
            [Custom("Caption", "Act Remind user")]
            public System.Boolean act_remind_user {
                get { return fact_remind_user; }
                set { SetPropertyValue("act_remind_user", ref fact_remind_user, value); }
            }
    
            private System.String ftrg_priority_from;
            [Size(16)]
            [Custom("Caption", "Trg Priority from")]
            public System.String trg_priority_from {
                get { return ftrg_priority_from; }
                set { SetPropertyValue("trg_priority_from", ref ftrg_priority_from, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftrg_date_type;
            [Size(16)]
            [Custom("Caption", "Trg Date type")]
            public System.String trg_date_type {
                get { return ftrg_date_type; }
                set { SetPropertyValue("trg_date_type", ref ftrg_date_type, value); }
            }
    
            private System.String ftrg_state1_from;
            [Size(16)]
            [Custom("Caption", "Trg State1 from")]
            public System.String trg_state1_from {
                get { return ftrg_state1_from; }
                set { SetPropertyValue("trg_state1_from", ref ftrg_state1_from, value); }
            }
    
        
            private res_users fact_user_id;
            //FK FK_crm_case_rule_act_user_id
            [Custom("Caption", "Act User id")]
            public res_users act_user_id {
                get { return fact_user_id; }
                set { SetPropertyValue<res_users>("act_user_id", ref fact_user_id, value); }
            }
    
            private System.String fact_state1;
            [Size(16)]
            [Custom("Caption", "Act State1")]
            public System.String act_state1 {
                get { return fact_state1; }
                set { SetPropertyValue("act_state1", ref fact_state1, value); }
            }
    
            private System.Boolean fact_mail_to_user;
            [Custom("Caption", "Act Mail to user")]
            public System.Boolean act_mail_to_user {
                get { return fact_mail_to_user; }
                set { SetPropertyValue("act_mail_to_user", ref fact_mail_to_user, value); }
            }
    
            private System.String fact_mail_body;
            [Size(-1)]
            [Custom("Caption", "Act Mail body")]
            public System.String act_mail_body {
                get { return fact_mail_body; }
                set { SetPropertyValue("act_mail_body", ref fact_mail_body, value); }
            }
    
        
            private crm_case_section fact_section_id;
            //FK FK_crm_case_rule_act_section_id
            [Custom("Caption", "Act Section id")]
            public crm_case_section act_section_id {
                get { return fact_section_id; }
                set { SetPropertyValue<crm_case_section>("act_section_id", ref fact_section_id, value); }
            }
    
        
            private res_partner ftrg_partner_id;
            //FK FK_crm_case_rule_trg_partner_id
            [Custom("Caption", "Trg Partner id")]
            public res_partner trg_partner_id {
                get { return ftrg_partner_id; }
                set { SetPropertyValue<res_partner>("trg_partner_id", ref ftrg_partner_id, value); }
            }
    
        
            private res_partner_category ftrg_partner_categ_id;
            //FK FK_crm_case_rule_trg_partner_categ_id
            [Custom("Caption", "Trg Partner categ id")]
            public res_partner_category trg_partner_categ_id {
                get { return ftrg_partner_categ_id; }
                set { SetPropertyValue<res_partner_category>("trg_partner_categ_id", ref ftrg_partner_categ_id, value); }
            }
    
            private System.String fact_email_cc;
            [Size(250)]
            [Custom("Caption", "Act Email cc")]
            public System.String act_email_cc {
                get { return fact_email_cc; }
                set { SetPropertyValue("act_email_cc", ref fact_email_cc, value); }
            }
    
            private System.String fact_priority;
            [Size(16)]
            [Custom("Caption", "Act Priority")]
            public System.String act_priority {
                get { return fact_priority; }
                set { SetPropertyValue("act_priority", ref fact_priority, value); }
            }
    
            private System.Boolean fact_mail_to_watchers;
            [Custom("Caption", "Act Mail to watchers")]
            public System.Boolean act_mail_to_watchers {
                get { return fact_mail_to_watchers; }
                set { SetPropertyValue("act_mail_to_watchers", ref fact_mail_to_watchers, value); }
            }
    
            private System.String ftrg_state1_to;
            [Size(16)]
            [Custom("Caption", "Trg State1 to")]
            public System.String trg_state1_to {
                get { return ftrg_state1_to; }
                set { SetPropertyValue("trg_state1_to", ref ftrg_state1_to, value); }
            }
    
            private System.String fact_mail_to_email;
            [Size(128)]
            [Custom("Caption", "Act Mail to email")]
            public System.String act_mail_to_email {
                get { return fact_mail_to_email; }
                set { SetPropertyValue("act_mail_to_email", ref fact_mail_to_email, value); }
            }
    
        
            private res_users ftrg_user_id;
            //FK FK_crm_case_rule_trg_user_id
            [Custom("Caption", "Trg User id")]
            public res_users trg_user_id {
                get { return ftrg_user_id; }
                set { SetPropertyValue<res_users>("trg_user_id", ref ftrg_user_id, value); }
            }
    
            private System.Boolean fact_remind_attach;
            [Custom("Caption", "Act Remind attach")]
            public System.Boolean act_remind_attach {
                get { return fact_remind_attach; }
                set { SetPropertyValue("act_remind_attach", ref fact_remind_attach, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public crm_case_rule(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

