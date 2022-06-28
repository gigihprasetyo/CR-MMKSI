#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPRegister Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/4/2007 - 04:05:18 PM
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

    Public Class SAPRegisterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPRegister"
        Private m_UpdateStatement As String = "up_UpdateSAPRegister"
        Private m_RetrieveStatement As String = "up_RetrieveSAPRegister"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPRegisterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPRegister"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sAPRegister As SAPRegister = Nothing
            While dr.Read

                sAPRegister = Me.CreateObject(dr)

            End While

            Return sAPRegister

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sAPRegisterList As ArrayList = New ArrayList

            While dr.Read
                Dim sAPRegister As SAPRegister = Me.CreateObject(dr)
                sAPRegisterList.Add(sAPRegister)
            End While

            Return sAPRegisterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPRegister As SAPRegister = CType(obj, SAPRegister)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPRegister.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPRegister As SAPRegister = CType(obj, SAPRegister)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@IsCancelled", DbType.Byte, sAPRegister.IsCancelled)
            DbCommandWrapper.AddInParameter("@WritingTestScore", DbType.Decimal, sAPRegister.WritingTestScore)
            DbCommandWrapper.AddInParameter("@IsEntryTestScore", DbType.Byte, sAPRegister.IsEntryTestScore)
            DbCommandWrapper.AddInParameter("@GradeSWAP", DbType.Int32, sAPRegister.GradeSWAP)
            DbCommandWrapper.AddInParameter("@GradePresentasi", DbType.Int32, sAPRegister.GradePresentasi)
            DbCommandWrapper.AddInParameter("@GradeKonsistensi", DbType.Int32, sAPRegister.GradeKonsistensi)
            DbCommandWrapper.AddInParameter("@GradeKelengkapan", DbType.Int32, sAPRegister.GradeKelengkapan)
            DbCommandWrapper.AddInParameter("@GradeFrekuensi", DbType.Int32, sAPRegister.GradeFrekuensi)
            DbCommandWrapper.AddInParameter("@JumlahPeserta", DbType.Int16, sAPRegister.JumlahPeserta)
            DbCommandWrapper.AddInParameter("@RptProsPek", DbType.Int32, sAPRegister.RptProsPek)
            DbCommandWrapper.AddInParameter("@RptHotProspek", DbType.Int32, sAPRegister.RptHotProspek)
            DbCommandWrapper.AddInParameter("@RptFaktur", DbType.Int32, sAPRegister.RptFaktur)
            DbCommandWrapper.AddInParameter("@RptPDI", DbType.Int32, sAPRegister.RptPDI)
            DbCommandWrapper.AddInParameter("@RptAvgScoreSubOrdinate", DbType.Decimal, sAPRegister.RptAvgScoreSubOrdinate)
            DbCommandWrapper.AddInParameter("@RptEffectivity", DbType.Int32, sAPRegister.RptEffectivity)
            DbCommandWrapper.AddInParameter("@RptAchievement", DbType.Int32, sAPRegister.RptAchievement)
            DbCommandWrapper.AddInParameter("@RptAvgScoreNominator", DbType.Int32, sAPRegister.RptAvgScoreNominator)
            DbCommandWrapper.AddInParameter("@RptWinnerAmount", DbType.Int32, sAPRegister.RptWinnerAmount)
            DbCommandWrapper.AddInParameter("@RptKomposisi", DbType.Int32, sAPRegister.RptKomposisi)
            DbCommandWrapper.AddInParameter("@GradeFinal", DbType.Int32, sAPRegister.GradeFinal)
            DbCommandWrapper.AddInParameter("@IsWinner", DbType.Byte, sAPRegister.IsWinner)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPRegister.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPRegister.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sAPRegister.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@SAPPeriodID", DbType.Int32, Me.GetRefObject(sAPRegister.SAPPeriod))

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

            Dim sAPRegister As SAPRegister = CType(obj, SAPRegister)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPRegister.ID)
            DbCommandWrapper.AddInParameter("@IsCancelled", DbType.Byte, sAPRegister.IsCancelled)
            DbCommandWrapper.AddInParameter("@WritingTestScore", DbType.Decimal, sAPRegister.WritingTestScore)
            DbCommandWrapper.AddInParameter("@IsEntryTestScore", DbType.Byte, sAPRegister.IsEntryTestScore)
            DbCommandWrapper.AddInParameter("@GradeSWAP", DbType.Int32, sAPRegister.GradeSWAP)
            DbCommandWrapper.AddInParameter("@GradePresentasi", DbType.Int32, sAPRegister.GradePresentasi)
            DbCommandWrapper.AddInParameter("@GradeKonsistensi", DbType.Int32, sAPRegister.GradeKonsistensi)
            DbCommandWrapper.AddInParameter("@GradeKelengkapan", DbType.Int32, sAPRegister.GradeKelengkapan)
            DbCommandWrapper.AddInParameter("@GradeFrekuensi", DbType.Int32, sAPRegister.GradeFrekuensi)
            DbCommandWrapper.AddInParameter("@JumlahPeserta", DbType.Int16, sAPRegister.JumlahPeserta)
            DbCommandWrapper.AddInParameter("@RptProsPek", DbType.Int32, sAPRegister.RptProsPek)
            DbCommandWrapper.AddInParameter("@RptHotProspek", DbType.Int32, sAPRegister.RptHotProspek)
            DbCommandWrapper.AddInParameter("@RptFaktur", DbType.Int32, sAPRegister.RptFaktur)
            DbCommandWrapper.AddInParameter("@RptPDI", DbType.Int32, sAPRegister.RptPDI)
            DbCommandWrapper.AddInParameter("@RptAvgScoreSubOrdinate", DbType.Decimal, sAPRegister.RptAvgScoreSubOrdinate)
            DbCommandWrapper.AddInParameter("@RptEffectivity", DbType.Int32, sAPRegister.RptEffectivity)
            DbCommandWrapper.AddInParameter("@RptAchievement", DbType.Int32, sAPRegister.RptAchievement)
            DbCommandWrapper.AddInParameter("@RptAvgScoreNominator", DbType.Int32, sAPRegister.RptAvgScoreNominator)
            DbCommandWrapper.AddInParameter("@RptWinnerAmount", DbType.Int32, sAPRegister.RptWinnerAmount)
            DbCommandWrapper.AddInParameter("@RptKomposisi", DbType.Int32, sAPRegister.RptKomposisi)
            DbCommandWrapper.AddInParameter("@GradeFinal", DbType.Int32, sAPRegister.GradeFinal)
            DbCommandWrapper.AddInParameter("@IsWinner", DbType.Byte, sAPRegister.IsWinner)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPRegister.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sAPRegister.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sAPRegister.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@SAPPeriodID", DbType.Int32, Me.GetRefObject(sAPRegister.SAPPeriod))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPRegister

            Dim sAPRegister As SAPRegister = New SAPRegister

            sAPRegister.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCancelled")) Then sAPRegister.IsCancelled = CType(dr("IsCancelled"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("WritingTestScore")) Then sAPRegister.WritingTestScore = CType(dr("WritingTestScore"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IsEntryTestScore")) Then sAPRegister.IsEntryTestScore = CType(dr("IsEntryTestScore"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("GradeSWAP")) Then sAPRegister.GradeSWAP = CType(dr("GradeSWAP"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GradePresentasi")) Then sAPRegister.GradePresentasi = CType(dr("GradePresentasi"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GradeKonsistensi")) Then sAPRegister.GradeKonsistensi = CType(dr("GradeKonsistensi"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GradeKelengkapan")) Then sAPRegister.GradeKelengkapan = CType(dr("GradeKelengkapan"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GradeFrekuensi")) Then sAPRegister.GradeFrekuensi = CType(dr("GradeFrekuensi"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JumlahPeserta")) Then sAPRegister.JumlahPeserta = CType(dr("JumlahPeserta"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RptProsPek")) Then sAPRegister.RptProsPek = CType(dr("RptProsPek"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptHotProspek")) Then sAPRegister.RptHotProspek = CType(dr("RptHotProspek"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptFaktur")) Then sAPRegister.RptFaktur = CType(dr("RptFaktur"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptPDI")) Then sAPRegister.RptPDI = CType(dr("RptPDI"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptAvgScoreSubOrdinate")) Then sAPRegister.RptAvgScoreSubOrdinate = CType(dr("RptAvgScoreSubOrdinate"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RptEffectivity")) Then sAPRegister.RptEffectivity = CType(dr("RptEffectivity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptAchievement")) Then sAPRegister.RptAchievement = CType(dr("RptAchievement"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptAvgScoreNominator")) Then sAPRegister.RptAvgScoreNominator = CType(dr("RptAvgScoreNominator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptWinnerAmount")) Then sAPRegister.RptWinnerAmount = CType(dr("RptWinnerAmount"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RptKomposisi")) Then sAPRegister.RptKomposisi = CType(dr("RptKomposisi"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GradeFinal")) Then sAPRegister.GradeFinal = CType(dr("GradeFinal"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsWinner")) Then sAPRegister.IsWinner = CType(dr("IsWinner"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sAPRegister.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sAPRegister.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sAPRegister.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sAPRegister.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sAPRegister.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                sAPRegister.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SAPPeriodID")) Then
                sAPRegister.SAPPeriod = New SAPPeriod(CType(dr("SAPPeriodID"), Integer))
            End If

            Return sAPRegister

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPRegister) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPRegister), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPRegister).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
