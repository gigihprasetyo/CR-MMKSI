<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmClaimReason.aspx.vb" Inherits="FrmClaimReason" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmClaimReason</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		
		function SetMandatory(objchk)
			{
				var lblMandatory = document.getElementById("lblMandatory");
				if (objchk.checked)
				{
					lblMandatory.innerText='Ya';
				}
				else
				{
					lblMandatory.innerText='Tidak';
				}
			}
			function ShowDataHistory(sUrl){
			//alert(sUrl);return false;
				showPopUp(sUrl,'',500,760);
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">CLAIM - Parameter Alasan Claim </td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						Header
					</TD>
				</TR>
				<tr>
					<td width="100%"><asp:panel id="panelHeader" runat="server">
							<TABLE id="tblHeader" cellSpacing="1" cellPadding="2" width="100%" border="0">
								<TR>
									<TD class="titleField" style="WIDTH: 156px" width="156">
										<asp:label id="Label1" runat="server">Kategori</asp:label></TD>
									<TD width="1%">:</TD>
									<TD width="79%">
										<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtCode" onblur="omitSomeCharacter('txtCode','<>?*%$;')"
											runat="server" Width="64px" MaxLength="4" CssClass="mandatory"></asp:textbox>
										<asp:RequiredFieldValidator id="rfHeadCode" runat="server" ControlToValidate="txtCode" ErrorMessage="Kategori">*</asp:RequiredFieldValidator></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 156px">
										<asp:label id="Label5" runat="server"> Deskripsi</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:textbox onkeypress="return alphaNumericExcept(event,'')" id="txtDescription" onblur="omitSomeCharacter('txtDescription','')"
											runat="server" Width="408px" MaxLength="50" CssClass="mandatory"></asp:textbox>
										<asp:RequiredFieldValidator id="rfHeadDesc" runat="server" ControlToValidate="txtDescription" ErrorMessage="Deskripsi">*</asp:RequiredFieldValidator></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 156px">
										<asp:label id="Label2" runat="server">Batas Pengajuan Klaim</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:textbox onkeypress="return NumericOnlyWith(event,'')" id="txtTimeLimit" onblur="NumOnlyBlurWithOnGridTxt(this,'')"
											runat="server" Width="80px" MaxLength="4" CssClass="mandatory"></asp:textbox>
										<asp:RequiredFieldValidator id="rfHeadLimit" runat="server" ControlToValidate="txtTimeLimit" ErrorMessage="Batas Pengajuan Klaim">*</asp:RequiredFieldValidator></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 156px">
										<asp:label id="Label3" runat="server">Persyaratan</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:textbox onkeypress="return alphaNumericExcept(event,'')" id="txtPrerequisite" onblur="omitSomeCharacter('txtPrerequisite','')"
											runat="server" Width="408px" MaxLength="80" CssClass="mandatory"></asp:textbox>
										<asp:RequiredFieldValidator id="rfHeadSyarat" runat="server" ControlToValidate="txtPrerequisite" ErrorMessage="Persyaratan">*</asp:RequiredFieldValidator></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 156px">
										<asp:Label id="Label9" runat="server" Width="152px">Penanggung jawab Analisa</asp:Label></TD>
									<TD>:</TD>
									<TD>
										<asp:TextBox id="txtIncharge" runat="server" Width="232px" MaxLength="30"></asp:TextBox>
										<asp:RequiredFieldValidator id="rfTxtIncharge" ControlToValidate="txtIncharge" ErrorMessage="Penanggung Jawab Analisa"
											Runat="server">*</asp:RequiredFieldValidator></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 156px">
										<asp:label id="Label4" runat="server">Status</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 156px">
										<asp:label id="Label7" runat="server">Lampiran/Bukti</asp:label></TD>
									<TD>:</TD>
									<TD>
										<asp:CheckBox id="chkMandatory" onclick="SetMandatory(this);" runat="server"></asp:CheckBox>
										<asp:Label id="lblMandatory" runat="server" Width="104px"></asp:Label></TD>
								</TR>
                                <tr>
                                    <td></td>
                                    <td>:</td>
                                    <td>
                                    <asp:DataGrid ID="Datagrid1" runat="server" Width="20%" AllowSorting="True" AllowPaging="True"
                                        AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3"
                                        BorderColor="#E0E0E0" ShowFooter="True">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe Bukti">
                                                <HeaderStyle Width="95%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlDocumentType" AutoPostBack="true" runat="server" Width="100%"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                             <asp:TemplateColumn>
                                                 <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                    </td>
                                </tr>
								<TR>
									<TD class="titleField" style="WIDTH: 156px">
										<asp:label id="Label6" runat="server" Visible="False">Level</asp:label></TD>
									<TD></TD>
									<TD>
										<asp:textbox onkeypress="return alphaNumericExcept(event,'')" id="txtLevel" onblur="omitSomeCharacter('txtLevel','')"
											runat="server" Width="112px" MaxLength="80" Visible="False" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 156px"></TD>
									<TD></TD>
									<TD>
										<asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>&nbsp;
										<asp:button id="btnBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
								</TR>
								<TR>
									<TD colSpan="3">
										<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 200px" DESIGNTIMEDRAGDROP="245">
											<asp:datagrid id="dtgClaimReason" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
												AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3"
												BorderColor="#E0E0E0">
												<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
												<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
												<ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
												<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
												<Columns>
													<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
														<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn HeaderText="No">
														<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
														<ItemStyle HorizontalAlign=Center></ItemStyle>
														<ItemTemplate>
															<asp:Label id="lblNo" runat="server"></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Code" HeaderText="Kategori">
														<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
														<ItemTemplate>
															<asp:Label id=lblNamaKategori runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
															</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="Reason" SortExpression="Reason" HeaderText="Deskripsi">
														<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="TimeLimit" SortExpression="TimeLimit" HeaderText="Batas Pengajuan (Hari)">
														<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
														<ItemStyle HorizontalAlign=RIGHT></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Prerequisite" SortExpression="Prerequisite" HeaderText="Persyaratan">
														<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="incharge" SortExpression="incharge" HeaderText="Penanggung Jawab Analisa">
														<HeaderStyle Width="20%" CssClass="titleTableParts" />
													</asp:BoundColumn>
													<asp:TemplateColumn HeaderText="Status" SortExpression="Status">
														<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
														<ItemStyle HorizontalAlign="CENTER" VerticalAlign="Middle"></ItemStyle>
														<ItemTemplate>
															<asp:Label id="lblStatusDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusDesc") %>'>
															</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Lampiran/Bukti" SortExpression="IsMandatoryUpload">
														<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
														<ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
														<ItemTemplate>
															<asp:Label id="lblMandatory" runat="server"></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="File Lampiran">
                                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileLampiran" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateColumn>
													<asp:TemplateColumn>
														<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
														<ItemTemplate>
															<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="View">
																<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
															<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
																<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
															<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete">
																<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
															<asp:Label id="lblHistoryStatus" runat="server">
																	<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Data"></asp:Label>
															
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
											</asp:datagrid></DIV>
									</TD>
								</TR>
							</TABLE>
						</asp:panel><asp:validationsummary id="valsumDetail" runat="server" HeaderText="Data Harus Diisi :" ShowMessageBox="True"
							ShowSummary="False"></asp:validationsummary></td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<!-- remark by Ery -->
				<asp:Panel ID="xxx" Runat="server" Visible="False">
					<TR>
						<TD class="titlePage" colSpan="3">
							<P>Detail</P>
						</TD>
					</TR>
					<TR>
						<TD width="100%" colSpan="3">
							<asp:panel id="panelDetail" runat="server">
								<TABLE id="tbelDetail" cellSpacing="1" cellPadding="2" width="100%" border="0">
									<TR>
										<TD class="titleField">
											<asp:label id="Label8" runat="server"> Deskripsi</asp:label></TD>
										<TD>:</TD>
										<TD>
											<asp:textbox onkeypress="return alphaNumericExcept(event,'')" id="txtdDescription" onblur="omitSomeCharacter('txtdDescription','')"
												runat="server" Width="408px" MaxLength="50" CssClass="mandatory"></asp:textbox>
											<asp:RequiredFieldValidator id="rfDetDesc" runat="server" ControlToValidate="txtdDescription" ErrorMessage="Deskripsi">*</asp:RequiredFieldValidator></TD>
									</TR>
									<TR>
										<TD class="titleField" style="HEIGHT: 22px">
											<asp:label id="Label12" runat="server" Visible="False">Level</asp:label></TD>
										<TD style="HEIGHT: 22px"></TD>
										<TD style="HEIGHT: 22px">
											<asp:textbox onkeypress="return alphaNumericExcept(event,'')" id="txtdLevel" onblur="omitSomeCharacter('txtdLevel','')"
												runat="server" Width="112px" MaxLength="80" Visible="False" ReadOnly="True"></asp:textbox></TD>
									<TR>
										<TD></TD>
										<TD></TD>
										<TD>
											<asp:button id="btndSave" runat="server" Text="Simpan"></asp:button>
											<asp:button id="btndBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
									</TR>
									<TR>
										<TD colSpan="3">
											<DIV id="divdetail" style="OVERFLOW: auto; HEIGHT: 200px">
												<asp:datagrid id="dtgdCR" runat="server" Width="100%" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True"
													AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0">
													<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
													<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
													<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
													<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
													<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
															<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn HeaderText="No">
															<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblNoDetail" runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="Reason" SortExpression="Reason" HeaderText="Deskripsi">
															<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn>
															<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:LinkButton id="lbtnViewd" runat="server" Text="Lihat" Width="20px" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="View">
																	<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
																<asp:LinkButton id="lbtnEditd" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
																	<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
																<asp:LinkButton id="lbtnDeleted" runat="server" Text="Hapus" Width="16px" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete">
																	<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
																
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></DIV>
										</TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
				</asp:Panel>
			</TABLE>
		</form>
		<script language="javascript">
		//	if (window.parent==window)
		//	{
		//		if (!navigator.appName=="Microsoft Internet Explorer")
		//		{
		//		  self.opener = null;
		//		  self.close();
		//		}
		//		else
		//		{
		//		   this.name = "origWin";
		//		   origWin= window.open(window.location, "origWin");
		//		   window.opener = top;
        //          window.close();
		//		}
		//	}	
			
			for(i=0;i< Page_Validators.length;i++)
			{
				ValidatorEnable(Page_Validators[i], false);
			}
			function enableHeader()
			{
				for(i=0;i< Page_Validators.length;i++)
				{
			      ValidatorEnable(Page_Validators[i], false);
				}
				ValidatorEnable(rfHeadCode, true);
				ValidatorEnable(rfHeadDesc, true);
				ValidatorEnable(rfHeadLimit, true);
				ValidatorEnable(rfHeadSyarat, true);
			}
			
			function enableDetail()
			{
				for(i=0;i< Page_Validators.length;i++)
				{
					ValidatorEnable(Page_Validators[i], false);
				}
				ValidatorEnable(rfDetDesc, true);
			}
		</script>
	</body>
</HTML>
