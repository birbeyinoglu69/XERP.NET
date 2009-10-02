
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
    [Persistent("mrp_routing_workcenter")]
	public partial class mrp_routing_workcenter : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_routing_workcenter_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_routing_workcenter_create_uid
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
            //FK FK_mrp_routing_workcenter_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
            private System.Double fcycle_nbr;
            [Custom("Caption", "Cycle Nbr")]
            public System.Double cycle_nbr {
                get { return fcycle_nbr; }
                set { SetPropertyValue("cycle_nbr", ref fcycle_nbr, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private mrp_routing frouting_id;
            //FK FK_mrp_routing_workcenter_routing_id
            [Custom("Caption", "Routing Id")]
            public mrp_routing routing_id {
                get { return frouting_id; }
                set { SetPropertyValue<mrp_routing>("routing_id", ref frouting_id, value); }
            }
    
        
            private mrp_workcenter fworkcenter_id;
            //FK FK_mrp_routing_workcenter_workcenter_id
            [Custom("Caption", "Workcenter Id")]
            public mrp_workcenter workcenter_id {
                get { return fworkcenter_id; }
                set { SetPropertyValue<mrp_workcenter>("workcenter_id", ref fworkcenter_id, value); }
            }
    
            private System.Double fhour_nbr;
            [Custom("Caption", "Hour Nbr")]
            public System.Double hour_nbr {
                get { return fhour_nbr; }
                set { SetPropertyValue("hour_nbr", ref fhour_nbr, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_routing_workcenter(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

