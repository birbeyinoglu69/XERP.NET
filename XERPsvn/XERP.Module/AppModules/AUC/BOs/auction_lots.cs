
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
	[DefaultProperty("lot_local")]
    [Persistent("auction_lots")]
	public partial class auction_lots : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //auction_lots_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_auction_lots_create_uid
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
            //FK FK_auction_lots_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean fis_ok;
            [Custom("Caption", "Is Ok")]
            public System.Boolean is_ok {
                get { return fis_ok; }
                set { SetPropertyValue("is_ok", ref fis_ok, value); }
            }
    
            private System.Double fvnd_lim;
            [Custom("Caption", "Vnd Lim")]
            public System.Double vnd_lim {
                get { return fvnd_lim; }
                set { SetPropertyValue("vnd_lim", ref fvnd_lim, value); }
            }
    
            private System.Byte[] fimage;
            [Custom("Caption", "Image")]
            public System.Byte[] image {
                get { return fimage; }
                set { SetPropertyValue("image", ref fimage, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_auction_lots_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private System.Int32 flot_num;
            [Custom("Caption", "Lot Num")]
            public System.Int32 lot_num {
                get { return flot_num; }
                set { SetPropertyValue("lot_num", ref flot_num, value); }
            }
    
        
            private res_partner fach_uid;
            //FK FK_auction_lots_ach_uid
            [Custom("Caption", "Ach Uid")]
            public res_partner ach_uid {
                get { return fach_uid; }
                set { SetPropertyValue<res_partner>("ach_uid", ref fach_uid, value); }
            }
    
        
            private account_invoice fsel_inv_id;
            //FK FK_auction_lots_sel_inv_id
            [Custom("Caption", "Sel Inv id")]
            public account_invoice sel_inv_id {
                get { return fsel_inv_id; }
                set { SetPropertyValue<account_invoice>("sel_inv_id", ref fsel_inv_id, value); }
            }
    
            private System.Boolean fvnd_lim_net;
            [Custom("Caption", "Vnd Lim net")]
            public System.Boolean vnd_lim_net {
                get { return fvnd_lim_net; }
                set { SetPropertyValue("vnd_lim_net", ref fvnd_lim_net, value); }
            }
    
        
            private auction_deposit fbord_vnd_id;
            //FK FK_auction_lots_bord_vnd_id
            [Custom("Caption", "Bord Vnd id")]
            public auction_deposit bord_vnd_id {
                get { return fbord_vnd_id; }
                set { SetPropertyValue<auction_deposit>("bord_vnd_id", ref fbord_vnd_id, value); }
            }
    
            private System.Boolean fach_emp;
            [Custom("Caption", "Ach Emp")]
            public System.Boolean ach_emp {
                get { return fach_emp; }
                set { SetPropertyValue("ach_emp", ref fach_emp, value); }
            }
    
            private System.Double fnet_revenue;
            [Custom("Caption", "Net Revenue")]
            public System.Double net_revenue {
                get { return fnet_revenue; }
                set { SetPropertyValue("net_revenue", ref fnet_revenue, value); }
            }
    
        
            private auction_artists fartist2_id;
            //FK FK_auction_lots_artist2_id
            [Custom("Caption", "Artist2 Id")]
            public auction_artists artist2_id {
                get { return fartist2_id; }
                set { SetPropertyValue<auction_artists>("artist2_id", ref fartist2_id, value); }
            }
    
            private System.Boolean fobj_comm;
            [Custom("Caption", "Obj Comm")]
            public System.Boolean obj_comm {
                get { return fobj_comm; }
                set { SetPropertyValue("obj_comm", ref fobj_comm, value); }
            }
    
            private System.Boolean fpaid_ach;
            [Custom("Caption", "Paid Ach")]
            public System.Boolean paid_ach {
                get { return fpaid_ach; }
                set { SetPropertyValue("paid_ach", ref fpaid_ach, value); }
            }
    
            private System.String flot_local;
            [Size(64)]
            [Custom("Caption", "Lot Local")]
            public System.String lot_local {
                get { return flot_local; }
                set { SetPropertyValue("lot_local", ref flot_local, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private auction_dates fauction_id;
            //FK FK_auction_lots_auction_id
            [Custom("Caption", "Auction Id")]
            public auction_dates auction_id {
                get { return fauction_id; }
                set { SetPropertyValue<auction_dates>("auction_id", ref fauction_id, value); }
            }
    
        
            private auction_artists fartist_id;
            //FK FK_auction_lots_artist_id
            [Custom("Caption", "Artist Id")]
            public auction_artists artist_id {
                get { return fartist_id; }
                set { SetPropertyValue<auction_artists>("artist_id", ref fartist_id, value); }
            }
    
            private System.String fach_login;
            [Size(64)]
            [Custom("Caption", "Ach Login")]
            public System.String ach_login {
                get { return fach_login; }
                set { SetPropertyValue("ach_login", ref fach_login, value); }
            }
    
            private System.Double fgross_revenue;
            [Custom("Caption", "Gross Revenue")]
            public System.Double gross_revenue {
                get { return fgross_revenue; }
                set { SetPropertyValue("gross_revenue", ref fgross_revenue, value); }
            }
    
            private System.String flot_type;
            [Size(64)]
            [Custom("Caption", "Lot Type")]
            public System.String lot_type {
                get { return flot_type; }
                set { SetPropertyValue("lot_type", ref flot_type, value); }
            }
    
        
            private account_tax fauthor_right;
            //FK FK_auction_lots_author_right
            [Custom("Caption", "Author Right")]
            public account_tax author_right {
                get { return fauthor_right; }
                set { SetPropertyValue<account_tax>("author_right", ref fauthor_right, value); }
            }
    
            private System.Double fach_avance;
            [Custom("Caption", "Ach Avance")]
            public System.Double ach_avance {
                get { return fach_avance; }
                set { SetPropertyValue("ach_avance", ref fach_avance, value); }
            }
    
            private System.Double fgross_margin;
            [Custom("Caption", "Gross Margin")]
            public System.Double gross_margin {
                get { return fgross_margin; }
                set { SetPropertyValue("gross_margin", ref fgross_margin, value); }
            }
    
            private System.Boolean fimportant;
            [Custom("Caption", "Important")]
            public System.Boolean important {
                get { return fimportant; }
                set { SetPropertyValue("important", ref fimportant, value); }
            }
    
            private System.String fname2;
            [Size(64)]
            [Custom("Caption", "Name2")]
            public System.String name2 {
                get { return fname2; }
                set { SetPropertyValue("name2", ref fname2, value); }
            }
    
            private System.Double flot_est1;
            [Custom("Caption", "Lot Est1")]
            public System.Double lot_est1 {
                get { return flot_est1; }
                set { SetPropertyValue("lot_est1", ref flot_est1, value); }
            }
    
            private System.Double flot_est2;
            [Custom("Caption", "Lot Est2")]
            public System.Double lot_est2 {
                get { return flot_est2; }
                set { SetPropertyValue("lot_est2", ref flot_est2, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Int32 fobj_num;
            [Custom("Caption", "Obj Num")]
            public System.Int32 obj_num {
                get { return fobj_num; }
                set { SetPropertyValue("obj_num", ref fobj_num, value); }
            }
    
            private System.Double fbuyer_price;
            [Custom("Caption", "Buyer Price")]
            public System.Double buyer_price {
                get { return fbuyer_price; }
                set { SetPropertyValue("buyer_price", ref fbuyer_price, value); }
            }
    
        
            private account_invoice fach_inv_id;
            //FK FK_auction_lots_ach_inv_id
            [Custom("Caption", "Ach Inv id")]
            public account_invoice ach_inv_id {
                get { return fach_inv_id; }
                set { SetPropertyValue<account_invoice>("ach_inv_id", ref fach_inv_id, value); }
            }
    
            private System.Double fobj_price;
            [Custom("Caption", "Obj Price")]
            public System.Double obj_price {
                get { return fobj_price; }
                set { SetPropertyValue("obj_price", ref fobj_price, value); }
            }
    
            private System.Double fobj_ret;
            [Custom("Caption", "Obj Ret")]
            public System.Double obj_ret {
                get { return fobj_ret; }
                set { SetPropertyValue("obj_ret", ref fobj_ret, value); }
            }
    
            private System.Double fcosts;
            [Custom("Caption", "Costs")]
            public System.Double costs {
                get { return fcosts; }
                set { SetPropertyValue("costs", ref fcosts, value); }
            }
    
            private System.Boolean fpaid_vnd;
            [Custom("Caption", "Paid Vnd")]
            public System.Boolean paid_vnd {
                get { return fpaid_vnd; }
                set { SetPropertyValue("paid_vnd", ref fpaid_vnd, value); }
            }
    
            private System.Double fnet_margin;
            [Custom("Caption", "Net Margin")]
            public System.Double net_margin {
                get { return fnet_margin; }
                set { SetPropertyValue("net_margin", ref fnet_margin, value); }
            }
    
            private System.String fobj_desc;
            [Size(-1)]
            [Custom("Caption", "Obj Desc")]
            public System.String obj_desc {
                get { return fobj_desc; }
                set { SetPropertyValue("obj_desc", ref fobj_desc, value); }
            }
    
            private System.Double fseller_price;
            [Custom("Caption", "Seller Price")]
            public System.Double seller_price {
                get { return fseller_price; }
                set { SetPropertyValue("seller_price", ref fseller_price, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public auction_lots(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

