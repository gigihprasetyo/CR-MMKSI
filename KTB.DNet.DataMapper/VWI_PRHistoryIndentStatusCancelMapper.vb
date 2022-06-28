
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_PRHistoryIndentStatusCancel Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 08/05/2018 - 8:18:07
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

    Public Class VWI_PRHistoryIndentStatusCancelMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_PRHistoryIndentStatusCancel"
        Private m_UpdateStatement As String = "up_UpdateVWI_PRHistoryIndentStatusCancel"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_PRHistoryIndentStatusCancel"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_PRHistoryIndentStatusCancelList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_PRHistoryIndentStatusCancel"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_PRHistoryIndentStatusCancel As VWI_PRHistoryIndentStatusCancel = Nothing
            While dr.Read

                vWI_PRHistoryIndentStatusCancel = Me.CreateObject(dr)

            End While

            Return vWI_PRHistoryIndentStatusCancel

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_PRHistoryIndentStatusCancelList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_PRHistoryIndentStatusCancel As VWI_PRHistoryIndentStatusCancel = Me.CreateObject(dr)
                vWI_PRHistoryIndentStatusCancelList.Add(vWI_PRHistoryIndentStatusCancel)
            End While

            Return vWI_PRHistoryIndentStatusCancelList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_PRHistoryIndentStatusCancel As VWI_PRHistoryIndentStatusCancel = CType(obj, VWI_PRHistoryIndentStatusCancel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_PRHistoryIndentStatusCancel.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_PRHistoryIndentStatusCancel As VWI_PRHistoryIndentStatusCancel = CType(obj, VWI_PRHistoryIndentStatusCancel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, vWI_PRHistoryIndentStatusCancel.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_PRHistoryIndentStatusCancel.DealerCode)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vWI_PRHistoryIndentStatusCancel.PONumber)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, vWI_PRHistoryIndentStatusCancel.DMSPRNo)
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

            Dim vWI_PRHistoryIndentStatusCancel As VWI_PRHistoryIndentStatusCancel = CType(obj, VWI_PRHistoryIndentStatusCancel)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_PRHistoryIndentStatusCancel.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, vWI_PRHistoryIndentStatusCancel.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_PRHistoryIndentStatusCancel.DealerCode)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vWI_PRHistoryIndentStatusCancel.PONumber)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, vWI_PRHistoryIndentStatusCancel.DMSPRNo)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_PRHistoryIndentStatusCancel

            Dim vWI_PRHistoryIndentStatusCancel As VWI_PRHistoryIndentStatusCancel = New VWI_PRHistoryIndentStatusCancel

            vWI_PRHistoryIndentStatusCancel.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then vWI_PRHistoryIndentStatusCancel.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_PRHistoryIndentStatusCancel.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then vWI_PRHistoryIndentStatusCancel.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then vWI_PRHistoryIndentStatusCancel.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_PRHistoryIndentStatusCancel.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_PRHistoryIndentStatusCancel

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_PRHistoryIndentStatusCancel) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_PRHistoryIndentStatusCancel), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_PRHistoryIndentStatusCancel).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace