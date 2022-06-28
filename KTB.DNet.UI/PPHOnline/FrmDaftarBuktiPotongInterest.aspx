<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarBuktiPotongInterest.aspx.vb" Inherits=".FrmDaftarBuktiPotongInterest" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtDealerCode");
            txtDealerSelection.value = tempParam;
        }

        function setCheckAll() {
            var txtChkAll = document.getElementById("txtChkAll");
            if (txtChkAll.value == '0') {
                txtChkAll.value = '1';
            }
            else {
                txtChkAll.value = '0';
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">Status DO - Daftar Bukti Potong PPH Interest</td>
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
                    <asp:TextBox ID="txtDealerCode" runat="server"></asp:TextBox>
                    <asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPPDealerSelection()"></asp:label>
                </td>
                <td style="width:50px"></td>
                <td class="titleField" width="100px">Tgl Pengajuan</td>
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
            </tr>
            <tr>    
                <td class="titleField" >No Bukti Potong</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtNoBuktiPotong" runat="server"></asp:TextBox>
                </td>
                <td></td>
                <td class="titleField">No Reg</td>
                <td class="titleField">:</td>
                <td>
                    <asp:TextBox ID="txtNoReg" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>    
                <td class="titleField" >Status</td>
                <td class="titleField">:</td>
                <td>
                   <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                </td>
                <td></td>
                <td class="titleField"></td>
                <td class="titleField"></td>
                <td></td>
            </tr>
            <tr>
                <td><td><asp:Button ID="btnSearch" runat="server" Text="Cari"  /></td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                     <asp:DataGrid ID="dgDaftarBukti" runat="server" Width="100%" AllowPaging="True" PageSize="50" AllowCustomPaging="True" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
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
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkAdd', document.all.chkAllItems.checked); setCheckAll();"
                                        type="checkbox">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAdd" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                                                
                            <asp:TemplateColumn SortExpression="ID" HeaderText="No">
                                <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                                                
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Code">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="NoReg" HeaderText="No Pengajuan/Reg">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoReg" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="WitholdingNumber" HeaderText="No Bukti Potong">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoBuktiPotong" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="TaxPeriod" HeaderText="Periode Pajak">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriodePajak" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                                                
                            <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tgl Pengajuan">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTanggalPengajuan" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="TotalPPHAmount" HeaderText="Total PPH dipotong">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalPPH" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="DealerNPWP" HeaderText="NPWP Pemotong">
                                <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNPWPPemotong" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Catatan">
                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCatatan" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="SubmissionStatus" HeaderText="Status">
                                <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Lampiran">
                                <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton id="lbtnDownloadEv" runat="server" Text="Download" CausesValidation="False" CommandName="DownloadEv">
												<img src="../images/download.gif" border="0" alt="Evidence"></asp:LinkButton>
                                    <asp:LinkButton id="lbtnDownloadRef" runat="server" Text="Download" CausesValidation="False" CommandName="DownloadRef">
												<img src="../images/download.gif" border="0" alt="Reference"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Actions">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit">
										<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                    <asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="true" CommandName="View">
										<img src="../images/detail.gif" alt="Perubahan Status"></asp:LinkButton>
									
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:DropDownList ID="ddlProcess" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnProcess" runat="server" Text="Proses"  />
                    <asp:Button ID="btnDownlaod" runat="server" Text="Download Report"  />

                    <asp:Button ID="btnDownloadMultipleAttachment" runat="server" Text="Download Multiple Attachment"  />
                    
                    <asp:TextBox ID="txtChkAll" runat="server" Text="0" style="visibility:hidden"></asp:TextBox>
                    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
