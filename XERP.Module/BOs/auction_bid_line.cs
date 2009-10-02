
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
    [Persistent("auction_bid_line")]
	public partial class auction_bid_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //auction_bid_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_auction_bid_line_create_uid
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
            //FK FK_auction_bid_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean fcall;
            [Custom("Caption", "Call")]
            public System.Boolean call {
                get { return fcall; }
                set { SetPropertyValue("call", ref fcall, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fauction;
            [Size(64)]
            [Custom("Caption", "Auction")]
            public System.String auction {
                get { return fauction; }
                set { SetPropertyValue("auction", ref fauction, value); }
            }
    
        
            private auction_lots flot_id;
            //FK FK_auction_bid_line_lot_id
            [Custom("Caption", "Lot Id")]
            public auction_lots lot_id {
                get { return flot_id; }
                set { SetPropertyValue<auction_lots>("lot_id", ref flot_id, value); }
            }
    
            private System.Double fprice;
            [Custom("Caption", "Price")]
            public System.Double price {
                get { return fprice; }
                set { SetPropertyValue("price", ref fprice, value); }
            }
    
        
            private auction_bid fbid_id;
            //FK FK_auction_bid_line_bid_id
            [Custom("Caption", "Bid Id")]
            public auction_bid bid_id {
                get { return fbid_id; }
                set { SetPropertyValue<auction_bid>("bid_id", ref fbid_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public auction_bid_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

