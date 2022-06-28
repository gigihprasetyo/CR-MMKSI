<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAlokasiBabit.aspx.vb" Inherits="FrmAlokasiBabit" smartNavigation="False"%>
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
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%^():|\@#$;+=`~{}');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');
			}
			
        </script>
        <script language="javascript">
		
		function CalculateDanaBabit(txtPC, txtCV, txtLCV, lblDanaBabit)
		{
			var PC = document.getElementById(txtPC).value.replace(/\./g,"");
			var CV = document.getElementById(txtCV).value.replace(/\./g,"");
			var LCV = document.getElementById(txtLCV).value.replace(/\./g,"");
			var DanaBabit = document.getElementById(lblDanaBabit);
						
			var _PC = parseFloat(PC);
			if (isNaN(_PC)|| _PC=="")	
			{
				_PC=0;
			}									

			var _CV = parseFloat(CV);
			if (isNaN(_CV)|| _CV=="")	
			{
				_CV=0;
			}									

			var _LCV = parseFloat(LCV);
			if (isNaN(_LCV)|| _LCV=="")	
			{
				_LCV=0;
			}									

			var amount=parseFloat(_PC)+parseFloat(_CV)+parseFloat(_LCV);
			if (isNaN(amount))
			{
				DanaBabit.innerHTML="";
			}
			else
			{
				DanaBabit.innerHTML=amount;		
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

		function ShowPPDealerSelection()
		{
			var ddlJenisAlokasi = document.getElementById("ddlJenisAlokasi");
			if (ddlJenisAlokasi.value == 1) // value = 1 = alokasi tambahan
			{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			else
			{
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
		}
		function DealerSelection(selectedDealer)
		{
			var data = selectedDealer.split(";");
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = selectedDealer;
			/*if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtDealerCodeSelection.focus();
				txtDealerCodeSelection.blur();
			}
			else
			{
				txtDealerCodeSelection.onchange();
			}*/
		}
		
		function ShowPPAlocationSelection()
		{
			var txtDealerCode=document.getElementById("txtDealerCode");
			if (txtDealerCode.value == "") 
			{
			  alert("Silakan pilih dealer!");
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
                    <td class="titlePage" style="HEIGHT: 18px"><asp:label id="lblTitle" Runat="server" Text="BABIT - Entry Alokasi BABIT"></asp:label></td>
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
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">Jenis Alokasi</TD>
                                <TD style="HEIGHT: 3px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 3px" width="375"><asp:dropdownlist id="ddlJenisAlokasi" AutoPostBack="True" Runat="server" Width="128px"></asp:dropdownlist><asp:label id="lblJenisAlokasi" Runat="server" Width="128px"></asp:label></TD>
                                <TD class="titleField" style="HEIGHT: 3px" width="10%"></TD>
                                <TD style="HEIGHT: 3px" width="1%"></TD>
                                <TD style="HEIGHT: 3px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Dealer</TD>
                                <TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                        runat="server" Width="128px" ToolTip="Dealer Search 1"></asp:textbox><asp:label id="lblDealerCode" Runat="server"></asp:label><asp:label id="lblPopUpDealer" runat="server" width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:label></TD>
                                <TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
                                <TD style="HEIGHT: 10px" width="1%"></TD>
                                <TD style="HEIGHT: 10px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 10px" width="24%">Nomor Alokasi</TD>
                                <TD style="HEIGHT: 10px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:textbox onkeypress="TxtKeypress();" id="txtNoPerjanjian" onblur="TxtBlur('txtNoPerjanjian');"
                                        runat="server" Width="128px" MaxLength="10"></asp:textbox><asp:label id="lblNoPerjanjian" Runat="server" Width="128px"></asp:label><asp:linkbutton id="lnkbtnPopUpNoPerjanjian" runat="server" width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:linkbutton></TD>
                                <TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
                                <TD style="HEIGHT: 10px" width="1%"></TD>
                                <TD style="HEIGHT: 10px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 3px" width="24%">Periode</TD>
                                <TD style="HEIGHT: 3px" width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 3px" width="375"><asp:dropdownlist id="ddlMonthPeriodeFrom" Runat="server" Width="128px"></asp:dropdownlist>&nbsp;<asp:dropdownlist id="ddlTahun" Runat="server" Width="128px"></asp:dropdownlist><asp:label id="lblMonthPeriodeFrom" Runat="server"></asp:label>&nbsp;s/d
                                    <asp:dropdownlist id="ddlMonthPeriodeTo" Runat="server" Width="128px"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlTahunTo" Runat="server" Width="128px"></asp:dropdownlist>&nbsp;<asp:label id="lblMonthPeriodeTo" Runat="server"></asp:label></TD>
                                <TD class="titleField" style="HEIGHT: 3px" width="10%"></TD>
                                <TD style="HEIGHT: 3px" width="1%"></TD>
                                <TD style="HEIGHT: 3px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
                                <TD style="HEIGHT: 20px" width="1%"></TD>
                                <TD style="WIDTH: 375px; HEIGHT: 20px" noWrap width="375"><asp:button id="btnEntryAlokasi" Runat="server" Text="Masukan Alokasi"></asp:button></TD>
                                <TD class="titleField" style="HEIGHT: 20px" width="10%"></TD>
                                <TD style="HEIGHT: 20px" width="1%"></TD>
                                <TD style="HEIGHT: 20px" width="10%"></TD>
                            </TR>
                            <TR>
                                <TD vAlign="top" colSpan="6">
                                    <div id="div1" style="OVERFLOW: auto"><asp:datagrid id="dgAlokasiBabit" runat="server" Width="100%" AllowSorting="True" PageSize="25"
                                            AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                            CellPadding="3" DataKeyField="ID">
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
                                                        <asp:Label ID="lblNamaDealer" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") + ". " + DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") + " - " + DataBinder.Eval(Container, "DataItem.Dealer.Area1.Description") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Alokasi">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id=lblNOP runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPerjanjian") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPerjanjian") %>'>
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="PC">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPC" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.PC"),"#,##0")   %>' onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="12" >
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblPC" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.PC"),"#,##0")   %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="LCV">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtLCV" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.LCV"),"#,##0")  %>' onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="12">
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblLCV" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.LCV"),"#,##0")   %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="CV">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCV" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.CV"),"#,##0")  %>' onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="12" >
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblCV" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.CV"),"#,##0")  %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Dana Babit">
                                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDanaBabit" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.DanaBabit"),"#,##0")  %>' >
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:datagrid>
                                    </div>
                                </TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
                <TR>
                    <TD style="HEIGHT: 8px">
                        <asp:button id="btnSimpan" Runat="server" Width="80" Text="Simpan"></asp:button>
                        <asp:button id="btnRelease" Runat="server" Width="80" Text="Rilis"></asp:button>
                        <input runat="server" type="button" style="width:80px;" value="Kembali" id="btnBack" onclick="window.history.back();return false;">
                    </TD>
                </TR>
            </TABLE>
            <INPUT id="hdnValNew" type="hidden" value="-1" runat="server" NAME="hdnValNew"> <INPUT id="hdnValRelease" type="hidden" value="-1" runat="server" NAME="hdnValRelease">
        </form>
    </body>
</HTML>
