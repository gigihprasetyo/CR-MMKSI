<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmWSCParameterDetail.aspx.vb" Inherits=".FrmWSCParameterDetail"%>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmWSCParameterDetail</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        var conditionEditIndex = 0
        var popupFillIndex = 0

        window.onload = function () {
            var dtgWscParam = document.getElementById("dtgWscCondition");
            var index = dtgWscParam.rows.length;
            ddlConditionIC(index - 1, true);

            dtgWscParam = document.getElementById("dtgWscParam");
            index = dtgWscParam.rows.length;
            ddlParameterIC(index - 1, true);
        };

        function ddlOperatorFooterIC() {
            var dtgWscParam = document.getElementById("dtgWscParam");
            var index = GetCurrentInputIndex("dtgWscParam");
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var ddlOpr = dtgWscParam.rows[index].getElementsByTagName("SELECT")[1];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;
            var selectedOperatorValue = ddlOpr.options[ddlOpr.selectedIndex].value

            if (selectedParameterValue == "0" || selectedParameterValue == "5" || selectedParameterValue == "9" ||
                selectedParameterValue == "10" || selectedParameterValue == "11" || selectedParameterValue == "12" ||
                selectedParameterValue == "15" || selectedParameterValue == "19") {
                if (selectedOperatorValue != "9" && selectedOperatorValue != "10") {
                    alert("Operator tidak dapat digunakan");
                    ddlOpr.selectedIndex = 0;
                }
            }
            else if (selectedParameterValue == "1" || selectedParameterValue == "2" || selectedParameterValue == "3" ||
                selectedParameterValue == "4" || selectedParameterValue == "6" || selectedParameterValue == "7" ||
                selectedParameterValue == "8" || selectedParameterValue == "16" || selectedParameterValue == "18" || selectedParameterValue == "20") {
                if (selectedOperatorValue == "2" || selectedOperatorValue == "3" || selectedOperatorValue == "4" ||
                    selectedOperatorValue == "9" || selectedOperatorValue == "10") {
                    alert("Operator tidak dapat digunakan")
                    ddlOpr.selectedIndex = 0;
                }
            }
        };

        function ddlOperatorConditionFooterIC() {
            var dtgWscParam = document.getElementById("dtgWscCondition");
            var index = GetCurrentInputIndex("dtgWscCondition");
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var ddlOpr = dtgWscParam.rows[index].getElementsByTagName("SELECT")[1];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;
            var selectedOperatorValue = ddlOpr.options[ddlOpr.selectedIndex].value

            if (selectedParameterValue == "0" || selectedParameterValue == "5" || selectedParameterValue == "9" ||
                selectedParameterValue == "10" || selectedParameterValue == "11" || selectedParameterValue == "12" ||
                selectedParameterValue == "15" || selectedParameterValue == "19") {
                if (selectedOperatorValue != "9" && selectedOperatorValue != "10") {
                    alert("Operator tidak dapat digunakan");
                    ddlOpr.selectedIndex = 0;
                }
            }
            else if (selectedParameterValue == "1" || selectedParameterValue == "2" || selectedParameterValue == "3" ||
                selectedParameterValue == "4" || selectedParameterValue == "6" || selectedParameterValue == "7" ||
                selectedParameterValue == "8" || selectedParameterValue == "16" || selectedParameterValue == "18" || selectedParameterValue == "20") {
                if (selectedOperatorValue == "2" || selectedOperatorValue == "3" || selectedOperatorValue == "4" ||
                    selectedOperatorValue == "9" || selectedOperatorValue == "10" ) {
                    alert("Operator tidak dapat digunakan")
                    ddlOpr.selectedIndex = 0;
                }
            }
        };

        function ddlOperatorConditionEditIC(row) {
            var rowData = row.parentNode.parentNode;
            var index = rowData.rowIndex;

            var dtgWscParam = document.getElementById("dtgWscCondition");
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var ddlOpr = dtgWscParam.rows[index].getElementsByTagName("SELECT")[1];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;
            var selectedOperatorValue = ddlOpr.options[ddlOpr.selectedIndex].value

            if (selectedParameterValue == "0" || selectedParameterValue == "5" || selectedParameterValue == "9" ||
                selectedParameterValue == "10" || selectedParameterValue == "11" || selectedParameterValue == "12" ||
                selectedParameterValue == "15" || selectedParameterValue == "19") {
                if (selectedOperatorValue != "9" && selectedOperatorValue != "10") {
                    alert("Operator tidak dapat digunakan");
                    ddlOpr.selectedIndex = 0;
                }
            }
            else if (selectedParameterValue == "1" || selectedParameterValue == "2" || selectedParameterValue == "3" ||
                selectedParameterValue == "4" || selectedParameterValue == "6" || selectedParameterValue == "7" ||
                selectedParameterValue == "8" || selectedParameterValue == "16" || selectedParameterValue == "20") {
                if (selectedOperatorValue == "2" || selectedOperatorValue == "3" || selectedOperatorValue == "4" ||
                    selectedOperatorValue == "9" || selectedOperatorValue == "10" || selectedOperatorValue == "18") {
                    alert("Operator tidak dapat digunakan")
                    ddlOpr.selectedIndex = 0;
                }
            }
        };

        function ddlParameterIC(index, errFlag) {
            var dtgWscParam = document.getElementById("dtgWscParam");
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var ddlOp = dtgWscParam.rows[index].getElementsByTagName("SELECT")[1];
            var icValue = dtgWscParam.rows[index].getElementsByTagName("SPAN")[1];
            var txtValue = dtgWscParam.rows[index].getElementsByTagName("INPUT")[0];
            var txtValueNum = dtgWscParam.rows[index].getElementsByTagName("INPUT")[1];
            var lblSearchChassis = dtgWscParam.rows[index].getElementsByTagName("SPAN")[0];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;
            if (selectedParameterValue == "0" || selectedParameterValue == "5" || selectedParameterValue == "9" ||
                selectedParameterValue == "10" || selectedParameterValue == "11" || selectedParameterValue == "12" || selectedParameterValue == "13" ||
                selectedParameterValue == "15" || selectedParameterValue == "19") {
                //Popup
                txtValue.style.display = 'table-row'
                //txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'table-row'
                icValue.style.display = 'none'
            }
            else if (selectedParameterValue == "1" || selectedParameterValue == "2" || selectedParameterValue == "3" || selectedParameterValue == "18") {
                //Calendar
                txtValue.style.display = 'none'
                txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'table-row'
            }
            else if (selectedParameterValue == "4" || selectedParameterValue == "6" || selectedParameterValue == "7" || selectedParameterValue == "8" ||
                     selectedParameterValue == "16" || selectedParameterValue == "17" || selectedParameterValue == "20") {
                //Freetext Number
                txtValueNum.style.display = 'table-row'
                //txtValue.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'none'
            }
            else {
                txtValue.style.display = 'none'
                txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'none'
            }

            if (!errFlag) {
                ddlOp.selectedIndex = 0
                txtValue.value = ''
                txtValueNum.value = ''
            }
        };

        function ddlConditionIC(index, errFlag) {
            var dtgWscParam = document.getElementById("dtgWscCondition");
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var ddlOp = dtgWscParam.rows[index].getElementsByTagName("SELECT")[1];
            var icValue = dtgWscParam.rows[index].getElementsByTagName("SPAN")[1];
            var txtValue = dtgWscParam.rows[index].getElementsByTagName("INPUT")[0];
            var txtValueNum = dtgWscParam.rows[index].getElementsByTagName("INPUT")[1];
            var lblSearchChassis = dtgWscParam.rows[index].getElementsByTagName("SPAN")[0];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;
            if (selectedParameterValue == "0" || selectedParameterValue == "5" || selectedParameterValue == "9" ||
                selectedParameterValue == "10" || selectedParameterValue == "11" || selectedParameterValue == "12" || selectedParameterValue == "13" ||
                selectedParameterValue == "15" || selectedParameterValue == "19") {
                //Popup
                txtValue.style.display = 'table-row'
                txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'table-row'
                icValue.style.display = 'none'
            }
            else if (selectedParameterValue == "1" || selectedParameterValue == "2" || selectedParameterValue == "3" || selectedParameterValue == "18") {
                //Calendar
                txtValue.style.display = 'none'
                txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'table-row'
            }
            else if (selectedParameterValue == "4" || selectedParameterValue == "6" || selectedParameterValue == "7" || selectedParameterValue == "8" ||
                     selectedParameterValue == "16" || selectedParameterValue == "17" || selectedParameterValue == "20") {
                //Freetext Number
                txtValueNum.style.display = 'table-row'
                txtValue.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'none'
            }
            else {
                txtValue.style.display = 'none'
                txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'none'
            }

            if (!errFlag) {
                ddlOp.selectedIndex = 0
                txtValue.value = ''
                txtValueNum.value = ''
            }
        };

        function GetCurrentInputIndex(GridName) {
            var dtgTakDiUndang = document.getElementById(GridName);
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            index = dtgTakDiUndang.rows.length - 1
            inputs = dtgTakDiUndang.rows[index].getElementsByTagName("INPUT");

            if (inputs != null && inputs.length > 0) {
                for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                    if (inputs[indexInput].type != "hidden" && inputs[indexInput].type != "checkbox")
                        return index;
                }
            }

            return -1;
        };

        function ddlParameterItemIC() {
            ddlParameterIC("Item");
        };

        function ddlParameterFooterIC(row) {
            var rowData = row.parentNode.parentNode;
            var index = rowData.rowIndex;

            ddlParameterIC(index, false);
        };

        function ddlConditionFooterIC(row) {
            var rowData = row.parentNode.parentNode;
            var index = rowData.rowIndex;

            ddlConditionIC(index, false);
        }

        function ddlConditionEditIC(row) {
            var rowData = row.parentNode.parentNode;
            var index = rowData.rowIndex;

            var dtgWscParam = document.getElementById("dtgWscParam");
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var ddlOp = dtgWscParam.rows[index].getElementsByTagName("SELECT")[1];
            var icValue = dtgWscParam.rows[index].getElementsByTagName("SPAN")[1];
            var txtValue = dtgWscParam.rows[index].getElementsByTagName("INPUT")[0];
            var txtValueNum = dtgWscParam.rows[index].getElementsByTagName("INPUT")[1];
            var lblSearchChassis = dtgWscParam.rows[index].getElementsByTagName("SPAN")[0];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;
            if (selectedParameterValue == "0" || selectedParameterValue == "5" || selectedParameterValue == "9" ||
                selectedParameterValue == "10" || selectedParameterValue == "11" || selectedParameterValue == "12" || selectedParameterValue == "13" ||
                selectedParameterValue == "15" || selectedParameterValue == "19") {
                //Popup
                txtValue.style.display = 'table-row'
                //txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'table-row'
                icValue.style.display = 'none'
            }
            else if (selectedParameterValue == "1" || selectedParameterValue == "2" || selectedParameterValue == "3" || selectedParameterValue == "18") {
                //Calendar
                txtValue.style.display = 'none'
                txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'table-row'
            }
            else if (selectedParameterValue == "4" || selectedParameterValue == "6" || selectedParameterValue == "7" || selectedParameterValue == "8" ||
                     selectedParameterValue == "16" || selectedParameterValue == "17" || selectedParameterValue == "20") {
                //Freetext Number
                txtValueNum.style.display = 'table-row'
                txtValue.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'none'
            }
            else {
                txtValue.style.display = 'none'
                txtValueNum.style.display = 'none'
                lblSearchChassis.style.display = 'none'
                icValue.style.display = 'none'
            }
            ddlOp.selectedIndex = 0
            txtValue.value = ''
            txtValueNum.value = ''
        };

        function ShowChassisSelection(row) {
            var rowData = row.parentNode.parentNode.parentNode;
            var index = rowData.rowIndex;
            popupFillIndex = index

            var dtgWscParam = document.getElementById("dtgWscParam");
            var claimTypeValue = document.getElementsByTagName("SPAN")[2].innerText;
            //var claimTypeValue = document.getElementById("SPAN")[3].innerText;
            var txtValue = dtgWscParam.rows[index].getElementsByTagName("INPUT")[0];
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;

            if (selectedParameterValue == "0") {
                //Nomor Buletin
                showPopUp('../PopUp/PopUpWSCParamBuletinNumSelection.aspx', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "5") {
                //Kode Posisi
                showPopUp('../PopUp/PopUpWSCParamKodePosisiSelection.aspx?TypeClaim=' + claimTypeValue, '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "9") {
                //Kode Kerja
                showPopUp('../PopUp/PopUpWSCParamKodeKerjaSelection.aspx', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "10") {
                //Kode Kerusakan A
                showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=A', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "11") {
                //Kode Kerusakan B
                showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=B', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "12") {
                //Kode Kerusakan C
                showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=C', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "13") {
                //Nomor Rangka
                showPopUp('../PopUp/PopUpChassisMasterSelection.aspx?pqrNo=' + txtValue.value, '', 500, 760, getSelectedToValueTB);
                //} else if (selectedParameterValue == "14") {
                //    //Kode Kerusakan B
                //    showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=B', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "15") {
                //Part
                showPopUp('../PopUp/PopUpSparePartSelection.aspx', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "16") {
                //Amount
                showPopUp('../PopUp/PopUpSparePartSelection.aspx', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "19") {
                showPopUp('../PopUp/PopUpMSPExtTypeSelection.aspx', '', 500, 760, getSelectedToValueTB);
            }

        };

        function getSelectedToValueTB(selectedValue) {
            var index = popupFillIndex
            var dtgWscParam = document.getElementById("dtgWscParam");
            var ValueTB = dtgWscParam.rows[index].getElementsByTagName("INPUT")[0];
            ValueTB.value = selectedValue
        };

        function ShowChassisSelectionEdit(row) {
            var rowData = row.parentNode.parentNode.parentNode;
            var index = rowData.rowIndex;
            popupFillIndex = index

            var dtgWscParam = document.getElementById("dtgWscCondition");
            var claimTypeValue = document.getElementsByTagName("SPAN")[2].innerText;
            //var claimTypeValue = document.getElementById("SPAN")[3].innerText;
            var txtValue = dtgWscParam.rows[index].getElementsByTagName("INPUT")[0];
            var ddlParameter = dtgWscParam.rows[index].getElementsByTagName("SELECT")[0];
            var selectedParameterValue = ddlParameter.options[ddlParameter.selectedIndex].value;

            if (selectedParameterValue == "0") {
                //Nomor Buletin
                showPopUp('../PopUp/PopUpWSCParamBuletinNumSelection.aspx', '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "5") {
                //Kode Posisi
                showPopUp('../PopUp/PopUpWSCParamKodePosisiSelection.aspx?TypeClaim=' + claimTypeValue, '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "9") {
                //Kode Kerja
                showPopUp('../PopUp/PopUpWSCParamKodeKerjaSelection.aspx', '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "10") {
                //Kode Kerusakan A
                showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=A', '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "11") {
                //Kode Kerusakan B
                showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=B', '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "12") {
                //Kode Kerusakan C
                showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=C', '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "13") {
                //Nomor Rangka
                showPopUp('../PopUp/PopUpChassisMasterSelection.aspx?pqrNo=' + txtValue.value, '', 500, 760, getSelectedToValueCondTB);
                //} else if (selectedParameterValue == "14") {
                //    //Kode Kerusakan B
                //    showPopUp('../PopUp/PopUpWSCParamKodeKerusakanSelection.aspx?DmgType=B', '', 500, 760, getSelectedToValueTB);
            } else if (selectedParameterValue == "15") {
                //Part
                showPopUp('../PopUp/PopUpSparePartSelection.aspx', '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "16") {
                //Amount
                showPopUp('../PopUp/PopUpSparePartSelection.aspx', '', 500, 760, getSelectedToValueCondTB);
            } else if (selectedParameterValue == "19") {
                showPopUp('../PopUp/PopUpMSPExtTypeSelection.aspx', '', 500, 760, getSelectedToValueCondTB);
            }

        };


        function getSelectedToValueCondTB(selectedValue) {
            var index = popupFillIndex
            var dtgWscParam = document.getElementById("dtgWscCondition");
            var ValueTB = dtgWscParam.rows[index].getElementsByTagName("INPUT")[0];
            ValueTB.value = selectedValue
        };



    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 15px;
        }
        .auto-style2 {
            height: 15px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Waranty Service Claim - Detail Parameter WSC</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="14%">Deskripsi</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Status</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Tipe Claim</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblClaimType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="auto-style1" width="24%" valign="top">Kategori / 
									Tipe Kendaraan</td>
                            <td width="1%" valign="top" class="auto-style2">:</td>
                            <td width="75%" valign="top" class="auto-style2">
                                <asp:ListBox ID="lboxCategory" runat="server" Width="183px" Rows="5"></asp:ListBox>
                                <asp:ListBox ID="lboxVehicleType" runat="server" Width="183px" Rows="5"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: auto; margin-bottom: 20px">
                        Kondisi Data Input WSC<br />
                        <asp:DataGrid ID="dtgWscCondition" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                            CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" PageSize="15">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#29A663" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo0" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Parameter">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="ddlParameterItem" runat="server" Width="250px" OnChange="ddlParameterItemIC()"></asp:DropDownList>--%>
                                        <asp:Label ID="lblParameter0" runat="server" Width="250px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlParameterEditC" runat="server" OnChange="ddlConditionEditIC(this)" Width="250px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlParameterFooter0" runat="server" Width="250px" OnChange="ddlConditionFooterIC(this)"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Operator">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="ddlOperatorItem" runat="server" Width="200px"></asp:DropDownList>--%>
                                        <asp:Label ID="lblOperator0" runat="server" Width="250px" />
                                        <br />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlOperatorEditC" runat="server" OnChange="ddlOperatorConditionEditIC(this)" Width="250px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlOperatorFooter0" runat="server" Width="250px" OnChange="ddlOperatorConditionFooterIC()"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Value">
                                    <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue0" runat="server" Width="200px" />
                                        <br />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValueEditC" Width="200px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtValueEditNumC" onkeypress="return NumericOnlyWith(event,'');" Width="200px" runat="server"></asp:TextBox>
                                        <asp:Label class="hideSpanOnPrint" ID="lblSearchChassisEditC" runat="server">
										    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowChassisSelectionEdit(this);">
                                        </asp:Label>
                                        <span id="spanCalendarC" class="hideSpanOnPrint">
                                            <cc1:inticalendar id="icValueEditC" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                                        </span>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtValueFooter0" Width="200px" runat="server" Style="display:none"></asp:TextBox>
                                        <asp:TextBox ID="txtValueFooterNum0" onkeypress="return NumericOnlyWith(event,'');" Width="200px" runat="server" Style="display:none"></asp:TextBox>
                                        <asp:Label class="hideSpanOnPrint" ID="lblSearchChassisFooter0" runat="server" Style="display:none">
										    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowChassisSelectionEdit(this);">
                                        </asp:Label>
                                        <span id="spanCalendarF0" class="hideSpanOnPrint" Style="display:none">
                                            <cc1:inticalendar id="icValueFooter0" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                                        </span>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Parameter Master">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlParameterMasterEditC" runat="server" OnChange="ddlOperatorFooterIC()" Width="100px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlParameterMaster" runat="server" OnChange="ddlOperatorFooterIC()" Width="100px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblParameterMaster" runat="server" Width="100px" />
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlConditionFunctionEditC" runat="server" OnChange="ddlOperatorFooterIC()" Width="100px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlConditionFunction" runat="server" OnChange="ddlOperatorFooterIC()" Width="100px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFunction" runat="server" Width="100px"></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Aksi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnUbah" CausesValidation="False" runat="server" Text="Ubah" CommandName="Edit">
												<img border="0" src="../images/edit.gif" alt="Ubah"></asp:LinkButton>
                                    
                                        <asp:LinkButton ID="lbtnDelete0" CausesValidation="False" runat="server" Text="Hapus" CommandName="Delete"
                                            ToolTip="Hapus">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnSave" Runat="server" CausesValidation="True" CommandName="Update" tabIndex="40" text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Runat="server" CausesValidation="True" CommandName="Cancel" tabIndex="50" text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnAdd0" CausesValidation="False" runat="server" Text="Tambah" CommandName="Add"
                                            ToolTip="Tambah">
													<img src="../images/add.gif" border="0" alt="Parameter Detail"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                        <br />
                        <br />
                        Validasi Data Input WSC
                        <asp:DataGrid ID="dtgWscParam" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                            CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" HorizontalAlign="Center" PageSize="100">
                            <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle CssClass="titleTableService" Width="4%" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="titleTableService" ForeColor="White" Width="4%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Parameter">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlParameterEditP" runat="server" OnChange="ddlParameterFooterIC(this)" Width="250px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlParameterFooter" runat="server" OnChange="ddlParameterFooterIC(this)" Width="250px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="ddlParameterItem" runat="server" Width="250px" OnChange="ddlParameterItemIC()"></asp:DropDownList>--%>
                                        <asp:Label ID="lblParameter" runat="server" Width="250px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="titleTableService" Width="25%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Operator">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlOperatorEditP" runat="server" OnChange="ddlOperatorFooterIC()" Width="250px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlOperatorFooter" runat="server" OnChange="ddlOperatorFooterIC()" Width="250px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="ddlOperatorItem" runat="server" Width="200px"></asp:DropDownList>--%>
                                        <asp:Label ID="lblOperator" runat="server" Width="250px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="titleTableService" Width="15%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Value">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValueEditP" Width="200px" runat="server" Style=""></asp:TextBox>
                                        <asp:TextBox ID="txtValueEditNumP" onkeypress="return NumericOnlyWith(event,'');" Width="200px" runat="server" Style=""></asp:TextBox>
                                        <asp:Label class="hideSpanOnPrint" ID="lblSearchChassisEditP" runat="server" Style="">
										    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowChassisSelection(this);">
                                        </asp:Label>
                                        <span id="spanCalendarC" class="hideSpanOnPrint" style="">
                                            <cc1:inticalendar id="icValueEditP" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                                        </span>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtValueFooter" runat="server" Style="display: none" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="txtValueFooterNum" runat="server" onkeypress="return NumericOnlyWith(event,'');" Style="display: none" Width="200px"></asp:TextBox>
                                        <asp:Label ID="lblSearchChassisFooter" runat="server" class="hideSpanOnPrint" Style="display: none">
										    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowChassisSelection(this);">
                                        </asp:Label>
                                        <span id="spanCalendarF" class="hideSpanOnPrint" style="display: none">
                                        <cc1:IntiCalendar ID="icValueFooter" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </span>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" runat="server" Width="200px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="titleTableService" Width="30%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kondisi Data Input">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlKondisiDataInputEditP" runat="server" Width="100px" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlKondisiDataInput" runat="server" OnChange="ddlOperatorFooterIC()" Width="100px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKondisiDataInput" runat="server" Width="100px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="titleTableService" Width="7%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Reason Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtReasonCodeEditP" runat="server" Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtReasonCodeFooter" runat="server" Width="100px" Style=""></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReasonCode" runat="server" Width="100px" Style=""/>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="titleTableService" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Aksi">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnSave" Runat="server" CausesValidation="True" CommandName="Update" tabIndex="40" text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Runat="server" CausesValidation="True" CommandName="Cancel" tabIndex="50" text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="False" CommandName="Add" Text="Tambah" ToolTip="Tambah">
													<img src="../images/add.gif" border="0" alt="Parameter Detail"></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnUbah" runat="server" CausesValidation="False" CommandName="Edit" Text="Ubah">
												<img border="0" src="../images/edit.gif" alt="Ubah"></asp:LinkButton>
                                    
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Hapus" ToolTip="Hapus">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="titleTableService" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle BackColor="#29A663" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    <script language="javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
        <p>
        <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan" CausesValidation="False"></asp:Button>
        <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Kembali" Style="margin-left: 5px"></asp:Button>
        </p>
    </form>
    </body>
</html>
