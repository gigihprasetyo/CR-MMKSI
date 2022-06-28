<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmUploadPencairanDepositATahunan.aspx.vb" Inherits="frmUploadPencairanDepositATahunan"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Sales - DepositA&nbsp;-&nbsp;Upload Pencairan Deposit A 
						Tahunan</td>
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
								<TD class="titleField" width="20%">
									<asp:label id="Label5" runat="server" Width="88px">Periode</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="80%">
									<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><cc1:inticalendar id="FromDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="ToDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;&nbsp;</TD>
											<TD><asp:Button id="btnCari" runat="server" Text="Cari" Width="64px"></asp:Button></TD>

                                            <td style="width: 90px"></td>
                                            <td class="titleField" style="width: 90px">Produk</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">

                                    <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                                </td>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblFile" runat="server">File To Upload</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="80%"><INPUT onkeypress="return false;" id="fileUpload" style="WIDTH: 400px; HEIGHT: 20px" type="file"
										size="47" name="fileUpload" runat="server">&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="80%"><EM><FONT color="#6600ff">* Tipe file yang disupport : Excel (*.xls) dengan 
											baris pertama sebagai judul kolom</FONT></EM></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="80%"><asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>&nbsp;
									<asp:button id="btnSave" tabIndex="50" runat="server" Text="Simpan" CausesValidation="False"></asp:button>&nbsp;
									<asp:button id="btnCancel" runat="server" Text="Batal" Width="64px"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="80%"><asp:label id="lblError" runat="server" Width="200px" ForeColor="Red" EnableViewState="False"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 260px" DESIGNTIMEDRAGDROP="245">
										<table width="100%">
											<tr>
												<td><asp:datagrid id="dgUpload" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True"
														BorderColor="#CDCDCD" BorderStyle="None" PageSize="1000" BorderWidth="1px" BackColor="#E0E0E0"
														CellPadding="3">
														<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
														<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
														<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
														<ItemStyle BackColor="White"></ItemStyle>
														<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="No">
																<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
																<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id=lblDealerCode Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' Runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
																<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id=lblProductCategoryCode Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>' Runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Nama Dealer">
																<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblNamaDealer" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' Runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="NettoAmount" HeaderText="Jumlah">
																<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label ID="lblJumlah" Runat="server"></asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Pesan Kesalahan">
																<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id=lblMessage runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.ErrorMessage") %>'>
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
