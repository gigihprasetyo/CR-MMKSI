<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarStatusSPAF.aspx.vb" Inherits="FrmDaftarStatusSPAF" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ConfirmDailyPO</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		
		function ConfirmDialog()
		{
			var ddl = document.getElementById("ddlAction");
			return confirm("Yakin mau melakukan proses" + ddl.outerText);
		}
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		function ShowPopUpHistorySPAF(id)
		{
			showPopUp('../PopUp/PopUpHistorySPAFStatus.aspx?ID=' + id, '', 340, 560);
		}
		function ShowPopUpUploadDocument(id)
		{
			showPopUp('../PopUp/PopUpSPAFDokumenUpload.aspx?ID=' + id,'',60,360,'');
		}
		
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">Daftar Dokumen</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" width="22%"></TD>
								<TD width="1%"></TD>
								<TD width="35%"></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="20%"></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="22%"><asp:label id="Label1" runat="server"> Leasing</asp:label></TD>
								<TD width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD width="35%"><asp:dropdownlist id="ddlDealer" runat="server">
										<asp:ListItem Value="-1">Kode Leasing</asp:ListItem>
										<asp:ListItem Value="TAF">TAF</asp:ListItem>
										<asp:ListItem Value="DSF">DSF</asp:ListItem>
									</asp:dropdownlist>
									<asp:Label ID="lblSearchDealer" Runat="server"></asp:Label>
								</TD>
								<TD class="titleField" width="20%"><asp:label id="lblNoRangka" runat="server"> Nomor Rangka</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="20%"><asp:textbox onblur="alphaNumericPlusBlur(txtNoRangka)" id="txtNoRangka" onkeypress="return alphaNumericPlusUniv(event)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="22%"><asp:label id="Label3" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="35%"><asp:listbox id="lboxStatus" runat="server" Width="136px" SelectionMode="Multiple" Rows="3"></asp:listbox></TD>
								<TD class="titleField" width="20%"><asp:label id="Label8" runat="server">Tipe</asp:label></TD>
								<TD width="1%"><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD width="20%"><asp:listbox id="lboxTipe" runat="server" Width="136px" SelectionMode="Multiple" Rows="3"></asp:listbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" rowSpan="2">Tipe Dokumen</TD>
								<TD rowSpan="2"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD rowSpan="2"><asp:dropdownlist id="ddlDocType" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField"><STRONG>Total Quantity</STRONG></TD>
								<TD>:</TD>
								<TD><asp:label id="lblQuantity" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTotalHarga" runat="server" Font-Bold="True">Total Harga </asp:label></TD>
								<TD>:</TD>
								<TD><STRONG><asp:label id="Label12" runat="server" Font-Bold="True">Rp.</asp:label>&nbsp;&nbsp;<asp:label id="lblTotalHargaValue" runat="server" Font-Bold="True"></asp:label></STRONG></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Periode Kirim</asp:label></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD colSpan="3">
									<TABLE border="0" cellPadding="0" width="50%">
										<TR>
											<td><asp:checkbox id="cbPeriodeKirim" runat="server"></asp:checkbox></td>
											<TD><cc1:inticalendar id="calDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="calSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label7" runat="server">Periode Persetujuan</asp:label></TD>
								<TD>:</TD>
								<TD colSpan="3">
									<TABLE border="0" cellPadding="0" width="50%">
										<TR>
											<td><asp:checkbox id="cbPeriodePersetujuan" runat="server"></asp:checkbox></td>
											<TD><cc1:inticalendar id="calDariSetuju" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="calSampaiSetuju" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
								<td><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button></td>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<DIV style="HEIGHT: 240px; OVERFLOW: auto" id="div1" DESIGNTIMEDRAGDROP="1988"><asp:datagrid id="dtgSPAF" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#CDCDCD"
											BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
													<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn Visible="true">
													<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect',document.all.chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblStatus" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Leasing">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblLeasing" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox4" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ReffLetter" SortExpression="ReffLetter" HeaderText="No Kontrak">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DateLetter" SortExpression="DateLetter" HeaderText="Tgl Kontrak" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PostingDate" SortExpression="PostingDate" HeaderText="Tgl Kirim" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CustomerName" SortExpression="CustomerName" HeaderText="Nama Pelanggan">
													<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DealerLeasing" SortExpression="DealerLeasing" HeaderText="Dealer Leasing">
													<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="NoRangka">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblCM" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Dealer">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKodeDealer" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Subsidi" HeaderText="SPAF per Unit (Rp)">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblSubsidi" runat="server" Text='<%# Iif(DataBinder.Eval(Container, "DataItem.DocType") = 0, DataBinder.Eval(Container, "DataItem.SPAF", "{0:#,##0}"), DataBinder.Eval(Container, "DataItem.Subsidi", "{0:#,##0}")) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PPh" HeaderText="PPh">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPh", "{0:#,##0}") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AfterPPh" HeaderText="SPAF setelah PPh (Rp)">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AfterPPh", "{0:#,##0}") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PPn" HeaderText="PPN">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPn", "{0:#,##0}") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="True" DataField="AlasanPenolakan" HeaderText="Catatan">
													<HeaderStyle ForeColor="White" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle ForeColor="White" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<TABLE>
															<TR>
																<TD>
																	<asp:LinkButton id="lbtnUpload1" runat="server" CommandName="Upload">
																		<img src="../images/icon_evid.gif" alt="Upload" border="0">
																	</asp:LinkButton>
																	<asp:LinkButton id="lbtnDownload1" runat="server" CommandName="Download">
																		<img src="../images/download.gif" alt="Simpan" border="0">
																	</asp:LinkButton></TD>
																<TD>
																	<asp:LinkButton id="lbtnDeleteFile1" runat="server" CommandName="DeleteFile">
																		<img src="../images/in-aktif.gif" alt="Hapus File" border="0">
																	</asp:LinkButton></TD>
															</TR>
															<TR>
																<TD>
																	<asp:LinkButton id="lbtnUpload2" runat="server" CommandName="Upload">
																		<img src="../images/icon_evid.gif" alt="Upload" border="0">
																	</asp:LinkButton>
																	<asp:LinkButton id="lbtnDownload2" runat="server" CommandName="Download">
																		<img src="../images/download.gif" alt="Simpan" border="0">
																	</asp:LinkButton></TD>
																<TD>
																	<asp:LinkButton id="lbtnDeleteFile2" runat="server" CommandName="DeleteFile">
																		<img src="../images/in-aktif.gif" alt="Hapus File" border="0">
																	</asp:LinkButton></TD>
															</TR>
														</TABLE>
														<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
															<img src="../images/trash.gif" alt="Hapus" border="0">
														</asp:LinkButton>
														<asp:LinkButton id="lbtnHistory" runat="server" CommandName="History">
															<img src="../images/alur_flow.gif" alt="Status Histori" border="0">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="1" width="100%">
										<TR>
											<asp:panel id="pnlUtama" Runat="server">
												<TD>
													<asp:label id="Label9" runat="server" Font-Bold="True" Font-Italic="True">Mengubah Status :</asp:label>&nbsp;&nbsp;
													<asp:dropdownlist id="ddlAction" runat="server" AutoPostBack="True"></asp:dropdownlist>
													<asp:button id="btnProses" runat="server" Text="Proses" Visible ="True"></asp:button>
													<asp:button id="btnProsesAll" runat="server" Text="Proses All"></asp:button>
													<asp:button id="btnDownloadAll" runat="server" Text="Download All"></asp:button>
													<asp:button id="btnDownload" runat="server" Text="Download Detail"></asp:button>
													<asp:button id="btnGeneralDownload" runat="server" Text="Download Umum"></asp:button>
													<asp:button id="btnDownloadGeneralAll" runat="server" Text="Download All General"></asp:button>
													<asp:button id="btnDownloadDoc" runat="server" Text="Download Doc"></asp:button>
													<asp:button id="btnDownloadDocAll" runat="server" Text="Download Doc All"></asp:button></TD>
												<TD></TD>
											</asp:panel><asp:panel id="pnlStatus" Runat="server" Visible="False">
												<TD class="titleField">Deskripsi:
													<asp:TextBox id="txtStatusTolak" Runat="server" MaxLength="200"></asp:TextBox>
													<asp:Button id="btnSave" runat="server" Text="Simpan"></asp:Button>
													<asp:Button id="btnCancel" runat="server" Text="Batal"></asp:Button></TD>
											</asp:panel><asp:panel id="pnlUpload" Runat="server" Visible="False">
												<TD class="titleField">Upload File: <INPUT style="WIDTH: 264px; HEIGHT: 20px" id="fileUploadSPAFDoc" size="24" type="file"
														name="fileUpload" runat="server">
													<asp:Button id="btnUpload" runat="server" Text="Upload"></asp:Button></TD>
											</asp:panel></TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<SCRIPT language="javascript">
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
		</SCRIPT>
	</body>
</HTML>
