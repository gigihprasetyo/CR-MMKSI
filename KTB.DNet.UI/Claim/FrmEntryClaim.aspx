<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryClaim.aspx.vb" Inherits="FrmEntryClaim" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmEntryClaim</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPNoFaktur() {
            showPopUp('../PopUp/PopUpNoFaktur.aspx?claim=1', '', 710, 700, NoFaktur);
        }
        function ShowPPKodeBarangSelection() {
            var txtNoFaktur = document.getElementById("txtNoFaktur");
            var NoSO = document.getElementById("lblNoSO").innerHTML;
            //showPopUp('../PopUp/PopUpSparePartByFaktur.aspx?NoFaktur='+ txtNoFaktur.value ,'',700,700,KodeBarang);
            showPopUp('../PopUp/PopUpSparePartByFaktur.aspx?NoFaktur=' + txtNoFaktur.value + '&NoSO=' + NoSO, '', 700, 700, KodeBarang);
        }
        function KodeBarang(selectedCode) {
            var indek = GetCurrentInputIndex();
            var dtgPesananKendaraan = document.getElementById("dtgEntryClaim");
            var tempParam = selectedCode.split(';');
            var KodeBarang = dtgEntryClaim.rows[indek].getElementsByTagName("INPUT")[0];
            if (navigator.appName == "Microsoft Internet Explorer") {
                KodeBarang.innerText = tempParam[0];
            }
            else {
                KodeBarang.value = tempParam[0];
            }
        }
        function GetCurrentInputIndex() {
            var dtgEntryClaim = document.getElementById("dtgEntryClaim");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dtgEntryClaim.rows.length; index++) {
                inputs = dtgEntryClaim.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }

            return -1;
        }

        function NoFaktur(selectedCode) {
            var tempParam = selectedCode.split(';');
            var NoFaktur = document.getElementById("txtNoFaktur");
            var FakturDate = document.getElementById("lblTglFaktur");
            var NoSO = document.getElementById("lblNoSO");
            var HPONumber = document.getElementById("HPONumber");

            NoFaktur.value = tempParam[0];
            FakturDate.innerHTML = tempParam[1];
            NoSO.innerHTML = tempParam[2];
            HPONumber.value = tempParam[2];


        }

        function change_label() {
            document.getElementById("lblNoSO").innerText = document.getElementById("HPONumber").value;
        }

        function CalculateAmount(qtyClaim, HargaSatuan, Total) {
            var qtyClaimx = document.getElementById(qtyClaim);
            var qtyClaimPrevious = qtyClaim;
            var qtyClaim = document.getElementById(qtyClaim).value.replace('.', '');

            if (qtyClaim == "0") {
                alert('Qty tidak boleh 0. Bila sparepart ini tidak akan diclaim, harap dikosongkan saja');
                //alert
                qtyClaimx.value = '';
                CalculateAmount(qtyClaimPrevious, HargaSatuan, Total)
                return
            }
            var HargaSatuan = document.getElementById(HargaSatuan).innerHTML;
            HargaSatuan = replace(HargaSatuan, '.', '');
            var Total = document.getElementById(Total);
            var partPrice = parseFloat(HargaSatuan);

            if (isNaN(partPrice) || partPrice == "") {
                partPrice = 0;
            }

            var amount = parseInt(qtyClaim) * parseInt(partPrice);
            if (isNaN(amount)) {
                Total.innerHTML = "";
            }
            else {
                var amuntStr = amount.toLocaleString()
                if (navigator.appName == "Microsoft Internet Explorer") {
                    Total.innerHTML = replace(amuntStr.substring(0, amuntStr.length - 3), ',', '.');
                }
                else {
                    var searchStr = ",";
                    var re = new RegExp(searchStr, "g");
                    //var replaceStr = document.myForm.myName.value;
                    //var div = document.getElementById("boilerplate");
                    //div.firstChild.nodeValue = div.firstChild.nodeValue.replace(re, replaceStr);
                    Total.innerHTML = amuntStr.replace(',', '.');
                }
            }

        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">ORDER CLAIM - Pengajuan Claim</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleField">Kode Dealer</td>
                <td>:</td>
                <td>
                    <asp:Literal ID="ltrDealerCode" runat="server"></asp:Literal></td>
                <td class="titleField"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 32px">Nama Dealer</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:Literal ID="ltrDealerName" runat="server"></asp:Literal></td>
                <td class="titleField" style="height: 32px">Nomor Faktur / SO</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoFaktur" onblur="omitSomeCharacter('txtNoFaktur','<>?*%$;')"
                        runat="server"></asp:TextBox><asp:Label ID="lblSearch" TabIndex="20" runat="server" DESIGNTIMEDRAGDROP="32" Height="10px">
							<img style="cursor:hand" alt="Klik Disini untuk memilih No Faktur" src="../images/popup.gif"
								border="0"></asp:Label><asp:Button ID="btnSearch" runat="server" Visible="False" Text="Cari"></asp:Button>
                    &nbsp;  / &nbsp; 
                    <asp:Label ID="lblNoSO" runat="server">  </asp:Label>
                    <asp:HiddenField ID="HPONumber" runat="server" ClientIDMode="Static" />

                </td>
            </tr>
            <tr valign="top">
                <td class="titleField">Nomor / Tanggal Claim</td>
                <td>:</td>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtClaimNo" runat="server" BackColor="#efefef" Enabled="False">[Dibuat oleh sistem]</asp:TextBox></td>
                            <td>
                                <cc1:inticalendar id="icClaimDate" runat="server" Enabled="False" TextBoxWidth="60"></cc1:inticalendar></td>
                        </tr>
                    </table>
                </td>
                <td class="titleField">Tanggal Faktur</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblTglFaktur" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">Status</td>
                <td>:</td>
                <td>
                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal></td>
                <td class="titleField">Penjelasan</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">Alasan Claim</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlReasonClaimHeader" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                <td class="titleField">Lampiran/Bukti</td>
                <td>:</td>
                <td> 
                    <asp:DataGrid ID="dgEvidence" TabIndex="99" runat="server" Width="100%" BorderWidth="1px"
                        BorderColor="#CDCDCD" CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe Bukti">
                                <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimEvidenceType" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList runat="server" ID="ddlClaimEvidenceTypeFooter" Width="100px"></asp:DropDownList>*File Max 1Mb
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="File">
                                <HeaderStyle Width="90%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnFileClaimEVIDENCE" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.FilePath")%>'>
                                        <asp:Label ID="lblFileClaimEVIDENCE" runat="server" alt="Download"></asp:Label>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle Wrap="False"></FooterStyle>
                                <FooterTemplate>
                                    <input type="file" id="iFileClaimEVIDENCE" runat="server" tabindex="0">
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnFileAttachmentTopDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkbtnFileAttachmentTopAdd" runat="server" CommandName="Add" CausesValidation="False"
                                        TabIndex="0" Width="100%">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid> 
                </td>
                    <%--<input id="fuEvidence" type="file" runat="server">&nbsp;
						<asp:Button ID="btnUpload" runat="server" Text="Upload"></asp:Button></td>--%>
            </tr>
            <tr>
                <td class="titleField">Persyaratan
                </td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblSyarat" runat="server" Width="280px"></asp:Label></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="lblFilename" runat="server"></asp:Label></td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 280px">
            <asp:DataGrid ID="dtgEntryClaim" runat="server" BackColor="#CDCDCD" Width="100%" DataKeyField="ID"
                ShowFooter="True" AutoGenerateColumns="False" AllowSorting="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BorderWidth="0px">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text="1"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Size="Small"></FooterStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nomor Barang">
                        <HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoBarang" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartNumber") %>' runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFooterNomorBarang" runat="server" BackColor="White" Width="80px" MaxLength="18"></asp:TextBox>
                            <asp:Label ID="lblFooterNomorBarang" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNomorBarangEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SparePartPOStatusDetail.SparePartMaster.PartNumber" ) %>' BackColor="White" Width="95px" MaxLength="18">
                            </asp:TextBox>
                            <asp:Label ID="lblEditNomorBarang" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Barang">
                        <HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNamaBarang" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartName") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty Claim">
                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyClaim" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtQtyClaimEntry" Width="30px" runat="server" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
                                MaxLength="6" />
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQtyClaimEntry" ErrorMessage="Quantity claim harus lebih besar dari 0"
                                MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQtyClaimEntryEdit" Width="30px" runat="server" CssClass="textRight" onkeypress="return numericOnlyUniv(event)" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>' />
                            <asp:RangeValidator ID="Rangevalidator1" runat="server" ControlToValidate="txtQtyClaimEntryEdit" ErrorMessage="Quantity claim harus lebih besar dari 0"
                                MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Harga Satuan">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblHargaSatuan" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.ClaimPriceUnit"),"#,##0") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" EnableViewState="True" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty Approved">
                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyApproved" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Keterangan">
                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtKeteranganEntry" runat="server" MaxLength="280" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtKeteranganEntryEdit" runat="server" MaxLength="280" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
                        CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
                        EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
                        <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:EditCommandColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
             <br />
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p>Pesan Error :</p><br />
            <p class="text-danger">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>

        </div>
        <br>
        <br />
       

        <asp:Button ID="btnSave" Text="Simpan" runat="server"></asp:Button><asp:Button ID="btnCancel" runat="server" Text="Kembali"></asp:Button>
    </form>
</body>
</html>
