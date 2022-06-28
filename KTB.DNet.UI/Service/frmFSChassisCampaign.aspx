<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmFSChassisCampaign.aspx.vb" Inherits="frmFSChassisCampaign" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFSChassisCampign</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		
	    <style type="text/css">
            .auto-style1 {
                width: 37%;
            }
        </style>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FREE SERVICE -&nbsp; Daftar Chassis Campaign</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="14%">Lokasi File</TD>
								<TD width="1%">:</TD>
								<TD class="auto-style1">
                                    <INPUT id="FileText" type="file" name="File1" size="34" runat="server" onkeypress="return false;">
            						<asp:button id="btnUpload" runat="server" Width="72px" Text="Upload"></asp:button>
								</TD>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../DataFile/FS/data.xlsx">Download Template (*.xls / *.xlsx)</asp:HyperLink>
                                    <br />
                                    Status Block :<br />
                                    1 = unblock<br />
                                    0 = block</td>
							</TR>
                            <TR>
								<TD class="titleField" valign="top">Jenis Free Service</TD>
								<TD width="1%" valign="top">:</TD>
								<TD width="75%" valign="top" colspan="2">
									<asp:listbox id="lboxFSType" runat="server" Width="184px" Rows="3" SelectionMode="Multiple"></asp:listbox>
									<!--<asp:dropdownlist id="ddlFSKind" runat="server" Width="208px"></asp:dropdownlist>-->
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="14%">No Rangka</TD>
								<TD width="1%">:</TD>
								<TD width="75%" colspan="2">
                                    <asp:textbox id="txtNoRangka" Width="250px" Runat="server"></asp:textbox>
                            	</TD>
							</TR>
							<TR>
								<TD class="titleField" width="14%">Keterangan</TD>
								<TD width="1%">:</TD>
								<TD width="75%" colspan="2">
                                    <asp:textbox id="txtKeterangan" Width="250px" Runat="server"></asp:textbox>
                            	</TD>
							</TR>
							<TR>
								<TD class="titleField" width="14%">Status</TD>
								<TD width="1%">:</TD>
								<TD width="75%" colspan="2">
                                    <asp:DropDownList ID="ddlIsAllow" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="-">All</asp:ListItem>
                                        <asp:ListItem Value="True">1 - Unblock</asp:ListItem>
                                        <asp:ListItem Value="False">0 - Block</asp:ListItem>
                                    </asp:DropDownList>                                    
                                </TD>
							</TR>
							<TR>
								<TD class="titleField" width="14%"></TD>
								<TD width="1%"></TD>
								<TD class="auto-style1"><asp:textbox id="txtMessage" Width="250px" Runat="server" Visible="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD colspan="2">
                                    <asp:button id="btnBatal" runat="server" Width="60px" Text="Batal"></asp:button>								
								    <asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" CausesValidation="False"></asp:button>
									<asp:button id="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False" Visible="True"></asp:button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR> 
					<TD vAlign="top" colspan="2">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px"><asp:datagrid id="dtgFSChassis" runat="server" Width="100%" BorderStyle="None" AllowPaging="True"
								PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" BackColor="#CDCDCD"
								CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FSKind.KindCode" HeaderText="Jenis Free Service">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.KindDescription")%>' id="lblFSType">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No. Rangka">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber")%>' id="lblChassisNumber">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Remarks" HeaderText="Keterangan">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks")%>' id="lblRemarks">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                     <asp:TemplateColumn SortExpression="ISAllow" HeaderText="Status Block" Visible="false">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" id="lblBlock">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="Pesan Error">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ErrorMessage")%>' id="lblErrorMessage">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Aksi">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton id="lbtnDetail" runat="server" Text="Ubah" CommandName="View" CausesValidation="False"
												ToolTip="Detail">
												<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" CausesValidation="False" Runat="server" text="Ubah" CommandName="Edit"
												ToolTip="Ubah">
												<img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand"></asp:LinkButton>
											<asp:LinkButton id="lbtnActive" CausesValidation="False" Runat="server" text="Unblock" CommandName="Active"
												ToolTip="Unblock">
												<img border="0" src="../images/aktif.gif" alt="Unblock" style="cursor:hand"></asp:LinkButton>
											<asp:LinkButton id="lbtnInactive" CausesValidation="False" Runat="server" text="Block" CommandName="Inactive"
												ToolTip="Block">
												<img border="0" src="../images/in-aktif.gif" alt="Block" style="cursor:hand"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" CausesValidation="False" Runat="server" text="Hapus" CommandName="Delete"
												ToolTip="Hapus">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
                        <asp:button id="btnDnLoad" runat="server" Width="96px" Text="Download"></asp:button>
					</TD>
				</TR>
			</TABLE>
            <asp:HiddenField ID="hdnMsg" Value="" runat="server" />
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

           <script language="javascript">
               var hdnMsg = document.getElementById("hdnMsg");
               if ( hdnMsg.value != '') {
                   var msg = hdnMsg.value;
                   hdnMsg.value = '';
                   alert(msg);
               }
                
    </script>
		
	</body>
</HTML>
