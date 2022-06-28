<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInformasiGrade.aspx.vb" Inherits="FrmInformasiGrade" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmInformasiGrade</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery.min.js"></script>
    <script language="javascript">
        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function CekBlankToZerro(obj) {
            if(trim(obj.value) == '')
            {
                obj.value = 0;
            }
        }

    </script>
</head>
<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <input id="hdnFleetGradeRetentionID1" type="hidden" value="0" runat="server">
            <input id="hdnFleetGradeRetentionID2" type="hidden" value="0" runat="server">
            <input id="hdnFleetGradeDiscountID" type="hidden" value="0" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DISCOUNT PROPOSAL - Informasi Grade</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
            </TABLE>

            <div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="KEPEMILIKAN KENDARAAN" Font-Size="13px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                </table>
            </div>
                
            <div style="width: 50%">
                <table width="100%">	
            	    <TR>
					    <TD vAlign="top" align="left">
                            <table width="100%">
                                <tr>
                                    <td width="50%" valign="top">
						                <TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							                <TR>
								                <TD class="titleField" width="24%">Grade</TD>
								                <TD width="1%">:</TD>
								                <td width="75%"><asp:dropdownlist id="ddlGrade1" runat="server"></asp:dropdownlist></td>
							                </TR>
							                <TR valign="top">
								                <TD class="titleField" width="24%">Operator</TD>
								                <TD width="1%">:</TD>
								                <td width="75%"><asp:dropdownlist id="ddlOperator1" runat="server" AutoPostBack="true"></asp:dropdownlist>
								                </td>
							                </TR>
						                </TABLE>
                                    </td>
                                    <td width="50%" valign="top">
						                <TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							                <TR valign="center">
								                <TD class="titleField" width="24%">Unit</TD>
								                <TD width="1%">:</TD>
								                <td width="75%">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtUnitFrom1" Style="text-align: right" runat="server" Text="0" onblur="CekBlankToZerro(this)"
                                                                    onkeypress="return NumericOnlyWith(event,'');" Width="40px" />
                                                            </td>
                                                            <td><asp:Label Runat="server" ID="lblStrip1" Text="-" Visible="false"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnitTo1" Visible="false" Style="text-align: right" runat="server" Text="0" onblur="CekBlankToZerro(this)"
                                                                    onkeypress="return NumericOnlyWith(event,'');" Width="40px" />
                                                            </td>
                                                        </tr>
                                                    </table>
								                </td>
							                </TR>
							                <TR valign="top">
								                <TD class="titleField"colspan="3">                                                
									                <asp:button id="btnSearch1" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button>&nbsp;
                                                    <asp:Button ID="btnSimpan1" runat="server" Text="Simpan" OnClientClick="return confirm('Anda yakin mau simpan data ini?');"  />&nbsp;
                                                    <asp:button id="btnBatal1" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;
									                <asp:button id="btnKembali1" runat="server" Width="64px" Text="Kembali" CausesValidation="False"></asp:button>
                                                </TD>
							                </TR>
						                </TABLE>
                                    </td>
                                </tr>
                                <tr align="center">
								    <td colspan="2"></td>
                                </tr>
                            </table>
					    </TD>
				    </TR>
                </table>
            </div>

			<div id="div1" style="OVERFLOW: auto; HEIGHT: auto;width:50%">
                <asp:datagrid id="dtgFleetGradeRetention1" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
					BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="False"
                    AllowPaging="True" AllowSorting="True" PageSize="10" AllowCustomPaging="True">
					<FooterStyle ForeColor="#003399" VerticalAlign="Top" BackColor="White"></FooterStyle>
					<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
					<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
							<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblNo"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Grade" SortExpression="Grade">
                            <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblGrade" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Operator" SortExpression="Operators">
                            <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblOperator" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Unit" SortExpression="UnitFrom">
                            <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
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
				</asp:datagrid>
			</div>
            <div><br /></div>
            <div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="JUMLAH UNIT PEMESANAN" Font-Size="13px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td background="../images/bg_hor.gif" height="1" colspan="2">
                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                    </tr>
                </table>
            </div>
                
            <div style="width: 70%">
                <table width="100%">	
            	    <TR>
					    <TD vAlign="top" align="left">
                            <table width="100%">
                                <tr>
                                    <td width="40%" valign="top">
						                <TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							                <TR>
								                <TD class="titleField" width="320px">Grade</TD>
								                <TD width="1%">:</TD>
								                <td width="55%"><asp:dropdownlist id="ddlGrade2" runat="server"></asp:dropdownlist></td>
							                </TR>
							                <TR valign="top">
								                <TD class="titleField" width="320px">Kode Kendaraan</TD>
								                <TD width="1%">:</TD>
								                <td width="55%"><asp:TextBox ID="txtVehicleType" runat="server" Width="220px" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
							                </TR>
						                </TABLE>
                                    </td>
                                    <td width="5%"></td>
                                    <td width="40%" valign="top">
						                <TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							                <TR valign="top">
								                <TD class="titleField" width="24%">Operator</TD>
								                <TD width="1%">:</TD>
								                <td width="75%"><asp:dropdownlist id="ddlOperator2" runat="server" AutoPostBack="true"></asp:dropdownlist>
								                </td>
							                </TR>
							                <TR valign="center">
								                <TD class="titleField" width="24%">Unit</TD>
								                <TD width="1%">:</TD>
								                <td width="75%">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtUnitFrom2" Style="text-align: right" runat="server" Text="0" onblur="CekBlankToZerro(this)"
                                                                    onkeypress="return NumericOnlyWith(event,'');" Width="40px" />
                                                            </td>
                                                            <td><asp:Label Runat="server" ID="lblStrip2" Text="-" Visible="false"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnitTo2" Visible="false" Style="text-align: right" runat="server" Text="0" onblur="CekBlankToZerro(this)"
                                                                    onkeypress="return NumericOnlyWith(event,'');" Width="40px" />
                                                            </td>
                                                        </tr>
                                                    </table>
								                </td>
							                </TR>
							                <TR valign="top">
								                <TD class="titleField"colspan="3">                                                
									                <asp:button id="btnSearch2" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button>&nbsp;
                                                    <asp:Button ID="btnSimpan2" runat="server" Text="Simpan" OnClientClick="return confirm('Anda yakin mau simpan data ini?');"  />&nbsp;
                                                    <asp:button id="btnBatal2" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;
									                <asp:button id="btnKembali2" runat="server" Width="64px" Text="Kembali" CausesValidation="False"></asp:button>
                                                </TD>
							                </TR>
						                </TABLE>
                                    </td>
                                </tr>
                                <tr align="center">
								    <td colspan="4"></td>
                                </tr>
                            </table>
					    </TD>
				    </TR>
                </table>
            </div>

		    <div id="div10" style="OVERFLOW: auto; HEIGHT: auto;width: 65%">
                <asp:datagrid id="dtgFleetGradeRetention2" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
					BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="False"
                    AllowPaging="True" AllowSorting="True" PageSize="10" AllowCustomPaging="True">
					<FooterStyle ForeColor="#003399" VerticalAlign="Top" BackColor="White"></FooterStyle>
					<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
					<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<HeaderStyle VerticalAlign="Top"></HeaderStyle>
				    <Columns>
					    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
						    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
						    <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
					    </asp:BoundColumn>
					    <asp:TemplateColumn HeaderText="No">
						    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
						    <ItemTemplate>
							    <asp:Label Runat="server" ID="lblNo"></asp:Label>
						    </ItemTemplate>
					    </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Grade" SortExpression="Grade">
                            <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
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
                        <asp:TemplateColumn HeaderText="Operator" SortExpression="Operators">
                            <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblOperator" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Unit" SortExpression="UnitFrom">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
					    <asp:TemplateColumn>
						    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
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
			    </asp:datagrid>
		    </div>
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
