<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBuletinCategory.aspx.vb" Inherits="FrmBuletinCategory" smartNavigation="False"%>
<HTML>
	<HEAD>
		<title>FrmBuletinCategory</title>
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<script language="javascript">
			function ShowPPUserGroup()
			{
				showPopUp('../General/../PopUp/PopUpUserGroup2.aspx?x=Territory','',500,760,UserGroupSelection);
			}
			function UserGroupSelection(val)
			{				
				var txtUserGroup = document.getElementById("txtUserGroup");
				txtUserGroup.value = val;			
			}	
			function showPopUp(Url, Parameters, Height, Width, CallbackFunction)
			{
				var strFeature = 'dialogHeight:' + Height + 'px;';	
				strFeature += 'dialogWidth:' + Width + 'px';
				strFeature += 'center:yes;';	
				strFeature += 'status:no;';
				strFeature += 'help:no;';
				strFeature += 'resizable:no;';
				
				if(navigator.appName == "Microsoft Internet Explorer")
				{
					var retVal = window.showModalDialog(Url, Parameters,strFeature);					
					if (CallbackFunction != null && retVal != null) {
					CallbackFunction(retVal);
					}
				}
				else
				{
					openDGDialog(Url, Width, Height, CallbackFunction);
					return false;
				}	
			}		
			
			function openDGDialog(url, width, height, returnFunc, args) {
				if (!dialogWin.win || (dialogWin.win && dialogWin.win.closed)) {
					// Initialize properties of the modal dialog object.
					dialogWin.returnFunc = returnFunc
					dialogWin.returnedValue = ""
					dialogWin.args = args
					dialogWin.url = url
					dialogWin.width = width
					dialogWin.height = height
					// Keep name unique so Navigator doesn't overwrite an existing dialog.
					dialogWin.name = (new Date()).getSeconds().toString()
					// Assemble window attributes and try to center the dialog.
					if (Nav4) {
						// Center on the main window.
						dialogWin.left = window.screenX + 
						((window.outerWidth - dialogWin.width) / 2)
						dialogWin.top = window.screenY + 
						((window.outerHeight - dialogWin.height) / 2)
						var attr = "screenX=" + dialogWin.left + 
							",screenY=" + dialogWin.top + ",resizable=no,width=" + 
							dialogWin.width + ",height=" + dialogWin.height
					} else {
						// The best we can do is center in screen.
						dialogWin.left = (screen.width - dialogWin.width) / 2
						dialogWin.top = (screen.height - dialogWin.height) / 2
						var attr = "left=" + dialogWin.left + ",top=" + 
						dialogWin.top + ",resizable=no,width=" + dialogWin.width + 
						",height=" + dialogWin.height
					}
		
					// Generate the dialog and make sure it has focus.
					dialogWin.win=window.open(dialogWin.url, dialogWin.name, attr)
					dialogWin.win.focus()
			} else {
				dialogWin.win.focus()
			}
		}
			
			function alphaNumericExcept(event, addKey, nextControlName)
			{
				var pressedKey
				var nextControl = document.getElementById(nextControlName);
				var thisControl;
	
				if(navigator.appName == "Microsoft Internet Explorer")
					thisControl = event.srcElement;
				else
					thisControl = event.target;

				if(navigator.appName == "Microsoft Internet Explorer")	
					pressedKey = event.keyCode;
				else
				{
					pressedKey = event.charCode;
				}				
				// 03-Sep-2007	Deddy H		Penambahan code, case char ', tdk diperbolehkan, krn bs error saat button search
				if (pressedKey == 39)
				{ 
					return false;
				}
				if ( (isAccepted(pressedKey,addKey)==false) || (pressedKey == 0))
				{		
					if (nextControlName != "") 
					{
						if ( thisControl.maxLength == thisControl.value.length + 1 )
						{
							if (pressedKey>0)
							{
								thisControl.value = thisControl.value + String.fromCharCode(pressedKey);
								if (nextControl)
								{
									nextControl.focus();
								}
								return false;
							}
						}		
					return true;
					}
					else
					{
						return true;
					}
				}
				else
					return false;
			}


			function omitSomeCharacter(controlName,toBeOmittedChars_)
			{
				var key = document.getElementById(controlName).value;
				var newValue = "";
				var isOmitted = false;
				var toBeOmittedChars = toBeOmittedChars_ +"'";
				for (i=0;i<key.length;i++)	
				{
					isOmitted = false;
					for (j=0;j<toBeOmittedChars.length;j++)	
					{	
						if (key.charCodeAt(i) == toBeOmittedChars.charCodeAt(j))
						{
							isOmitted = true;
							break;
						}
					}	
					if (isOmitted == false)
						newValue = newValue + key.substring(i,i+1);
				}
				document.getElementById(controlName).value = newValue;
			}
			
			  function ValidateEmpty()
			  {
		
				if (Form1.txtName.value == "")
				{
					alert('Nama Kategori tidak boleh kosong');
					return false;
				}
				
				else if (Form1.txtDescription.value == "")
				{
					alert('Description tidak boleh kosong');
					return false;
				}
				
				else if (Form1.txtUserGroup.value == "")
				{
					alert('User Group tidak boleh kosong');
					return false;
				}
				
				
				return true;
			  }
			  
			
			</script>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">BULETIN &amp; MANUAL - Kategori Buletin</td>
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
					<TD class="titleField" width="20%"><asp:label id="Label1" runat="server">Parent</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:dropdownlist id="ddlParent" runat="server" AutoPostBack="True" Width="296px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label5" runat="server">Sub Parent</asp:label></TD>
					<td>:</td>
					<TD><asp:dropdownlist id="ddlSubParent" runat="server" Width="296px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label2" runat="server">Nama Kategori</asp:label></TD>
					<td>:</td>
					<TD><asp:textbox id="txtName" onblur="omitSomeCharacter('txtName','<>?*%$;')" runat="server" Width="408px"
							MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label3" runat="server">Deskripsi</asp:label></TD>
					<td>:</td>
					<TD><asp:textbox id="txtDescription" onblur="omitSomeCharacter('txtDescription','<>?*%$;')" runat="server"
							Width="505px" MaxLength="80"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label4" runat="server">Status</asp:label></TD>
					<td>:</td>
					<TD><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label6" runat="server">User Group</asp:label></TD>
					<TD></TD>
					<TD><asp:TextBox id="txtUserGroup" runat="server" width="152px" ></asp:TextBox>
                        <asp:label id="lblUserGroup" onclick="ShowPPUserGroup();" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
						</asp:label>&nbsp;
					</TD>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<TD><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;
						<asp:button id="btnCari" runat="server" Width="56px" Text="Cari" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgBuletinCategory" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
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
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Nama Kategori">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblNamaKategori runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="40%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
