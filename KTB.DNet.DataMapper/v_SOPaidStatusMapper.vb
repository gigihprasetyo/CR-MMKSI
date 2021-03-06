
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_SOPaidStatus Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 09/08/2016 - 16:42:51
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

    Public Class v_SOPaidStatusMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_SOPaidStatus"
        Private m_UpdateStatement As String = "up_Updatev_SOPaidStatus"
        Private m_RetrieveStatement As String = "up_Retrievev_SOPaidStatus"
        Private m_RetrieveListStatement As String = "up_Retrievev_SOPaidStatusList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_SOPaidStatus"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SOPaidStatus As v_SOPaidStatus = Nothing
            While dr.Read

                v_SOPaidStatus = Me.CreateObject(dr)

            End While

            Return v_SOPaidStatus

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SOPaidStatusList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SOPaidStatus As v_SOPaidStatus = Me.CreateObject(dr)
                v_SOPaidStatusList.Add(v_SOPaidStatus)
            End While

            Return v_SOPaidStatusList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SOPaidStatus As v_SOPaidStatus = CType(obj, v_SOPaidStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SOPaidStatus.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SOPaidStatus As v_SOPaidStatus = CType(obj, v_SOPaidStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, v_SOPaidStatus.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, v_SOPaidStatus.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, v_SOPaidStatus.TotalIT)
            DbCommandWrapper.AddInParameter("@PaymentVH", DbType.Currency, v_SOPaidStatus.PaymentVH)
            DbCommandWrapper.AddInParameter("@PaymentPP", DbType.Currency, v_SOPaidStatus.PaymentPP)
            DbCommandWrapper.AddInParameter("@PaymentIT", DbType.Currency, v_SOPaidStatus.PaymentIT)
            DbCommandWrapper.AddInParameter("@IsFullyPaidVH", DbType.Int32, v_SOPaidStatus.IsFullyPaidVH)
            DbCommandWrapper.AddInParameter("@IsFullyPaidPP", DbType.Int32, v_SOPaidStatus.IsFullyPaidPP)
            DbCommandWrapper.AddInParameter("@IsFullyPaidIT", DbType.Int32, v_SOPaidStatus.IsFullyPaidIT)


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

            Dim v_SOPaidStatus As v_SOPaidStatus = CType(obj, v_SOPaidStatus)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SOPaidStatus.ID)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, v_SOPaidStatus.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, v_SOPaidStatus.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, v_SOPaidStatus.TotalIT)
            DbCommandWrapper.AddInParameter("@PaymentVH", DbType.Currency, v_SOPaidStatus.PaymentVH)
            DbCommandWrapper.AddInParameter("@PaymentPP", DbType.Currency, v_SOPaidStatus.PaymentPP)
            DbCommandWrapper.AddInParameter("@PaymentIT", DbType.Currency, v_SOPaidStatus.PaymentIT)
            DbCommandWrapper.AddInParameter("@IsFullyPaidVH", DbType.Int32, v_SOPaidStatus.IsFullyPaidVH)
            DbCommandWrapper.AddInParameter("@IsFullyPaidPP", DbType.Int32, v_SOPaidStatus.IsFullyPaidPP)
            DbCommandWrapper.AddInParameter("@IsFullyPaidIT", DbType.Int32, v_SOPaidStatus.IsFullyPaidIT)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_SOPaidStatus

            Dim v_SOPaidStatus As v_SOPaidStatus = New v_SOPaidStatus

            v_SOPaidStatus.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVH")) Then v_SOPaidStatus.TotalVH = CType(dr("TotalVH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPP")) Then v_SOPaidStatus.TotalPP = CType(dr("TotalPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalIT")) Then v_SOPaidStatus.TotalIT = CType(dr("TotalIT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentVH")) Then v_SOPaidStatus.PaymentVH = CType(dr("PaymentVH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPP")) Then v_SOPaidStatus.PaymentPP = CType(dr("PaymentPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentIT")) Then v_SOPaidStatus.PaymentIT = CType(dr("PaymentIT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFullyPaidVH")) Then v_SOPaidStatus.IsFullyPaidVH = CType(dr("IsFullyPaidVH"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFullyPaidPP")) Then v_SOPaidStatus.IsFullyPaidPP = CType(dr("IsFullyPaidPP"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFullyPaidIT")) Then v_SOPaidStatus.IsFullyPaidIT = CType(dr("IsFullyPaidIT"), Integer)

            Return v_SOPaidStatus

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_SOPaidStatus) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_SOPaidStatus), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_SOPaidStatus).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

