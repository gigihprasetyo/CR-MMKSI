#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AnnualDiscount Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/28/2005 - 11:08:14 AM
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

    Public Class AnnualDiscountMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAnnualDiscount"
        Private m_UpdateStatement As String = "up_UpdateAnnualDiscount"
        Private m_RetrieveStatement As String = "up_RetrieveAnnualDiscount"
        Private m_RetrieveListStatement As String = "up_RetrieveAnnualDiscountList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAnnualDiscount"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim annualDiscount As AnnualDiscount = Nothing
            While dr.Read

                annualDiscount = Me.CreateObject(dr)

            End While

            Return annualDiscount

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim annualDiscountList As ArrayList = New ArrayList

            While dr.Read
                Dim annualDiscount As AnnualDiscount = Me.CreateObject(dr)
                annualDiscountList.Add(annualDiscount)
            End While

            Return annualDiscountList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDiscount As AnnualDiscount = CType(obj, AnnualDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDiscount.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDiscount As AnnualDiscount = CType(obj, AnnualDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PartNo", DbType.AnsiString, annualDiscount.PartNo)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, annualDiscount.PartName)
            DbCommandWrapper.AddInParameter("@Model", DbType.AnsiString, annualDiscount.Model)
            DbCommandWrapper.AddInParameter("@MinimumQty", DbType.Int32, annualDiscount.MinimumQty)
            DbCommandWrapper.AddInParameter("@Point", DbType.Int32, annualDiscount.Point)
            DbCommandWrapper.AddInParameter("@ValidateDateFrom", DbType.DateTime, annualDiscount.ValidateDateFrom)
            DbCommandWrapper.AddInParameter("@ValidateDateTo", DbType.DateTime, annualDiscount.ValidateDateTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDiscount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, annualDiscount.LastUpdateBy)
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

            Dim annualDiscount As AnnualDiscount = CType(obj, AnnualDiscount)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDiscount.ID)
            DbCommandWrapper.AddInParameter("@PartNo", DbType.AnsiString, annualDiscount.PartNo)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, annualDiscount.PartName)
            DbCommandWrapper.AddInParameter("@Model", DbType.AnsiString, annualDiscount.Model)
            DbCommandWrapper.AddInParameter("@MinimumQty", DbType.Int32, annualDiscount.MinimumQty)
            DbCommandWrapper.AddInParameter("@Point", DbType.Int32, annualDiscount.Point)
            DbCommandWrapper.AddInParameter("@ValidateDateFrom", DbType.DateTime, annualDiscount.ValidateDateFrom)
            DbCommandWrapper.AddInParameter("@ValidateDateTo", DbType.DateTime, annualDiscount.ValidateDateTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDiscount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, annualDiscount.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AnnualDiscount

            Dim annualDiscount As AnnualDiscount = New AnnualDiscount

            annualDiscount.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNo")) Then annualDiscount.PartNo = dr("PartNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then annualDiscount.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Model")) Then annualDiscount.Model = dr("Model").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MinimumQty")) Then annualDiscount.MinimumQty = CType(dr("MinimumQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Point")) Then annualDiscount.Point = CType(dr("Point"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDateFrom")) Then annualDiscount.ValidateDateFrom = CType(dr("ValidateDateFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDateTo")) Then annualDiscount.ValidateDateTo = CType(dr("ValidateDateTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then annualDiscount.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then annualDiscount.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then annualDiscount.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then annualDiscount.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then annualDiscount.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return annualDiscount

        End Function

        Private Sub SetTableName()

            If Not (GetType(AnnualDiscount) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AnnualDiscount), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AnnualDiscount).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"
        Private m_RetrieveDisctinctStatement As String = "up_RetrieveAnnualDiscountDistinctValidDate"
        Private m_MapperExceptionPolicy As String = "Mapper Policy"
        Private Function GetRetrieveDistinctValidDateParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveDisctinctStatement)
            'DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Private Function DoRetrieveListDistinct(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim annualDiscountList As ArrayList = New ArrayList

            While dr.Read
                Dim annualDiscount As annualDiscount = Me.CreateDistinctObject(dr)
                annualDiscountList.Add(annualDiscount)
            End While

            Return annualDiscountList

        End Function

        Private Function CreateDistinctObject(ByVal dr As IDataReader) As AnnualDiscount

            Dim annualDiscount As annualDiscount = New annualDiscount

            annualDiscount.ID = 0
            annualDiscount.PartNo = String.Empty
            annualDiscount.PartName = String.Empty
            annualDiscount.Model = String.Empty
            annualDiscount.MinimumQty = 0
            annualDiscount.Point = 0
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDateFrom")) Then annualDiscount.ValidateDateFrom = CType(dr("ValidateDateFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDateTo")) Then annualDiscount.ValidateDateTo = CType(dr("ValidateDateTo"), DateTime)
            annualDiscount.RowStatus = 0
            annualDiscount.CreatedBy = String.Empty
            annualDiscount.CreatedTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            annualDiscount.LastUpdateBy = String.Empty
            annualDiscount.LastUpdateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

            Return annualDiscount

        End Function
        Public Function RetrieveDistinctValidDate() As Object
            Dim dr As IDataReader = Nothing
            Dim domainObject As Object = Nothing
            Try
                DBCommandWrapper = GetRetrieveDistinctValidDateParameter()
                dr = Db.ExecuteReader(DBCommandWrapper)
                domainObject = DoRetrieveListDistinct(dr)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObject
        End Function

#End Region

    End Class
End Namespace

