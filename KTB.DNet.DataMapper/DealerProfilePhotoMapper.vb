#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerProfilePhoto Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/10/2007 - 10:35:10 AM
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

    Public Class DealerProfilePhotoMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerProfilePhoto"
        Private m_UpdateStatement As String = "up_UpdateDealerProfilePhoto"
        Private m_RetrieveStatement As String = "up_RetrieveDealerProfilePhoto"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerProfilePhotoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerProfilePhoto"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerProfilePhoto As DealerProfilePhoto = Nothing
            While dr.Read

                dealerProfilePhoto = Me.CreateObject(dr)

            End While

            Return dealerProfilePhoto

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerProfilePhotoList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerProfilePhoto As DealerProfilePhoto = Me.CreateObject(dr)
                dealerProfilePhotoList.Add(dealerProfilePhoto)
            End While

            Return dealerProfilePhotoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerProfilePhoto As DealerProfilePhoto = CType(obj, DealerProfilePhoto)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerProfilePhoto.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerProfilePhoto As DealerProfilePhoto = CType(obj, DealerProfilePhoto)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@InitialFileName", DbType.AnsiString, dealerProfilePhoto.InitialFileName)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, dealerProfilePhoto.Image)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerProfilePhoto.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerProfilePhoto.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerProfilePhoto.Dealer))

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

            Dim dealerProfilePhoto As DealerProfilePhoto = CType(obj, DealerProfilePhoto)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerProfilePhoto.ID)
            DbCommandWrapper.AddInParameter("@InitialFileName", DbType.AnsiString, dealerProfilePhoto.InitialFileName)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, dealerProfilePhoto.Image)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerProfilePhoto.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerProfilePhoto.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerProfilePhoto.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerProfilePhoto

            Dim dealerProfilePhoto As DealerProfilePhoto = New DealerProfilePhoto

            dealerProfilePhoto.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InitialFileName")) Then dealerProfilePhoto.InitialFileName = dr("InitialFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Image")) Then dealerProfilePhoto.Image = CType(dr("Image"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerProfilePhoto.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerProfilePhoto.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerProfilePhoto.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerProfilePhoto.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerProfilePhoto.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerProfilePhoto.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return dealerProfilePhoto

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerProfilePhoto) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerProfilePhoto), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerProfilePhoto).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

