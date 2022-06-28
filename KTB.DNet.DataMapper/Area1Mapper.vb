#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : Area1 Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/28/2005 - 6:27:43 PM
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

    Public Class Area1Mapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertArea1"
        Private m_UpdateStatement As String = "up_UpdateArea1"
        Private m_RetrieveStatement As String = "up_RetrieveArea1"
        Private m_RetrieveListStatement As String = "up_RetrieveArea1List"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteArea1"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim area1 As Area1 = Nothing
            While dr.Read

                area1 = Me.CreateObject(dr)

            End While

            Return area1

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim area1List As ArrayList = New ArrayList

            While dr.Read
                Dim area1 As Area1 = Me.CreateObject(dr)
                area1List.Add(area1)
            End While

            Return area1List

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim area1 As Area1 = CType(obj, Area1)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, area1.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim area1 As Area1 = CType(obj, Area1)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@AreaCode", DbType.AnsiString, area1.AreaCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, area1.Description)
            DbCommandWrapper.AddInParameter("@PICSales", DbType.AnsiString, area1.PICSales)
            DbCommandWrapper.AddInParameter("@PICServices", DbType.AnsiString, area1.PICServices)
            DbCommandWrapper.AddInParameter("@PICSpareparts", DbType.AnsiString, area1.PICSpareparts)
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(area1.MainArea))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, area1.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, area1.LastUpdateBy)
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

            Dim area1 As Area1 = CType(obj, Area1)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, area1.ID)
            DbCommandWrapper.AddInParameter("@AreaCode", DbType.AnsiString, area1.AreaCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, area1.Description)
            DbCommandWrapper.AddInParameter("@PICSales", DbType.AnsiString, area1.PICSales)
            DbCommandWrapper.AddInParameter("@PICServices", DbType.AnsiString, area1.PICServices)
            DbCommandWrapper.AddInParameter("@PICSpareparts", DbType.AnsiString, area1.PICSpareparts)
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(area1.MainArea))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, area1.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, area1.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Area1

            Dim area1 As Area1 = New Area1

            area1.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("AreaCode")) Then area1.AreaCode = dr("AreaCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then area1.Description = dr("Description").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("PICSales")) Then area1.PICSales = dr("PICSales").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("PICServices")) Then area1.PICServices = dr("PICServices").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("PICSpareparts")) Then area1.PICSpareparts = dr("PICSpareparts").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then area1.RowStatus = CType(dr("RowStatus"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then area1.CreatedBy = dr("CreatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then area1.CreatedTime = CType(dr("CreatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then area1.LastUpdateBy = dr("LastUpdateBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then area1.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("MainAreaID")) Then
                area1.MainArea = New MainArea(CType(dr("MainAreaID"), Integer))
            End If

            Return area1

        End Function

        Private Sub SetTableName()

            If Not (GetType(Area1) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Area1), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Area1).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

