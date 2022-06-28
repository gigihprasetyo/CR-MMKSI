
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FreePPh Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/13/2009 - 11:25:34 AM
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

    Public Class FreePPhMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFreePPh"
        Private m_UpdateStatement As String = "up_UpdateFreePPh"
        Private m_RetrieveStatement As String = "up_RetrieveFreePPh"
        Private m_RetrieveListStatement As String = "up_RetrieveFreePPhList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFreePPh"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim freePPh As FreePPh = Nothing
            While dr.Read

                freePPh = Me.CreateObject(dr)

            End While

            Return freePPh

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim freePPhList As ArrayList = New ArrayList

            While dr.Read
                Dim freePPh As FreePPh = Me.CreateObject(dr)
                freePPhList.Add(freePPh)
            End While

            Return freePPhList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim freePPh As FreePPh = CType(obj, FreePPh)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, freePPh.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim freePPh As FreePPh = CType(obj, FreePPh)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, freePPh.DealerID)
            DbCommandWrapper.AddInParameter("@LetterNumber", DbType.AnsiString, freePPh.LetterNumber)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, freePPh.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, freePPh.PeriodEnd)
            DbCommandWrapper.AddInParameter("@KTBApprovalBy", DbType.AnsiString, freePPh.KTBApprovalBy)
            DbCommandWrapper.AddInParameter("@KTBApprovalDate", DbType.DateTime, freePPh.KTBApprovalDate)
            DbCommandWrapper.AddInParameter("@ProposedBy", DbType.AnsiString, freePPh.ProposedBy)
            DbCommandWrapper.AddInParameter("@ProposedDate", DbType.DateTime, freePPh.ProposedDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, freePPh.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, freePPh.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, freePPh.LastUpdateBy)
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

            Dim freePPh As FreePPh = CType(obj, FreePPh)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, freePPh.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, freePPh.DealerID)
            DbCommandWrapper.AddInParameter("@LetterNumber", DbType.AnsiString, freePPh.LetterNumber)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, freePPh.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, freePPh.PeriodEnd)
            DbCommandWrapper.AddInParameter("@KTBApprovalBy", DbType.AnsiString, freePPh.KTBApprovalBy)
            DbCommandWrapper.AddInParameter("@KTBApprovalDate", DbType.DateTime, freePPh.KTBApprovalDate)
            DbCommandWrapper.AddInParameter("@ProposedBy", DbType.AnsiString, freePPh.ProposedBy)
            DbCommandWrapper.AddInParameter("@ProposedDate", DbType.DateTime, freePPh.ProposedDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, freePPh.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, freePPh.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, freePPh.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FreePPh

            Dim freePPh As FreePPh = New FreePPh

            freePPh.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then freePPh.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LetterNumber")) Then freePPh.LetterNumber = dr("LetterNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then freePPh.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then freePPh.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBApprovalBy")) Then freePPh.KTBApprovalBy = dr("KTBApprovalBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KTBApprovalDate")) Then freePPh.KTBApprovalDate = CType(dr("KTBApprovalDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProposedBy")) Then freePPh.ProposedBy = dr("ProposedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProposedDate")) Then freePPh.ProposedDate = CType(dr("ProposedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then freePPh.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then freePPh.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then freePPh.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then freePPh.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then freePPh.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then freePPh.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return freePPh

        End Function

        Private Sub SetTableName()

            If Not (GetType(FreePPh) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FreePPh), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FreePPh).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

