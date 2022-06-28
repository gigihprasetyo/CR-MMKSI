
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ParkingFeeReturnHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 3/8/2012 - 9:00:39 AM
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

    Public Class ParkingFeeReturnHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertParkingFeeReturnHeader"
        Private m_UpdateStatement As String = "up_UpdateParkingFeeReturnHeader"
        Private m_RetrieveStatement As String = "up_RetrieveParkingFeeReturnHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveParkingFeeReturnHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteParkingFeeReturnHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim parkingFeeReturnHeader As ParkingFeeReturnHeader = Nothing
            While dr.Read

                parkingFeeReturnHeader = Me.CreateObject(dr)

            End While

            Return parkingFeeReturnHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim parkingFeeReturnHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim parkingFeeReturnHeader As ParkingFeeReturnHeader = Me.CreateObject(dr)
                parkingFeeReturnHeaderList.Add(parkingFeeReturnHeader)
            End While

            Return parkingFeeReturnHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim parkingFeeReturnHeader As ParkingFeeReturnHeader = CType(obj, ParkingFeeReturnHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, parkingFeeReturnHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim parkingFeeReturnHeader As ParkingFeeReturnHeader = CType(obj, ParkingFeeReturnHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, parkingFeeReturnHeader.NoReg)
            DbCommandWrapper.AddInParameter("@BuktiPotongNumber", DbType.AnsiString, parkingFeeReturnHeader.BuktiPotongNumber)
            DbCommandWrapper.AddInParameter("@ReturnDate", DbType.DateTime, parkingFeeReturnHeader.ReturnDate)
            DbCommandWrapper.AddInParameter("@ReturnAssignNumber", DbType.AnsiString, parkingFeeReturnHeader.ReturnAssignNumber)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, parkingFeeReturnHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, parkingFeeReturnHeader.PPHAmount)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, parkingFeeReturnHeader.Description)
            DBCommandWrapper.AddInParameter("@Status", DbType.Byte, parkingFeeReturnHeader.Status)
            DBCommandWrapper.AddInParameter("@KantorPajak", DbType.AnsiString, parkingFeeReturnHeader.KantorPajak)
            DBCommandWrapper.AddInParameter("@KotaDealer", DbType.AnsiString, parkingFeeReturnHeader.KotaDealer)
            DBCommandWrapper.AddInParameter("@Pejabat", DbType.AnsiString, parkingFeeReturnHeader.Pejabat)
            DBCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, parkingFeeReturnHeader.Jabatan)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, parkingFeeReturnHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, parkingFeeReturnHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(parkingFeeReturnHeader.Dealer))

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

            Dim parkingFeeReturnHeader As ParkingFeeReturnHeader = CType(obj, ParkingFeeReturnHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, parkingFeeReturnHeader.ID)
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, parkingFeeReturnHeader.NoReg)
            DbCommandWrapper.AddInParameter("@BuktiPotongNumber", DbType.AnsiString, parkingFeeReturnHeader.BuktiPotongNumber)
            DbCommandWrapper.AddInParameter("@ReturnDate", DbType.DateTime, parkingFeeReturnHeader.ReturnDate)
            DbCommandWrapper.AddInParameter("@ReturnAssignNumber", DbType.AnsiString, parkingFeeReturnHeader.ReturnAssignNumber)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, parkingFeeReturnHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, parkingFeeReturnHeader.PPHAmount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, parkingFeeReturnHeader.Description)
            DBCommandWrapper.AddInParameter("@Status", DbType.Byte, parkingFeeReturnHeader.Status)
            DBCommandWrapper.AddInParameter("@KantorPajak", DbType.AnsiString, parkingFeeReturnHeader.KantorPajak)
            DBCommandWrapper.AddInParameter("@KotaDealer", DbType.AnsiString, parkingFeeReturnHeader.KotaDealer)
            DBCommandWrapper.AddInParameter("@Pejabat", DbType.AnsiString, parkingFeeReturnHeader.Pejabat)
            DBCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, parkingFeeReturnHeader.Jabatan)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, parkingFeeReturnHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, parkingFeeReturnHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(parkingFeeReturnHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ParkingFeeReturnHeader

            Dim parkingFeeReturnHeader As parkingFeeReturnHeader = New parkingFeeReturnHeader

            parkingFeeReturnHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoReg")) Then parkingFeeReturnHeader.NoReg = dr("NoReg").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BuktiPotongNumber")) Then parkingFeeReturnHeader.BuktiPotongNumber = dr("BuktiPotongNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReturnDate")) Then parkingFeeReturnHeader.ReturnDate = CType(dr("ReturnDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReturnAssignNumber")) Then parkingFeeReturnHeader.ReturnAssignNumber = dr("ReturnAssignNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then parkingFeeReturnHeader.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHAmount")) Then parkingFeeReturnHeader.PPHAmount = CType(dr("PPHAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then parkingFeeReturnHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then parkingFeeReturnHeader.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("KantorPajak")) Then parkingFeeReturnHeader.KantorPajak = dr("KantorPajak").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KotaDealer")) Then parkingFeeReturnHeader.KotaDealer = dr("KotaDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Pejabat")) Then parkingFeeReturnHeader.Pejabat = dr("Pejabat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jabatan")) Then parkingFeeReturnHeader.Jabatan = dr("Jabatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then parkingFeeReturnHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then parkingFeeReturnHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then parkingFeeReturnHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then parkingFeeReturnHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then parkingFeeReturnHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                parkingFeeReturnHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return parkingFeeReturnHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(ParkingFeeReturnHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ParkingFeeReturnHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ParkingFeeReturnHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

