#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineeLevelDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2019 - 4:18:53 PM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("TrTraineeLevelDetail")> _
    Public Class TrTraineeLevelDetail
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _trTrainee As TrTrainee
        Private _trTraineeLevel As TrTraineeLevel
        Private _tanggalLulus As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _trCourseCategory As TrCourseCategory
        Private _trCertificateConfig As TrCertificateConfig
        Private _namaSiswa As String
        Private _trClassRegistration As String
        Private _certificateNumber As String
        Private _listOfTrClassRegistration As New ArrayList
        Private _rowStatus As Short
        Private _status As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("TrTraineeID", "{0}")> _
        <RelationInfo("TrTrainee", "ID", "TrTraineeLevelDetail", "TrTraineeID")> _
        Public Property TrTrainee As TrTrainee
            Get
                Try
                    If Not IsNothing(Me._trTrainee) AndAlso (Not Me._trTrainee.IsLoaded) Then

                        Me._trTrainee = CType(DoLoad(GetType(TrTrainee).ToString(), _trTrainee.ID), TrTrainee)
                        Me._trTrainee.MarkLoaded()

                    End If

                    Return Me._trTrainee

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTrainee)

                Me._trTrainee = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTrainee.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("TrTraineeLevelID", "{0}"), _
       RelationInfo("TrTraineeLevel", "ID", "TrTraineeLevelDetail", "TrTraineeLevelID")> _
        Public Property TrTraineeLevel() As TrTraineeLevel
            Get
                Try
                    If Not IsNothing(Me._trTraineeLevel) AndAlso (Not Me._trTraineeLevel.IsLoaded) Then

                        Me._trTraineeLevel = CType(DoLoad(GetType(TrTraineeLevel).ToString(), _trTraineeLevel.ID), TrTraineeLevel)
                        Me._trTraineeLevel.MarkLoaded()

                    End If

                    Return Me._trTraineeLevel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTraineeLevel)

                Me._trTraineeLevel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTraineeLevel.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TrCourseCategoryID", "{0}"), _
     RelationInfo("TrCourseCategory", "ID", "TrTraineeLevelDetail", "TrCourseCategoryID")> _
        Public Property TrCourseCategory() As TrCourseCategory
            Get
                Try
                    If Not IsNothing(Me._trCourseCategory) AndAlso (Not Me._trCourseCategory.IsLoaded) Then

                        Me._trCourseCategory = CType(DoLoad(GetType(TrCourseCategory).ToString(), _trCourseCategory.ID), TrCourseCategory)
                        Me._trCourseCategory.MarkLoaded()

                    End If

                    Return Me._trCourseCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrCourseCategory)

                Me._trCourseCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trCourseCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TrCertificateConfigID", "{0}"), _
   RelationInfo("TrCertificateConfig", "ID", "TrTraineeLevelDetail", "TrCertificateConfigID")> _
        Public Property TrCertificateConfig() As TrCertificateConfig
            Get
                Try
                    If Not IsNothing(Me._trCertificateConfig) AndAlso (Not Me._trCertificateConfig.IsLoaded) Then

                        Me._trCertificateConfig = CType(DoLoad(GetType(TrCertificateConfig).ToString(), _trCertificateConfig.ID), TrCertificateConfig)
                        Me._trCertificateConfig.MarkLoaded()

                    End If

                    Return Me._trCertificateConfig

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrCertificateConfig)

                Me._trCertificateConfig = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trCertificateConfig.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TanggalLulus", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TanggalLulus As DateTime
            Get
                Return _tanggalLulus
            End Get
            Set(ByVal value As DateTime)
                _tanggalLulus = value
            End Set
        End Property

        <ColumnInfo("NamaSiswa", "'{0}'")> _
        Public Property NamaSiswa As String
            Get
                Return _namaSiswa
            End Get
            Set(ByVal value As String)
                _namaSiswa = value
            End Set
        End Property

        <ColumnInfo("TrClassRegistration", "'{0}'")> _
        Public Property TrClassRegistration As String
            Get
                Return _trClassRegistration
            End Get
            Set(ByVal value As String)
                _trClassRegistration = value
            End Set
        End Property

        <ColumnInfo("CertificateNumber", "'{0}'")> _
        Public Property CertificateNumber As String
            Get
                Return _certificateNumber
            End Get
            Set(ByVal value As String)
                _certificateNumber = value
            End Set
        End Property

        Public ReadOnly Property ListOfTrClassRegistration As ArrayList
            Get
                If TrClassRegistration <> String.Empty Then
                    Try
                        _listOfTrClassRegistration = New ArrayList
                        Dim splitId() As String = TrClassRegistration.Split(",")

                        For Each ID As String In splitId
                            Dim TrClassRegisData As TrClassRegistration = CType(DoLoad(GetType(TrClassRegistration).ToString(), ID), TrClassRegistration)
                            _listOfTrClassRegistration.Add(TrClassRegisData)
                        Next

                    Catch ex As Exception
                        _listOfTrClassRegistration = New ArrayList
                    End Try
                End If

                Return _listOfTrClassRegistration
            End Get
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property




#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Property"
        Private _classID As Integer
        Public Property ClassID() As Integer
            Get
                Return _classID
            End Get
            Set(ByVal value As Integer)
                _classID = value
            End Set
        End Property

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
