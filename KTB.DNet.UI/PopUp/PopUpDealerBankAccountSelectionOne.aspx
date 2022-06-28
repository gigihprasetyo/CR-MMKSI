<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDealerBankAccountSelectionOne.aspx.vb" Inherits="PopUpDealerBankAccountSelectionOne"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpDealerBankAccountSelectionOne</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedDealerAccount()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dgDealerBankAccount");
			var Dealer ='';
			for (i = 1; i < table.rows.length; i++)
			{
					
			var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						//Dealer = replace(table.rows[i].cells[4].innerText,' ','') + ';' + table.rows[i].cells[5].innerText;
						Dealer = replace(table.rows[i].cells[4].innerText,' ','');
					
						window.returnValue = Dealer;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    //Dealer = replace(table.rows[i].cells[4].innerText,' ','') + ';' + table.rows[i].cells[5].innerText;
					    Dealer = replace(table.rows[i].cells[4].innerText, ' ', '');

					    window.opener.dialogWin.returnFunc(Dealer);
					    bcheck = true;
					}
					else
					{
						//Dealer = replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[5].innerHTML,' ','') ;
						Dealer = replace(table.rows[i].cells[4].innerHTML,' ','');
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
				alert("Silahkan pilih Account");	
			  }
		}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="7">Dealer Bank Account</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD class="titleField" colSpan="7"></TD>
				</TR>
				<TR>
					<TD colSpan="7">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 200px">
							<asp:datagrid id="dgDealerBankAccount" runat="server" Width="100%" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											&nbsp;
										</HeaderTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="BankKey" HeaderText="Kode Bank" SortExpression="BankKey">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BankAccount" HeaderText="Nomor Rekening" SortExpression="BankAccount">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BankName" HeaderText="Nama Bank" SortExpression="BankName">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</DIV>
					</TD>
				</TR>
				<TR>
					<TD colspan="7" align="center">
						<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealerAccount()"
							type="button" value="Pilih" name="btnChoose" runat="server"> <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
