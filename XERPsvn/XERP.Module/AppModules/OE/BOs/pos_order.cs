
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
	[DefaultProperty("note")]
    [Persistent("pos_order")]
	public partial class pos_order : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //pos_order_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_pos_order_create_uid
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
            //FK FK_pos_order_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_users fsalesman_id;
            //FK FK_pos_order_salesman_id
            [Custom("Caption", "Salesman Id")]
            public res_users salesman_id {
                get { return fsalesman_id; }
                set { SetPropertyValue<res_users>("salesman_id", ref fsalesman_id, value); }
            }
    
        
            private account_journal fsale_journal;
            //FK FK_pos_order_sale_journal
            [Custom("Caption", "Sale Journal")]
            public account_journal sale_journal {
                get { return fsale_journal; }
                set { SetPropertyValue<account_journal>("sale_journal", ref fsale_journal, value); }
            }
    
        
            private account_account faccount_receivable;
            //FK FK_pos_order_account_receivable
            [Custom("Caption", "Account Receivable")]
            public account_account account_receivable {
                get { return faccount_receivable; }
                set { SetPropertyValue<account_account>("account_receivable", ref faccount_receivable, value); }
            }
    
        
            private account_move faccount_move;
            //FK FK_pos_order_account_move
            [Custom("Caption", "Account Move")]
            public account_move account_move {
                get { return faccount_move; }
                set { SetPropertyValue<account_move>("account_move", ref faccount_move, value); }
            }
    
            private DateTime? fdate_order;
            [Custom("Caption", "Date Order")]
            public DateTime? date_order {
                get { return fdate_order; }
                set { SetPropertyValue("date_order", ref fdate_order, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_pos_order_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private stock_picking flast_out_picking;
            //FK FK_pos_order_last_out_picking
            [Custom("Caption", "Last Out picking")]
            public stock_picking last_out_picking {
                get { return flast_out_picking; }
                set { SetPropertyValue<stock_picking>("last_out_picking", ref flast_out_picking, value); }
            }
    
            private System.Int32 fnb_print;
            [Custom("Caption", "Nb Print")]
            public System.Int32 nb_print {
                get { return fnb_print; }
                set { SetPropertyValue("nb_print", ref fnb_print, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_pos_order_user_id
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
    
        
            private account_invoice finvoice_id;
            //FK FK_pos_order_invoice_id
            [Custom("Caption", "Invoice Id")]
            public account_invoice invoice_id {
                get { return finvoice_id; }
                set { SetPropertyValue<account_invoice>("invoice_id", ref finvoice_id, value); }
            }
    
            private System.Boolean finvoice_wanted;
            [Custom("Caption", "Invoice Wanted")]
            public System.Boolean invoice_wanted {
                get { return finvoice_wanted; }
                set { SetPropertyValue("invoice_wanted", ref finvoice_wanted, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private sale_shop fshop_id;
            //FK FK_pos_order_shop_id
            [Custom("Caption", "Shop Id")]
            public sale_shop shop_id {
                get { return fshop_id; }
                set { SetPropertyValue<sale_shop>("shop_id", ref fshop_id, value); }
            }
    
        
            private product_pricelist fpricelist_id;
            //FK FK_pos_order_pricelist_id
            [Custom("Caption", "Pricelist Id")]
            public product_pricelist pricelist_id {
                get { return fpricelist_id; }
                set { SetPropertyValue<product_pricelist>("pricelist_id", ref fpricelist_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public pos_order(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

