using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_AssessmentReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        txtChildno.Text = Request.QueryString["id"];
        txtAssessno.Text = Request.QueryString["idd"];
        if (!String.IsNullOrEmpty(Request.QueryString["Format"]))
        {
            ReturnDocument(Request.QueryString["Format"].ToString());
        }
    }
    private void ReturnDocument(string format)
    {
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        LocalReport report = new LocalReport();
        report.ReportPath = ReportViewer1.LocalReport.ReportPath;
        FillDataSets("SelectAssessReport1", "DataSetChildAssessment", report);
        FillDataSets("SelectAssessReport2", "DataSetProbationinf", report);
        FillDataSets("SelectAssessReport3", "DataSetAssessInfo", report);
        FillDataSets("SelectAssessReport4", "DataSet1CourtInfo", report);
        FillDataSets("SelectAssessReport5", "DataSet1Supervisor", report);
        FillDataSets("SelectAssessReport6", "DataSetPersonal", report);
        FillDataSets("SelectAssessReport7", "DataSetMedicalInfo", report);
        FillDataSets("SelectAssessReport8", "DataSetEducation", report);
        FillDataSets("SelectAssessReport9", "DataSetCaregiver", report);
        FillDataSets("SelectAssessReport10", "DataSetFamilyInfo", report);
        FillDataSets("SelectAssessReport11", "DataSetSocioEco", report);
        FillDataSets("SelectAssessReport12", "DataSetOffence", report);
        FillDataSets("SelectAssessReport13", "DataSetCoaccused", report);
        FillDataSets("SelectAssessReport14", "DataSetPreviousinv", report);
        FillDataSets("SelectAssessReport15", "DataSetVictim", report);
        FillDataSets("SelectAssessReport16", "DataSetDevelopment", report);
        FillDataSets("SelectAssessReport17", "DataSetRecommendatio", report);
        FillDataSets("SelectAssessReport18", "DataSetGeneral", report);

        byte[] bytes = report.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=AssessmentReport." + extension);
        Response.BinaryWrite(bytes); // create the file
        Response.Flush(); // send it to the client to download
    }
    private void FillDataSets(string commandText,string datasetName,LocalReport report)
    {
       // using (SqlConnection sqlConn = new SqlConnection())
        using (SqlDataAdapter da = new SqlDataAdapter(commandText, ConfigurationManager.ConnectionStrings["PCMv1ConnectionString"].ConnectionString))
        {
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            switch (datasetName)
            {
                case "DataSetChildAssessment":
                case "DataSetPersonal":
                    da.SelectCommand.Parameters.Add("@child_no", SqlDbType.Int).Value = Convert.ToInt32(txtChildno.Text);
                    break;
                default:
                    da.SelectCommand.Parameters.Add("@child_no", SqlDbType.Int).Value = Convert.ToInt32(txtChildno.Text);
                    da.SelectCommand.Parameters.Add("@ass_no", SqlDbType.Int).Value = Convert.ToInt32(txtAssessno.Text);
                    break;
            }
            
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            ReportDataSource rds = new ReportDataSource();
            rds.Name = datasetName;
            rds.Value = ds.Tables[0];
            report.DataSources.Add(rds);
        }
    }
    protected void ReportViewer_OnLoad(object sender, EventArgs e)
    {
        string exportOption = "Excel";
        string exportOption1 = "Word";
        //string exportOption = "PDF";
        RenderingExtension extension = this.ReportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        RenderingExtension extension1 = ReportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));

        if (extension != null)
        {
            System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            fieldInfo.SetValue(extension, false);
        }

        if (extension1 != null)
        {
            System.Reflection.FieldInfo fieldInfo = extension1.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            fieldInfo.SetValue(extension1, false);
        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
}