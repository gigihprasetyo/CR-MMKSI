<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPenggunaanDanaBabit.aspx.vb" Inherits="FrmPenggunaanDanaBabit" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>Penggunaan Dana Babit</title>
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
		
		function CalculateDanaBabit(txtPC, txtCV, txtLCV, lblDanaBabit)
		{
			var PC = document.getElementById(txtPC).value;
			var CV = document.getElementById(txtCV).value;
			var LCV = document.getElementById(txtLCV).value;
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
		
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{

			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			var lblDealerNameSelection = document.getElementById("lblDealerName");
			var hfDealerNameSelection = document.getElementById("hfDealerName");
			
				//var tempParam= selectedDealer;
				//var txtDealerSelection = document.getElementById("txtKodeDealer");
				//txtDealerSelection.value = tempParam;
			
			
			var tempParam = selectedDealer;
			txtDealerCodeSelection.value = tempParam;
			
			//hfDealerNameSelection.value = tempParam;
			//lblDealerNameSelection.innerHTML = tempParam[2];
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
		
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="HEIGHT: 17px">BABIT - Penggunaan Dana BABIT</td>
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
                            <TBODY>
                                <TR>
                                    <TD class="titleField" width="24%">Kode Dealer</TD>
                                    <TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
                                    <TD width="25%" colSpan="4"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerCode" onblur="TxtBlur('txtDealerCode');"
                                            runat="server" MaxLength="10" ToolTip="Dealer Search 1" Width="100px"></asp:textbox>&nbsp;<asp:label id="lblDealerCode" Runat="server"></asp:label>&nbsp;<asp:label id="lblPopUpDealer" runat="server" width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                        </asp:label>&nbsp;<asp:label id="lblDealerName" Runat="server"></asp:label><INPUT id="hfDealerName" type="hidden" runat="server"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" width="24%">Tipe Babit</TD>
                                    <TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
                                    <TD width="25%"><asp:dropdownlist id="ddlTipeBabit" Width="100px" Runat="server"></asp:dropdownlist>
                                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlTipeBabit"
                                            InitialValue="-1"></asp:RequiredFieldValidator></TD>
                                    <TD class="titleField" width="24%"></TD>
                                    <TD width="1%"></TD>
                                    <TD width="25%"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField">Jenis Aktivitas</TD>
                                    <TD><asp:label id="Label5" runat="server">:</asp:label></TD>
                                    <TD><asp:dropdownlist id="ddlJenisAktivitas" Width="100px" Runat="server"></asp:dropdownlist></TD>
                                    <TD></TD>
                                    <TD></TD>
                                    <TD></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField">Periode</TD>
                                    <TD><asp:label id="Label4" runat="server">:</asp:label></TD>
                                    <TD noWrap colSpan="3"><asp:dropdownlist id="ddlMonthPeriodeFrom" Width="100px" Runat="server"></asp:dropdownlist>&nbsp;
                                        <asp:dropdownlist id="ddlTahun" Width="128px" Runat="server"></asp:dropdownlist>&nbsp;S/D
                                        <asp:dropdownlist id="ddlMonthPeriodeTo" Width="100px" Runat="server"></asp:dropdownlist>&nbsp;
                                        <asp:dropdownlist id="ddlTahunTo" Width="128px" Runat="server"></asp:dropdownlist></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField"></TD>
                                    <TD></TD>
                                    <TD noWrap><asp:button id="btnCari" Width="60px" Runat="server" Text="Cari"></asp:button></TD>
                                    <TD class="titleField"></TD>
                                    <TD></TD>
                                    <TD></TD>
                                </TR>
                                <tr>
                                    <td><b>Alokasi Babit :</b>
                                        <asp:label id="lblDanaAwal" Runat="server"></asp:label></td>
                                    <td colspan="2"><b>Penggunaan BABIT : </b>
                                        <asp:label id="lblDebet" Runat="server"></asp:label></td>
                                    <td><b>Alokasi Tambahan :</b>
                                        <asp:label id="lblKredit" Runat="server"></asp:label></td>
                                    <td></td>
                                    <td><b>Sisa Dana :</b>
                                        <asp:label id="lblSisaDana" Runat="server"></asp:label></td>
                                </tr>
                                <TR>
                                    <TD vAlign="top" colSpan="6">
                                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 280px">
                                            <asp:datagrid id="dgAlokasiBabit" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
                                                CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True"
                                                PageSize="25">
                                                <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                                <ItemStyle BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="No">
                                                        <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tgl Masuk">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTglMasuk" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.Tanggal"),"dd/MM/yyyy")  %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Penggunaan BABIT">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebetDetail" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.Kredit"),"#,##0")  %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Alokasi BABIT">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKreditDetail" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.Debet"),"#,##0")  %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Referensi">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferensi" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Referensi")  %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Keterangan">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtKeterangan" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Keterangan")  %>'>
                                                            </asp:TextBox>
                                                            <asp:LinkButton ID="lnkSaveDesc" Runat="server" CommandName="SaveDesc" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.BabitProposalID")  %>'>Save</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                            </asp:datagrid></div>
                                    </TD>
                                </TR>
                            </TBODY>
                        </TABLE>
                    </TD>
                </TR>
                <TR>
                    <TD style="HEIGHT: 8px"></TD>
                </TR>
            </TABLE>
            <INPUT id="hdnValNew" type="hidden" value="-1" runat="server" NAME="hdnValNew">
        </form>
    </body>
</HTML>
