
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CreditMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 1:37:21 PM
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

    Public Class CreditMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCreditMaster"
        Private m_UpdateStatement As String = "up_UpdateCreditMaster"
        Private m_RetrieveStatement As String = "up_RetrieveCreditMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveCreditMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCreditMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim creditMaster As CreditMaster = Nothing
            While dr.Read

                creditMaster = Me.CreateObject(dr)

            End While

            Return creditMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim creditMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim creditMaster As CreditMaster = Me.CreateObject(dr)
                creditMasterList.Add(creditMaster)
            End While

            Return creditMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim creditMaster As CreditMaster = CType(obj, CreditMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, creditMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim creditMaster As CreditMaster = CType(obj, CreditMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, creditMaster.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, creditMaster.PaymentType)
            DbCommandWrapper.AddInParameter("@Plafon", DbType.Currency, creditMaster.Plafon)
            DbCommandWrapper.AddInParameter("@OutStanding", DbType.Currency, creditMaster.OutStanding)
            DBCommandWrapper.AddInParameter("@AvailablePlafon", DbType.Currency, creditMaster.AvailablePlafon)
            DBCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, creditMaster.MaxTOPDate)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, creditMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, creditMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(creditMaster.ProductCategory))

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

            Dim creditMaster As CreditMaster = CType(obj, CreditMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, creditMaster.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, creditMaster.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, creditMaster.PaymentType)
            DbCommandWrapper.AddInParameter("@Plafon", DbType.Currency, creditMaster.Plafon)
            DbCommandWrapper.AddInParameter("@OutStanding", DbType.Currency, creditMaster.OutStanding)
            DBCommandWrapper.AddInParameter("@AvailablePlafon", DbType.Currency, creditMaster.AvailablePlafon)
            DBCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, creditMaster.MaxTOPDate)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, creditMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, creditMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(creditMaster.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CreditMaster

            Dim creditMaster As CreditMaster = New CreditMaster

            creditMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then creditMaster.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then creditMaster.PaymentType = CType(dr("PaymentType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Plafon")) Then creditMaster.Plafon = CType(dr("Plafon"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OutStanding")) Then creditMaster.OutStanding = CType(dr("OutStanding"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AvailablePlafon")) Then creditMaster.AvailablePlafon = CType(dr("AvailablePlafon"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDate")) Then creditMaster.MaxTOPDate = CType(dr("MaxTOPDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then creditMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then creditMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then creditMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then creditMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then creditMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                creditMaster.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If

            Return creditMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(CreditMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CreditMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CreditMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

