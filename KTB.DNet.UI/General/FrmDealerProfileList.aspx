<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerProfileList.aspx.vb" Inherits="FrmDealerProfileList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" type="text/javascript">

		var allSlides;
		var currSlide = 0;
		var slideInterval;


		function initSlides() 
		{
			
			var myDate = new Date( );
			getAllSlides();
			slideInterval = setInterval("nexttimer()", 4000);
		}

		function getAllSlides() {
			var allChildren = document.getElementById("pnlSlideShow").childNodes;
			
			var slideElems = new Array( );
			var x=0;
			for (var i = 0; i < allChildren.length-1; i++) 
			{
				
				if (allChildren[i].id != null)
				{	
								
					if (allChildren[i].id.substring(0,7) == "MyImage") 
					{
						slideElems[slideElems.length] = allChildren[i];
						x++;
					}
				}
			}
			allSlides = slideElems;
		}


		function displayslide(indexslide)
		{
			allSlides[currSlide].style.display = "none";
			currSlide=indexslide-1;
			next( );
		}

		function next( ) 
		{
			if (currSlide < 0) 
			{
				allSlides[++currSlide].style.display = "block";
			} 
			else if (currSlide < allSlides.length - 1) 
			{
				allSlides[currSlide].style.display = "none";
				allSlides[++currSlide].style.display = "block";
			} else if (currSlide == allSlides.length - 1) 
			{
				allSlides[currSlide].style.display = "none";
				allSlides[0].style.display = "block";
				currSlide=0;
			}
			

			clearInterval(slideInterval);
			slideInterval = setInterval("nexttimer()", 4000);
		}

		function previous( ) 
		{
			if (currSlide == 0) 
			{
				allSlides[0].style.display = "none";
				currSlide=allSlides.length - 1;
				allSlides[currSlide].style.display = "block";
			} 
			else if (currSlide <= allSlides.length - 1) 
			{
				allSlides[currSlide].style.display = "none";
				allSlides[currSlide-1].style.display = "block";
				currSlide=currSlide-1;
			} 
			

			clearInterval(slideInterval);
			slideInterval = setInterval("nexttimer()", 4000);
		}


		function nexttimer() 
		{
			next( );
		}

		
		</script>
	</HEAD>
	<body onload="initSlides();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">DEALER - Dealer Profile List</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR valign="top">
								<TD class="titleField" rowspan="13" colspan="3" width="24%">
									<TABLE id="Table3">
										<TR>
											<TD align="center"><asp:panel id="pnlSlideShow" runat="server" BorderStyle="Solid" Height="250px" BorderWidth="1px"
													Width="250px"></asp:panel><asp:image id="Image1" style="CURSOR: hand" onclick="previous();" runat="server" ImageUrl="../images/page_prev.gif"></asp:image>&nbsp;&nbsp; 
												&nbsp; &nbsp;&nbsp;&nbsp;
												<asp:image id="Image2" style="CURSOR: hand" onclick="next();" runat="server" ImageUrl="../images/page_next.gif"></asp:image></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="29%"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="20%">Nama Dealer</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="29%"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="20%">Alamat</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="29%"><asp:label id="lblAddress" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Kota</TD>
								<TD style="HEIGHT: 26px"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:label id="lblCityName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Area</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblAreaCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Group</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblGroupName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Telepon</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblPhone" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Fax</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblFax" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Email</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblEmail" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Status Dealer</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblDealerStatus" runat="server" Width="144px"></asp:label>
									<asp:Label id="lblStatusAdd" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Klasifikasi Dealer</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblDealerClass" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">Tahun Berdiri</TD>
								<TD style="HEIGHT: 11px"><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:label id="lblHeldYear" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"><asp:button id="btnClose" runat="server" Width="60px" Text="Tutup"></asp:button></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px">&nbsp;</TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR valign="top">
								<TD vAlign="top" colSpan="4"><asp:panel id="PnlManajemen" runat="server" Height="24px" Font-Bold="True">MANAJEMEN</asp:panel></TD>
								<td colspan="2">
									<asp:LinkButton ID="lbtnShowroomAudit" Runat="server">Dealer Identity</asp:LinkButton><br>
									<asp:LinkButton ID="lbtnStrukturOrganisasi" Runat="server">Struktur Organisasi</asp:LinkButton><br>
									<asp:LinkButton ID="lbtnSalesForce" Runat="server">Detail Sales Force</asp:LinkButton><br>
								</td>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
