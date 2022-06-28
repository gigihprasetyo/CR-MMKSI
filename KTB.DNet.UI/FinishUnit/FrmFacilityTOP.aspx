<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFacilityTOP.aspx.vb" Inherits="FrmFacilityTOP" %>
<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFacilityTOP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" type="text/javascript">
		function ValidateRadioButton(obj)
			{
				var rbtnDate = document.getElementById("rbtnDate");
				var rbtnDay = document.getElementById("rbtnDay");
				var rbtnNone = document.getElementById("rbtnNone");
				rbtnDate.checked = false;
				rbtnDay.checked = false;
				rbtnNone.checked = false;
				obj.checked = true;			
			}
			
		
		function GetSelectedTOP()
		{
			var rbtnDate = document.getElementById("rbtnDate");
			var rbtnDay = document.getElementById("rbtnDay");
			var rbtnNone = document.getElementById("rbtnNone");
			var icmaxdate = document.getElementById("icmaxdate");
			var txtMaxDay = document.getElementById("txtMaxDay");
			
			if(rbtnDate.checked)
			{
					var selectedTOP = "0," + icmaxdate.value +",0";
					window.returnValue = selectedTOP;
			}
			else if(rbtnDay.checked)
			{
					var selectedTOP = "1," + icmaxdate.value + "," + CInt(txtMaxDay.Text);
					window.returnValue = selectedTOP;
			}
			else
			{
					var selectedTOP = "-1,,";
					window.returnValue selectedTOP;
			}
			window.close()
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="Label6" runat="server">Fasilitas TOP</asp:label>xxxx</TD>
					<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label11" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 20px" width="25%">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:radiobutton id="rbtnDate" runat="server" Text="s/d Tanggal" GroupName="TOP"></asp:radiobutton></TD>
								<TD><cc1:inticalendar id="icMaxDate" runat="server"></cc1:inticalendar></TD>
							</TR>
						</TABLE>
					</TD>
					<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
					<TD style="HEIGHT: 20px" width="1%"></TD>
					<TD style="HEIGHT: 20px" width="29%"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
					<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label15" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 20px" width="25%">
						<TABLE id="Table4" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:radiobutton id="rbtnDay" runat="server" Text="Hari" GroupName="TOP"></asp:radiobutton></TD>
								<TD><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtMaxDay" runat="server" MaxLength="3"
										size="22">0</asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
					<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
					<TD style="HEIGHT: 20px" width="1%"></TD>
					<TD style="HEIGHT: 20px" width="29%"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
					<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label16" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 20px" width="25%">
						<TABLE id="Table5" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:radiobutton id="rbtnNone" runat="server" Text="Tidak Ada" Checked="True" GroupName="TOP"></asp:radiobutton></TD>
							</TR>
						</TABLE>
					</TD>
					<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
					<TD style="HEIGHT: 20px" width="1%"></TD>
					<TD style="HEIGHT: 20px" width="29%"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="24%"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedTOP()" type="button" value="Pilih"
							name="btnChoose">&nbsp; <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
							name="btnCancel">
					</TD>
					<TD style="HEIGHT: 20px" width="1%"></TD>
					<TD style="HEIGHT: 20px" width="25%">&nbsp;</TD>
					<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
					<TD style="HEIGHT: 20px" width="1%"></TD>
					<TD style="HEIGHT: 20px" width="29%"></TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
