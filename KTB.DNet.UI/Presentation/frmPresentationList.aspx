<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmPresentationList.aspx.vb" Inherits=".frmPresentationList" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>FrmPresentationUpload</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js?id=1"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script src="../WebResources/jquery-1.7.2.min.js"></script>
   <%-- <script src="../WebResources/jquery-ui.min.1.7.js"></script>
    <link href="../WebResources/css/jquery-ui.1.7.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        var UrlP = 'Presentation/frmPresentationSlider.aspx?key=77B7CBCD-7E4B-4E3C-AD75-418D6F49BAFD&number=34';
      

        function ShowPopUpForumMember() {
            showPopUp('../PopUp/PopUpForumMember.aspx', '', 500, 760, GetIdMemberSelection);
        }

        function GetIdMemberSelection(selectedUserId) {
            var txtKodeID = document.getElementById("txtIDMember");
            txtKodeID.value = selectedUserId
        }
        function GetUserGroupSelection(selectedUserId) {

        }
        //$(document).ready(function () {
           
        //    //var $dialog2 =   window.parent.$("#dialogWindow2").dialog({
        //    //    autoOpen: false,
        //    //    modal: false,
        //    //    minWidth: 640,
        //    //    //height: 720,
        //    //    width: 720,
        //    //    resizable: "true",
        //    //    position: "top"
        //    //});
        //    //var $dialog = $("#dialogWindow").dialog({
        //    //    autoOpen: false,
        //    //    modal: false,
        //    //    minWidth: 560,
        //    //    height:1024,
        //    //    width: 720,
        //    //    resizable: "true",
        //    //    position: "top"
                
        //    //});

        //    $('#btnTest').click(function (e) {
        //       e.preventDefault();
        //       // $('#dialogWindow').load('../Presentation/frmPresentationSlider.aspx?key=77B7CBCD-7E4B-4E3C-AD75-418D6F49BAFD&number=34');
        //       // window.parent.$('#dialogWindow2').load('../Presentation/frmPresentationSlider.aspx?key=77B7CBCD-7E4B-4E3C-AD75-418D6F49BAFD&number=34');
        //        // $dialog.dialog('open');
        //        var _width = $(window.parent.document).width();
        //        var _height = $(window.parent.document).height();
        //        _width = _width -50;
        //        _height = _height-100;

        //        window.parent.$("#thedialog").attr('src', UrlP);
        //        window.parent.$("#somediv").dialog({
        //            //width: _width,
        //            //height: _height,
        //            width: "auto", height: "auto",
        //            autoOpen: false,
        //            closeOnEscape: true,
        //            modal: true,
        //            position: 'top',
        //            close: function () {
        //              window.parent.$("#thedialog").attr('src', "about:blank");
        //                window.parent.$("#somediv").dialog("destroy");
                       
                     
        //            }
        //        });
        //        var dlg = window.parent.$("#somediv");
        //      //  window.parent.$("#somediv").load(UrlP);
        //        window.parent.$("#somediv").dialog('open');
              
        //        window.parent.$("#thedialog").css("height", (_height) + "px");
        //        window.parent.$("#thedialog").css("width", (_width) + "px");
        //        //window.parent.$(window).resize(function () {
        //        //    window.parent.$('.ui-dialog').css({
        //        //        'width': window.parent.$(window).width(),
        //        //        'height': window.parent.$(window).height(),
        //        //        'left': '0px',
        //        //        'top': '0px'
        //        //    });
        //        //}).resize();

            

        //        return false;

        //    });
        //});
    </script>
</head>

<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" runat="server" mehod="post">
        
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Presentasi &nbsp; - &nbsp;Daftar Presentasi</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="/images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblTitle" runat="server">Nama</asp:Label></td>
                            <td>:</td>
                            <td colspan="3">
                                <asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event);" ID="txtTitle" onblur="alphaNumericPlusSpaceBlur(txtTitle);"
                                    runat="server" size="65" MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Deskripsi</td>
                            <td>:</td>
                            <td colspan="3">
                                <asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event);" ID="txtDescription" onblur="alphaNumericPlusSpaceBlur(txtDescription);"
                                    runat="server" size="65" MaxLength="255" />
                            </td>
                        </tr>


                        <tr id="tdTglUpload" runat="server" visible="true">
                            <td class="titleField"  >  <label for="chkTgl">Tanggal Upload</label>   </td>
                            <td>:</td>
                            <td colspan="3" class="titleField">
                                <table border="0" style="margin: 0; padding: 0;">
                                    <tr>
                                         <td style="margin:0 auto; padding:0;">
                                            <asp:CheckBox ID="chkTgl" runat="server" /></td>
                                        <td>&nbsp;&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>

                        </tr>


                        <tr id="tdStatus" runat="server" visible="true">
                            <td class="titleField">Status</td>
                            <td>:</td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="1">Aktif</asp:ListItem>
                                    <asp:ListItem Value="0">Non Aktif</asp:ListItem>

                                </asp:DropDownList>
                            </td>
                        </tr>


                        <tr style="display:none;">
                            <td class="titleField" ></td>
                            <td></td>
                            <td colspan="3">
                                <button id="btnTest">Click Me</button>

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td colspan="3">
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px"></asp:Button>&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 240px">
                        <asp:DataGrid ID="dtgPresentation" runat="server" Width="100%" Visible="False" AllowSorting="True"
                            AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" AllowPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                            type="CheckBox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" ViewStateMode="Enabled"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFile" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UniqueName") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="Title" HeaderText="Nama">
                                    <HeaderStyle Width="20%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserGroupCode" Text='<%# DataBinder.Eval(Container, "DataItem.Title") %>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
                                    <HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserDescription" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' runat="server">
                                                
                                        </asp:Label>
                                        <asp:Label Visible="false" ID="lblUniq" Text='<%# DataBinder.Eval(Container, "DataItem.UniqueName") %>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status" Visible="false">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn Visible="False" DataField="Uploader" HeaderText="DiUpload Oleh" SortExpression="CreatedBy">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn Visible="True" DataField="CreatedTime" HeaderText="Tanggal Upload" DataFormatString="{0:dd/MM/yyyy}" SortExpression="CreatedTime">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn Visible="False" DataField="Updater" HeaderText="Diubah Oleh" SortExpression="LastUpdateBy">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn Visible="False" DataField="LastUpdateTime" HeaderText="Tanggal Ubah" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LastUpdateTime">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                </asp:BoundColumn>





                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat Presentasi"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDownload" runat="server" Width="20px" Text="Download" CausesValidation="False" CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Ubah"></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 11px">
                    <asp:Button ID="btnDeleteUserGroup" runat="server" Text="Delete UserGroup" Visible="false" OnClientClick="SetSource(this.id)"></asp:Button>
                    <asp:HiddenField ID="CustomHiddenField" runat="server" />
                </td>
            </tr>
        </table>

         
          <asp:HiddenField ID="txtDownload" runat="server" />
    </form>

    <script type="text/javascript" language="javascript">
       
     

        if (document.getElementById("txtDownload").value != "") {
           
                var downloadURL = document.getElementById("txtDownload").value;
               
                document.getElementById("txtDownload").value = "";

            try {
             
                showPopUpPPT(downloadURL, '', $(window.parent.document).height(), $(window.parent.document).width());
            } catch (e) {
                alert(e.message);
            }
                
            }

      

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
    <div id="dialogWindow"></div>
</body>
</html>
