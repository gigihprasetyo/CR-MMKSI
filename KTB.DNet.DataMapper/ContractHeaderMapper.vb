#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ContractHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 11/2/2006 - 9:06:21 AM
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

    Public Class ContractHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertContractHeader"
        Private m_UpdateStatement As String = "up_UpdateContractHeader"
        Private m_RetrieveStatement As String = "up_RetrieveContractHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveContractHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteContractHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim contractHeader As ContractHeader = Nothing
            While dr.Read

                contractHeader = Me.CreateObject(dr)

            End While

            Return contractHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim contractHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim contractHeader As ContractHeader = Me.CreateObject(dr)
                contractHeaderList.Add(contractHeader)
            End While

            Return contractHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim contractHeader As ContractHeader = CType(obj, ContractHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, contractHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim contractHeader As ContractHeader = CType(obj, ContractHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, contractHeader.ContractNumber)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, contractHeader.PKNumber)
            DbCommandWrapper.AddInParameter("@DealerPKNumber", DbType.AnsiString, contractHeader.DealerPKNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, contractHeader.Status)
            DBCommandWrapper.AddInParameter("@ContractPeriodDay", DbType.Int16, contractHeader.ContractPeriodDay)
            DBCommandWrapper.AddInParameter("@ContractPeriodMonth", DbType.Int16, contractHeader.ContractPeriodMonth)
            DbCommandWrapper.AddInParameter("@ContractPeriodYear", DbType.Int16, contractHeader.ContractPeriodYear)
            DBCommandWrapper.AddInParameter("@PricePeriodDay", DbType.Int16, contractHeader.PricePeriodDay)
            DBCommandWrapper.AddInParameter("@PricePeriodMonth", DbType.Int16, contractHeader.PricePeriodMonth)
            DBCommandWrapper.AddInParameter("@PricePeriodYear", DbType.Int16, contractHeader.PricePeriodYear)
            DBCommandWrapper.AddInParameter("@ContractType", DbType.Int16, contractHeader.ContractType)
            DbCommandWrapper.AddInParameter("@Purpose", DbType.Int16, contractHeader.Purpose)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, contractHeader.ProjectName)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, contractHeader.ProductionYear)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Int32, contractHeader.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@FreePPh22LastUpdateBy", DbType.AnsiString, contractHeader.FreePPh22LastUpdateBy)
            DbCommandWrapper.AddInParameter("@FreePPh22LastUpdateTime", DbType.DateTime, contractHeader.FreePPh22LastUpdateTime)
            DbCommandWrapper.AddInParameter("@SPLNumber", DbType.AnsiString, contractHeader.SPLNumber)
            DbCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Int32, contractHeader.FreeIntIndicator)
            DBCommandWrapper.AddInParameter("@RefContractNumber", DbType.AnsiString, contractHeader.RefContractNumber)
            DBCommandWrapper.AddInParameter("@IsCarriedOver", DbType.Int16, contractHeader.IsCarriedOver)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, contractHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, contractHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(contractHeader.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(contractHeader.Dealer))
            DBCommandWrapper.AddInParameter("@PKHeaderID", DbType.Int32, Me.GetRefObject(contractHeader.PKHeader(True)))

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

            Dim contractHeader As ContractHeader = CType(obj, ContractHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, contractHeader.ID)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, contractHeader.ContractNumber)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, contractHeader.PKNumber)
            DbCommandWrapper.AddInParameter("@DealerPKNumber", DbType.AnsiString, contractHeader.DealerPKNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, contractHeader.Status)
            DBCommandWrapper.AddInParameter("@ContractPeriodDay", DbType.Int16, contractHeader.ContractPeriodDay)
            DBCommandWrapper.AddInParameter("@ContractPeriodMonth", DbType.Int16, contractHeader.ContractPeriodMonth)
            DbCommandWrapper.AddInParameter("@ContractPeriodYear", DbType.Int16, contractHeader.ContractPeriodYear)
            DBCommandWrapper.AddInParameter("@PricePeriodDay", DbType.Int16, contractHeader.PricePeriodDay)
            DBCommandWrapper.AddInParameter("@PricePeriodMonth", DbType.Int16, contractHeader.PricePeriodMonth)
            DBCommandWrapper.AddInParameter("@PricePeriodYear", DbType.Int16, contractHeader.PricePeriodYear)
            DBCommandWrapper.AddInParameter("@ContractType", DbType.Int16, contractHeader.ContractType)
            DbCommandWrapper.AddInParameter("@Purpose", DbType.Int16, contractHeader.Purpose)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, contractHeader.ProjectName)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, contractHeader.ProductionYear)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Int32, contractHeader.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@FreePPh22LastUpdateBy", DbType.AnsiString, contractHeader.FreePPh22LastUpdateBy)
            DbCommandWrapper.AddInParameter("@FreePPh22LastUpdateTime", DbType.DateTime, contractHeader.FreePPh22LastUpdateTime)
            DbCommandWrapper.AddInParameter("@SPLNumber", DbType.AnsiString, contractHeader.SPLNumber)
            DBCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Int32, contractHeader.FreeIntIndicator)
            DBCommandWrapper.AddInParameter("@RefContractNumber", DbType.AnsiString, contractHeader.RefContractNumber)
            DBCommandWrapper.AddInParameter("@IsCarriedOver", DbType.Int16, contractHeader.IsCarriedOver)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, contractHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, contractHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(contractHeader.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(contractHeader.Dealer))
            DBCommandWrapper.AddInParameter("@PKHeaderID", DbType.Int32, Me.GetRefObject(contractHeader.PKHeader(True)))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ContractHeader

            Dim contractHeader As ContractHeader = New ContractHeader

            contractHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractNumber")) Then contractHeader.ContractNumber = dr("ContractNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PKNumber")) Then contractHeader.PKNumber = dr("PKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPKNumber")) Then contractHeader.DealerPKNumber = dr("DealerPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then contractHeader.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContractPeriodDay")) Then contractHeader.ContractPeriodDay = CType(dr("ContractPeriodDay"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractPeriodMonth")) Then contractHeader.ContractPeriodMonth = CType(dr("ContractPeriodMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractPeriodYear")) Then contractHeader.ContractPeriodYear = CType(dr("ContractPeriodYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PricePeriodDay")) Then contractHeader.PricePeriodDay = CType(dr("PricePeriodDay"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PricePeriodMonth")) Then contractHeader.PricePeriodMonth = CType(dr("PricePeriodMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PricePeriodYear")) Then contractHeader.PricePeriodYear = CType(dr("PricePeriodYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractType")) Then contractHeader.ContractType = CType(dr("ContractType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Purpose")) Then contractHeader.Purpose = CType(dr("Purpose"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then contractHeader.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then contractHeader.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22Indicator")) Then contractHeader.FreePPh22Indicator = CType(dr("FreePPh22Indicator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22LastUpdateBy")) Then contractHeader.FreePPh22LastUpdateBy = dr("FreePPh22LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22LastUpdateTime")) Then contractHeader.FreePPh22LastUpdateTime = CType(dr("FreePPh22LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SPLNumber")) Then contractHeader.SPLNumber = dr("SPLNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FreeIntIndicator")) Then contractHeader.FreeIntIndicator = CType(dr("FreeIntIndicator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RefContractNumber")) Then contractHeader.RefContractNumber = CType(dr("RefContractNumber"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCarriedOver")) Then contractHeader.IsCarriedOver = CType(dr("IsCarriedOver"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then contractHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then contractHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then contractHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then contractHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then contractHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                contractHeader.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                contractHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PKHeaderID")) Then
                contractHeader.PKHeader = New PKHeader(CType(dr("PKHeaderID"), Integer))
            End If

            Return contractHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(ContractHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ContractHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ContractHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

