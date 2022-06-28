#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : Area2 Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/28/2005 - 6:28:12 PM
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

    Public Class Area2Mapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertArea2"
        Private m_UpdateStatement As String = "up_UpdateArea2"
        Private m_RetrieveStatement As String = "up_RetrieveArea2"
        Private m_RetrieveListStatement As String = "up_RetrieveArea2List"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteArea2"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim area2 As Area2 = Nothing
            While dr.Read

                area2 = Me.CreateObject(dr)

            End While

            Return area2

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim area2List As ArrayList = New ArrayList

            While dr.Read
                Dim area2 As Area2 = Me.CreateObject(dr)
                area2List.Add(area2)
            End While

            Return area2List

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim area2 As Area2 = CType(obj, Area2)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, area2.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim area2 As Area2 = CType(obj, Area2)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AreaCode", DbType.AnsiString, area2.AreaCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, area2.Description)
            DbCommandWrapper.AddInParameter("@ACFinishUnit", DbType.AnsiString, area2.ACFinishUnit)
            DbCommandWrapper.AddInParameter("@ACSparePart", DbType.AnsiString, area2.ACSparePart)
            DbCommandWrapper.AddInParameter("@ACService", DbType.AnsiString, area2.ACService)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, area2.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, area2.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(area2.Area1))

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

            Dim area2 As Area2 = CType(obj, Area2)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, area2.ID)
            DbCommandWrapper.AddInParameter("@AreaCode", DbType.AnsiString, area2.AreaCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, area2.Description)
            DbCommandWrapper.AddInParameter("@ACFinishUnit", DbType.AnsiString, area2.ACFinishUnit)
            DbCommandWrapper.AddInParameter("@ACSparePart", DbType.AnsiString, area2.ACSparePart)
            DbCommandWrapper.AddInParameter("@ACService", DbType.AnsiString, area2.ACService)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, area2.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, area2.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(area2.Area1))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Area2

            Dim area2 As Area2 = New Area2

            area2.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AreaCode")) Then area2.AreaCode = dr("AreaCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then area2.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ACFinishUnit")) Then area2.ACFinishUnit = dr("ACFinishUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ACSparePart")) Then area2.ACSparePart = dr("ACSparePart").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ACService")) Then area2.ACService = dr("ACService").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then area2.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then area2.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then area2.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then area2.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then area2.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Area1ID")) Then
                area2.Area1 = New Area1(CType(dr("Area1ID"), Integer))
            End If

            Return area2

        End Function

        Private Sub SetTableName()

            If Not (GetType(Area2) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Area2), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Area2).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

