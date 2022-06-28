<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpIndentPart.aspx.vb" Inherits="PopUpIndentPart" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpIndentPart</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			//showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealerCode");
			//var txtDealerName = document.getElementById("txtDealerName");
			txtDealer.value = tempParam[0];
			//txtDealerName.value = tempParam[1];				
		}
		
		function GetSelectedPONOMany()
		{	
			var table;
			var bcheck =false;
			table = document.getElementById("dtgIndentPart");
			
			var Indent = '';
			for (var i = 0, len = sessionStorage.length; i < len; i++) {
			    var key = sessionStorage.key(i);
			    var value = sessionStorage[key];
			    var splitedKey = key.split('-');
			    if (splitedKey[0] == 'ck' && (value == "true")) {
			        sessionStorage.setItem(key, null)
			        var reqNo = splitedKey[1];
			        if (Indent == '') {
			            Indent = reqNo;
			        }
			        else {
			            Indent = Indent + ';' + reqNo;
			        }
			        bcheck = true;

			        if(navigator.appName == "Microsoft Internet Explorer")
			        {
			             window.returnValue = Indent;
                    }
			    }

			}

			if(navigator.appName != "Microsoft Internet Explorer")
			{
				opener.dialogWin.returnFunc(Indent);
			}

			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan Pilih Data terlebih dahulu");	
			  }
		}

		function windowClose() {
            //manual clear session storage for ie ****
		    for (var i = 0, len = sessionStorage.length; i < len; i++) {
		        var key = sessionStorage.key(i);
		        var value = sessionStorage[key];
		        var splitedKey = key.split('-');
		        if (splitedKey[0] == 'ck') {
		            sessionStorage.setItem(key, null)
		        }
		    }
		    window.close();
		}
		
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
				    if (re.test(elm.name)) {
				        elm.checked = checkVal
				        var par = elm.parentElement;
				        var reqNO = 'ck-' + par.getAttribute('data-requestno')
				        sessionStorage.setItem(reqNO, checkVal)
					}
				}
			}
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td class="titleField" style="HEIGHT: 26px" vAlign="top" width="15%">Kode Dealer
					</td>
					<td width="1%">:
					</td>
					<td><asp:textbox id="txtDealerCode" runat="server" Width="152px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
							onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</tr>
				<TR>
					<TD class="titleField" style="HEIGHT: 26px" vAlign="top" width="15%">Tanggal 
						Pengajuan</TD>
					<TD style="HEIGHT: 26px" vAlign="top" width="1%">:</TD>
					<td style="HEIGHT: 26px" width="50%">
						<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="icPODateFrom" runat="server"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="icPODateUntil" runat="server"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 26px" vAlign="top" width="15%">Tipe Barang</TD>
					<TD width="1%">:</TD>
					<TD width="50%"><asp:dropdownlist id="ddlMaterialType" runat="server" Width="160px"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<td width="30%"></td>
					<td width="1%"></td>
					<td><asp:button id="btnSearch" runat="server" width="60px" Text="Cari"></asp:button></td>
				</tr>
				<tr>
					<td colSpan="3"><div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgIndentPart" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
								CellPadding="3" GridLines="None" PageSize="15">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox onclick="Check(this)" id="chkItemChecked" runat="server" data-requestno='<%# DataBinder.Eval(Container, "DataItem.RequestNo")%>'></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblNo" Runat="server"></asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Pengajuan">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblNoPO runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RequestNo") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="RequestDate" SortExpression="RequestDate" HeaderText="Tanggal Pengajuan"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="45%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td colSpan="3" align="center">&nbsp;<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedPONOMany()" type="button"
							value="Pilih" name="btnChoose" runat="server"> <INPUT id="btnCancel" style="WIDTH: 60px" onclick="windowClose();" type="button" value="Tutup"
							name="btnCancel">
					</td>
				</tr>
			</table>
		</form>
        <script language="javascript" type="text/javascript" src="../WebResources/jquery-1.7.2.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
		        re = new RegExp(':' + 'chkItemChecked' + '$')
		            for (i = 0; i < document.forms[0].elements.length; i++) {
		                elm = document.forms[0].elements[i]
		                if (elm.type == 'checkbox') {
		                    var par = elm.parentElement;
		                    var reqNO = 'ck-' + par.getAttribute('data-requestno')
		                    if (re.test(elm.name)) {
		                        elm.checked = (sessionStorage.getItem(reqNO) == "true")
		                    }
		                }
		            }
            });

            function Check(el) {
                var value = document.getElementById(el.id).checked;
                var par = el.parentElement;
                var reqNO = 'ck-' + par.getAttribute('data-requestno')
                sessionStorage.setItem(reqNO, value)
            }
        </script>
	</body>
</HTML>
