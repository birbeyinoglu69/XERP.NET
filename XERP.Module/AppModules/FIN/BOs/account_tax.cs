
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
	[DefaultProperty("domain")]
    [Persistent("account_tax")]
	public partial class account_tax : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_tax_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_tax_create_uid
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
            //FK FK_account_tax_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_tax_code fref_base_code_id;
            //FK FK_account_tax_ref_base_code_id
            [Custom("Caption", "Ref Base code id")]
            public account_tax_code ref_base_code_id {
                get { return fref_base_code_id; }
                set { SetPropertyValue<account_tax_code>("ref_base_code_id", ref fref_base_code_id, value); }
            }
    
            private System.String fdomain;
            [Size(32)]
            [Custom("Caption", "Domain")]
            public System.String domain {
                get { return fdomain; }
                set { SetPropertyValue("domain", ref fdomain, value); }
            }
    
            private System.String fdescription;
            [Size(32)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
        
            private account_tax_code fref_tax_code_id;
            //FK FK_account_tax_ref_tax_code_id
            [Custom("Caption", "Ref Tax code id")]
            public account_tax_code ref_tax_code_id {
                get { return fref_tax_code_id; }
                set { SetPropertyValue<account_tax_code>("ref_tax_code_id", ref fref_tax_code_id, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private account_account faccount_paid_id;
            //FK FK_account_tax_account_paid_id
            [Custom("Caption", "Account Paid id")]
            public account_account account_paid_id {
                get { return faccount_paid_id; }
                set { SetPropertyValue<account_account>("account_paid_id", ref faccount_paid_id, value); }
            }
    
            private System.String ftax_group;
            [Size(16)]
            [Custom("Caption", "Tax Group")]
            public System.String tax_group {
                get { return ftax_group; }
                set { SetPropertyValue("tax_group", ref ftax_group, value); }
            }
    
            private System.Double fref_base_sign;
            [Custom("Caption", "Ref Base sign")]
            public System.Double ref_base_sign {
                get { return fref_base_sign; }
                set { SetPropertyValue("ref_base_sign", ref fref_base_sign, value); }
            }
    
            private System.String ftype_tax_use;
            [Size(16)]
            [Custom("Caption", "Type Tax use")]
            public System.String type_tax_use {
                get { return ftype_tax_use; }
                set { SetPropertyValue("type_tax_use", ref ftype_tax_use, value); }
            }
    
        
            private account_tax_code fbase_code_id;
            //FK FK_account_tax_base_code_id
            [Custom("Caption", "Base Code id")]
            public account_tax_code base_code_id {
                get { return fbase_code_id; }
                set { SetPropertyValue<account_tax_code>("base_code_id", ref fbase_code_id, value); }
            }
    
            private System.Double fbase_sign;
            [Custom("Caption", "Base Sign")]
            public System.Double base_sign {
                get { return fbase_sign; }
                set { SetPropertyValue("base_sign", ref fbase_sign, value); }
            }
    
            private System.Boolean fchild_depend;
            [Custom("Caption", "Child Depend")]
            public System.Boolean child_depend {
                get { return fchild_depend; }
                set { SetPropertyValue("child_depend", ref fchild_depend, value); }
            }
    
            private System.Boolean finclude_base_amount;
            [Custom("Caption", "Include Base amount")]
            public System.Boolean include_base_amount {
                get { return finclude_base_amount; }
                set { SetPropertyValue("include_base_amount", ref finclude_base_amount, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Double fref_tax_sign;
            [Custom("Caption", "Ref Tax sign")]
            public System.Double ref_tax_sign {
                get { return fref_tax_sign; }
                set { SetPropertyValue("ref_tax_sign", ref fref_tax_sign, value); }
            }
    
            private System.String fapplicable_type;
            [Size(16)]
            [Custom("Caption", "Applicable Type")]
            public System.String applicable_type {
                get { return fapplicable_type; }
                set { SetPropertyValue("applicable_type", ref fapplicable_type, value); }
            }
    
        
            private account_account faccount_collected_id;
            //FK FK_account_tax_account_collected_id
            [Custom("Caption", "Account Collected id")]
            public account_account account_collected_id {
                get { return faccount_collected_id; }
                set { SetPropertyValue<account_account>("account_collected_id", ref faccount_collected_id, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_account_tax_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private account_tax_code ftax_code_id;
            //FK FK_account_tax_tax_code_id
            [Custom("Caption", "Tax Code id")]
            public account_tax_code tax_code_id {
                get { return ftax_code_id; }
                set { SetPropertyValue<account_tax_code>("tax_code_id", ref ftax_code_id, value); }
            }
    
        
            private account_tax fparent_id;
            //FK FK_account_tax_parent_id
            [Custom("Caption", "Parent Id")]
            public account_tax parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<account_tax>("parent_id", ref fparent_id, value); }
            }
    
            private System.Decimal famount;
            [Custom("Caption", "Amount")]
            public System.Decimal amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
            }
    
            private System.String fpython_compute;
            [Size(-1)]
            [Custom("Caption", "Python Compute")]
            public System.String python_compute {
                get { return fpython_compute; }
                set { SetPropertyValue("python_compute", ref fpython_compute, value); }
            }
    
            private System.Double ftax_sign;
            [Custom("Caption", "Tax Sign")]
            public System.Double tax_sign {
                get { return ftax_sign; }
                set { SetPropertyValue("tax_sign", ref ftax_sign, value); }
            }
    
            private System.String fpython_compute_inv;
            [Size(-1)]
            [Custom("Caption", "Python Compute inv")]
            public System.String python_compute_inv {
                get { return fpython_compute_inv; }
                set { SetPropertyValue("python_compute_inv", ref fpython_compute_inv, value); }
            }
    
            private System.String fpython_applicable;
            [Size(-1)]
            [Custom("Caption", "Python Applicable")]
            public System.String python_applicable {
                get { return fpython_applicable; }
                set { SetPropertyValue("python_applicable", ref fpython_applicable, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.Boolean fprice_include;
            [Custom("Caption", "Price Include")]
            public System.Boolean price_include {
                get { return fprice_include; }
                set { SetPropertyValue("price_include", ref fprice_include, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_tax(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

