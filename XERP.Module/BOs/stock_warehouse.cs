
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
    [Persistent("stock_warehouse")]
	public partial class stock_warehouse : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //stock_warehouse_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_stock_warehouse_create_uid
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
            //FK FK_stock_warehouse_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private stock_location flot_input_id;
            //FK FK_stock_warehouse_lot_input_id
            [Custom("Caption", "Lot Input id")]
            public stock_location lot_input_id {
                get { return flot_input_id; }
                set { SetPropertyValue<stock_location>("lot_input_id", ref flot_input_id, value); }
            }
    
        
            private res_partner_address fpartner_address_id;
            //FK FK_stock_warehouse_partner_address_id
            [Custom("Caption", "Partner Address id")]
            public res_partner_address partner_address_id {
                get { return fpartner_address_id; }
                set { SetPropertyValue<res_partner_address>("partner_address_id", ref fpartner_address_id, value); }
            }
    
        
            private stock_location flot_output_id;
            //FK FK_stock_warehouse_lot_output_id
            [Custom("Caption", "Lot Output id")]
            public stock_location lot_output_id {
                get { return flot_output_id; }
                set { SetPropertyValue<stock_location>("lot_output_id", ref flot_output_id, value); }
            }
    
            private System.String fname;
            [Size(60)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private stock_location flot_stock_id;
            //FK FK_stock_warehouse_lot_stock_id
            [Custom("Caption", "Lot Stock id")]
            public stock_location lot_stock_id {
                get { return flot_stock_id; }
                set { SetPropertyValue<stock_location>("lot_stock_id", ref flot_stock_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public stock_warehouse(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

