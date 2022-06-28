<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPaymentAssignmentTypeReff.aspx.vb" Inherits="FrmPaymentAssignmentTypeReff" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPaymentAssignmentTypeReff</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		function ShowPPUserInfo()
		{
			showPopUp('../PopUp/PopUpUserInfo.aspx','',500,760,UserSelected);
		}
		function GetCurrentInputIndex()
			{
				var dtgReff = document.getElementById("dtgReff");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgReff.rows.length; index++)
				{
					inputs = dtgReff.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].type != "hidden")
								return index;
						}
					}
				}				
				return -1;
			}	
		function UserSelected(selectedCode)
		{
			var indek = GetCurrentInputIndex();
			var dtgReff = document.getElementById("dtgReff");
			var tempParam = selectedCode.split(';');
			var txtUserID = dtgReff.rows[indek].getElementsByTagName("INPUT")[0];
			var txtUserName = dtgReff.rows[indek].getElementsByTagName("SPAN")[1];
			var txtEmail = dtgReff.rows[indek].getElementsByTagName("SPAN")[2];
				
			if(navigator.appName == "Microsoft Internet Explorer")
			{
			txtUserID.innerText = tempParam[0];
			txtUserName.innerText = tempParam[2];
			txtEmail.innerHTML = tempParam[1];	
			}
			else
			{
			txtUserID.value = tempParam[0];
			txtUserName.value = tempParam[2];
			txtEmail.value = tempParam[1];
			}
		}
		
		function focusSave()
		{
			document.getElementById("btnSimpan").focus();			
		}
		function closeWindow()
		{
			window.close();
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Tipe&nbsp; Referensi Assignment</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgReff" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" GridLines="None" BorderStyle="None" BorderColor="#E0E0E0"
								AutoGenerateColumns="False" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label ID="elblNo" Runat="server"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="User ID">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUserID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.ID") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox ID="ftxtUsertID" onkeypress="return NumericOnlyWith(event,'');" Runat="server" onblur="NumOnlyBlurWithOnGridTxt(this,'');" />
											<asp:Label ID="flblUserSearch" Runat="server">
												<img src="../images/popup.gif"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="etxtUsertID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.ID") %>'/>
											<asp:Label ID="elblUserSearch" Runat="server">
												<img src="../images/popup.gif"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.UserName") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label ID="flblUserName" Runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Email">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Email") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label ID="flblEmail" Runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lbtnAdd" CommandName="add" text="Tambah" Runat="server">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lbtnSave" CommandName="save" text="Simpan" Runat="server">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lbtnCancel" CommandName="cancel" text="Batal" Runat="server">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<tr>
				</tr>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD></TD>
					<TD></TD>
					<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button><asp:button id="btnBack" runat="server" Width="60px" Text="Kembali"></asp:button></td>
				</TR>
			</TABLE>
			</TR></TABLE></form>
	</body>
</HTML>
