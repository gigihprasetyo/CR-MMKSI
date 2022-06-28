<%@ Page Title="Informasi" Language="vb" AutoEventWireup="false" CodeBehind="Information.aspx.vb" Inherits="KTB.DNet.Indent.Information" MasterPageFile="~/MLogin.Master" %>

<%@ MasterType VirtualPath="~/MLogin.Master" %> 

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="InfoForm">
                <div class="form-horizontal">
                    <h4></h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder runat="server" ID="PHPesan" Visible="true">
                        <div class="control-group">
                            <p>
                                <asp:Label ID="lblOK" runat="server" Visible="true">
                            Terima kasih atas kepercayaan anda dalam memilih dan memesan Mitsubishi Xpander, berikut detail pesanan Anda yang sedang kami proses:</asp:Label>

                                <asp:Label ID="lblFalse" runat="server" Visible="false">
                            Pesanan unit telah dibatalkan per tanggal {0} {1} . Berikut detail data pesanan Anda yang dibatalkan: 


 
                                </asp:Label>
                            </p>
                         
                            <br />


                        </div>
                    </asp:PlaceHolder>

                </div>

            </section>

            <section id="InfoSPK">
                <div class="form-horizontal">

                    <asp:PlaceHolder runat="server" ID="PHSPK" Visible="true">

                        <table style="border: 0; width: 100%; padding: 2px;" class="table">
                            <tr>
                                <td>
                                    <asp:Label runat="server" Style="font-weight: bold">SPK No</asp:Label></td>
                                <td>
                                    <asp:Label runat="server" CssClass="control-label" ID="lblDealerSPKNumber"></asp:Label>
                            </tr>

                            
                            <tr>
                                <td>
                                    <asp:Label runat="server" Style="font-weight: bold">Kode Booking</asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblSPKNumber"></asp:Label>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label runat="server" Style="font-weight: bold">Dealer Pemesanan</asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDealerName"></asp:Label></td>
                            </tr>

                            
			   <tr>
                                <td>
                                    <asp:Label runat="server" Style="font-weight: bold">Nama</asp:Label></td>
                                <td>
                                    <asp:Label runat="server" CssClass="control-label" ID="lblName"></asp:Label>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label runat="server" Style="font-weight: bold">Nomor Handphone</asp:Label></td>
                                <td>
                                    <asp:Label runat="server" CssClass="control-label" ID="lblHP"></asp:Label>
                            </tr>
			    
			    <tr>
                                <td>
                                    <asp:Label runat="server" Style="font-weight: bold" visible="true">Tanggal Pemesanan</asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblOrderDate"  visible="true"></asp:Label>
                            </tr>

                            

                        </table>


                    </asp:PlaceHolder>
                </div>
            </section>


            <asp:PlaceHolder runat="server" ID="phVehcileInfo" Visible="false">
                <section id="InfoVehicle">
                    <div class="form-horizontal">
                        <h4><b>Informasi Kendaraan :</b></h4>
                        <hr />
                        <div class="row">
                            <div class="control-group">
                                <asp:GridView ID="gvVehicle" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>



                                        <asp:TemplateField HeaderText="Tipe Unit">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lblLChassis" runat="server" CommandName="InfoChassis" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.VechileTypeCode") %>'>

                                                    <asp:Label runat="server" ID="lblType" Text='<%# DataBinder.Eval(Container, "DataItem.Tipe") %>'>
                                                 
                                                    </asp:Label>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="ColorIndName" HeaderText="Warna" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                        <asp:BoundField DataField="Teralokasi" HeaderText="Alokasi" Visible="false" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" Visible="true" />

                                    </Columns>



                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </section>
            </asp:PlaceHolder>


            <asp:PlaceHolder runat="server" ID="PHChassis" Visible="false">
                <section id="InfoChasis">
                    <div class="form-horizontal">
                        <h4><b>Status Pesanan Kendaraan :</b>
                        </h4>
                        <hr />
                        <div class="row">
                            <div class="control-group">
                                <asp:GridView ID="GVChassis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:BoundField DataField="Description" HeaderText="Tipe Unit" />
                                        <asp:BoundField DataField="ChassisNumber" HeaderText="No Rangka" />
                                        <%--<asp:BoundField DataField="HandOverDate" HeaderText="Tanggal Serah Terima" />--%>


                                        <asp:TemplateField HeaderText="Tanggal Serah Terima Kendaraan">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="txtHOD">

                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>



                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </section>
            </asp:PlaceHolder>


            <section id="footerinfo">
                <div class="form-horizontal">
                    <h4></h4>
                    <br />

                    <hr />

                    <div>
                        <div class="control-group">
                            <p>
                                <span style="font-weight: bold; font-size: 11px;">
                                    <span style="color: red">*Sebagai informasi, Kode Booking adalah kode registrasi pesanan kendaraan yang tidak bersifat sequential atau berurutan. </span>
                                </span>
                            </p>
                            <p>
                                <span>Jika data di atas ini tidak sesuai mohon menghubungi Dealer tempat anda memesan atau menghubungi Mitsubishi Customer Care 0804-1-300-300 untuk informasi lebih lanjut.
                                </span>

                            </p>






                            <br />


                        </div>
                    </div>

                </div>

            </section>


        </div>


    </div>

</asp:Content>
