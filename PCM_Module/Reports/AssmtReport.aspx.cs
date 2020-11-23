using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;



public partial class Reports_Assmt : System.Web.UI.Page
{
    private SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelRefno.Text = Request["id"].ToString();
       // Button1.Attributes.Add("onclick", "javascript:window.print();");
        selectProbofficer();
        selectChildDetails();
        selectFamilyno();
        selectFamilyinfo();
        selectChildMedical();
        selectChildEducation();
        selectChildCaregiver();
        selectSocioEconomy();
        selectOffenceno();
        selectOffenceDetails();
    }

    private void selectProbofficer()
    {
        if (!IsPostBack)
        {
            #region connection
            ConnectionString();
            #endregion

            #region SQL Command

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectProbofficer";

            cmd.Parameters.Add("@office_ref_no", SqlDbType.VarChar);
            cmd.Parameters["@office_ref_no"].Value = LabelRefno.Text;


            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    LabelChildNameprb.Text = dr["child_name"].ToString() + " ,"  ;
                    LabelChildSurnmprb.Text = dr["child_surname"].ToString();
                    LabelProbOfficer.Text = dr["prob_officer"].ToString() + " ," ;
                    LabelPrboffSurnm.Text = dr["prob_officer_surname"].ToString();
                    LabelStreetPrb.Text = dr["street_name"].ToString()+ ",";
                    LabelTownpbr.Text = dr["town"].ToString() + ",";
                    LabelProvpbr.Text = dr["province"].ToString()+",";
                    LabelOffTelpbr.Text = dr["office_tell_no"].ToString();
                    LabelAddContpbr.Text = dr["add_contact_info"].ToString();
                    LabelMagiDistr.Text = dr["destrict"].ToString();
                    DateTime assDate = DateTime.Parse(dr["ass_date"].ToString());
                    LabelAssdate.Text = assDate.ToString("yyyy-MM-dd");
                    LabelAssmTim.Text = dr["ass_time"].ToString();
                    LabelSupervipbr.Text = dr["supervisor_name"].ToString();
                    LabelCodepbr.Text = dr["code"].ToString();
                    //DropDownListProvice.SelectedValue = dr["province"].ToString();
                    //DropDownListDistrict.SelectedValue = dr["destrict"].ToString();
                    //DropDownListArea.SelectedValue = dr["area"].ToString();
                    //txtTown.Text = dr["town"].ToString();
                    //txtStreetName.Text = dr["street_name"].ToString();
                    //txtOfficeNo.Text = dr["office_no"].ToString();
                    //LabelCodepbr.Text = dr["code"].ToString();
                    //txtOfficeTel.Text = dr["office_tell_no"].ToString();
                    //LabelAddContpbr.Text = dr["add_contact_info"].ToString();
                    //DateTime assDate = DateTime.Parse(dr["ass_date"].ToString());
                    //txtAssessmentDate.Text = assDate.ToString("yyyy-MM-dd");
                    //txtAssessmenttime.Text = dr["ass_time"].ToString();
                    //txtSupervisorName.Text = dr["supervisor_name"].ToString();



                }
                con.Close();
            }

            catch
            {
                con.Close();
            }

            #endregion
        }


    }

    private void ConnectionString()
    {
        #region connection
        con = new SqlConnection();
        String StrCon = ConfigurationManager.ConnectionStrings["SDIMSConnectionString"].ConnectionString;
        con.ConnectionString = StrCon;
        #endregion
    }

    private void selectChildDetails()
    {
        if (!IsPostBack)
        {
            #region variables

            string gender = "";
            string confirmation = "";
            string disability = "";
            string age_verification = "";
            string language = "";
            string populGroup = "";
            #endregion

            #region connection
            ConnectionString();
            #endregion

            #region SQL Command

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectChildDetail";

            cmd.Parameters.Add("@office_ref_no", SqlDbType.VarChar);
            cmd.Parameters["@office_ref_no"].Value = LabelRefno.Text;


            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txthidechldno.Text = dr["child_no"].ToString();
                    LabelChildName.Text = dr["childname"].ToString();
                    LabelChildSurname.Text = dr["surname"].ToString();
                    LabelChildAlias.Text = dr["aliases"].ToString();
                    LabelIDnoCh.Text = dr["id_no"].ToString();
                    LabelDobCh.Text = dr["birth_date"].ToString();
                    

                    confirmation = dr["confirmation"].ToString();

                    if (confirmation == "Yes")
                    {
                        CheckBoxConfy.Checked = true;
                    }
                    else if (confirmation == "No")
                    {
                        CheckBoxConfN.Checked = true;
                    }
                    gender = dr["gender"].ToString();
                    if (gender == "Male")
                    {
                        CheckBoxPmale.Checked = true;
                    }
                    else if (gender == "Female")
                    {
                        CheckBoxPfemal.Checked = true;
                    }
                    LabelTownCh.Text = dr["town"].ToString()+",";
                    LabelStreetCh.Text = dr["street_name"].ToString();
                    //txtChildHouseNo.Text = dr["house_no"].ToString();
                    LabelCodeCh.Text = dr["code"].ToString();
                    //disability = dr["disability"].ToString();
                    //if (disability == "Yes")
                    //{
                    //    rdChildDisability.Checked = true;
                    //}
                    //else if (disability == "No")
                    //{
                    //    rdChildDisabilityNo.Checked = true;
                    //}
                    //DropDownListDisabType.SelectedValue = dr["type_disability"].ToString();
                    LabelYearCh.Text = dr["age_estimate"].ToString();
                    LabelAgevrSourceCh.Text = dr["age_verification"].ToString();
                    //if (age_verification == "Yes")
                    //{
                    //    rdChildAgeVerYes.Checked = true;
                    //}
                    //else if (age_verification == "No")
                    //{
                    //    rdChildAgeVerNo.Checked = true;
                    //}

                    //DropDownListPopulationGrp.SelectedValue = dr["populationgroup"].ToString();
                    populGroup = dr["populationgroup"].ToString();
                    if (populGroup == "Asian")
                    {
                        CheckBoxAsian.Checked = true;
                    }
                    else if (populGroup == "Black")
                    {
                        CheckBoxBlack.Checked = true;
                    }
                    else if (populGroup == "Coloured")
                    {
                        CheckBoxColoured.Checked = true;
                    }
                    else if (populGroup == "White")
                    {
                        CheckBoxWhite.Checked = true;
                    }
                    else if (populGroup == "Other(Specify)")
                    {
                        CheckBoxOtherP.Checked = true;
                    }
                    //txtOtherPopGroup.Text = dr["otherPopgroup"].ToString();
                    LabelNationCh.Text = dr["nationality"].ToString();
                    LabelReligCh.Text = dr["religion"].ToString();
                    language = dr["languages"].ToString();
                    if (language == "English")
                    {
                        CheckBoxEng.Checked = true;
                    }
                    else if (language == "Afrikaans")
                    {
                        CheckBoxAfr.Checked = true;
                    }
                    else if (language == "Xhosa")
                    {
                        CheckBoxXho.Checked = true;
                    }
                    else if (language == "Zulu")
                    {
                        CheckBoxZul.Checked = true;
                    }
                    if (language == "Tswana")
                    {
                        CheckBoxTsw.Checked = true;
                    }
                    else if (language == "Sesotho")
                    {
                        CheckBoxSes.Checked = true;
                    }
                    else if (language == "Sepedi")
                    {
                        CheckBoxSep.Checked = true;
                    }
                    else if (language == "Venda")
                    {
                        CheckBoxVen.Checked = true;
                    }
                    else if (language == "Tsonga")
                    {
                        CheckBoxTso.Checked = true;
                    }
                    else if (language == "Ndebele")
                    {
                        CheckBoxNde.Checked = true;
                    }
                    else if (language == "Seswati")
                    {
                        CheckBoxSesw.Checked = true;
                    }
                    else if (language == "Other(Specify)")
                    {
                        CheckBoxOtherSpch.Checked = true;
                    }
                    LabelOtherlangCh.Text = dr["otherLanguage"].ToString();



                }
                con.Close();
            }

            catch
            {
                con.Close();
            }

            #endregion
        }
    }

    private void selectFamilyno()
    {

        #region connection
        ConnectionString();
        #endregion

        #region SQL Command

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SelectFamilyNo";

        cmd.Parameters.Add("@child_no", SqlDbType.VarChar);
        cmd.Parameters["@child_no"].Value = txthidechldno.Text;


        try
        {
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtHidefamily.Text = dr["family_no"].ToString();


            }
            con.Close();
        }

        catch
        {
            con.Close();
        }

        #endregion

    }

    private void selectFamilyinfo()
    {

        #region connection
        ConnectionString();
        #endregion

        #region SQL Command

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SelectFamilyinfo";

        cmd.Parameters.Add("@child_no", SqlDbType.VarChar);
        cmd.Parameters["@child_no"].Value = txthidechldno.Text;


        try
        {
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {

               TextBox1.Text = dr["family_bground"].ToString();



            }
            con.Close();
        }

        catch
        {
            con.Close();
        }

        #endregion

    }

    private void selectChildMedical()
    {
            #region connection
            ConnectionString();
            #endregion

            #region SQL Command

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectMedicalinfo";

            cmd.Parameters.Add("@child_no", SqlDbType.Int);
            cmd.Parameters["@child_no"].Value = txthidechldno.Text;


            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    LabelHSmedical.Text = dr["Health_Status"].ToString();
                    LabelINJmedical.Text = dr["injuries"].ToString();
                    LabelMDmedication.Text = dr["allergies"].ToString();
                    LabelFutMAmedical.Text = dr["medication"].ToString();
                    //txtFutureMedApp.Text = dr["future_med_app"].ToString();
                }
                con.Close();
            }

            catch
            {
                con.Close();
            }

            #endregion

        
    }

    private void selectChildEducation()
    {
        
            #region connection
            ConnectionString();
            #endregion

            #region SQL Command

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectEducationinfo";

            cmd.Parameters.Add("@child_no", SqlDbType.Int);
            cmd.Parameters["@child_no"].Value = txthidechldno.Text;


            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    LabelFSCHeduc.Text = dr["first_school"].ToString();
                    LabelPRVschool.Text = dr["previous_school"].ToString();
                    LabelCRNTschool.Text = dr["current_school"].ToString();
                    LabelDATElstatt.Text = dr["date_last_attend"].ToString();
                    LabelSchoolAddr.Text = dr["town"].ToString() + ",";
                    LabelSchoolstr.Text = dr["street_name"].ToString()+",";
                    LabelStreetno.Text = dr["street_no"].ToString();
                    LabelCodeSchool.Text = dr["code"].ToString();
                    LabelGradeCompleted.Text = dr["grade_completed"].ToString();
                    LabelYearCompleted.Text = dr["year_completed"].ToString();
                    LabelNameEduc.Text = dr["EducatorName"].ToString();
                    txtAddtlinfoeduc.Text = dr["AdditionalInfo"].ToString();

                }
                con.Close();
            }

            catch
            {
                con.Close();
            }

            #endregion
        
    }

    private void selectChildCaregiver()
    {
        
            #region variables

            string employStatus = "";

            #endregion

            #region connection
            ConnectionString();
            #endregion

            #region SQL Command

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectCaregiver";

            cmd.Parameters.Add("@child_no", SqlDbType.Int);
            cmd.Parameters["@child_no"].Value = txthidechldno.Text;


            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    LabelNameCaregv.Text = dr["Careg_name"].ToString();
                    LabelSurnameCaregv.Text = dr["surname"].ToString();
                    LabelRelationship.Text = dr["relationship"].ToString();
                    LabelOtherRelation.Text = dr["otherRelation"].ToString();
                    LabelHouseno.Text = dr["res_house_no"].ToString()+",";
                    LabelStreetCaregv.Text = dr["res_street_name"].ToString();
                    LabelREStown.Text = dr["res_town"].ToString()+",";
                    LabelREScode.Text = dr["res_code"].ToString();
                    LabelCurrentYears.Text = dr["current_careg_yrs"].ToString(); 
                    LabelStreetnowrk.Text = dr["work_street_no"].ToString(); 
                    LabelStreetNmwrk.Text = dr["work_street_name"].ToString();
                    LabelTownWork.Text = dr["work_town"].ToString();
                    LabelCarecode.Text = dr["work_code"].ToString();
                    LabelCareTel.Text = dr["tell_home"].ToString();
                    LabelWorktel.Text = dr["tell_work"].ToString();
                    LabelCellcare.Text = dr["tell_cell"].ToString();
                    LabelOthertel.Text = dr["other_contact"].ToString();
                    employStatus = dr["employment_status"].ToString();
                    if (employStatus == "Employed")
                    {
                        CheckBoxEmp.Checked = true;
                    }
                    if (employStatus == "UnEmployed")
                    {
                        CheckBoxUnEmp.Checked = true;
                    }
                    if (employStatus == "Self Employed")
                    {
                        CheckBoxSelEmp.Checked = true;
                    }
                    if (employStatus == "Other(Specify)")
                    {
                        CheckBoxOthsp.Checked = true;
                    }
                    LabelOtherStatus.Text = dr["otherStatus"].ToString();

                }
                con.Close();
            }

            catch
            {
                con.Close();
            }

            #endregion
        
    }

    private void selectSocioEconomy()
    {
        

            #region connection
            ConnectionString();
            #endregion

            #region SQL Command

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectSocio_Economy";

            cmd.Parameters.Add("@family_no", SqlDbType.Int);
            cmd.Parameters["@family_no"].Value = txtHidefamily.Text;


            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    TextBox2.Text = dr["finance_work_rec"].ToString();
                    TextBox3.Text = dr["housing"].ToString();
                    TextBox4.Text = dr["social_circum"].ToString();
                    TextBox5.Text = dr["prev_intervention"].ToString();
                    TextBox6.Text = dr["inter_person_relate"].ToString();
                    TextBox7.Text = dr["peer_presure"].ToString();
                    TextBox8.Text = dr["substance_abuse"].ToString();
                    TextBox9.Text = dr["religious_involve"].ToString();
                    TextBox10.Text = dr["other"].ToString();
                }
                con.Close();
            }

            catch
            {
                con.Close();
            }

            #endregion
       
    }

    private void selectOffenceno()
    {

        #region connection
        ConnectionString();
        #endregion

        #region SQL Command

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Selectoffence_no";

        cmd.Parameters.Add("@child_no", SqlDbType.VarChar);
        cmd.Parameters["@child_no"].Value = txthidechldno.Text;


        try
        {
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txthideoffence.Text = dr["offence_no"].ToString();
                //txthoff.Text = dr["offence_no"].ToString();
                //txtHideoff4prv.Text = dr["offence_no"].ToString();
            }
            con.Close();
        }

        catch
        {
            con.Close();
        }

        #endregion

    }

    private void selectOffenceDetails()
    {
        if (!IsPostBack)
        {
            #region variable
            string respond = "";
            string legalRep = "";
            #endregion

            #region connection
            ConnectionString();
            #endregion

            #region SQL Command

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectOffence_details";

            cmd.Parameters.Add("@offence_no", SqlDbType.VarChar);
            cmd.Parameters["@offence_no"].Value = txthideoffence.Text;


            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    LabelOffence.Text = dr["nature_of_offence"].ToString();
                    LabelCharge.Text = dr["charge"].ToString();
                    TextBox11.Text = dr["offence_circumstance"].ToString();
                    LabelGoods.Text = dr["value_of_goods"].ToString();
                    LabelRecovery.Text = dr["value_recovered"].ToString();
                    respond = dr["child_responsible"].ToString();
                    if (respond == "Yes")
                    {
                        chckbChildRespyes.Checked = true;
                    }
                    else
                    {
                        chckbChildResno.Checked = true;
                    }
                    TextBox12.Text = dr["responsibility_details"].ToString();
                    LabelDate.Text = dr["date_arrested"].ToString();
                    LabelTime.Text = dr["time_arrested"].ToString();
                    LabelArresting.Text = dr["arresting_officer"].ToString();
                    LabelInvestig.Text = dr["investigate_officer"].ToString();
                    LabelSAPS.Text = dr["saps_station"].ToString();
                    LabelTelNumber.Text = dr["tell_no"].ToString();
                    LabelDetension.Text = dr["detention_place"].ToString();
                    //DropDownListMagistDistr0.SelectedValue = dr["magistrate_destrict"].ToString();
                    //DropDownListCourt0.SelectedValue = dr["court"].ToString();
                    //txtCasno0.Text = dr["CAS_no"].ToString();
                    LabelDateofApp.Text = dr["first_appear_date"].ToString();
                    LabelNumDays.Text = dr["num_days_custody"].ToString();
                    //legalRep = dr["legal_representitive"].ToString();
                    //if (legalRep == "Yes")
                    //{
                    //    rdnLegalRepyes0.Checked = true;
                    //}
                    //else
                    //{
                    //    rdnLegalRepno0.Checked = true;
                    //}

                    //txtNameOfRep0.Text = dr["name_legal_rep"].ToString();
                }
                con.Close();
            }

            catch
            {
                con.Close();
            }
            #endregion
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {

    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}