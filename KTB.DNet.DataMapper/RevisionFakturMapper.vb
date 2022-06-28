
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : RevisionFaktur Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 09/08/2018 - 15:04:00
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

    Public Class RevisionFakturMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRevisionFaktur"
        Private m_UpdateStatement As String = "up_UpdateRevisionFaktur"
        Private m_RetrieveStatement As String = "up_RetrieveRevisionFaktur"
        Private m_RetrieveListStatement As String = "up_RetrieveRevisionFakturList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRevisionFaktur"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim revisionFaktur As RevisionFaktur = Nothing
            While dr.Read

                revisionFaktur = Me.CreateObject(dr)

            End While

            Return revisionFaktur

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim revisionFakturList As ArrayList = New ArrayList

            While dr.Read
                Dim revisionFaktur As RevisionFaktur = Me.CreateObject(dr)
                revisionFakturList.Add(revisionFaktur)
            End While

            Return revisionFakturList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionFaktur As RevisionFaktur = CType(obj, RevisionFaktur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionFaktur.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionFaktur As RevisionFaktur = CType(obj, RevisionFaktur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, revisionFaktur.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionStatus", DbType.Int16, revisionFaktur.RevisionStatus)
            DbCommandWrapper.AddInParameter("@RevisionTypeID", DbType.Int16, revisionFaktur.RevisionTypeID)
            DbCommandWrapper.AddInParameter("@IsPay", DbType.Int16, revisionFaktur.IsPay)
            DbCommandWrapper.AddInParameter("@NewValidationDate", DbType.DateTime, revisionFaktur.NewValidationDate)
            DbCommandWrapper.AddInParameter("@NewValidationBy", DbType.AnsiString, revisionFaktur.NewValidationBy)
            DbCommandWrapper.AddInParameter("@NewConfirmationDate", DbType.DateTime, revisionFaktur.NewConfirmationDate)
            DbCommandWrapper.AddInParameter("@NewConfirmationBy", DbType.AnsiString, revisionFaktur.NewConfirmationBy)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, revisionFaktur.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionFaktur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, revisionFaktur.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(revisionFaktur.ChassisMaster))
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(revisionFaktur.EndCustomer))
            DbCommandWrapper.AddInParameter("@OldEndCustomerID", DbType.Int32, Me.GetRefObject(revisionFaktur.OldEndCustomer))

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

            Dim revisionFaktur As RevisionFaktur = CType(obj, RevisionFaktur)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionFaktur.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, revisionFaktur.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionStatus", DbType.Int16, revisionFaktur.RevisionStatus)
            DbCommandWrapper.AddInParameter("@RevisionTypeID", DbType.Int16, revisionFaktur.RevisionTypeID)
            DbCommandWrapper.AddInParameter("@IsPay", DbType.Int16, revisionFaktur.IsPay)
            DbCommandWrapper.AddInParameter("@NewValidationDate", DbType.DateTime, revisionFaktur.NewValidationDate)
            DbCommandWrapper.AddInParameter("@NewValidationBy", DbType.AnsiString, revisionFaktur.NewValidationBy)
            DbCommandWrapper.AddInParameter("@NewConfirmationDate", DbType.DateTime, revisionFaktur.NewConfirmationDate)
            DbCommandWrapper.AddInParameter("@NewConfirmationBy", DbType.AnsiString, revisionFaktur.NewConfirmationBy)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, revisionFaktur.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionFaktur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, revisionFaktur.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(revisionFaktur.ChassisMaster))
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(revisionFaktur.EndCustomer))
            DbCommandWrapper.AddInParameter("@OldEndCustomerID", DbType.Int32, Me.GetRefObject(revisionFaktur.OldEndCustomer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RevisionFaktur

            Dim revisionFaktur As RevisionFaktur = New RevisionFaktur

            revisionFaktur.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then revisionFaktur.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionStatus")) Then revisionFaktur.RevisionStatus = CType(dr("RevisionStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionTypeID")) Then revisionFaktur.RevisionTypeID = CType(dr("RevisionTypeID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsPay")) Then revisionFaktur.IsPay = CType(dr("IsPay"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NewValidationDate")) Then revisionFaktur.NewValidationDate = CType(dr("NewValidationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NewValidationBy")) Then revisionFaktur.NewValidationBy = dr("NewValidationBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NewConfirmationDate")) Then revisionFaktur.NewConfirmationDate = CType(dr("NewConfirmationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NewConfirmationBy")) Then revisionFaktur.NewConfirmationBy = dr("NewConfirmationBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then revisionFaktur.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then revisionFaktur.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then revisionFaktur.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then revisionFaktur.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then revisionFaktur.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then revisionFaktur.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                revisionFaktur.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then
                revisionFaktur.EndCustomer = New EndCustomer(CType(dr("EndCustomerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("OldEndCustomerID")) Then
                revisionFaktur.OldEndCustomer = New EndCustomer(CType(dr("OldEndCustomerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionTypeID")) Then
                revisionFaktur.RevisionType = New RevisionType(CType(dr("RevisionTypeID"), Integer))
            End If

            Return revisionFaktur

        End Function

        Private Sub SetTableName()

            If Not (GetType(RevisionFaktur) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RevisionFaktur), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RevisionFaktur).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

