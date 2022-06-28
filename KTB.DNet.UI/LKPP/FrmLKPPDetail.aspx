<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmLKPPDetail.aspx.vb" Inherits=".FrmLKPPDetail"  smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html  >
<head  >
    <title>LKPP - Entry</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
		
    <script language="javascript">

		    function ShowPPDealerSelection() {
		        showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var txtDealerName = document.getElementById("txtDealerName");
		        txtDealerName.value = selectedDealer;
		    }

		    function ShowPPKodeModelSelection() {
		        showPopUp('../General/FrmModelSelection.aspx?Prod=' + '1', '', 400, 400, KodeTipe)
		    }

		    function GetCurrentInputIndex() {
		        var dgSPDetail = document.getElementById("dgLKPPDetail");
		        var currentRow;
		        var index = 0;
		        var inputs;
		        var indexInput;

		        for (index = 0; index < dgSPDetail.rows.length; index++) {
		            inputs = dgSPDetail.rows[index].getElementsByTagName("INPUT");

		            if (inputs != null && inputs.length > 0) {
		                for (indexInput = 0; indexInput < inputs.length; indexInput++) {
		                    if (inputs[indexInput].type != "hidden")
		                        return index;
		                }
		            }
		        }
		        return -1;
		    }

		    function KodeTipe(selectedType) {
		        var indek = GetCurrentInputIndex();
		        var dgSPDetail = document.getElementById("dgLKPPDetail");
		        var KodeTipe = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[0];
		        KodeTipe.value = selectedType;

		    }

		</script>

   
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 20px;
        }
        .auto-style2 {
            height: 20px;
        }
        .auto-style3 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 14px;
        }
        .auto-style4 {
            height: 14px;
        }
    </style>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="form1" runat="server" method="post">
		<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
            <tr>
				<td class="titlePage" style="HEIGHT: 17px">UMUM&nbsp;- Input LKPP</td>
	        </tr>
            <tr>
				<td background="../images/bg_hor.gif" height="1">
                    <IMG height="1" src="../images/bg_hor.gif" border="0"></td>
			</tr>
			<tr>
				<td style="HEIGHT: 6px" height="6">
                    <IMG height="1" src="../images/dot.gif" border="0"></td>
			</tr>
            <TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR vAlign="top">
								<TD class="auto-style1" width="24%" style="HEIGHT: 20px"><asp:label id="lblLKPPNumber" runat="server">Nomor Pengadaan</asp:label></TD>
								<TD width="1%" class="auto-style2" style="HEIGHT: 20px"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD width="25%" class="auto-style2" style="HEIGHT: 20px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;,{}=+^`~');" id="txtLKPPNumber"
										onblur="omitSomeCharacter('txtLKPPNumber','<>?*%$;');" runat="server" Width="150" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtLKPPNumber" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
								<TD class="auto-style1" width="24%" style="HEIGHT: 20px"><asp:label id="Label6" runat="server">Tanggal Pengajuan</asp:label></TD>
								<TD width="1%" class="auto-style2" style="HEIGHT: 20px"><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD width="25%" class="auto-style2" style="HEIGHT: 20px"><cc1:inticalendar id="icLetterDate" runat="server" TextBoxWidth="70" CanPostBack="False" Enabled="False" Friday="True" Monday="True" Saturday="True" ScriptOnFocusOut="" Sunday="True" TargetForm="" TargetTemporaryFocus="" TargetTextBox="" Thursday="True" Tuesday="True" Value="01/11/2016 14:24:10" Wednesday="True"></cc1:inticalendar></TD>
							</TR>
							<TR vAlign="top">
								<TD class="auto-style3" style="HEIGHT: 20px" width="24%"><asp:label id="lblName" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%" class="auto-style4"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%" class="auto-style4"><asp:label id="txtdealername" runat="server"></asp:label>
                                    <%-- <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtDealerName" ErrorMessage="*">
                                    </asp:requiredfieldvalidator>--%>
                                    <asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="auto-style3" style="HEIGHT: 20px" width="24%"><asp:label id="lblDesc" runat="server">Deskripsi</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%" class="auto-style4"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%" rowspan="2"><asp:textbox id="txtDescription" runat="server" Width="209px" MaxLength="250" TextMode="MultiLine" Height="40px"></asp:textbox></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="lblName0" runat="server">Nama Dealer</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label15" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblNamaDealer" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%">&nbsp;</TD>
								<TD style="HEIGHT: 20px" width="1%">&nbsp;</TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="Label2" runat="server">Metode Pengadaan</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:dropdownlist id="ddlClassification" runat="server" Width="163px"   Height="18px"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"><asp:Label ID="lblNote0" runat="server">Catatan</asp:Label></TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="29%"><asp:textbox id="txtNote0" runat="server" Width="209px" MaxLength="50" TextMode="MultiLine" Height="40px"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="lblCustName" runat="server">Nama Customer</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox id="txtCustName" runat="server" Width="200px" MaxLength="250"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtCustName" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>

							<TR>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top" width="24%"><asp:label id="Label8" runat="server">Attachment</asp:label></TD>
								<TD style="HEIGHT: 20px" vAlign="top" width="1%"><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><INPUT id="inFileLocation" onkeydown="return false;" style="WIDTH: 240px" type="file" name="File1"
										runat="server">
                                    <INPUT id="hdnAttachment" type="hidden" runat="server" name="hdnAttachment">
									<asp:label id="lblAttachment" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top" width="20%" colSpan="3"><asp:label id="Label12" runat="server" Width="231px">*.PDF, *.Zip ( Ukuran Maksimum File 2Mb)</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:dropdownlist id="ddlStatus" runat="server" Width="80px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR id="tr1" runat="server">
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Dibuat Oleh</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDibuatOleh" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Dibuat Pada</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDibuatPada" runat="server"></asp:label></TD>
							</TR>
							<TR id="tr3" runat="server">
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Diubah Oleh</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDiubahOleh" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Diubah Pada</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDiubahPada" runat="server"></asp:label></TD>
							</TR>
							
							<TR>
								<TD class="titleField" colSpan="3">
																<div id="div1" style="OVERFLOW: auto; HEIGHT: 180px" DESIGNTIMEDRAGDROP="275">
            <asp:datagrid id="dgLKPPDetail" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True" >
											<FooterStyle ForeColor="#003399" VerticalAlign="Top" BackColor="White"></FooterStyle>
											<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle VerticalAlign="Top"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Font-Size="Small"></FooterStyle>
												</asp:TemplateColumn>

												<asp:TemplateColumn HeaderText="Tipe">
													<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Width="30%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNamaType" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterKodeModel" runat="server" Width="100" MaxLength="4"></asp:TextBox>
														<asp:RequiredFieldValidator runat="server" ID="RequiredFooterTipe" ErrorMessage="*" ControlToValidate="txtFooterKodeModel"   ValidationGroup="footer"></asp:RequiredFieldValidator>
														<asp:Label id="lblFooterKodeModel" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditKodeModel" runat="server" Width="100" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "VechileType.VechileTypeCode" ) %>' MaxLength="4">
														</asp:TextBox>
														<asp:RequiredFieldValidator runat="server" ID="RequiredEditTipe" ErrorMessage="*" ControlToValidate="txtEditKodeModel"></asp:RequiredFieldValidator>
														<asp:Label id="lblEditKodeModel" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Unit">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="center" Width="10%" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewUnit runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "Unit"), "#,###") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterUnit" runat="server" Width="80" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															MaxLength="11" onkeyup="pic(this,this.value,'9999999999','N')"></asp:TextBox>
														<asp:RequiredFieldValidator runat="server" ID="RequiredFooterUnit" ErrorMessage="*" ControlToValidate="txtFooterUnit" ValidationGroup="footer" ></asp:RequiredFieldValidator>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtEditUnit runat="server" width="80" tooltip="Unit Permintaan harus lebih besar dari 0" MaxLength="11" Text='<%# DataBinder.Eval(Container.DataItem, "Unit") %>' CssClass="textRight" onkeyup="pic(this,this.value,'9999999999','N')" onkeypress="return numericOnlyUniv(event">
														</asp:TextBox>
														<asp:RequiredFieldValidator runat="server" ID="RequiredEditUnit" ErrorMessage="*" ControlToValidate="txtEditUnit"></asp:RequiredFieldValidator>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Sisa Unit">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="center" Width="10%" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewUnitRemain runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "UnitRemain"), "#,###")%>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterUnitRemain" runat="server" Width="80" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															MaxLength="11" onkeyup="pic(this,this.value,'9999999999','N')"></asp:TextBox>
														<asp:RequiredFieldValidator runat="server" ID="RequiredFooterUnitRemain" ErrorMessage="*" ControlToValidate="txtFooterUnitRemain" Enabled="false"></asp:RequiredFieldValidator>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtEditUnitRemain runat="server" width="80" tooltip="Unit Permintaan harus lebih besar dari 0" MaxLength="11" Text='<%# DataBinder.Eval(Container.DataItem, "Unit") %>' CssClass="textRight" onkeyup="pic(this,this.value,'9999999999','N')" onkeypress="return numericOnlyUniv(event">
														</asp:TextBox>
														<asp:RequiredFieldValidator  runat="server" ID="RequiredEditUnitRemain" ErrorMessage="*" ControlToValidate="txtEditUnitRemain" Enabled="false"></asp:RequiredFieldValidator>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus">
														</asp:LinkButton>
														<asp:Label id="lblViewFaktur" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="PK Header">
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton1" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
														</asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Update"
															text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" CausesValidation="True" CommandName="Cancel"
															text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID" Visible ="false">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblID" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
													<EditItemTemplate>
														<asp:Label ID="lblIDEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
								</TD>
                                <td></td>
                                <td></td>
                                <td></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px" colSpan="6">
                                    <asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"> </asp:button>
                                   <asp:button id="btnValidasi" runat="server" Width="60px" Text="Validasi" Height="22px"> </asp:button>
                                    <asp:button id="btnApprove" runat="server" Width="60px" Text="Setuju" Height="22px" Visible="False"> </asp:button>
                                   <asp:button id="btnReject" runat="server" Width="60px" Text="Tolak" CausesValidation="False" Visible="False"></asp:button>
                                    <asp:button id="btnCancelValidatsi" runat="server" Width="87px" Text="Batal Validasi" Height="22px" Visible="False" Enabled="False"> </asp:button>
                                    <asp:button id="btnCancelApprove" runat="server" Width="87px" Text="Batal Setuju" Height="22px" Visible="False"> </asp:button>
                                    <asp:button id="btnCancelReject" runat="server" Width="87px" Text="Batal Tolak" Height="22px" Visible="False"> </asp:button>
                                   <asp:button id="btnBack" runat="server" Width="60px" Text="Kembali" CausesValidation="False"></asp:button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
        </table>
        <asp:HiddenField ID="hdnValidasi" runat="server"  />
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
