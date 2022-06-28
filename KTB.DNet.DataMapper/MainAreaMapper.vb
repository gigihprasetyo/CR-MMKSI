
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MainArea Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 11/11/2016 - 14:21:07
'//
'// ===========================================================================	
#End Region


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

    Public Class MainAreaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMainArea"
        Private m_UpdateStatement As String = "up_UpdateMainArea"
        Private m_RetrieveStatement As String = "up_RetrieveMainArea"
        Private m_RetrieveListStatement As String = "up_RetrieveMainAreaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMainArea"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mainArea As MainArea = Nothing
            While dr.Read

                mainArea = Me.CreateObject(dr)

            End While

            Return mainArea

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mainAreaList As ArrayList = New ArrayList

            While dr.Read
                Dim mainArea As MainArea = Me.CreateObject(dr)
                mainAreaList.Add(mainArea)
            End While

            Return mainAreaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mainArea As MainArea = CType(obj, MainArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mainArea.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mainArea As MainArea = CType(obj, MainArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AreaCode", DbType.AnsiString, mainArea.AreaCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, mainArea.Description)
            DbCommandWrapper.AddInParameter("@PICSales", DbType.AnsiString, mainArea.PICSales)
            DbCommandWrapper.AddInParameter("@PICServices", DbType.AnsiString, mainArea.PICServices)
            DbCommandWrapper.AddInParameter("@PICSpareparts", DbType.AnsiString, mainArea.PICSpareparts)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mainArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mainArea.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim mainArea As MainArea = CType(obj, MainArea)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mainArea.ID)
            DbCommandWrapper.AddInParameter("@AreaCode", DbType.AnsiString, mainArea.AreaCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, mainArea.Description)
            DbCommandWrapper.AddInParameter("@PICSales", DbType.AnsiString, mainArea.PICSales)
            DbCommandWrapper.AddInParameter("@PICServices", DbType.AnsiString, mainArea.PICServices)
            DbCommandWrapper.AddInParameter("@PICSpareparts", DbType.AnsiString, mainArea.PICSpareparts)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mainArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mainArea.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MainArea

            Dim mainArea As MainArea = New MainArea

            mainArea.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AreaCode")) Then mainArea.AreaCode = dr("AreaCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then mainArea.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PICSales")) Then mainArea.PICSales = dr("PICSales").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PICServices")) Then mainArea.PICServices = dr("PICServices").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PICSpareparts")) Then mainArea.PICSpareparts = dr("PICSpareparts").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mainArea.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mainArea.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mainArea.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mainArea.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mainArea.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return mainArea

        End Function

        Private Sub SetTableName()

            If Not (GetType(MainArea) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MainArea), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MainArea).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

