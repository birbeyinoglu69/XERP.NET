
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
	[DefaultProperty("supply_method")]
    [Persistent("product_template")]
	public partial class product_template : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //product_template_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_product_template_create_uid
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
            //FK FK_product_template_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Double fwarranty;
            [Custom("Caption", "Warranty")]
            public System.Double warranty {
                get { return fwarranty; }
                set { SetPropertyValue("warranty", ref fwarranty, value); }
            }
    
            private System.String fsupply_method;
            [Size(16)]
            [Custom("Caption", "Supply Method")]
            public System.String supply_method {
                get { return fsupply_method; }
                set { SetPropertyValue("supply_method", ref fsupply_method, value); }
            }
    
        
            private product_uom fuos_id;
            //FK FK_product_template_uos_id
            [Custom("Caption", "Uos Id")]
            public product_uom uos_id {
                get { return fuos_id; }
                set { SetPropertyValue<product_uom>("uos_id", ref fuos_id, value); }
            }
    
            private System.Decimal flist_price;
            [Custom("Caption", "List Price")]
            public System.Decimal list_price {
                get { return flist_price; }
                set { SetPropertyValue("list_price", ref flist_price, value); }
            }
    
            private System.Double fweight;
            [Custom("Caption", "Weight")]
            public System.Double weight {
                get { return fweight; }
                set { SetPropertyValue("weight", ref fweight, value); }
            }
    
            private System.Decimal fstandard_price;
            [Custom("Caption", "Standard Price")]
            public System.Decimal standard_price {
                get { return fstandard_price; }
                set { SetPropertyValue("standard_price", ref fstandard_price, value); }
            }
    
            private System.Boolean fpurchase_ok;
            [Custom("Caption", "Purchase Ok")]
            public System.Boolean purchase_ok {
                get { return fpurchase_ok; }
                set { SetPropertyValue("purchase_ok", ref fpurchase_ok, value); }
            }
    
            private System.String fmes_type;
            [Size(16)]
            [Custom("Caption", "Mes Type")]
            public System.String mes_type {
                get { return fmes_type; }
                set { SetPropertyValue("mes_type", ref fmes_type, value); }
            }
    
        
            private product_uom fuom_id;
            //FK FK_product_template_uom_id
            [Custom("Caption", "Uom Id")]
            public product_uom uom_id {
                get { return fuom_id; }
                set { SetPropertyValue<product_uom>("uom_id", ref fuom_id, value); }
            }
    
            private System.String fdescription_purchase;
            [Size(-1)]
            [Custom("Caption", "Description Purchase")]
            public System.String description_purchase {
                get { return fdescription_purchase; }
                set { SetPropertyValue("description_purchase", ref fdescription_purchase, value); }
            }
    
            private System.Decimal fuos_coeff;
            [Custom("Caption", "Uos Coeff")]
            public System.Decimal uos_coeff {
                get { return fuos_coeff; }
                set { SetPropertyValue("uos_coeff", ref fuos_coeff, value); }
            }
    
            private System.Boolean fsale_ok;
            [Custom("Caption", "Sale Ok")]
            public System.Boolean sale_ok {
                get { return fsale_ok; }
                set { SetPropertyValue("sale_ok", ref fsale_ok, value); }
            }
    
            private System.String floc_row;
            [Size(16)]
            [Custom("Caption", "Loc Row")]
            public System.String loc_row {
                get { return floc_row; }
                set { SetPropertyValue("loc_row", ref floc_row, value); }
            }
    
        
            private res_users fproduct_manager;
            //FK FK_product_template_product_manager
            [Custom("Caption", "Product Manager")]
            public res_users product_manager {
                get { return fproduct_manager; }
                set { SetPropertyValue<res_users>("product_manager", ref fproduct_manager, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_product_template_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String floc_rack;
            [Size(16)]
            [Custom("Caption", "Loc Rack")]
            public System.String loc_rack {
                get { return floc_rack; }
                set { SetPropertyValue("loc_rack", ref floc_rack, value); }
            }
    
        
            private product_uom fuom_po_id;
            //FK FK_product_template_uom_po_id
            [Custom("Caption", "Uom Po id")]
            public product_uom uom_po_id {
                get { return fuom_po_id; }
                set { SetPropertyValue<product_uom>("uom_po_id", ref fuom_po_id, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private System.Double fweight_net;
            [Custom("Caption", "Weight Net")]
            public System.Double weight_net {
                get { return fweight_net; }
                set { SetPropertyValue("weight_net", ref fweight_net, value); }
            }
    
            private System.Double fvolume;
            [Custom("Caption", "Volume")]
            public System.Double volume {
                get { return fvolume; }
                set { SetPropertyValue("volume", ref fvolume, value); }
            }
    
            private System.String fprocure_method;
            [Size(16)]
            [Custom("Caption", "Procure Method")]
            public System.String procure_method {
                get { return fprocure_method; }
                set { SetPropertyValue("procure_method", ref fprocure_method, value); }
            }
    
            private System.String fcost_method;
            [Size(16)]
            [Custom("Caption", "Cost Method")]
            public System.String cost_method {
                get { return fcost_method; }
                set { SetPropertyValue("cost_method", ref fcost_method, value); }
            }
    
        
            private product_category fcateg_id;
            //FK FK_product_template_categ_id
            [Custom("Caption", "Categ Id")]
            public product_category categ_id {
                get { return fcateg_id; }
                set { SetPropertyValue<product_category>("categ_id", ref fcateg_id, value); }
            }
    
            private System.Boolean frental;
            [Custom("Caption", "Rental")]
            public System.Boolean rental {
                get { return frental; }
                set { SetPropertyValue("rental", ref frental, value); }
            }
    
            private System.Double fsale_delay;
            [Custom("Caption", "Sale Delay")]
            public System.Double sale_delay {
                get { return fsale_delay; }
                set { SetPropertyValue("sale_delay", ref fsale_delay, value); }
            }
    
            private System.String floc_case;
            [Size(16)]
            [Custom("Caption", "Loc Case")]
            public System.String loc_case {
                get { return floc_case; }
                set { SetPropertyValue("loc_case", ref floc_case, value); }
            }
    
            private System.String fdescription_sale;
            [Size(-1)]
            [Custom("Caption", "Description Sale")]
            public System.String description_sale {
                get { return fdescription_sale; }
                set { SetPropertyValue("description_sale", ref fdescription_sale, value); }
            }
    
            private System.Double fproduce_delay;
            [Custom("Caption", "Produce Delay")]
            public System.Double produce_delay {
                get { return fproduce_delay; }
                set { SetPropertyValue("produce_delay", ref fproduce_delay, value); }
            }
    
            private System.Decimal fmember_price;
            [Custom("Caption", "Member Price")]
            public System.Decimal member_price {
                get { return fmember_price; }
                set { SetPropertyValue("member_price", ref fmember_price, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public product_template(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

