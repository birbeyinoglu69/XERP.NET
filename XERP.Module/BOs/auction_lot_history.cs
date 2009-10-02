
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
    [Persistent("auction_lot_history")]
	public partial class auction_lot_history : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //auction_lot_history_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_auction_lot_history_create_uid
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
            //FK FK_auction_lot_history_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private auction_lots flot_id;
            //FK FK_auction_lot_history_lot_id
            [Custom("Caption", "Lot Id")]
            public auction_lots lot_id {
                get { return flot_id; }
                set { SetPropertyValue<auction_lots>("lot_id", ref flot_id, value); }
            }
    
            private System.Decimal fprice;
            [Custom("Caption", "Price")]
            public System.Decimal price {
                get { return fprice; }
                set { SetPropertyValue("price", ref fprice, value); }
            }
    
        
            private auction_dates fauction_id;
            //FK FK_auction_lot_history_auction_id
            [Custom("Caption", "Auction Id")]
            public auction_dates auction_id {
                get { return fauction_id; }
                set { SetPropertyValue<auction_dates>("auction_id", ref fauction_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public auction_lot_history(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

