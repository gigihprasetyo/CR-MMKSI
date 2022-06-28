#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Letter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2007 - 11:36:17 AM
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

    Public Class LetterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLetter"
        Private m_UpdateStatement As String = "up_UpdateLetter"
        Private m_RetrieveStatement As String = "up_RetrieveLetter"
        Private m_RetrieveListStatement As String = "up_RetrieveLetterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLetter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim letter As Letter = Nothing
            While dr.Read

                letter = Me.CreateObject(dr)

            End While

            Return letter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim letterList As ArrayList = New ArrayList

            While dr.Read
                Dim letter As Letter = Me.CreateObject(dr)
                letterList.Add(letter)
            End While

            Return letterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim letter As Letter = CType(obj, Letter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, letter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim letter As Letter = CType(obj, Letter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, letter.NomorSurat)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, letter.UploadDate)
            DbCommandWrapper.AddInParameter("@Penerima", DbType.AnsiString, letter.Penerima)
            DbCommandWrapper.AddInParameter("@Perihal", DbType.String, letter.Perihal)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiStringFixedLength, letter.UploadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadBy", DbType.AnsiString, letter.LastDownloadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadDate", DbType.DateTime, letter.LastDownloadDate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, letter.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, letter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, letter.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(letter.Dealer))
            DbCommandWrapper.AddInParameter("@DepartmentID", DbType.Int32, Me.GetRefObject(letter.Department))
            DbCommandWrapper.AddInParameter("@KindOfLetterID", DbType.Int32, Me.GetRefObject(letter.KindOfLetter))

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

            Dim letter As Letter = CType(obj, Letter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, letter.ID)
            DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, letter.NomorSurat)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, letter.UploadDate)
            DbCommandWrapper.AddInParameter("@Penerima", DbType.AnsiString, letter.Penerima)
            DbCommandWrapper.AddInParameter("@Perihal", DbType.String, letter.Perihal)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiStringFixedLength, letter.UploadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadBy", DbType.AnsiString, letter.LastDownloadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadDate", DbType.DateTime, letter.LastDownloadDate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, letter.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, letter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, letter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(letter.Dealer))
            DbCommandWrapper.AddInParameter("@DepartmentID", DbType.Int32, Me.GetRefObject(letter.Department))
            DbCommandWrapper.AddInParameter("@KindOfLetterID", DbType.Int32, Me.GetRefObject(letter.KindOfLetter))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Letter

            Dim letter As Letter = New Letter

            letter.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NomorSurat")) Then letter.NomorSurat = dr("NomorSurat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then letter.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Penerima")) Then letter.Penerima = dr("Penerima").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Perihal")) Then letter.Perihal = dr("Perihal").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadBy")) Then letter.UploadBy = dr("UploadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastDownloadBy")) Then letter.LastDownloadBy = dr("LastDownloadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastDownloadDate")) Then letter.LastDownloadDate = CType(dr("LastDownloadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then letter.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then letter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then letter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then letter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then letter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then letter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                letter.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DepartmentID")) Then
                letter.Department = New Department(CType(dr("DepartmentID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("KindOfLetterID")) Then
                letter.KindOfLetter = New KindOfLetter(CType(dr("KindOfLetterID"), Integer))
            End If

            Return letter

        End Function

        Private Sub SetTableName()

            If Not (GetType(Letter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Letter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Letter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

