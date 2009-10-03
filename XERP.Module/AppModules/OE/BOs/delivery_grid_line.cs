
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
	[DefaultProperty("name")]
    [Persistent("delivery_grid_line")]
	public partial class delivery_grid_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //delivery_grid_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_delivery_grid_line_create_uid
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
            //FK FK_delivery_grid_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Double flist_price;
            [Custom("Caption", "List Price")]
            public System.Double list_price {
                get { return flist_price; }
                set { SetPropertyValue("list_price", ref flist_price, value); }
            }
    
            private System.String fname;
            [Size(32)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fprice_type;
            [Size(16)]
            [Custom("Caption", "Price Type")]
            public System.String price_type {
                get { return fprice_type; }
                set { SetPropertyValue("price_type", ref fprice_type, value); }
            }
    
            private System.Double fmax_value;
            [Custom("Caption", "Max Value")]
            public System.Double max_value {
                get { return fmax_value; }
                set { SetPropertyValue("max_value", ref fmax_value, value); }
            }
    
            private System.Double fstandard_price;
            [Custom("Caption", "Standard Price")]
            public System.Double standard_price {
                get { return fstandard_price; }
                set { SetPropertyValue("standard_price", ref fstandard_price, value); }
            }
    
        
            private delivery_grid fgrid_id;
            //FK FK_delivery_grid_line_grid_id
            [Custom("Caption", "Grid Id")]
            public delivery_grid grid_id {
                get { return fgrid_id; }
                set { SetPropertyValue<delivery_grid>("grid_id", ref fgrid_id, value); }
            }
    
            private System.String fvariable_factor;
            [Size(16)]
            [Custom("Caption", "Variable Factor")]
            public System.String variable_factor {
                get { return fvariable_factor; }
                set { SetPropertyValue("variable_factor", ref fvariable_factor, value); }
            }
    
            private System.String foperator1;
            [Size(16)]
            [Custom("Caption", "Operator")]
            public System.String operator1 {
                get { return foperator1; }
                set { SetPropertyValue("operator1", ref foperator1, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public delivery_grid_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

