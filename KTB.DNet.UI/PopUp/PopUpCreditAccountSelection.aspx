<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpCreditAccountSelection.aspx.vb" Inherits="PopUpCreditAccountSelection" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpCreditAccountSelection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		
		function getQueryVariable(variable)
		{
			var query = window.location.search.substring(1);
			var vars = query.split("&");
			for (var i=0;i<vars.length;i++)
			{	
				var pair = vars[i].split("=");
				if (pair[0] == variable)
				{
					return pair[1];
				}
			}
			return "nothing";
		}
		
		function GetSelectedDealer()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgDealerSelection");
			var Dealer ='';
			for (i = 1; i < table.rows.length; i++)
			{
						
			var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						if(getQueryVariable("x") == "Territory")
						{	Dealer = replace(table.rows[i].cells[2].innerText,' ','') + ';' + table.rows[i].cells[3].innerText;// + ';' + replace(table.rows[i].cells[5].innerText,' ','')+ ';' + replace(table.rows[i].cells[6].innerText,' ','');
						}
						else
						{	Dealer = replace(table.rows[i].cells[2].innerText,' ','') + ';' + table.rows[i].cells[3].innerText;
						}
						window.returnValue = Dealer;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (getQueryVariable("x") == "Territory") {
					        Dealer = replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + table.rows[i].cells[3].innerText;// + ';' + replace(table.rows[i].cells[5].innerText,' ','')+ ';' + replace(table.rows[i].cells[6].innerText,' ','');
					    }
					    else {
					        Dealer = replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + table.rows[i].cells[3].innerText;
					    }
					    bcheck = true;
					    try {
					        window.opener.dialogWin.returnFunc(Dealer);
					    } catch (e) {
					        try {
					            window.returnValue = Dealer;
					        } catch (e2) {

					        }
					    }
					    
					   
					}

					else
					{
						if(getQueryVariable("x") == "Territory")
						{	
							Dealer = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + table.rows[i].cells[3].innerHTML + ';' + table.rows[i].cells[5].innerHTML + ';' +  table.rows[i].cells[6].innerHTML; 
						}
						else
						{	
							Dealer = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[3].innerHTML,' ','') ;
						}
						window.opener.dialogWin.returnFunc(Dealer);
						bcheck=true;
					}
					break;
				}
			}
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih dealer");	
			  }
		}

		function replace(string,text,by) 
			{
				var strLength = string.length, txtLength = text.length;
				if ((strLength == 0) || (txtLength == 0)) return string;

				var i = string.indexOf(text);
				if ((!i) && (text != string.substring(0,txtLength))) return string;
				if (i == -1) return string;

				var newstr = string.substring(0,i) + by;

				if (i+txtLength < strLength)
				newstr += replace(string.substring(i+txtLength,strLength),text,by);

				return newstr;
			}	
		
		/*
		function GetSelectedAccount()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgDealerSelection");
			var Dealer ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Dealer == '')
						{
							Dealer = replace(table.rows[i].cells[3].innerText,' ','');
						}
						else
						{
							Dealer = Dealer + ';' + replace(table.rows[i].cells[3].innerText,' ','');
						}
					window.returnValue = Dealer;
					bcheck=true;
					}
					else
					{
						if (Dealer == '')
						{
							Dealer = replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Dealer = Dealer + ';' + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						bcheck=true;
					}
				}
			}
			if (bcheck)
			  {
					window.close();
					if(navigator.appName != "Microsoft Internet Explorer")
					{	opener.dialogWin.returnFunc(Dealer);	}
			  }
			else
			  {
				alert("Silahkan Pilih Dealer terlebih dahulu");	
			  }
		}

		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
		}
		
		function replace(string,text,by) 
			{
				var strLength = string.length, txtLength = text.length;
				if ((strLength == 0) || (txtLength == 0)) return string;

				var i = string.indexOf(text);
				if ((!i) && (text != string.substring(0,txtLength))) return string;
				if (i == -1) return string;

				var newstr = string.substring(0,i) + by;

				if (i+txtLength < strLength)
				newstr += replace(string.substring(i+txtLength,strLength),text,by);

				return newstr;
			}	
			*/	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">CREDIT ACCOUNT&nbsp;-&nbsp;Pencarian Credit 
									Account</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 20px" width="20%">Nama Dealer</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox id="txtDealerName" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%">Credit Account</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="33%"><asp:textbox id="txtCreditAccount" runat="server"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 13px">Grup</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:listbox id="lboxGroup" runat="server" Width="152px" Rows="3" SelectionMode="Multiple"></asp:listbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"><asp:button id="btnSearch" runat="server" Width="80px" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgDealerSelection" runat="server" Width="100%" AllowSorting="True" CellPadding="2"
											AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<HeaderStyle ForeColor="white" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CreditAccount" HeaderText="Kode Dealer">
													<HeaderStyle Width="10%"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CreditAccount" HeaderText="Credit Account">
													<HeaderStyle Width="10%"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCreditAccount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="21%"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="GroupName" HeaderText="Grup Dealer">
													<HeaderStyle Width="7%"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroupName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GroupName") %>' >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealer()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
