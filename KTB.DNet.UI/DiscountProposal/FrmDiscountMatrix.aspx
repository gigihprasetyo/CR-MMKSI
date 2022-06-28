<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDiscountMatrix.aspx.vb" Inherits="FrmDiscountMatrix" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDiscountMatrix</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function CekBlankToZerro(obj) {
            if (trim(obj.value) == '') {
                obj.value = 0;
            }
        }

    </script>
</head>
<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <input id="hdnFleetGradeDiscountID" type="hidden" value="0" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DISCOUNT PROPOSAL - Discount Matrix</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
                        <table width="100%">
                            <tr>
                                <td width="50%" valign="top">
						            <TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							            <TR>
								            <TD class="titleField" width="24%">Grade</TD>
								            <TD width="1%">:</TD>
								            <td width="75%"><asp:dropdownlist id="ddlGrade" runat="server"></asp:dropdownlist>
                                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkInformasiGrade" runat="server"><b><i>Informasi Grade</i></b></asp:LinkButton>
								            </td>
							            </TR>
							            <TR valign="top">
								            <TD class="titleField" width="24%">Kode Kendaraan</TD>
								            <TD width="1%">:</TD>
								            <td width="75%"><asp:TextBox ID="txtVehicleType" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
							            </TR>
						            </TABLE>
                                </td>
                                <td width="50%" valign="top">
						            <TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							            <TR valign="center">
								            <TD class="titleField" width="24%">Periode Berlaku</TD>
								            <TD width="1%">:</TD>
								            <td width="75%">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="cbPeriode" runat="server" /></td>
                                                        <td>
                                                            <cc1:IntiCalendar ID="icPeriodStart" runat="server" TextBoxWidth="65"></cc1:IntiCalendar>
                                                        </td>
                                                        <td>s/d</td>
                                                        <td>
                                                            <cc1:IntiCalendar ID="icPeriodEnd" runat="server" TextBoxWidth="65"></cc1:IntiCalendar>
                                                        </td>
                                                    </tr>
                                                </table>
								            </td>
							            </TR>
							            <TR valign="top">
								            <TD class="titleField" width="24%">Jumlah Diskon</TD>
								            <TD width="1%">:</TD>
								            <td width="75%">&nbsp;<asp:TextBox ID="txtDiscount" Style="text-align: right" runat="server" Text="0" onblur="CekBlankToZerro(this)"
                                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="120px" /></td>
							            </TR>
						            </TABLE>
                                </td>
                            </tr>
                            <tr align="center">
								<td colspan="2">
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClientClick="return confirm('Anda yakin mau simpan data ini?');"  />&nbsp;
                                    <asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;
									<asp:button id="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button></td>
                            </tr>
                        </table>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 360px">
                            <asp:datagrid id="dtgFleetGradeDiscount" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None"
								BorderWidth="0px" BackColor="#CDCDCD" GridLines="None" BorderColor="#CDCDCD" CellPadding="3" PageSize="50" AllowCustomPaging="True" AllowPaging="True"
								AllowSorting="True" CellSpacing="1" Font-Names="Microsoft Sans Serif">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
									BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Grade" SortExpression="Grade">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrade" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Kode Kendaraan" SortExpression="VehicleType">
                                        <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVehicleType" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Jumlah Diskon" SortExpression="Discount">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Periode Berlaku" SortExpression="PeriodStart">
                                        <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
		    if (window.parent == window) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            self.opener = null;
		            self.close();
		        }
		        else {
		            this.name = "origWin";
		            origWin = window.open(window.location, "origWin");
		            window.opener = top;
		            window.close();
		        }
		    }
		</script>
	</body>
</html>
