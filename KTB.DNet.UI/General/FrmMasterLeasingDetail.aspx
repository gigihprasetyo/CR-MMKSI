<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterLeasingDetail.aspx.vb" Inherits="FrmMasterLeasingDetail" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="5" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" colSpan="2">UMUM - Master Leasing Detail</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" colSpan="2" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>

                <tr>
					<td colspan="2">
                        <asp:Panel ID="formLeasing" runat="server" >
                            <table>
                                <tr>
					                <td class="titleField" width="10%">Kode Leasing&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblCode" runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Nama Leasing&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblName"  runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Leasing Group Name&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblGroupName" runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="15%">Propinsi&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblProvince" 
							                runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="15%">Kota&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblCity" 
							                runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Alamat&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblAlamat" runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Postal Code&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblPostalCode" runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Phone Number&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblPhoneNo" runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Email&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblEmail" runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Contact Person&nbsp;</td>
								    <TD width="3%" align="center">:</TD>
					                <td><asp:label id="lblCP" runat="server" Width="242px"></asp:label></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Handphone Number&nbsp;</td>
								    <TD width="3%" align="center" align="center">:</TD>
					                <td><asp:label id="lblHP" runat="server" Width="242px"></asp:label></td>
				                </tr>
				                <tr>
					                <td class="titleField" width="20%">&nbsp;</td>
								    <TD width="3%" align="center"></TD>
					                <td><br /> 
                                        <input id="btnKembali" type="button" value="Kembali" onclick="javascript:history.back()" />&nbsp;          
					                </td>
				                </tr>

                            </table>

                        </asp:Panel>
                    </td>
				</tr>
                 <tr>
					<td colspan="2">
                         
                    </td>
				</tr>
				
				
			</TABLE>
		</form>
	</body>
</HTML>
