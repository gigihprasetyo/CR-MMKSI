<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTransferPaymentList.aspx.vb" Inherits=".FrmTransferPaymentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        
    <title>Daftar Pembayaran Transfer</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script>
        function Detail()
        { }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }
    </script>

</head>
<body>
    
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						<asp:Label ID="lblTitle" runat="server">TRANSFER - Daftar Pembayaran Transfer</asp:Label></td>
                </tr>
                <tr style="height: 1px;">
                    <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;"></td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="8" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="1300px"></td>
                                <td width="1px"></td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Product Category</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:DropDownList ID="ddlProductCategoryX" runat="server" Width="160px"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><b>Kode Dealer</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
                                </td>
                                <td><b>Produk</b></td>
                                <td><b>:</b></td>
                                <td><asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" width="130px"  ></asp:dropdownlisT></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>Tgl Transfer</b></td>
                                <td><b>:</b></td>
                                <td>                                    
									<table border="0" cellPadding="0">
										<tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkPlanTransferDate" Text="" />
                                            </td>
											<td><cc1:inticalendar id="calPlanTransferDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s.d&nbsp;</td>
											<td><cc1:inticalendar id="calPlanTransferDateEnd" runat="server" TextBoxWidth="70" CanPostBack="False"></cc1:inticalendar></td>
										</tr>
									</table>
                                </td>
                                <td><b>No.Reg</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtRegNumber"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>Tgl&nbsp; Kirim</b></td>
                                <td><b>:</b></td>
                                <td>                                    
									<table border="0" cellPadding="0">
										<tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkDueDate" Text="" />
                                            </td>
											<td><cc1:inticalendar id="calDueDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s.d&nbsp;</td>
											<td><cc1:inticalendar id="calDueDateEnd" runat="server" TextBoxWidth="70" CanPostBack="False"></cc1:inticalendar></td>
										</tr>
									</table>
                                </td>
                                <td><b>Debit Number</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDebitNumber"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>         
                                <td><b>Tujuan Pembayaran</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentPurpose" runat="server"  Width="142px" ></asp:DropDownList>
                                </td>
                                <td><b>No SO</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSONumber"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr >                                
                                <td><b>No Reg Pemercepat</b></td>
                                <td><b>:</b></td>
                                <td> <asp:TextBox runat="server" ID="txtRegPemercepat"></asp:TextBox> </td>
                                <td><b>Status</b>
                                    </td>
                                <td><b>
                                :</b></td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server"   Width="142px" ></asp:DropDownList>
                                </td>
                                <td></td>                                
                            </tr>
                             <tr  >                                
                                <td><b>No Reg Dipercepat</b></td>
                                <td><b>:</b></td>
                                <td>  <asp:TextBox runat="server" ID="txtRegDipercepat"></asp:TextBox></td>
                                <td><b>
                                    <asp:Label runat="server" id="lblIsNotOntime" text="On Time" />
                                    </b></td>
                                <td><b><asp:Label runat="server" id="lblIsNotOntimeSeparator" Text=":" /></b></td>
                                <td>
                                    <asp:DropDownList runat="server" id="ddlIsNotOntime"></asp:DropDownList>
                                </td>
                                <td></td>                               
                            </tr>
                             <tr>                                
                                <td><b></b></td>
                                <td><b></b></td>
                                <td></td>
                                <td><strong>Total Amount</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:Label ID="lblTAmount" runat="server"></asp:Label> </td>
                                <td></td>
                            </tr>
                             <tr style="display:none;">                                
                                <td><b></b></td>
                                <td><b></b></td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td><b></b></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr style="display:none;">                                
                                <td><b></b></td>
                                <td><b></b></td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td><b></b></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6">

                                    <div id="divHidden" style="overflow: auto; width: 100%; height: 290px">
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
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle ForeColor="White" Width="40px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNo"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Kategori Produk" Visible="false">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblProductCategory"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Kode Dealer" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDealerCode"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Transfer" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPlanTransferDate"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Jatuh Tempo" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDueDate"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tujuan Pembayaran" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPaymentPurposeCode"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Reg" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRegNumber"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                  <asp:TemplateColumn HeaderText="Nomor Reg Pemercepat" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRegNumberPemercepat"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>


                                                  <asp:TemplateColumn HeaderText="Nomor Reg DiPercepat" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRegNumberDipercepat"  CssClass="textRight" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Total Amount" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmount"  CssClass="textRight" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                 
                                                   <asp:TemplateColumn HeaderText="Selisih Transfer" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSelisih"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>


                                                <asp:TemplateColumn HeaderText="Nilai Transfer">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>

                                                           <asp:LinkButton ID="lbtnTransferactual" runat="server"  >
															 

                                                        <asp:Label runat="server" ID="lblNilaiTransfer"  CssClass="textRight" ></asp:Label></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>


                                               

                                               <asp:TemplateColumn HeaderText="Tgl Transfer">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTglTransfer"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                  <asp:TemplateColumn HeaderText="Total Aktual Amount" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblActAmount"  CssClass="textRight" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>


                                                

                                                  <asp:TemplateColumn HeaderText="Tgl Aktual Transfer" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblActTransfer"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>



                                                <asp:TemplateColumn HeaderText="Status" >
                                                    <HeaderStyle ForeColor="White" Width="50px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Detail">
															<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>

                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6"><asp:Button Text="Download" runat="server" ID="btnDownload" visible="True"/></td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
