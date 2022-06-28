
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 11:08:12 AM
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

    Public Class BenefitClaimHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitClaimHeader"
        Private m_UpdateStatement As String = "up_UpdateBenefitClaimHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitClaimHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitClaimHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitClaimHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitClaimHeader As BenefitClaimHeader = Nothing
            While dr.Read

                benefitClaimHeader = Me.CreateObject(dr)

            End While

            Return benefitClaimHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitClaimHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitClaimHeader As BenefitClaimHeader = Me.CreateObject(dr)
                benefitClaimHeaderList.Add(benefitClaimHeader)
            End While

            Return benefitClaimHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimHeader As BenefitClaimHeader = CType(obj, BenefitClaimHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimHeader As BenefitClaimHeader = CType(obj, BenefitClaimHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ClaimRegNo", DbType.String, benefitClaimHeader.ClaimRegNo)
            DbCommandWrapper.AddInParameter("@MMKSINotes", DbType.String, benefitClaimHeader.MMKSINotes)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, benefitClaimHeader.ClaimDate)
            
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.String, benefitClaimHeader.JVNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Int16, benefitClaimHeader.IsTransfer)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitClaimHeader.Status)


            DbCommandWrapper.AddInParameter("@BenefitTypeID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.BenefitType))
            DbCommandWrapper.AddInParameter("@BenefitEventHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.BenefitEventHeader))

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.Dealer))
            DbCommandWrapper.AddInParameter("@LeasingCompanyID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.LeasingCompany))

        

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

            Dim benefitClaimHeader As BenefitClaimHeader = CType(obj, BenefitClaimHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimHeader.ID)
            DbCommandWrapper.AddInParameter("@ClaimRegNo", DbType.String, benefitClaimHeader.ClaimRegNo)
            DbCommandWrapper.AddInParameter("@MMKSINotes", DbType.String, benefitClaimHeader.MMKSINotes)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, benefitClaimHeader.ClaimDate)
           
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.String, benefitClaimHeader.JVNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitClaimHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Int16, benefitClaimHeader.IsTransfer)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitClaimHeader.Status)

            DbCommandWrapper.AddInParameter("@BenefitTypeID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.BenefitType))
            DbCommandWrapper.AddInParameter("@BenefitEventHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.BenefitEventHeader))

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.Dealer))
            DbCommandWrapper.AddInParameter("@LeasingCompanyID", DbType.Int32, Me.GetRefObject(benefitClaimHeader.LeasingCompany))

         

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitClaimHeader

            Dim benefitClaimHeader As BenefitClaimHeader = New BenefitClaimHeader

            benefitClaimHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimRegNo")) Then benefitClaimHeader.ClaimRegNo = dr("ClaimRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MMKSINotes")) Then benefitClaimHeader.MMKSINotes = dr("MMKSINotes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimDate")) Then benefitClaimHeader.ClaimDate = CType(dr("ClaimDate"), DateTime)
            
            If Not dr.IsDBNull(dr.GetOrdinal("JVNumber")) Then benefitClaimHeader.JVNumber = dr("JVNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitClaimHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitClaimHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitClaimHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitClaimHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitClaimHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) Then benefitClaimHeader.IsTransfer = CType(dr("IsTransfer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then benefitClaimHeader.Status = CType(dr("Status"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("BenefitTypeID")) Then
                benefitClaimHeader.BenefitType = New BenefitType(CType(dr("BenefitTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitEventHeaderID")) Then
                benefitClaimHeader.BenefitEventHeader = New BenefitEventHeader(CType(dr("BenefitEventHeaderID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                benefitClaimHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("LeasingCompanyID")) Then
                benefitClaimHeader.LeasingCompany = New LeasingCompany(CType(dr("LeasingCompanyID"), Short))
            End If

         
            Return benefitClaimHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitClaimHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitClaimHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitClaimHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"



#End Region

    End Class
End Namespace

