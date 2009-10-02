
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
	[DefaultProperty("comment")]
    [Persistent("res_partner")]
	public partial class res_partner : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_partner_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_partner_create_uid
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
            //FK FK_res_partner_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fcomment;
            [Size(-1)]
            [Custom("Caption", "Comment")]
            public System.String comment {
                get { return fcomment; }
                set { SetPropertyValue("comment", ref fcomment, value); }
            }
    
            private System.String fean13;
            [Size(13)]
            [Custom("Caption", "Ean13")]
            public System.String ean13 {
                get { return fean13; }
                set { SetPropertyValue("ean13", ref fean13, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String flang;
            [Size(5)]
            [Custom("Caption", "Lang")]
            public System.String lang {
                get { return flang; }
                set { SetPropertyValue("lang", ref flang, value); }
            }
    
            private System.Boolean fcustomer;
            [Custom("Caption", "Customer")]
            public System.Boolean customer {
                get { return fcustomer; }
                set { SetPropertyValue("customer", ref fcustomer, value); }
            }
    
            private System.Double fcredit_limit;
            [Custom("Caption", "Credit Limit")]
            public System.Double credit_limit {
                get { return fcredit_limit; }
                set { SetPropertyValue("credit_limit", ref fcredit_limit, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_res_partner_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftitle;
            [Size(32)]
            [Custom("Caption", "Title")]
            public System.String title {
                get { return ftitle; }
                set { SetPropertyValue("title", ref ftitle, value); }
            }
    
            private System.String fwebsite;
            [Size(64)]
            [Custom("Caption", "Website")]
            public System.String website {
                get { return fwebsite; }
                set { SetPropertyValue("website", ref fwebsite, value); }
            }
    
        
            private res_partner fparent_id;
            //FK FK_res_partner_parent_id
            [Custom("Caption", "Parent Id")]
            public res_partner parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<res_partner>("parent_id", ref fparent_id, value); }
            }
    
            private System.Boolean fsupplier;
            [Custom("Caption", "Supplier")]
            public System.Boolean supplier {
                get { return fsupplier; }
                set { SetPropertyValue("supplier", ref fsupplier, value); }
            }
    
            private System.String fref1;
            [Size(64)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
            private System.String fvat;
            [Size(32)]
            [Custom("Caption", "Vat")]
            public System.String vat {
                get { return fvat; }
                set { SetPropertyValue("vat", ref fvat, value); }
            }
    
            private System.Double fdebit_limit;
            [Custom("Caption", "Debit Limit")]
            public System.Double debit_limit {
                get { return fdebit_limit; }
                set { SetPropertyValue("debit_limit", ref fdebit_limit, value); }
            }
    
            private System.Boolean fvat_subjected;
            [Custom("Caption", "Vat Subjected")]
            public System.Boolean vat_subjected {
                get { return fvat_subjected; }
                set { SetPropertyValue("vat_subjected", ref fvat_subjected, value); }
            }
    
            private System.String fpurchase_warn;
            [Size(16)]
            [Custom("Caption", "Purchase Warn")]
            public System.String purchase_warn {
                get { return fpurchase_warn; }
                set { SetPropertyValue("purchase_warn", ref fpurchase_warn, value); }
            }
    
            private System.String fsale_warn;
            [Size(16)]
            [Custom("Caption", "Sale Warn")]
            public System.String sale_warn {
                get { return fsale_warn; }
                set { SetPropertyValue("sale_warn", ref fsale_warn, value); }
            }
    
            private System.String fpicking_warn_msg;
            [Size(-1)]
            [Custom("Caption", "Picking Warn msg")]
            public System.String picking_warn_msg {
                get { return fpicking_warn_msg; }
                set { SetPropertyValue("picking_warn_msg", ref fpicking_warn_msg, value); }
            }
    
            private System.String finvoice_warn;
            [Size(16)]
            [Custom("Caption", "Invoice Warn")]
            public System.String invoice_warn {
                get { return finvoice_warn; }
                set { SetPropertyValue("invoice_warn", ref finvoice_warn, value); }
            }
    
            private System.String fpicking_warn;
            [Size(16)]
            [Custom("Caption", "Picking Warn")]
            public System.String picking_warn {
                get { return fpicking_warn; }
                set { SetPropertyValue("picking_warn", ref fpicking_warn, value); }
            }
    
            private System.String fpurchase_warn_msg;
            [Size(-1)]
            [Custom("Caption", "Purchase Warn msg")]
            public System.String purchase_warn_msg {
                get { return fpurchase_warn_msg; }
                set { SetPropertyValue("purchase_warn_msg", ref fpurchase_warn_msg, value); }
            }
    
            private System.String fsale_warn_msg;
            [Size(-1)]
            [Custom("Caption", "Sale Warn msg")]
            public System.String sale_warn_msg {
                get { return fsale_warn_msg; }
                set { SetPropertyValue("sale_warn_msg", ref fsale_warn_msg, value); }
            }
    
            private System.String finvoice_warn_msg;
            [Size(-1)]
            [Custom("Caption", "Invoice Warn msg")]
            public System.String invoice_warn_msg {
                get { return finvoice_warn_msg; }
                set { SetPropertyValue("invoice_warn_msg", ref finvoice_warn_msg, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_partner(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

