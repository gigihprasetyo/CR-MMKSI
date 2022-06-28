Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain

Public Class AlertDataRetriever
    Public MinutesToCache As Integer = 15
    Public Function Retrieve() As ArrayList
        Return RetrieveNewlyAddedAlerts()
    End Function

    Public Shared Function GetCurrentDateTime() As DateTime
        Dim dtCurrent As DateTime = DateTime.Now  'New DateTime(2007, 10, 23, 11, 0, 0)
        dtCurrent = New DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day, dtCurrent.Hour, dtCurrent.Minute, 0)

        Return dtCurrent
    End Function

    Private Function RetrieveNewlyAddedAlerts() As ArrayList
        Dim facade As New KTB.DNet.BusinessFacade.AlertManagement.AlertMasterFacade(GetUserPrincipal())

        Dim cc As New KTB.DNet.Domain.Search.CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, 0))
        Dim dtCurrent As DateTime = GetCurrentDateTime()
        Dim dtValid As DateTime = New DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day, 0, 0, 0)

        'e.g. date valid: 1 jan 07 00:00 s/d 2 jan 07 23:59
        cc.opAnd(New Criteria(GetType(AlertMaster), "DateValidFrom", MatchType.LesserOrEqual, dtValid))
        cc.opAnd(New Criteria(GetType(AlertMaster), "DateValidTo", MatchType.GreaterOrEqual, dtValid))

        Dim timeStartFrom As DateTime = New DateTime(1900, 1, 1, dtCurrent.Hour, dtCurrent.Minute, 0)
        Dim timeStartTo As DateTime = New DateTime(1900, 1, 1, dtCurrent.Hour, dtCurrent.Minute, 0)
        timeStartTo = timeStartTo.AddMinutes(MinutesToCache)


        cc.opAnd(New Criteria(GetType(AlertMaster), "TimeStartFrom", MatchType.LesserOrEqual, timeStartFrom), "((((", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "TimeStartTo", MatchType.GreaterOrEqual, timeStartFrom), ")", False)
        cc.opOr(New Criteria(GetType(AlertMaster), "TimeStartFrom", MatchType.LesserOrEqual, timeStartTo), "(", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "TimeStartTo", MatchType.GreaterOrEqual, timeStartTo), "))", False)

        Dim NULL_DATETIME As DateTime = New DateTime(1900, 1, 1, 0, 0, 0, 0)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForDashboard", MatchType.Exact, NULL_DATETIME))
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForAlertBox", MatchType.Exact, NULL_DATETIME))
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForSMS", MatchType.Exact, NULL_DATETIME))
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForEmail", MatchType.Exact, NULL_DATETIME), ")", False)

        'Include allready process and updated alerts
        Dim dtMinutesToCache As DateTime = dtCurrent.AddMinutes(MinutesToCache)
        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForDashboard", MatchType.GreaterOrEqual, dtCurrent), "((", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForDashboard", MatchType.LesserOrEqual, dtMinutesToCache), ")", False)

        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForAlertBox", MatchType.GreaterOrEqual, dtCurrent), "(", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForAlertBox", MatchType.LesserOrEqual, dtMinutesToCache), ")", False)

        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForSMS", MatchType.GreaterOrEqual, dtCurrent), "(", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForSMS", MatchType.LesserOrEqual, dtMinutesToCache), ")", False)

        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForEmail", MatchType.GreaterOrEqual, dtCurrent), "(", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForEmail", MatchType.LesserOrEqual, dtMinutesToCache), ")))", False)


        'Include all alerts whose next run is below current time, just in case our service has been shutdowned
        'cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForDashboard", MatchType.LesserOrEqual, dtCurrent), "(", True)
        'cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForAlertBox", MatchType.LesserOrEqual, dtCurrent))
        'cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForSMS", MatchType.LesserOrEqual, dtCurrent))
        'cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForEmail", MatchType.LesserOrEqual, dtCurrent), ")", False)

        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForDashboard", MatchType.LesserOrEqual, dtCurrent), "((", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForDashboard", MatchType.Greater, NULL_DATETIME), ")", False)

        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForAlertBox", MatchType.LesserOrEqual, dtCurrent), "(", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForAlertBox", MatchType.Greater, NULL_DATETIME), ")", False)

        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForSMS", MatchType.LesserOrEqual, dtCurrent), "(", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForSMS", MatchType.Greater, NULL_DATETIME), ")", False)

        cc.opOr(New Criteria(GetType(AlertMaster), "NextRunForEmail", MatchType.LesserOrEqual, dtCurrent), "(", True)
        cc.opAnd(New Criteria(GetType(AlertMaster), "NextRunForEmail", MatchType.Greater, NULL_DATETIME), "))", False)

        'Only include all alerts whose IsViaAlertBox = 1 OR IsViaSMS = 1 OR IsViaEmail = 1
        cc.opAnd(New Criteria(GetType(AlertMaster), "IsViaAlertBox", MatchType.Exact, 1), "(", True)
        cc.opOr(New Criteria(GetType(AlertMaster), "IsViaSMS", MatchType.Exact, 1))
        cc.opOr(New Criteria(GetType(AlertMaster), "IsViaEmail", MatchType.Exact, 1), ")", False)

        'Exclude one time alert 
        'cc.opAnd(New Criteria(GetType(AlertMaster), "AnnouncementAlertType", MatchType.Exact, CInt(EnumAlertManagement.AnnouncementAlertType.OneTimeAlert)))

        Dim arl As ArrayList = facade.Retrieve(cc)

        Return arl
    End Function

    Public Function RecoverFromShut() As ArrayList

    End Function

    Private Function GetUserPrincipal() As System.Security.Principal.IPrincipal
        Dim ident As New System.Security.Principal.GenericIdentity("000002irsan")
        Dim prin As New System.Security.Principal.GenericPrincipal(ident, New String() {})

        Return prin
    End Function
End Class
