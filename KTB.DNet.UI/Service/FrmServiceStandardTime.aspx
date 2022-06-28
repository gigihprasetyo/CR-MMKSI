<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmServiceStandardTime.aspx.vb" Inherits=".FrmServiceStandardTime" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmServiceStandardTime</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = temp[0];
        
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

        function ShowPPServiceStdTime() {
            showPopUp('../General/../PopUp/PopUpServiceStandardTime.aspx', '', 300, 450, StdTimeSelection);
        }

        function StdTimeSelection(selectedStdTime) {
            //var temp = selectedStdTime.split(';');
            //var txttt = document.getElementById("Textbox1");
            //txttt.value = selectedStdTime; //temp[0];
            alert(selectedStdTime)
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 30px;
        }
        .auto-style2 {
            width: 2px;
            height: 30px;
        }
        .auto-style3 {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage"><asp:Label ID="Label1" runat="server" Text="Stall - Service Standard Time"></asp:Label></td>
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

        <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
             <tr>
                <td class="titleField" style="width: 24%">Kode Dealer</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 75%">
                               <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server" ToolTip="Dealer Search"></asp:TextBox>
                                <asp:Label ID="lblPopupDealer" onclick="ShowPPDealerSelection();" runat="server" Style="z-index: 0">
					                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </td>
            </tr>

            <tr>
                <td class="titleField">Assist Service Type</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddlAssistServiceType" runat="server" OnSelectedIndexChanged="ddlAssistServiceType_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="auto-style1">Model Kendaraan</td>
                <td valign="top" class="auto-style2">:</td>
                <td valign="top" class="auto-style3">
                    <asp:DropDownList ID="ddlModelKendaraan" runat="server" OnSelectedIndexChanged="ddlModelKendaraan_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="titleField">Kode Tipe Kendaraan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                   <asp:DropDownList ID="ddlVehicleType" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <asp:HiddenField ID="HiddenField2" runat="server"/>
            <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
            <asp:HiddenField ID="HiddenField3" runat="server" />
            <tr>
                <td class="titleField">Jenis Kegiatan</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddlJenisKegiatan" runat="server" OnSelectedIndexChanged="ddlJenisKegiatan_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="titleField">Jenis Service</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                      
                    <asp:DropDownList ID="ddlJenisService" runat="server" OnSelectedIndexChanged="ddlJenisService_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="titleField">Standard Waktu Dealer (jam)</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtStandardDealer" runat="server" Width="134px" onkeydown="this.value=this.value.replace(/[^\d,]/g,'')" ToolTip="Hanya bisa angka dan koma. Ex : 1,20"></asp:TextBox>                
                </td>
            </tr>

            <tr>
                <td class="titleField">Standard Waktu System (jam)</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txtStandardSystem" runat="server" Width="134px" ToolTip="Auto Generate"></asp:TextBox>
                </td>
            </tr>

            <tr>
                            <td class="titleField" style="width: 24%"></td>
                            <td style="width: 1%"></td>
                            <td style="width: 75%">
                                <asp:Button ID="btnSave" runat="server" Width="88px" Text="Simpan" OnClick="btnSave_Click"></asp:Button>&nbsp;
						        <asp:Button ID="btnCari" runat="server" Width="88px" Text="Cari" OnClick="btnCari_Click"></asp:Button>
                                <asp:Button ID="btnBatal" runat="server" Width="88px" Text="Batal" OnClick="btnBatal_Click"></asp:Button>
                            </td>
            </tr>
        </table>
        
        <table id="Table3" cellspacing="3" cellpadding="3" width="100%" border="0">
        <tr>
              
                <td>
                    <asp:DataGrid ID="dtgServiceStandardTime" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
							BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
											AllowPaging="True">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
                                
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbNo" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<asp:CheckBox id="cbAll" runat="server" onclick="CheckAll('cbItem',this.checked)"></asp:CheckBox>
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
														<asp:CheckBox id="cbInitial" style="display:none" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Assist Service Type">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbAssistServiceType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssistServiceTypeCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Model Kendaraan">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbModelKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.IndDescription")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Model Kendaraan" Visible="false">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbModelKendaraan2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe Kendaraan">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbTipeKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Kegiatan">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisKegiatan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceTypeID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Kegiatan" Visible="false">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisKegiatan2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceTypeID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Service">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisService" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KindCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Jenis Service" Visible="false">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisService2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KindCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Standard Waktu Dealer (Jam)">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbStandardDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerStandardTime", "{0:0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Standard Waktu System (Jam)">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbStandardSystem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SystemStandardTime", "{0:0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                               
                                <asp:TemplateColumn HeaderText="">
													<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkDetail" runat="server" CommandName="lnkDetail">
															<img src="../images/edit.gif" border="0" >
														</asp:LinkButton>
                                                        <asp:LinkButton id="lnkProcess" runat="server" CommandName="lnkProcess" Visible="false">
															<img src="../images/reload.gif" border="0" >
														</asp:LinkButton>

                                                       
													</ItemTemplate>
												</asp:TemplateColumn>
                            </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </td>
                </tr>
           </table>
        <table>
            <tr>
                            <td>
                                 <asp:Button ID="btnDownload" runat="server" Text=" Download " Width="80px" CausesValidation="False" OnClick="btnDownload_Click" Enabled ="false"></asp:Button>
                           
                                 <asp:Button ID="btnCalculateAll" runat="server" Text=" Calculate " Width="80px" CausesValidation="False" AutoPostBack="False" OnClick="btnCalculateAll_Click1"></asp:Button></td>

            </tr>
            <tr>
                <td>
                    <asp:panel id="pnlCalculate" runat="server" Width="400px" Visible="false">
                        <table id="Table5" cellspacing="3" cellpadding="3" width="100%" border="0">
                            <tr>
                                 <td>Jenis Kegiatan</td>
                                 <td>:</td>
                                 <td><asp:DropDownList ID="ddlJenisKegiatan2" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                 <td>Period From</td>
                                 <td>:</td>
                                 <td>
                                 <cc1:inticalendar id="ICPeriodFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCalculate1" runat="server" Text=" Proses " Width="80px" CausesValidation="False" AutoPostBack="true" OnClick="btnCalculate1_Click1"></asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" Text=" Batal " Width="80px" CausesValidation="False" AutoPostBack="true" OnClick="btnCancel_Click"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:panel>
                    <asp:TextBox ID="TextBox1" runat="server" Width="134px" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        

        
        <table id="Table4" cellspacing="3" cellpadding="3" width="100%" border="0">
            
        </table>

    </form>
</body>
</html>
