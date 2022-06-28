
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_POEstimateHaveBilling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/04/2018 - 14:46:54
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

    Public Class VWI_POEstimateHaveBillingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_POEstimateHaveBilling"
        Private m_UpdateStatement As String = "up_UpdateVWI_POEstimateHaveBilling"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_POEstimateHaveBilling"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_POEstimateHaveBillingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_POEstimateHaveBilling"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_POEstimateHaveBilling As VWI_POEstimateHaveBilling = Nothing
            While dr.Read

                vWI_POEstimateHaveBilling = Me.CreateObject(dr)

            End While

            Return vWI_POEstimateHaveBilling

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_POEstimateHaveBillingList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_POEstimateHaveBilling As VWI_POEstimateHaveBilling = Me.CreateObject(dr)
                vWI_POEstimateHaveBillingList.Add(vWI_POEstimateHaveBilling)
            End While

            Return vWI_POEstimateHaveBillingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_POEstimateHaveBilling As VWI_POEstimateHaveBilling = CType(obj, VWI_POEstimateHaveBilling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_POEstimateHaveBilling.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_POEstimateHaveBilling As VWI_POEstimateHaveBilling = CType(obj, VWI_POEstimateHaveBilling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SparePartPOEstimateID", DbType.Int32, vWI_POEstimateHaveBilling.SparePartPOEstimateID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vWI_POEstimateHaveBilling.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, vWI_POEstimateHaveBilling.SODate)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vWI_POEstimateHaveBilling.PONumber)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, vWI_POEstimateHaveBilling.DMSPRNo)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, vWI_POEstimateHaveBilling.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_POEstimateHaveBilling.DealerCode)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, vWI_POEstimateHaveBilling.DocumentType)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(vWI_POEstimateHaveBilling.SparePartPO))

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

            Dim vWI_POEstimateHaveBilling As VWI_POEstimateHaveBilling = CType(obj, VWI_POEstimateHaveBilling)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_POEstimateHaveBilling.ID)
            DbCommandWrapper.AddInParameter("@SparePartPOEstimateID", DbType.Int32, vWI_POEstimateHaveBilling.SparePartPOEstimateID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vWI_POEstimateHaveBilling.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, vWI_POEstimateHaveBilling.SODate)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vWI_POEstimateHaveBilling.PONumber)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, vWI_POEstimateHaveBilling.DMSPRNo)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, vWI_POEstimateHaveBilling.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_POEstimateHaveBilling.DealerCode)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, vWI_POEstimateHaveBilling.DocumentType)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(vWI_POEstimateHaveBilling.SparePartPO))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_POEstimateHaveBilling

            Dim vWI_POEstimateHaveBilling As VWI_POEstimateHaveBilling = New VWI_POEstimateHaveBilling

            'vWI_POEstimateHaveBilling.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOEstimateID")) Then vWI_POEstimateHaveBilling.SparePartPOEstimateID = CType(dr("SparePartPOEstimateID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then vWI_POEstimateHaveBilling.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then vWI_POEstimateHaveBilling.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then vWI_POEstimateHaveBilling.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then vWI_POEstimateHaveBilling.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then vWI_POEstimateHaveBilling.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_POEstimateHaveBilling.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then vWI_POEstimateHaveBilling.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentValue")) Then vWI_POEstimateHaveBilling.TermOfPaymentValue = CType(dr("TermOfPaymentValue"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentCode")) Then vWI_POEstimateHaveBilling.TermOfPaymentCode = dr("TermOfPaymentCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentDesc")) Then vWI_POEstimateHaveBilling.TermOfPaymentDesc = dr("TermOfPaymentDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_POEstimateHaveBilling.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOID")) Then
                vWI_POEstimateHaveBilling.SparePartPO = New SparePartPO(CType(dr("SparePartPOID"), Integer))
            End If

            Return vWI_POEstimateHaveBilling

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_POEstimateHaveBilling) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_POEstimateHaveBilling), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_POEstimateHaveBilling).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

