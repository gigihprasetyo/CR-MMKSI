#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_LeasingDaftarKondisi Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/14/2009 - 10:28:11 AM
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

    Public Class V_LeasingDaftarKondisiMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_LeasingDaftarKondisi"
        Private m_UpdateStatement As String = "up_UpdateV_LeasingDaftarKondisi"
        Private m_RetrieveStatement As String = "up_RetrieveV_LeasingDaftarKondisi"
        Private m_RetrieveListStatement As String = "up_RetrieveV_LeasingDaftarKondisiList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_LeasingDaftarKondisi"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_LeasingDaftarKondisi As V_LeasingDaftarKondisi = Nothing
            While dr.Read

                v_LeasingDaftarKondisi = Me.CreateObject(dr)

            End While

            Return v_LeasingDaftarKondisi

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_LeasingDaftarKondisiList As ArrayList = New ArrayList

            While dr.Read
                Dim v_LeasingDaftarKondisi As V_LeasingDaftarKondisi = Me.CreateObject(dr)
                v_LeasingDaftarKondisiList.Add(v_LeasingDaftarKondisi)
            End While

            Return v_LeasingDaftarKondisiList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_LeasingDaftarKondisi As V_LeasingDaftarKondisi = CType(obj, V_LeasingDaftarKondisi)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_LeasingDaftarKondisi.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_LeasingDaftarKondisi As V_LeasingDaftarKondisi = CType(obj, V_LeasingDaftarKondisi)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.Int16, v_LeasingDaftarKondisi.DocumentType)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, v_LeasingDaftarKondisi.VechileTypeID)
            'DBCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, v_LeasingDaftarKondisi.ValidFrom)

            DBCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, v_LeasingDaftarKondisi.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, v_LeasingDaftarKondisi.BasePrice)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, v_LeasingDaftarKondisi.RetailPrice)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, v_LeasingDaftarKondisi.Subsidi)
            DbCommandWrapper.AddInParameter("@PPh", DbType.Decimal, v_LeasingDaftarKondisi.PPh)
            DbCommandWrapper.AddInParameter("@AfterPPh", DbType.Decimal, v_LeasingDaftarKondisi.AfterPPh)
            DbCommandWrapper.AddInParameter("@PPn", DbType.Decimal, v_LeasingDaftarKondisi.PPn)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, v_LeasingDaftarKondisi.SPAF)
            DbCommandWrapper.AddInParameter("@AssistFee", DbType.Currency, v_LeasingDaftarKondisi.AssistFee)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_LeasingDaftarKondisi.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_LeasingDaftarKondisi.LastUpdateBy)
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

            Dim v_LeasingDaftarKondisi As V_LeasingDaftarKondisi = CType(obj, V_LeasingDaftarKondisi)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_LeasingDaftarKondisi.ID)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.Int16, v_LeasingDaftarKondisi.DocumentType)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, v_LeasingDaftarKondisi.VechileTypeID)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, v_LeasingDaftarKondisi.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, v_LeasingDaftarKondisi.BasePrice)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, v_LeasingDaftarKondisi.RetailPrice)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, v_LeasingDaftarKondisi.Subsidi)
            DbCommandWrapper.AddInParameter("@PPh", DbType.Decimal, v_LeasingDaftarKondisi.PPh)
            DbCommandWrapper.AddInParameter("@AfterPPh", DbType.Decimal, v_LeasingDaftarKondisi.AfterPPh)
            DbCommandWrapper.AddInParameter("@PPn", DbType.Decimal, v_LeasingDaftarKondisi.PPn)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, v_LeasingDaftarKondisi.SPAF)
            DbCommandWrapper.AddInParameter("@AssistFee", DbType.Currency, v_LeasingDaftarKondisi.AssistFee)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_LeasingDaftarKondisi.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_LeasingDaftarKondisi.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_LeasingDaftarKondisi

            Dim v_LeasingDaftarKondisi As V_LeasingDaftarKondisi = New V_LeasingDaftarKondisi

            v_LeasingDaftarKondisi.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then v_LeasingDaftarKondisi.DocumentType = CType(dr("DocumentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then v_LeasingDaftarKondisi.VechileTypeID = CType(dr("VechileTypeID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then v_LeasingDaftarKondisi.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then v_LeasingDaftarKondisi.ValidTo = CType(dr("ValidTo"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BasePrice")) Then v_LeasingDaftarKondisi.BasePrice = CType(dr("BasePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then v_LeasingDaftarKondisi.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Subsidi")) Then v_LeasingDaftarKondisi.Subsidi = CType(dr("Subsidi"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh")) Then v_LeasingDaftarKondisi.PPh = CType(dr("PPh"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AfterPPh")) Then v_LeasingDaftarKondisi.AfterPPh = CType(dr("AfterPPh"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPn")) Then v_LeasingDaftarKondisi.PPn = CType(dr("PPn"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAF")) Then v_LeasingDaftarKondisi.SPAF = CType(dr("SPAF"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistFee")) Then v_LeasingDaftarKondisi.AssistFee = CType(dr("AssistFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_LeasingDaftarKondisi.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_LeasingDaftarKondisi.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_LeasingDaftarKondisi.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_LeasingDaftarKondisi.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_LeasingDaftarKondisi.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_LeasingDaftarKondisi

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_LeasingDaftarKondisi) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_LeasingDaftarKondisi), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_LeasingDaftarKondisi).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

