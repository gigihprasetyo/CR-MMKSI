<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBlokPengajuanFaktur.aspx.vb" Inherits=".FrmBlokPengajuanFaktur" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    	<HEAD>
		<title>Master Assessment Form</title> 
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
            <script language="javascript">
                function ShowPPChassisMasterSelection() {
                    showPopUp('../SparePart/../PopUp/PopUpBlokPengajuanFaktur.aspx', '', 500, 760, ChassisMasterSelection);
                }

                function ChassisMasterSelection(selectedNumber) {
                    var txtChassisNumber = document.getElementById("txtChassisNumber");
                    txtChassisNumber.value = selectedNumber;
                }
            </script>
	</HEAD>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="FrmDealAssessmentMaster" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0" >
            <tr>
                <td class="titlePage">Sales Faktur - Blok Pengajuan Faktur</td>
            </tr>
            <tr>
				<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
			</tr>
			<tr>
				<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
			</tr>
            <tr>
                <td align="left">
                    <TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
								<TD class="titleField" style="WIDTH: 98px">Chassis Number</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262">
                                    <asp:TextBox runat="server" ID="txtChassisNumber" Width="230px" />
                                    <asp:label id="lblSearchChassisNumber" runat="server">
									<img style="cursor:hand" alt="Klik Disini untuk memilih Chassis Number" src="../images/popup.gif"
									border="0"></asp:label>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="WIDTH: 120px">Remark Notification</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 330px" width="262">
                                    <asp:TextBox runat="server" Height="80px" ID="txtRemarkNotification" Width="250px" TextMode="MultiLine" ></asp:TextBox>
								</td>
							</TR>

                        <tr><td></td><td></td></tr>
                        <tr>
                            <td></td><td></td>
                            <td>
                                &nbsp;
                                <asp:Button Text="Tambah" ID="btnTambah" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Simpan" ID="btnSimpan" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Cari" ID="btnCari" runat="server" Width="75px" />
                            </td>
                        </tr>
                    </TABLE>
                </td>
            </tr>
            <tr><td></td></tr>
            <tr>
                <td valign="top" >
                    <div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
                        <asp:datagrid id="dtgChassisMaster" runat="server" Width="100%" PageSize="50" CellPadding="3"
								BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
								AllowSorting="True" Font-Names="Microsoft Sans Serif">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
									BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID">
									</asp:BoundColumn>
									
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									<asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" HeaderText="Chassis Number">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									
									<asp:BoundColumn DataField="PendingDesc" SortExpression="PendingDesc" HeaderText="Remark">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>

                                    <asp:BoundColumn DataField="ErrorMessage" FooterStyle-Wrap="true" HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete">
											    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton id="lbtnUnblock" runat="server" Width="20px" Text="UnBlock" CausesValidation="False" CommandName="UnBlock">
											    <img src="../images/aktif.gif" border="0" alt="UnBlock"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton id="lbtnBlock" runat="server" Width="20px" Text="Block Faktur" CausesValidation="False" CommandName="Block">
											    <img src="../images/in-aktif.gif" border="0" alt="Block"></asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
                    </div>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>

