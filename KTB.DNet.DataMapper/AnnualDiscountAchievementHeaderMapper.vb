#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AnnualDiscountAchievementHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:34:49 PM
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

    Public Class AnnualDiscountAchievementHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAnnualDiscountAchievementHeader"
        Private m_UpdateStatement As String = "up_UpdateAnnualDiscountAchievementHeader"
        Private m_RetrieveStatement As String = "up_RetrieveAnnualDiscountAchievementHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveAnnualDiscountAchievementHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAnnualDiscountAchievementHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim annualDiscountAchievementHeader As AnnualDiscountAchievementHeader = Nothing
            While dr.Read

                annualDiscountAchievementHeader = Me.CreateObject(dr)

            End While

            Return annualDiscountAchievementHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim annualDiscountAchievementHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim annualDiscountAchievementHeader As AnnualDiscountAchievementHeader = Me.CreateObject(dr)
                annualDiscountAchievementHeaderList.Add(annualDiscountAchievementHeader)
            End While

            Return annualDiscountAchievementHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDiscountAchievementHeader As AnnualDiscountAchievementHeader = CType(obj, AnnualDiscountAchievementHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDiscountAchievementHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDiscountAchievementHeader As AnnualDiscountAchievementHeader = CType(obj, AnnualDiscountAchievementHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, annualDiscountAchievementHeader.DealerCode)
            DbCommandWrapper.AddInParameter("@ValidateDateFrom", DbType.DateTime, annualDiscountAchievementHeader.ValidateDateFrom)
            DbCommandWrapper.AddInParameter("@ValidateDateTo", DbType.DateTime, annualDiscountAchievementHeader.ValidateDateTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDiscountAchievementHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, annualDiscountAchievementHeader.LastUpdateBy)
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

            Dim annualDiscountAchievementHeader As AnnualDiscountAchievementHeader = CType(obj, AnnualDiscountAchievementHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDiscountAchievementHeader.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, annualDiscountAchievementHeader.DealerCode)
            DbCommandWrapper.AddInParameter("@ValidateDateFrom", DbType.DateTime, annualDiscountAchievementHeader.ValidateDateFrom)
            DbCommandWrapper.AddInParameter("@ValidateDateTo", DbType.DateTime, annualDiscountAchievementHeader.ValidateDateTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDiscountAchievementHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, annualDiscountAchievementHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AnnualDiscountAchievementHeader

            Dim annualDiscountAchievementHeader As AnnualDiscountAchievementHeader = New AnnualDiscountAchievementHeader

            annualDiscountAchievementHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then annualDiscountAchievementHeader.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDateFrom")) Then annualDiscountAchievementHeader.ValidateDateFrom = CType(dr("ValidateDateFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDateTo")) Then annualDiscountAchievementHeader.ValidateDateTo = CType(dr("ValidateDateTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then annualDiscountAchievementHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then annualDiscountAchievementHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then annualDiscountAchievementHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then annualDiscountAchievementHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then annualDiscountAchievementHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return annualDiscountAchievementHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(AnnualDiscountAchievementHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AnnualDiscountAchievementHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AnnualDiscountAchievementHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

