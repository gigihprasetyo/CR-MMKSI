#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPL Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:49:46 AM
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

    Public Class SPLMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPL"
        Private m_UpdateStatement As String = "up_UpdateSPL"
        Private m_RetrieveStatement As String = "up_RetrieveSPL"
        Private m_RetrieveListStatement As String = "up_RetrieveSPLList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPL"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPL As SPL = Nothing
            While dr.Read

                sPL = Me.CreateObject(dr)

            End While

            Return sPL

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPLList As ArrayList = New ArrayList

            While dr.Read
                Dim sPL As SPL = Me.CreateObject(dr)
                sPLList.Add(sPL)
            End While

            Return sPLList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPL As SPL = CType(obj, SPL)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPL.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPL As SPL = CType(obj, SPL)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SPLNumber", DbType.AnsiString, sPL.SPLNumber)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, sPL.DealerName)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sPL.CustomerName)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sPL.Description)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, sPL.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, sPL.ValidTo)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, sPL.Attachment)
            DbCommandWrapper.AddInParameter("@NumOfInstallment", DbType.Int32, sPL.NumOfInstallment)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, sPL.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, sPL.Status)
            DbCommandWrapper.AddInParameter("@IsAutoApprovedDealer", DbType.Int16, sPL.IsAutoApprovedDealer)
            DbCommandWrapper.AddInParameter("@ApprovalStatus", DbType.Int32, sPL.ApprovalStatus)
            DbCommandWrapper.AddInParameter("@FinalApproval", DbType.Int16, sPL.FinalApproval)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, sPL.Comment)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPL.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sPL.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@IsFromDP", DbType.Int16, sPL.IsFromDP)

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

            Dim sPL As SPL = CType(obj, SPL)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPL.ID)
            DbCommandWrapper.AddInParameter("@SPLNumber", DbType.AnsiString, sPL.SPLNumber)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, sPL.DealerName)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sPL.CustomerName)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sPL.Description)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, sPL.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, sPL.ValidTo)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, sPL.Attachment)
            DbCommandWrapper.AddInParameter("@NumOfInstallment", DbType.Int32, sPL.NumOfInstallment)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, sPL.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, sPL.Status)
            DbCommandWrapper.AddInParameter("@IsAutoApprovedDealer", DbType.Int16, sPL.IsAutoApprovedDealer)
            DbCommandWrapper.AddInParameter("@ApprovalStatus", DbType.Int32, sPL.ApprovalStatus)
            DbCommandWrapper.AddInParameter("@FinalApproval", DbType.Int16, sPL.FinalApproval)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, sPL.Comment)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPL.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPL.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@IsFromDP", DbType.Int16, sPL.IsFromDP)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPL

            Dim sPL As SPL = New SPL

            sPL.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPLNumber")) Then sPL.SPLNumber = dr("SPLNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then sPL.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then sPL.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then sPL.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then sPL.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then sPL.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then sPL.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NumOfInstallment")) Then sPL.NumOfInstallment = CType(dr("NumOfInstallment"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDay")) Then sPL.MaxTOPDay = CType(dr("MaxTOPDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPL.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsAutoApprovedDealer")) Then sPL.IsAutoApprovedDealer = CType(dr("IsAutoApprovedDealer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovalStatus")) Then sPL.ApprovalStatus = CType(dr("ApprovalStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FinalApproval")) Then sPL.FinalApproval = CType(dr("FinalApproval"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Comment")) Then sPL.Comment = dr("Comment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPL.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPL.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPL.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPL.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPL.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("IsFromDP")) Then sPL.IsFromDP = CType(dr("IsFromDP"), Short)

            Return sPL

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPL) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPL), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPL).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

