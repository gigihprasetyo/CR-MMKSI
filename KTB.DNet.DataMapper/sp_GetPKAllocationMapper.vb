#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_GetPKAllocation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 3/16/2009 - 11:57:35 AM
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
    Public Class sp_GetPKAllocationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertsp_GetPKAllocation"
        Private m_UpdateStatement As String = "up_Updatesp_GetPKAllocation"
        Private m_RetrieveStatement As String = "up_Retrievesp_GetPKAllocation"
        Private m_RetrieveListStatement As String = "up_Retrievesp_GetPKAllocationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_GetPKAllocation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_CreditMaster As sp_GetPKAllocation = Nothing
            While dr.Read

                sp_CreditMaster = Me.CreateObject(dr)

            End While

            Return sp_CreditMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_GetPKAllocationList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_GetPKAllocation As sp_GetPKAllocation = Me.CreateObject(dr)
                sp_GetPKAllocationList.Add(sp_GetPKAllocation)
            End While

            Return sp_GetPKAllocationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_GetPKAllocation As sp_GetPKAllocation = CType(obj, sp_GetPKAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_GetPKAllocation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_GetPKAllocation As sp_GetPKAllocation = CType(obj, sp_GetPKAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@VechileModelCode", DbType.AnsiString, sp_GetPKAllocation.VechileModelCode)
            DbCommandWrapper.AddInParameter("@TotalQty", DbType.Int32, sp_GetPKAllocation.TotalQty)
            'DbCommandWrapper.AddInParameter("@FreeDays", DbType.AnsiString, sp_GetPKAllocation.FreeDays)

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

            Dim sp_GetPKAllocation As sp_GetPKAllocation = CType(obj, sp_GetPKAllocation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_GetPKAllocation.ID)
            'DbCommandWrapper.AddInParameter("@VehicleModelCode", DbType.AnsiString, sp_GetPKAllocation.VechileModelCode)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int32, sp_GetPKAllocation.TotalQty)
            'DbCommandWrapper.AddInParameter("@FreeDays", DbType.AnsiString, sp_GetPKAllocation.FreeDays)


            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_GetPKAllocation

            Dim sp_GetPKAllocation As sp_GetPKAllocation = New sp_GetPKAllocation

            sp_GetPKAllocation.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("VechileModelCode")) Then sp_GetPKAllocation.VechileModelCode = dr("VechileModelCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalQty")) Then sp_GetPKAllocation.TotalQty = CType(dr("TotalQty"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("FreeDays")) Then sp_GetPKAllocation.FreeDays = dr("FreeDays").ToString

            Return sp_GetPKAllocation

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_CreditMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_CreditMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(sp_CreditMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region
    End Class

End Namespace


