#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper
    Public Class SparePartForecastMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartForecastMaster"
        Private m_UpdateStatement As String = "up_UpdateSparePartForecastMaster"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartForecastMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartForecastMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartForecastMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartForecastMaster As SparePartForecastMaster = Nothing
            While dr.Read

                sparePartForecastMaster = Me.CreateObject(dr)

            End While

            Return sparePartForecastMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartForecastMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartForecastMaster As SparePartForecastMaster = Me.CreateObject(dr)
                sparePartForecastMasterList.Add(sparePartForecastMaster)
            End While

            Return sparePartForecastMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForecastMaster As SparePartForecastMaster = CType(obj, SparePartForecastMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForecastMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForecastMaster As SparePartForecastMaster = CType(obj, SparePartForecastMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@NoRecallCategory", DbType.AnsiString, sparePartForecastMaster.NoReCallCategory)
            DbCommandWrapper.AddInParameter("@NoBulletinService", DbType.AnsiString, sparePartForecastMaster.NoBulletinService)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, sparePartForecastMaster.Stock)
            DbCommandWrapper.AddInParameter("@MaxOrder", DbType.Int32, sparePartForecastMaster.MaxOrder)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForecastMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForecastMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, sparePartForecastMaster.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartForecastMaster.SparePartMaster))
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForecastMaster As SparePartForecastMaster = CType(obj, SparePartForecastMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForecastMaster.ID)
            DbCommandWrapper.AddInParameter("@NoRecallCategory", DbType.AnsiString, sparePartForecastMaster.NoReCallCategory)
            DbCommandWrapper.AddInParameter("@NoBulletinService", DbType.AnsiString, sparePartForecastMaster.NoBulletinService)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, sparePartForecastMaster.Stock)
            DbCommandWrapper.AddInParameter("@MaxOrder", DbType.Int32, sparePartForecastMaster.MaxOrder)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForecastMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForecastMaster.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartForecastMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartForecastMaster.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartForecastMaster

            Dim sparePartForecastMaster As SparePartForecastMaster = New SparePartForecastMaster

            sparePartForecastMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoRecallCategory")) Then sparePartForecastMaster.NoReCallCategory = dr("NoRecallCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoBulletinService")) Then sparePartForecastMaster.NoBulletinService = dr("NoBulletinService").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Stock")) Then sparePartForecastMaster.Stock = CType(dr("Stock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxOrder")) Then sparePartForecastMaster.MaxOrder = CType(dr("MaxOrder"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartForecastMaster.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartForecastMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartForecastMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartForecastMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then sparePartForecastMaster.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then sparePartForecastMaster.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartMasterID")) Then
                sparePartForecastMaster.SparePartMaster = New SparePartMaster(CType(dr("SparepartMasterID"), Integer))
            End If
            Return sparePartForecastMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartForecastMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartForecastMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartForecastMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"
        'Public Function RetrieveSparpartModel(ByVal modelCode As String, Optional ByVal companyCode As String = "") As ArrayList
        '    Dim dr As IDataReader = Nothing
        '    Dim domainObjects As ArrayList = New ArrayList
        '    Dim sqlQuery As String

        '    Dim iProduct As Integer = 0
        '    If companyCode.Trim.ToUpper = "MMC" Then
        '        iProduct = 1
        '    End If
        '    If companyCode.Trim.ToUpper = "MFTBC" Then
        '        iProduct = 2
        '    End If

        '    modelCode = modelCode.Replace(";", "")
        '    modelCode = modelCode.Replace("--", "")
        '    modelCode = modelCode.Replace("'", "")
        '    Try
        '        DbCommandWrapper = GetRetrieveCommand()
        '        If modelCode.Trim = "" Then
        '            sqlQuery = "select distinct(ModelCode) from sparepartmaster "
        '            If iProduct > 0 Then
        '                sqlQuery = sqlQuery & " where ProductCategoryID = " & iProduct
        '            End If
        '        Else
        '            sqlQuery = "select distinct(ModelCode) from sparepartmaster where ModelCode like '%" & modelCode.Trim & "%'"
        '            If iProduct > 0 Then
        '                sqlQuery = sqlQuery & " and ProductCategoryID = " & iProduct
        '            End If
        '        End If

        '        DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
        '        dr = Db.ExecuteReader(DbCommandWrapper)
        '        Dim spModel As SparePartForecastMasterModel
        '        While dr.Read
        '            spModel = New SparePartForecastMasterModel
        '            spModel.ModelCode = CType(dr(0), String)
        '            domainObjects.Add(spModel)
        '        End While
        '    Catch ex As Exception
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
        '        If rethrow Then
        '            Throw
        '        End If
        '    Finally
        '        If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
        '            dr.Close()
        '        End If
        '    End Try
        '    Return domainObjects
        'End Function
#End Region

    End Class

    Public Class SparePartForecastMasterModel
        Private _model As String
        Public Property ModelCode() As String
            Get
                Return _model
            End Get
            Set(ByVal Value As String)
                _model = Value
            End Set
        End Property
    End Class

End Namespace


