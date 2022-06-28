#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitAllocation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2007 - 2:24:04 PM
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

    Public Class BabitAllocationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitAllocation"
        Private m_UpdateStatement As String = "up_UpdateBabitAllocation"
        Private m_RetrieveStatement As String = "up_RetrieveBabitAllocation"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitAllocationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitAllocation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitAllocation As BabitAllocation = Nothing
            While dr.Read

                babitAllocation = Me.CreateObject(dr)

            End While

            Return babitAllocation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitAllocationList As ArrayList = New ArrayList

            While dr.Read
                Dim babitAllocation As BabitAllocation = Me.CreateObject(dr)
                babitAllocationList.Add(babitAllocation)
            End While

            Return babitAllocationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitAllocation As BabitAllocation = CType(obj, BabitAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitAllocation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitAllocation As BabitAllocation = CType(obj, BabitAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PC", DbType.Currency, babitAllocation.PC)
            DbCommandWrapper.AddInParameter("@LCV", DbType.Currency, babitAllocation.LCV)
            DbCommandWrapper.AddInParameter("@CV", DbType.Currency, babitAllocation.CV)
            DbCommandWrapper.AddInParameter("@NoPerjanjian", DbType.AnsiString, babitAllocation.NoPerjanjian)
            DbCommandWrapper.AddInParameter("@ReffNoPerjanjian", DbType.AnsiString, babitAllocation.ReffNoPerjanjian)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, babitAllocation.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitAllocation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitID", DbType.Int32, Me.GetRefObject(babitAllocation.Babit))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitAllocation.Dealer))

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

            Dim babitAllocation As BabitAllocation = CType(obj, BabitAllocation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitAllocation.ID)
            DbCommandWrapper.AddInParameter("@PC", DbType.Currency, babitAllocation.PC)
            DbCommandWrapper.AddInParameter("@LCV", DbType.Currency, babitAllocation.LCV)
            DbCommandWrapper.AddInParameter("@CV", DbType.Currency, babitAllocation.CV)
            DbCommandWrapper.AddInParameter("@NoPerjanjian", DbType.AnsiString, babitAllocation.NoPerjanjian)
            DbCommandWrapper.AddInParameter("@ReffNoPerjanjian", DbType.AnsiString, babitAllocation.ReffNoPerjanjian)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, babitAllocation.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitAllocation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitID", DbType.Int32, Me.GetRefObject(babitAllocation.Babit))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitAllocation.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitAllocation

            Dim babitAllocation As BabitAllocation = New BabitAllocation

            babitAllocation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PC")) Then babitAllocation.PC = CType(dr("PC"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LCV")) Then babitAllocation.LCV = CType(dr("LCV"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("CV")) Then babitAllocation.CV = CType(dr("CV"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NoPerjanjian")) Then babitAllocation.NoPerjanjian = dr("NoPerjanjian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReffNoPerjanjian")) Then babitAllocation.ReffNoPerjanjian = dr("ReffNoPerjanjian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitAllocation.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitAllocation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitAllocation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitAllocation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitAllocation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitAllocation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitID")) Then
                babitAllocation.Babit = New Babit(CType(dr("BabitID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitAllocation.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return babitAllocation

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitAllocation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitAllocation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitAllocation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

