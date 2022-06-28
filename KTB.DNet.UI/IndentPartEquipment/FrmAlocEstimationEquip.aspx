<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAlocEstimationEquip.aspx.vb" Inherits="FrmAlocEstimationEquip" %>
<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmAlocEstimationEquip</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
        <script language="javascript">
        
   		function ShowSparePartMasterSelection()
		{
		    showPopUp('../PopUp/PopUpSparePart.aspx?IPMaterialtype=2&IsMultiple=True','',700,700,SparePartMasterSelection);
		}

        function ShowDocFlow(ponumber)
        {
            showPopUp('../PopUp/PopUpDocFlowEqpPo.aspx?ponumber=' + ponumber,'',300,560,DealerSelection);
        }
        
        function ShowLastUpdatedHistory(DocNumber)
        {
            showPopUp('../PopUp/PopUpChangeStatusHistorySV.aspx?DocType=3&DocNumber=' + DocNumber,'',500,760,DealerSelection);
        }
    
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}

		function DealerSelection(selectedDealer)
		{
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = selectedDealer;
		}
		
		function PONOSelection(PONOSelection)
		{
			var txtPONumber= document.getElementById("txtPONumber");
			txtPONumber.value = PONOSelection;				
		}

		function POSelection(POSelection)
		{
			var txtNoPO= document.getElementById("txtEstimationNumber");
			txtNoPO.value = POSelection;				
		}
		
		function SparePartMasterSelection(SparePartNumber)
		{
			var txtPartNumber= document.getElementById("txtPartNumber");
			txtPartNumber.value = SparePartNumber
		}
		
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
					elm.checked = checkVal
					}
				}
			}
		}
		
        function NumOnlyBlurWithOnGridTxtCustom(txtclientid, lblremainqtyclientid, lblorderqtyclientid, hdnremainqtyclientid)
	    {
		    var txtalocqty = document.getElementById(txtclientid);
		    var lblRemainQty = document.getElementById(lblremainqtyclientid);
		    var lblOrderQty = document.getElementById(lblorderqtyclientid);
		    var hdnRemainQty = document.getElementById(hdnremainqtyclientid);
   		    var alocqty = parseFloat(txtalocqty.value);
   		    var orderqty = parseFloat(lblOrderQty.innerText);
   		    var hdnremainqty = parseFloat(hdnRemainQty.value);
   		    var remainqty = alocqty + hdnremainqty;
   		    if (alocqty > (orderqty - hdnremainqty)) 
   		    {
   		        alert('Alokasi Qty Tidak Bisa Lebih Besar Dari Order Qty');
   		        txtalocqty.focus();
   		        txtalocqty.value = orderqty - hdnremainqty;
   		        return;
   		    }
            document.getElementById(lblremainqtyclientid).innerText = parseFloat(remainqty);
	    }
	    	
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <div class="titlePage">INDENT PART EQUIPMENT - Pengajuan Alokasi PO Indent Part 
                Equipment
            </div>
            <TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
                <TR>
                    <TD>Kode Dealer</TD>
                    <TD>:</TD>
                    <TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                            runat="server" Width="160px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
                            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
                    <TD><STRONG>Keterangan :</STRONG></TD>
                </TR>
                <TR>
                    <TD>Nomor Pengajuan</TD>
                    <TD>:</TD>
                    <TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtPONumber" onblur="omitSomeCharacter('txtNoPO','<>?*%$')"
                            runat="server" Width="160px"></asp:textbox><asp:label id="lblPopUpPoNo" runat="server">
                            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
                    </TD>
                    <TD></TD>
                </TR>
                <TR>
                    <TD>Tanggal Pengajuan</TD>
                    <TD>:</TD>
                    <TD><cc1:inticalendar id="icPODateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar>&nbsp;s/d&nbsp;
                        <cc1:inticalendar id="icPODateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
                    <TD></TD>
                </TR>
                <TR>
                    <TD>Tipe Barang</TD>
                    <TD>:</TD>
                    <TD>Equipment</TD>
                    <TD></TD>
                </TR>
                <TR>
                    <TD>Nomor Barang</TD>
                    <TD>:</TD>
                    <TD>
                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPartNumber" tabIndex="10"
                            runat="server" width="160px"></asp:TextBox>
                        <asp:Label id="lblPopUpSparePart" tabIndex="20" runat="server" height="10px">
                            <img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
                                border="0"></asp:Label></TD>
                    <TD></TD>
                </TR>
                <TR>
                    <TD></TD>
                    <TD></TD>
                    <TD><asp:button id="btnFind" runat="server" Text="Cari"></asp:button></TD>
                    <TD></TD>
                </TR>
                <TR>
                    <TD></TD>
                    <TD></TD>
                    <TD></TD>
                    <TD></TD>
                </TR>
                <TR>
                    <TD colSpan="4">
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 500px">
                            <asp:datagrid id="dtgEquipPO" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
                                BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None"
                                BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No.">
                                        <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <HeaderTemplate>
                                            <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															document.forms[0].chkAllItems.checked)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItemChecked" Runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="SparePartPO.Dealer.DealerCode" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPO.Dealer.DealerCode") %>' runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
                                        <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
                                        <HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label Runat="server" ID="lblPartName" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>' >Label</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model">
                                        <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label Runat="server" ID="lblModel" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.ModelCode") %>' >Label</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Remain Qty">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label Runat="server" ID="lblRemainQty" CssClass="textRight" Text=""></asp:Label>
                                            <input type="hidden" id="hdnRemainQty" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Order Qty">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderQty" Runat="server" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="AllocationQty" HeaderText="Alokasi Qty">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:TextBox id="txtQtyAllocation" onkeypress="return numericOnlyUniv(event)" Width="60px" MaxLength="6"
                                                Text="0" CssClass="textRight" Runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="SparePartPO.PONumber" HeaderText="Nomor Pengajuan">
                                        <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label Runat="server" ID="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPO.PONumber") %>' >
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="SparePartPO.CreatedTime" HeaderText="Tanggal Pengajuan">
                                        <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label Runat="server" ID="Label4" Text='<%# format(DataBinder.Eval(Container, "DataItem.SparePartPO.CreatedTime"),"dd MMM yyyy") %>' >
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False" HeaderText="Nomor PO">
                                        <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblPO" Runat="server" text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton id=lbtnEdit runat="server" Text="Ubah" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
                                                <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton id=lbtnSave tabIndex=49 CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Save" Runat="server" text="Simpan">
                                                <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                            <asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="Cancel" Runat="server" text="Batal">
                                                <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:datagrid>
                        </div>
                    </TD>
                </TR>
                <TR>
                    <TD colSpan="4" align="center"><asp:button id="btnSave" Text="Simpan" Runat="server" Enabled="False"></asp:button><asp:button id="btnDownload" Text="Download" Runat="server" Enabled="False"></asp:button></TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
