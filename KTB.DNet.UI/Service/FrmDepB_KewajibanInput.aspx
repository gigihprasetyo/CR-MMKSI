<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_KewajibanInput.aspx.vb" Inherits=".FrmDepB_KewajibanInput" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmMaterialPromotionUpload</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

        var indexRow;

        function GetIndex(CtlID) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                var row = CtlID.parentElement.parentElement;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
            else {
                var row = CtlID.parentNode.parentNode;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
        }

        function ShowEquipPart(lblId) {
            var indek = GetIndex(lblId);
            var ddlKewajiban = document.getElementById("ddlKewajiban");
            val = ddlKewajiban.value;
            if (val == 1) {
                showPopUp('../PopUp/PopUpEquipment.aspx', '', 500, 760, SelectedEquipment)
            }
            else {
                showPopUp('../PopUp/PopUpSparePart.aspx?IPMaterialtype=2', '', 700, 700, SelectedSparePart);
            }
        }

        function ShowEquipPartEdit(lblId) {
            var indek = GetIndex(lblId);
            var ddlKewajiban = document.getElementById("ddlKewajiban");
            val = ddlKewajiban.value;
            if (val == 1) {
                showPopUp('../PopUp/PopUpEquipment.aspx', '', 500, 760, SelectedEquipment)
            }
            else {
                showPopUp('../PopUp/PopUpSparePart.aspx?IPMaterialtype=2', '', 700, 700, SelectedSparePartEdit);
            }
        }

        function SelectedEquipment(selectedType) {
            //alert(selectedType);
            //return;
            var indek = indexRow;
            var temp = selectedType.split(";")
            var dgUpload = document.getElementById("dgUpload");
            var PartID = dgUpload.rows[indek].getElementsByTagName("INPUT")[0];
            PartID.value = temp[5];
            var Kode = dgUpload.rows[indek].getElementsByTagName("INPUT")[1];
            Kode.value = temp[0];
            var lblPartName = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartNameFooter");
            lblPartName.innerText = temp[0];
            var lblPartName = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartNameFooter");
            lblPartName.innerText = temp[1];
            var lblAmountFooter = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblAmountFooter");
            lblAmountFooter.innerText = addCommas(temp[2]);
        }

        function SelectedSparePart(selectedType) {
            var indek = indexRow;
            var temp = selectedType.split(";")
            var dgUpload = document.getElementById("dgUpload");
            var PartID = dgUpload.rows[indek].getElementsByTagName("INPUT")[0];
            PartID.value = temp[5];
            var Kode = dgUpload.rows[indek].getElementsByTagName("INPUT")[1];
            Kode.value = temp[0];
            var lblPartName = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartNameFooter");
            lblPartName.innerText = temp[0];
            var lblPartName = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartNameFooter");
            lblPartName.innerText = temp[1];
            var lblAmountFooter = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblAmountFooter");
            lblAmountFooter.innerText = addCommas(temp[2]);

            //var lblPartIDFooter = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartIDFooter");
            //lblPartIDFooter.innerText = temp[5];
        }

        function SelectedSparePartEdit(selectedType) {
            var indek = indexRow;
            var temp = selectedType.split(";")
            var dgUpload = document.getElementById("dgUpload");
            var PartID = dgUpload.rows[indek].getElementsByTagName("INPUT")[0];
            PartID.value = temp[5];
            var Kode = dgUpload.rows[indek].getElementsByTagName("INPUT")[1];
            Kode.value = temp[0];
            var lblPartName = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartNameEdit");
            lblPartName.innerText = temp[0];
            var lblPartName = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartNameEdit");
            lblPartName.innerText = temp[1];
            var lblAmountFooter = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblAmountEdit");
            lblAmountFooter.innerText = addCommas(temp[2]);

            //var lblPartIDFooter = document.getElementById("dgUpload__ctl" + (indek + 1) + "_lblPartIDEdit");
            //lblPartIDFooter.innerText = temp[5];
        }

        function addCommas(nStr) {
            nStr += '';
            x = nStr.split(',');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + '.' + '$2');
            }
            return x1 + x2;
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
    <style type="text/css">
        .HiddenItem {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Deposit B&nbsp;-&nbsp;Input Kewajiban</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                    runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tipe Kewajiban</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlKewajiban" runat="server" Width="140px" AutoPostBack="True"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tahun</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Produk</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlProductCategory" runat="server" Width="140px" ></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblFile" runat="server">File To Upload</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td width="80%">
                                <input onkeypress="return false;" id="fileUpload" style="width: 400px; height: 20px" type="file"
                                    size="47" name="fileUpload" runat="server">&nbsp;
                                <asp:Button ID="btnUpload" runat="server" Text="Upload"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="80%"><em><font color="#6600ff">* Tipe file yang disupport : Excel (*.xls / *.xlsx) dengan 
											baris pertama sebagai judul kolom</font></em>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="80%">
                                <asp:LinkButton ID="LinkDownload" runat="server">Template  Upload Excel</asp:LinkButton>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="6">
                                <div id="div1" style="overflow: auto; width: 100%; height: 260px">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:DataGrid ID="dgUpload" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True"
                                                    BorderColor="#CDCDCD" BorderStyle="None" PageSize="1000" BorderWidth="1px" BackColor="#E0E0E0"
                                                    CellPadding="3" ShowFooter="True">
                                                    <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                                                    <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                                    <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                                    <ItemStyle BackColor="White"></ItemStyle>
                                                    <HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="No">
                                                            <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNo" runat="server" Text='<%# container.itemindex+1 %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Kode Barang">
                                                            <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartCode" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="lblPartIDEdit" runat="server" CssClass="HiddenItem"></asp:TextBox>
                                                                <asp:TextBox ID="txtPartCodeEdit" Width="120px" runat="server" CssClass="textLeft" />
                                                                <asp:Label ID="lblPartCodeEdit" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterStyle HorizontalAlign="center" VerticalAlign="Top"></FooterStyle>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="lblPartIDFooter" runat="server" CssClass="HiddenItem" ></asp:TextBox>
                                                                <asp:TextBox ID="txtPartCodeFooter" Width="120px" runat="server" CssClass="textLeft" />
                                                                <asp:Label ID="lblPartCodeFooter" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                            </FooterTemplate>

                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Nama Barang">
                                                            <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblPartNameEdit" runat="server"></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterStyle HorizontalAlign="left"></FooterStyle>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblPartNameFooter" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Harga">
                                                            <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblAmountEdit" runat="server"></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterStyle HorizontalAlign="right"></FooterStyle>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblAmountFooter" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Qty">
                                                            <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtQtyEdit" Width="60px" runat="server" Style="text-align: right" onkeypress="return NumberOnly()" />
                                                            </EditItemTemplate>
                                                            <FooterStyle HorizontalAlign="center"></FooterStyle>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtQtyFooter" Width="60px" runat="server" Style="text-align: right" onkeypress="return NumberOnly()" />
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pesan Kesalahan" Visible="false">
                                                            <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMessage" runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Top"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>

                                                                <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                                                    ImageUrl="../images/trash.gif" alt="Hapus" />

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lbtnSave" TabIndex="40" runat="server" CausesValidation="True" CommandName="Update"
                                                                    Text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnCancel" TabIndex="50" runat="server" CausesValidation="True" CommandName="Cancel"
                                                                    Text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                            </EditItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" Width="2%" VerticalAlign="Top"></FooterStyle>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
                                                                </asp:LinkButton>
                                                            </FooterTemplate>

                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <%--<td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>--%>
                            <td colspan="3">
                                <asp:Button ID="btnSave" TabIndex="50" runat="server" Text="Simpan" CausesValidation="False"></asp:Button>&nbsp;
								<asp:Button ID="btnCancel" runat="server" Text="Batal" Width="64px" EnableViewState="true"></asp:Button>
                                <asp:Button ID="btnBack" runat="server" Text="Kembali" Width="64px"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="80%">
                                <asp:Label ID="lblError" runat="server" Width="200px" ForeColor="Red" EnableViewState="False"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 151px" colspan="2"></td>
            </tr>
        </table>
    </form>
</body>
</html>
