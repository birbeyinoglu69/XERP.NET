
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
	[DefaultProperty("ean13")]
    [Persistent("product_product")]
	public partial class product_product : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //product_product_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_product_product_create_uid
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
            //FK FK_product_product_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fean13;
            [Size(13)]
            [Custom("Caption", "Ean13")]
            public System.String ean13 {
                get { return fean13; }
                set { SetPropertyValue("ean13", ref fean13, value); }
            }
    
            private System.Decimal fprice_extra;
            [Custom("Caption", "Price Extra")]
            public System.Decimal price_extra {
                get { return fprice_extra; }
                set { SetPropertyValue("price_extra", ref fprice_extra, value); }
            }
    
            private System.String fdefault_code;
            [Size(64)]
            [Custom("Caption", "Default Code")]
            public System.String default_code {
                get { return fdefault_code; }
                set { SetPropertyValue("default_code", ref fdefault_code, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String fvariants;
            [Size(64)]
            [Custom("Caption", "Variants")]
            public System.String variants {
                get { return fvariants; }
                set { SetPropertyValue("variants", ref fvariants, value); }
            }
    
        
            private product_template fproduct_tmpl_id;
            //FK FK_product_product_product_tmpl_id
            [Custom("Caption", "Product Tmpl id")]
            public product_template product_tmpl_id {
                get { return fproduct_tmpl_id; }
                set { SetPropertyValue<product_template>("product_tmpl_id", ref fproduct_tmpl_id, value); }
            }
    
            private System.Decimal fprice_margin;
            [Custom("Caption", "Price Margin")]
            public System.Decimal price_margin {
                get { return fprice_margin; }
                set { SetPropertyValue("price_margin", ref fprice_margin, value); }
            }
    
            private System.Boolean ftrack_production;
            [Custom("Caption", "Track Production")]
            public System.Boolean track_production {
                get { return ftrack_production; }
                set { SetPropertyValue("track_production", ref ftrack_production, value); }
            }
    
            private System.Boolean ftrack_outgoing;
            [Custom("Caption", "Track Outgoing")]
            public System.Boolean track_outgoing {
                get { return ftrack_outgoing; }
                set { SetPropertyValue("track_outgoing", ref ftrack_outgoing, value); }
            }
    
            private System.Boolean ftrack_incoming;
            [Custom("Caption", "Track Incoming")]
            public System.Boolean track_incoming {
                get { return ftrack_incoming; }
                set { SetPropertyValue("track_incoming", ref ftrack_incoming, value); }
            }
    
            private System.String fpricelist_sale;
            [Size(-1)]
            [Custom("Caption", "Pricelist Sale")]
            public System.String pricelist_sale {
                get { return fpricelist_sale; }
                set { SetPropertyValue("pricelist_sale", ref fpricelist_sale, value); }
            }
    
            private System.String fpricelist_purchase;
            [Size(-1)]
            [Custom("Caption", "Pricelist Purchase")]
            public System.String pricelist_purchase {
                get { return fpricelist_purchase; }
                set { SetPropertyValue("pricelist_purchase", ref fpricelist_purchase, value); }
            }
    
            private System.String fsale_line_warn_msg;
            [Size(-1)]
            [Custom("Caption", "Sale Line warn msg")]
            public System.String sale_line_warn_msg {
                get { return fsale_line_warn_msg; }
                set { SetPropertyValue("sale_line_warn_msg", ref fsale_line_warn_msg, value); }
            }
    
            private System.String fpurchase_line_warn_msg;
            [Size(-1)]
            [Custom("Caption", "Purchase Line warn msg")]
            public System.String purchase_line_warn_msg {
                get { return fpurchase_line_warn_msg; }
                set { SetPropertyValue("purchase_line_warn_msg", ref fpurchase_line_warn_msg, value); }
            }
    
            private System.String fpurchase_line_warn;
            [Size(16)]
            [Custom("Caption", "Purchase Line warn")]
            public System.String purchase_line_warn {
                get { return fpurchase_line_warn; }
                set { SetPropertyValue("purchase_line_warn", ref fpurchase_line_warn, value); }
            }
    
            private System.String fsale_line_warn;
            [Size(16)]
            [Custom("Caption", "Sale Line warn")]
            public System.String sale_line_warn {
                get { return fsale_line_warn; }
                set { SetPropertyValue("sale_line_warn", ref fsale_line_warn, value); }
            }
    
            private System.Boolean fis_medicament;
            [Custom("Caption", "Is Medicament")]
            public System.Boolean is_medicament {
                get { return fis_medicament; }
                set { SetPropertyValue("is_medicament", ref fis_medicament, value); }
            }
    
            private System.Boolean fis_vaccine;
            [Custom("Caption", "Is Vaccine")]
            public System.Boolean is_vaccine {
                get { return fis_vaccine; }
                set { SetPropertyValue("is_vaccine", ref fis_vaccine, value); }
            }
    
            private System.Boolean fmembership;
            [Custom("Caption", "Membership")]
            public System.Boolean membership {
                get { return fmembership; }
                set { SetPropertyValue("membership", ref fmembership, value); }
            }
    
            private System.Boolean fhr_expense_ok;
            [Custom("Caption", "Hr Expense ok")]
            public System.Boolean hr_expense_ok {
                get { return fhr_expense_ok; }
                set { SetPropertyValue("hr_expense_ok", ref fhr_expense_ok, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public product_product(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

