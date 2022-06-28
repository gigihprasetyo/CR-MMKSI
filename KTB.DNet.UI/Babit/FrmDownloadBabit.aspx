<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownloadBabit.aspx.vb" Inherits="FrmDownloadBabit" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmAplikasiHeader</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$')
			}
			
        </script>
        <script language="javascript">
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
		
		function ViewForm()
		{
		}

		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
		
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = selectedDealer;
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtDealerCodeSelection.focus();
				txtDealerCodeSelection.blur();
			}
			else
			{
				txtDealerCodeSelection.onchange();
			}
		}
		
		function ShowPPAlocationSelection()
		{
			var txtDealerCode=document.getElementById("txtDealerCode");
			if (txtDealerCode.value == "") 
			{
			  alert("Silakan pilih min 1 dealer!");
			  return;
			}

			showPopUp('../PopUp/PopUpBabitAlocation.aspx?DealerCode=' + txtDealerCode.value  ,'',600,600,AllocationSelection);
		}
		
		function AllocationSelection(selectedNoPerjanjian)
		{
			var txtNoPerjanjianSelection = document.getElementById("txtNoPerjanjian");
			txtNoPerjanjianSelection.value = selectedNoPerjanjian;
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtNoPerjanjianSelection.focus();
				txtNoPerjanjianSelection.blur();
			}
			else
			{
				txtNoPerjanjianSelection.onchange();
			}
		}
		
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="HEIGHT: 17px">BABIT&nbsp;- Download BABIT</td>
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
                                <TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Dealer</TD>
                                <TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
                                <TD width="70%"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerCode" onblur="TxtBlur('txtDealerCode');"
                                        runat="server" Width="300px" ToolTip="Dealer Search 1"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">No. Pembayaran</TD>
                                <TD style="HEIGHT: 3px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
                                <TD><asp:textbox onkeypress="TxtKeypress();" id="txtPaymentNo" onblur="TxtBlur('txtDealerCode');"
                                        runat="server" Width="128px" ToolTip="Dealer Search 1" MaxLength="50"></asp:textbox>&nbsp;
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 10px" width="24%">
                                    <asp:CheckBox id="chkTglPembayaran" runat="server" Text="Tgl. Pembayaran" AutoPostBack="True"></asp:CheckBox></TD>
                                <TD style="HEIGHT: 10px" width="1%">:</TD>
                                <TD align="left" width="375">
                                    <table cellSpacing="0" cellPadding="0">
                                        <tr>
                                            <td><cc1:inticalendar id="icPaymentDateFrom" runat="server" TextBoxWidth="70" Enabled="False"></cc1:inticalendar></td>
                                            <td>&nbsp;s/d&nbsp;</td>
                                            <td><cc1:inticalendar id="icPaymentDateTo" runat="server" TextBoxWidth="70" Enabled="False"></cc1:inticalendar></td>
                                        </tr>
                                    </table>
                                </TD>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">Periode</TD>
                                <TD style="HEIGHT: 3px" width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
                                <TD><asp:dropdownlist id="ddlMonthPeriodeFrom" Runat="server"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlTahun" Runat="server"></asp:dropdownlist>&nbsp;s/d
                                    <asp:dropdownlist id="ddlMonthPeriodeTo" Runat="server"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlTahunTo" Runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">Jenis Aktifitas</TD>
                                <TD style="HEIGHT: 3px" width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
                                <TD><asp:dropdownlist id="ddlActivityType" Runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
                                <TD style="HEIGHT: 20px" width="1%"></TD>
                                <TD noWrap width="375"><asp:button id="btnSearch" Width="60px" Runat="server" Text="Cari"></asp:button></TD>
                            </TR>
                            <TR>
                                <TD vAlign="top" colSpan="3">
                                    <div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dgAlokasiBabit" runat="server" Width="100%" DataKeyField="ID" CellPadding="3"
                                            BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25"
                                            AllowSorting="True" AllowPaging="True">
                                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
														document.forms[0].chkAllItems.checked)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelection" Runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" Runat="server" text= '<%# container.itemindex+1 %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKodeDealer" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNamaDealer" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="BabitProposal.ActivityType" HeaderText="Jenis Aktifitas">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityType" Runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="NomorPembayaran" HeaderText="No. Pembayaran">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.NomorPembayaran")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="BabitProposal.KTBPaymentAmount" HeaderText="Jumlah Pembayaran">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.BabitProposal.KTBApprovalAmount"), "#,##0")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Keterangan" HeaderText="Keterangan">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtKeterangan" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Keterangan")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditKeterangan" Runat="server" MaxLength="40" Text = '<%# DataBinder.Eval(Container, "DataItem.Keterangan")  %>'>
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="GLAccount.Description" HeaderText="G/L Account">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLAccount" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.GLAccount.Description")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEditGLAccount" Runat="server"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="CostCenter.ShortText" HeaderText="Cost Center">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShortText" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.CostCenter.ShortText")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEditShortText" Runat="server"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
                                                    CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
                                                    EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
                                                    <HeaderStyle Width="4%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                </asp:EditCommandColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:datagrid></div>
                                </TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
                <TR>
                    <TD style="HEIGHT: 8px"><asp:button id="btnSimpan" Width="80" Runat="server" Text="Simpan"></asp:button>&nbsp;
                        <asp:button id="btnDownload" Width="100" Runat="server" Text="Transfer ke SAP"></asp:button></TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
