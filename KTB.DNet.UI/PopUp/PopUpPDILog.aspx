<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPDILog.aspx.vb" Inherits=".PopUpPDILog" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <head>
		<title>FrmPopUpPDILog</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
        <base target="_self">
</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
		<table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
			<tr>
                <td class="titleTableParts3" colspan="3">Popup PDI Kadaluarsa</td>
            </tr>
			<tr valign="top">
				<td class="titleField">Nomor Rangka</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtChassisNumber" runat="server"></asp:TextBox>
                </td>
			</tr>
            <tr valign="top">
				<td class="titleField">Tanggal PDI</td>
                <td>:</td>
                <td>
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:CheckBox ID="CBTglPDI" runat="server" Checked="true" />
                            </td>
                            <td>
                                <cc1:IntiCalendar ID="icTglPDIFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                            </td>
                            <td>&nbsp;s/d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="icTglPDITo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                            </td>
                        </tr>
                    </table>
                </td>
			</tr>
            <tr valign="top">
				<td class="titleField">Tanggal Kadaluarsa</td>
                <td>:</td>
                <td>
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:CheckBox ID="CBTglExpired" runat="server" />
                            </td>
                            <td>
                                <cc1:IntiCalendar ID="icTglExpiredFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                            </td>
                            <td>&nbsp;s/d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="icTglExpiredTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                            </td>
                        </tr>
                    </table>
                </td>
			</tr>
            <tr valign="top">
				<td class="titleField">Nomor WO</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtWONumber" runat="server"></asp:TextBox>
                </td>
			</tr>
            <tr>
                <td colspan="2"></td>
				<td align="left">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Cari" />
				</td>
			</tr>
            <tr>
                <td colspan="3">
                    <div id="div1" style="overflow: auto; max-height: 300px">
                        <asp:DataGrid ID="dtgPDILog" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
                            AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3"
                            OnPageIndexChanged="dtgPDILog_PageIndexChanged"
                            OnSortCommand="dtgPDILog_SortCommand"
                            OnItemDataBound="dtgPDILog_ItemDataBound"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" PageSize="10">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nomor Rangka" SortExpression="PDI.ChassisMaster.ChassisNumber">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChassisNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PDI.ChassisMaster.ChassisNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal PDI" SortExpression="PDI.PDIDate">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDIDate" runat="server" Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.PDI.PDIDate"), Microsoft.VisualBasic.DateFormat.ShortDate)%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Kadaluarsa" SortExpression="ExpiredPDIDate">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpiredPDIDate" runat="server" Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.ExpiredPDIDate"), Microsoft.VisualBasic.DateFormat.ShortDate)%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nomor WO" SortExpression="PDI.WorkOrderNumber">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkOrderNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PDI.WorkOrderNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
			<tr>
				<td align="center" colspan="3">
                    <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup" name="btnCancel" />
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
