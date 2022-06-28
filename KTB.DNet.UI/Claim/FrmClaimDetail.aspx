<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmClaimDetail.aspx.vb" Inherits="FrmClaimDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmClaimDetails</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPKodeBarangSelection()
			{
			    var ltrNoFaktur = document.getElementById('ltrNoFaktur');
			    var NoSO = document.getElementById("lblNoSO").innerHTML;
			    //showPopUp('../PopUp/PopUpSparePartByFaktur.aspx?NoFaktur='+ ltrNoFaktur.innerText ,'',700,700,KodeBarang);
			    showPopUp('../PopUp/PopUpSparePartByFaktur.aspx?NoFaktur=' + ltrNoFaktur.innerHTML + '&NoSO=' + NoSO, '', 700, 700, KodeBarang);
			}
			function KodeBarang(selectedCode)
			{
			    
			    var indek = GetCurrentInputIndex();
			   
				var dtgEntryClaimEdit = document.getElementById("dtgEntryClaimEdit");
				var tempParam = selectedCode.split(';');
			 
				var KodeBarangg = dtgEntryClaimEdit.rows[indek].getElementsByTagName("INPUT")[0];
				 
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				    KodeBarangg.innerText = tempParam[0];
				}
				else
				{
				    KodeBarangg.value = tempParam[0];
				}

			}
			function GetCurrentInputIndex()
			{
				var dtgEntryClaimEdit = document.getElementById("dtgEntryClaimEdit");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
		 
				for (index = 0; index < dtgEntryClaimEdit.rows.length; index++)
				{
					inputs = dtgEntryClaimEdit.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0) {
					    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
					        if (inputs[indexInput].type == "text") {
					            var IsReadOnly = inputs[indexInput].readOnly;
					            //console.log("rows :" + index + " ; col : " + indexInput   );
					            // console.log(inputs[indexInput]);
					            if ( !IsReadOnly )
					            {
					                //console.log(IsReadOnly);
					                return index;
					            }
					        }
					    }
					}
				}
				
				return -1;
			}		

			function ValidateReject(ddlStatus, txtQtyApproved)
			{	
				var ddlStatusx=document.getElementById(ddlStatus);
				var txtQtyApprovedx=document.getElementById(txtQtyApproved);
				//alert(ddlStatusx.value);
				if (ddlStatusx.value=='1')
				{
					//txtQtyApprovedx.value='0';
					return
				}
				

			}	

			function ValidateAmount(qtyApproved, qtyClaim, Status)
			{	
				var result=true;
				var qtyApprovedID=qtyApproved;
				var txtqtyApproved=document.getElementById(qtyApproved);
				var qtyClaim = parseInt(document.getElementById(qtyClaim).value);
				var qtyApproved = parseInt(document.getElementById(qtyApproved).value);
				var ddlStatus=document.getElementById(Status);
				if (qtyApproved>qtyClaim)
				{
					alert('Qty Approved Tidak Boleh Lebih Besar Dari Qty Claim');
					txtqtyApproved.value='';
					return false;
				}
				
				//else
				//{
				//	if ( ddlStatus.value=='1' && qtyApproved>0 )
				//	{
				//		alert('Qty Approved Harus 0 untuk status tolak');
				//		txtqtyApproved.value='0';
				//		return;
				//	}
				//
				//}
				
				return true;
			}	

			function CalculateAmount(qtyClaim, HargaSatuan, Total,qtyClaimx, Status)
			{	
				if (ValidateAmount(qtyClaim,qtyClaimx, Status)==false)
				{
					return;
				}
				var qtyClaim = document.getElementById(qtyClaim).value;
				var HargaSatuan = document.getElementById(HargaSatuan).innerHTML.replace('.','');
				var Total = document.getElementById(Total);
							
				var partPrice = parseFloat(HargaSatuan);
				if (isNaN(partPrice)|| partPrice=="")	
				{
					partPrice=0;
				}									
				var amount=parseInt(qtyClaim)*parseFloat(partPrice);
				if (isNaN(amount))
				{
					Total.innerHTML="";
				}
				else
				{
					Total.innerHTML=amount;		
				}		
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
        </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">CLAIM - Detail Claim</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titleField">Kode Dealer</TD>
					<TD>:</TD>
					<TD><asp:literal id="ltrDealerCode" runat="server"></asp:literal></TD>
					<TD class="titleField">Nomor Faktur &nbsp;/&nbsp;SO</TD>
					<TD>:</TD>
					<TD><asp:label id="ltrNoFaktur" runat="server"></asp:label>
                        &nbsp;/&nbsp;
                        <asp:Label ID="lblNoSO" runat="server"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD class="titleField">Nama Dealer</TD>
					<TD>:</TD>
					<TD><asp:literal id="ltrDealerName" runat="server"></asp:literal></TD>
					<TD class="titleField">Tanggal Faktur</TD>
					<TD>:</TD>
					<TD><asp:label id="lblTglFaktur" runat="server"></asp:label></TD>
				</TR>
				<TR vAlign="top">
					<TD class="titleField">Nomor / Tanggal Claim</TD>
					<TD>:</TD>
					<TD><asp:literal id="ltrNoAndDate" runat="server"></asp:literal></TD>
					<TD class="titleField">Penjelasan</TD>
					<TD>:</TD>
					<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtComment" onblur="omitSomeCharacter('txtComment','<>?*%$;')"
							runat="server" TextMode="MultiLine"></asp:textbox><asp:literal id="ltrPenjelasan" runat="server"></asp:literal></TD>
				</TR>
				<TR>
					<TD class="titleField">Status</TD>
					<TD>:</TD>
					<TD><asp:literal id="ltrStatus" runat="server"></asp:literal></TD>
					<TD class="titleField">Upload Evidence</TD>
					<TD>:</TD>
					<TD><input id="fuEvidence" type="file" name="fuEvidence" runat="server">&nbsp;
						<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button></TD>
				</TR>
				<TR>
					<TD class="titleField">Alasan Claim</TD>
					<TD>:</TD>
					<TD><asp:dropdownlist id="ddlClaimReasonHeader" runat="server" Enabled="False"></asp:dropdownlist><asp:literal id="ltrAlasanClain" runat="server" Visible="False"></asp:literal></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:label id="lblFilename" runat="server"></asp:label></TD>
				</TR>
			</TABLE>
			<asp:panel id="pnlEdit" Runat="server">
				<asp:datagrid id="dtgEntryClaimEdit" runat="server" DataKeyField="ID" ShowFooter="True" Width="100%"
					AutoGenerateColumns="False" AllowSorting="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro"
					BackColor="#CDCDCD" BorderWidth="0px">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id=lblNo runat="server" text="<%# container.itemindex+1 %>">
								</asp:Label>
							</ItemTemplate>
							<FooterStyle Font-Size="Small"></FooterStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="SparePartPOStatusDetail.SparePartMaster.PartNumber" HeaderText="Nomor Barang">
							<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id=lblNoBarang Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartNumber") %>' Runat="server">
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:TextBox id="txtFooterNomorBarang" runat="server" Width="60px" BackColor="White" MaxLength="18" ></asp:TextBox>
								<asp:Label id="lblFooterNomorBarang" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:TextBox id=txtNomorBarangEdit runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SparePartPOStatusDetail.SparePartMaster.PartNumber" ) %>' Width="95px" BackColor="White" MaxLength="18">
								</asp:TextBox>
								<asp:Label id="lblEditNomorBarang" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
							<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id=lblNamaBarang runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartName") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Qty Claim">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblQtyClaim" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<FooterTemplate>
								<asp:TextBox ID="txtQtyClaimEntry" Width="30px" Runat="server" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
									MaxLength="6" />
								<asp:RangeValidator id="RangeValidator2" runat="server" ControlToValidate="txtQtyClaimEntry" ErrorMessage="Quantity claim harus lebih besar dari 0"
									MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="txtQtyClaimEntryEdit" Width="30px" Runat="server" CssClass="textRight" onkeypress="return numericOnlyUniv(event)" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'/>
								<asp:RangeValidator id="Rangevalidator1" runat="server" ControlToValidate="txtQtyClaimEntryEdit" ErrorMessage="Quantity claim harus lebih besar dari 0"
									MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Qty Disetujui">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id=lblQtyApproved Text='<%# Format(DataBinder.Eval(Container, "DataItem.ApprovedQty"),"#,##0") %>' Visible="False" Runat="server" CssClass="textRight">
								</asp:Label>
								<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=TxtQtyApproved Text='<%# DataBinder.Eval(Container, "DataItem.ApprovedQty") %>' Runat="server" Width="60px" CssClass="textRight" MaxLength="6" ReadOnly="True">
								</asp:TextBox>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<EditItemTemplate>
								<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtQtyApprovedEdit Text='<%# DataBinder.Eval(Container, "DataItem.ApprovedQty") %>' Runat="server" Width="60px" CssClass="textRight" MaxLength="6">
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Harga Satuan">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblHargaSatuan" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.ClaimPriceUnit"),"#,##0") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Harga Total">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label ID="lblTotal" Runat="server" />
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Keterangan">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id=lblKeterangan Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>' Runat="server" >
								</asp:Label>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<FooterTemplate>
								<asp:TextBox id="txtKeteranganEntry" Runat="server" MaxLength="280"></asp:TextBox>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:TextBox id=txtKeteranganEntryEdit Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan")%>' Runat="server" MaxLength="280">
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Kondisi Barang">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblConditionID" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimGoodCondition.ID") %>' Runat="server" visible="False">
								</asp:Label>
								<asp:DropDownList id="drpConditionKTB" Runat="server"></asp:DropDownList>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList id="drpConditionFooter" Runat="server" Enabled="False" Visible="False"></asp:DropDownList>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:Label id=lblConditionEditID Text='<%# DataBinder.Eval(Container, "DataItem.ClaimGoodCondition.ID") %>' Runat="server" visible="False">
								</asp:Label>
								<asp:DropDownList id="drpConditionEdit" Runat="server"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id=lblStatusID Text='<%# DataBinder.Eval(Container, "DataItem.StatusDetailKTB") %>' Visible="False" Runat="server">
								</asp:Label>
								<asp:DropDownList id="ddlStatusEditDtg" Runat="server"></asp:DropDownList>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList id="ddlStatusFooter" Runat="server"></asp:DropDownList>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:Label id=lblStatusEditID Text='<%# DataBinder.Eval(Container, "DataItem.StatusDetailKTB") %>' Runat="server" visible="False">
								</asp:Label>
								<asp:DropDownList id="ddlStatus" Runat="server"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
							CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
							EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
							<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:EditCommandColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
							<FooterTemplate>
								<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False" HeaderText="Claim ID">
							<ItemTemplate>
								<asp:Label id=lblClaimDtlID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid>
			</asp:panel><asp:panel id="pnlView" Runat="server">
				<asp:datagrid id="dgView" runat="server" ShowFooter="True" Width="100%" AutoGenerateColumns="False"
					AllowSorting="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD"
					BorderWidth="0px">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:BoundColumn ReadOnly="True" HeaderText="No">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Nomor Barang" SortExpression="SparePartPOStatusDetail.SparePartMaster.PartNumber">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label ID="Label1" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartNumber") %>'/>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Nama Barang" SortExpression="SparePartPOStatusDetail.SparePartMaster.PartName">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.SparePartMaster.PartName") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<b>Total</b>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Qty Faktur" SortExpression="SparePartPOStatusDetail.BillingQuantity">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label ID="Label7" Runat="server" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.BillingQuantity", "{0:#,###}") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<FooterTemplate>
								<asp:Label ID="Label8" Runat="server" Width="60" CssClass="textRight"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Qty Claim" SortExpression="Qty">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label ID="Label3" Runat="server" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.Qty", "{0:#,###}") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<FooterTemplate>
								<asp:Label ID="lblSumQtyView" Runat="server" Width="60" CssClass="textRight"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Qty Disetujui" SortExpression="ApprovedQty">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label ID="Label6" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ApprovedQty", "{0:#,##0}") %>' />
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<FooterTemplate>
								<asp:Label ID="lblSumQtyApprovedView" Runat="server" Width="60" CssClass="textRight"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Harga Satuan">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatusDetail.ClaimPriceUnit", "{0:#,##0}") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Harga Total">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label ID="Label5" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount", "{0:#,##0}") %>' />
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
							<FooterTemplate>
								<asp:Label ID="lblSumTotalView" Runat="server" Width="60" CssClass="textRight"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Keterangan">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label ID="Textbox1" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>' /></label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Kondisi Barang" SortExpression="ClaimGoodCondition.Condition">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label ID="lblCondition" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimGoodCondition.Condition") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status" SortExpression="StatusDetail">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label ID="lblStatus" Runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid>
			</asp:panel><BR>
              <br />
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p>Pesan Error :</p><br />
            <p class="text-danger">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>
			<asp:button class="hideButtonOnPrint" id="btnSave" Text="Simpan" Runat="server"></asp:button><asp:button class="hideButtonOnPrint" id="btnCancel" runat="server" Text="Kembali"></asp:button><INPUT class="hideButtonOnPrint" onclick="window.print()" type="button" value="Cetak">
		</form>
	</body>
</HTML>
