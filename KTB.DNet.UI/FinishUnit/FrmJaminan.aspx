<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmJaminan.aspx.vb" Inherits="FrmJaminan" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		function ValidateRadioButton(obj)
		{
			var rbtnDate = document.getElementById("rbtnDate");
			var rbtnDay = document.getElementById("rbtnDay");
			var rbtnNone = document.getElementById("rbtnNone");
			rbtnDate.checked = false;
			rbtnDay.checked = false;
			rbtnNone.checked = false;
			obj.checked = true;			
		}
		function ShowPPKodeModelSelection()
		{
			//var ddlCategory = '';				
			showPopUp('../General/FrmModelSelection.aspx?cat='+'test','',400,400,KodeTipe)
		}
	
		function GetCurrentInputIndex()
		{
			var dgSPDetail = document.getElementById("dgSPDetail");
			var currentRow;
			var index = 0;
			var inputs;
			var indexInput;
			
			for (index = 0; index < dgSPDetail.rows.length; index++)
			{
				inputs = dgSPDetail.rows[index].getElementsByTagName("INPUT");
				
				if (inputs != null && inputs.length > 0)
				{
					for (indexInput = 0; indexInput < inputs.length; indexInput++)
					{	
						if (inputs[indexInput].type != "hidden")
							return index;
					}
				}
			}				
			return -1;
		}
				
		function KodeTipe(selectedType)
		{
			var indek = GetCurrentInputIndex();
			var dgSPDetail = document.getElementById("dgSPDetail");
			var KodeTipe = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[0];
			KodeTipe.value = selectedType;
						
		}
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerName");
			if (txtDealerSelection.value == '')
			{
				txtDealerSelection.value = selectedDealer + ';';	
			}
			else
			{
				txtDealerSelection.value = txtDealerSelection.value + selectedDealer + ';';	
			}
					
		}
			
		function ShowPPFasilitasTOPSelection()
		{
			showPopUp('../FinishUnit/FrmTOPFacility.aspx?x=100c','',400,400,TOP)
		}

		function GetCurrentInputIndex()
		{
			var dgSPDetail = document.getElementById("dgSPDetail");
			var currentRow;
			var index = 0;
			var inputs;
			var indexInput;
			
			
			for (index = 0; index < dgSPDetail.rows.length; index++)
			{
				inputs = dgSPDetail.rows[index].getElementsByTagName("INPUT");
				
				if (inputs != null && inputs.length > 0)
				{
					for (indexInput = 0; indexInput < inputs.length; indexInput++)
					{	
						if (inputs[indexInput].type != "hidden")
							return index;
					}
				}
			}				
			return -1;
		}
			
		function GetCurrentSpanIndex()
		{
			var dgSPDetail = document.getElementById("dgSPDetail");
			var currentRow;
			var index = 0;
			var inputs;
			var indexInput;
			
			
			for (index = 0; index < dgSPDetail.rows.length; index++)
			{
				inputs = dgSPDetail.rows[index].getElementsByTagName("SPAN");
				
				if (inputs != null && inputs.length > 0)
				{
					for (indexInput = 0; indexInput < inputs.length; indexInput++)
					{	
						if (inputs[indexInput].type != "hidden")
							return index;
					}
				}
			}				
			return -1;
		}		
		function TOP(selectedTOP)
		{
					
			var indek = GetCurrentInputIndex();
			var dgSPDetail = document.getElementById("dgSPDetail");
			var KodeTipe = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[5];
			//var KodeTipe = dgSPDetail.rows[indek].getElementsById("txtFooterTop");
			KodeTipe.value = selectedTOP;
		}
		
		function ShowPKHeader()
		{
			var indek = GetCurrentSpanIndex();
			var _splnumber =  document.getElementById("txtSPLNumber");
			var dgSPDetail = document.getElementById("dgSPDetail"); 
			var _period = dgSPDetail.rows[indek].getElementsByTagName("SPAN")[8];
			var _kodetipe = dgSPDetail.rows[indek].getElementsByTagName("SPAN")[1];	
			showPopUp('../FinishUnit/FrmPKHeaderSPL.aspx?_splnumber='+_splnumber.value+'&_kodetipe='+_kodetipe.innerHTML+'&_period='+_period.innerHTML,'',400,500,'')
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">UMUM&nbsp;- Detail Jaminan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 23px" width="24%"><asp:label id="lblName" runat="server">Nama Dealer</asp:label></TD>
								<TD style="HEIGHT: 23px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 23px" width="25%"><asp:textbox id="txtDealerName" runat="server" Width="200px" MaxLength="5000" TextMode="MultiLine"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtDealerName" ErrorMessage="*"></asp:requiredfieldvalidator><asp:label id="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="lblDesc" runat="server">Deskripsi</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;');" id="txtDescription" onblur="omitSomeCharacter('txtSPLNumber','<>?*%$;');"
										runat="server" Width="200" MaxLength="250" TextMode="MultiLine" Height="40px"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="lblCustName" runat="server">Keterangan Jaminan</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;,/#@!~^&amp;()_-+=|\{}[]:');"
										id="txtDepositInfo" onblur="omitSomeCharacter('txtCustName','<>?*%$;');" runat="server" Width="200px" MaxLength="50"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="lblValidFrom" runat="server">Periode Tebus Dari</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtValidFrom" runat="server" Width="72px"
										MaxLength="6"></asp:textbox><asp:label id="Label2" runat="server">MMyyyy</asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtValidFrom" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="Label5" runat="server">Periode Tebus Sampai</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtValidTo" runat="server" Width="72px"
										MaxLength="6"></asp:textbox><asp:label id="Label7" runat="server">MMyyyy</asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtValidTo" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top" width="24%"><asp:label id="Label8" runat="server">Attachment</asp:label></TD>
								<TD style="HEIGHT: 20px" vAlign="top" width="1%"><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><INPUT id="inFileLocation" onkeydown="return false;" style="WIDTH: 240px" type="file" name="File1"
										runat="server">
									<asp:label id="lblAttachment" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top" width="20%" colSpan="3"><asp:label id="Label12" runat="server" Width="200px">*.DOC, *.PDF, *.XLS, *.ZIP, *.RAR</asp:label></TD>
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
								<TD class="titleField" style="HEIGHT: 11px" colSpan="6" align="center">
									<TABLE width="528" border="0">
										<TR>
											<td noWrap style="WIDTH: 221px"><b>Periode Tebus</b></td>
											<TD style="WIDTH: 188px"><asp:linkbutton id="lbtnPrevMonth" Runat="server">
													<img src="../images/page_prev.gif" alt="Previous Month" align="right" border="0">
												</asp:linkbutton></TD>
											<td align="center" width="50%"><asp:label id="lblCurrentPeriode" runat="server"></asp:label></td>
											<TD><asp:linkbutton id="lbtnNextMonth" Runat="server">
													<img src="../images/page_next.gif" alt="Next Month" align="right" border="0">
												</asp:linkbutton></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="6" align="center">
									<div id="div1" style="WIDTH: 528px; HEIGHT: 180px; OVERFLOW: auto" align="center">
										<asp:datagrid id="dgSPDetail" runat="server" Width="504px" BackColor="#E0E0E0" AutoGenerateColumns="False"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True">
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
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Width="13%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNamaType" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterKodeModel" runat="server" Width="50" MaxLength="4"></asp:TextBox>
														<asp:Label id="lblFooterKodeModel" runat="server" OnClick="ShowPPKodeModelSelection();">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditKodeModel" runat="server" Width="80" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleTypeCode" ) %>' MaxLength="4">
														</asp:TextBox>
														<asp:Label id="lblEditKodeModel" runat="server" OnClick="ShowPPKodeModelSelection();">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Amount (Rp)">
													<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right" Width="5%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblViewAmount" runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "Amount"),"#,###") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterAmount" runat="server" Width="90" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditAmount" runat="server" Width="90" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' CssClass="textRight" onkeyup="pic(this,this.value,'9999999999','N')" onkeypress="return numericOnlyUniv(event)" MaxLength="11">
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kondisi Pesanan">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblPurpose runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Purpose" )  %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:DropDownList id="cboFooterPurpose" runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList id="cboEditPurpose" runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus">
														</asp:LinkButton>
														<asp:Label id="lblViewPK" runat="server" Visible="False">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="PK Header">
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
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
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px" colSpan="6"><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBack" runat="server" Width="60px" Text="Kembali" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
