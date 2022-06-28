<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMaterialPromotionUpload.aspx.vb" Inherits="FrmMaterialPromotionUpload" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmMaterialPromotionUpload</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		
		function ShowPPPeriodSelection()
		{
			//alert('Showing Popup');
			showPopUp('../PopUp/PopUpMPPeriod.aspx','',500,760,PeriodSelection);
		}
		function PeriodSelection(selectedMP)
		{
			var splited = selectedMP.split(';');
			var txtMPPeriodId = document.getElementById("txtPeriodID");
			var txtMPPeriodName= document.getElementById("txtPeriodName");
			txtMPPeriodId.value = splited[0];
			txtMPPeriodName.value = splited[1];
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtMPPeriodId.focus();
				txtMPPeriodId.blur();
			}
			else
			{
				txtMPPeriodId.onchange();
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MATERIAL PROMOSI&nbsp;-&nbsp;Upload Alokasi Material Promosi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="20%">Periode</TD>
								<TD style="HEIGHT: 26px" width="1%">:</TD>
								<TD style="HEIGHT: 26px" width="80%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPeriodName" onblur="omitSomeCharacter('txtPeriodName','<>?*%$;')"
										runat="server"   Width="192px"></asp:textbox><asp:label id="lblSearchPeriod" onclick="ShowPPPeriodSelection();" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPeriodID" onblur="omitSomeCharacter('txtPeriodName','<>?*%$;')"
										style="VISIBILITY: hidden" runat="server" Width="72px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblFile" runat="server">File To Upload</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="80%"><INPUT onkeypress="return false;" id="fileUpload" type="file" size="59" name="fileUpload"
										runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="80%"><asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>&nbsp;
									<asp:button id="btnSave" tabIndex="50" runat="server" Text="Simpan" CausesValidation="False"></asp:button>&nbsp;
									<asp:button id="btnCancel" runat="server" Width="64px" Text="Batal"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="80%">
									<P><asp:label id="lblError" runat="server" Width="472px" EnableViewState="False" ForeColor="Red"></asp:label></P>
									<P><asp:label id="lblError2" runat="server" Width="472px" EnableViewState="False" ForeColor="Red"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblPeriode" runat="server" Visible="False">Periode</asp:label></TD>
								<TD width="1%"><asp:label id="Label3" runat="server" Visible="False">:</asp:label></TD>
								<TD width="80%"><asp:label id="lblPeriodeVal" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblYearPeriod" runat="server" Visible="False">Tahun Periode</asp:label></TD>
								<TD width="1%"><asp:label id="Label4" runat="server" Visible="False">:</asp:label></TD>
								<TD width="80%"><asp:label id="lblYearPeriodVal" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblStartMonthPeriod" runat="server" Visible="False">Mulai Periode - Bulan ke</asp:label></TD>
								<TD width="1%"><asp:label id="Label5" runat="server" Visible="False">:</asp:label></TD>
								<TD width="80%"><asp:label id="lblStartMonthPeriodVal" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblEndMonthPeriod" runat="server" Visible="False">Akhir Periode - Bulan ke</asp:label></TD>
								<TD width="1%"><asp:label id="Label7" runat="server" Visible="False">:</asp:label></TD>
								<TD width="80%"><asp:label id="lblEndMonthPeriodVal" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 260px">
										<table width="100%">
											<tr>
												<td><asp:datagrid id="dgAlokasi" runat="server" Width="100%" CellPadding="3" BackColor="#E0E0E0" BorderWidth="1px"
														BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
														<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
														<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
														<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
														<ItemStyle BackColor="White"></ItemStyle>
														<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="No">
																<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="Label1" runat="server" text= '<%# container.itemindex+1 %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Kode Dealer">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblKodeDealer" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.KodeDealer") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-1">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo1" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo1") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-2">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo2" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo2") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-3">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo3" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo3") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-4">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo4" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo4") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-5">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo5" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo5") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-6">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo6" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo6") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-7">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo7" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo7") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-8">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo8" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo8") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-9">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo9" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo9") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-10">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo10" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo10") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-11">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo11" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo11") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-12">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo12" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo12") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-13">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo13" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo13") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-14">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo14" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo14") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-15">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo15" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo15") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-16">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo16" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo16") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-17">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo17" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo17") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-18">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo18" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo18") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-19">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo19" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo19") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Barang-20">
																<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblGoodNo20" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.GoodNo20") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
													</asp:datagrid></td>
											</tr>
										</table>
									</div>
									<br>
								</TD>
							</TR>
							<TR>
								<TD colSpan="6">&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 151px" colSpan="2"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
