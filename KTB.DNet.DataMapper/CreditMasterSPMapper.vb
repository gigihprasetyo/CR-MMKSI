
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CreditMasterSP Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2014 
'// ---------------------
'// $History      : $
'// Generated on 9/9/2014 - 8:59:57 AM
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

    Public Class CreditMasterSPMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCreditMasterSP"
        Private m_UpdateStatement As String = "up_UpdateCreditMasterSP"
        Private m_RetrieveStatement As String = "up_RetrieveCreditMasterSP"
        Private m_RetrieveListStatement As String = "up_RetrieveCreditMasterSPList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCreditMasterSP"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim creditMasterSP As CreditMasterSP = Nothing
            While dr.Read

                creditMasterSP = Me.CreateObject(dr)

            End While

            Return creditMasterSP

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim creditMasterSPList As ArrayList = New ArrayList

            While dr.Read
                Dim creditMasterSP As CreditMasterSP = Me.CreateObject(dr)
                creditMasterSPList.Add(creditMasterSP)
            End While

            Return creditMasterSPList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim creditMasterSP As CreditMasterSP = CType(obj, CreditMasterSP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, creditMasterSP.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim creditMasterSP As CreditMasterSP = CType(obj, CreditMasterSP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, creditMasterSP.CreditAccount)
            DbCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, creditMasterSP.Ceiling)
            DbCommandWrapper.AddInParameter("@CeilingBalance", DbType.Currency, creditMasterSP.CeilingBalance)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, creditMasterSP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, creditMasterSP.LastUpdateBy)
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

            Dim creditMasterSP As CreditMasterSP = CType(obj, CreditMasterSP)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, creditMasterSP.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, creditMasterSP.CreditAccount)
            DbCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, creditMasterSP.Ceiling)
            DbCommandWrapper.AddInParameter("@CeilingBalance", DbType.Currency, creditMasterSP.CeilingBalance)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, creditMasterSP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, creditMasterSP.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CreditMasterSP

            Dim creditMasterSP As CreditMasterSP = New CreditMasterSP

            creditMasterSP.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then creditMasterSP.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Ceiling")) Then creditMasterSP.Ceiling = CType(dr("Ceiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("CeilingBalance")) Then creditMasterSP.CeilingBalance = CType(dr("CeilingBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then creditMasterSP.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then creditMasterSP.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then creditMasterSP.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then creditMasterSP.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then creditMasterSP.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return creditMasterSP

        End Function

        Private Sub SetTableName()

            If Not (GetType(CreditMasterSP) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CreditMasterSP), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CreditMasterSP).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

