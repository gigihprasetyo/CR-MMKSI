<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpRevisionStatusHistory.aspx.vb" Inherits=".PopUpRevisionStatusHistory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>Daftar Perubahan Status</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
</head>
<body MS_POSITIONING="GridLayout">
    <form id="form1" method="post" runat="server">
        <table id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
            <tr>
                <td class="titleField" width="34%" style="height: 17px">
                    <asp:Label ID="lblRevisionType" runat="server">Jenis Revisi</asp:Label></td>
                <td width="1%" style="height: 17px">:</td>
                <td width="35%" style="height: 17px">
                    <asp:Label ID="lblRevisionTypeValue" runat="server"></asp:Label></td>
                <td width="30%" style="height: 17px"></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblRegNumber" runat="server">Nomor Pengajuan Revisi</asp:Label></td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblRegNumberValue" runat="server"></asp:Label></td>
                <td width="50%"></td>
            </tr>
            <tr valign="top" height="250">
                <td colSpan="4">
                    <asp:DataGrid id="dtgStatusChangeHistory" runat="server" AutoGenerateColumns="False" Width="100%">
						<Columns>
							<asp:TemplateColumn HeaderText="No">
								<HeaderStyle HorizontalAlign="Center" Width="2%" CssClass="titleTableSales"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:BoundColumn DataField="OldStatus" HeaderText="Status Lama">
								<HeaderStyle HorizontalAlign="Center" Width="26%" CssClass="titleTableSales"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
                            <asp:BoundColumn DataField="NewStatus" HeaderText="Status Baru">
								<HeaderStyle HorizontalAlign="Center" Width="26%" CssClass="titleTableSales"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="ProcessDate" HeaderText="Diproses Tanggal" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
								<HeaderStyle HorizontalAlign="Center" Width="26%" CssClass="titleTableSales"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="ProcessBy" HeaderText="Diproses Oleh">
								<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableSales"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
						</Columns>
					</asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <input id="btnCancel" style="width: 55px" onclick="window.close()" type="button" value="Tutup" name="btnCancel">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
