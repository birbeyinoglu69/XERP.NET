
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
    [Persistent("fiscalyear_seq")]
	public partial class fiscalyear_seq : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //fiscalyear_seq_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_fiscalyear_seq_create_uid
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
            //FK FK_fiscalyear_seq_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_fiscalyear ffiscalyear_id;
            //FK FK_fiscalyear_seq_fiscalyear_id
            [Custom("Caption", "Fiscalyear Id")]
            public account_fiscalyear fiscalyear_id {
                get { return ffiscalyear_id; }
                set { SetPropertyValue<account_fiscalyear>("fiscalyear_id", ref ffiscalyear_id, value); }
            }
    
        
            private ir_sequence fsequence_id;
            //FK FK_fiscalyear_seq_sequence_id
            [Custom("Caption", "Sequence Id")]
            public ir_sequence sequence_id {
                get { return fsequence_id; }
                set { SetPropertyValue<ir_sequence>("sequence_id", ref fsequence_id, value); }
            }
    
        
            private account_journal fjournal_id;
            //FK FK_fiscalyear_seq_journal_id
            [Custom("Caption", "Journal Id")]
            public account_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_journal>("journal_id", ref fjournal_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public fiscalyear_seq(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

