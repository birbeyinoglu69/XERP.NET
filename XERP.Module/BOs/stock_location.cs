
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
    [Persistent("stock_location")]
	public partial class stock_location : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //stock_location_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.Int32 fparent_left;
            [Custom("Caption", "Parent Left")]
            public System.Int32 parent_left {
                get { return fparent_left; }
                set { SetPropertyValue("parent_left", ref fparent_left, value); }
            }
    
            private System.Int32 fparent_right;
            [Custom("Caption", "Parent Right")]
            public System.Int32 parent_right {
                get { return fparent_right; }
                set { SetPropertyValue("parent_right", ref fparent_right, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_stock_location_create_uid
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
            //FK FK_stock_location_write_uid
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
    
        
            private res_partner_address faddress_id;
            //FK FK_stock_location_address_id
            [Custom("Caption", "Address Id")]
            public res_partner_address address_id {
                get { return faddress_id; }
                set { SetPropertyValue<res_partner_address>("address_id", ref faddress_id, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_stock_location_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
            private System.Int32 fchained_delay;
            [Custom("Caption", "Chained Delay")]
            public System.Int32 chained_delay {
                get { return fchained_delay; }
                set { SetPropertyValue("chained_delay", ref fchained_delay, value); }
            }
    
            private System.Int32 fposz;
            [Custom("Caption", "Posz")]
            public System.Int32 posz {
                get { return fposz; }
                set { SetPropertyValue("posz", ref fposz, value); }
            }
    
            private System.Int32 fposx;
            [Custom("Caption", "Posx")]
            public System.Int32 posx {
                get { return fposx; }
                set { SetPropertyValue("posx", ref fposx, value); }
            }
    
            private System.String fallocation_method;
            [Size(16)]
            [Custom("Caption", "Allocation Method")]
            public System.String allocation_method {
                get { return fallocation_method; }
                set { SetPropertyValue("allocation_method", ref fallocation_method, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_stock_location_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
            private System.String ficon;
            [Size(64)]
            [Custom("Caption", "Icon")]
            public System.String icon {
                get { return ficon; }
                set { SetPropertyValue("icon", ref ficon, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private stock_location fchained_location_id;
            //FK FK_stock_location_chained_location_id
            [Custom("Caption", "Chained Location id")]
            public stock_location chained_location_id {
                get { return fchained_location_id; }
                set { SetPropertyValue<stock_location>("chained_location_id", ref fchained_location_id, value); }
            }
    
            private System.Int32 fposy;
            [Custom("Caption", "Posy")]
            public System.Int32 posy {
                get { return fposy; }
                set { SetPropertyValue("posy", ref fposy, value); }
            }
    
            private System.String fchained_auto_packing;
            [Size(16)]
            [Custom("Caption", "Chained Auto packing")]
            public System.String chained_auto_packing {
                get { return fchained_auto_packing; }
                set { SetPropertyValue("chained_auto_packing", ref fchained_auto_packing, value); }
            }
    
            private System.String fusage;
            [Size(16)]
            [Custom("Caption", "Usage")]
            public System.String usage {
                get { return fusage; }
                set { SetPropertyValue("usage", ref fusage, value); }
            }
    
            private System.String fchained_location_type;
            [Size(16)]
            [Custom("Caption", "Chained Location type")]
            public System.String chained_location_type {
                get { return fchained_location_type; }
                set { SetPropertyValue("chained_location_type", ref fchained_location_type, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public stock_location(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

