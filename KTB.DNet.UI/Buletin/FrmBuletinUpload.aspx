<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBuletinUpload.aspx.vb" Inherits="FrmBuletinUpload" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmBuletinUpload</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPopUpForumMember()
		{
			showPopUp('../PopUp/PopUpForumMember.aspx','',500,760,GetIdMemberSelection);
		}
		
		function GetIdMemberSelection(selectedUserId)
		{
			var txtKodeID = document.getElementById("txtIDMember");			
			txtKodeID.value = selectedUserId			
		}

		function GetUserGroupSelection(selectedUserId)
		{
		    //var txtTemp = document.getElementById("txtTemp");
		    //var txtKeywords = document.getElementById("txtKeywords");
		    //txtKeywords.value = "OKOKOKOKOKO";
		    //txtTemp.innerText = "1";
		    //txtTemp.value = "1";
		    //txtTemp.innerHTML = "1";
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">BULETIN &amp; MANUAL - Upload Buletin</TD>
				</TR>
				<TR>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</TR>
				<TR>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Periode</TD>
								<TD width="1%">:</TD>
								<TD colSpan="3"><asp:dropdownlist id="ddlYearPeriod" runat="server"></asp:dropdownlist><asp:dropdownlist id="ddlMonthPeriod" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Kategori</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:dropdownlist id="ddlParent" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlParent">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Sub Kategori</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:dropdownlist id="ddlSubParent" runat="server"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlSubParent">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Nama</asp:label></TD>
								<TD>:</TD>
								<TD colSpan="3">
                                    <%--<asp:TextBox ID="txtTemp" runat="server" Visible="false"></asp:TextBox>--%>
                                    <asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event);" id="txtTitle" onblur="alphaNumericPlusSpaceBlur(txtTitle);"
										runat="server" size="65" MaxLength="100"></asp:textbox><asp:requiredfieldvalidator id="rfvTitle" runat="server" ErrorMessage="*" ControlToValidate="txtTitle">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event);" id="txtDescription" onblur="alphaNumericPlusSpaceBlur(txtDescription);"
										runat="server" size="65" MaxLength="255"></asp:textbox><asp:requiredfieldvalidator id="rfvDescription" runat="server" ErrorMessage="*" ControlToValidate="txtDescription">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Valid dari</TD>
								<TD>:</TD>
								<TD colSpan="3">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icValidFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icValidTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Kata Kunci</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event);" id="txtKeywords" onblur="alphaNumericPlusSpaceBlur(txtKeywords);"
										runat="server" MaxLength="100" Width="356px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Di-upload Oleh</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:label id="lblUploadedBy" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Update Terakhir</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:label id="lblLastUpdate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">File</TD>
								<TD>:</TD>
								<TD colSpan="3"><INPUT onkeypress="return false;" id="fileUpload" style="WIDTH: 216px; HEIGHT: 20px" type="file"
										size="16" runat="server">
									<asp:label id="lblIndikator" Runat="server">
										<img src="../images/green.gif" border="0" alt="File Aktif"></asp:label><asp:button id="btnRemoveFile" Runat="server" Text="Hapus Lampiran" Visible="False"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD colSpan="3">
									<asp:Label id="lblMessage" runat="server" ForeColor="Red" Visible="False">Panjang nama file tidak boleh melebihi 50 character</asp:Label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD colSpan="3"><asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>&nbsp;
                                    <asp:button id="btnBack" runat="server" Text="Kembali" CausesValidation="False"></asp:button><asp:button id="btnTambahMember" Visible="false" runat="server" Text="Tambah Member" CausesValidation="False"></asp:button><asp:button id="btnTambahGroup" runat="server" Text="Tambah Group" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 160px">
                            <%--<asp:datagrid id="dtgBuletinMember" runat="server" Width="100%" Visible="False" AllowSorting="True"
								AllowCustomPaging="True" PageSize="25" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserInfo.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Dealer.DealerCode") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserInfo.UserName" HeaderText="User ID">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUserName" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.UserName") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserInfo.JobPosition.Description" HeaderText="Posisi">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblJobPosition" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.JobPosition.Description") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>--%>

                            <%--Buletin User Group--%>
                            <asp:datagrid id="dtgBuletinGroupMember" runat="server" Width="100%" Visible="False" AllowSorting="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0"  AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<%--<asp:TemplateColumn SortExpression="UserGroupMember.UserInfo.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Dealer.DealerCode") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>--%>
									<asp:TemplateColumn SortExpression="UserGroup.Code" HeaderText="Group ID">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUserName" Text='<%# DataBinder.Eval(Container, "DataItem.UserGroup.Code") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserGroup.Description" HeaderText="Description">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDescription" Text='<%# DataBinder.Eval(Container, "DataItem.UserGroup.Description")%>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
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
