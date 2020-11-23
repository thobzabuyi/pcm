<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssessmentReport.aspx.cs" Inherits="Reports_AssessmentReport" %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

  
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" Height="" InteractiveDeviceInfos="(Collection)" 
                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="" 
                        ShowPrintButton="False" SizeToReportContent="True" OnLoad="ReportViewer_OnLoad" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                        <LocalReport ReportPath="Reports\AssessmentReport.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" 
                                    Name="DataSetChildAssessment" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource2" 
                                    Name="DataSetProbationinf" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource3" 
                                    Name="DataSetAssessInfo" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource4" 
                                    Name="DataSet1CourtInfo" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource5" 
                                    Name="DataSet1Supervisor" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource6" Name="DataSetPersonal" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource7" 
                                    Name="DataSetMedicalInfo" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource8" Name="DataSetEducation" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource9" Name="DataSetCaregiver" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource10" 
                                    Name="DataSetFamilyInfo" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource11" Name="DataSetSocioEco" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource12" Name="DataSetOffence" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource13" 
                                    Name="DataSetCoaccused" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource14" 
                                    Name="DataSetPreviousinv" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource15" Name="DataSetVictim" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource16" 
                                    Name="DataSetDevelopment" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource17" 
                                    Name="DataSetRecommendatio" />
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource18" Name="DataSetGeneral" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:SqlDataSource ID="SqlDataSource18" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport18" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource17" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport17" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource16" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport16" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource15" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport15" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource14" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport14" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource13" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport13" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource12" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport12" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource11" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport11" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource10" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport10" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource9" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport9" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport8" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport7" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport6" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
                        SelectCommand="SelectAssessReport5" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChildno" Name="child_no" 
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtAssessno" Name="ass_no" PropertyName="Text" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:PCMv1ConnectionString %>" 
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
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    <asp:TextBox ID="txtChildno" runat="server" Height="16px" Visible="False" 
        Width="16px"></asp:TextBox>
    <asp:TextBox ID="txtAssessno" runat="server" Height="16px" Visible="False" 
        Width="16px"></asp:TextBox>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </form>
</body>
</html>
