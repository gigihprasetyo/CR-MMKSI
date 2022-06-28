<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPengajuanBabitKhusus.aspx.vb" Inherits="FrmListPengajuanBabitKhusus" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmPengajuanBabitKhusus</title>
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
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <TR>
                    <TD class="titlePage">BABIT&nbsp;- Daftar Pengajuan BABIT Khusus</TD>
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
                                <TD class="titleField" width="25%">Kode Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD width="75%">
                                    <table cellSpacing="0" cellPadding="0" border="0">
                                        <tr>
                                            <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                                    runat="server"></asp:textbox></td>
                                            <td><asp:panel id="pnlSearch" Width="40px" Runat="server">
                                                    <asp:label id="lblKodeDealer" runat="server">
                                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
                                                </asp:panel></td>
                                        </tr>
                                    </table>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 164px">Jenis Kegiatan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:dropdownlist id="ddlJenisKegiatan" runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 164px">No Pengajuan Babit Khusus</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoPengajuan" onblur="omitSomeCharacter('txtNoPengajuan','<>?*%$;')"
                                        runat="server"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 164px">Periode</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:dropdownlist id="ddlStart" runat="server"></asp:dropdownlist>&nbsp;
                                    <asp:dropdownlist id="ddlTahun" Runat="server" Width="128px"></asp:dropdownlist>&nbsp;s.d
                                    <asp:dropdownlist id="ddlEnd" runat="server"></asp:dropdownlist>&nbsp;
                                    <asp:dropdownlist id="ddlTahunTo" Runat="server" Width="128px"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 164px"></TD>
                                <TD style="WIDTH: 2px"></TD>
                                <TD class="titleField"><asp:button id="btnSearch" runat="server" Width="56px" Text="Cari"></asp:button></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px"></TD>
                                <TD style="WIDTH: 2px"></TD>
                                <TD class="titleField">
                                    <asp:Panel id="pnlHead" runat="server">Budget&nbsp;Babit Khusus&nbsp;: 
<asp:label id="lblAlokasiBabit" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            Terpakai : 
            <asp:label id="lblTerpakai" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            Sisa : 
          <asp:label id="lblSisa" runat="server"></asp:label></asp:Panel></TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
            <div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgList" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
                    PageSize="25" AllowCustomPaging="True" DataKeyField="ID" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                    AutoGenerateColumns="False">
                    <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                    <PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
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
                            <HeaderStyle Width="5%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                            <HeaderStyle Width="10%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblStatus" runat="server"></asp:Label>
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
                        <asp:TemplateColumn HeaderText="Jenis Kegiatan" SortExpression="ActivityType">
                            <HeaderStyle Width="15%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblActivityType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Pengajuan Dana Babit" SortExpression="BabitKhususAmount">
                            <HeaderStyle Width="10%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblPengajuanDana" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.BabitKhususAmount"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Persetujuan Dana babit">
                            <HeaderStyle Width="10%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPersetujuan" MaxLength="16" Runat="server" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# Format(DataBinder.Eval(Container, "DataItem.KTBApprovalAmount"), "#,##0") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Persetujuan">
                            <HeaderStyle Width="10%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNoPersetujuan" MaxLength="16" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPersetujuan") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Pengajuan Babit Khusus" SortExpression="NoPengajuan">
                            <HeaderStyle Width="15%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblNoPengajuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPengajuan") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="5%" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton id="lbtnDetails" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Lihat" Runat="server">
                                    <img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
                                <asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" Visible="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Ubah" Runat="server">
                                    <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
                                <asp:LinkButton id="lbtnDownload" CausesValidation="False" CommandName="download" Visible="False" text="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.FileName") %>' Runat="server">
                                    <img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>&nbsp;
                                <asp:LinkButton id="lbtnUploadReal" CausesValidation="False" CommandName="uploadReal" text="Upload Realization" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Runat="server">
                                    <img src="../images/icon_update.gif" border="0" alt="Upload Realization"></asp:LinkButton>&nbsp;
                                <asp:LinkButton id="lbtnDownloadReal" CausesValidation="False" CommandName="downloadReal" Visible="False" text="Download Realization" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.BabitRealizationFile") %>' Runat="server">
                                    <img src="../images/set.gif" border="0" alt="Download Realization"></asp:LinkButton>&nbsp;
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status" Visible="False">
                            <ItemTemplate>
                                <asp:Label id="lblStatusHid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:datagrid></div>
            <br>
            <asp:panel id="pnlChangeStatus" Runat="server" Visible="False">
                <SPAN class="titleField">Mengubah Status : </SPAN>
                <asp:DropDownList id="ddlStatus" runat="server"></asp:DropDownList>
                <asp:Button id="btnProcess" runat="server" Text="Proses"></asp:Button>
                <asp:Button id="btnSave" runat="server" Text="Simpan"></asp:Button>
                <INPUT id="hdnValNew" type="hidden" value="-1" name="hdnValNew" runat="server">
            </asp:panel>
        </form>
    </body>
</HTML>
