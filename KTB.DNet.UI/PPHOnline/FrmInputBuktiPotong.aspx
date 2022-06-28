
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBuktiPotong.aspx.vb" Inherits=".FrmInputBuktiPotong" MaintainScrollPositionOnPostback="true" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PPH Interest</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var dealerCode = selectedDealer.split(";")
            var txtDealerSelection = document.getElementById("txtDealerCode");
            txtDealerSelection.value = dealerCode[0];
        }

        function ShowPPDealerSelectionInput() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelectionInput);
          
        }

        function DealerSelectionInput(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealerInput");
            txtDealerSelection.value = selectedDealer;
        }
       
        function NumericOnlyWithMaxLength(event, ln, txtBoxElm)
        {
            var txtBox = txtBoxElm.value;
            if (txtBox.length < ln)
            {
                return NumericOnlyWith(event, '');
            }
            else
            {
                return false;
            }

        }
        function validateMonth()
        {
            var txtBox = document.getElementById('txtMasaPajakBulan').value;
            if (parseInt(txtBox) > 12)
            {
                alert('Format bulan salah');
            }
        }

        function loadDealerName()
        {
            var btnLoadDN = document.getElementById('btnLoadDealerName');
            btnLoadDN.click();
        }

        
	</script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">
                    <asp:Label ID="lblTitle" runat="server" Text="Label title"></asp:Label>
                </td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>

        <table>
            <tr>    
                <td class="titleField" style="width:100px">Kode Dealer</td>
                <td class="titleField">:</td>
                <td style="width:200px">
                    <asp:TextBox ID="txtDealerCode" runat="server" onBlur="loadDealerName();"></asp:TextBox>
                    <asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPPDealerSelection();"></asp:label>
                    <asp:Button ID="btnLoadDealerName" runat="server" Text="Button" style="visibility: hidden; display: none;" OnClick="btnLoadDealerName_Click" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtDealerCode"></asp:RequiredFieldValidator>   
                </td>
                <td style="width:50px"></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>    
                <td class="titleField">Nama Dealer</td>
                <td class="titleField">:</td>
                <td><asp:Label ID="lblNamaDealer" runat="server"></asp:Label></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>    
                <td class="titleField" >No Pengajuan</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtNoPengajuan" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td></td>
                <td class="titleField">Status</td>
                <td class="titleField">:</td>
                <td>
                   <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>    
                <td class="titleField" >No Bukti Potong</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtNoBuktiPotong" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtNoBuktiPotong"></asp:RequiredFieldValidator> 
                </td>
                <td></td>
                <td class="titleField">Catatan</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>    
                <td class="titleField" >Masa Pajak</td>
                <td class="titleField">:</td>
                <td>
                    Bulan:<asp:TextBox ID="txtMasaPajakBulan" runat="server" Width="20px" onkeypress="return NumericOnlyWithMaxLength(event, 2, this)" onblur="validateMonth()"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtMasaPajakBulan"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Tahun:<asp:TextBox ID="txtMasaPajakThn" runat="server" Width="40" onkeypress="return NumericOnlyWithMaxLength(event, 4, this)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ControlToValidate="txtMasaPajakThn"></asp:RequiredFieldValidator>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td class="titleField">IDENTITAS PEMOTONG PAJAK</td>
            </tr>
        </table>
        <table>
            <tr>    
                <td class="titleField" style="width:100px">NPWP Pemotong</td>
                <td class="titleField">:</td>
                <td style="width:200px">
                    <asp:TextBox ID="txtNPWPPemotong" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtNPWPPemotong"></asp:RequiredFieldValidator>
                </td>
                <td style="width:50px"></td>
                <td class="titleField">Tanggal pemotongan</td>
                <td class="titleField">:</td>
                <td>
                    <cc1:IntiCalendar ID="icTglPemotongan" runat="server"></cc1:IntiCalendar>
                </td>
            </tr>
            <tr>    
                <td class="titleField" >Nama Wajib Pajak Pemotong</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtNamaPemotong" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtNamaPemotong"></asp:RequiredFieldValidator>
                </td>
                <td></td>  
                <td class="titleField" >Nama Penandatangan</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtNamaPenandatangan" runat="server"></asp:TextBox>
                   
                </td>
            </tr>
            
        </table>
        <br />
        <table>
            <tr>
                <td class="titleField">PAJAK PENGHASILAN YANG DIPOTONG</td>
            </tr>
        </table>
        <table>
            <tr>    
                <td class="titleField" style="width:100px">Jumlah Penghasilan Bruto</td>
                <td class="titleField">:</td>
                <td style="width:200px">
                    <asp:TextBox ID="txtPenghasilanBruto" runat="server" onkeypress="return NumericOnlyWith(event, '');" Text="0" onblur="pic(this,this.value,'9999999999','N')" CssClass="textRight" onkeyup="pic(this,this.value,'9999999999','N')"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txtPenghasilanBruto"></asp:RequiredFieldValidator>
                </td>
                <td style="width:50px"></td>
                <td class="titleField" style="width:100px">Jumlah PPH</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtPPH" runat="server" onkeypress="return NumericOnlyWith(event, '');" Text="0" onblur="pic(this,this.value,'9999999999','N')" CssClass="textRight" onkeyup="pic(this,this.value,'9999999999','N')"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txtPPH"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <br />

        <asp:Panel ID="pnlUploadDoc" runat="server">
            
            <table id="tblUpload">
                <tr>
                    <td class="titleField" style="width:100px">Upload E-Form</td>
                    <td>:</td>
                    <td  colspan="2">
                        <INPUT id="iEvidenceDoc" type="file" size="25" name="File1" runat="server" onkeypress="return false;" style="border:thin">  &nbsp;&nbsp;  <asp:Button id="btnUploadEvidence" runat="server" Text="Upload" CausesValidation="False"></asp:Button>
                    </td>
                    <%--<td style="width:50px"></td>--%>
                    <td>
                      
                        <br />
                        <span>
                        <asp:Label ID="lblUploadedFile" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblWarningEvidence" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:LinkButton id="btnDownloadEv" runat="server" Text="Download" CausesValidation="False" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                        <asp:HiddenField ID="hdnPathEvidence" runat="server" />
                            </span><br />
                        <asp:Button ID="btnParsePDF" runat="server" Text="Baca File PDF" Visible="false" style="display:none;" Enabled="false" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblFileFormatInfoEv" runat="server" Text="" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="titleField" style="width:100px">Upload Berkas Export Bupot PPH 23</td>
                    <td>:</td>
                    <td  colspan="2">
                        <INPUT id="iReferenceDoc" type="file" size="25" name="File1" runat="server" onkeypress="return false;" style="border:thin"> &nbsp;&nbsp;<asp:Button id="btnUploadReference" runat="server" Text="Upload" CausesValidation="False"></asp:Button>
                    </td>
                    <%--<td style="width:50px"></td>--%>
                    <td>
                        
                        <asp:Label ID="lblUploadedReference" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblWarningReference" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:LinkButton id="btnDownloadRef" runat="server" Text="Download" CausesValidation="False" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                        <asp:HiddenField ID="hdnPathRef" runat="server" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="lbtnTemplateRef" runat="server">Download Template</asp:LinkButton><br />
                        <asp:Label ID="lblFileFormatInfoRef" runat="server" Text="" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="titleField" style="width:100px">Nama Dokumen Refrensi</td>
                    <td class="titleField">:</td>
                    <td>
                        <asp:TextBox ID="txtNamaDocReferensi" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txtNamaDocReferensi" Enabled="false" Visible="false"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtPembetulanKe" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td></td>
                     
                </tr>
            </table>            
        </asp:Panel>
        
        <asp:Button ID="btnSearch" runat="server" Text="Cari" />
        <br />

        <asp:Button ID="btnOpenModal" runat="server" Text="Tambah Data" />
            <br />
            <asp:Panel ID="pnlGeneratePayment" runat="server" BackColor="#CCCCCC" Width="600px" Visible="false">
                <h2>Input / Edit</h2>
                <table>        
                    <tr>
                        <td class="titleField" width="100px">Tgl Invoice</td>
                        <td class="titleField">:</td>
                        <td>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <cc1:IntiCalendar ID="icPeriodStart" runat="server"></cc1:IntiCalendar>
                                    <td>s.d</td>
                                    <td>
                                        <cc1:IntiCalendar ID="icPeriodEnd" runat="server"></cc1:IntiCalendar>
                                </tr>
                            </table>
                        </td>
                        <td style="width:100px"></td>
                        <td class="titleField">Status</td>
                        <td class="titleField">:</td>
                        <td><asp:DropDownList ID="ddlStatusInput" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="titleField">Kode Dealer</td>
                        <td class="titleField">:</td>
                        <td>
                            <asp:TextBox ID="txtKodeDealerInput" runat="server"></asp:TextBox>
                            <asp:label id="Label1" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPPDealerSelectionInput()"></asp:label>
                        </td>
                        <td style="width:100px"></td>
                        <td class="titleField">No SO</td>
                        <td class="titleField">:</td>
                        <td>
                            <asp:TextBox ID="txtSOInput" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                        <td colspan="7">

                                <asp:LinkButton ID="lnkSOdownload" runat="server">Download SO upload</asp:LinkButton><br />
                        <asp:Label ID="Label3" runat="server" Text="" ForeColor="Green"></asp:Label>

                                    <INPUT id="fileSO" type="file" size="25" name="File1" runat="server" onkeypress="return false;" style="border:thin"> &nbsp;&nbsp;<asp:Button id="btnSOupload" runat="server" Text="SO Upload" CausesValidation="False"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7"><asp:Button ID="btnSearchInput" runat="server" Text="Cari"  /> 
                    </tr>
                </table>
                <br />
                <asp:DataGrid ID="dgListSOInterest" runat="server" Width="100%" AllowPaging="False" PageSize="160" AllowCustomPaging="True" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
                    CellSpacing="1" AllowSorting="True">
                    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                    <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
                    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn Visible="False" HeaderText="ID">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text=''>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn>
                            <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkAdd', document.all.chkAllItems.checked)"
                                    type="checkbox">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAdd" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn  HeaderText="No">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn  HeaderText="Dealer Code">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDealerCode" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="No PO">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoPO" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="SO">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSO" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="Billing No">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblBilling" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="Type">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn  HeaderText="%">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPercentage" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="DPP">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDPP" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="PPH">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPPH" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="Nilai Setelah PPH">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAfterPPH" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn  HeaderText="Tanggal Billing">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblBillingDate" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
                <br />
                <asp:Button ID="btnAddInput" runat="server" Text="Tambah Data"  />
                <asp:Button ID="btnCancelInput" runat="server" Text="Batal"  />
            </asp:Panel>


        <table>
            <tr>
                <td>
                    <asp:DataGrid ID="dgBuktiPotong" runat="server" Width="100%" AllowPaging="false" PageSize="200" AllowCustomPaging="True" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
                        CellSpacing="1" AllowSorting="True">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
                        <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                        <Columns>                        
                            <asp:TemplateColumn  HeaderText="No">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="Dealer Code">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="Dealer Name">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="Dealer PO No">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoPO" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="SO">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSO" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="Billing No">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblBilling" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                                                
                            <asp:TemplateColumn  HeaderText="%">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPercentage" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="DPP">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDPP" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="PPH">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPPH" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="Nilai Setelah PPH" Visible="false">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAfterPPH" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="Tanggal Billing">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblBillingDate" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                                 <asp:TemplateColumn  HeaderText="Doc Number">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDocNumber" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn  HeaderText="">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit" Visible="false">
										    <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
										    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Total DPP : <asp:Label ID="lblTotalDPP" runat="server" ></asp:Label>
                            </td>
                            <td>
                                Total PPH : <asp:Label ID="lblTotalPPH" runat="server" ></asp:Label>
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan"  />
                <asp:Button ID="btnValidate" runat="server" Text="Validasi"  />
                <asp:Button ID="btnKembali" runat="server" Text="Kembali"  />
                <asp:Button ID="btnTestAja" runat="server" Text="test aja"  Visible="false"  Style ="display:none" />
                </td>
            </tr>
        </table>
        <table style="display:none;">
            <tr>
                <td class="titleField">Upload E-Form</td>
                <td>:</td>
                <td style="width:250px">
                    <INPUT id="File1" type="file" size="25" name="File1" runat="server" onkeypress="return false;" style="border:thin">
                </td>
                <td>
                    <asp:Button id="Button1" runat="server" Text="Upload" CausesValidation="False" Visible="false"></asp:Button>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
