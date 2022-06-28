<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPemotonganAlokasiBabit.aspx.vb" Inherits="FrmPemotonganAlokasiBabit" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmPemotonganAlokasiBabit</title>
        <META http-equiv="Content-Type" content="text/html; charset=windows-1252">
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
        <script language="javascript">
        
            /*function DoRefresh(flag)
            {
                if (flag = "DoRefresh") then
                {
                    __doPostBack('btnSearch', '','')
                }
            }*/
            
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam;
			}
			
			function ShowPPNoPengajuan()
			{
				showPopUp('../PopUp/PopUpBabitProposal.aspx','',500,760,NoPengajuan);
			}
					
			function NoPengajuan(selectedDealer)
			{				
				var tempParam= selectedDealer.split('@')[0];
				var txtSelection = document.getElementById("txtNoPengajuan");
				txtSelection.value = tempParam;
			}
			function ShowPPNoPersetujuan()
			{
				showPopUp('../PopUp/PopUpBabitProposal.aspx','',500,760,NoPersetujuan);
			}
					
			function NoPersetujuan(selectedDealer)
			{				
				var tempParam= selectedDealer.split('@')[1];
				var txtSelection = document.getElementById("txtNoPersetujuan");
				txtSelection.value = tempParam;
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
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <TR>
                    <TD class="titlePage">BABIT&nbsp;- Persetujuan BABIT</TD>
                </TR>
                <TR>
                    <TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
                </TR>
                <TR>
                    <TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
                </TR>
                <TR>
                    <TD>
                        <TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Kode Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                                        runat="server" MaxLength="10"></asp:textbox>
                                    <asp:label id="lblKodeDealer" runat="server">
                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:label>&nbsp;
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">No Pengajuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <asp:textbox id="txtNoPengajuan" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtNoPengajuan','<>?*%$')"
                                        runat="server" MaxLength="50"></asp:textbox>
                                    <asp:label id="lblSearchNoPengajuan" runat="server">
                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:label>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" width="146">Tipe Babit</TD>
                                <TD width="2"><asp:label id="Label2" runat="server">:</asp:label></TD>
                                <TD><asp:dropdownlist id="ddlTipeBabit" Width="128px" Runat="server"></asp:dropdownlist>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlTipeBabit"></asp:RequiredFieldValidator></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px"><asp:checkbox id="chkTanggalPengajuan" runat="server" AutoPostBack="True"></asp:checkbox>Tanggal 
                                    Pengajuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <TABLE id="Table3" border="0" cellpadding="0" cellspacing="0">
                                        <TR>
                                            <TD class="titleField"><CC1:INTICALENDAR id="icDateFrom" runat="server" TextBoxWidth="70" Enabled="False"></CC1:INTICALENDAR></TD>
                                            <TD>&nbsp;s.d&nbsp;</TD>
                                            <TD class="titleField"><CC1:INTICALENDAR id="icDateUntil" runat="server" TextBoxWidth="70" Enabled="False"></CC1:INTICALENDAR></TD>
                                        </TR>
                                    </TABLE>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">No Persetujuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <asp:textbox id="txtNoPersetujuan" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtNoPersetujuan','<>?*%$')"
                                        runat="server" MaxLength="50"></asp:textbox>
                                    <asp:label id="Label1" onclick="ShowPPNoPersetujuan()" runat="server">
                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:label>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px"><asp:checkbox id="chkTanggalPersetujuan" runat="server" AutoPostBack="True"></asp:checkbox>Tanggal 
                                    Persetujuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <TABLE id="Table4" border="0" cellpadding="0" cellspacing="0">
                                        <TR>
                                            <TD class="titleField"><CC1:INTICALENDAR id="icTglPersetujuanFrom" runat="server" TextBoxWidth="70" Enabled="False"></CC1:INTICALENDAR></TD>
                                            <TD>&nbsp;s.d&nbsp;</TD>
                                            <TD class="titleField"><CC1:INTICALENDAR id="icTglPersetujuanTo" runat="server" TextBoxWidth="70" Enabled="False"></CC1:INTICALENDAR></TD>
                                        </TR>
                                    </TABLE>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Status Persetujuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <asp:DropDownList id="ddlStatusPersetujuan" runat="server"></asp:DropDownList></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px"></TD>
                                <TD style="WIDTH: 2px"></TD>
                                <TD><asp:button id="btnSearch" runat="server" Width="56px" Text="Cari"></asp:button>
                                    <asp:button id="btnUbah" runat="server" Text="Ubah" Width="56px"></asp:button></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px"></TD>
                                <TD style="WIDTH: 2px"></TD>
                                <TD class="titleField">Alokasi Babit :
                                    <asp:label id="lblAlokasiBabit" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    Terpakai :
                                    <asp:label id="lblTerpakai" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    Sisa :
                                    <asp:label id="lblSisa" runat="server"></asp:label></TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
            <div id="div1" style="OVERFLOW: auto; HEIGHT: 220px">
                <asp:datagrid id="dtgList" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="1"
                    CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px" DataKeyField="ID"
                    AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
                    <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="white"></ItemStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn ReadOnly="True" HeaderText="No">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn SortExpression="NoPengajuan" HeaderText="No Pengajuan">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton id="lnbNoPengajuan" runat="server" CommandName="Link" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                    <%# DataBinder.Eval(Container, "DataItem.NoPengajuan") %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                            <HeaderStyle Width="10%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="Dealer.DealerName">
                            <HeaderStyle Width="25%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="ActivityType" HeaderText="Jenis Kegiatan">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblActivityType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tgl Pengajuan">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblTglPengajuan" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CreatedTime"),"dd/MM/yyyy") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Jumlah Pengajuan">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblJumlahPengajuan" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tgl Terima Evidence">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <CC1:INTICALENDAR id="intiCalTglEvi" runat="server"></CC1:INTICALENDAR>
                                <asp:Label id="lblCalender" Visible="False" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Persetujuan MMKSI">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPersetujuanKTB" MaxLength="13" Runat="server" Width="80" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# Format(DataBinder.Eval(Container, "DataItem.KTBApprovalAmount"),"#,##0") %>'>
                                </asp:TextBox>
                                <asp:Label id="lblPersetujuanKTB" Visible="False" Text='<%# Format(DataBinder.Eval(Container, "DataItem.KTBApprovalAmount"),"#,##0") %>' runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="NoPersetujuan" HeaderText="No Persetujuan">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblNoPersetujuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPersetujuan") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete" Visible="False">
                                    <img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
                                <asp:LinkButton Runat="server" ID="lnkDetail" CommandName="detail"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False" SortExpression="ActivityType" HeaderText="ID">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                </asp:datagrid></div>
            <br>
            <asp:panel id="pnlButton" Runat="server" Visible="False">
                <asp:Button id="btnSave" Runat="server" Text="Simpan"></asp:Button>
                <asp:Button id="btnRilis" runat="server" Text="Disetujui"></asp:Button>
                <INPUT class="hideButtonOnPrint" id="btnPrint" onclick="window.print()" type="button" value="Cetak"
                    name="btnPrint" runat="server">
            </asp:panel>
            <input runat="server" type="button" value="Kembali" id="btnCancel" onclick="window.history.back();return false;"
                NAME="btnCancel">
        </form>
    </body>
</HTML>
