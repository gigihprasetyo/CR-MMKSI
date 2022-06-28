<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAlokasiBabitList.aspx.vb" Inherits="FrmAlokasiBabitList" smartNavigation="False"%>
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
			/* Deddy H	validasi value *********************************** */
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

			showPopUp('../PopUp/PopUpBabitAlocation.aspx?DealerCode=' + txtDealerCode.value  ,'',500,760,AllocationSelection);
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
                    <td class="titlePage" style="HEIGHT: 17px">BABIT - Daftar Alokasi BABIT</td>
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
                                <TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerCode" onblur="TxtBlur('txtDealerCode');"
                                        runat="server" Width="128px" ToolTip="Dealer Search 1"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:label></TD>
                                <TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
                                <TD style="HEIGHT: 10px" width="1%"></TD>
                                <TD style="HEIGHT: 10px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">Jenis Alokasi</TD>
                                <TD style="HEIGHT: 3px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 3px" width="375"><asp:dropdownlist id="ddlJenisAlokasi" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
                                <TD class="titleField" style="HEIGHT: 3px" width="10%"></TD>
                                <TD style="HEIGHT: 3px" width="1%"></TD>
                                <TD style="HEIGHT: 3px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 10px" width="24%">Nomor Alokasi BABIT</TD>
                                <TD style="HEIGHT: 10px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:textbox onkeypress="TxtKeypress();" id="txtNoPerjanjian" onblur="TxtBlur('txtNoPerjanjian');"
                                        runat="server" Width="128px" MaxLength="30"></asp:textbox><asp:linkbutton id="lnkbtnPopUpNoPerjanjian" runat="server" width="16px" Visible="False">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:linkbutton></TD>
                                <TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
                                <TD style="HEIGHT: 10px" width="1%"></TD>
                                <TD style="HEIGHT: 10px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">Periode</TD>
                                <TD style="HEIGHT: 3px" width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 3px" width="375"><asp:dropdownlist id="ddlMonthPeriodeFrom" Runat="server"></asp:dropdownlist><asp:dropdownlist id="ddlTahun" Runat="server"></asp:dropdownlist>&nbsp;S/D
                                    <asp:dropdownlist id="ddlMonthPeriodeTo" Runat="server"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlTahunTo" Runat="server"></asp:dropdownlist></TD>
                                <TD class="titleField" style="HEIGHT: 3px" width="10%"></TD>
                                <TD style="HEIGHT: 3px" width="1%"></TD>
                                <TD style="HEIGHT: 3px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">Status</TD>
                                <TD style="HEIGHT: 3px" width="1%">:</TD>
                                <TD style="WIDTH: 375px; HEIGHT: 3px" width="375">
                                    <asp:DropDownList id="ddlStatus" runat="server"></asp:DropDownList></TD>
                                <TD class="titleField" style="HEIGHT: 3px" width="10%"></TD>
                                <TD style="HEIGHT: 3px" width="1%"></TD>
                                <TD style="HEIGHT: 3px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
                                <TD style="HEIGHT: 20px" width="1%"></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 20px" noWrap width="375"><asp:button id="btnSearch" Width="60px" Runat="server" Text="Cari"></asp:button></TD>
                                <TD class="titleField" style="HEIGHT: 20px" width="10%"></TD>
                                <TD style="HEIGHT: 20px" width="1%"></TD>
                                <TD style="HEIGHT: 20px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD vAlign="top" colSpan="6">
                                    <div id="div1" style="OVERFLOW: auto; HEIGHT: 280px">
                                        <asp:datagrid id="dgAlokasiBabit" runat="server" Width="100%" DataKeyField="ID" CellPadding="3"
                                            BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False"
                                            AllowCustomPaging="True" PageSize="25" AllowSorting="True" AllowPaging="True">
                                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White"></ItemStyle>
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
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="NoPerjanjian" SortExpression="NoPerjanjian" HeaderText="Nomor Alokasi">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKodeDealer" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                                    <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNamaDealer" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Babit.AllocationType" HeaderText="Jenis Alokasi">
                                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJenisAlokasi" Runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="DanaBabit" HeaderText="Dana Babit">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDanaBabit" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.DanaBabit"),"#,##0")  %>' >
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Penggunaan BABIT" SortExpression="PenggunaanBabit">
                                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBabitProposal" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.PenggunaanBabit"),"#,##0")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SisaBabit" SortExpression="SisaBabit" HeaderText="Sisa Babit" DataFormatString="{0:#,##0}">
                                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton id=lnkbtnView runat="server" Width="20px" Text="Detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Detail" CausesValidation="False">
                                                            <img alt="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
                                                        <asp:LinkButton id=lnkbtnEdit runat="server" Width="20px" Text="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit" CausesValidation="False">
                                                            <img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                                        <asp:LinkButton id=lnkbtnDelete Width="20px" Runat="server" Text="Hapus" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>' CommandName="Delete" CausesValidation="False">
                                                            <img alt="Hapus" onclick="return confirm('Anda yakin?');" src="../images/trash.gif" border="0"></asp:LinkButton>
                                                        <asp:LinkButton id=lnkbtnPopUp runat="server" Text="Dana Babit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="PopUp" CausesValidation="False">
                                                            <img alt="Dana Babit" src="../images/popup.gif" border="0"></asp:LinkButton>
                                                        <asp:LinkButton id=lnkbtnProposal runat="server" Text="Pengajuan babit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Proposal" CausesValidation="False">
                                                            <img alt="Pengajuan Babit" src="../images/alur_flow.gif" border="0"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:datagrid></div>
                                </TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
                <TR>
                    <TD style="HEIGHT: 8px"><asp:button id="btnRelease" Width="80" Runat="server" Text="Rilis"></asp:button></TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
