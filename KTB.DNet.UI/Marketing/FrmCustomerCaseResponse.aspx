<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCustomerCaseResponse.aspx.vb" Inherits="FrmCustomerCaseResponse" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Case Management</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/FormFunctions.js"></script>

    <script language="javascript" type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" language="javascript">
        function setSelected(selRow) {
            var tbl = document.getElementById("dgCase");
            var row = selRow.parentNode.parentNode.parentNode;
            var rowTbl = selRow.parentNode.parentNode.parentNode.parentNode;
            var rowIndex = row.rowIndex;
            for (var i = 1; i < tbl.rows.length; i++) {
                if (rowIndex == i) {
                    tbl.rows[i].style.fontWeight = "Bold";
                    var oldColor = tbl.rows[i].style.backgroundColor;
                    tbl.rows[i].style.backgroundColor = "#A8AEB7";

                    if (!confirm('Yakin ingin menghapus data ini?')) {
                        tbl.rows[i].style.fontWeight = "Normal";
                        tbl.rows[i].style.backgroundColor = oldColor;
                        return false;
                    }
                    tbl.rows[i].style.fontWeight = "Normal";
                    tbl.rows[i].style.backgroundColor = oldColor;
                    break;
                }
            }
        }

        function SetUniqueRadioButton(strGroupName, current) {
            $("input[name$='" + strGroupName + "']").attr('checked', false);
            current.checked = true;
        }

        function onlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57) || (charcode ==58))
                return false;
            return true;
        }
    </script>

    <style type="text/css">
            #div1 {
              position:relative;
              top:1em; /* or whatever the header height is */
              right:0;
              bottom:0;
              left:0;
              overflow-x:auto;
            }
            .DataGridFixedHeader {background-color: white; position:relative; top:expression(this.offsetParent.scrollTop);}
            .Space label
            {
               margin-left: 5px;
            }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Customer Satisfaction&nbsp;- Case Management 
                    <asp:Label ID="lblTittle" runat="server" Text="Label"></asp:Label>
                </td>
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
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">

                        <tr valign="top">
                            <td class="titleField" height="20">Kode Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblDealer" runat="server"></asp:Label></td>

                            <td class="titleField">Nomor Case</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblCaseNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label2" runat="server">Nama Konsumen</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Sumber Informasi</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCaseNumberRef" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Phone</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Tanggal Case</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCaseDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Email</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Kategori</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCategory" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Tipe Kendaraan</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblVehicleType" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Kategori 1</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCategory1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Varian</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblVariant" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Kategori 2</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCategory2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Nomor Mesin</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblEngineNo" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Kategori 3</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCategory3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Nomor Rangka</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblChassisNo" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Kategori 4</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblCategory4" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Nomor Polisi</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblNoPol" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%">Tipe</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">Odometer</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblOdometer" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%" valign="top">Subyek</td>
                            <td width="1%" valign="top">:</td>
                            <td width="30%">
                                <asp:Label ID="lblSubject" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%"></td>
                            <td width="1%"></td>
                            <td width="30%">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" width="18%" valign="top">Tanggal <asp:Label ID="lblKeteranganTambahan" runat="server"></asp:Label></td>
                            <td width="1%" valign="top">:</td>
                            <td width="30%">
                                <asp:Label ID="lblTanggalX" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%"></td>
                            <td width="1%"></td>
                            <td width="30%"></td>
                            <td class="titleField" width="18%" valign="top">Keterangan</td>
                            <td width="1%" valign="top">:</td>
                            <td width="30%">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlEntry" runat="server">
                        <table id="tblEntry" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr valign="top">
                                <td class="titleField" height="20" width="18%">Status</td>
                                <td class="titleField" height="20" width="1%">:</td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    &nbsp;&nbsp<asp:Label ID="lblValidddlStatus" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
                                    <asp:Label ID="lblResponseID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trChangeBook" runat="server" visible="false" valign="top">
                                <td class="titleField" height="20" width="18%">Perubahan Waktu Booking</td>
                                <td class="titleField" height="20" width="1%">:</td>
                                <td valign="top" align="left">
                                    <table border="0">
                                        <tr>
                                            <td><cc1:IntiCalendar ID="tglBooking" runat="server"></cc1:IntiCalendar></td>
                                            <td>
                                                <asp:TextBox ID="txtClock" runat="server" Text="12:00" Width="50px"  onkeypress="return alphaNumericExcept(event,'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz')"></asp:TextBox>
                                                <asp:Label ID="lblClock" runat="server" Text="hh:mm"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trResponse" runat="server" valign="top">
                                <td class="titleField" height="20" width="18%">Template Respon</td>
                                <td class="titleField" height="20" width="1%">:</td>
                                <td valign="top">
                                    <table border="0">
                                        <tr>
                                            <td style="border:1px solid" width="20%" valign="top">
                                                <%--Dealer                                             
                                                <asp:RadioButtonList ID="rbtnResponses" runat="server" AutoPostBack="True">
                                                    <asp:ListItem Value="1" Selected="True">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="2">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="3">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="4">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="5">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="6">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="7">Respon lain</asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                                <asp:Repeater ID="rptDealerResp" runat="server" OnItemDataBound="rptDealerResp_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table cellpadding="3" cellspacing="1" border="0">
                                                        <th align="left">Dealer</th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID ="rbDealer" runat="server" CssClass="Space"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ValueDesc")%>' 
                                                                    GroupName="response" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td style="border:1px solid" width="20%" valign="top">
                                                <%--Customer
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True">
                                                    <asp:ListItem Value="1" Selected="True">Customer 1</asp:ListItem>
                                                    <asp:ListItem Value="2">Customer 1</asp:ListItem>
                                                    <asp:ListItem Value="3">Customer 1</asp:ListItem>
                                                    <asp:ListItem Value="4">Customer 1</asp:ListItem>
                                                    <asp:ListItem Value="5">Customer 1</asp:ListItem>
                                                    <asp:ListItem Value="6">Customer 1</asp:ListItem>
                                                    <asp:ListItem Value="7">Customer lain</asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                                <asp:Repeater ID="rptCustResp" runat="server" OnItemDataBound="rptCustResp_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table cellpadding="3" cellspacing="1" border="0">
                                                        <th align="left">Customer</th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID ="rbCust" runat="server" CssClass="Space"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ValueDesc")%>' 
                                                                    GroupName="response" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" height="20" width="18%">Komentar</td>
                                <td class="titleField" height="20" width="1%">:</td>
                                <td valign="top">
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="5" MaxLength="100" Width="450px" onkeypress="return alphaNumericExcept(event,'<>?*%$;%^&@#!')" onblur="omitSomeCharacter('txtComment','<>?*%$;%^&@#!')"></asp:TextBox>
                                    &nbsp;&nbsp<asp:Label ID="lblValidtxtComment" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" height="20" width="18%">File Pendukung
                                    <br />
                                    <i>
                                        <asp:Label ID="Label3" runat="server" Font-Size="XX-Small" ForeColor="Red" EnableViewState="False">(Maksimal file 3 MB & <br />Tipe file jpg, jpeg, bmp, png, pdf)</asp:Label></i></td>
                                <td class="titleField" height="20" width="1%">:</td>
                                <td>
                                    <table>
                                        <tr valign="middle">
                                            <td>
                                                <div id="div2" style="width: 450px; overflow: auto">
                                                    <asp:DataGrid ID="dgEvidenceFile" TabIndex="99" runat="server" Width="100%" BorderWidth="1px"
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
                                                                    <asp:Literal ID="ltrEvidenceFileNo" runat="server"></asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="File">
                                                                <HeaderStyle Width="90%" CssClass="titleTableService"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEvidenceFileName" runat="server"><%# DataBinder.Eval(Container, "DataItem.FileName")%></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle Wrap="False"></FooterStyle>
                                                                <FooterTemplate>
                                                                    <input type="file" id="iFileAttachmentEvidence" runat="server" tabindex="0">
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbtnEvidenceFileDelete" runat="server" CommandName="Delete" Text="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnDownload" CommandName="Download" Text="Download" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.EvidenceFile") %>'>
								                            <img border="0" src="../images/download.gif" alt="Download" style="cursor:hand"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lnkbtnEvidenceFileAdd" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												                <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                            <td>&nbsp;&nbsp<asp:Label ID="lblValidFile" runat="server" ForeColor="Red" EnableViewState="False" Visible="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table id="TblCommand" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="18%"></td>
                            <td width="1%"></td>
                            <td width="30%">
                                <asp:Button ID="btnSave" Text="Simpan" runat="server" />&nbsp;
                                <asp:Button ID="btnBatal" Text="Batal" runat="server" />&nbsp;
                                <asp:Button ID="btnBack" Text="Kembali" runat="server" />&nbsp;
                                </td>
                            <td class="titleField" width="5%">
                                <asp:Button ID="btnSB" Text="Service Booking" runat="server" />
                            </td>
                            <td class="titleField" width="5%">
                                
                                <asp:Button ID="btnGSR" Text="Service Reminder" runat="server" />
                            </td>
                            <td width="1%"></td>
                            <td width="30%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>

        <div id="div1" style="overflow:auto;HEIGHT: 500px">
            <asp:DataGrid ID="dgCase" runat="server" Width="100%" DataKeyField="ID" BorderStyle="None"
                AllowPaging="True" PageSize="10" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px"
                BackColor="White" CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1" CssClass="tbl">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F1F6FB"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="White"></ItemStyle>
                <HeaderStyle CssClass="ms-formlabel DataGridFixedHeader" Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                <%--<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="titleTableSales" BackColor="#000084"></HeaderStyle>--%>
                <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            &nbsp&nbsp<asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status">
                        <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tanggal Reschedule" SortExpression="BookingDatetime">
                        <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBookingDatetime" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nomor Service Booking" SortExpression="ServiceBooking.ServiceBookingCode" Visible="false">
                        <HeaderStyle CssClass="titleTableMrk" Width="12%"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoServiceBooking" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Nomor Service Booking" SortExpression="ServiceBooking.ServiceBookingCode">
                        <HeaderStyle CssClass="titleTableMrk" Width="14%"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbServiceBooking" runat="server" CausesValidation="False" CommandName="View">
								</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nomor Work Order" SortExpression="WorkOrderNumber">
                        <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoWorkOrder" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Note">
                        <HeaderStyle Width="23%" CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Respon Customer">
                        <HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblResponCustomer" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Respon Dealer">
                        <HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblResponDealer" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Tanggal Update">
                        <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CreatedBy" SortExpression="CreatedBy" HeaderText="User Update">
                        <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Detail">
                        <HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDetail" CommandName="Detail" Text="Detail" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
								<img border="0" src="../images/detail.gif" alt="Detail" style="cursor:hand"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CommandName="Edit" CausesValidation="False" Visible="false">
								<img src="../images/edit.gif" border="0" alt="Ubah" style="cursor:hand"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CausesValidation="False" Visible="false">
								<img src="../images/trash.gif" border="0" alt="Hapus" style="cursor:hand" OnClick="return setSelected(this)"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Re-Send">
                        <HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnIsSend" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.IsSend")%>' />
                            <asp:LinkButton ID="lbtnResend" CommandName="ReSend" Text="ReSend" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
								<img border="0" src="../images/reload.gif" alt="ReSend" style="cursor:hand"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
            <br /><br />
        </div>

        <table id="tblFooter" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr valign="top">
                <td><br /><br /></td>
            </tr>
        </table>
    </form>
</body>
</html>
