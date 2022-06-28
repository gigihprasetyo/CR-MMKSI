<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitReportEvent.aspx.vb" Inherits="FrmInputBabitReportEvent" SmartNavigation="False"  MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmPengajuanBabit</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script language="javascript">
        function btnGetInfoBabitHeaderClick() {
            var btnGetInfoBabitHeader = document.getElementById("btnGetInfoBabitHeader");
            btnGetInfoBabitHeader.click();
        }

        function ShowPPBabitHeaderSelection() {
            showPopUp('../PopUp/PopUpBabitHeaderSelectionOne.aspx', '', 500, 760, BabitHeaderSelection);
        }

        function BabitHeaderSelection(selectedEvent) {
            var data = selectedEvent.split(";");
            var txtBabitRegNumber = document.getElementById("txtBabitRegNumber");
            var txtNoSurat = document.getElementById("txtNoSurat");
            var hdnBabitHeaderID = document.getElementById("hdnBabitHeaderID");
            hdnBabitHeaderID.value = data[0];
            txtBabitRegNumber.value = data[1];
            txtNoSurat.value = data[2];
            btnGetInfoBabitHeaderClick()
            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtBabitRegNumber.focus();
            //    txtBabitRegNumber.blur();
            //}
            //else {
            //    txtBabitRegNumber.onchange();
            //}
        }

        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnBabitReportHeaderID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT - INPUT LAPORAN BABIT EVENT</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 50%" valign="top">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                         <tr>
                            <td class="titleField" style="width: 146px">Nomor Registrasi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtBabitRegNumber" 
                                    onblur="omitSomeCharacter('txtBabitRegNumber','<>?*%$');if (this.value != ''){btnGetInfoBabitHeaderClick();}"
                                    runat="server" ToolTip="Babit Event Search 1" Width="128px" ReadOnly="true">
                                </asp:TextBox>
                                <asp:Label ID="lblPopUpBabitHeader" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <input id="hdnBabitHeaderID" type="hidden" value="" runat="server">
                                <asp:Button ID="btnGetInfoBabitHeader" runat="server" Style="display: none"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblDealerCodeName" runat="server"></asp:Label>
                                <input id="hdnDealerID" type="hidden" value="" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Temporary Outlet</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblDealerBranchCodeName" runat="server"></asp:Label>
                                <input id="hdnDealerBranchID" type="hidden" value="" runat="server">
                            </td>
                        </tr>                    
                        <tr>
                            <td class="titleField" style="width: 146px">Area</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblAreaName" runat="server"></asp:Label>
                                <input id="hdnAreaName" type="hidden" value="" runat="server">
                            </td>
                        </tr>  
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Surat</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNoSurat" Width="150px" Enabled="false" runat="server"></asp:TextBox></td>
                        </tr> 
                        <tr valign="top">
                            <td class="titleField" style="width: 146px">Periode</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            <asp:Label ID="lblPeriodStart" runat="server"></asp:Label></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            <asp:Label ID="lblPeriodEnd" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                     </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
                         <tr>
                            <td class="titleField" style="width: 146px">Kategori Kegiatan</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlBabitMasterEventType" Enabled="false" AutoPostBack="true" Width="140px" runat="server" /></td>
                        </tr>                     
                        <tr>
                            <td class="titleField" style="width: 146px">Lokasi Event</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtLocationName" runat="server" Enabled="false" Width="178px"/></td>
                        </tr>
                        <tr id="trProvinsi">
                            <td class="titleField">
                                <asp:Label ID="lblProvinsi" runat="server">Provinsi</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlProvinsi" runat="server" Enabled="false" Width="140px" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblKota" runat="server">Kota / Kab</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlKota" runat="server" Enabled="false" Width="140px"></asp:DropDownList>
                            </td>
                        </tr>                       
                    </table>
                </td>
            </tr>
        </table>
        <hr />
        
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label6" runat="server" Text="Kehadiran Undangan" Font-Size="15px" Font-Bold="True"></asp:Label>
        </div>
        <table>
            <tr>
                <td style="width: 50%" valign="top">
                    <table>
                        <tr>
                            <td class="titleField" style="width: 146px">Jumlah Undangan</td>
                            <td style="width: 2px">:</td>
                            <td valign="middle">
                                <asp:TextBox ID="txtInvitationQty" Style="text-align: right" runat="server" text="0"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                                &nbsp;<label>Undangan</label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 146px">Jumlah Kehadiran</td>
                            <td style="width: 2px">:</td>
                            <td valign="middle">
                                <asp:TextBox ID="txtAttendeeQty" Style="text-align: right" runat="server" text="0"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                                &nbsp;<label>Hadir</label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%" valign="top">

                </td>                
            </tr>
        </table>
        <div>
            <hr />
        </div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label5" runat="server" Text="Kategori Display dan Target Penjualan" Font-Size="15px" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 80%">
            <asp:DataGrid ID="dgDisplayAndTarget" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <%# container.itemindex+1 %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKategoriKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Model">
                        <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblModelKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty Display">
                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyDisplay" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Target Penjualan">
                        <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTargetPenjualan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Test Drive">
                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTestDrive" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div>
            <hr />
        </div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label2" runat="server" Text="Biaya" Font-Size="15px" Font-Bold="True">Rincian Kegiatan</asp:Label>
        </div>
        <div style="width: 80%; overflow: scroll">
            <asp:DataGrid ID="dgBabitReportEvent" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label
                                ID="lblCategoryBabitEvent" runat="server" Text='' Font-Bold="true">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFCategoryBabitEvent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFCategoryBabitEvent_SelectedIndexChanged">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlECategoryBabitEvent" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblJenisBabitEvent" runat="server" Text=''>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFJenisBabitEvent" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEJenisBabitEvent" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Deskripsi">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFDesc" runat="server" Width="460px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEDesc" runat="server" Width="460px" Text='<%#DataBinder.Eval(Container, "DataItem.Description")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo" Width="40px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton id="lbtnCancel" Runat="server" CausesValidation="True" CommandName="cancel" text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div><hr /></div>
        
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label7" runat="server" Font-Size="15px" Text="Daftar Prospek Yang di Dapat Saat Acara" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 80%; overflow: scroll">
            <asp:DataGrid ID="dgBabitEventSPKProspek" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" 
                CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis Kendaraan" SortExpression="VechileTypeKind">
                        <HeaderStyle Width="120px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileTypeKind" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeKind")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tipe Kendaraan" SortExpression="VechileTypeName">
                        <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileTypeName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer" HeaderStyle-Width="100px" SortExpression="DealerCode">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="DealerName">
                        <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Unit" SortExpression="QtyUnit">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyUnit" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div><hr /></div>

        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label4" runat="server" Font-Size="15px" Text="Open Sales (SPK saat acara)" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 80%; overflow: scroll">
            <asp:DataGrid ID="dgBabitEventSPK" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" 
                CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis Kendaraan" SortExpression="VechileTypeKind">
                        <HeaderStyle Width="120px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileTypeKind" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeKind")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tipe Kendaraan" SortExpression="VechileTypeName">
                        <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileTypeName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer" HeaderStyle-Width="100px" SortExpression="DealerCode">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="DealerName">
                        <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Unit" SortExpression="QtyUnit">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyUnit" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div><hr /></div>

        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label3" runat="server" Font-Size="15px" Text="Aktivitas" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 80%; overflow: scroll">
            <asp:DataGrid ID="dgBabitReportEventActivity" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis Aktivitas">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="25%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblActivityType" runat="server" Text='' Font-Bold="true">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFActivityType" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEActivityType" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Deskripsi">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFDescription" runat="server" Width="350px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEDescription" runat="server" Width="350px" Text='<%#DataBinder.Eval(Container, "DataItem.Description")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton id="lbtnCancel" Runat="server" CausesValidation="True" CommandName="cancel" text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div><hr /></div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label1" runat="server" Font-Size="15px" Text="Lampiran" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 80%">
            <asp:DataGrid ID="dgBabitReportDocument" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10px" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama File">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <input id="UploadFile" onkeydown="return false;" style="width: 267px; height: 20px" type="file" size="25" name="File1" runat="server">
                            <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Keterangan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileDescription")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtKeterangan" runat="server" Width="350px" />
                        </FooterTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="Delete" Text="Hapus" runat="server">
                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div>
            <hr />
        </div>
        <br />
        <label runat="server" id="lblNotes"><b>Evaluasi Marbox dan Event (Kisah Sukses, Hambatan, Komentar, dll):</b></label><br />
        <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine" Height="130px"></asp:TextBox>
        <div>
            <br />
        </div>

        <asp:Button ID="btnBaru" runat="server" Text="Baru" Style="display: none"></asp:Button>&nbsp;
        <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan"></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>&nbsp;
        <asp:Button ID="btnCetak" runat="server" Text="Cetak" OnClientClick="window.print()" Style="width:60px"></asp:Button>
    </form>
</body>
</html>
