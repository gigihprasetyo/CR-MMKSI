
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOTypeTOP Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 19/09/2018 - 9:46:24
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

    Public Class SparePartPOTypeTOPMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPOTypeTOP"
        Private m_UpdateStatement As String = "up_UpdateSparePartPOTypeTOP"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPOTypeTOP"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPOTypeTOPList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPOTypeTOP"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPOTypeTOP As SparePartPOTypeTOP = Nothing
            While dr.Read

                sparePartPOTypeTOP = Me.CreateObject(dr)

            End While

            Return sparePartPOTypeTOP

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPOTypeTOPList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPOTypeTOP As SparePartPOTypeTOP = Me.CreateObject(dr)
                sparePartPOTypeTOPList.Add(sparePartPOTypeTOP)
            End While

            Return sparePartPOTypeTOPList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOTypeTOP As SparePartPOTypeTOP = CType(obj, SparePartPOTypeTOP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOTypeTOP.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOTypeTOP As SparePartPOTypeTOP = CType(obj, SparePartPOTypeTOP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SparePartPOType", DbType.AnsiString, sparePartPOTypeTOP.SparePartPOType)
            DbCommandWrapper.AddInParameter("@IsTOP", DbType.Boolean, sparePartPOTypeTOP.IsTOP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOTypeTOP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPOTypeTOP.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(sparePartPOTypeTOP.TermOfPaymentIDNotTOP))

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

            Dim sparePartPOTypeTOP As SparePartPOTypeTOP = CType(obj, SparePartPOTypeTOP)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOTypeTOP.ID)
            DbCommandWrapper.AddInParameter("@SparePartPOType", DbType.AnsiString, sparePartPOTypeTOP.SparePartPOType)
            DbCommandWrapper.AddInParameter("@IsTOP", DbType.Boolean, sparePartPOTypeTOP.IsTOP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOTypeTOP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPOTypeTOP.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(sparePartPOTypeTOP.TermOfPaymentIDNotTOP))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPOTypeTOP

            Dim sparePartPOTypeTOP As SparePartPOTypeTOP = New SparePartPOTypeTOP

            sparePartPOTypeTOP.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOType")) Then sparePartPOTypeTOP.SparePartPOType = dr("SparePartPOType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsTOP")) Then sparePartPOTypeTOP.IsTOP = CType(dr("IsTOP"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPOTypeTOP.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPOTypeTOP.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPOTypeTOP.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPOTypeTOP.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPOTypeTOP.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentIDNotTOP")) Then
                sparePartPOTypeTOP.TermOfPaymentIDNotTOP = New TermOfPayment(CType(dr("TermOfPaymentIDNotTOP"), Integer))
            End If
            Return sparePartPOTypeTOP

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPOTypeTOP) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPOTypeTOP), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPOTypeTOP).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

