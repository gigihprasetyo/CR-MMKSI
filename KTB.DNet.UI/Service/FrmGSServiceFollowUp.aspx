<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmGSServiceFollowUp.aspx.vb" Inherits=".FrmGSServiceFollowUp" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Satisfaction - Service Reminder Follow Up </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/scheduler_traditional.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
   
    <script type="text/javascript">
        function ShowPopUpEdit(id, mode) {
            //alert(chassisnumber + ' ' + mode);
            showPopUp('../PopUp/PopUpEditNamePhone.aspx?id=' + id + '&mode=' + mode, '', 400, 400, AfterSave);
        }
        function AfterSave(msg) {
            alert(msg);
            var btn = document.getElementById("btnHidden")
            btn.click();
        }
        var rbtnResponses = document.getElementById('rbtnResponses')
        rbtnResponses.addEventListener('change', function () {
            alert("berubah");
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 200px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <table id="TittleTable" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CUSTOMER SATISFACTION -&nbsp; Service Reminder Follow Up</td>
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
        <table id="Table1" cellspacing="0" cellpadding="0" border="0">
			<tr>
                <td valign="top" align="left" width="50%">
                    <table id="Table_left" cellspacing="1" cellpadding="2" border="0">
                        <tr>
                            <td class="auto-style1" colspan="2">Kode dealer</td>
                            <td colspan="2">:
                                <asp:Label ID="lblDealerCode" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Nama Pemilik Kendaraan / Phone</td>
                            <td colspan="2">:
                                <asp:Label ID="lblConsumenName" runat="server" Text="Label"></asp:Label>
                                <asp:ImageButton ID="btnEditPemilik" runat="server" ImageUrl="~/images/edit.gif" OnClick="btnEditPemilik_Click"></asp:ImageButton>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Nama Konsumen / Phone</td>
                            <td colspan="2">:
                                <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label>
                               <asp:ImageButton ID="btnEditKonsumen" runat="server" ImageUrl="~/images/edit.gif" OnClick="btnEditKonsumen_Click"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Tanggal Batas Reminder</td>
                            <td colspan="2">:
                                <asp:Label ID="lblReminderDate" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="auto-style1" colspan="2">Status</td>
                            <td colspan="2">:
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Tanggal Booking</td>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chBookingDate" runat="server" AutoPostBack="True" Checked="false" Visible="false"/>
                                        </td>
                                        <td>
                                            <cc1:IntiCalendar ID="icBookingDate" runat="server" TextBoxWidth="70" CanPostBack="False" Friday="True" Monday="True" Saturday="True" ScriptOnFocusOut="" Sunday="True" TargetForm="" TargetTemporaryFocus="" TargetTextBox="" Thursday="True" Tuesday="True" Wednesday="True"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Nomor WO</td>
                            <td colspan="2">: <asp:TextBox ID="txtWONumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" style="vertical-align:top" colspan="2">Respon</td>
                            <td colspan="2">:   <asp:RadioButtonList ID="rbtnResponses" runat="server" AutoPostBack="True">
                                                    <%--<asp:ListItem Value="1" Selected="True">Follow Up Konsumen</asp:ListItem>
                                                    <asp:ListItem Value="2">Konsumen belum ada respon</asp:ListItem>
                                                    <asp:ListItem Value="3">Konsumen telah Booking service</asp:ListItem>
                                                    <asp:ListItem Value="4">Konsumen jadwal ulang service</asp:ListItem>
                                                    <asp:ListItem Value="5">Konsumen telah melakukan service di dealer lain</asp:ListItem>
                                                    <asp:ListItem Value="6">Konsumen telah datang melakukan service</asp:ListItem>
                                                    <asp:ListItem Value="7">Respon lain</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                                <asp:TextBox ID="txtResponse" TabIndex="0" runat="server" Width="260px" TextMode="MultiLine" Height="60px" MaxLength="100" onkeyDown="checkTextAreaMaxLength(this,event,'100');"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2"> </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" onClientClick="return confirm('Simpan Follow Up?');" Text="Simpan" CausesValidation="False"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Batal" />  
                                <asp:Button ID="btnBack" runat="server" Text="Kembali" />
                                <asp:Button ID="btnHidden" runat="server" Text="" onClick="btnHidden_Click" ClientIDMode="Static" Style="display:none"/>
                            </td>
                        </tr>                  
                            
                    </table>
                </td>
                <td valign="bottom" align="center" width="10%">
                    <asp:Button ID="btnSB" runat="server" Text="Service Booking" />
                </td>
                <td valign="bottom" align="center" width="10%">
                    <asp:Button ID="btnCC" Text="Case Management" runat="server" />
                </td>
                <td valign="top" align="left" width="50%">
                    <table id="Table_right" cellspacing="1" cellpadding="2" width="100%" border="0">
                       <tr>
                            <td class="auto-style1" colspan="2">Nomor Rangka</td>
                            <td colspan="2">:
                                <asp:Label ID="lblChassisNo" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Nama / Phone (MMKSI System)</td>
                            <td colspan="2">:
                                <asp:Label ID="lblEngineNo" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Tipe Kendaraan</td>
                            <td colspan="2">:
                                <asp:Label ID="lblVehicleType" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Odometer</td>
                            <td colspan="2">:
                                <asp:Label ID="lblOdometer" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Tipe Servis</td>
                            <td colspan="2">
:
                                <asp:Label ID="lblRemark2" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Remark</td>
                            <td colspan="2">:
                                <asp:Label ID="lblRemark" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:DataGrid ID="dgSvcReminderFU" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
            AllowPaging="True" AllowCustomPaging="True" PageSize="25">
            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
            <ItemStyle BackColor="White"></ItemStyle>
            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" ReadOnly="True" HeaderText="ID">
                    <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="No">
                    <HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Status">
                    <HeaderStyle CssClass="titleTableMrk" Width="5%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Respon">
                    <HeaderStyle CssClass="titleTableMrk" Width="10%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblResponse" runat="server"></asp:Label>
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
                        <HeaderStyle CssClass="titleTableMrk" Width="12%"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbServiceBooking" runat="server" CausesValidation="False" CommandName="View">
								</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Tanggal Booking Service">
                    <HeaderStyle CssClass="titleTableMrk" Width="7%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblBookingSvcDate" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Tanggal Update">
                    <HeaderStyle CssClass="titleTableMrk" Width="7%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblUpdateDate" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="User Update">
                    <HeaderStyle CssClass="titleTableMrk" Width="7%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblUserUpdate" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>                
                <asp:TemplateColumn HeaderText="Detail">
                    <HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Ubah" Width="20px">
								<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
    </form>
</body>
</html>
