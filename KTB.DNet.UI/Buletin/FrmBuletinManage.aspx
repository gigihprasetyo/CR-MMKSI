<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBuletinManage.aspx.vb" Inherits="FrmBuletinManage" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Bulletin Management</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			
		function ValidateDelete(aspCheckBoxID) {

			re = new RegExp(':' + aspCheckBoxID + '$')  
			//generated control name starts with a colon

			for(i = 0; i < document.forms[0].elements.length; i++) {
	 
				elm = document.forms[0].elements[i]

				if (elm.type == 'checkbox') {

					if (re.test(elm.name)) {

						if (elm.checked )
						{
							return confirm('Apakah Data Akan Dihapus?');
						}
					}
				}
			}
			
			alert('Pilih dulu data yang akan dihapus !');
			return false;
		}
			
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" align="center" border="0">
				<TR>
					<TD colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">BULETIN &amp; MANUAL - Pengelolaan Buletin</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD width="24%" class="titleField" style="HEIGHT: 21px">Periode (tahun, bulan)</TD>
					<td width="1%" style="HEIGHT: 21px">:</td>
					<TD width="75%" style="HEIGHT: 21px"><asp:DropDownList id="ddlYearPeriod" runat="server"></asp:DropDownList>,
						<asp:dropdownlist id="ddlMonthPeriods" runat="server" DataTextField="Text" DataValueField="Value"></asp:dropdownlist>
						<!--<asp:textbox onkeypress="return numericOnlyUniv(event)" onblur="numericOnlyBlur(txtYearPeriods)" id="txtYearPeriods" runat="server" MaxLength="4" Columns="4"></asp:textbox> --></TD>
				</TR>
				<TR>
					<TD class="titleField">Kategori</TD>
					<td>:</td>
					<TD><asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField">Sub Kategori</TD>
					<td>:</td>
					<TD><asp:dropdownlist id="ddlSubCategory" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField">Kata Kunci</TD>
					<td>:</td>
					<TD><asp:textbox id="txtKeywords" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtKeywords','<>?*%$;')"></asp:textbox>
					</td>
						
				</TR>
				<TR>
					<TD class="titleField">Judul</TD>
					<td>:</td>
					<TD><asp:textbox id="txtJudul" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtJudul','<>?*%$;')"></asp:textbox>
					</td>
				</TR>
				<TR>
					<TD class="titleField">Deskripsi</TD>
					<td>:</td>
					<TD><asp:textbox id="txtDeskripsi" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;')"></asp:textbox>&nbsp;
						<asp:button id="btnSearch" runat="server" Text="Cari" CommandName="Search" Width="64px"></asp:button>
					</TD>
				</TR>
				<!--TR>
					<TD colSpan="3"><asp:button id="btnDelete1" runat="server" Text="Hapus" CommandName="DeleteCheck"></asp:button></TD>
				</TR-->
				<TR>
					<TD align="center" colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="49"><asp:datagrid id="dtgBuletin" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
								BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" name="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkTick', document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox runat="server" ID="chkTick"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PeriodYear" HeaderText="Periode">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# setPeriods(DataBinder.Eval(Container, "DataItem.PeriodYear"), DataBinder.Eval(Container, "DataItem.PeriodMonth")) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="BuletinCategory.ID" HeaderText="Kategori">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# setCategoryName(DataBinder.Eval(Container, "DataItem.BuletinCategory.ID")) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Title" SortExpression="Title" HeaderText="Judul">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Keterangan">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UploadBy" SortExpression="UploadBy" HeaderText="DiUpload Oleh">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<table border="0" align="center" cellpadding="1" cellspacing="1">
												<tr>
													<td align="center" valign="top">
														<asp:LinkButton id="lbtnEdit" CommandName="Edit" text="Ubah" Runat="server" CausesValidation="False">
															<img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand">
														</asp:LinkButton>
													</td>
													<td align="center" valign="top">
														<asp:LinkButton id="lbtnDelete" CommandName="Delete" text="Hapus" Runat="server" CausesValidation="False"
															Visible="False">
															<img border="0" src="../images/batal.gif" alt="Hapus" style="cursor:hand">
														</asp:LinkButton>
													</td>
												</tr>
											</table>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:button id="btnDelete2" runat="server" Text="Hapus" CommandName="DeleteCheck"></asp:button></TD>
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
