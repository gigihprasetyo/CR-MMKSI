<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpChassisMasterMultiSelection.aspx.vb" Inherits="PopUpChassisMasterMultiSelection"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpChassisMaster</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
        <link href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
        <base target="_self">
		<script language="javascript">
            		
		    function GetSelectedChassis() {
		        var table;
		        var bcheck = false;                
		        table = document.getElementById("dtgChassisMaster");
		        var ChassisMaster = '';
		        for (i = 1; i < table.rows.length; i++) {
		            var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		            if (radioBtn != null && radioBtn.checked) {
		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    if (ChassisMaster == '') {
		                        ChassisMaster = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        ChassisMaster = ChassisMaster + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    window.returnValue = ChassisMaster;
		                    bcheck = true;		                    
		                }
		                else if (navigator.appName == "Netscape") {
		                    if (ChassisMaster == '') {
		                        ChassisMaster = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        ChassisMaster = ChassisMaster + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    bcheck = true;		                    
		                }
		                else {
		                    if (ChassisMaster == '') {
		                        ChassisMaster = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    }
		                    else {
		                        ChassisMaster = ChassisMaster + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    }
		                    bcheck = true;		                    
		                }
		            }
		        }

		        if (bcheck) {
		            window.close();
		            if (navigator.appName != "Microsoft Internet Explorer")
		            { opener.dialogWin.returnFunc(ChassisMaster); }
		        }
		        else {
		            alert("Silahkan pilih No Rangka");
		        }
		    }

		    function CheckAll(aspCheckBoxID, checkVal) {
		        re = new RegExp(':' + aspCheckBoxID + '$')
		        for (i = 0; i < document.forms[0].elements.length; i++) {
		            elm = document.forms[0].elements[i]
		            if (elm.type == 'checkbox') {
		                if (re.test(elm.name)) {
		                    elm.checked = checkVal
		                }
		            }
		        }
		    }

		    function GetSelectedChassiss(aspCheckBoxID) {		       
		        var ChassisMaster = '';
		        var bcheck = false;
		        <%--var x = document.getElementById('<%=chkItemChecked.ClientID%>').checked--%>
		        
		        var x = document.getElementById('ArrayChassis').value;
		        if (x == '') {
		            ChassisMaster = "";
		        }
		        else {
		            ChassisMaster = x
		            window.returnValue = ChassisMaster;
		            bcheck = true;
		        }


		        if (bcheck) {
		            window.close();
		            if (navigator.appName != "Microsoft Internet Explorer")
		            { opener.dialogWin.returnFunc(ChassisMaster); }
		        }
		        else {
		            alert("Silahkan pilih No Rangka");
		        }
		    }

		    function chkSelection_CheckedChanged2() {
		        var aspCheckBoxID = "chkItemChecked"
		        var x = document.getElementById(aspCheckBoxID.ClientID).checked
		        var ChassisMaster = [];
		        var vCode = document.getElementById('chkItemChecked').ClientID
                    //document.getElementById("lblNoRangka");
		        ChassisMaster = ChassisMaster.push(vCode)
		        window.returnValue = ChassisMaster;
		        bcheck = true;

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

			function ClosePopUp(){
			    window.close();
			}
			
        </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">Daftar- Kategori No Rangka</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 91px">Nomor&nbsp;Rangka</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtNoRangka" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoRangka','<>?*%$;')"
										runat="server" Width="230px"></asp:textbox><INPUT id="hdnIndent" type="hidden" runat="server"></TD>
							</TR>
							<asp:Panel Visible="False">
								<TR>
									<TD class="titleField" width="91" style="WIDTH: 91px">Nomor&nbsp;Mesin</TD>
									<TD width="1%">:</TD>
									<TD width="75%"><asp:textbox id="txtMoMesin" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtMoMesin','<>?*%$;')"
											runat="server" Width="228px"></asp:textbox></TD>
								</TR>
							</asp:Panel>
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 91px"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></td>
                                <asp:textbox id="ArrayChassis" runat="server" Width="0px" BorderColor="White" ></asp:textbox>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
                        
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">                        
							<asp:datagrid id="dtgChassisMaster" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
								BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
								AllowCustomPaging="True" AllowPaging="True" PageSize="25">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    </asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderTemplate>
                                            <%--<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked', document.forms[0].chkAllItems.checked)" />--%>  
                                            <%--<asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelection_All"></asp:CheckBox>--%>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItemChecked" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelection_CheckedChanged"></asp:CheckBox>                                            
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No" Visible="false">
                                        <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            <asp:Label ID="lblID" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisNo" HeaderText="Nomor Rangka">
										<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNo")%>'></asp:Label>
											<asp:Label id="lblTypeColor" runat="server" style="display:none"></asp:Label>
                                            <%--<asp:Label ID="lblID" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' runat="server" Visible="false"></asp:Label>--%>
										</ItemTemplate>
                                        
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EngineNumber" HeaderText="Nomor Mesin">
										<HeaderStyle Width="50%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
                                            <asp:Label runat="server" ID="lblNoMesin">
											<%--<asp:Label id="lblNoMesin" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallCategory.RecallRegNo")%>'>--%>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>									
								</Columns>
								<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>                                
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center"><br>
						<INPUT id="btnChooseIndent" style="WIDTH: 60px" onclick="GetSelectedChassisIndent()" type="button" value="Pilih" name="btnChooseIndent" runat="server"> 
                        <INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedChassiss()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;
                        <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
