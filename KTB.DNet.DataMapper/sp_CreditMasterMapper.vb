
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_CreditMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2009 - 1:17:35 PM
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

    Public Class sp_CreditMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertsp_CreditMaster"
        Private m_UpdateStatement As String = "up_Updatesp_CreditMaster"
        Private m_RetrieveStatement As String = "up_Retrievesp_CreditMaster"
        Private m_RetrieveListStatement As String = "up_Retrievesp_CreditMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_CreditMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_CreditMaster As sp_CreditMaster = Nothing
            While dr.Read

                sp_CreditMaster = Me.CreateObject(dr)

            End While

            Return sp_CreditMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_CreditMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_CreditMaster As sp_CreditMaster = Me.CreateObject(dr)
                sp_CreditMasterList.Add(sp_CreditMaster)
            End While

            Return sp_CreditMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_CreditMaster As sp_CreditMaster = CType(obj, sp_CreditMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_CreditMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_CreditMaster As sp_CreditMaster = CType(obj, sp_CreditMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, sp_CreditMaster.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, sp_CreditMaster.PaymentType)
            DbCommandWrapper.AddInParameter("@Plafon", DbType.Currency, sp_CreditMaster.Plafon)
            DbCommandWrapper.AddInParameter("@OutStanding", DbType.Currency, sp_CreditMaster.OutStanding)
            DbCommandWrapper.AddInParameter("@AvailablePlafon", DbType.Currency, sp_CreditMaster.AvailablePlafon)
            DbCommandWrapper.AddInParameter("@ProposedPO", DbType.Currency, sp_CreditMaster.ProposedPO)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_CreditMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sp_CreditMaster.LastUpdateBy)
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

            Dim sp_CreditMaster As sp_CreditMaster = CType(obj, sp_CreditMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_CreditMaster.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, sp_CreditMaster.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, sp_CreditMaster.PaymentType)
            DbCommandWrapper.AddInParameter("@Plafon", DbType.Currency, sp_CreditMaster.Plafon)
            DbCommandWrapper.AddInParameter("@OutStanding", DbType.Currency, sp_CreditMaster.OutStanding)
            DbCommandWrapper.AddInParameter("@AvailablePlafon", DbType.Currency, sp_CreditMaster.AvailablePlafon)
            DbCommandWrapper.AddInParameter("@ProposedPO", DbType.Currency, sp_CreditMaster.ProposedPO)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_CreditMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sp_CreditMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_CreditMaster

            Dim sp_CreditMaster As sp_CreditMaster = New sp_CreditMaster

            sp_CreditMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then sp_CreditMaster.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryCode")) Then sp_CreditMaster.ProductCategoryCode = dr("ProductCategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then sp_CreditMaster.PaymentType = CType(dr("PaymentType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Plafon")) Then sp_CreditMaster.Plafon = CType(dr("Plafon"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OutStanding")) Then sp_CreditMaster.OutStanding = CType(dr("OutStanding"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AvailablePlafon")) Then sp_CreditMaster.AvailablePlafon = CType(dr("AvailablePlafon"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ProposedPO")) Then sp_CreditMaster.ProposedPO = CType(dr("ProposedPO"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDate")) Then sp_CreditMaster.MaxTOPDate = CType(dr("MaxTOPDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sp_CreditMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sp_CreditMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sp_CreditMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sp_CreditMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sp_CreditMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sp_CreditMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_CreditMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_CreditMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
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

