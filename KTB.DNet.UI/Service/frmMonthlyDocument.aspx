<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmMonthlyDocument.aspx.vb" Inherits="frmMonthlyDocument" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Dokumen Service</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/jquery-1.10.2.js" type="text/javascript"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }


        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function UploadFaktureEvidance() {

        }

    </script>

    <script language="javascript" type="text/javascript">

        function on_create_form_clicked(lnk) {
            try {
                var row = lnk.parentNode.parentNode;
                var message = "Row Index: " + (row.rowIndex - 1);
                //alert(message);
                var tbl = document.getElementById('dtgMonthlyDocument');
                var value = row.cells[0].getElementsByTagName("SPAN")[0].innerHTML

                showPopUp('../General/../PopUp/PopUpUploadFaktureEvidance.aspx?IDmonth=' + String(value) + '&mode=input', '', 500, 760, UploadFaktureEvidance);
            } catch (e) {
                alert(e.message);
            }

        }

        function on_create_form_clicked_view(lnk) {
            try {
                var row = lnk.parentNode.parentNode;
                var message = "Row Index: " + (row.rowIndex - 1);
                //alert(message);
                var tbl = document.getElementById('dtgMonthlyDocument');
                var value = row.cells[0].getElementsByTagName("SPAN")[0].innerHTML

                showPopUp('../General/../PopUp/PopUpUploadFaktureEvidance.aspx?IDmonth=' + String(value) + '&mode=view', '', 500, 760, UploadFaktureEvidance);
            } catch (e) {
                alert(e.message);
            }

        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">

                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">UMUM - Daftar Dokumen Service</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" src="/images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="titleField" style="height: 16px" width="24%">
                                <asp:Label ID="lblDealer" runat="server">Dealer</asp:Label></td>
                            <td style="height: 16px" width="1%">:</td>
                            <td style="height: 16px" width="75%">
                                <asp:TextBox ID="txtKodeDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblPeriode" runat="server"> Periode</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlPeriode" runat="server" Width="120px"></asp:DropDownList><asp:DropDownList ID="ddlPeriodeYear" runat="server" Width="120px"></asp:DropDownList>
                                &nbsp; s.d &nbsp;
                                <asp:DropDownList ID="ddlPeriodeTo" runat="server" Width="120px"></asp:DropDownList><asp:DropDownList ID="ddlPeriodeYearTo" runat="server" Width="120px"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblJenisDokumen" runat="server">Jenis Dokumen</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlJenisDokumen" runat="server" Width="240px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblStatusDownload" runat="server">Status Download</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatusDownload" runat="server" Width="120px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Produk</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nomor Faktur</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNomorFaktur" runat="server" Width="174px"></asp:TextBox>
                                <asp:HiddenField ID="SCDealer" runat="server" />
                                <asp:HiddenField ID="JDoc" runat="server" />
                                <asp:HiddenField ID="NoFaktur" runat="server" />
                                <asp:HiddenField ID="Month" runat="server" />
                                <asp:HiddenField ID="Year" runat="server" />
                                <asp:HiddenField ID="MonthTo" runat="server" />
                                <asp:HiddenField ID="YearTo" runat="server" />
                                <asp:HiddenField ID="PCategory" runat="server" />
                                <asp:HiddenField ID="Downloads" runat="server" />
                                <asp:HiddenField ID="BillingNo" runat="server" />
                                <asp:HiddenField ID="AccountingNo" runat="server" />
                            </td>
                        </tr>   
                        <tr>
                            <td class="titleField">No Accounting</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNoAccounting" runat="server" Width="174px"></asp:TextBox>
                            </td>
                        </tr> 
                        <tr>
                            <td class="titleField">Billing No</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtBillingNo" runat="server" Width="174px"></asp:TextBox>
                            </td>
                        </tr>                    
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnCari" runat="server" Width="64px" Text="Cari" Style="z-index: 0"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="40%">
                    <asp:Label ID="lblAlert" runat="server" ForeColor="#ff6666" Font-Bold="True">
							<b style='mso-bidi-font-weight:normal'>
								<span style='font-size:11.0pt;line-height:115%;font-family:Calibri;mso-fareast-font-family:
									Calibri;mso-bidi-font-family:"Times New Roman";mso-ansi-language:EN-US;
									mso-fareast-language:EN-US;mso-bidi-language:AR-SA'>
								</span>
							</b>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <div id="div1" style="height: 310px; overflow: auto">
                        <asp:DataGrid ID="dtgMonthlyDocument" runat="server" Width="100%" OnItemDataBound="dtgMonthlyDocument_ItemDataBound"
                            OnItemCommand="dtgMonthlyDocument_ItemCommand" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="50"
                            AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#ededed"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" Style="display: none" runat="server"></asp:Label>
                                        <asp:Label ID="lblDownload" runat="server">
												<img src="../images/red.gif" border="0" alt="Belum Didownload"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text="<%# container.itemindex+1 %>">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn SortExpression="Kind" HeaderText="Jenis Dokumen">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Periode">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" SortExpression="FileName" HeaderText="NamaFile">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn SortExpression="FileSize" HeaderText="Ukuran File (kb)">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Dibuat" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn SortExpression="LastDownloadBy" HeaderText="Didownload Oleh">
                                    <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn SortExpression="LastDownloadDate" HeaderText="Tanggal Download" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>


                                <asp:BoundColumn DataField="AccountingNo" SortExpression="AccountingNo" HeaderText="Nomor Accounting">
                                    <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BillingNo" SortExpression="BillingNo" HeaderText="Nomor Billing">
                                    <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="TransferDate" SortExpression="TransferDate" HeaderText="Estimasi Tanggal Transfer" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Nomor Faktur">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomorFaktur" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Tanggal Upload">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglUpload" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Tanggal Rencana Transfer" Visible="false">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglTanggalRencanaTransfer" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Tanggal Transfer">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglTransfer" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Aktual Transfer" Visible="false">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnViewDetail" runat="server" CommandName="ViewDetail">
															<img src="../images/detail.gif" border="0" alt="ViewDetail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDownload" runat="server" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnUpload" runat="server" CommandName="Upload">
												<img src="../images/icon_update.gif" border="0" alt="Upload"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnView" runat="server" CommandName="View">
															<img src="../images/detail.gif" border="0" alt="View"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="FileName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFullName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="hapus"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDeleteDetail" runat="server" CommandName="DeleteDetail">
												<img src="../images/trash.gif" border="0" alt="hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#CCCCCC" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr id="OpClient" runat="server">
                <td valign="middle" colspan="2">
                    <asp:TextBox ID="txtDownload" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 40px" align="left">
                    <asp:Button ID="btnDownload" runat="server" Width="100px" Text="Download"></asp:Button>
                    &nbsp;&nbsp;
                        <asp:Label ID="lblLoading" ForeColor="Red" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
        document.getElementById("txtDownload").style.visibility = "hidden";
        if (document.getElementById("txtDownload").value != "") {
            var downloadURL = document.getElementById("txtDownload").value;
            document.getElementById("txtDownload").value = "";
            document.location.href = "../download.aspx?file=" + downloadURL;

        }
    </script>
    <script language="javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
