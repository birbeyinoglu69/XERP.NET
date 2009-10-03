
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
    [Persistent("product_pricelist_item")]
	public partial class product_pricelist_item : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //product_pricelist_item_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_product_pricelist_item_create_uid
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
            //FK FK_product_pricelist_item_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Decimal fprice_round;
            [Custom("Caption", "Price Round")]
            public System.Decimal price_round {
                get { return fprice_round; }
                set { SetPropertyValue("price_round", ref fprice_round, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private product_pricelist fbase_pricelist_id;
            //FK FK_product_pricelist_item_base_pricelist_id
            [Custom("Caption", "Base Pricelist id")]
            public product_pricelist base_pricelist_id {
                get { return fbase_pricelist_id; }
                set { SetPropertyValue<product_pricelist>("base_pricelist_id", ref fbase_pricelist_id, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.Decimal fprice_max_margin;
            [Custom("Caption", "Price Max margin")]
            public System.Decimal price_max_margin {
                get { return fprice_max_margin; }
                set { SetPropertyValue("price_max_margin", ref fprice_max_margin, value); }
            }
    
        
            private product_template fproduct_tmpl_id;
            //FK FK_product_pricelist_item_product_tmpl_id
            [Custom("Caption", "Product Tmpl id")]
            public product_template product_tmpl_id {
                get { return fproduct_tmpl_id; }
                set { SetPropertyValue<product_template>("product_tmpl_id", ref fproduct_tmpl_id, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_product_pricelist_item_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private System.Int32 fbase1;
            [Custom("Caption", "Base")]
            public System.Int32 base1 {
                get { return fbase1; }
                set { SetPropertyValue("base1", ref fbase1, value); }
            }
    
        
            private product_pricelist_version fprice_version_id;
            //FK FK_product_pricelist_item_price_version_id
            [Custom("Caption", "Price Version id")]
            public product_pricelist_version price_version_id {
                get { return fprice_version_id; }
                set { SetPropertyValue<product_pricelist_version>("price_version_id", ref fprice_version_id, value); }
            }
    
            private System.Int32 fmin_quantity;
            [Custom("Caption", "Min Quantity")]
            public System.Int32 min_quantity {
                get { return fmin_quantity; }
                set { SetPropertyValue("min_quantity", ref fmin_quantity, value); }
            }
    
            private System.Decimal fprice_surcharge;
            [Custom("Caption", "Price Surcharge")]
            public System.Decimal price_surcharge {
                get { return fprice_surcharge; }
                set { SetPropertyValue("price_surcharge", ref fprice_surcharge, value); }
            }
    
            private System.Decimal fprice_min_margin;
            [Custom("Caption", "Price Min margin")]
            public System.Decimal price_min_margin {
                get { return fprice_min_margin; }
                set { SetPropertyValue("price_min_margin", ref fprice_min_margin, value); }
            }
    
        
            private product_category fcateg_id;
            //FK FK_product_pricelist_item_categ_id
            [Custom("Caption", "Categ Id")]
            public product_category categ_id {
                get { return fcateg_id; }
                set { SetPropertyValue<product_category>("categ_id", ref fcateg_id, value); }
            }
    
            private System.Decimal fprice_discount;
            [Custom("Caption", "Price Discount")]
            public System.Decimal price_discount {
                get { return fprice_discount; }
                set { SetPropertyValue("price_discount", ref fprice_discount, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public product_pricelist_item(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

