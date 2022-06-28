<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputFleetCustomer.aspx.vb" Inherits=".FrmInputFleetCustomer" smartNavigation="False"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<HTML>
	<HEAD>

    <title>Input Fleet Customer</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
	<script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        
        function CustomerGroupSelection(selectedCustomerGroup) {
            var txtGroupName = document.getElementById("txtGroupName");
            txtGroupName.value = selectedCustomerGroup;
            //var lblGroupNameFill = document.getElementById("lblGroupNameSelect");
            //lblGroupNameFill.innerHTML = selectedCustomerGroup;
            var hdnGroupName = document.getElementById("hdnGroupName");
            hdnGroupName.value = selectedCustomerGroup;
        }

        function ShowCustomerGroupList() {
            showPopUp('../PopUp/PopUpCustomerGroupList.aspx?Tyjiuy678=code', '', 500, 760, CustomerGroupSelection);
        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            //alert(selectedDealer);
            var txtDealerSelection = document.getElementById("txtDealerCodeList");
            txtDealerSelection.value = selectedDealer;
            var btnDealerHelper = document.getElementById('btnDealerHelper');
            if (btnDealerHelper) btnDealerHelper.click();
           
        }
        
    </script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />

    <style type="text/css">
        .auto-style3 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 30px;
        }
        .auto-style4 {
            height: 30px;
        }
        tbody{
            margin-top: -15px;
        }
    </style>

</head>
<body ms_positioning="GridLayout">
   
    <form id="form1" runat="server">
        <table cellspacing="0" id="Table2" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 16px">&nbsp;
					<asp:Label ID="lblTitle" runat="server">FLEET MANAGEMENT - Input Fleet Customer</asp:Label>
                    <asp:Label ID="SessionStatus" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="hdnFleetCustomerID" runat="server" />
                </td>
            </tr>
            <tr style="height: 1px;">
                <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;">

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlFleetCustomer" runat="server" Width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td colspan="6">
                                <table border="0" cellSpacing="1" cellPadding="2" width="100%">
                                    <tr>
                                        <td class="titleField">Kategori</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True"></asp:dropdownlist>
                                            <asp:label id="lblCategory" runat="server" Visible="false">Perusahaan</asp:label>                                
                                        </td>
                                        <td class="titleField">Klasifikasi</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:dropdownlist id="ddlClassification" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:label id="lblClassification" runat="server" Visible="False"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Tipe</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:dropdownlist id="ddlTipePerusahaan" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:label id="lblTipePerusahaan" runat="server" Visible="False"></asp:label>
                                        </td>
                                        <td class="titleField">Profil Bisnis</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:dropdownlist id="ddlBusinessSector" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:label id="lblBusinessSector" runat="server" Visible="False"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Tipe Identitas</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:dropdownlist id="ddlIdentityType" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:label id="lblIdentityType" runat="server" Visible="False"></asp:label>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">No. Identitas</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblIdentityNumber" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtIdentityNumber','<>?*%$;');" id="txtIdentityNumber" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" Width="180px" MaxLength="30"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <%--<tr>
                                        <td class="titleField">No NPWP</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblNoNpwp" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtNoNPWP','<>?*%$;');" id="txtNoNPWP" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" Width="180px" MaxLength="30"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>--%>
                                    <tr>
                                        <td class="titleField">Nama Grup</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblGroupName" runat="server" Visible="False"></asp:label>
                                            <asp:HiddenField runat="server" ID="hdnGroupName" />
                                            <asp:textbox onblur="omitSomeCharacter('txtGroupName','<>?*%$;');" id="txtGroupName" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" Width="150px" MaxLength="10"></asp:textbox><asp:label id="lbtnCustomerGroup" onclick="ShowCustomerGroupList();" Runat="server">
								                <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                            </asp:label>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kode</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblCode" runat="server" Visible="False"></asp:label>
                                            <%--<asp:label id="lblTypePerusahaanSelect" runat="server" text="[Tipe]"></asp:label>-<asp:label id="lblCustBusinessSelect" runat="server" text="[Profil]"></asp:label>-<asp:label id="lblGroupNameSelect" runat="server"></asp:label>---%>
                                            <asp:textbox onblur="NumOnlyBlurWithOnGridTxt(this,'');" id="txtCode" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" onkeyup="autofocus(this,'txtName');" runat="server" Width="60px" MaxLength="5"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Nama</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblName" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtName','<>?*%$;');" id="txtName" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" onkeyup="autofocus(this,'txtGedung');" Width="180px" MaxLength="50"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3">Gedung</td>
                                        <td width="1%" class="auto-style4">:</td>
                                        <td class="auto-style4">
                                            <asp:label id="lblGedung" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtGedung','<>?*%$;');" id="txtGedung" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" onkeyup="autofocus(this,'txtAlamat');" Width="180px" MaxLength="50"></asp:textbox>
                                        </td>
                                        <td colspan="4" class="auto-style4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Alamat</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblAlamat" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtAlamat','<>?*%$;');" id="txtAlamat" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" Width="200px" MaxLength="150"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Provinsi</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:dropdownlist id="ddlPropinsi" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:label id="lblPropinsi" runat="server" Visible="False"></asp:label>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kota/Kabupaten</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblKota" runat="server" Visible="False"></asp:label>
                                            <asp:dropdownlist id="ddlPreArea" runat="server">
							                    <asp:ListItem Value="blank" Selected="True">Silahkan Pilih</asp:ListItem>
							                    <asp:ListItem Value="KAB">KAB</asp:ListItem>
							                    <asp:ListItem Value="KODYA">KODYA</asp:ListItem>
							                    <asp:ListItem Value="KABUPATEN">KABUPATEN</asp:ListItem>
							                    <asp:ListItem Value="KOTA MADYA">KOTAMADYA</asp:ListItem>
							                    <asp:ListItem Value="KOTA">KOTA</asp:ListItem>
						                    </asp:dropdownlist>
                                            <asp:dropdownlist id="ddlKota" runat="server"></asp:dropdownlist>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kecamatan</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblKecamatan" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtKecamatan','<>?*%$;');" id="txtKecamatan" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" onkeyup="autofocus(this,'txtKelurahan');" runat="server" Width="180px" MaxLength="75"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kelurahan</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblkelurahan" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtKelurahan','<>?*%$;');" id="txtKelurahan" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" onkeyup="autofocus(this,'txtKodepos');" runat="server" Width="180px" MaxLength="75"></asp:textbox>
                                        </td>
                                        <td class="titleField">Kode Pos</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblKodepos" runat="server" Visible="False"></asp:label>
                                            <asp:textbox  id="txtKodepos" onblur="NumOnlyBlurWithOnGridTxt(this,'0');" onkeypress="return numericOnlyUniv(event);" onkeyup="autofocus(this,'txtEmail');" runat="server" Width="88px" MaxLength="5"></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Email</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblEmail" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtEmail','<>?*%$;');" id="txtEmail" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;~!');" onkeyup="autofocus(this,'txtNotlp');" runat="server" Width="180px" MaxLength="100"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                     <tr>
                                        <td class="titleField">No Telepon</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:label id="lblNotlp" runat="server" Visible="False"></asp:label>
                                            <asp:textbox onblur="omitSomeCharacter('txtNotlp','<>?*%$;');" id="txtNotlp" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" onkeyup="autofocus(this,'txtNoNPWP');" runat="server" Width="180px" MaxLength="15"></asp:textbox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Lampiran</td>
                                        <td width="1%">:</td>
                                        <td>
                                            <INPUT id="inFileLocation" onkeydown="return false;" style="WIDTH: 240px" type="file" name="File1"
										            runat="server"><br />
                                           <asp:label id="lblAttachment" runat="server"></asp:label>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
				            <td background="../images/bg_hor.gif" height="1" colspan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                            <td></td>
			            </tr>
                        <tr>
                            <td colspan="6">
                                <div id="divHiddenMain" style="width: 100%">
                                   
                                    <asp:DataGrid ID="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
                                        CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                        AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                        DataKeyField="ID" ShowFooter="True">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="false" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundColumn>
                               
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                            </asp:TemplateColumn>

                                             <asp:TemplateColumn HeaderText="ID" Visible="false">
                                                <HeaderStyle ForeColor="White" Width="25%" HorizontalAlign="Left" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn> 

                                            <asp:TemplateColumn HeaderText="Nama">
                                                <HeaderStyle ForeColor="White" Width="20%" HorizontalAlign="Left" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name")%>'>
                                                </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" ID="lblNameE"></asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblNameF" ></asp:Label>
                                                    <div>
                                                    <asp:TextBox runat="server" ID="txtName" Width="95%"
                                                        CssClass="textLeft" onkeyup="AutoThousandSeparator(this,event);"></asp:TextBox>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Posisi">
                                                <HeaderStyle ForeColor="White" Width="20%" HorizontalAlign="Left" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPosition" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Position")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" ID="lblPositionE">&nbsp;</asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblPositionF" ></asp:Label>
                                                    <div>
                                                    <asp:TextBox runat="server" ID="txtPosition" Width="95%"
                                                        CssClass="textLeft" onkeyup="AutoThousandSeparator(this,event);"></asp:TextBox>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Phone">
                                                <HeaderStyle ForeColor="White" Width="20%" HorizontalAlign="Left" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPhone" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PhoneNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" ID="lblPhoneE">&nbsp;</asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblPhoneF"></asp:Label>
                                                    <div>
                                                    <asp:TextBox runat="server" ID="txtPhone" Width="95%" MaxLength="20"
                                                        CssClass="textLeft" onkeyup="AutoThousandSeparator(this,event);"></asp:TextBox>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Handphone">
                                                <HeaderStyle ForeColor="White" Width="20%" HorizontalAlign="Left" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHandphone" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Handphone")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" ID="lblHandphoneE">&nbsp;</asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblHandphoneF"></asp:Label>
                                                    <div>
                                                    <asp:TextBox runat="server" ID="txtHandphone" Width="95%" MaxLength="20"
                                                        CssClass="textLeft" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Email">
                                                <HeaderStyle ForeColor="White" Width="20%" HorizontalAlign="Left" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Email")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" ID="lblEmailE">&nbsp;</asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblEmailF"></asp:Label>
                                                    <div>
                                                    <asp:TextBox runat="server" ID="txtEmail" Width="95%" MaxLength="100"
                                                        CssClass="textLeft" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"></asp:TextBox>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn>
                                                <HeaderStyle ForeColor="White" Width="5%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="delete" Visible="true">
											            <img src="../images/trash.gif" border="0" alt="Hapus">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="add">
											            <img src="../images/add.gif" border="0" alt="Tambah">
                                                    </asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                </div>
                            </td>
                            <td></td>
                        </tr>
                        </table>
                    </asp:panel>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Panel ID="pnlKepemilikanKendaraan" runat="server" Width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td colspan="6" class="titlePanel">
                                    <b>Kepemilikan Kendaraan</b>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
				                <td background="../images/bg_hor.gif" height="1" colspan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                                <td></td>
			                </tr>
                            <tr>
                                <td colspan="6">
                                    <div id="divHiddenKepemilikanKendaraan" style="overflow: auto; width: 100%;height:200px">

                                        <asp:Datagrid ID="dtgKepemilikanKendaraan" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
                                            CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                            AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                            DataKeyField="ID" ShowFooter="True">
                                    
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>

                                            </asp:Datagrid>

                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td colspan="7">                    
                    <asp:Panel ID="pnlOtorisasiDealer" runat="server" Width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td colspan="6" class="titlePanel">
                                    <b>Otorisasi Dealer</b>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
				                <td background="../images/bg_hor.gif" height="1" colspan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                                <td></td>
			                </tr>
                            <tr>
                                <td colspan="6" class="titlePanel">
                                    <div style="vertical-align:central">
                                        <asp:Label ID="lblPencarianDealer" runat="server" Text="Pencarian Dealer :"></asp:Label>
                                        <asp:label id="lblPopUpDealer" onclick="ShowPPDealerSelection();" Runat="server"> <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"> </asp:label>
                                        <asp:HiddenField  ID="txtDealerCodeList" runat="server"></asp:HiddenField>
                                    </div>
                                
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div id="divHiddenDealer" style="width: 100%">
                                   
                                        <asp:Datagrid ID="dtgDealerSelection" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
                                            CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                            AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                            DataKeyField="ID" ShowFooter="True">
                                    
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								                <Columns>
									                <asp:TemplateColumn HeaderText="No">
                                                        <HeaderStyle Width="3%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                    
                                                    <asp:TemplateColumn SortExpression="DealerID" HeaderText="Dealer ID" Visible="false">
										                <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                <ItemTemplate>
											                <asp:Label id="lblDealerID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
											                </asp:Label>
										                </ItemTemplate>
									                </asp:TemplateColumn>
									                <asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
										                <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                <ItemTemplate>
											                <asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
											                </asp:Label>
										                </ItemTemplate>
									                </asp:TemplateColumn>
									                <asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Dealer">
										                <HeaderStyle Width="21%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                </asp:BoundColumn>
									                <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										                <HeaderStyle Width="10%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                <ItemTemplate>
											                <asp:Label id="lblCity" runat="server" Text=""></asp:Label>
										                </ItemTemplate>
									                </asp:TemplateColumn>
									                <asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
										                <HeaderStyle Width="10%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                <ItemTemplate>
											                <asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
										                </ItemTemplate>
									                </asp:TemplateColumn>
									                <asp:BoundColumn DataField="SearchTerm1" SortExpression="SearchTerm1" HeaderText="Term Cari 1">
										                <HeaderStyle Width="12%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                </asp:BoundColumn>
									                <asp:BoundColumn DataField="SearchTerm2" SortExpression="SearchTerm2" HeaderText="Term Cari 2">
										                <HeaderStyle Width="15%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                </asp:BoundColumn>

                                                    <asp:TemplateColumn>
                                                        <HeaderStyle ForeColor="White" Width="5%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                        <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbtnDelete" CausesValidation="false" runat="server" CommandName="delete" Visible="true">
												                <img src="../images/trash.gif" border="0" alt="Hapus" >
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                    
                                                        </FooterTemplate>
                                                    </asp:TemplateColumn>
								                </Columns>
							                </asp:datagrid>

                                    </div>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>                     
                </td>
            </tr>

                   
        </table><br />
        <asp:Button ID="btnSave" runat="server" Text="Simpan" />
        <asp:Button ID="btnDealerHelper" style="display:none;" runat="server"  Text="" CausesValidation="false" />
        <asp:Button ID="btnBack" runat="server" Text="Kembali" />
    </form>
</body>
</html>
