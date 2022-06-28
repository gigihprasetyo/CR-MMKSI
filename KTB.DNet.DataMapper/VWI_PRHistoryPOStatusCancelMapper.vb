
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_PRHistoryPOStatusCancel Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/11/2018 - 11:11:31
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

    Public Class VWI_PRHistoryPOStatusCancelMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_PRHistoryPOStatusCancel"
        Private m_UpdateStatement As String = "up_UpdateVWI_PRHistoryPOStatusCancel"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_PRHistoryPOStatusCancel"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_PRHistoryPOStatusCancelList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_PRHistoryPOStatusCancel"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_PRHistoryPOStatusCancel As VWI_PRHistoryPOStatusCancel = Nothing
            While dr.Read

                VWI_PRHistoryPOStatusCancel = Me.CreateObject(dr)

            End While

            Return VWI_PRHistoryPOStatusCancel

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_PRHistoryPOStatusCancelList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_PRHistoryPOStatusCancel As VWI_PRHistoryPOStatusCancel = Me.CreateObject(dr)
                VWI_PRHistoryPOStatusCancelList.Add(VWI_PRHistoryPOStatusCancel)
            End While

            Return VWI_PRHistoryPOStatusCancelList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_PRHistoryPOStatusCancel As VWI_PRHistoryPOStatusCancel = CType(obj, VWI_PRHistoryPOStatusCancel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_PRHistoryPOStatusCancel.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_PRHistoryPOStatusCancel As VWI_PRHistoryPOStatusCancel = CType(obj, VWI_PRHistoryPOStatusCancel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            'DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, VWI_PRHistoryPOStatusCancel.ServiceTypeCode)
            'DbCommandWrapper.AddInParameter("@ServiceTypeDescription", DbType.AnsiString, VWI_PRHistoryPOStatusCancel.ServiceTypeDescription)
            'DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_PRHistoryPOStatusCancel.Status)
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

            Dim VWI_PRHistoryPOStatusCancel As VWI_PRHistoryPOStatusCancel = CType(obj, VWI_PRHistoryPOStatusCancel)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            'DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_PRHistoryPOStatusCancel.ID)
            'DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, VWI_PRHistoryPOStatusCancel.ServiceTypeCode)
            'DbCommandWrapper.AddInParameter("@ServiceTypeDescription", DbType.AnsiString, VWI_PRHistoryPOStatusCancel.ServiceTypeDescription)
            'DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_PRHistoryPOStatusCancel.Status)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_PRHistoryPOStatusCancel

            Dim VWI_PRHistoryPOStatusCancel As VWI_PRHistoryPOStatusCancel = New VWI_PRHistoryPOStatusCancel

            VWI_PRHistoryPOStatusCancel.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then VWI_PRHistoryPOStatusCancel.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_PRHistoryPOStatusCancel.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then VWI_PRHistoryPOStatusCancel.PoNumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then VWI_PRHistoryPOStatusCancel.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNO")) Then VWI_PRHistoryPOStatusCancel.DMSPRNO = dr("DMSPRNO").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_PRHistoryPOStatusCancel.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_PRHistoryPOStatusCancel

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_PRHistoryPOStatusCancel) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_PRHistoryPOStatusCancel), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_PRHistoryPOStatusCancel).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

