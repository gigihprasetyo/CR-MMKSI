<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventReport.aspx.vb" Inherits="FrmEventReport" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventReport</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" type="text/javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
			}
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}

		    function ShowPPDealerGroupSelection()
		    {
			    showPopUp('../PopUp/PopUpGroupDealerSelection.aspx?x=Territory','',500,760,DealerGroupSelection);
		    }
		    function DealerGroupSelection(selectedDealer)
		    {
			    var txtGroupDealer= document.getElementById("txtGroupDealer");
			    txtGroupDealer.value = selectedDealer;				
		    }
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 31px">EVENT&nbsp;-&nbsp;List Event Report</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="18%">
									<asp:RadioButton id="rbDealerCode" Checked="True" runat="server" Text="Kode Dealer" GroupName="dealer"></asp:RadioButton></td>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px">
									<table cellSpacing="0" cellPadding="0">
										<tr>
											<td style="WIDTH: 282px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"
													runat="server" Width="232px"></asp:textbox>
												<asp:label id="lblSearchDealer" runat="server">
													<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
														border="0"></asp:label></td>
											<td></td>
											<TD></TD>
										</tr>
									</table>
								</TD>
								<TD class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Area</TD>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlSalesmanArea" Width="142px" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px" width="18%">
									<asp:RadioButton id="rbDealerGroup" runat="server" Text="DealerGroup" GroupName="dealer"></asp:RadioButton></TD>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px">
									<asp:textbox onkeypress="TxtKeypress();" id="txtGroupDealer" onblur="TxtBlur('txtGroupDealer');"
										runat="server" Width="232px"></asp:textbox>
									<asp:label id="lblSearchGroupDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Tanggal Acara</TD>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px">
									<cc1:inticalendar id="dtmEvent" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:checkbox id="cbPeriode" Runat="server" Text="Periode Kegiatan"></asp:checkbox></TD>
								<TD style="WIDTH: 1px" width="1">:</TD>
								<TD width="82%" colSpan="5">
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<td></td>
											<TD><cc1:inticalendar id="calDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
											<TD>&nbsp;&nbsp;s.d&nbsp;&nbsp;</TD>
											<TD><cc1:inticalendar id="calSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Jenis Kegiatan</TD>
								<TD style="WIDTH: 1px" width="1">:</TD>
								<TD colSpan="5">
									<asp:dropdownlist id="ddlJenisKegiatan" Runat="server" AutoPostBack="True"></asp:dropdownlist>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Nama Kegiatan</TD>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px" colSpan="5">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlNamaKegiatan" Runat="server"></asp:dropdownlist></td>
											<td class="titleField" style="WIDTH: 50px; HEIGHT: 18px; TEXT-ALIGN: right">Tahun</td>
											<TD style="WIDTH: 10px; HEIGHT: 18px; TEXT-ALIGN: center" width="1">:</TD>
											<td><asp:dropdownlist id="ddlYear" Runat="server"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<td class="titleField"></td>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD colSpan="5">
									<asp:button id="btnCari" runat="server" Width="65px" Text="Cari" CausesValidation="False" Visible="False"></asp:button>
									<asp:Button id="btnSave" runat="server" Width="65px" Text="Simpan"></asp:Button>
									<input type="button" value="Kembali" onClick="history.go(-1)">
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</TABLE>
			<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="1988">
				<asp:datagrid id="dtgEvent" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
					AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" ShowFooter="True">
					<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle ForeColor="White"></HeaderStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id"></asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
							<HeaderTemplate>
								<INPUT id="chkAllItems" onclick="CheckAll('chkSelect',document.all.chkAllItems.checked)"
									type="checkbox">
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Kategori Kendaraan">
							<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblVechileCategory" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Category.Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList AutoPostBack="True" Runat="server" ID="ddlfVechileCategory" OnSelectedIndexChanged="ddlCarCategory_SelectedIndexChanged"></asp:DropDownList>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:DropDownList AutoPostBack="True" Runat="server" ID="ddleVechileCategory" OnSelectedIndexChanged="ddlCarCategory_SelectedIndexChanged"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Tipe Kendaraan">
							<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblVechileType" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList Runat="server" ID="ddlfVechileType"></asp:DropDownList>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:DropDownList Runat="server" ID="ddleVechileType"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Jumlah">
							<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id=lblQty runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Jumlah") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id="txtfQty" runat="server" MaxLength="9"
									Width="100%"></asp:TextBox>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txteQty runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Jumlah") %>' MaxLength="9" Width="100%">
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Deskripsi">
							<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:TextBox id="txtfdesc" runat="server" Width="100%"></asp:TextBox>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txteDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Width="100%">
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:LinkButton id=lnbCardEdit runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
									<img src="../images/edit.gif" border="0" alt="Ubah">
								</asp:LinkButton>
								<asp:LinkButton id=lnbCarDelete runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus">
								</asp:LinkButton>
							</ItemTemplate>
							<FooterTemplate>
								<asp:LinkButton id="lnkAdd" runat="server" CausesValidation="false" CommandArgument="0" CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah">
								</asp:LinkButton>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:LinkButton id=lnbCarUpdate runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Save">
									<img src="../images/simpan.gif" border="0" alt="Simpan">
								</asp:LinkButton>
								<asp:LinkButton id="lnbCarCancel" runat="server" CausesValidation="false" CommandArgument="0" CommandName="Cancel">
									<img src="../images/batal.gif" border="0" alt="Batal">
								</asp:LinkButton>
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
			</DIV>
		</form>
	</body>
</HTML>
