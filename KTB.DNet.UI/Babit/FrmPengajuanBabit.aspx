<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPengajuanBabit.aspx.vb" Inherits="FrmPengajuanBabit" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmPengajuanBabit</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">			
        
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var data = selectedDealer.split(";");
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = data[0];
			var lblDealerName = document.getElementById("lblDealerName");
			lblDealerName.innerHTML = data[1];
		}
		
        
			function daysBetween(date1, date2) {				
				//Calculate difference btw the two dates, and convert to days
				var one_day=1000*60*60*24;				
				var days = (Math.ceil((date2.getTime()-date1.getTime())/(one_day)))
				if (days < 0) {
					return -1;
				}else{
					return days;
				}
			}
			function updateJumlahHari(dt1, txt, IsGoRight){
				if(IsGoRight){
					var str = dt1.value.split("/");
					
					var date1 = new Date(str[1] + "/" + str[0] + "/" + str[2]);				
					str = dt1.parentNode.parentNode.parentNode.parentNode.parentNode.nextSibling.nextSibling.childNodes[1].rows[0].cells[1].childNodes[0].value;
					str = str.split("/");
					var date2 = new Date(str[1] + "/" + str[0]+ "/" + str[2]);
					txt.value = daysBetween(date1, date2) + 1;
					/*if days < 0
					{
						txt.value = 0;
					}
					else
					{
						txt.value = days;
					}*/
				}else{
					var str = dt1.value.split("/");
					
					var date2 = new Date(str[1] + "/" + str[0] + "/" + str[2]);				
					str = dt1.parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling.previousSibling.childNodes[1].rows[0].cells[1].childNodes[0].value;
					str = str.split("/");
					var date1 = new Date(str[1] + "/" + str[0]+ "/" + str[2]);
					txt.value = daysBetween(date1, date2) + 1;				
					/*if days < 0
					{
						txt.value = 0;
					}
					else
					{
						txt.value = days;
					}*/
				}				
			}
			function DisplayJenisKegiatan()
			{
				var jenis  = document.getElementById('ddlJenisKegiatan');				
				var Pameran  = document.getElementById('divPameran');
				var Event  = document.getElementById('divEvent');
				var Iklan  = document.getElementById('divIklan');
				var PameranGrid  = document.getElementById('divPameranGrid');
				var EventGrid = document.getElementById('divEventGrid');
				var hdnPameran = document.getElementById('hdnPameran');
				var hdnIklan = document.getElementById('hdnIklan');
				var hdnEvent = document.getElementById('hdnEvent');
				var hdnPameranSubmit = document.getElementById('hdnPameranSubmit');
				var hdnIklanSubmit = document.getElementById('hdnIklanSubmit');
				var hdnEventSubmit = document.getElementById('hdnEventSubmit');
				var btnSave = document.getElementById('btnSave');
				var btnSubmit = document.getElementById('btnSubmit');
				var NoPengajuan = document.getElementById('lblNoPengajuan');

				if (jenis.value == '-1') {
					Pameran.style.display='none';
					Event.style.display='none';
					Iklan.style.display='none';
					PameranGrid.style.display='none';
					EventGrid.style.display='none';
					btnSave.disabled=true;
					if(btnSubmit){
						btnSubmit.disabled=true;
					}
				}
				else if (jenis.value == '0') {
					Pameran.style.display='block';
					Event.style.display='none';
					Iklan.style.display='none';
					PameranGrid.style.display='block';
					EventGrid.style.display='none';
					if (hdnPameran.value != '') {
						NoPengajuan.innerHTML=hdnPameran.value;
						btnSave.disabled=true;
						if(btnSubmit){
							btnSubmit.disabled=false;
						}
					}
					else {
						NoPengajuan.innerHTML='Auto Generated';
						btnSave.disabled=false;
						if(btnSubmit){
							btnSubmit.disabled=true;
						}
					}
					if (hdnPameranSubmit.value != '') {
						if(btnSubmit){
							btnSubmit.disabled=true;
						}
					}
				}
				else if (jenis.value == '1') {
					Pameran.style.display='none';
					Event.style.display='none';
					Iklan.style.display='block';
					PameranGrid.style.display='none';
					EventGrid.style.display='none';
					if (hdnIklan.value != '') {
						NoPengajuan.innerHTML=hdnIklan.value;
						btnSave.disabled=true;
						if(btnSubmit){
							btnSubmit.disabled=false;
						}
					}
					else {
						NoPengajuan.innerHTML='Auto Generated';
						btnSave.disabled=false;
						if(btnSubmit){
							btnSubmit.disabled=true;
						}
					}
					if (hdnIklanSubmit.value != '') {
						if(btnSubmit){
							btnSubmit.disabled=true;
						}
					}
				}
				else if (jenis.value == '2') {
					Pameran.style.display='none';
					Event.style.display='block';
					Iklan.style.display='none';
					PameranGrid.style.display='none';
					EventGrid.style.display='block';
					if (hdnEvent.value != '') {
						NoPengajuan.innerHTML=hdnEvent.value;
						btnSave.disabled=true;
						if(btnSubmit){
							btnSubmit.disabled=false;
						}
					}
					else {
						NoPengajuan.innerHTML='Auto Generated';
						btnSave.disabled=false;
						if(btnSubmit){
							btnSubmit.disabled=true;
						}
					}
					if (hdnEventSubmit.value != '') {
						if(btnSubmit){
							btnSubmit.disabled=true;
						}
					}
				}
			}
			function SaveSelectedVehicle(ddl){
				document.all.hdnSelectedVehicleType.value = ddl.options[ddl.selectedIndex].value;
				document.all.hdnSelectedVehicleTypeIndex.value = ddl.selectedIndex;
			}
			function RestoreSelectedVehicle(ddl){
				ddl.selectedIndex = parseInt(document.all.hdnSelectedVehicleTypeIndex.value);
			}
			function RestoreSelectedVehicleByValue(ddl, val){				
				
				for(var i = 0; i < ddl.options.length; i++){					
					
					if(ddl.options[i].value == val){
						ddl.selectedIndex = i;
						document.getElementById('hdnSelectedVehicleType').value = ddl.options[ddl.selectedIndex].value;
						document.getElementById('hdnSelectedVehicleTypeIndex').value = i;
						break;
					}
				}				
			}			
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <input id="hdnSelectedVehicleType" type="hidden" value="-1" runat="server"> <input id="hdnSelectedVehicleTypeIndex" type="hidden" value="0" runat="server">
            <asp:placeholder id="phJS" Runat="server"></asp:placeholder>
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <TR>
                    <TD class="titlePage">BABIT - Pengajuan BABIT</TD>
                </TR>
                <TR>
                    <TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
                </TR>
                <TR>
                    <TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
                </TR>
                <TR>
                    <TD>
                        <TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">No Pengajuan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:label id="lblNoPengajuan" runat="server">Auto Generated</asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">No Surat Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:textbox id="txtNoSuratDealer" runat="server" MaxLength="30"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Kode Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                        runat="server" ToolTip="Dealer Search 1" Width="128px"></asp:textbox><asp:label id="lblDealerCode" runat="server"></asp:label><asp:label id="lblPopUpDealer" runat="server" width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:label>&nbsp;<asp:button id="btnFindDealer" runat="server" Text="Find"></asp:button></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Nama Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Kota</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:label id="lblCity" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Propinsi</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:label id="lblProvince" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Nomor Alokasi</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblNoPerjanjian" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Periode</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblPeriode" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Sisa Alokasi Babit</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblSisaAlokasiBabit" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Jenis Kegiatan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:dropdownlist id="ddlJenisKegiatan" runat="server" onchange="DisplayJenisKegiatan();"></asp:dropdownlist><asp:label id="lblJenisKegiatan" runat="server"></asp:label></TD>
                            </TR>
                        </TABLE>
                        <TABLE id="divPameran" style="DISPLAY: block" cellSpacing="1" cellPadding="2" width="100%"
                            border="0" runat="server">
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Tempat/Lokasi Pameran</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox id="txtTempatPameran" runat="server" MaxLength="200"></asp:textbox><asp:label id="lblTempatPameran" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Tanggal</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <TABLE cellSpacing="0" cellPadding="0" border="0">
                                        <TR>
                                            <TD><cc1:inticalendar id="icDatePameranMulai" runat="server" TextBoxWidth="70"></cc1:inticalendar><asp:label id="lblTglPameranAwal" runat="server"></asp:label></TD>
                                            <TD>&nbsp;s.d&nbsp;</TD>
                                            <TD><cc1:inticalendar id="icDatePameranAkhir" runat="server" TextBoxWidth="70"></cc1:inticalendar><asp:label id="lblTglPameranAkhir" runat="server"></asp:label></TD>
                                        </TR>
                                    </TABLE>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Ukuran Tempat</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtUkuranTempatPameran" onblur="omitSomeCharacter('txtUkuranTempatPameran','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="50"></asp:textbox><asp:label id="lblUkuranTempatPameran" runat="server"></asp:label>m2
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Jumlah Hari</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtJumlahHariPameran" onblur="omitSomeCharacter('txtJumlahHariPameran','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="4" Enabled="False"></asp:textbox><asp:label id="lblJumlahHariPameran" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Target Penjualan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtTargetPenjualanPameran" onblur="omitSomeCharacter('txtTargetPenjualanPameran','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="14"></asp:textbox><asp:label id="lblTargetPejualanPameran" runat="server"></asp:label>unit</TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Biaya Sewa Tempat</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>Rp.
                                    <asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtBiayaPameran" onblur="omitSomeCharacter('txtBiayaPameran','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="14"></asp:textbox><asp:label id="lblBiayaPameran" runat="server"></asp:label></TD>
                            </TR>
                        </TABLE>
                        <TABLE id="divEvent" style="DISPLAY: none" cellSpacing="1" cellPadding="2" width="100%"
                            border="0" runat="server">
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Tempat</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox id="txtTempatEvent" runat="server" MaxLength="50"></asp:textbox><asp:label id="lblTempatEvent" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Tanggal</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <TABLE cellSpacing="0" cellPadding="0" border="0">
                                        <TR>
                                            <TD><cc1:inticalendar id="icDateEventMulai" runat="server" TextBoxWidth="70"></cc1:inticalendar><asp:label id="lblTglEventAwal" runat="server"></asp:label></TD>
                                            <TD>&nbsp;s.d&nbsp;</TD>
                                            <TD><cc1:inticalendar id="icDateEventAkhir" runat="server" TextBoxWidth="70"></cc1:inticalendar><asp:label id="lblTglEventAkhir" runat="server"></asp:label></TD>
                                        </TR>
                                    </TABLE>
                                </TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Ukuran Tempat</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtUkuranTempatEvent" onblur="omitSomeCharacter('txtUkuranTempatEvent','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="50"></asp:textbox><asp:label id="lblUkuranTempatEvent" runat="server"></asp:label>m2</TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px; HEIGHT: 25px">Jumlah Hari</TD>
                                <TD style="WIDTH: 2px; HEIGHT: 25px">:</TD>
                                <TD style="HEIGHT: 25px"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtJumlahHariEvent" onblur="omitSomeCharacter('txtJumlahHariEvent','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="4" Enabled="False"></asp:textbox><asp:label id="lblJumlahHariEvent" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Target Penjualan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtTargetPenjualanEvent" onblur="omitSomeCharacter('txtTargetPenjualanEvent','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" MaxLength="14"></asp:textbox><asp:label id="lblTargetPenjualanEvent" runat="server"></asp:label>unit</TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
            <div id="divIklan" style="DISPLAY: none" runat="server">
                <div class="titleField" style="PADDING-RIGHT: 2px; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; WIDTH: 75%; PADDING-TOP: 2px; TEXT-ALIGN: right"><asp:label id="lblTotalBiayaIklan" Runat="server" Text="Total Biaya : Rp. 0" Font-Bold="True"></asp:label></div>
                <asp:datagrid id="dgIklan" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#DEDEDE"></FooterStyle>
                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo"
                        BackColor="#FFCC00"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" BackColor="#FFCC00"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <%# container.itemindex+1 %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Media">
                            <HeaderStyle ForeColor="White" BackColor="#FFCC00"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblMedia" runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlFMedia" Runat="server" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEMedia" Runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama Media">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo" BackColor="#FFCC00"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblNamaMedia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MediaName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFNamaMedia" Runat="server" Width="80px" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtENamaMedia" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MediaName") %>' Width="80px"/>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tgl Tayang Iklan">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo" BackColor="#FFCC00"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblStartDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.StartDate"),"dd/MM/yyyy") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <cc1:inticalendar id="icFDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <cc1:inticalendar id="icEDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tgl Selesai">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo" BackColor="#FFCC00"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblEndDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndDate"),"dd/MM/yyyy") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <cc1:inticalendar id="icFEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <cc1:inticalendar id="icEEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Produk Kategori">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblProductCatIklan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.Description") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList id="ddlFProductCatIklan" Runat="server" AutoPostBack="false"></asp:DropDownList>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList id="ddlEProductCatIklan" Text='<%# DataBinder.Eval(Container, "DataItem.Category.ID") %>' Runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Produk Display">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblKendDisplay" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlFKendDisplay" Runat="server" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEKendDisplay" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.ID") %>' />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Biaya Iklan (Rp)">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo" BackColor="#FFCC00"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblCost" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Expense"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFCost" Runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    Width="60px" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtECost" Runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Expense"),"#,##0") %>' Width="60px"/>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo" BackColor="#FFCC00"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton id="lbtnEdit" Runat="server" text="Ubah" CommandName="edit" CausesValidation="False">
                                    <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                <asp:LinkButton id="lbtnDelete" Runat="server" text="Hapus" CommandName="delete" CausesValidation="False">
                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:LinkButton id="lbtnAdd" tabIndex="40" Runat="server" text="Tambah" CommandName="add">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" text="Simpan" CommandName="save">
                                    <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                <asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" text="Batal" CommandName="cancel">
                                    <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:datagrid></div>
            <div id="divPameranGrid" style="DISPLAY: block" runat="server">
                <div class="titleField" style="PADDING-RIGHT: 2px; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; WIDTH: 75%; PADDING-TOP: 2px; TEXT-ALIGN: right"><asp:label id="lblTotalBiayaPameran" Runat="server" Text="Total Biaya : Rp. 0" Font-Bold="True"></asp:label></div>
                <asp:datagrid id="dgPameran" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD"
                    BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#DEDEDE"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="NO.">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <%# container.itemindex+1 %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Produk Kategori">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id=lblProductCategory runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.Description") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList id="ddlFProductCategory" Runat="server" AutoPostBack="false"></asp:DropDownList>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList id=ddlEProductCategory Text='<%# DataBinder.Eval(Container, "DataItem.Category.ID") %>' Runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kendaraan Display">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblCarDisplay" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlFCarDisplay" Runat="server" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlECarDisplay" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.ID") %>' />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Lain-lain (Rp)">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblOthers" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Others"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFOthers" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    Runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEOthers" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Others"),"#,##0") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
                                    <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                <asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:LinkButton id="lbtnAdd" tabIndex="40" CommandName="add" text="Tambah" Runat="server">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="save" text="Simpan" Runat="server">
                                    <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                <asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancel" text="Batal" Runat="server">
                                    <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:datagrid></div>
            <div id="divEventGrid" style="DISPLAY: none" runat="server">
                <div class="titleField" style="PADDING-RIGHT: 2px; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; WIDTH: 75%; PADDING-TOP: 2px; TEXT-ALIGN: right"><asp:label id="lblTotalBiayaEvent" Runat="server" Text="Total Biaya : Rp. 0" Font-Bold="True"></asp:label></div>
                <asp:datagrid id="dgEvent" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="NO.">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <%# container.itemindex+1 %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sewa Tempat (Rp)">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblPlace" runat="server" Text='<%# Format(cDec(DataBinder.Eval(Container, "DataItem.Place")),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFPlace" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    Runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEPlace" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Runat="server" Text='<%# Format(cdec(DataBinder.Eval(Container, "DataItem.Place")),"#,##0") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Konsumsi (Rp)">
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblConsumsion" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Comsumption"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFConsumsion" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    Runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEConsumsion" Runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Comsumption"),"#,##0") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Entertainment (Rp)">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblEntertainment" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Entertainment"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFEntertainment" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    Runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEEntertainment" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Entertainment"),"#,##0") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Perlengkapan (Rp)">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblEquipment" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Equipment"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFEquipment" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    Runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEEquipment" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Equipment"),"#,##0") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Lain-lain (Rp)">
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblOthers" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Others"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFOthers" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
                                    Runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEOthers" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Others"),"#,##0") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
                                    <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                <asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:LinkButton id="lbtnAdd" tabIndex="40" CommandName="add" text="Tambah" Runat="server">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="save" text="Simpan" Runat="server">
                                    <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                <asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancel" text="Batal" Runat="server">
                                    <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:datagrid></div>
            <TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
                <TR vAlign="top">
                    <TD class="titleField" style="WIDTH: 146px">Upload File</TD>
                    <TD style="WIDTH: 2px">:</TD>
                    <TD><asp:label id="lblFileUpload" runat="server"></asp:label><INPUT id="UploadFile" onkeydown="return false;" style="WIDTH: 267px; HEIGHT: 20px" type="file"
                            size="25" name="File1" runat="server"><br>
                        Keterangan : <i>
                            <BR>
                            - Upload Informasi Pameran/Event
                            <BR>
                            - Iklan: Image desain iklan</i>
                    </TD>
                </TR>
            </TABLE>
            <br>
            <input id="hdn" type="hidden" runat="server"> <input id="hdnPameran" type="hidden" runat="server">
            <input id="hdnEvent" type="hidden" runat="server"> <input id="hdnIklan" type="hidden" runat="server">
            <input id="hdnPameranSubmit" type="hidden" runat="server"> <input id="hdnEventSubmit" type="hidden" runat="server">
            <input id="hdnIklanSubmit" type="hidden" runat="server"> <INPUT id="hdnValNew" type="hidden" value="-1" name="hdnValNew" runat="server">
            <INPUT id="hdnValSubmit" type="hidden" value="-1" name="hdnValSubmit" runat="server">
            <asp:button id="btnBaru" Runat="server" Text="Baru"></asp:button>
            <asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>
            <asp:button id="btnSubmit" runat="server" Text="Validasi" Enabled="False"></asp:button>
            <INPUT class="hideButtonOnPrint" id="btnPrint" onclick="window.print()" type="button" value="Cetak"
                name="btnPrint" runat="server"> <input runat="server" type="button" style="WIDTH:60px" value="Kembali" id="btnBack" onclick="window.history.back();return false;"
                NAME="btnBack"> <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Kembali"
                name="btnCancel" runat="server">
        </form>
        <asp:panel id="pnlScript" Runat="server">
            <SCRIPT type="text/javascript">
		DisplayJenisKegiatan();
            </SCRIPT>
        </asp:panel><asp:placeholder id="phBottomScript" Runat="server"></asp:placeholder>
    </body>
</HTML>
