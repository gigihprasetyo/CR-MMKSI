
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PhisingGuardImage Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/5/2007 - 3:39:35 PM
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

    Public Class PhisingGuardImageMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPhisingGuardImage"
        Private m_UpdateStatement As String = "up_UpdatePhisingGuardImage"
        Private m_RetrieveStatement As String = "up_RetrievePhisingGuardImage"
        Private m_RetrieveListStatement As String = "up_RetrievePhisingGuardImageList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePhisingGuardImage"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim phisingGuardImage As PhisingGuardImage = Nothing
            While dr.Read

                phisingGuardImage = Me.CreateObject(dr)

            End While

            Return phisingGuardImage

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim phisingGuardImageList As ArrayList = New ArrayList

            While dr.Read
                Dim phisingGuardImage As PhisingGuardImage = Me.CreateObject(dr)
                phisingGuardImageList.Add(phisingGuardImage)
            End While

            Return phisingGuardImageList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim phisingGuardImage As PhisingGuardImage = CType(obj, PhisingGuardImage)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, phisingGuardImage.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim phisingGuardImage As PhisingGuardImage = CType(obj, PhisingGuardImage)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ImageCode", DbType.AnsiString, phisingGuardImage.ImageCode)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, phisingGuardImage.Image)
            DbCommandWrapper.AddInParameter("@UploadedUserID", DbType.Int32, phisingGuardImage.UploadedUserID)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, phisingGuardImage.Type)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, phisingGuardImage.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, phisingGuardImage.LastUpdateBy)
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

            Dim phisingGuardImage As PhisingGuardImage = CType(obj, PhisingGuardImage)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, phisingGuardImage.ID)
            DbCommandWrapper.AddInParameter("@ImageCode", DbType.AnsiString, phisingGuardImage.ImageCode)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, phisingGuardImage.Image)
            DbCommandWrapper.AddInParameter("@UploadedUserID", DbType.Int32, phisingGuardImage.UploadedUserID)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, phisingGuardImage.Type)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, phisingGuardImage.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, phisingGuardImage.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PhisingGuardImage

            Dim phisingGuardImage As PhisingGuardImage = New PhisingGuardImage

            phisingGuardImage.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ImageCode")) Then phisingGuardImage.ImageCode = dr("ImageCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Image")) Then phisingGuardImage.Image = CType(dr("Image"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("UploadedUserID")) Then phisingGuardImage.UploadedUserID = CType(dr("UploadedUserID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then phisingGuardImage.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then phisingGuardImage.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then phisingGuardImage.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then phisingGuardImage.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then phisingGuardImage.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then phisingGuardImage.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return phisingGuardImage

        End Function

        Private Sub SetTableName()

            If Not (GetType(PhisingGuardImage) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PhisingGuardImage), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PhisingGuardImage).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

