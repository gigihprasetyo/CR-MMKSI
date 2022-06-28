
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : DMSWOWarrantyClaim Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 30/08/2018 - 12:44:13
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

    Public Class DMSWOWarrantyClaimMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDMSWOWarrantyClaim"
        Private m_UpdateStatement As String = "up_UpdateDMSWOWarrantyClaim"
        Private m_RetrieveStatement As String = "up_RetrieveDMSWOWarrantyClaim"
        Private m_RetrieveListStatement As String = "up_RetrieveDMSWOWarrantyClaimList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDMSWOWarrantyClaim"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dMSWOWarrantyClaim As DMSWOWarrantyClaim = Nothing
            While dr.Read

                dMSWOWarrantyClaim = Me.CreateObject(dr)

            End While

            Return dMSWOWarrantyClaim

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dMSWOWarrantyClaimList As ArrayList = New ArrayList

            While dr.Read
                Dim dMSWOWarrantyClaim As DMSWOWarrantyClaim = Me.CreateObject(dr)
                dMSWOWarrantyClaimList.Add(dMSWOWarrantyClaim)
            End While

            Return dMSWOWarrantyClaimList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dMSWOWarrantyClaim As DMSWOWarrantyClaim = CType(obj, DMSWOWarrantyClaim)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dMSWOWarrantyClaim.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dMSWOWarrantyClaim As DMSWOWarrantyClaim = CType(obj, DMSWOWarrantyClaim)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, dMSWOWarrantyClaim.ChassisNumber)
            DbCommandWrapper.AddInParameter("@isBB", DbType.Boolean, dMSWOWarrantyClaim.isBB)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, dMSWOWarrantyClaim.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@FailureDate", DbType.DateTime, dMSWOWarrantyClaim.FailureDate)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, dMSWOWarrantyClaim.ServiceDate)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, dMSWOWarrantyClaim.Owner)
            DbCommandWrapper.AddInParameter("@Mileage", DbType.Int32, dMSWOWarrantyClaim.Mileage)
            DbCommandWrapper.AddInParameter("@ServiceBuletin", DbType.AnsiString, dMSWOWarrantyClaim.ServiceBuletin)
            DbCommandWrapper.AddInParameter("@Symptoms", DbType.AnsiString, dMSWOWarrantyClaim.Symptoms)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, dMSWOWarrantyClaim.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, dMSWOWarrantyClaim.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, dMSWOWarrantyClaim.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dMSWOWarrantyClaim.RowStatus)
            DbCommandWrapper.AddInParameter("@CreateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dMSWOWarrantyClaim.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(dMSWOWarrantyClaim.DealerBranch))

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

            Dim dMSWOWarrantyClaim As DMSWOWarrantyClaim = CType(obj, DMSWOWarrantyClaim)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dMSWOWarrantyClaim.ID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, dMSWOWarrantyClaim.ChassisNumber)
            DbCommandWrapper.AddInParameter("@isBB", DbType.Boolean, dMSWOWarrantyClaim.isBB)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, dMSWOWarrantyClaim.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@FailureDate", DbType.DateTime, dMSWOWarrantyClaim.FailureDate)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, dMSWOWarrantyClaim.ServiceDate)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, dMSWOWarrantyClaim.Owner)
            DbCommandWrapper.AddInParameter("@Mileage", DbType.Int32, dMSWOWarrantyClaim.Mileage)
            DbCommandWrapper.AddInParameter("@ServiceBuletin", DbType.AnsiString, dMSWOWarrantyClaim.ServiceBuletin)
            DbCommandWrapper.AddInParameter("@Symptoms", DbType.AnsiString, dMSWOWarrantyClaim.Symptoms)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, dMSWOWarrantyClaim.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, dMSWOWarrantyClaim.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, dMSWOWarrantyClaim.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dMSWOWarrantyClaim.RowStatus)
            DbCommandWrapper.AddInParameter("@CreateBy", DbType.AnsiString, dMSWOWarrantyClaim.CreateBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dMSWOWarrantyClaim.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(dMSWOWarrantyClaim.DealerBranch))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DMSWOWarrantyClaim

            Dim dMSWOWarrantyClaim As DMSWOWarrantyClaim = New DMSWOWarrantyClaim

            dMSWOWarrantyClaim.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then dMSWOWarrantyClaim.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("isBB")) Then dMSWOWarrantyClaim.isBB = CType(dr("isBB"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then dMSWOWarrantyClaim.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FailureDate")) Then dMSWOWarrantyClaim.FailureDate = CType(dr("FailureDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then dMSWOWarrantyClaim.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then dMSWOWarrantyClaim.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Mileage")) Then dMSWOWarrantyClaim.Mileage = CType(dr("Mileage"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBuletin")) Then dMSWOWarrantyClaim.ServiceBuletin = dr("ServiceBuletin").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Symptoms")) Then dMSWOWarrantyClaim.Symptoms = dr("Symptoms").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Causes")) Then dMSWOWarrantyClaim.Causes = dr("Causes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Results")) Then dMSWOWarrantyClaim.Results = dr("Results").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then dMSWOWarrantyClaim.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dMSWOWarrantyClaim.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreateBy")) Then dMSWOWarrantyClaim.CreateBy = dr("CreateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dMSWOWarrantyClaim.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dMSWOWarrantyClaim.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dMSWOWarrantyClaim.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dMSWOWarrantyClaim.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                dMSWOWarrantyClaim.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            Return dMSWOWarrantyClaim

        End Function

        Private Sub SetTableName()

            If Not (GetType(DMSWOWarrantyClaim) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DMSWOWarrantyClaim), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DMSWOWarrantyClaim).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

