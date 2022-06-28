<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmServiceReminder.aspx.vb" Inherits=".FrmServiceReminder" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmFSKindOnVechileType</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">SERVICE -&nbsp; Service Reminder (1.000 KM s.d 50.000 KM)</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField">Chassis Number</td>
                            <td>:</td>
                            <td>
                                <asp:CheckBox ID ="chkchassisnumber" runat="server" Visible="false"/>
                                <asp:TextBox ID="txtChassissNumber" Width="150px" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtChassissNumber" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                <%--<asp:label id="lblPopChassisNumber" runat="server" width="10">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>--%>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" valign="top">Tanggal PKT</td>
                            <td valign="top">:</td>
                            <td valign="top">
                                <table cellspacing="0">
                                    <tr>
                                        <td><asp:CheckBox ID="chkPKTDate" runat="server" /></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPktFrom" runat="server"></cc1:IntiCalendar>
                                        </td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPKTTo" runat="server"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" valign="top">Tanggal Faktur</td>
                            <td valign="top">:</td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td><asp:CheckBox ID="chkFactureDate" runat="server" /></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icOpenFacturDateFrom" runat="server"></cc1:IntiCalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icOpenFacturDateTo" runat="server"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">FS Kind</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList  ID="ddlFSKind" Width="150px" runat="server"></asp:DropDownList>                               
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td colspan="2" style="align-content: center; align-items: center; align-self: center">
                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button>
                                <asp:Button ID="btnDownload" runat="server" Width="70px" Text="Download" Height="22px"></asp:Button>
                            </td>
                        </tr>
				        <TR>
                            <td></td>
                            <td></td>
					        <TD vAlign="top" align="left">
                                <asp:Label ID="Label1" runat="server" Text="Catatan:" ForeColor="Red"></asp:Label>
					        </TD>
				        </TR>
				        <TR>
                            <td></td>
                            <td></td>
					        <TD vAlign="top" align="left">
                                <asp:Label ID="lblKeterangan" runat="server" Text="1. Kendaraan PKT sebelum September, validasi dari 
                                            tanggal faktur. Kendaraan PKT September, validasi dari tanggal PKT" ForeColor="Red"></asp:Label>
					        </TD>
				        </TR>
				        <TR>
                            <td></td>
                            <td></td>
					        <TD vAlign="top" align="left">
                                <asp:Label ID="Label4" runat="server" Text="2. Untuk Kendaraan Expired Periode 13 Maret - 31 Mei 2020, 
                                                MMKSI akan menerima Claim Gratis Service sampai batas waktu yg wajar" ForeColor="Red"></asp:Label>
					        </TD>
				        </TR>
                        <tr>
                            <td valign="top" colspan=" 3">
                                <div id="div1" style="overflow: auto; height: 350px">
                                    <asp:DataGrid ID="dtgFSReminder" runat="server" Width="100%" CellSpacing="1" AllowSorting="True"
                                        PageSize="50" AllowPaging="True" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF"
                                        AutoGenerateColumns="False" ShowFooter="True" AllowCustomPaging="True" ForeColor="Gray">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama" SortExpression="Name">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No HP">
                                                <HeaderStyle ForeColor="White" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kota">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='' ID="lblCity">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor Chassiss">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblchassisNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tanggal Buka Faktur">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOpenFakturDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tanggal Faktur">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFakturDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PKT Date" SortExpression="PKTDate">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPKTDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe FS">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFSKind">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Expired Dari PKT" SortExpression="ExpiredDateByPKTDate" >
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblExpiredDateByPKTDate">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Expired Dari Faktur" SortExpression="ExpiredDateByOpenFakturDate">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblExpiredDateByOpenFakturDate">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="KM">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblKM">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
