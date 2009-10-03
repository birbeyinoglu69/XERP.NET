
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
    [Persistent("board_board_line")]
	public partial class board_board_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //board_board_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_board_board_line_create_uid
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
            //FK FK_board_board_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private board_board fboard_id;
            //FK FK_board_board_line_board_id
            [Custom("Caption", "Board Id")]
            public board_board board_id {
                get { return fboard_id; }
                set { SetPropertyValue<board_board>("board_id", ref fboard_id, value); }
            }
    
            private System.Int32 fwidth;
            [Custom("Caption", "Width")]
            public System.Int32 width {
                get { return fwidth; }
                set { SetPropertyValue("width", ref fwidth, value); }
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
    
            private System.String fposition;
            [Size(16)]
            [Custom("Caption", "Position")]
            public System.String position {
                get { return fposition; }
                set { SetPropertyValue("position", ref fposition, value); }
            }
    
        
            private ir_act_window faction_id;
            //FK FK_board_board_line_action_id
            [Custom("Caption", "Action Id")]
            public ir_act_window action_id {
                get { return faction_id; }
                set { SetPropertyValue<ir_act_window>("action_id", ref faction_id, value); }
            }
    
            private System.Int32 fheight;
            [Custom("Caption", "Height")]
            public System.Int32 height {
                get { return fheight; }
                set { SetPropertyValue("height", ref fheight, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public board_board_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

