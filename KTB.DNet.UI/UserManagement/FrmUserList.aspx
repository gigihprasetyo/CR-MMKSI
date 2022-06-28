<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUserList.aspx.vb" Inherits="FrmUserList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">ADMIN SISTEM&nbsp;- Daftar User</td>
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
							<TR>
								<TD class="titleField" style="HEIGHT: 12px" width="24%"><asp:label id="lblLogin" runat="server">Nama Login</asp:label></TD>
								<TD style="HEIGHT: 12px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 12px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtLoginName" onblur="omitSomeCharacter('txtLoginName','<>?*%$;')"
										runat="server" MaxLength="20" size="22"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 12px" width="20%"><asp:label id="lblCode" runat="server">Kode Organisasi</asp:label></TD>
								<TD style="HEIGHT: 12px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 12px" width="29%"><asp:label id="lblDealerCode" runat="server" Width="16px"></asp:label>&nbsp;/
									<asp:label id="lblGroupName" runat="server" Width="160px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%"><asp:label id="lblFirst" runat="server">Nama Depan</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtFirstName" onblur="omitSomeCharacter('txtFirstName','<>?*%$;')"
										runat="server" MaxLength="50" size="22"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"><asp:label id="lblName" runat="server">Nama Organisasi</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="29%"><asp:label id="lblDealerName" runat="server" Width="240px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%"><asp:label id="lblStatus" runat="server">Status User</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:dropdownlist id="ddlUserStatus" runat="server" Width="140"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><asp:label id="lblDealers" runat="server">Organisasi</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="29%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="176px"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 9px"></TD>
								<TD style="HEIGHT: 9px"></TD>
								<TD style="HEIGHT: 9px"></TD>
								<TD class="titleField" style="HEIGHT: 9px"></TD>
								<TD style="HEIGHT: 9px"></TD>
								<TD style="HEIGHT: 9px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgUserList" runat="server" Width="100%" AllowPaging="True" PageSize="25" AllowCustomPaging="True"
											AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Org">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' ID="lblDealer">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="DealerBranch.DealerCode" HeaderText="Kode Sub Org">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerCode")%>' ID="lblDealerBranch">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="UserName" SortExpression="UserName" ReadOnly="True" HeaderText="Nama Login">
													<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FirstName" SortExpression="FirstName" ReadOnly="True" HeaderText="Nama Depan">
													<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle VerticalAlign="Top"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="LastName" SortExpression="LastName" ReadOnly="True" HeaderText="Nama Belakang">
													<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Posisi" SortExpression="JobPosition.Description">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition.Description") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="UserStatDesc" SortExpression="UserStatDesc" ReadOnly="True" HeaderText="Status">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="LastLogin" SortExpression="LastLogin" ReadOnly="True" HeaderText="Login Terakhir">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Aksi">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" CausesValidation="False" Runat="server" text="Ubah" CommandName="Edit"
															ToolTip="Ubah">
															<img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnActive" CausesValidation="False" Runat="server" text="Aktif" CommandName="Active"
															ToolTip="Aktivkan">
															<img border="0" src="../images/aktif.gif" alt="Aktifkan" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnGroup" CausesValidation="False" Runat="server" text="Grup" CommandName="Group"
															ToolTip="Role">
															<img border="0" src="../images/group.gif" alt="Grup" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnInactive" CausesValidation="False" Runat="server" text="Non-aktif" CommandName="Inactive"
															ToolTip="Non Aktivkan">
															<img border="0" src="../images/in-aktif.gif" alt="Non-aktifkan" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" CausesValidation="False" Runat="server" text="Hapus" CommandName="Delete"
															ToolTip="Hapus">
															<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnRole" CausesValidation="False" Runat="server" text="Role" CommandName="Role">
															<img border="0" src="../images/seru.gif" alt="Role" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnUnRegister" CausesValidation="False" Runat="server" text="Unregister" CommandName="Unregister"
															ToolTip="Un Register">
															<img border="0" src="../images/71.gif" alt="Un-Register" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lnkResetPwd" CausesValidation="False" Runat="server" text="Reset Password" CommandName="ResetPassword"
															ToolTip="Reset Password">
															<img border="0" src="../images/reset_password.gif" alt="Reset Password" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnSetPassword" CausesValidation="False" Runat="server" text="Reset Password 'test123'" CommandName="SetPassword"
															ToolTip="Reset Password 'test123'">
															<img border="0" src="../images/set_password.gif" alt="Reset Password 'test123'" style="cursor:hand"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">

 if (document.getElementById("lblDealerCode").innerHTML == "180002")
		    {
                 
		        document.getElementById('txtKodeDealer').value = "180002";
		        document.getElementById('txtKodeDealer').readOnly = true;
		        document.getElementById('lblPopUpDealer').style.display = "none";
		        
		    }

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
