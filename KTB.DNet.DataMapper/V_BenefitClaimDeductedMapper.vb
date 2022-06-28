
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_BenefitClaimDeductedMapper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 07/12/2017 - 5:17:06 PM
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

    Public Class V_BenefitClaimDeductedMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_BenefitClaimDeducted"
        Private m_UpdateStatement As String = "up_UpdateV_BenefitClaimDeducted"
        Private m_RetrieveStatement As String = "up_RetrieveV_BenefitClaimDeducted"
        Private m_RetrieveListStatement As String = "up_RetrieveV_BenefitClaimDeductedList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_BenefitClaimDeducted"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim V_BenefitClaimDeducted As V_BenefitClaimDeducted = Nothing
            While dr.Read

                V_BenefitClaimDeducted = Me.CreateObject(dr)

            End While

            Return V_BenefitClaimDeducted

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim V_BenefitClaimDeductedList As ArrayList = New ArrayList

            While dr.Read
                Dim V_BenefitClaimDeducted As V_BenefitClaimDeducted = Me.CreateObject(dr)
                V_BenefitClaimDeductedList.Add(V_BenefitClaimDeducted)
            End While

            Return V_BenefitClaimDeductedList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_BenefitClaimDeducted As V_BenefitClaimDeducted = CType(obj, V_BenefitClaimDeducted)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_BenefitClaimDeducted.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_BenefitClaimDeducted As V_BenefitClaimDeducted = CType(obj, V_BenefitClaimDeducted)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)

            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, V_BenefitClaimDeducted.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, V_BenefitClaimDeducted.DealerName)
            DbCommandWrapper.AddInParameter("@RegNumberDSF", DbType.AnsiString, V_BenefitClaimDeducted.RegNumberDSF)
            DbCommandWrapper.AddInParameter("@RegNumberDealer", DbType.AnsiString, V_BenefitClaimDeducted.RegNumberDealer)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, V_BenefitClaimDeducted.ChassisNumber)
            DbCommandWrapper.AddInParameter("@DeductedAmount", DbType.Decimal, V_BenefitClaimDeducted.DeductedAmount)
            DbCommandWrapper.AddInParameter("@RemainAmount", DbType.Decimal, V_BenefitClaimDeducted.RemainAmount)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_BenefitClaimDeducted.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_BenefitClaimDeducted.LastUpdateBy)
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

            Dim V_BenefitClaimDeducted As V_BenefitClaimDeducted = CType(obj, V_BenefitClaimDeducted)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_BenefitClaimDeducted.ID)
            
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, V_BenefitClaimDeducted.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, V_BenefitClaimDeducted.DealerName)
            DbCommandWrapper.AddInParameter("@RegNumberDSF", DbType.AnsiString, V_BenefitClaimDeducted.RegNumberDSF)
            DbCommandWrapper.AddInParameter("@RegNumberDealer", DbType.AnsiString, V_BenefitClaimDeducted.RegNumberDealer)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, V_BenefitClaimDeducted.ChassisNumber)
            DbCommandWrapper.AddInParameter("@DeductedAmount", DbType.Decimal, V_BenefitClaimDeducted.DeductedAmount)
            DbCommandWrapper.AddInParameter("@RemainAmount", DbType.Decimal, V_BenefitClaimDeducted.RemainAmount)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_BenefitClaimDeducted.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_BenefitClaimDeducted.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_BenefitClaimDeducted

            Dim V_BenefitClaimDeducted As V_BenefitClaimDeducted = New V_BenefitClaimDeducted

            V_BenefitClaimDeducted.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then V_BenefitClaimDeducted.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then V_BenefitClaimDeducted.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumberDSF")) Then V_BenefitClaimDeducted.RegNumberDSF = dr("RegNumberDSF").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumberDealer")) Then V_BenefitClaimDeducted.RegNumberDealer = dr("RegNumberDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then V_BenefitClaimDeducted.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeductedAmount")) Then V_BenefitClaimDeducted.DeductedAmount = CType(dr("DeductedAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RemainAmount")) Then V_BenefitClaimDeducted.RemainAmount = CType(dr("RemainAmount"), Decimal)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then V_BenefitClaimDeducted.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then V_BenefitClaimDeducted.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then V_BenefitClaimDeducted.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then V_BenefitClaimDeducted.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then V_BenefitClaimDeducted.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return V_BenefitClaimDeducted

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_BenefitClaimDeducted) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_BenefitClaimDeducted), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_BenefitClaimDeducted).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

