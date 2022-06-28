@Code
    ViewData("Title") = "SendFile"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@code
    Dim strapi As String = "api/sapupload"

    Dim IsProduction As Boolean = False
    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("LoginMode")

    If str.ToLower().Equals("live") Then
        IsProduction = True
    End If
End Code


@section scripts
    <script type="text/javascript">

        function SubmitData() {



            var vPassword = $('#txtPassword').val();
            var vDoc = $('#jnsFile').val();
            if (vPassword == "") {
                alert("Masukan Password");
                $('#txtPassword').focus();
                return;
            }



            if (vDoc == "") {
                alert("Pilih Jenis Document");
                $('#jnsFile').focus();
                return;
            }


            var files = $("#myFile").get(0).files;
            var fileData = new FormData();

            if (files.length == 0 || files.length == null) {
                alert("Pilih Filenya");
                $('#myFile').focus();
                return;
            }
            console.log(files);
            for (var i = 0; i < files.length; i++) {
                fileData.append("fileInput" + i, files[i]);
            }

            var vapiurl = $('#apiurl').val();

            $.ajax({
                type: "POST",
                Origin: "@ViewBag.IP",
                headers: { 'x-pass-header': vPassword },
                cache: false,

                url: vapiurl,
                dataType: "json",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data

                crossDomain: true,
                data: fileData
            }).done(function (str) {
                console.log(str);
                alert(str);
            });
        }
    </script>
End Section



<div id="body">

    <section class="content-wrapper main-content clear-fix">
        
        <div>
            <fieldset>
                <input type="password" autocomplete="off" placeholder="Password" id="txtPassword" />
            </fieldset>

            <fieldset>
                <label>
                    Brows File
                </label>
                <input name="myFile" type="file" multiple id="myFile" />
            </fieldset>


            <fieldset>
                <select id="jnsFile">
                    <option value=''>Silahkan Pilih Jenis File</option>
                    <option value='DOC-MONTHLY'>Monthly Document</option>
                </select>

            </fieldset>



            <fieldset>
                @If Not IsNothing(ViewBag.IP) Then
                    @<select id="apiurl">
                        <option value="@ViewBag.IP@strapi">@ViewBag.IP@strapi</option>
                    </select>

                End If
                <button id="BtnCoba" onclick="SubmitData();">Upload Data</button>
            </fieldset>
        </div>

        <hr />

    </section>
</div>
