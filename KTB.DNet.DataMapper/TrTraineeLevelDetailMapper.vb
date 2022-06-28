#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineeLevelDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2019 - 4:22:25 PM
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

    Public Class TrTraineeLevelDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTraineeLevelDetail"
        Private m_UpdateStatement As String = "up_UpdateTrTraineeLevelDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTrTraineeLevelDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineeLevelDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTraineeLevelDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim TrTraineeLevelDetail As TrTraineeLevelDetail = Nothing
            While dr.Read

                TrTraineeLevelDetail = Me.CreateObject(dr)

            End While

            Return TrTraineeLevelDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim TrTraineeLevelDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim TrTraineeLevelDetail As TrTraineeLevelDetail = Me.CreateObject(dr)
                TrTraineeLevelDetailList.Add(TrTraineeLevelDetail)
            End While

            Return TrTraineeLevelDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TrTraineeLevelDetail As TrTraineeLevelDetail = CType(obj, TrTraineeLevelDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TrTraineeLevelDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TrTraineeLevelDetail As TrTraineeLevelDetail = CType(obj, TrTraineeLevelDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrTrainee))
            DbCommandWrapper.AddInParameter("@TrTraineeLevelID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrTraineeLevel))
            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@TrCertificateConfigID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrCertificateConfig))
            DbCommandWrapper.AddInParameter("@TrClassRegistration", DbType.AnsiString, TrTraineeLevelDetail.TrClassRegistration)
            DbCommandWrapper.AddInParameter("@NamaSiswa", DbType.AnsiString, TrTraineeLevelDetail.NamaSiswa)
            DbCommandWrapper.AddInParameter("@CertificateNumber", DbType.AnsiString, TrTraineeLevelDetail.CertificateNumber)
            DbCommandWrapper.AddInParameter("@TanggalLulus", DbType.DateTime, TrTraineeLevelDetail.TanggalLulus)
            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, TrTraineeLevelDetail.ClassID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, TrTraineeLevelDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, TrTraineeLevelDetail.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, TrTraineeLevelDetail.LastUpdateBy)
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

            Dim TrTraineeLevelDetail As TrTraineeLevelDetail = CType(obj, TrTraineeLevelDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TrTraineeLevelDetail.ID)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrTrainee))
            DbCommandWrapper.AddInParameter("@TrTraineeLevelID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrTraineeLevel))
            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@TrCertificateConfigID", DbType.Int32, Me.GetRefObject(TrTraineeLevelDetail.TrCertificateConfig))
            DbCommandWrapper.AddInParameter("@TrClassRegistration", DbType.AnsiString, TrTraineeLevelDetail.TrClassRegistration)
            DbCommandWrapper.AddInParameter("@NamaSiswa", DbType.AnsiString, TrTraineeLevelDetail.NamaSiswa)
            DbCommandWrapper.AddInParameter("@CertificateNumber", DbType.AnsiString, TrTraineeLevelDetail.CertificateNumber)
            DbCommandWrapper.AddInParameter("@TanggalLulus", DbType.DateTime, TrTraineeLevelDetail.TanggalLulus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, TrTraineeLevelDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, TrTraineeLevelDetail.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, TrTraineeLevelDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTraineeLevelDetail

            Dim TrTraineeLevelDetail As TrTraineeLevelDetail = New TrTraineeLevelDetail

            TrTraineeLevelDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeID")) Then TrTraineeLevelDetail.TrTrainee = New TrTrainee(CType(dr("TrTraineeID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeLevelID")) Then TrTraineeLevelDetail.TrTraineeLevel = New TrTraineeLevel(CType(dr("TrTraineeLevelID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalLulus")) Then TrTraineeLevelDetail.TanggalLulus = CType(dr("TanggalLulus"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TrCourseCategoryID")) Then TrTraineeLevelDetail.TrCourseCategory = New TrCourseCategory(CType(dr("TrCourseCategoryID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrCertificateConfigID")) Then TrTraineeLevelDetail.TrCertificateConfig = New TrCertificateConfig(CType(dr("TrCertificateConfigID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrClassRegistration")) Then TrTraineeLevelDetail.TrClassRegistration = dr("TrClassRegistration").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NamaSiswa")) Then TrTraineeLevelDetail.NamaSiswa = dr("NamaSiswa").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CertificateNumber")) Then TrTraineeLevelDetail.CertificateNumber = dr("CertificateNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then TrTraineeLevelDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then TrTraineeLevelDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then TrTraineeLevelDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then TrTraineeLevelDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then TrTraineeLevelDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then TrTraineeLevelDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return TrTraineeLevelDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTraineeLevelDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTraineeLevelDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTraineeLevelDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
