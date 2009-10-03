
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
	[DefaultProperty("sexual_partners")]
    [Persistent("medical_patient")]
	public partial class medical_patient : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_patient_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_patient_create_uid
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
            //FK FK_medical_patient_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean fsecond_hand_smoker;
            [Custom("Caption", "Second Hand smoker")]
            public System.Boolean second_hand_smoker {
                get { return fsecond_hand_smoker; }
                set { SetPropertyValue("second_hand_smoker", ref fsecond_hand_smoker, value); }
            }
    
            private System.Boolean ffertile;
            [Custom("Caption", "Fertile")]
            public System.Boolean fertile {
                get { return ffertile; }
                set { SetPropertyValue("fertile", ref ffertile, value); }
            }
    
            private System.Boolean fprison_current;
            [Custom("Caption", "Prison Current")]
            public System.Boolean prison_current {
                get { return fprison_current; }
                set { SetPropertyValue("prison_current", ref fprison_current, value); }
            }
    
            private System.Int32 fdeaths_2nd_week;
            [Custom("Caption", "Deaths 2nd week")]
            public System.Int32 deaths_2nd_week {
                get { return fdeaths_2nd_week; }
                set { SetPropertyValue("deaths_2nd_week", ref fdeaths_2nd_week, value); }
            }
    
            private System.Byte[] fphoto;
            [Custom("Caption", "Photo")]
            public System.Byte[] photo {
                get { return fphoto; }
                set { SetPropertyValue("photo", ref fphoto, value); }
            }
    
            private System.Int32 ffull_term;
            [Custom("Caption", "Full Term")]
            public System.Int32 full_term {
                get { return ffull_term; }
                set { SetPropertyValue("full_term", ref ffull_term, value); }
            }
    
            private System.Boolean fhostile_area;
            [Custom("Caption", "Hostile Area")]
            public System.Boolean hostile_area {
                get { return fhostile_area; }
                set { SetPropertyValue("hostile_area", ref fhostile_area, value); }
            }
    
            private System.Boolean fcurrently_pregnant;
            [Custom("Caption", "Currently Pregnant")]
            public System.Boolean currently_pregnant {
                get { return fcurrently_pregnant; }
                set { SetPropertyValue("currently_pregnant", ref fcurrently_pregnant, value); }
            }
    
            private System.Boolean fsexual_abuse;
            [Custom("Caption", "Sexual Abuse")]
            public System.Boolean sexual_abuse {
                get { return fsexual_abuse; }
                set { SetPropertyValue("sexual_abuse", ref fsexual_abuse, value); }
            }
    
            private System.Boolean fsewers;
            [Custom("Caption", "Sewers")]
            public System.Boolean sewers {
                get { return fsewers; }
                set { SetPropertyValue("sewers", ref fsewers, value); }
            }
    
            private System.Int32 fsmoking_number;
            [Custom("Caption", "Smoking Number")]
            public System.Int32 smoking_number {
                get { return fsmoking_number; }
                set { SetPropertyValue("smoking_number", ref fsmoking_number, value); }
            }
    
            private System.String fsexual_partners;
            [Size(16)]
            [Custom("Caption", "Sexual Partners")]
            public System.String sexual_partners {
                get { return fsexual_partners; }
                set { SetPropertyValue("sexual_partners", ref fsexual_partners, value); }
            }
    
        
            private medical_occupation foccupation;
            //FK FK_medical_patient_occupation
            [Custom("Caption", "Occupation")]
            public medical_occupation occupation {
                get { return foccupation; }
                set { SetPropertyValue<medical_occupation>("occupation", ref foccupation, value); }
            }
    
            private System.Int32 fborn_alive;
            [Custom("Caption", "Born Alive")]
            public System.Int32 born_alive {
                get { return fborn_alive; }
                set { SetPropertyValue("born_alive", ref fborn_alive, value); }
            }
    
            private System.String flifestyle_info;
            [Size(-1)]
            [Custom("Caption", "Lifestyle Info")]
            public System.String lifestyle_info {
                get { return flifestyle_info; }
                set { SetPropertyValue("lifestyle_info", ref flifestyle_info, value); }
            }
    
            private System.Int32 fabortions;
            [Custom("Caption", "Abortions")]
            public System.Int32 abortions {
                get { return fabortions; }
                set { SetPropertyValue("abortions", ref fabortions, value); }
            }
    
            private System.Boolean felectricity;
            [Custom("Caption", "Electricity")]
            public System.Boolean electricity {
                get { return felectricity; }
                set { SetPropertyValue("electricity", ref felectricity, value); }
            }
    
            private System.Boolean fexcercise;
            [Custom("Caption", "Excercise")]
            public System.Boolean excercise {
                get { return fexcercise; }
                set { SetPropertyValue("excercise", ref fexcercise, value); }
            }
    
            private System.String fgpa;
            [Size(32)]
            [Custom("Caption", "Gpa")]
            public System.String gpa {
                get { return fgpa; }
                set { SetPropertyValue("gpa", ref fgpa, value); }
            }
    
            private System.Boolean fmenopausal;
            [Custom("Caption", "Menopausal")]
            public System.Boolean menopausal {
                get { return fmenopausal; }
                set { SetPropertyValue("menopausal", ref fmenopausal, value); }
            }
    
            private System.Boolean fsoft_drinks;
            [Custom("Caption", "Soft Drinks")]
            public System.Boolean soft_drinks {
                get { return fsoft_drinks; }
                set { SetPropertyValue("soft_drinks", ref fsoft_drinks, value); }
            }
    
            private System.String fblood_type;
            [Size(16)]
            [Custom("Caption", "Blood Type")]
            public System.String blood_type {
                get { return fblood_type; }
                set { SetPropertyValue("blood_type", ref fblood_type, value); }
            }
    
            private System.Boolean fsingle_parent;
            [Custom("Caption", "Single Parent")]
            public System.Boolean single_parent {
                get { return fsingle_parent; }
                set { SetPropertyValue("single_parent", ref fsingle_parent, value); }
            }
    
        
            private medical_family_code ffamily_code;
            //FK FK_medical_patient_family_code
            [Custom("Caption", "Family Code")]
            public medical_family_code family_code {
                get { return ffamily_code; }
                set { SetPropertyValue<medical_family_code>("family_code", ref ffamily_code, value); }
            }
    
            private System.Int32 falcohol_beer_number;
            [Custom("Caption", "Alcohol Beer number")]
            public System.Int32 alcohol_beer_number {
                get { return falcohol_beer_number; }
                set { SetPropertyValue("alcohol_beer_number", ref falcohol_beer_number, value); }
            }
    
            private System.Boolean fworking_children;
            [Custom("Caption", "Working Children")]
            public System.Boolean working_children {
                get { return fworking_children; }
                set { SetPropertyValue("working_children", ref fworking_children, value); }
            }
    
            private System.String fincome;
            [Size(16)]
            [Custom("Caption", "Income")]
            public System.String income {
                get { return fincome; }
                set { SetPropertyValue("income", ref fincome, value); }
            }
    
            private System.Boolean fgas;
            [Custom("Caption", "Gas")]
            public System.Boolean gas {
                get { return fgas; }
                set { SetPropertyValue("gas", ref fgas, value); }
            }
    
            private System.Boolean ftelevision;
            [Custom("Caption", "Television")]
            public System.Boolean television {
                get { return ftelevision; }
                set { SetPropertyValue("television", ref ftelevision, value); }
            }
    
            private System.Boolean fprison_past;
            [Custom("Caption", "Prison Past")]
            public System.Boolean prison_past {
                get { return fprison_past; }
                set { SetPropertyValue("prison_past", ref fprison_past, value); }
            }
    
            private System.Boolean fdomestic_violence;
            [Custom("Caption", "Domestic Violence")]
            public System.Boolean domestic_violence {
                get { return fdomestic_violence; }
                set { SetPropertyValue("domestic_violence", ref fdomestic_violence, value); }
            }
    
            private System.Int32 ffirst_sexual_encounter;
            [Custom("Caption", "First Sexual encounter")]
            public System.Int32 first_sexual_encounter {
                get { return ffirst_sexual_encounter; }
                set { SetPropertyValue("first_sexual_encounter", ref ffirst_sexual_encounter, value); }
            }
    
            private System.String fsexual_practices;
            [Size(16)]
            [Custom("Caption", "Sexual Practices")]
            public System.String sexual_practices {
                get { return fsexual_practices; }
                set { SetPropertyValue("sexual_practices", ref fsexual_practices, value); }
            }
    
            private System.Boolean fsmoking;
            [Custom("Caption", "Smoking")]
            public System.Boolean smoking {
                get { return fsmoking; }
                set { SetPropertyValue("smoking", ref fsmoking, value); }
            }
    
            private System.String ffam_apgar_help;
            [Size(16)]
            [Custom("Caption", "Fam Apgar help")]
            public System.String fam_apgar_help {
                get { return ffam_apgar_help; }
                set { SetPropertyValue("fam_apgar_help", ref ffam_apgar_help, value); }
            }
    
            private System.Boolean ftrash;
            [Custom("Caption", "Trash")]
            public System.Boolean trash {
                get { return ftrash; }
                set { SetPropertyValue("trash", ref ftrash, value); }
            }
    
        
            private res_partner fname;
            //FK FK_medical_patient_name
            [Custom("Caption", "Name")]
            public res_partner name {
                get { return fname; }
                set { SetPropertyValue<res_partner>("name", ref fname, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.String fmarital_status;
            [Size(16)]
            [Custom("Caption", "Marital Status")]
            public System.String marital_status {
                get { return fmarital_status; }
                set { SetPropertyValue("marital_status", ref fmarital_status, value); }
            }
    
            private System.Int32 fpremature;
            [Custom("Caption", "Premature")]
            public System.Int32 premature {
                get { return fpremature; }
                set { SetPropertyValue("premature", ref fpremature, value); }
            }
    
            private System.Boolean frelative_in_prison;
            [Custom("Caption", "Relative In prison")]
            public System.Boolean relative_in_prison {
                get { return frelative_in_prison; }
                set { SetPropertyValue("relative_in_prison", ref frelative_in_prison, value); }
            }
    
            private System.Boolean fdeceased;
            [Custom("Caption", "Deceased")]
            public System.Boolean deceased {
                get { return fdeceased; }
                set { SetPropertyValue("deceased", ref fdeceased, value); }
            }
    
            private System.Boolean fworks_at_home;
            [Custom("Caption", "Works At home")]
            public System.Boolean works_at_home {
                get { return fworks_at_home; }
                set { SetPropertyValue("works_at_home", ref fworks_at_home, value); }
            }
    
            private System.Int32 fage_start_smoking;
            [Custom("Caption", "Age Start smoking")]
            public System.Int32 age_start_smoking {
                get { return fage_start_smoking; }
                set { SetPropertyValue("age_start_smoking", ref fage_start_smoking, value); }
            }
    
            private System.String fses;
            [Size(16)]
            [Custom("Caption", "Ses")]
            public System.String ses {
                get { return fses; }
                set { SetPropertyValue("ses", ref fses, value); }
            }
    
            private System.String fsexual_preferences;
            [Size(16)]
            [Custom("Caption", "Sexual Preferences")]
            public System.String sexual_preferences {
                get { return fsexual_preferences; }
                set { SetPropertyValue("sexual_preferences", ref fsexual_preferences, value); }
            }
    
            private System.Boolean ftelephone;
            [Custom("Caption", "Telephone")]
            public System.Boolean telephone {
                get { return ftelephone; }
                set { SetPropertyValue("telephone", ref ftelephone, value); }
            }
    
            private System.String fsex;
            [Size(16)]
            [Custom("Caption", "Sex")]
            public System.String sex {
                get { return fsex; }
                set { SetPropertyValue("sex", ref fsex, value); }
            }
    
            private System.String fallergy_info;
            [Size(-1)]
            [Custom("Caption", "Allergy Info")]
            public System.String allergy_info {
                get { return fallergy_info; }
                set { SetPropertyValue("allergy_info", ref fallergy_info, value); }
            }
    
            private System.Int32 fdeaths_1st_week;
            [Custom("Caption", "Deaths 1st week")]
            public System.Int32 deaths_1st_week {
                get { return fdeaths_1st_week; }
                set { SetPropertyValue("deaths_1st_week", ref fdeaths_1st_week, value); }
            }
    
            private System.Int32 fhours_outside;
            [Custom("Caption", "Hours Outside")]
            public System.Int32 hours_outside {
                get { return fhours_outside; }
                set { SetPropertyValue("hours_outside", ref fhours_outside, value); }
            }
    
            private System.String feducation;
            [Size(16)]
            [Custom("Caption", "Education")]
            public System.String education {
                get { return feducation; }
                set { SetPropertyValue("education", ref feducation, value); }
            }
    
            private System.Int32 fmenarche;
            [Custom("Caption", "Menarche")]
            public System.Int32 menarche {
                get { return fmenarche; }
                set { SetPropertyValue("menarche", ref fmenarche, value); }
            }
    
            private System.Boolean fdrug_addiction;
            [Custom("Caption", "Drug Addiction")]
            public System.Boolean drug_addiction {
                get { return fdrug_addiction; }
                set { SetPropertyValue("drug_addiction", ref fdrug_addiction, value); }
            }
    
            private System.Boolean falcohol;
            [Custom("Caption", "Alcohol")]
            public System.Boolean alcohol {
                get { return falcohol; }
                set { SetPropertyValue("alcohol", ref falcohol, value); }
            }
    
            private System.String ffam_apgar_timesharing;
            [Size(16)]
            [Custom("Caption", "Fam Apgar timesharing")]
            public System.String fam_apgar_timesharing {
                get { return ffam_apgar_timesharing; }
                set { SetPropertyValue("fam_apgar_timesharing", ref ffam_apgar_timesharing, value); }
            }
    
            private System.Int32 fnumber_of_meals;
            [Custom("Caption", "Number Of meals")]
            public System.Int32 number_of_meals {
                get { return fnumber_of_meals; }
                set { SetPropertyValue("number_of_meals", ref fnumber_of_meals, value); }
            }
    
            private System.Boolean finternet;
            [Custom("Caption", "Internet")]
            public System.Boolean internet {
                get { return finternet; }
                set { SetPropertyValue("internet", ref finternet, value); }
            }
    
            private System.Int32 fcoffee_cups;
            [Custom("Caption", "Coffee Cups")]
            public System.Int32 coffee_cups {
                get { return fcoffee_cups; }
                set { SetPropertyValue("coffee_cups", ref fcoffee_cups, value); }
            }
    
            private System.String frh;
            [Size(16)]
            [Custom("Caption", "Rh")]
            public System.String rh {
                get { return frh; }
                set { SetPropertyValue("rh", ref frh, value); }
            }
    
            private System.String fses_notes;
            [Size(-1)]
            [Custom("Caption", "Ses Notes")]
            public System.String ses_notes {
                get { return fses_notes; }
                set { SetPropertyValue("ses_notes", ref fses_notes, value); }
            }
    
            private System.String ffam_apgar_affection;
            [Size(16)]
            [Custom("Caption", "Fam Apgar affection")]
            public System.String fam_apgar_affection {
                get { return ffam_apgar_affection; }
                set { SetPropertyValue("fam_apgar_affection", ref ffam_apgar_affection, value); }
            }
    
            private System.Int32 fgravida;
            [Custom("Caption", "Gravida")]
            public System.Int32 gravida {
                get { return fgravida; }
                set { SetPropertyValue("gravida", ref fgravida, value); }
            }
    
            private System.Int32 falcohol_wine_number;
            [Custom("Caption", "Alcohol Wine number")]
            public System.Int32 alcohol_wine_number {
                get { return falcohol_wine_number; }
                set { SetPropertyValue("alcohol_wine_number", ref falcohol_wine_number, value); }
            }
    
            private System.Int32 fexcercise_minutes_day;
            [Custom("Caption", "Excercise Minutes day")]
            public System.Int32 excercise_minutes_day {
                get { return fexcercise_minutes_day; }
                set { SetPropertyValue("excercise_minutes_day", ref fexcercise_minutes_day, value); }
            }
    
            private System.Boolean fex_smoker;
            [Custom("Caption", "Ex Smoker")]
            public System.Boolean ex_smoker {
                get { return fex_smoker; }
                set { SetPropertyValue("ex_smoker", ref fex_smoker, value); }
            }
    
            private System.Int32 falcohol_liquor_number;
            [Custom("Caption", "Alcohol Liquor number")]
            public System.Int32 alcohol_liquor_number {
                get { return falcohol_liquor_number; }
                set { SetPropertyValue("alcohol_liquor_number", ref falcohol_liquor_number, value); }
            }
    
            private System.Boolean fwater;
            [Custom("Caption", "Water")]
            public System.Boolean water {
                get { return fwater; }
                set { SetPropertyValue("water", ref fwater, value); }
            }
    
        
            private medical_ethnicity fethnic_group;
            //FK FK_medical_patient_ethnic_group
            [Custom("Caption", "Ethnic Group")]
            public medical_ethnicity ethnic_group {
                get { return fethnic_group; }
                set { SetPropertyValue<medical_ethnicity>("ethnic_group", ref fethnic_group, value); }
            }
    
            private System.Boolean feats_alone;
            [Custom("Caption", "Eats Alone")]
            public System.Boolean eats_alone {
                get { return feats_alone; }
                set { SetPropertyValue("eats_alone", ref feats_alone, value); }
            }
    
            private System.Boolean fcoffee;
            [Custom("Caption", "Coffee")]
            public System.Boolean coffee {
                get { return fcoffee; }
                set { SetPropertyValue("coffee", ref fcoffee, value); }
            }
    
            private System.Int32 fmenopause;
            [Custom("Caption", "Menopause")]
            public System.Int32 menopause {
                get { return fmenopause; }
                set { SetPropertyValue("menopause", ref fmenopause, value); }
            }
    
            private System.String ffam_apgar_decisions;
            [Size(16)]
            [Custom("Caption", "Fam Apgar decisions")]
            public System.String fam_apgar_decisions {
                get { return ffam_apgar_decisions; }
                set { SetPropertyValue("fam_apgar_decisions", ref ffam_apgar_decisions, value); }
            }
    
            private System.String ffam_apgar_discussion;
            [Size(16)]
            [Custom("Caption", "Fam Apgar discussion")]
            public System.String fam_apgar_discussion {
                get { return ffam_apgar_discussion; }
                set { SetPropertyValue("fam_apgar_discussion", ref ffam_apgar_discussion, value); }
            }
    
            private System.String fanticonceptive;
            [Size(16)]
            [Custom("Caption", "Anticonceptive")]
            public System.String anticonceptive {
                get { return fanticonceptive; }
                set { SetPropertyValue("anticonceptive", ref fanticonceptive, value); }
            }
    
            private System.Boolean fdrug_usage;
            [Custom("Caption", "Drug Usage")]
            public System.Boolean drug_usage {
                get { return fdrug_usage; }
                set { SetPropertyValue("drug_usage", ref fdrug_usage, value); }
            }
    
            private DateTime? fdod;
            [Custom("Caption", "Dod")]
            public DateTime? dod {
                get { return fdod; }
                set { SetPropertyValue("dod", ref fdod, value); }
            }
    
            private System.Boolean fteenage_pregnancy;
            [Custom("Caption", "Teenage Pregnancy")]
            public System.Boolean teenage_pregnancy {
                get { return fteenage_pregnancy; }
                set { SetPropertyValue("teenage_pregnancy", ref fteenage_pregnancy, value); }
            }
    
            private System.Boolean fschool_withdrawal;
            [Custom("Caption", "School Withdrawal")]
            public System.Boolean school_withdrawal {
                get { return fschool_withdrawal; }
                set { SetPropertyValue("school_withdrawal", ref fschool_withdrawal, value); }
            }
    
        
            private medical_pathology fcod;
            //FK FK_medical_patient_cod
            [Custom("Caption", "Cod")]
            public medical_pathology cod {
                get { return fcod; }
                set { SetPropertyValue<medical_pathology>("cod", ref fcod, value); }
            }
    
            private System.Boolean fsalt;
            [Custom("Caption", "Salt")]
            public System.Boolean salt {
                get { return fsalt; }
                set { SetPropertyValue("salt", ref fsalt, value); }
            }
    
            private System.String fhousing;
            [Size(16)]
            [Custom("Caption", "Housing")]
            public System.String housing {
                get { return fhousing; }
                set { SetPropertyValue("housing", ref fhousing, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_patient(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

