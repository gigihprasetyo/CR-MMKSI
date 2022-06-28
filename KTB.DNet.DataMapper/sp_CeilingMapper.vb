
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_Ceiling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2012 - 4:37:11 PM
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

    Public Class sp_CeilingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertsp_Ceiling"
        Private m_UpdateStatement As String = "up_Updatesp_Ceiling"
        Private m_RetrieveStatement As String = "up_Retrievesp_Ceiling"
        Private m_RetrieveListStatement As String = "up_Retrievesp_CeilingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_Ceiling"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_Ceiling As sp_Ceiling = Nothing
            While dr.Read

                sp_Ceiling = Me.CreateObject(dr)

            End While

            Return sp_Ceiling

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_CeilingList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_Ceiling As sp_Ceiling = Me.CreateObject(dr)
                sp_CeilingList.Add(sp_Ceiling)
            End While

            Return sp_CeilingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_Ceiling As sp_Ceiling = CType(obj, sp_Ceiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_Ceiling.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_Ceiling As sp_Ceiling = CType(obj, sp_Ceiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, sp_Ceiling.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, sp_Ceiling.PaymentType)
            DbCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, sp_Ceiling.Ceiling)
            DbCommandWrapper.AddInParameter("@ProposedPO", DbType.Currency, sp_Ceiling.ProposedPO)
            DbCommandWrapper.AddInParameter("@LiquifiedPO", DbType.Currency, sp_Ceiling.LiquifiedPO)
            DbCommandWrapper.AddInParameter("@OutStanding", DbType.Currency, sp_Ceiling.OutStanding)
            DbCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, sp_Ceiling.MaxTOPDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_Ceiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sp_Ceiling.LastUpdateBy)
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

            Dim sp_Ceiling As sp_Ceiling = CType(obj, sp_Ceiling)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_Ceiling.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, sp_Ceiling.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, sp_Ceiling.PaymentType)
            DbCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, sp_Ceiling.Ceiling)
            DbCommandWrapper.AddInParameter("@ProposedPO", DbType.Currency, sp_Ceiling.ProposedPO)
            DbCommandWrapper.AddInParameter("@LiquifiedPO", DbType.Currency, sp_Ceiling.LiquifiedPO)
            DbCommandWrapper.AddInParameter("@OutStanding", DbType.Currency, sp_Ceiling.OutStanding)
            DbCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, sp_Ceiling.MaxTOPDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_Ceiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sp_Ceiling.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_Ceiling

            Dim sp_Ceiling As sp_Ceiling = New sp_Ceiling

            sp_Ceiling.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then sp_Ceiling.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then sp_Ceiling.PaymentType = CType(dr("PaymentType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Ceiling")) Then sp_Ceiling.Ceiling = CType(dr("Ceiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ProposedPO")) Then sp_Ceiling.ProposedPO = CType(dr("ProposedPO"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LiquifiedPO")) Then sp_Ceiling.LiquifiedPO = CType(dr("LiquifiedPO"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OutStanding")) Then sp_Ceiling.OutStanding = CType(dr("OutStanding"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDate")) Then sp_Ceiling.MaxTOPDate = CType(dr("MaxTOPDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sp_Ceiling.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sp_Ceiling.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sp_Ceiling.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sp_Ceiling.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sp_Ceiling.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sp_Ceiling

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_Ceiling) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_Ceiling), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(sp_Ceiling).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

