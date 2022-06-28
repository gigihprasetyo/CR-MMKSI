Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Configuration
Imports System.Web
Imports System.Web.Caching
Imports KTB.DNET.Menu.AppNavigationSchema
Imports System.ComponentModel
Imports System.IO
Imports System.Xml.Xsl
Imports System.Xml
Imports System.Reflection
Imports System.Text
Imports System.Xml.XPath


<DefaultProperty("Text"), ToolboxData("<{0}:TreeMenu runat=server></{0}:TreeMenu>")> _
Public Class TreeMenu
    Inherits WebControl
    ' Methods
    Private Function BuildMenuDataTable() As DataTable

        If ((Not Me.cache.Item(Me._TreeMenu_DataTable) Is Nothing) AndAlso TypeOf Me.cache.Item(Me._TreeMenu_DataTable) Is DataTable) Then
            Return DirectCast(Me.cache.Item(Me._TreeMenu_DataTable), DataTable)
        End If
        Dim table As DataTable = NavigationHelper.BuildMenuDataTableStructure
        Dim path As String = ConfigurationSettings.AppSettings.Item(Me.XMLPath)

        If ((Not path Is Nothing) AndAlso (path.Trim <> "")) Then
            If path.StartsWith("/") Then
                path = MyBase.ResolveUrl(("~" & path))
            Else
                path = MyBase.ResolveUrl(("~/" & path))
            End If
            path = HttpContext.Current.Server.MapPath(path)

        End If
        Dim dependencies As New CacheDependency(path)
        Dim schema As New AppNavigationSchema
        schema.ReadXml(path)
        Dim row As AppMenuRow
        For Each row In schema.AppMenu.Rows
            Dim row2 As DataRow = table.NewRow
            row2.Item(0) = row.AppMenuId
            row2.Item(1) = row.MenuName
            row2.Item(2) = row.ScreenId
            row2.Item(3) = row.ParentMenuId
            row2.Item(4) = row.SortOrder
            row2.Item(5) = row.AppScreenRow.NavigateUrl
            row2.Item(6) = row.AppScreenRow.QueryString
            row2.Item(7) = row.AppScreenRow.GroupId
            table.Rows.Add(row2)
        Next
        Me.cache.Insert(Me._TreeMenu_DataTable, table, dependencies, cache.NoAbsoluteExpiration, cache.NoSlidingExpiration, CacheItemPriority.High, Nothing)
        Return table
    End Function

    Private Function BuildMenuWithXML(ByVal dataSrc As DataTable) As String
        Dim setx As New DataSet("MenuScreen")
        setx.Tables.Add(dataSrc)
        Dim applicationPath As String = Me.request.ApplicationPath
        Dim strArray As String() = Me.Parent.TemplateSourceDirectory.Split(New Char() {"/"c})
        Dim parameter As String = (MyBase.ResolveUrl("~") & "/images/")
        Dim args As New XsltArgumentList
        args.AddParam("ImageDir", "", parameter)
        Dim xml As String = setx.GetXml
        Dim transform As New XslTransform
        If (Me.mode = TreeMenuMode.Dynamic) Then
            If ((Not Me.cache.Item("TreeMenu_Dynamic_XSL") Is Nothing) AndAlso TypeOf Me.cache.Item("TreeMenu_Dynamic_XSL") Is XslTransform) Then
                transform = DirectCast(Me.cache.Item("TreeMenu_Dynamic_XSL"), XslTransform)
            Else
                Dim stylesheet As New XmlTextReader(Assembly.GetExecutingAssembly.GetManifestResourceStream(Me.GetType(), "TreeMenuXSL_Dynamic.xslt"))
                transform.Load(stylesheet, Nothing, MyBase.GetType.Assembly.Evidence)
                stylesheet.Close()
                Me.cache.Insert("TreeMenu_Dynamic_XSL", transform, Nothing, cache.NoAbsoluteExpiration, cache.NoSlidingExpiration, CacheItemPriority.High, Nothing)
            End If
        ElseIf ((Not Me.cache.Item("TreeMenu_Static_XSL") Is Nothing) AndAlso TypeOf Me.cache.Item("TreeMenu_Static_XSL") Is XslTransform) Then
            transform = DirectCast(Me.cache.Item("TreeMenu_Static_XSL"), XslTransform)
        Else
            '        	Assembly assembly = this.GetType().Assembly;
            'Stream stream =	assembly.GetManifestResourceStream(this.GetType(), "IntiCalendar.js");
            Dim reader2 As New XmlTextReader(Assembly.GetExecutingAssembly.GetManifestResourceStream(Me.GetType(), "TreeMenuXSL_Static.xslt"))
            transform.Load(reader2, Nothing, MyBase.GetType.Assembly.Evidence)
            reader2.Close()
            Me.cache.Insert("TreeMenu_Static_XSL", transform, Nothing, cache.NoAbsoluteExpiration, cache.NoSlidingExpiration, CacheItemPriority.High, Nothing)
        End If
        Dim document As New XmlDocument
        document.LoadXml(xml)
        Dim writer As New StringWriter
        transform.Transform(DirectCast(document, IXPathNavigable), args, DirectCast(writer, TextWriter), Nothing)
        Return writer.GetStringBuilder.ToString
    End Function

    Private Function RegisterJavascript() As String
        Dim builder As New StringBuilder
        builder.Append("<script language=JavaScript>  var op2, menu_tb, menu_url;")
        builder.Append("")
        builder.Append("function menuLeftImageClick(os, url) {")
        builder.Append(ChrW(9) & "var imageDirObj = eval(document.forms[0].elements[""imageDir""]);")
        builder.Append(ChrW(9) & "var imageDir = imageDirObj.value;")
        builder.Append(ChrW(9))
        builder.Append("    var ot = os.parentNode.parentNode.nextSibling.firstChild.nextSibling.firstChild;")
        builder.Append("    ")
        builder.Append("    var lensrc = (os.src.length - 8);")
        builder.Append("    var s = os.src.substr(lensrc, 8);")
        builder.Append("    if (s == ""apse.gif"") {")
        builder.Append("        ot.style.display = ""none"";")
        builder.Append("        os.src = imageDir + ""expand.gif"";")
        builder.Append("    }")
        builder.Append("    if (s == ""pand.gif"") {")
        builder.Append("        ot.style.display = ""block"";")
        builder.Append("        os.src = imageDir + ""collapse.gif"";")
        builder.Append(ChrW(9) & ChrW(9) & "menu_url = url;")
        builder.Append(ChrW(9) & ChrW(9) & "menu_tb = os;")
        builder.Append("    }")
        builder.Append("}")
        builder.Append("")
        builder.Append("function menuClicked(e) {")
        builder.Append(ChrW(9) & "if (typeof op2 != ""undefined"") {")
        builder.Append(ChrW(9) & ChrW(9) & "op2.className = ""menuClicked"";")
        builder.Append(ChrW(9) & "}")
        builder.Append(ChrW(9) & "e.className = ""menuClicked"";")
        builder.Append(ChrW(9) & "op2 = e;")
        builder.Append("}")
        builder.Append(" </script>")
        Return builder.ToString
    End Function

    Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
        Me.request = HttpContext.Current.Request
        Me.cache = HttpContext.Current.Cache
        If (((Me.ScreenId <= 0) AndAlso (Not Me.request.QueryString.Item("screenid") Is Nothing)) AndAlso (Me.request.QueryString.Item("screenid").Trim <> "")) Then
            Me.ScreenId = Integer.Parse(Me.request.QueryString.Item("screenid").Trim)
        End If
        Dim applicationRootPath As String = MyBase.ResolveUrl("~")
        Dim dataSrc As DataTable = New NavigationHelper().GetMenuDataSource(Me.ScreenId, Me.request.QueryString, Me.BuildMenuDataTable, applicationRootPath)
        Dim str2 As String = Me.BuildMenuWithXML(dataSrc)
        output.Write(str2)
        output.Write(Me.RegisterJavascript)
    End Sub

    Public Sub ResetCache()
        Me.cache = HttpContext.Current.Cache
        Me.cache.Remove(Me._TreeMenu_DataTable)
    End Sub


    ' Properties
    <Category("Behavior"), Bindable(True)> _
    Public Property MenuMode As TreeMenuMode
        Get
            Return Me.mode
        End Get
        Set(ByVal value As TreeMenuMode)
            Me.mode = value
        End Set
    End Property

    <Bindable(True), Category("Behavior")> _
    Public Property ScreenId As Integer
        Get
            Return Me._screenId
        End Get
        Set(ByVal value As Integer)
            Me._screenId = value
        End Set
    End Property

    <Category("Behavior"), Bindable(True)> _
    Public Property XMLPath As String
        Get
            Return Me._xmlPath
        End Get
        Set(ByVal value As String)
            Me._xmlPath = value
        End Set
    End Property


    ' Fields
    Private _TreeMenu_DataTable As String = "TreeMenu_DataTable"
    Private cache As Cache
    Private Const CACHE_DYNAMIC_XSL As String = "TreeMenu_Dynamic_XSL"
    Private Const CACHE_STATIC_XSL As String = "TreeMenu_Static_XSL"
    Private mode As TreeMenuMode = TreeMenuMode.[Static]
    Private _xmlPath As String = ""
    Private _screenId As Integer = 0
    Private request As HttpRequest


    Public Sub New()
        Me.mode = TreeMenuMode.Static
        Me._TreeMenu_DataTable = "TreeMenu_DataTable"
    End Sub


   

End Class

Public Enum TreeMenuMode
    ' Fields
    Dynamic = 1
    [Static] = 0
End Enum




