<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="True" Codebehind="FrmComplainResponse.aspx.vb" Inherits="FrmComplainResponse" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Feedback</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td class="titlePage">Feedback</td>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<TD class="titleField">ID</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:Label ID="lblIDComplain" Runat="server" EnableViewState="True"></asp:Label>
								</TD>
							</tr>
							<tr>
								<TD class="titleField">Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:Label ID="lblDealer" Runat="server" EnableViewState="True"></asp:Label>
								</TD>
							</tr>
							<tr>
								<TD class="titleField">Nama Konsumen</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:Label ID="lblConsumer" Runat="server" EnableViewState="True"></asp:Label>
								</TD>
							</tr>
							<tr>
								<TD class="titleField">Tanggal Survey</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:Label ID="lblSurveyDate" Runat="server" EnableViewState="True"></asp:Label>
								</TD>
							</tr>
							<tr>
								<TD class="titleField">Keluhan</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:Literal ID="ltrComplain" Runat="server" EnableViewState="True"></asp:Literal>
								</TD>
							</tr>
							<tr>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td width="60%"><asp:Button ID="btnBack" runat="server" Text="Kembali"></asp:Button></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
							<asp:datagrid id="dgFeedback" runat="server" Width="100%" PageSize="25" CellPadding="3" BackColor="#CDCDCD"
								AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False"
								CellSpacing="1" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ID" Visible="False">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Sequence") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Note" HeaderText="Tanggapan">
										<HeaderStyle Width="80%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtFooterNotes" runat="server" Width="100%"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEditNotes" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Note") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status" Visible="True">
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:DropDownList Runat="server" ID="ddlEditStatus" Width="50px"></asp:DropDownList>
										</EditItemTemplate>
										<FooterTemplate>
											<asp:DropDownList Runat="server" ID="ddlFooterStatus" Width="50px"></asp:DropDownList>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
										CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; alt=&quot;Batal Ubah&quot;&gt;"
										EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
										<HeaderStyle Width="40px" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:EditCommandColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="40px" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="40px" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<asp:label id="lblError" runat="server" Width="100%" EnableViewState="False" ForeColor="Red"></asp:label><BR>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
