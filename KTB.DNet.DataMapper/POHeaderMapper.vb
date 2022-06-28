
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : POHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/20/2009 - 9:46:35 AM
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

    Public Class POHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPOHeader"
        Private m_UpdateStatement As String = "up_UpdatePOHeader"
        Private m_RetrieveStatement As String = "up_RetrievePOHeader"
        Private m_RetrieveListStatement As String = "up_RetrievePOHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePOHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pOHeader As POHeader = Nothing
            While dr.Read

                pOHeader = Me.CreateObject(dr)

            End While

            Return pOHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pOHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim pOHeader As POHeader = Me.CreateObject(dr)
                pOHeaderList.Add(pOHeader)
            End While

            Return pOHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pOHeader As POHeader = CType(obj, POHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pOHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pOHeader As POHeader = CType(obj, POHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, pOHeader.PONumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, pOHeader.Status)
            DbCommandWrapper.AddInParameter("@ReqAllocationDate", DbType.Byte, pOHeader.ReqAllocationDate)
            DbCommandWrapper.AddInParameter("@ReqAllocationMonth", DbType.Byte, pOHeader.ReqAllocationMonth)
            DbCommandWrapper.AddInParameter("@ReqAllocationYear", DbType.Int16, pOHeader.ReqAllocationYear)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, pOHeader.ReqAllocationDateTime)
            DBCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, pOHeader.EffectiveDate)
            DBCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, pOHeader.DealerPONumber)
            DbCommandWrapper.AddInParameter("@POType", DbType.AnsiStringFixedLength, pOHeader.POType)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.Byte, pOHeader.ReleaseDate)
            DbCommandWrapper.AddInParameter("@ReleaseMonth", DbType.Byte, pOHeader.ReleaseMonth)
            DbCommandWrapper.AddInParameter("@ReleaseYear", DbType.Int16, pOHeader.ReleaseYear)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, pOHeader.SONumber)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Byte, pOHeader.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@PassTOP", DbType.Byte, pOHeader.PassTOP)
            DbCommandWrapper.AddInParameter("@LastReqAllocationDateTime", DbType.DateTime, pOHeader.LastReqAllocationDateTime)
            DbCommandWrapper.AddInParameter("@RemarkStatus", DbType.Int16, pOHeader.RemarkStatus)
            DBCommandWrapper.AddInParameter("@DOBlockHistory", DbType.Int16, pOHeader.DOBlockHistory)
            DBCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, pOHeader.Remark)
            DbCommandWrapper.AddInParameter("@ChangedTime", DbType.DateTime, pOHeader.ChangedTime)
            DbCommandWrapper.AddInParameter("@ChangedBy", DbType.AnsiString, pOHeader.ChangedBy)
            DbCommandWrapper.AddInParameter("@BlockedStatus", DbType.Int16, pOHeader.BlockedStatus)
            DBCommandWrapper.AddInParameter("@IsFactoring", DbType.Int16, pOHeader.IsFactoring)
            'DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, pOHeader.SPLID)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Int16, pOHeader.IsTransfer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pOHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pOHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ContractHeaderID", DbType.Int32, Me.GetRefObject(pOHeader.ContractHeader))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(pOHeader.TermOfPayment))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pOHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(pOHeader.SPL))
            DbCommandWrapper.AddInParameter("@PODestinationID", DbType.Int32, Me.GetRefObject(pOHeader.PODestination))

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

            Dim pOHeader As POHeader = CType(obj, POHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pOHeader.ID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, pOHeader.PONumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, pOHeader.Status)
            DbCommandWrapper.AddInParameter("@ReqAllocationDate", DbType.Byte, pOHeader.ReqAllocationDate)
            DbCommandWrapper.AddInParameter("@ReqAllocationMonth", DbType.Byte, pOHeader.ReqAllocationMonth)
            DbCommandWrapper.AddInParameter("@ReqAllocationYear", DbType.Int16, pOHeader.ReqAllocationYear)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, pOHeader.ReqAllocationDateTime)
            DBCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, pOHeader.EffectiveDate)
            DBCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, pOHeader.DealerPONumber)
            DbCommandWrapper.AddInParameter("@POType", DbType.AnsiStringFixedLength, pOHeader.POType)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.Byte, pOHeader.ReleaseDate)
            DbCommandWrapper.AddInParameter("@ReleaseMonth", DbType.Byte, pOHeader.ReleaseMonth)
            DbCommandWrapper.AddInParameter("@ReleaseYear", DbType.Int16, pOHeader.ReleaseYear)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, pOHeader.SONumber)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Byte, pOHeader.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@PassTOP", DbType.Byte, pOHeader.PassTOP)
            DbCommandWrapper.AddInParameter("@LastReqAllocationDateTime", DbType.DateTime, pOHeader.LastReqAllocationDateTime)
            DbCommandWrapper.AddInParameter("@RemarkStatus", DbType.Int16, pOHeader.RemarkStatus)
            DBCommandWrapper.AddInParameter("@DOBlockHistory", DbType.Int16, pOHeader.DOBlockHistory)
            DBCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, pOHeader.Remark)
            DbCommandWrapper.AddInParameter("@ChangedTime", DbType.DateTime, pOHeader.ChangedTime)
            DbCommandWrapper.AddInParameter("@ChangedBy", DbType.AnsiString, pOHeader.ChangedBy)
            DbCommandWrapper.AddInParameter("@BlockedStatus", DbType.Int16, pOHeader.BlockedStatus)
            DbCommandWrapper.AddInParameter("@IsFactoring", DbType.Int16, pOHeader.IsFactoring)
            'DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, pOHeader.SPLID)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Int16, pOHeader.IsTransfer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pOHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pOHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ContractHeaderID", DbType.Int32, Me.GetRefObject(pOHeader.ContractHeader))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(pOHeader.TermOfPayment))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pOHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(pOHeader.SPL))
            DbCommandWrapper.AddInParameter("@PODestinationID", DbType.Int32, Me.GetRefObject(pOHeader.PODestination))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As POHeader

            Dim pOHeader As POHeader = New POHeader

            pOHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then pOHeader.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then pOHeader.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDate")) Then pOHeader.ReqAllocationDate = CType(dr("ReqAllocationDate"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationMonth")) Then pOHeader.ReqAllocationMonth = CType(dr("ReqAllocationMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationYear")) Then pOHeader.ReqAllocationYear = CType(dr("ReqAllocationYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDateTime")) Then pOHeader.ReqAllocationDateTime = CType(dr("ReqAllocationDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then pOHeader.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPONumber")) Then pOHeader.DealerPONumber = dr("DealerPONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("POType")) Then pOHeader.POType = dr("POType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then pOHeader.ReleaseDate = CType(dr("ReleaseDate"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseMonth")) Then pOHeader.ReleaseMonth = CType(dr("ReleaseMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseYear")) Then pOHeader.ReleaseYear = CType(dr("ReleaseYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then pOHeader.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22Indicator")) Then pOHeader.FreePPh22Indicator = CType(dr("FreePPh22Indicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PassTOP")) Then pOHeader.PassTOP = CType(dr("PassTOP"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("LastReqAllocationDateTime")) Then pOHeader.LastReqAllocationDateTime = CType(dr("LastReqAllocationDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RemarkStatus")) Then pOHeader.RemarkStatus = CType(dr("RemarkStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DOBlockHistory")) Then pOHeader.DOBlockHistory = CType(dr("DOBlockHistory"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then pOHeader.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChangedTime")) Then pOHeader.ChangedTime = CType(dr("ChangedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChangedBy")) Then pOHeader.ChangedBy = dr("ChangedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BlockedStatus")) Then pOHeader.BlockedStatus = CType(dr("BlockedStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFactoring")) Then pOHeader.IsFactoring = CType(dr("IsFactoring"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPLID")) Then pOHeader.RowStatus = CType(dr("SPLID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) Then pOHeader.IsTransfer = CType(dr("IsTransfer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pOHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pOHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pOHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pOHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pOHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractHeaderID")) Then
                pOHeader.ContractHeader = New ContractHeader(CType(dr("ContractHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then
                pOHeader.TermOfPayment = New TermOfPayment(CType(dr("TermOfPaymentID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pOHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPLID")) Then
                pOHeader.SPL = New SPL(CType(dr("SPLID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PODestinationID")) Then
                pOHeader.PODestination = New PODestination(CType(dr("PODestinationID"), Integer))
            End If

            Return pOHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(POHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(POHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(POHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

