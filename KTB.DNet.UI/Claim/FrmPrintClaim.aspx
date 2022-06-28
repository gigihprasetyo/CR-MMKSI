<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPrintClaim.aspx.vb" Inherits="FrmPrintClaim" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPrintClaim</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD width="50%"><asp:label id="lblNoClaim1" runat="server"></asp:label></TD>
					<TD align="right" width="50%"><asp:label id="lblDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD><br>
					</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblHeader" runat="server"></asp:label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><br>
					</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><U>Perihal : Claim Spare Parts</U></TD>
				</TR>
				<TR>
					<TD><br>
					</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="2">Sehubungan dengan Surat Claim Saudara No.
						<asp:label id="lblNoClaim2" runat="server"></asp:label>, tertanggal
						<asp:label id="lblClaimDate" runat="server">Label</asp:label>. Dengan ini kami 
						konfirmasikan jawaban atas Surat tersebut bahwa :</TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:datagrid id="dgClaimDetail" runat="server" Width="100%" CellPadding="3" BorderWidth="1px"
							AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#EFEFEF"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
							<Columns>
								<asp:BoundColumn ReadOnly="True" HeaderText="No">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Nomor Part">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Literal ID="ltrNoBarang" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartNumber") %>'>
										</asp:Literal>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Part">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Literal ID="ltrNamaBarang" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartName") %>'>
										</asp:Literal>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Quantity">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Literal ID="ltrQtyClaim" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
										</asp:Literal>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kondisi Barang">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Literal ID="ltrKeterangan" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimGoodCondition.Condition") %>'>
										</asp:Literal>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Invoice">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Literal ID="ltrInvoice" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimHeader.FakturRetur") %>'>
										</asp:Literal>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Faktur">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblTglFaktur" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jawaban">
									<HeaderStyle CssClass="titleTablePrint"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblAnswer" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><br>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2">Demikianlah jawaban kami terhadap claim saudara.
					</TD>
				</TR>
				<TR>
					<TD colSpan="2"><br>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2">Atas perhatian dan kerjasamanya kami mengucapkan terima kasih.</TD>
				</TR>
				<TR>
					<TD colSpan="2"><br>
					</TD>
				</TR>
				<TR>
					<TD></TD>
					<TD align="center">Hormat Kami,
						<br>
						PT MITSUBISHI MOTORS KRAMA YUDHA SALES INDONESIA<br>
						PARTS&nbsp;SALES DEPARTMENT
						<br>
						<br>
						<br>
						<asp:label id="lblDepHead" runat="server"></asp:label>
						<P></P>
					</TD>
				</TR>
				<TR>
					<TD>Cc:<br>
						<asp:label id="lblFooter" runat="server"></asp:label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD><br>
					</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<table>
							<tr>
								<td><asp:Panel id="pnlPrint" runat="server" Width="70px"><INPUT class="hideButtonOnPrint" id="btnPrint" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.print()"
											type="button" value="Cetak" name="btnPrint"></asp:Panel></td>
								<td vAlign="top"><INPUT class="hideButtonOnPrint" id="btnClose" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.close()"
										type="button" value="Tutup" name="btnClose"></td>
							</tr>
						</table>
					</TD>
					<TD></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
