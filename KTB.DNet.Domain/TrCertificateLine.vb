#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCertificateLine Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/17/2005 - 8:15:06 AM
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
    <Serializable(), TableInfo("TrCertificateLine")> _
    Public Class TrCertificateLine
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
        Private _numTestResult As Decimal
        Private _charTestResult As String = String.Empty
        Private _entryType As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _trCourseEvaluation As TrCourseEvaluation
        Private _trClassRegistration As TrClassRegistration

        Private _pODetails As System.Collections.ArrayList = New System.Collections.ArrayList



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("NumTestResult", "#,##0")> _
        Public Property NumTestResult() As Decimal
            Get
                Return _numTestResult
            End Get
            Set(ByVal value As Decimal)
                _numTestResult = value
            End Set
        End Property


        <ColumnInfo("CharTestResult", "'{0}'")> _
        Public Property CharTestResult() As String
            Get
                Return _charTestResult
            End Get
            Set(ByVal value As String)
                _charTestResult = value
            End Set
        End Property


        <ColumnInfo("EntryType", "{0}")> _
        Public Property EntryType() As Short
            Get
                Return _entryType
            End Get
            Set(ByVal value As Short)
                _entryType = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property




        <ColumnInfo("CourseEvaluationID", "{0}"), _
        RelationInfo("TrCourseEvaluation", "ID", "TrCertificateLine", "CourseEvaluationID")> _
        Public Property TrCourseEvaluation() As TrCourseEvaluation
            Get
                Try
                    If Not IsNothing(Me._trCourseEvaluation) AndAlso (Not Me._trCourseEvaluation.IsLoaded) Then

                        Me._trCourseEvaluation = CType(DoLoad(GetType(TrCourseEvaluation).ToString(), _trCourseEvaluation.ID), TrCourseEvaluation)
                        Me._trCourseEvaluation.MarkLoaded()

                    End If

                    Return Me._trCourseEvaluation

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrCourseEvaluation)

                Me._trCourseEvaluation = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trCourseEvaluation.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("RegistrationID", "{0}"), _
        RelationInfo("TrClassRegistration", "ID", "TrCertificateLine", "RegistrationID")> _
        Public Property TrClassRegistration() As TrClassRegistration
            Get
                Try
                    If Not IsNothing(Me._trClassRegistration) AndAlso (Not Me._trClassRegistration.IsLoaded) Then

                        Me._trClassRegistration = CType(DoLoad(GetType(TrClassRegistration).ToString(), _trClassRegistration.ID), TrClassRegistration)
                        Me._trClassRegistration.MarkLoaded()

                    End If

                    Return Me._trClassRegistration

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrClassRegistration)

                Me._trClassRegistration = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trClassRegistration.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

