@Code
    ViewData("Title") = "MMKSI Submit Data"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@code
    Dim strapi As String = "api/saplistener/SendData"
    Dim strapi2 As String = "api/saplistenerdsk/SendData"
    Dim strapi3 As String = "api/json/SendData"
    Dim IsProduction As Boolean = False
    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("LoginMode")

    If str.ToLower().Equals("live") Then
        IsProduction = True
    End If
End Code


@section scripts
    <script>
    function SubmitData() {
        var content = $('#idData').val();
        

        var vPassword = $('#txtPassword').val();
        var vJsonKey = $('#idJsonKeyName').val();

        if (vPassword == "") {
            alert("Masukan Password");
            $('#txtPassword').focus();
            return;
        }

        var vapiurl = $('#apiurl').val();
        $.ajax({
            type: "POST",
            Origin:  "@ViewBag.IP",
            headers: { 'x-pass-header': vPassword, 'KEYNAME': vJsonKey },
            cache: false,

            url: vapiurl,


            crossDomain: true,
            data: content
        }).done(function (str) {
            alert('success. Status = ' + str.Status + '. Message = ' + str.Message);
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
                @If Not IsNothing(ViewBag.IP) Then
                    @<select id="apiurl">
                         <option value="@ViewBag.IP@strapi">@ViewBag.IP@strapi</option>
                         <option value="@ViewBag.IP@strapi2">@ViewBag.IP@strapi2</option>
                         <option value="@ViewBag.IP@strapi3">@ViewBag.IP@strapi3</option>
                </select>
                   
                End If
                <button id="BtnCoba" onclick="SubmitData();">Send Data</button>
            </fieldset>
        </div>
        <div>
            <fieldset>
                <textarea id="idJsonKeyName" style="width:30%; height:30px;" placeholder="Hanya digunakan untuk WS JSON"></textarea>
            </fieldset>
            <fieldset>
                <textarea id="idData" style="width:100%; height:150px;">K;TRANSPAYMENT_20161011153924\nH;100011160003;545800002;20161011\nD;1015350211;545800002\n</textarea>
            </fieldset>
        </div>
        <hr />



    </section>
</div>
