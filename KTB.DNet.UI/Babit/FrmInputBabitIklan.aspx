<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitIklan.aspx.vb" Inherits=".FrmInputBabitIklan" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmInputBabitIklan</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpTO() {
            var lblDealer = document.getElementById("lblDealerCode");
            var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
            //showPopUp('../PopUp/PopUpDealerBranchSelectionOne.aspx', '', 430, 800, TemporaryOutlet);
            showPopUp('../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 430, 800, TemporaryOutlet);
        }


        function TemporaryOutlet(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            var hdnTemporaryOutlet = document.getElementById("hdnTemporaryOutlet");
            var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            hdnTemporaryOutlet.value = selectedRefNumber;
            txtTemporaryOutlet.value = selectedRefNumber.split(";")[0];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 18px">
                    <asp:Label ID="lblTitle" runat="server" Text="BABIT - INPUT BABIT IKLAN"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr valign="top">
                <td valign="top">
                    <table>
                        <tr>
                            <td valign="top">
                                <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="width: 33%; height: 10px">Nomor Registrasi</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 3px">
                                            <asp:Label ID="lblJenisAlokasi" runat="server" Width="128px" Text="[Auto Generated]"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kode Dealer</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Temporary Outlet</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:TextBox ID="txtTemporaryOutlet" runat="server" Width="128px" Enabled="True"></asp:TextBox>
                                            <asp:HiddenField ID="hdnTemporaryOutlet" runat="server" />
                                            <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Nama Temporary Outlet</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblNamaCabang" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Area</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblArea" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="titleField">MarBox</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:DropDownList ID="ddlMarBox" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
                                            <asp:LinkButton ID="lnkReload" runat="server" Width="16px">
                                                    <img style="cursor:hand" alt="Reload MarBox" src="../images/reload.gif" border="0">
                                            </asp:LinkButton></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="titleField">Periode Marbox</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblPeriodeMarbox" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="titleField">Lokasi Marbox</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblLokasiMarbox" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table id="Table12" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="width: 33%; height: 10px">Nomor Surat</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:TextBox ID="txtNomorSurat" runat="server" Width="128px" MaxLength="30"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Bulan Periode</td>
                                        <td width="1%">:</td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" InitialValue=" "
                                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Tahun Periode</td>
                                        <td width="1%">:</td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:DropDownList ID="ddlYear" runat="server" Width="100px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" InitialValue=" "
                                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <%--<tr>
                                        <td class="titleField">Periode</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tr>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icDatePeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                    <td>&nbsp;s.d&nbsp;</td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icDatePeriodeAkhir" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td class="titleField" width="4%">Tipe Alokasi BABIT</td>
                                        <td width="4px" class="auto-style2">
                                            <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                        <td class="auto-style3">
                                            <asp:DropDownList ID="ddlTipeAlokasi" runat="server">
                                                <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Tambahan" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
                                    <tr valign="top" id="TR_CatatanMKS" runat="server">
                                        <td class="titleField">Catatan dari MMKSI</td>
                                        <td width="1%">:</td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="320px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="allocBlock" visible="false">
                <td>
                    <hr />
                    <asp:DataGrid ID="dgAlloc" runat="server" Width="60%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                        CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" Text='<%# container.itemindex+1 %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Dealer Alokasi">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" HorizontalAlign="Center" Width="250px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCodeName" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblDealerID" runat="server" Text="" style="display:none"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDealerCodeAlokasi" runat="server" Width="75px" Enabled="false"></asp:TextBox>
                                    <asp:Label ID="lblSearchDealerGrid" runat="server" style="display:none">
									        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>                                            
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Alokasi BABIT">
                                <HeaderStyle ForeColor="White" Width="17%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAllocationBabit" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlAllocationBabit" runat="server" AutoPostBack="true" 
                                            OnSelectedIndexChanged="ddlAllocationBabit_SelectedIndexChanged">
                                        <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Maksimal Subsidi">
                                <HeaderStyle ForeColor="White" Width="17%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblJmlMaxSubsidy" runat="server" Text="0"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right"/>
                                <FooterTemplate>
                                    <asp:Label ID="lblFJmlMaxSubsidy" runat="server" Text="0"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Jumlah Subsidi">
                                <HeaderStyle ForeColor="White" Width="17%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblJmlSubsidy" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right"/>
                                <FooterTemplate>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtJmlSubsidy" Style="text-align: right"
                                        runat="server" Width="90%" TabIndex="1"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemStyle Width="10%" />
                                <FooterStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="3" style="width: 24%">
                                <asp:DataGrid ID="dgIklan" runat="server" Width="90%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
                                    <FooterStyle ForeColor="#4A3C8C" BackColor="#DEDEDE"></FooterStyle>
                                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                                    <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                            <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No">
                                            <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# container.itemindex+1 %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblNoEdit" runat="server" Text='<%# container.itemindex+1 %>'>
                                                </asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Media">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMedia" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlMediaEdit" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlMediaEdit_SelectedIndexChanged">
                                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlMedia" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlMedia_SelectedIndexChanged">
                                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nama Media">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="20%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNamaMedia" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNamaMediaEdit"
                                                    runat="server" Width="90%" TabIndex="0"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNamaMedia"
                                                    runat="server" Width="90%" TabIndex="0"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipe">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="13%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipe" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTipeMediaEdit" runat="server" Width="90%">
                                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlTipeMedia" runat="server" Width="90%">
                                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Category">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="13%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCategoryEdit" runat="server" Width="90%">
                                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlCategory" runat="server" Width="90%">
                                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Ukuran">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUkuran" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtUkuranEdit"
                                                    runat="server" Width="90%" TabIndex="0"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtUkuran"
                                                    runat="server" Width="90%" TabIndex="0"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Periode Mulai">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPeriodIklanStart" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <cc1:IntiCalendar ID="icEditPeriodeIklanStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <cc1:IntiCalendar ID="icFooterPeriodeIklanStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Periode Selesai">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPeriodIklanEnd" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <cc1:IntiCalendar ID="icEditPeriodeIklanEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <cc1:IntiCalendar ID="icFooterPeriodeIklanEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jumlah Tayang">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJmlTayang" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" ID="txtJmlTayangEdit"
                                                    runat="server" Width="90%" TabIndex="0" Style="text-align: right"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" ID="txtJmlTayang"
                                                    runat="server" Width="90%" TabIndex="0" Style="text-align: right"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nilai Pengajuan">
                                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNilaiPengajuan" runat="server" Text="label"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" ID="txtNilaiPengajuanEdit"
                                                    runat="server" Width="90px" TabIndex="0" Style="text-align: right"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" Wrap="false" />
                                            <FooterTemplate>
                                                <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" ID="txtNilaiPengajuan"
                                                    runat="server" Width="90px" TabIndex="0" Style="text-align: right"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="50px" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkbtnSave" runat="server" CommandName="Save" CausesValidation="False">
												            <img src="../images/download.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancel" Text="Batal" runat="server">
                                                            <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnAdd" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="20px"></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="titlePage" style="height: 18px">
                    <asp:Label ID="Label5" runat="server" Text="Image Desain Iklan"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="width: 1%; vertical-align: top;">Upload</td>
                            <td style="width: 4px; vertical-align: top;">:</td>
                            <td style="width: 95%; vertical-align: top">
                                <asp:DataGrid ID="dgFiles" runat="server" Width="50%" CellPadding="3" CellSpacing="1"
                                    AutoGenerateColumns="False" ShowFooter="True" ShowHeader="false" GridLines="None">
                                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                    <FooterStyle ForeColor="Black" BackColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                                    <asp:Label ID="lblFileIklan" runat="server" Text="File Cache"></asp:Label>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <input id="FileUploadIklan" type="file" runat="server" tabindex="0" style="width: 100%">
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <FooterStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSimpan" Text="Simpan" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="btnKembali" Text="Kembali" runat="server" Visible="false" Style="margin-right: 10px;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
