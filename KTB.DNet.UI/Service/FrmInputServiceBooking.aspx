<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputServiceBooking.aspx.vb" Inherits=".FrmInputServiceBooking" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmInputServiceBooking</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/scheduler_traditional.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/css/jquery-ui.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/select2.min.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/jquery-clockpicker.min.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery.1.11.0.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-ui.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/select2.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-clockpicker.min.js"></script>

    <script type="text/javascript">
        function ShowPPPlanSelection(id, modelid, vechiletypeid, ccid) {
            showPopUp('../PopUp/PopUpPlanSelection.aspx?id=' + id + '&modelid=' + modelid + '&vechiletypeid=' + vechiletypeid + '&ccid=' + ccid, '', 488, 1059, PlanSelection);
        }

        function ShowPPCostEstimation(dealercode, vechiletypecode, chassisnumber) {
            showPopUp('../PopUp/PopUpCostEstimation.aspx?dealercode=' + dealercode + '&vechiletypecode=' + vechiletypecode+ '&chassisnumber=' + chassisnumber   , '', 488, 1059);
        }

        function ShowCancelServiceBooking(id, mode) {
            showPopUp('../PopUp/PopUpCancelServiceBooking.aspx?id=' + id + '&mode=' + mode, '', 600, 600, AfterSave);
        }

        function AfterSave(msg) {
            alert(msg);
            var btn = document.getElementById("btnCancel")
            hdConfirm.value = "0";
            hdCancel.value = "0";
            btn.click();
        }

        function PlanSelection(selectedPlan) {
            var data = selectedPlan.split(";");
            var txtIncomingPlan = document.getElementById("txtIncomingPlan");
            var txtStallName = document.getElementById("txtStallName");
            var hdStandardTime = document.getElementById("hdStandardTime");
            var hdJenisService = document.getElementById("hdJenisService");
            txtIncomingPlan.value = data[0];
            txtStallName.value = data[1];
            hdStandardTime.value = data[2];
            hdJenisService.value = data[3];

            __doPostBack('', '');
        }

        function ShowConfirm(msg, id) {
            var btn = document.getElementById(id);
            var hdConfirm = document.getElementById("hdConfirm");
            if (confirm(msg)) {
                hdConfirm.value = "0";
                btn.click();
            }
        }

        function GetCurrentInputIndex(GridName) {
            var dtgDamageCode = document.getElementById(GridName);
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dtgDamageCode.rows.length; index++) {
                inputs = dtgDamageCode.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }


        $(function () {
            $('.timepicker').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true
            });

            $("#ddlModelKendaraan").select2();
            $("#ddlVehicleTypeCode").select2();
            $("#ddlJnsKegiatan").select2();
            $("#ddlJnsService").select2();
            $("#ddlPickupType").select2();
            $("#ddlRespon").select2();
            $("#ddlServiceAdvisor").select2();
        });
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage"><asp:Label ID="lblHeader" runat="server" Text="Stall - Input Service Booking"></asp:Label></td>
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
        <table id="Table2" cellspacing="3" cellpadding="3" border="0">
            <tr>
                <td colspan="4" class="titleField">
                    <asp:Label ID="lblStatus" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 130px">Nomor Reservasi</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:Label ID="lblNoReservasi" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdID" runat="server" />
                    <asp:HiddenField ID="hdCCID" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="titleField">Nama Konsumen</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtNamaKonsumen" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Nomor Telp. Konsumen</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtNoTelp" runat="server"  onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Nomor Plat Mobil</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtPlatNomor" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$ ')"></asp:TextBox>
                </td>
            </tr>
            <tr>
				<td class="titleField">Jenis Kendaraan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:RadioButton ID="rbMitsubishi" runat="server" Text="Mitsubishi" CssClass="setMargin" GroupName="Kendaraan" OnCheckedChanged="rbMitsubishi_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                    <asp:RadioButton ID="rbNonMitsubishi" runat="server" Text="Non Mitsubishi" CssClass="setMargin" GroupName="Kendaraan" OnCheckedChanged="rbNonMitsubishi_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                </td>
			</tr>
            <tr id="trNonMitsu1" runat="server" visible="false">
				<td class="titleField">Tipe Kendaraan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtTipeKendaraan" runat="server"></asp:TextBox>
                </td>
			</tr>
            <tr>
                <td class="titleField">Nomor Rangka</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoChassis" 
                        onblur="omitSomeCharacter('txtNoChassis','<>?*%$')" runat="server" 
                        ToolTip="Nomor Chassis Search" style="position:relative; top:-5px;"></asp:TextBox>
                    <asp:ImageButton ID="btnGetInfoChassis" runat="server" ImageUrl="~/images/reload.gif" OnClick="btnGetInfoChassis_Click"></asp:ImageButton>
                </td>
            </tr>
            <tr id="trMitsu1" runat="server" visible="false">
                <td class="titleField">Model Kendaraan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                   <asp:DropDownList ID="ddlModelKendaraan" runat="server" Width="200"
                       OnSelectedIndexChanged="ddlModelKendaraan_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr id="trMitsu2" runat="server" visible="false">
                <td class="titleField">Kode Tipe Kendaraan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                   <%--<asp:TextBox ID="txtVehicleCode" runat="server"></asp:TextBox>--%>
                   <asp:DropDownList ID="ddlVehicleTypeCode" runat="server" Width="250" OnSelectedIndexChanged="ddlVehicleTypeCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField">Odometer</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtOdoMeter" runat="server"  onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="titleField">jnskegiatan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtjeniskegiatan" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="titleField">jnsservice</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtjenisservice" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Service Advisor</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddlServiceAdvisor" runat="server"  OnSelectedIndexChanged="ddlServiceAdvisor_SelectedIndexChanged" AutoPostBack="true" Width="180"></asp:DropDownList>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="titleField">Jenis Kegiatan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddlJnsKegiatan" runat="server"  OnSelectedIndexChanged="ddlJnsKegiatan_SelectedIndexChanged" AutoPostBack="true" Width="180"></asp:DropDownList>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="titleField">Jenis Service</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddlJnsService" runat="server" Width="250"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="titleField">
                    <asp:datagrid id="dgSBNew" runat="server" Width="250px" CellPadding="3" BorderWidth="1px"
						BorderColor="#CDCDCD" BackColor="White" AutoGenerateColumns="False" ShowFooter="True" OnItemCommand="dgSBNew_ItemCommand" OnItemDataBound="dgSBNew_ItemDataBound">
						<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
						<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
						<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
						<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
						<Columns>
							<%--<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>--%>
							<asp:TemplateColumn HeaderText="No">
								<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdID" runat="server" />
                                </ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Jenis Kegiatan">
								<HeaderStyle Width="20%"  CssClass="titleTableService"></HeaderStyle>
								<ItemTemplate>
									<asp:Label ID="lblJenisKegiatan" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.ServiceTypeID")  %>' >
									</asp:Label>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlJKEdt" runat="server" Width="180" OnSelectedIndexChanged="ddlJKEdt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </EditItemTemplate>
								<FooterTemplate>
                                    <asp:DropDownList ID="ddlJK" runat="server" Width="180" OnSelectedIndexChanged="ddlJK_SelectedIndexChanged" AutoPostBack="true" OnPreRender="ddlJK_PreRender"></asp:DropDownList>
								</FooterTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Jenis Service">
								<HeaderStyle Width="40%"  CssClass="titleTableService"></HeaderStyle>
								<ItemTemplate>
									<asp:Label ID="lblJenisService" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.KindCode") %>'>
									</asp:Label>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlJSEdit" runat="server" Width="180"></asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlJS" runat="server" Width="180" OnPreRender="ddlJS_PreRender"></asp:DropDownList>
								</FooterTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<HeaderStyle Width="10%"  CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<FooterStyle HorizontalAlign="Center"></FooterStyle>
								<ItemTemplate>
                                    <div style="width:40px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" width="20px"
                                        CommandName="edit" CausesValidation="False" Visible="false">
								        <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                    <asp:LinkButton id="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
										CommandName="Delete">
										<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </div>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <div style="width:40px">
                                        <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" CommandName="cancel" Text="Batal" runat="server">
                                            <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                        </div>
                                </EditItemTemplate>
								<FooterTemplate>
									<asp:LinkButton id="lnkbtnAdd" runat="server" Width="20px" Text="Tambah" CausesValidation="False"
										CommandName="Add">
										<img src="../images/add.gif" border="0" alt="Tambah">
									</asp:LinkButton>
								</FooterTemplate>
							</asp:TemplateColumn>
						</Columns>
						<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
					</asp:datagrid>                   
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td valign="top">
                        <asp:Button ID="btnCost" runat="server" Text="Estimasi Biaya" OnClick="btnCost_Click" />
                    </td>
                </tr>
                <tr id="trCost" runat="server" visible="false">
                    <td colspan="4" class="titleField">
                        <asp:datagrid id="dgCost" runat="server" Width="100%" CellPadding="3" BorderWidth="1px"
	                        BorderColor="#CDCDCD" BackColor="White" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="dgCost_ItemDataBound">
	                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
	                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
	                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
	                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
	                        <Columns>

                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Kegiatan">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJnsKegiatan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JenisKegiatan")%>'>
                                    </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Service">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
				                        <asp:Label ID="lblJnsService" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.JenisService")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
				                        <FooterTemplate>
                                            <asp:Label ID="lblGrand" runat="server" Text="Grand Total">
                                            </asp:Label>
			                        </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Subtotal">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubtotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JasaService")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
				                        <FooterTemplate>
                                            <asp:Label ID="lblGrandTotal" runat="server">
                                            </asp:Label>
			                        </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid>
                    </td>
                </tr>
                <tr id="trCost2" runat="server" visible="false">
                    <td colspan="4" class="titleField">
                    <asp:datagrid id="dgSparePart" runat="server" Width="100%" CellPadding="3" BorderWidth="1px"
	                    BorderColor="#CDCDCD" BackColor="White" AutoGenerateColumns="False" ShowFooter="True" OnItemCommand="dgSparePart_ItemCommand" OnItemDataBound="dgSparePart_ItemDataBound">
	                    <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
	                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
	                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
	                    <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
	                    <Columns>

                            <asp:TemplateColumn HeaderText="No">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Part">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaPart" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartName")%>'>
                                </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Harga Satuan">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
								    <asp:Label ID="lblHargaSatuan" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.RetalPrice")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Jumlah">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblJumlah" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartQuantity")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Diskon">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDiskon" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DiscountAmount")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
								    <FooterTemplate>
                                        <asp:Label ID="lblGrand" runat="server" Text="Grand Total">
                                        </asp:Label>
								</FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Subtotal">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubtotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Subtotal")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
								    <FooterTemplate>
                                        <asp:Label ID="lblGrandTotal" runat="server">
                                        </asp:Label>
								</FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                       <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:datagrid>
                </td>
            </tr>

            <tr>
                <td class="titleField">Rencana Pengerjaan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtIncomingPlan" runat="server" Width="170" OnTextChanged="txtIncomingPlan_TextChanged"></asp:TextBox>
                    <asp:ImageButton ID="btnPopupPlan" runat="server" ImageUrl="../images/popup.gif" AlternateText="Klik popup" OnClick="btnPopupPlan_Click" />
                </td>
            </tr>
            <tr id="trRespon" runat="server">
                <td class="titleField">Dealer Respon</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddlRespon" runat="server" Width="250"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField">Stall</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox id="txtStallName" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hdStandardTime" runat="server" />
                    <asp:HiddenField ID="hdJenisService" runat="server" />
                    <asp:HiddenField ID="hdBookingTime" runat="server" />
                    <asp:HiddenField ID="hdChangeVechile" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Rencana Kedatangan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                   <asp:DropDownList ID="ddlPickupType" runat="server" Width="150"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Waktu Kedatangan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txtIncomingStart" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Waktu Pengambilan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txtIncomingEnd" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">Catatan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtCatatan" runat="server" TextMode="MultiLine" Height="60" Width="250"></asp:TextBox>
                </td>
            </tr>
            
            
            <tr>
                <td colspan="2"></td>
                <td valign="top">
                    <input id="hdConfirm" type="hidden" value="-1" runat="server" />
                    <input id="hdCancel" type="hidden" value="-1" runat="server" />
                    <asp:TextBox ID="txtJnsKegiatan" runat="server" Width="10" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtJnsService" runat="server" Width="10" Visible="false"></asp:TextBox>
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Batal Booking" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnKonfirmasi" runat="server" Text="Konfirmasi" OnClick="btnKonfirmasi_Click" />
                    <asp:Button ID="btnBaru" runat="server" Text="Baru" OnClick="btnBaru_Click" />
                </td>
            </tr>
            
        </table>
        
        <table cellspacing="3" cellpadding="3" width="56%" border="0" style="left: 480px; top: 30px; position: absolute;">
            <tr>
                <td class="titleField" style="width: 200px">Nomor Rangka</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:Label id="lblNoRangka" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 200px">Model / Tipe / Warna</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:Label id="lblModelInfo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 200px">Program FS yang di dapat</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:Label id="lblProgFS" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="titleField">History</td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="margin-bottom:5px"><label><b><i>Service Data Campaign</i></b></label></div>
                    <div>
                        <asp:DataGrid ID="dgSC" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                            CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="5" 
                            OnPageIndexChanged="dgSC_PageIndexChanged"
                            OnSortCommand="dgSC_SortCommand"
                            OnItemDataBound="dgSC_ItemDataBound">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Dealer Pelaksana">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" 
                                            ToolTip='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.Dealer.DealerCode"), DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1"))%>' 
                                            Text='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.Dealer.DealerCode"), DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1"))%>'>
                                    </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal Pengerjaan">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Proses">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglPro" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Field Fix Reg No">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecallRegNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.RecallRegNo")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Deskripsi">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="margin-bottom:5px"><label><b><i>Free Service Data</i></b></label></div>
                    <div>
                        <asp:DataGrid ID="dgFS" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                            CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="5" 
                            OnPageIndexChanged="dgFS_PageIndexChanged"
                            OnSortCommand="dgFS_SortCommand"
                            OnItemDataBound="dgFS_ItemDataBound">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="FSKind.KindCode" HeaderText="Kind">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKind" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.KindDescription")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal FS">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Proses">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglPro" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" 
                                            ToolTip ='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.Dealer.DealerCode"), DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1"))%>'
                                            Text='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.Dealer.DealerCode"), DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1"))%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="margin-bottom:5px"><label><b><i>Periodical Maintenance</i></b></label></div>
                    <div>
                        <asp:DataGrid ID="dgPM" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                            CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="5" 
                            OnPageIndexChanged="dgPM_PageIndexChanged"
                            OnSortCommand="dgPM_SortCommand"
                            OnItemDataBound="dgPM_ItemDataBound">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="StandKM" HeaderText="Kind">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMKind.KindDescription")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.PMKind.KindCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="StandKM" SortExpression="StandKM" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal PM">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglPM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ReleaseDate" HeaderText="Tanggal Proses">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglRilis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReleaseDate","{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" 
                                            ToolTip='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.Dealer.DealerCode"), DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1"))%>' 
                                            Text='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.Dealer.DealerCode"), DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PMStatus" HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            


        </table>
    

    </form>
</body>
</html>
