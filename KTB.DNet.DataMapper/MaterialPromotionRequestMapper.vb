#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionRequest Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2007 - 02:19:38 PM
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

    Public Class MaterialPromotionRequestMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotionRequest"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotionRequest"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotionRequest"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionRequestList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotionRequest"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotionRequest As MaterialPromotionRequest = Nothing
            While dr.Read

                materialPromotionRequest = Me.CreateObject(dr)

            End While

            Return materialPromotionRequest

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionRequestList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotionRequest As MaterialPromotionRequest = Me.CreateObject(dr)
                materialPromotionRequestList.Add(materialPromotionRequest)
            End While

            Return materialPromotionRequestList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionRequest As MaterialPromotionRequest = CType(obj, MaterialPromotionRequest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionRequest.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionRequest As MaterialPromotionRequest = CType(obj, MaterialPromotionRequest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, materialPromotionRequest.RequestNo)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, materialPromotionRequest.RequestDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiStringFixedLength, materialPromotionRequest.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotionRequest.Status)
            DbCommandWrapper.AddInParameter("@GIStatus", DbType.Byte, materialPromotionRequest.GIStatus)
            DbCommandWrapper.AddInParameter("@IsValidate", DbType.Byte, materialPromotionRequest.IsValidate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionRequest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotionRequest.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionRequest.Dealer))

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

            Dim materialPromotionRequest As MaterialPromotionRequest = CType(obj, MaterialPromotionRequest)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionRequest.ID)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, materialPromotionRequest.RequestNo)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, materialPromotionRequest.RequestDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiStringFixedLength, materialPromotionRequest.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotionRequest.Status)
            DbCommandWrapper.AddInParameter("@GIStatus", DbType.Byte, materialPromotionRequest.GIStatus)
            DbCommandWrapper.AddInParameter("@IsValidate", DbType.Byte, materialPromotionRequest.IsValidate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionRequest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotionRequest.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionRequest.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotionRequest

            Dim materialPromotionRequest As MaterialPromotionRequest = New MaterialPromotionRequest

            materialPromotionRequest.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNo")) Then materialPromotionRequest.RequestNo = dr("RequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then materialPromotionRequest.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then materialPromotionRequest.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then materialPromotionRequest.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("GIStatus")) Then materialPromotionRequest.GIStatus = CType(dr("GIStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsValidate")) Then materialPromotionRequest.IsValidate = CType(dr("IsValidate"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotionRequest.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotionRequest.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotionRequest.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotionRequest.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotionRequest.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                materialPromotionRequest.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return materialPromotionRequest

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotionRequest) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotionRequest), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotionRequest).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

