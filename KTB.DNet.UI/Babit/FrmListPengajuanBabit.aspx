<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPengajuanBabit.aspx.vb" Inherits="FrmListPengajuanBabit" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmPengajuanBabit</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
                    <TD class="titlePage">BABIT&nbsp;- Daftar Pengajuan BABIT</TD>
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
                            <TR vAlign="top">
                                <TD class="titleField">Kode Dealer</TD>
                                <TD>:</TD>
                                <TD>
                                    <table cellSpacing="0" cellPadding="0" border="0">
                                        <tr>
                                            <td><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtKodeDealer" runat="server"
                                                    Width="128px"></asp:textbox></td>
                                            <td><asp:panel id="pnlSearch" Runat="server">
                                                    <asp:label id="lblKodeDealer" runat="server">
                                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
                                                </asp:panel></td>
                                        </tr>
                                    </table>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Jenis Kegiatan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:dropdownlist id="ddlJenisKegiatan" runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">No Pengajuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoPengajuan" onblur="omitSomeCharacter('txtNoPengajuan','<>?*%$;')"
                                        runat="server"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Periode</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:dropdownlist id="ddlStart" runat="server"></asp:dropdownlist>&nbsp;
                                    <asp:dropdownlist id="ddlTahun" Width="128px" Runat="server"></asp:dropdownlist>&nbsp;s.d
                                    <asp:dropdownlist id="ddlEnd" runat="server"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlTahunTo" Width="128px" Runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR vAlign="top">
                                <TD class="titleField" style="WIDTH: 146px">Status</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:listbox id="lbStatus" runat="server" Width="82px" SelectionMode="Multiple"></asp:listbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Status Persetujuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:dropdownlist id="ddlStatusPersetujuan" runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px"></TD>
                                <TD style="WIDTH: 2px"></TD>
                                <TD><asp:button id="btnSearch" runat="server" Width="56px" Text="Cari"></asp:button></TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
            <asp:Panel ID="pnlHeadKTB" Runat="server">
                <TABLE width="99%">
                    <TR>
                        <TD style="WIDTH: 33%">
                            <asp:label id="lblTotalAlokasiBabit" Runat="server" Text="Alokasi Babit : Rp. 0" Font-Bold="True"></asp:label></TD>
                        <TD style="WIDTH: 33%">
                            <asp:label id="lblAlokasiTambahan" Runat="server" Text="Alokasi Tambahan : Rp. 0" Font-Bold="True"></asp:label></TD>
                        <TD style="WIDTH: 33%">
                            <asp:label id="lblJumlahDisetujui" Runat="server" Text="Jumlah Disetujui : Rp. 0" Font-Bold="True"></asp:label></TD>
                    </TR>
                </TABLE>
            </asp:Panel>
            <asp:datagrid id="dtgList" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="1"
                CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px" DataKeyField="ID"
                AllowCustomPaging="True" PageSize="30" AllowPaging="True" AllowSorting="True">
                <AlternatingItemStyle BackColor="White" Wrap="False"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                <PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
                <HeaderStyle ForeColor="white"></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Image ID="imgStatus" Runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
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
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Pengajuan" SortExpression="NoPengajuan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblNoPengajuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPengajuan") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Persetujuan" SortExpression="NoPersetujuan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblNoPersetujuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPersetujuan") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="Dealer.DealerName">
                        <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis Kegiatan" SortExpression="ActivityType">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblActivityType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Krm Evdc." SortExpression="TglTerimaEvidance">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblSendEvc" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Krm Inv.">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblSendInv" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Biaya Pengajuan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblCost" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Biaya Persetujuan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblBiayaPersetujuan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sisa Babit">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblDanaBabit" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.BabitAllocation.SisaBabit"),"#,##0") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton id="lbtnDetails" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Lihat" Runat="server">
                                <img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
                            <asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" Visible="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Ubah" Runat="server">
                                <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
                            <asp:LinkButton id="lbtnStatus" CausesValidation="False" CommandName="status" text="Status" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Runat="server">
                                <img src="../images/popup.gif" border="0" alt="Status"></asp:LinkButton>&nbsp;
                            <asp:LinkButton id="lbtnDownload" CausesValidation="False" CommandName="download" text="Download" Visible="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.FileName") %>' Runat="server">
                                <img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>&nbsp;
                            <asp:LinkButton id="lbtnUploadReal" CausesValidation="False" CommandName="uploadReal" text="Upload Realization" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Runat="server">
                                <img src="../images/icon_update.gif" border="0" alt="Upload Realization"></asp:LinkButton>&nbsp;
                            <asp:LinkButton id="lbtnDownloadReal" CausesValidation="False" CommandName="downloadReal" Visible="False" text="Download Realization" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.BabitRealizationFile") %>' Runat="server">
                                <img src="../images/set.gif" border="0" alt="Download Realization"></asp:LinkButton>&nbsp;
                            <asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="Del" Visible="False" text="Delete" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Runat="server">
                                <img src="../images/batal.gif" border="0" alt="Delete"></asp:LinkButton>&nbsp;
                            <asp:LinkButton id="lbtnPersetujuanBabit" runat="server" CommandName="Persetujuan" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>Persetujuan</asp:LinkButton>&nbsp;
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status" Visible="False">
                        <ItemTemplate>
                            <asp:Label id="lblStatusHid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:datagrid>
            <br>
            <table>
                <tr>
                    <td vAlign="top">
                        <asp:panel id="pnlChangeStatus" Runat="server" Visible="False" Width="246px">
                            <SPAN class="titleField">Mengubah Status : </SPAN>
                            <asp:DropDownList id="ddlStatus" runat="server"></asp:DropDownList>
                            <asp:Button id="btnProcess" runat="server" Text="Proses"></asp:Button>
                        </asp:panel></td>
                    <td vAlign="top">
                        <asp:Button id="btnDownload" runat="server" Text="Download" Enabled="False"></asp:Button></td>
                </tr>
            </table>
            <INPUT id="hdnValNew" type="hidden" value="-1" runat="server" NAME="hdnValNew"> <INPUT id="hdnValDel" type="hidden" value="-1" runat="server" NAME="hdnValDel">
        </form>
    </body>
</HTML>
