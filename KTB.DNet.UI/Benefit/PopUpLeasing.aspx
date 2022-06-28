<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpLeasing.aspx.vb" Inherits="PopUpLeasing" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Leasing</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">

		    function getParameterByName(name) {
		        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
		        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
		        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
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

		function GetSelectedValue()
		{
			var table;
			var bcheck = false;
            
			table = document.getElementById('dgCompetitorType');
			var val = '';
			var output = {};
			var name = '';
			for (i = 1; i < table.rows.length; i++)
			{
			    var CheckBox = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
			    if (CheckBox != null && CheckBox.checked)
			    {
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						//val = table.rows[i].cells[1].innerText;						
						//window.returnValue = val;
						if (val == '') {
						    val = replace(table.rows[i].cells[1].innerText, ' ', '');
						    name = replace(table.rows[i].cells[2].innerText, ' ', '');
						}
						else {
						    val = val + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
						    name = name + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
						}
						output.id = val;
						output.name = name;
						window.returnValue = output;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    //val = table.rows[i].cells[1].innerText;						
					    //window.returnValue = val;

					    if (val == '') {
					        val = replace(table.rows[i].cells[1].innerText, ' ', '');
					        name = replace(table.rows[i].cells[2].innerText, ' ', '');
					    }
					    else {
					        val = val + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
					        name = name + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
					    }
					    output.id = val;
					    output.name = name;
					    bcheck = true;
					}
					else
					{
					    if (val == '') {
					        val = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
					        name = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
					    }
					    else {
					        val = val + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
					        name = name + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
					    }
						bcheck=true;
					}
				}
			}
			
			if (bcheck)
			{
			    output.id = val;
			    output.name = name;
			    window.close();
			    if (navigator.appName != "Microsoft Internet Explorer")
			    { opener.dialogWin.returnFunc(output); }
			    else
			        window.returnValue = output;
			    //opener.dialogWin.returnFunc(val);
			  }
			else
			  {
				alert("Silahkan pilih Leasing Company");	
			  }
		}

		function check(array, value) {
		    var result = false;
		    var splits = array.split(';');
		    for (var i = 0, len = splits.length; i < len; i++) {
		       
		        if (value == splits[i]) {
		            result = true;
		            break;
		        }
		    }
		    return result;
		}
		
		function showresult() {
		    var paramSelect = getParameterByName('select')
		    if (paramSelect != undefined && paramSelect != '') {
		        //alert(getParameterByName('select'))
		        var table;
		        table = document.getElementById('dgCompetitorType');		  
		        for (i = 1; i < table.rows.length; i++) {
		            var CheckBox = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
		            var id = table.rows[i].cells[1].getElementsByTagName('span')[0];
		            // if (CheckBox != null && CheckBox.checked) {
		            if (CheckBox != null) {
		               // alert(id)
		                if (check(paramSelect, id.innerHTML) == true) {
		                  
		                    CheckBox.checked = true
		                }

		                else 
		                    CheckBox.checked = false
		                //if (navigator.appName == "Microsoft Internet Explorer") {

		                //}
		                //else {

		                //}

		            }
		        }
		    }
		}
		setTimeout(function () { showresult();  }, 1000);
		</script>
        <style>
           
            .hiddencol
              {
                display: none;
              }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						Sales - Leasing Company</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 390px">
                            <asp:datagrid id="dgCompetitorType" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" PageSize="10"
                                BackColor="#CDCDCD" CellPadding="3">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                       <HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
										<ItemTemplate>
											 <asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="ID" >
										<HeaderStyle CssClass="titleTableSales hiddencol"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" CssClass=" hiddencol" ></ItemStyle>
										<ItemTemplate >
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Name">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LeasingName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

									
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedValue()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
