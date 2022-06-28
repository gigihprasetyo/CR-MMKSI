
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPKTersedia Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 5/13/2011 - 9:45:50 AM
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

    Public Class V_SPKTersediaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPK_Tersedia"
        Private m_UpdateStatement As String = "up_UpdateSPK_Tersedia"
        Private m_RetrieveStatement As String = "up_RetrieveSPK_Tersedia"
        Private m_RetrieveListStatement As String = "up_RetrieveSPK_TersediaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPK_Tersedia"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPK_Tersedia As V_SPKTersedia = Nothing
            While dr.Read

                sPK_Tersedia = Me.CreateObject(dr)

            End While

            Return sPK_Tersedia

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPK_TersediaList As ArrayList = New ArrayList

            While dr.Read
                Dim sPK_Tersedia As V_SPKTersedia = Me.CreateObject(dr)
                sPK_TersediaList.Add(sPK_Tersedia)
            End While

            Return sPK_TersediaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPK_Tersedia As V_SPKTersedia = CType(obj, V_SPKTersedia)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, sPK_Tersedia.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPK_Tersedia As V_SPKTersedia = CType(obj, V_SPKTersedia)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, sPK_Tersedia.DealerID)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sPK_Tersedia.Status)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, sPK_Tersedia.SPKNumber)
            DBCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, sPK_Tersedia.DealerSPKNumber)
            DBCommandWrapper.AddInParameter("@PlanDeliveryMonth", DbType.Byte, sPK_Tersedia.PlanDeliveryMonth)
            DBCommandWrapper.AddInParameter("@PlanDeliveryYear", DbType.Int16, sPK_Tersedia.PlanDeliveryYear)
            DBCommandWrapper.AddInParameter("@PlanDeliveryDate", DbType.DateTime, sPK_Tersedia.PlanDeliveryDate)
            DBCommandWrapper.AddInParameter("@PlanInvoiceMonth", DbType.Byte, sPK_Tersedia.PlanInvoiceMonth)
            DBCommandWrapper.AddInParameter("@PlanInvoiceYear", DbType.Int16, sPK_Tersedia.PlanInvoiceYear)
            DBCommandWrapper.AddInParameter("@PlanInvoiceDate", DbType.DateTime, sPK_Tersedia.PlanInvoiceDate)
            DBCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, sPK_Tersedia.CustomerRequestID)
            'DBCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, sPK_Tersedia.SPKCustomerID)
            DBCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, sPK_Tersedia.ValidateTime)
            DBCommandWrapper.AddInParameter("@ValidateBy", DbType.String, sPK_Tersedia.ValidateBy)
            DBCommandWrapper.AddInParameter("@RejectedReason", DbType.String, sPK_Tersedia.RejectedReason)
            'DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, sPK_Tersedia.SalesmanHeaderID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPK_Tersedia.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, sPK_Tersedia.LastUpdateBy)
            DBCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPK_Tersedia.Quantity)
            DBCommandWrapper.AddInParameter("@Faktur", DbType.Int32, sPK_Tersedia.Faktur)

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPK_Tersedia.Dealer))
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sPK_Tersedia.SalesmanHeader))
            DBCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(sPK_Tersedia.SPKCustomer))


            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPK_Tersedia As V_SPKTersedia = CType(obj, V_SPKTersedia)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, sPK_Tersedia.ID)
            'DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, sPK_Tersedia.DealerID)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sPK_Tersedia.Status)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, sPK_Tersedia.SPKNumber)
            DBCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, sPK_Tersedia.DealerSPKNumber)
            DBCommandWrapper.AddInParameter("@PlanDeliveryMonth", DbType.Byte, sPK_Tersedia.PlanDeliveryMonth)
            DBCommandWrapper.AddInParameter("@PlanDeliveryYear", DbType.Int16, sPK_Tersedia.PlanDeliveryYear)
            DBCommandWrapper.AddInParameter("@PlanDeliveryDate", DbType.DateTime, sPK_Tersedia.PlanDeliveryDate)
            DBCommandWrapper.AddInParameter("@PlanInvoiceMonth", DbType.Byte, sPK_Tersedia.PlanInvoiceMonth)
            DBCommandWrapper.AddInParameter("@PlanInvoiceYear", DbType.Int16, sPK_Tersedia.PlanInvoiceYear)
            DBCommandWrapper.AddInParameter("@PlanInvoiceDate", DbType.DateTime, sPK_Tersedia.PlanInvoiceDate)
            DBCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, sPK_Tersedia.CustomerRequestID)
            'DBCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, sPK_Tersedia.SPKCustomerID)
            DBCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, sPK_Tersedia.ValidateTime)
            DBCommandWrapper.AddInParameter("@ValidateBy", DbType.String, sPK_Tersedia.ValidateBy)
            DBCommandWrapper.AddInParameter("@RejectedReason", DbType.String, sPK_Tersedia.RejectedReason)
            'DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, sPK_Tersedia.SalesmanHeaderID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPK_Tersedia.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.String, sPK_Tersedia.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)
            DBCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPK_Tersedia.Quantity)
            DBCommandWrapper.AddInParameter("@Faktur", DbType.Int32, sPK_Tersedia.Faktur)

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPK_Tersedia.Dealer))
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sPK_Tersedia.SalesmanHeader))
            DBCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(sPK_Tersedia.SPKCustomer))


            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SPKTersedia

            Dim sPK_Tersedia As V_SPKTersedia = New V_SPKTersedia

            sPK_Tersedia.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then sPK_Tersedia.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPK_Tersedia.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then sPK_Tersedia.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) Then sPK_Tersedia.DealerSPKNumber = dr("DealerSPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryMonth")) Then sPK_Tersedia.PlanDeliveryMonth = CType(dr("PlanDeliveryMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryYear")) Then sPK_Tersedia.PlanDeliveryYear = CType(dr("PlanDeliveryYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryDate")) Then sPK_Tersedia.PlanDeliveryDate = CType(dr("PlanDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceMonth")) Then sPK_Tersedia.PlanInvoiceMonth = CType(dr("PlanInvoiceMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceYear")) Then sPK_Tersedia.PlanInvoiceYear = CType(dr("PlanInvoiceYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceDate")) Then sPK_Tersedia.PlanInvoiceDate = CType(dr("PlanInvoiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerRequestID")) Then sPK_Tersedia.CustomerRequestID = CType(dr("CustomerRequestID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomerID")) Then sPK_Tersedia.SPKCustomerID = CType(dr("SPKCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then sPK_Tersedia.ValidateTime = CType(dr("ValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateBy")) Then sPK_Tersedia.ValidateBy = dr("ValidateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReason")) Then sPK_Tersedia.RejectedReason = dr("RejectedReason").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then sPK_Tersedia.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Int32)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPK_Tersedia.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPK_Tersedia.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPK_Tersedia.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPK_Tersedia.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPK_Tersedia.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then sPK_Tersedia.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Faktur")) Then sPK_Tersedia.Faktur = CType(dr("Faktur"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sPK_Tersedia.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                sPK_Tersedia.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomerID")) Then
                sPK_Tersedia.SPKCustomer = New SPKCustomer(CType(dr("SPKCustomerID"), Integer))
            End If

            Return sPK_Tersedia

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SPKTersedia) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SPKTersedia), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SPKTersedia).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

