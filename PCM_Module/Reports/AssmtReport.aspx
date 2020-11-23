<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssmtReport.aspx.cs" Inherits="Reports_Assmt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 405px;
        }
        .style3
        {
            font-family: "Times New Roman", Times, serif;
            font-size: medium;
        }
        .style4
        {
            text-decoration: underline;
        }
        .style5
        {
        }
    </style>
  <script type="text/javascript">

  <!--
  function printpage() {
  window.print();
  }
  //-->
</script>

</head>
<body onload="printpage()" style="height: 5405px">
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1" runat="server" Height="118px" 
                        ImageUrl="~/Images/govLogo.bmp" Width="98px" />
                    <h4>
                        <strong>1 DEPARTMENT OF SOCIAL DEVELOPMENT</strong></h4>                                                
                </td>
            </tr>
        </table>
    <hr />
        <table class="style1">
            <tr>
                <td align="center" class="style3">
                    <strong>Assessment Report for Children In Conflict with the Law</strong></td>
            </tr>
        </table>
    <br />
    </div>
    <table class="style1">
        <tr>
            <td class="style2" style="width: 30%">
                &nbsp;</td>
            <td style="font-weight: 700;" align="center" colspan="2">
                Office Reference Number</td>
            <td style="border-bottom-style: dotted">
                <br />
                <asp:Label ID="LabelRefno" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Name of Child:</strong></td>
            <td style="border-bottom-style: dotted;" colspan="3" height="20%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="LabelChildNameprb" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelChildSurnmprb" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Probation Officer:</strong></td>
            <td style="border-bottom-style: dotted;" colspan="3" height="20%">
                <br />
                <asp:Label ID="LabelProbOfficer" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelPrboffSurnm" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Office Address:</strong></td>
            <td style="border-bottom-style: dotted;" colspan="3" height="20%">
                <br />
                <asp:Label ID="LabelStreetPrb" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelTownpbr" runat="server"></asp:Label>
&nbsp;<br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                &nbsp;</td>
            <td style="border-bottom-style: dotted;" colspan="3" height="20%">
                <br />
                <asp:Label ID="LabelProvpbr" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelCodepbr" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Office Telephone Number</strong></td>
            <td style="border-bottom-style: dotted;" height="20%">
                <asp:Label ID="LabelOffTelpbr" runat="server"></asp:Label>
            </td>
            <td style="border-bottom-style: dotted" height="20%">
                <br />
                <br />
            </td>
            <td style="border-bottom-style: dotted" height="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Additional Conctact Info.</strong></td>
            <td style="border-bottom-style: dotted;" colspan="3" height="20%">
                <br />
                <asp:Label ID="LabelAddContpbr" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Magisterial District:</strong></td>
            <td style="border-bottom-style: dotted;" colspan="3" height="20%">
                <br />
                <asp:Label ID="LabelMagiDistr" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Assessment Date:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;" height="20%">
                <asp:Label ID="LabelAssdate" runat="server"></asp:Label>
            </td>
            <td style="width: 30%" height="20%">
                <strong>Assessment Time[Start]<br />
                </strong></td>
            <td style="border-bottom-style: dotted" height="20%">
                <br />
                <asp:Label ID="LabelAssmTim" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Court:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;" height="20%">
                <asp:Label ID="LabelCourt" runat="server"></asp:Label>
            </td>
            <td style="width: 25%" height="20%">
                <strong>CAS Number<br />
                </strong></td>
            <td style="border-bottom-style: dotted" height="20%">
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 30%" height="20%">
                <strong>Supervisor Name:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;" height="20%">
                <asp:Label ID="LabelSupervipbr" runat="server"></asp:Label>
                </td>
            <td style="width: 25%" height="20%">
                <br />
            </td>
            <td height="20%">
                <br />
                <br />
            </td>


        </tr>
    </table>
    <p>
                &nbsp;
    </p>
    <p>
                &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <hr />
        &nbsp;<strong><span class="style4">A. PERSONAL DETAILS [CHILD]</span><br />
    </strong>
    <table class="style1">
        <tr>
            <td style="width: 30%">
                <strong>Name:</strong></td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelChildName" runat="server"></asp:Label>
            </td>
            <td style="width: 25%">
                <strong>Surname:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelChildSurname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Alias:</strong></td>
            <td style="border-bottom-style: dotted">
                <asp:Label ID="LabelChildAlias" runat="server"></asp:Label>
            </td>
            <td style="border-bottom-style: dotted">
                &nbsp;</td>
            <td style="border-bottom-style: dotted">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Address:</strong></td>
            <td style="border-bottom-style: dotted" colspan="2">
                <asp:Label ID="LabelStreetCh" runat="server"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelTownCh" runat="server"></asp:Label>
            </td>
            <td style="border-bottom-style: dotted">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
                &nbsp;</td>
            <td style="border-bottom-style: dotted" colspan="2">
                <asp:Label ID="LabelProvnCh" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelCodeCh" runat="server"></asp:Label>
            </td>
            <td style="border-bottom-style: dotted">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Date of Birth:</strong></td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelDobCh" runat="server"></asp:Label>
            </td>
            <td colspan="2">
                <strong>Confirmation Of Date Of&nbsp; Birth:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </strong>
                <asp:CheckBox ID="CheckBoxConfy" runat="server" Font-Bold="True" Text="Y" />
&nbsp;
                <asp:CheckBox ID="CheckBoxConfN" runat="server" Font-Bold="True" Text="N" />
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Gender:</strong></td>
            <td colspan="2">
                <asp:CheckBox ID="CheckBoxPmale" runat="server" Font-Bold="True" Text="Male" />
                <asp:CheckBox ID="CheckBoxPfemal" runat="server" Font-Bold="True" 
                    Text="Female" />
            </td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Identity Number:</strong></td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelIDnoCh" runat="server"></asp:Label>
            </td>
            <td colspan="2">
                <strong>Age Verification/Estimation:&nbsp;&nbsp;</strong><asp:Label 
                    ID="LabelYearCh" runat="server"></asp:Label>
                <span >&nbsp;&nbsp;
                </span><strong>Years</strong></td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Age Verification Source:</strong></td>
            <td colspan="2" style="border-bottom-style: dotted">
                <asp:Label ID="LabelAgevrSourceCh" runat="server"></asp:Label>
            </td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Population Group:</strong></td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxAsian" runat="server" Font-Bold="True" Text="Asian" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxBlack" runat="server" Font-Bold="True" Text="Black" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxColoured" runat="server" Font-Bold="True" 
                    Text="Coloured" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxWhite" runat="server" Font-Bold="True" Text="White" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxOtherP" runat="server" Font-Bold="True" 
                    Text="Other(Specify)" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" style="width: 30%">
                &nbsp;</td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Nationality:</strong></td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelNationCh" runat="server"></asp:Label>
            </td>
            <td style="width: 25%">
                <strong>Religion:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelReligCh" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <strong>Language:</strong></td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxEng" runat="server" Font-Bold="True" Text="English" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxAfr" runat="server" Font-Bold="True" 
                    Text="Afrikaans" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxXho" runat="server" Font-Bold="True" Text="Xhosa" />
&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxZul" runat="server" Font-Bold="True" Text="Zulu" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxTsw" runat="server" Font-Bold="True" Text="Tswana" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxSes" runat="server" Font-Bold="True" Text="Sesotho" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxSep" runat="server" Font-Bold="True" Text="Sepedi" />
&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxVen" runat="server" Font-Bold="True" Text="Venda" />
&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxTso" runat="server" Font-Bold="True" Text="Tsonga" />
&nbsp;&nbsp;
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxNde" runat="server" Font-Bold="True" Text="Ndebele" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxSesw" runat="server" Font-Bold="True" 
                    Text="Seswati" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBoxOtherSpch" runat="server" Font-Bold="True" 
                    Text="Other(Specify)" />
            </td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelOtherlangCh" runat="server"></asp:Label>
            </td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:TextBox ID="txthidechldno" runat="server" Height="16px" Width="16px" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtHidefamily" runat="server" Height="16px" Width="16px" 
                    Visible="False"></asp:TextBox>
            </td>
            <td style="width: 20%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
            <td style="width: 25%">
                &nbsp;</td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <br />
    <hr />
       
    <p class="style4">
        <strong>B. MEDICAL INFORMATION:</strong></p>
    <table class="style1">
        <tr>
            <td style="width: 50%">
                <strong>Health Status (Physiological / Physical):</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelHSmedical" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-bottom-style: dotted">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50%">
                <strong>Injuries:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelINJmedical" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td style="width: 50%; border-bottom-style: dotted;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50%">
                <strong>Medication:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelMDmedication" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <strong>Future Medical Appoinments:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelFutMAmedical" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <p class="style4">
        <strong>C. EDUCATION BACKGROUND</strong></p>
    <table class="style1">
        <tr>
            <td width="width:50%">
                <strong>First School Attended: [Grade 1]</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelFSCHeduc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="width:50%">
                <strong>Name of Previous Schools Attended:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelPRVschool" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="width:50%" colspan="2" style="border-bottom-style: dotted">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="width:50%">
                <strong>Name Of Current School:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelCRNTschool" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="width:50%">
                <strong>Date Last Attended:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelDATElstatt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="width:50%">
                <strong>School Address:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelStreetno" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelSchoolstr" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="width:50%">
                &nbsp;</td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelSchoolAddr" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelCodeSchool" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="width:50%" colspan="2" style="border-bottom-style: dotted">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 30%">
                <strong>Grade Completed:</strong></td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelGradeCompleted" runat="server"></asp:Label>
            </td>
            <td style="width: 15%">
                <strong>Year:</strong></td>
            <td style="width: 15%; border-bottom-style: dotted;">
                <asp:Label ID="LabelYearCompleted" runat="server"></asp:Label>
            </td>
            <td style="width: 20%">
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 50%">
                <strong>Name Of Educator:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelNameEduc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <strong>Additional Information:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtAddtlinfoeduc" runat="server" Height="101px" 
                    TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
        </tr>
        </table>
    <br />
    <hr />
    <p>
        &nbsp;</p>
    <p class="style4">
        <strong>D. PRIMARY CAREGIVER INFORMATION</strong></p>
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <strong>Name:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelNameCaregv" runat="server"></asp:Label>
            </td>
            <td style="width: 25%" align="right">
                Surname:</td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelSurnameCaregv" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <strong>Relationship:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelRelationship" runat="server"></asp:Label>
&nbsp;
                <asp:Label ID="LabelOtherRelation" runat="server"></asp:Label>
            </td>
            <td style="width: 25%" align="right">
                &nbsp;</td>
            <td style="width: 25%; border-bottom-style: dotted;">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style5" style="width: 50%">
                <strong>Residential Address:</strong></td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelHouseno" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelStreetCaregv" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" style="width: 50%">
                &nbsp;</td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelREStown" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td class="style5" style="width: 50%">
                &nbsp;</td>
            <td style="width: 50%; border-bottom-style: dotted;">
                <asp:Label ID="LabelREScode" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
     <br />
    <br />
    <br />
    <hr />
    <table class="style1">
        <tr>
            <td style="width: 50%">
                <strong>Years with Current Caregiver:</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                <asp:Label ID="LabelCurrentYears" runat="server"></asp:Label>
            </td>
            <td style="width: 40%">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 30%">
                <strong>Work Address:</strong></td>
            <td style="width: 70%; border-bottom-style: dotted;">
                <asp:Label ID="LabelStreetnowrk" runat="server"></asp:Label>
&nbsp;&nbsp;
                <asp:Label ID="LabelStreetNmwrk" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                &nbsp;</td>
            <td style="width: 70%; border-bottom-style: dotted;">
                <asp:Label ID="LabelTownWork" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                &nbsp;</td>
            <td style="width: 70%; border-bottom-style: dotted;">
                <asp:Label ID="LabelCarecode" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <strong>Telphone (H):</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelCareTel" runat="server"></asp:Label>
            </td>
            <td style="width: 25%">
                <strong>Telephone (W):</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelWorktel" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <strong>Cell:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                <asp:Label ID="LabelCellcare" runat="server"></asp:Label>
            </td>
            <td style="width: 25%">
                <strong>Other:</strong></td>
            <td style="border-bottom-style: dotted">
                <asp:Label ID="LabelOthertel" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <table class="style1">
        <tr>
            <td style="width: 50%">
                <strong>Employment Status:</strong></td>
            <td style="width: 50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50%">
                &nbsp;</td>
            <td style="width: 50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:CheckBox ID="CheckBoxEmp" runat="server" Font-Bold="True" 
                    Text="Employed" />
            </td>
            <td style="width: 50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:CheckBox ID="CheckBoxUnEmp" runat="server" Font-Bold="True" 
                    Text="Unemployed" />
            </td>
            <td style="width: 50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:CheckBox ID="CheckBoxSelEmp" runat="server" Font-Bold="True" 
                    Text="Self Employed" />
            </td>
            <td style="width: 50%">
                &nbsp;</td>
        </tr>
        </table>
    
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <asp:CheckBox ID="CheckBoxOthsp" runat="server" Font-Bold="True" 
                    Text="Other (Specify)" />
            </td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelOtherStatus" runat="server"></asp:Label>
            </td>
            <td style="width: 55%">
                &nbsp;</td>
        </tr>
    </table>
    <p class="style4">
        <strong>E: FAMILY INFORMATION</strong></p>
    <p>
        <strong>Composition</strong></p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="family_no" DataSourceID="SqlDataSource1" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="relationship" HeaderText="Relationship" 
                SortExpression="relationship" />
            <asp:BoundField DataField="Fisrt_name" HeaderText="Fisrt Name" 
                SortExpression="Fisrt_name" />
            <asp:BoundField DataField="surname" HeaderText="Surname" 
                SortExpression="surname" />
            <asp:BoundField DataField="age" HeaderText="Age" SortExpression="age" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SDIMSConnectionString %>" 
        SelectCommand="SelectFamilyinfo" SelectCommandType="StoredProcedure" 
        onselecting="SqlDataSource1_Selecting">
        <SelectParameters>
            <asp:ControlParameter ControlID="txthidechldno" Name="child_no" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <strong>Family Background:</strong></td>
            <td style="width: 75%; border-bottom-style: dotted;">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Height="179px" 
                    ontextchanged="TextBox1_TextChanged" TextMode="MultiLine" Width="100%" 
                    Font-Underline="False"></asp:TextBox>
            </td>
        </tr>
        </table>
    
    <p class="style4">
        <strong>F: SOCIO-ECONOMIC CIRCUMSTANCES</strong></p>
    <table class="style1">
        <tr>
            <td>
                <p>
                <strong>Finance &amp; Work Record</strong></p>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Housing</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Social Circumstances</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Previous Interventions</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Interpersonal Relationships</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox6" runat="server" Height="66px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Peer Group Pressure / Gang Involvement</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox7" runat="server" Height="66px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Substance Abuse</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox8" runat="server" Height="66px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Religions Involvement</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox9" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%" ontextchanged="TextBox9_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong>Other</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox10" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />    
    <br />
    <br />
    <br />
    <br />
    <br />
    <p class="style4">
        <strong>G: CASE INFORMATION [PARTICULARS OF OFFENCE]</strong></p>

        
    <table class="style1">
        <tr>
            <td style="width: 25% ;">
                <strong>Nature of Offence:</strong></td>
            <td style="width: 75%; border-bottom-style: dotted;">
                <asp:Label ID="LabelOffence" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 25% ;">
                <strong>Charge:</strong></td>
            <td style="width: 75%; border-bottom-style: dotted;">
                <asp:Label ID="LabelCharge" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <strong>Circumstances of Offences:</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox11" runat="server" Height="66px" Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table class="style1">
        <tr>
            <td style="width: 20%">
                <strong>Value Of Goods:</strong></td>
            <td align="right" style="width: 5%">
                <strong>R</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                &nbsp;&nbsp;
                <asp:Label ID="LabelGoods" runat="server"></asp:Label>
            </td>
            <td style="width: 10%">
                &nbsp;</td>
            <td style="width: 20%" align="right">
                <strong>Value Recovered:</strong></td>
            <td align="right" style="width: 5%">
                <strong>R</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                &nbsp;
                <asp:Label ID="LabelRecovery" runat="server"></asp:Label>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    <table class="style1">
        <tr>
            <td style="width: 60%">
                <strong>Does the Child Take Responsiblility for the Offence: </strong>
            </td>
            <td style="width: 40%">
                <asp:CheckBox ID="chckbChildRespyes" runat="server" Font-Bold="True" Text="Y" />
&nbsp;
                <asp:CheckBox ID="chckbChildResno" runat="server" Font-Bold="True" Text="N" />
            </td>
        </tr>
    </table>
    <asp:TextBox ID="TextBox12" runat="server" Height="66px" TextMode="MultiLine" 
        Width="100%"></asp:TextBox>
<br />
    <table class="style1">
        <tr>
            <td style="width: 20%">
                <strong>Date Of Arrest:</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                <asp:Label ID="LabelDate" runat="server"></asp:Label>
            </td>
            <td style="width: 20%" align="right">
                <strong>Time Of Arrest:</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                <asp:Label ID="LabelTime" runat="server"></asp:Label>
            </td>
            <td style="width: 40%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 20%">
                <strong>Arresting&nbsp;</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                <asp:Label ID="LabelArresting" runat="server"></asp:Label>
            </td>
            <td style="width: 20%" align="right">
                &nbsp;</td>
            <td style="width: 10%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td style="width: 40%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 20%">
                <strong>Investigating Officer:</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                <asp:Label ID="LabelInvestig" runat="server"></asp:Label>
            </td>
            <td style="width: 20%" align="right">
                &nbsp;</td>
            <td style="width: 10%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td style="width: 40%">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 20%">
                <strong>SAPS Station:</strong></td>
            <td style="border-bottom-style: dotted; width: 25%;">
                <asp:Label ID="LabelSAPS" runat="server"></asp:Label>
            </td>
            <td style="width: 25%" align="right">
                <strong>Telephone number:</strong></td>
            <td style="width: 15%; border-bottom-style: dotted;">
                <asp:Label ID="LabelTelNumber" runat="server"></asp:Label>
            </td>
            <td style="width: 15%">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <strong>Place of Detension:</strong></td>
            <td style="width: 75%; border-bottom-style: dotted;">
                <asp:Label ID="LabelDetension" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 30%">
                <strong>Date of First Appearance:</strong></td>
            <td style="width: 20%; border-bottom-style: dotted;">
                <asp:Label ID="LabelDateofApp" runat="server"></asp:Label>
            </td>
            <td style="width: 35%" align="right">
                <strong>Number Of Days In Custody:</strong></td>
            <td style="width: 15%; border-bottom-style: dotted;">
                <asp:Label ID="LabelNumDays" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 20%">
                <strong>Co - Accused:</strong></td>
            <td>
                <asp:CheckBox ID="chkbCoacusedy" runat="server" Font-Bold="True" Text="Y" />
&nbsp;
                <asp:CheckBox ID="chckCoacusedn" runat="server" Font-Bold="True" Text="N" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>
    <br />
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <strong>Legal Representation:</strong></td>
            <td>
                <asp:CheckBox ID="chckbLegRepy" runat="server" Font-Bold="True" Text="Y" />
&nbsp;
                <asp:CheckBox ID="chckbLegRepn" runat="server" Font-Bold="True" Text="N" />
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 35%">
                <strong>Legal Representation: (Name)</strong></td>
            <td style="width: 30%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td style="width: 45%">
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr>
            <td>
                <strong>Previous Involvement in Crime / Arrests / Conviction:</strong></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView3" runat="server">
    </asp:GridView>
    <br />
    <table class="style1">
        <tr>
            <td>
                <strong>Abscondments / Escapes:</strong></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView4" runat="server">
    </asp:GridView>
    <asp:TextBox ID="txthideoffence" runat="server" Height="16px" Visible="False" 
        Width="16px"></asp:TextBox>
    <br />
    <span class="style4"><strong>VICTIM PARTICULARS:<br />
    </strong></span>
    <hr />
    <table class="style1">
        <tr>
            <td style="width: 15%">
                <strong>Name:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td align="right" style="width: 15%">
                <strong>Surname:</strong></td>
            <td style="width: 25%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td style="width: 20%">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 10%">
                <strong>Age:</strong></td>
            <td style="width: 10%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td align="right" style="width: 10%">
                <strong>Gender:</strong></td>
            <td style="width: 20%">
                <asp:CheckBox ID="chckbGendm" runat="server" Font-Bold="True" Text="M" />
&nbsp;
                <asp:CheckBox ID="chckbgendf" runat="server" Font-Bold="True" Text="F" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 60%">
                <strong>Care Giver Full Names (If Victim Is A Minar):</strong></td>
            <td style="width: 40%; border-bottom-style: dotted;">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="border-bottom-style: dotted">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 25%">
                <strong>Residential Address:</strong></td>
            <td style="border-bottom-style: dotted; width: 75%;">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="border-bottom-style: dotted">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 20%">
                <strong>Telephone (H)</strong></td>
            <td style="width: 15%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td align="right" style="width: 20%">
                <strong>Telephone (W)</strong></td>
            <td style="width: 15%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td style="width: 30%">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td style="width: 10%">
                <strong>Cell:</strong></td>
            <td style="width: 15%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td align="right" style="width: 15%">
                <strong>Other</strong></td>
            <td style="width: 30%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr>
            <td style="width: 35%">
                <strong>Intervention Service / Referrals</strong></td>
            <td>
                <asp:CheckBox ID="chckbIntvry" runat="server" Font-Bold="True" Text="Y" />
                <asp:CheckBox ID="chckIntvn" runat="server" Font-Bold="True" Text="N" />
            </td>
        </tr>
    </table>
    <br />
    <span class="style4"><strong>H: DEVELOPMENTAL ASSESSMENT</strong></span><br />
    <br />
    <table class="style1">
        <tr>
            <td>
                <strong>Belonging</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox13" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <strong>Mastery</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox14" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <strong>Indepedence:</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox15" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <strong>Generosity:</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox16" runat="server" Height="66px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <span class="style4"><strong>I: EVALUATION</strong></span><br />
    <br />
    <table class="style1">
        <tr>
            <td>
                <asp:TextBox ID="TextBox17" runat="server" Height="129px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <span class="style4"><strong>J: RECOMMENDATION</strong></span><br />
    <br />
    <table class="style1">
        <tr>
            <td>
                <p>
                    <asp:CheckBox ID="CheckBox13" runat="server" Font-Bold="True" 
                        Text="Diversion" />
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox18" runat="server" Height="100px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr>
            <td>
                <p>
                    <asp:CheckBox ID="CheckBox14" runat="server" Font-Bold="True" 
                        Text="Conversion" />
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox19" runat="server" Height="100px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr>
            <td style="width: 40%">
                <p>
                    <asp:CheckBox ID="CheckBox15" runat="server" Font-Bold="True" 
                        Text="Placement" />
                </p>
            </td>
            <td>
                <strong>Placement Confirmed: </strong>
                <asp:CheckBox ID="chcky" runat="server" Font-Bold="True" Text="Y" />
                <asp:CheckBox ID="chckn" runat="server" Font-Bold="True" Text="N" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="TextBox20" runat="server" Height="100px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr>
            <td>
                <asp:CheckBox ID="CheckBox16" runat="server" Font-Bold="True" 
                    Text="Normal court procedures to be followed" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox21" runat="server" Height="100px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <span class="style4"><strong>L: EFFORTS TO TRACE FAMILY</strong></span><br />
    <br />
    <table class="style1">
        <tr>
            <td>
                <asp:TextBox ID="TextBox22" runat="server" Height="100px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <span class="style4"><strong>M: GENERAL</strong></span><br />
    <br />
    <table class="style1">
        <tr>
            <td style="width: 30%">
                <strong>Assessment Time [Finish]:</strong></td>
            <td style="width: 20%; border-bottom-style: dotted;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr>
            <td>
                <strong>Additional Information:</strong></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox23" runat="server" Height="100px" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <table class="style1">
        <tr>
            <td style="border-width: thin; border-color: #000000; width: 50%; border-bottom-style: solid;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50%">
                <strong>Probation Officer:</strong></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
    <table class="style1">
        <tr>
            <td style="border-width: thin; border-color: #000000; width: 25%; border-bottom-style: solid;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 25%">
                <strong>Date:</strong></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
    </form>
    </body>
</html>
