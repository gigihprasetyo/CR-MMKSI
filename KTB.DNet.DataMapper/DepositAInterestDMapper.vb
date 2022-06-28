
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositAInterestD Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 11/28/2008 - 2:58:12 PM
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

    Public Class DepositAInterestDMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositAInterestD"
        Private m_UpdateStatement As String = "up_UpdateDepositAInterestD"
        Private m_RetrieveStatement As String = "up_RetrieveDepositAInterestD"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositAInterestDList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositAInterestD"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositAInterestD As DepositAInterestD = Nothing
            While dr.Read

                depositAInterestD = Me.CreateObject(dr)

            End While

            Return depositAInterestD

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositAInterestDList As ArrayList = New ArrayList

            While dr.Read
                Dim depositAInterestD As DepositAInterestD = Me.CreateObject(dr)
                depositAInterestDList.Add(depositAInterestD)
            End While

            Return depositAInterestDList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAInterestD As DepositAInterestD = CType(obj, DepositAInterestD)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAInterestD.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAInterestD As DepositAInterestD = CType(obj, DepositAInterestD)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, depositAInterestD.DealerCode)
			DbCommandWrapper.AddInParameter("@Month",DbType.AnsiString,depositAInterestD.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositAInterestD.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositAInterestD.InterestAmount)
            DBCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositAInterestD.NettoAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAInterestD.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositAInterestD.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@HeaderID", DbType.Int32, Me.GetRefObject(depositAInterestD.DepositAInterestH))

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

            Dim depositAInterestD As DepositAInterestD = CType(obj, DepositAInterestD)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAInterestD.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, depositAInterestD.DealerCode)
			DbCommandWrapper.AddInParameter("@Month",DbType.AnsiString,depositAInterestD.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositAInterestD.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositAInterestD.InterestAmount)
            DBCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositAInterestD.NettoAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAInterestD.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositAInterestD.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@HeaderID", DbType.Int32, Me.GetRefObject(depositAInterestD.DepositAInterestH))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositAInterestD

            Dim depositAInterestD As DepositAInterestD = New DepositAInterestD

            depositAInterestD.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then depositAInterestD.DealerCode = dr("DealerCode").ToString
			if not dr.IsDBNull(dr.GetOrdinal("Month")) then depositAInterestD.Month = dr("Month").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then depositAInterestD.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InterestAmount")) Then depositAInterestD.InterestAmount = CType(dr("InterestAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NettoAmount")) Then depositAInterestD.NettoAmount = CType(dr("NettoAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositAInterestD.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositAInterestD.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositAInterestD.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositAInterestD.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositAInterestD.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("HeaderID")) Then
                depositAInterestD.DepositAInterestH = New DepositAInterestH(CType(dr("HeaderID"), Integer))
            End If

            Return depositAInterestD

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositAInterestD) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositAInterestD), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositAInterestD).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

