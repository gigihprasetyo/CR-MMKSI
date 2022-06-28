<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInvoiceRevision.aspx.vb" Inherits="FrmInvoiceRevision" smartNavigation="False"%>
<%@ Register TagPrefix="domain" Namespace="KTB.DNet.Domain" Assembly="KTB.DNet.Domain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
	<title>REVISI FAKTUR - INPUT REVISI FAKTUR</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
	<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
	<script language="javascript" src="../WebResources/FormFunctions.js" type="text/javascript">></script>
	<script language="javascript" type="text/javascript">
		function ShowPPDealerBranchSelection() {
			var lblDealer = document.getElementById("lblDealerCode");
			var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
			showPopUp('../FinishUnit/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
		}

		function TemporaryOutlet(selectedDealer) {
			if (selectedDealer.indexOf(";") > 0) {
				var txtDealerSelection = document.getElementById("txtDealerBranchCode");
				txtDealerSelection.value = selectedDealer.split(";")[0];
			}
			else {
				var txtDealerSelection = document.getElementById("txtDealerBranchCode");
				txtDealerSelection.value = selectedDealer;
			}
		}

		function ShowPPTujuanSelection() {
			showPopUp('../PopUp/PopUpCustomerSelectionOne.aspx?FilterLoginDealer=True', '', 500, 760, TujuanSelection);
		}

		function TujuanSelection(selectedTujuan) {
			var txtCustomerCode = document.getElementById('txtCustomerCode');

			selectedTujuan = selectedTujuan.replace(/&amp;/g, '&');//&amp=>'&'
			var arrValue = selectedTujuan.split(';');
			txtCustomerCode.value = arrValue[0];

			if (navigator.appName == 'Microsoft Internet Explorer') {
				txtCustomerCode.focus();
				txtCustomerCode.blur();
			}
		}
	</script>
</head>
<body MS_POSITIONING="GridLayout" onload="javascript:window.history.forward(1);">
	<form id="form1" method="post" runat="server">
		<table id="table1" cellSpacing="1" cellPadding="1" width="100%">
			<tr>
				<td>
					<table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0">
						<tr>
							<td class="titlePage" style="height: 17px">REVISI FAKTUR - INPUT REVISI FAKTUR</td>
						</tr>
						<tr>
							<td background="../images/bg_hor.gif" height="1"><img height="1" alt="" src="../images/bg_hor.gif" border="0"></td>
						</tr>
						<tr>
							<td style="height: 6px" height="6"><img height="1" alt="" src="../images/dot.gif" border="0"></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table id="Table3" cellspacing="1" cellpadding="2" width="100%">
						<tr>
							<td class="titleField" style="height: 11px" width="10%">
								<asp:label id="lblDealer" runat="server">Kode Dealer</asp:label></td>
							<td width="1%">
								<asp:label id="Label1" runat="server">:</asp:label></td>
							<td width="24%">
								<asp:Label ID="lblDealerCode" runat="server"></asp:Label> / <asp:Label ID="lblNamaDealer" runat="server"></asp:Label></td>
							
							<td class="titleField" style="height: 11px" width="10%">
								<asp:Label ID="lblCategory" runat="server"> Kategori</asp:Label></td>
							<td style="height: 11px" width="1%">
								<asp:Label ID="lblColon4" runat="server">:</asp:Label></td>
							<td style="height: 11px">
								<asp:DropDownList ID="ddlCategory" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>  
								<br />
								<br />
								<asp:DropDownList ID="ddlSubCategory" runat="server" Width="150px"></asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td class="titleField" style="height: 11px" width="10%">
								<asp:label id="Label2" runat="server">Cabang Dealer</asp:label></td>
							<td width="1%">
								<asp:label id="Label3" runat="server">:</asp:label></td>
							<td width="24%">
								<asp:TextBox ID="txtDealerBranchCode" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
									ToolTip="Cabang Dealer" AutoPostBack="true"></asp:TextBox>
								<asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
									<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
								</asp:Label>
								<asp:Label ID="lblDealerBranch" runat="server" Visible="False"></asp:Label>
							</td>

							<td class="titleField" style="height: 11px" width="10%">
                                <asp:CheckBox ID="chkFakturPeriod" runat="server" Text="Tgl Faktur"></asp:CheckBox></td>
                            <td style="height: 11px" width="1%">
								<asp:Label ID="Label7" runat="server">:</asp:Label></td>
							<td style="height: 11px">
								<table cellspacing="0" cellpadding="0" border="0">
									<tr>
										<td>
											<cc1:inticalendar id="icStartFaktur" runat="server" textboxwidth="70"></cc1:inticalendar>
										</td>
										<td>&nbsp;s/d&nbsp;</td>
										<td>
											<cc1:inticalendar id="icEndFaktur" runat="server" textboxwidth="70"></cc1:inticalendar>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td class="titleField" style="height: 11px" width="10%">
								<asp:Label ID="lblChassisNo" runat="server">Nomor Rangka</asp:Label></td>
							<td width="1%">
								<asp:label id="Label5" runat="server">:</asp:label></td>
							<td width="24%">
								<asp:TextBox ID="txtChassisNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
									runat="server" Width="150px" size="22" MaxLength="20"></asp:TextBox>
							</td>

                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:CheckBox ID="chkValidPeriod" runat="server" Text="Tgl Validasi"></asp:CheckBox></td>
                            <td>
								<asp:Label ID="Label8" runat="server">:</asp:Label></td>
							<td width="34%" nowrap>
								<table cellspacing="0" cellpadding="0" border="0">
									<tr>
										<td>
											<cc1:inticalendar id="icStartValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										<td>&nbsp;s/d&nbsp;</td>
										<td>
											<cc1:inticalendar id="icEndValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td class="titleField">
								<asp:Label ID="lblInvoiceNo" runat="server">Nomor Faktur</asp:Label></td>
							<td width="1%" style="height: 11px">
								<asp:Label ID="Label4" runat="server">:</asp:Label></td>
							 <td width="24%">
								<asp:TextBox ID="txtInvoiceNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtInvoiceNo','<>?*%$;')"
									runat="server" Width="150px" size="22" MaxLength="20"></asp:TextBox></td>

							<td class="titleField" style="height: 11px" width="10%">
                                <asp:CheckBox ID="chkPrintedPeriod" runat="server" Text="Tgl Selesai"></asp:CheckBox></td>
                            <td><asp:Label ID="Label10" runat="server">:</asp:Label></td>
							<td width="34%">
								<table cellspacing="0" cellpadding="0" border="0">
									<tr>
										<td>
											<cc1:inticalendar id="icStartPrinted" runat="server" textboxwidth="70"></cc1:inticalendar>
										</td>
										<td>&nbsp;s/d&nbsp;</td>
										<td>
											<cc1:inticalendar id="icEndPrinted" runat="server" textboxwidth="70"></cc1:inticalendar>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td class="titleField">
								<asp:label id="Label9" runat="server">Kode Konsumen</asp:label></td>
							<td width="1%" style="height: 11px">
								<asp:Label ID="Label11" runat="server">:</asp:Label></td>
							<td width="24%">
								<asp:TextBox ID="txtCustomerCode" runat="server" Width="150px"></asp:TextBox>
								<asp:ImageButton ID="imgCustomer" runat="server" ImageUrl="../images/popup.gif" Visible="False"></asp:ImageButton>
								<asp:Label ID="lblPopUp" runat="server" Width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
							</td>
							
                            <td width="24%">
                                <asp:CheckBox ID="chkIsTemporary" runat="server" Text=" Temporary Faktur" class="titleField"></asp:CheckBox></td>
                            <td colspan="2"></td>
						</tr>
                        <tr><td class="titleField"></td><td></td><td></td><td class="titleField"></td><td></td><td></td></tr>
						<tr>                           
							<td class="titleField"></td>
							<td></td>
							<td style="text-align: right;">
								<asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
							<td class="titleField"></td>
							<td></td>
							<td></td>
						</tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblJumRecord" runat="server"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td valign="top" colspan="6">
                                <div id="divInvoiceList" style="overflow: auto; height: 240px">
                                    <asp:DataGrid ID="dgInvoiceList" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
                                        BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="100" AllowPaging="True"
                                        AllowCustomPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Buat Revisi" Visible="false">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCreate" runat="server" CommandName="lnkCreate">
															<img src="../images/edit.gif" border="0" style="cursor:hand" alt="Buat Revisi">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            
                                            
                                            
                                            
                                                                                       
                                            <asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" ReadOnly="True" HeaderText="Nomor Rangka">
                                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Tipe/Warna">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>' ID="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            
                                            
                                            <asp:TemplateColumn SortExpression="EndCustomer.FakturDate" HeaderText="Tgl Faktur">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSPKCreatedDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndCustomer.FakturDate"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="FakturNumberText" SortExpression="EndCustomer.FakturNumber" ReadOnly="True"
                                                HeaderText="Nomor Faktur Kendaraan">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            
                                            <asp:TemplateColumn SortExpression="EndCustomer.Customer.Name1" HeaderText="Nama Konsumen">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.Customer.Name1")%>' ID="Label7">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="PrintedTimeText" SortExpression="EndCustomer.PrintedTime" ReadOnly="True"
                                                HeaderText="Tgl Selesai">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            
                                            <asp:TemplateColumn SortExpression="EndCustomer.IsTemporary" HeaderText="Temporary Faktur">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# If(Eval("EndCustomer.IsTemporary") > -1, EnumEndCustomer.TemporaryFakturDesc(Eval("EndCustomer.IsTemporary")), "")%>' ID="lblTemporary">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn HeaderText="Detail">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                                                                        

                                            
                                            
                                                                                        
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
					</table>
				</td>
			</tr>
			
		</table>
        <asp:HiddenField ID="hdnMsg" Value="" runat="server" />
	</form>

      <script type="text/javascript">
          var hdnMsg = document.getElementById("hdnMsg");

          if (hdnMsg != null && hdnMsg.value != "") {
              var msg = hdnMsg.value;
              hdnMsg.value = "";
              alert(msg);

          }

    </script>
</body>
</html>
