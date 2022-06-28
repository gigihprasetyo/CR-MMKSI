Namespace KTB.DNet.Utility
    Public Class BinderGrid


        Public Function RetrieveHierarkiDataView(ByVal MasterList As ArrayList, ByVal DetailList As ArrayList, ByVal MasterTable As String, ByVal DetailTable As String, ByVal masterColumn As String, ByVal detailColumn As String) As DataView
            Dim convertMaster As New ArrayListConverter(MasterList)
            Dim dtMaster As DataTable = convertMaster.ToDataTable()
            Dim convertDetails As New ArrayListConverter(DetailList)
            Dim dtDetails As DataTable = convertDetails.ToDataTable()

            dtMaster.TableName = MasterTable
            dtDetails.TableName = DetailTable

            Dim ds As New DataSet
            ds.Tables.Add(dtMaster)
            ds.Tables.Add(dtDetails)

            Dim dc1 As DataColumn = ds.Tables(0).Columns(masterColumn)
            Dim dc2 As DataColumn = ds.Tables(1).Columns(detailColumn)
            Dim dr As DataRelation = New DataRelation("Relation", dc1, dc2, False)
            ds.Relations.Add(dr)

            Dim DV As DataView = ds.Tables(MasterTable).DefaultView
            Return DV

        End Function
    End Class

End Namespace
