
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitEventHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 11:11:19 AM
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

    Public Class BenefitEventHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitEventHeader"
        Private m_UpdateStatement As String = "up_UpdateBenefitEventHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitEventHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitEventHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitEventHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitEventHeader As BenefitEventHeader = Nothing
            While dr.Read

                benefitEventHeader = Me.CreateObject(dr)

            End While

            Return benefitEventHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitEventHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitEventHeader As BenefitEventHeader = Me.CreateObject(dr)
                benefitEventHeaderList.Add(benefitEventHeader)
            End While

            Return benefitEventHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitEventHeader As BenefitEventHeader = CType(obj, BenefitEventHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitEventHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitEventHeader As BenefitEventHeader = CType(obj, BenefitEventHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@EventRegNo", DbType.AnsiString, benefitEventHeader.EventRegNo)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, benefitEventHeader.EventName)
            DbCommandWrapper.AddInParameter("@EventDate", DbType.DateTime, benefitEventHeader.EventDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitEventHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitEventHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitEventHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID", DbType.Int32, Me.GetRefObject(benefitEventHeader.BenefitMasterHeader))

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(benefitEventHeader.Dealer))

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

            Dim benefitEventHeader As BenefitEventHeader = CType(obj, BenefitEventHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitEventHeader.ID)
            DbCommandWrapper.AddInParameter("@EventRegNo", DbType.AnsiString, benefitEventHeader.EventRegNo)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, benefitEventHeader.EventName)
            DbCommandWrapper.AddInParameter("@EventDate", DbType.DateTime, benefitEventHeader.EventDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitEventHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitEventHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitEventHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID", DbType.Int32, Me.GetRefObject(benefitEventHeader.BenefitMasterHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(benefitEventHeader.Dealer))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitEventHeader

            Dim benefitEventHeader As BenefitEventHeader = New BenefitEventHeader

            benefitEventHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventRegNo")) Then benefitEventHeader.EventRegNo = dr("EventRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventName")) Then benefitEventHeader.EventName = dr("EventName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventDate")) Then benefitEventHeader.EventDate = CType(dr("EventDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then benefitEventHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitEventHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitEventHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitEventHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitEventHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitEventHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitMasterHeaderID")) Then
                benefitEventHeader.BenefitMasterHeader = New BenefitMasterHeader(CType(dr("BenefitMasterHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                benefitEventHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return benefitEventHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitEventHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitEventHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitEventHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

