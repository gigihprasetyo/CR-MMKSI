#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 10/12/2005 - 8:26:12 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Reflection

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.Helper

    Public Class HelperFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_userMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal, ByVal domainType As Type)
            Me.m_userPrincipal = userPrincipal
            Me.m_userMapper = MapperFactory.GetInstance.GetMapper(domainType.ToString())
        End Sub

#End Region

#Region "Custom Method"

        Public Function IsRecordExist(ByVal criteria As ICriteria, ByVal aggregate As IAggregate) As Boolean
            If CType(m_userMapper.RetrieveScalar(aggregate, criteria), Integer) > 0 Then
                Return True
            End If

            Return False
        End Function

        Public Function RecordCount(ByVal criteria As ICriteria, ByVal aggregate As IAggregate) As Integer
            Return CType(m_userMapper.RetrieveScalar(aggregate, criteria), Integer)
        End Function

#End Region

    End Class

    Public Class ListComparer
        Implements IComparer

        Private m_IsAsc As Boolean
        Private m_ColumnName As String

        Public Sub New(ByVal IsAsc As Boolean, ByVal ColumnName As String)
            Me.m_IsAsc = IsAsc
            Me.m_ColumnName = ColumnName
        End Sub

        'Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
        '    Implements IComparer.Compare

        '    Dim colNames As Array

        '    Dim objXType As Type = x.GetType()
        '    Dim objXValue As Object = objXType.GetProperty(Me.m_ColumnName).GetValue(x, BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.Public, _
        '        System.Type.DefaultBinder, Nothing, Nothing)

        '    Dim objYType As Type = y.GetType()
        '    Dim objYValue As Object = objYType.GetProperty(Me.m_ColumnName).GetValue(y, BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.Public, _
        '        System.Type.DefaultBinder, Nothing, Nothing)

        '    Return IIf(m_IsAsc, (New CaseInsensitiveComparer).Compare(objXValue, objYValue), _
        '        (New CaseInsensitiveComparer).Compare(objYValue, objXValue))
        'End Function

        Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
            Implements IComparer.Compare

            Dim splittedCol As String()
            splittedCol = m_ColumnName.Split(".")
            Dim tipeX As Type = x.GetType()
            Dim tipeY As Type = y.GetType()
            Dim valX As Object = x
            Dim valY As Object = y

            If splittedCol.Length = 1 Then
                valX = tipeX.GetProperty(m_ColumnName).GetValue(x, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                valY = tipeY.GetProperty(m_ColumnName).GetValue(y, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
            Else
                Dim counter As Integer
                For counter = 0 To (splittedCol.Length - 2)
                    Try
                        valX = tipeX.GetProperty(splittedCol(counter)).GetValue(valX, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                        valY = tipeY.GetProperty(splittedCol(counter)).GetValue(valY, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                        If valX.GetType.Name = splittedCol(counter) Then
                            tipeX = valX.GetType
                            tipeY = valY.GetType
                        End If
                    Catch ex As TypeLoadException
                        Throw ex
                    Catch exp As Exception
                        Throw exp
                    End Try

                    If counter = splittedCol.Length - 2 Then '
                        valX = tipeX.GetProperty(splittedCol(splittedCol.Length - 1)).GetValue(valX, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                        valY = tipeY.GetProperty(splittedCol(splittedCol.Length - 1)).GetValue(valY, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    End If
                Next
            End If

            Return IIf(m_IsAsc, (New CaseInsensitiveComparer).Compare(valX, valY), _
                (New CaseInsensitiveComparer).Compare(valY, valX))
        End Function
    End Class

    Public Class ArrayListPager
        Public Shared Function DoPage(ByVal objList As ArrayList, ByVal pageNumber As Integer, ByVal pageSize As Integer) As ArrayList

            If (objList.Count / pageSize) > pageNumber Then
                Dim returnList As ArrayList = New ArrayList
                Dim counter As Integer
                Dim startIndex As Integer = pageNumber * pageSize
                Dim EndIndex As Integer = startIndex + pageSize - 1
                For counter = startIndex To EndIndex
                    If objList.Count > counter Then
                        returnList.Add(objList.Item(counter))
                    End If
                Next
                Return returnList
            Else
                Return Nothing
            End If

        End Function
    End Class
End Namespace