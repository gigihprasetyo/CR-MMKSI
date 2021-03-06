#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : NationalEventDetailDate Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/22/2021 - 8:42:47 AM
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

    Public Class NationalEventDetailDateMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertNationalEventDetailDate"
        Private m_UpdateStatement As String = "up_UpdateNationalEventDetailDate"
        Private m_RetrieveStatement As String = "up_RetrieveNationalEventDetailDate"
        Private m_RetrieveListStatement As String = "up_RetrieveNationalEventDetailDateList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteNationalEventDetailDate"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim nationalEventDetailDate As NationalEventDetailDate = Nothing
            While dr.Read

                nationalEventDetailDate = Me.CreateObject(dr)

            End While

            Return nationalEventDetailDate

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim nationalEventDetailDateList As ArrayList = New ArrayList

            While dr.Read
                Dim nationalEventDetailDate As NationalEventDetailDate = Me.CreateObject(dr)
                nationalEventDetailDateList.Add(nationalEventDetailDate)
            End While

            Return nationalEventDetailDateList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEventDetailDate As NationalEventDetailDate = CType(obj, NationalEventDetailDate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEventDetailDate.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEventDetailDate As NationalEventDetailDate = CType(obj, NationalEventDetailDate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ActivityDate", DbType.DateTime, nationalEventDetailDate.ActivityDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEventDetailDate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, nationalEventDetailDate.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NationalEventDetailID", DbType.Int32, Me.GetRefObject(nationalEventDetailDate.NationalEventDetail))

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

            Dim nationalEventDetailDate As NationalEventDetailDate = CType(obj, NationalEventDetailDate)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEventDetailDate.ID)
            DbCommandWrapper.AddInParameter("@ActivityDate", DbType.DateTime, nationalEventDetailDate.ActivityDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEventDetailDate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, nationalEventDetailDate.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NationalEventDetailID", DbType.Int32, Me.GetRefObject(nationalEventDetailDate.NationalEventDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As NationalEventDetailDate

            Dim nationalEventDetailDate As NationalEventDetailDate = New NationalEventDetailDate

            nationalEventDetailDate.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityDate")) Then nationalEventDetailDate.ActivityDate = CType(dr("ActivityDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then nationalEventDetailDate.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then nationalEventDetailDate.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then nationalEventDetailDate.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then nationalEventDetailDate.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then nationalEventDetailDate.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            
            If Not dr.IsDBNull(dr.GetOrdinal("NationalEventDetailID")) Then
                nationalEventDetailDate.NationalEventDetail = New NationalEventDetail(CType(dr("NationalEventDetailID"), Integer))
            End If

            Return nationalEventDetailDate

        End Function

        Private Sub SetTableName()

            If Not (GetType(NationalEventDetailDate) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(NationalEventDetailDate), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(NationalEventDetailDate).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
