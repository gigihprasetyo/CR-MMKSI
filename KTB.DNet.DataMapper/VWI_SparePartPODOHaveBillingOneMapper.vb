#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SparePartPODOHaveBilling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 13/04/2018 - 7:40:23
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

    Public Class VWI_SparePartPODOHaveBillingOneMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPODOHaveBilling"
        Private m_UpdateStatement As String = "up_UpdateSparePartPODOHaveBilling"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPODOHaveBilling"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPODOHaveBillingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPODOHaveBilling"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pODO_HaveBilling As VWI_SparePartPODOHaveBillingOne = Nothing
            While dr.Read

                pODO_HaveBilling = Me.CreateObject(dr)

            End While

            Return pODO_HaveBilling

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pODO_HaveBillingList As ArrayList = New ArrayList

            While dr.Read
                Dim pODO_HaveBilling As VWI_SparePartPODOHaveBillingOne = Me.CreateObject(dr)
                pODO_HaveBillingList.Add(pODO_HaveBilling)
            End While

            Return pODO_HaveBillingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODO_HaveBilling As VWI_SparePartPODOHaveBillingOne = CType(obj, VWI_SparePartPODOHaveBillingOne)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODO_HaveBilling.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODO_HaveBilling As VWI_SparePartPODOHaveBillingOne = CType(obj, VWI_SparePartPODOHaveBillingOne)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SparePartDOID", DbType.Int32, pODO_HaveBilling.SparePartDOID)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, pODO_HaveBilling.DONumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, pODO_HaveBilling.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pODO_HaveBilling.DealerCode)
            DbCommandWrapper.AddInParameter("@DoDate", DbType.DateTime, pODO_HaveBilling.DoDate)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, pODO_HaveBilling.BillingDate)
            DbCommandWrapper.AddInParameter("@ExpeditionNo", DbType.AnsiString, pODO_HaveBilling.ExpeditionNo)
            DbCommandWrapper.AddInParameter("@TermOfPaymentValue", DbType.Int16, pODO_HaveBilling.TermOfPaymentValue)
            DbCommandWrapper.AddInParameter("@TermOfPaymentCode", DbType.AnsiString, pODO_HaveBilling.TermOfPaymentCode)
            DbCommandWrapper.AddInParameter("@TermOfPaymentDesc", DbType.AnsiString, pODO_HaveBilling.TermOfPaymentDesc)
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

            Dim pODO_HaveBilling As VWI_SparePartPODOHaveBillingOne = CType(obj, VWI_SparePartPODOHaveBillingOne)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODO_HaveBilling.ID)
            DbCommandWrapper.AddInParameter("@SparePartDOID", DbType.Int32, pODO_HaveBilling.SparePartDOID)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, pODO_HaveBilling.DONumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, pODO_HaveBilling.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pODO_HaveBilling.DealerCode)
            DbCommandWrapper.AddInParameter("@DoDate", DbType.DateTime, pODO_HaveBilling.DoDate)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, pODO_HaveBilling.BillingDate)
            DbCommandWrapper.AddInParameter("@ExpeditionNo", DbType.AnsiString, pODO_HaveBilling.ExpeditionNo)
            DbCommandWrapper.AddInParameter("@TermOfPaymentValue", DbType.Int16, pODO_HaveBilling.TermOfPaymentValue)
            DbCommandWrapper.AddInParameter("@TermOfPaymentCode", DbType.AnsiString, pODO_HaveBilling.TermOfPaymentCode)
            DbCommandWrapper.AddInParameter("@TermOfPaymentDesc", DbType.AnsiString, pODO_HaveBilling.TermOfPaymentDesc)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SparePartPODOHaveBillingOne

            Dim pODO_HaveBilling As VWI_SparePartPODOHaveBillingOne = New VWI_SparePartPODOHaveBillingOne

            pODO_HaveBilling.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDOID")) Then pODO_HaveBilling.SparePartDOID = CType(dr("SparePartDOID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then pODO_HaveBilling.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then pODO_HaveBilling.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pODO_HaveBilling.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DoDate")) Then pODO_HaveBilling.DoDate = CType(dr("DoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then pODO_HaveBilling.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then pODO_HaveBilling.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then pODO_HaveBilling.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ExpeditionNo")) Then pODO_HaveBilling.ExpeditionNo = dr("ExpeditionNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentValue")) Then pODO_HaveBilling.TermOfPaymentValue = CType(dr("TermOfPaymentValue"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentCode")) Then pODO_HaveBilling.TermOfPaymentCode = dr("TermOfPaymentCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentDesc")) Then pODO_HaveBilling.TermOfPaymentDesc = dr("TermOfPaymentDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pODO_HaveBilling.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return pODO_HaveBilling

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SparePartPODOHaveBillingOne) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SparePartPODOHaveBillingOne), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SparePartPODOHaveBillingOne).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


