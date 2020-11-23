<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssessmentReportPcm.aspx.cs" Inherits="PCM_Module.Reports.AssessmentReportPcm" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="922px">
                            
                            <LocalReport ReportPath="Reports\AssessmentReport.rdlc">
                                <DataSources>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSetChildAssessment">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="DataSetProbationinf">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource3" Name="DataSetAssessInfo">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource4" Name="DataSet1CourtInfo">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource5" Name="DataSet1Supervisor">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource6" Name="DataSetPersonal">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource7" Name="DataSetMedicalInfo">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource8" Name="DataSetEducation">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource9" Name="DataSetCaregiver">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource10" Name="DataSetFamilyInfo">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource11" Name="DataSetSocioEco">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource12" Name="DataSetOffence">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource13" Name="DataSetCoaccused">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource14" Name="DataSetPreviousinv">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource15" Name="DataSetVictim">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource16" Name="DataSetDevelopment">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource17" Name="DataSetRecommendatio">
                                    </rsweb:ReportDataSource>
                                    
                                    <rsweb:ReportDataSource DataSourceId="SqlDataSource18" Name="DataSetGeneral">
                                    </rsweb:ReportDataSource>
                                    
                                </DataSources>
                                
                            </LocalReport>
                            
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtChildno" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtAssessno" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:SqlDataSource ID="SqlDataSource11" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport11" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource10" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport10" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource9" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport9" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport8" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport7" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport6" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" DefaultValue="0" Name="ass_no" PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport5" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport4" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport3" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport1" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtAssessno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource18" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport18" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource17" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport17" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource16" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport16" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource15" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport15" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource14" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport14" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource13" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport13" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource12" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SDIIS_Database_StagingConnectionString %>" 
                        SelectCommand="SelectAssessReport12" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    </td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>
