using Hangfire;
using Microsoft.Owin;
using Owin;
using WebGrease;
using KTB.DNet.Web.Scheduling.Helper;
using Hangfire.Dashboard;
using System;
using KTB.DNet.SFIntegration.BusinessLogic;
using KTB.DNet.Web.Scheduling.Controllers;
using KTB.DNet.SFIntegration.SchedulingNonSF;
using KTB.DNet.SFIntegration.SchedullingSF;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(KTB.DNet.Web.Scheduling.Startup))]
namespace KTB.DNet.Web.Scheduling
{

    public partial class Startup
    {
        //public void Configuration(IAppBuilder app)
        //{
        //    ConfigureAuth(app);
        //}
        //+++++++++++++++++++++++++++==
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");

            app.UseHangfireDashboard();


            app.UseHangfireDashboard("/hangfire");


            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(
                () => VehicleMaster.GetVehicleMasterData(),
                Cron.Daily(3),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => LeadCustomerLogic.Write(),
                 "0 0 * ? * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );
            RecurringJob.AddOrUpdate(
                () => EmailController.SendSchedulerReporting(),
                Cron.Daily(7),
                //"*/5 * * * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => LeadCustomerLogic.Read(),
                 "0 0 * ? * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
             () => DealerMaster.GetDealerMasterData(),
             Cron.Daily(4),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
         );

            //RecurringJob.AddOrUpdate(
            // () => TeleSurveyMaster.GetTeleSurveyMasterData(),
            // Cron.Hourly(),
            // TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);

            RecurringJob.AddOrUpdate(
                () => SalesOrderLogic.ResendWS_SalesOrder(),
                "0 0 */4 ? * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => SPBillingLogic.WSResend_SPBilling(),
                Cron.Daily(4),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
             () => SparePartMasterPriceLogic.UpdateSparePartMasterRetalPrice(),
             Cron.Daily(2),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
         );
            //------------------------------GLOBAL SERVICE REMINDER ----------------------------------//
            //takeout berdasarkan keputusan bersama
            //RecurringJob.AddOrUpdate(
            //    () => ServiceReminderMaster.GenerateServiceReminder(),
            //    "0 * * * 7",
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);
            //end takeout

            RecurringJob.AddOrUpdate(
                () => ServiceReminderMaster.SendSvcReminderToSF(),
                "0 6,12 * * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            //RecurringJob.AddOrUpdate(
            //    () => ServiceReminderMaster.GenerateServiceReminder(),
            //    "0 5 * * 6",
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);

            //takeout
            //RecurringJob.AddOrUpdate(
            //    () => ServiceReminderMaster.repairData(),
            //    "0 */1 * * *",
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);
            //end takeout

            //---------------------------END GLOBAL SERVICE REMINDER ----------------------------------//

            ///Salesforce scheduller
            RecurringJob.AddOrUpdate(
                () => DealerContactLogic.WSSalesforce_Contact(bool.Parse(WebConfigurationManager.AppSettings["InitData"])),
                Cron.MinuteInterval(30),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => SmartPackageLogic.WSSalesforce_SmartPackage(bool.Parse(WebConfigurationManager.AppSettings["InitData"])),
                Cron.MinuteInterval(30),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            //takeout
            RecurringJob.AddOrUpdate(
                () => PurchaseTransactionLogic.WSSalesforce_PurchaseTransaction(bool.Parse(WebConfigurationManager.AppSettings["InitData"])),
                "0 0-5,8-15,20-23 * * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );
            //end takeout

            //takeout
            //RecurringJob.AddOrUpdate(
            //    () => ServiceHistoryLogic.WSSalesforce_ServiceHistory(bool.Parse(WebConfigurationManager.AppSettings["InitData"])),
            //    Cron.MinuteInterval(30),
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);
            //end takeout

            RecurringJob.AddOrUpdate(
                () => DiscountProposalLogic.SendMail(),
                //"0 0 */4 ? * *",
                Cron.MinuteInterval(10),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            //takeout
            //RecurringJob.AddOrUpdate(
            //    () => LeadEmailBroadcastLogic.Send(),
            //    Cron.Daily(8, 0),
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);
            //end takeout   

            RecurringJob.AddOrUpdate(
                () => CBUReturnReminder.SendMail(),
                "0 9,13,15 * * MON-FRI",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => CustomerCaseBroadcastNotif.Notify(),
                "0 11 * * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                 () => SPBackOrderLogic.SendMail(),
                 Cron.Hourly(),
                 TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                 () => SPBackOrderLogic.GenerateTextFiletoSAP(),
                 Cron.Hourly(),
                 TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            //takeout
            //RecurringJob.AddOrUpdate(
            //    () => LeadCustomerLogic.WriteLeadSource(),
            //    "0 0 * ? * *",
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);
            //end takeout

            RecurringJob.AddOrUpdate(
                () => ServiceDueDateReminderLogic.SendMail(),
                "0 9 * * MON-FRI",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => ServiceDueDateReminderLogic.SendMailMSPRegular(),
                "0 9 * * MON-FRI",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            /*
            //Document Service Outstanding
            RecurringJob.AddOrUpdate(
                () => ServiceDueDateReminderLogic.SendMailDocumentServiceOutstanding(),
                "0 3 * * MON-FRI",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );
            */

            //RecurringJob.AddOrUpdate(
            //    () => MSPRegistrationLogic.GenerateTextFiletoSAP(),
            //    "0 3 * * *",
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);

            //RecurringJob.AddOrUpdate(
            //    () => CBUReturnReminder.SendMail(),
            //    "0 9,13,15 * * MON-FRI",
            //    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            //);

            RecurringJob.AddOrUpdate(
                () => CustomerCaseResponseLogic.Process(),
                Cron.Hourly(),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => ServiceMessage.Resend(),
                Cron.Daily(10),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => PDIPKTEmailBroadcastLogic.Send(),
                "00 09 * * 1,4",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => AlarmWAEmailBroadcastLogic.Send(),
                Cron.Daily(8),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => PDIPKTService.Monitoring(),
                Cron.Hourly(20),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => SPKNationalEventLogic.SendMail(),
                // "0 */1 * * *",
                       "0 8 * * 1",
                //     Cron.Hourly(),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            //takeout
            RecurringJob.AddOrUpdate(
                () => GSRRilisLogic.Process(),
                "*/30 * * * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );
            //end takeout

            RecurringJob.AddOrUpdate(
                () => StallWorkingEmailBroadcastLogic.Send(),
                "0 9 * * 2",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            RecurringJob.AddOrUpdate(
                () => MessageSchedule.Send(),
                Cron.MinuteInterval(30),
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );

            
            RecurringJob.AddOrUpdate(
                () => ServiceHistoryBookletLogic.WSSalesforce_ServiceHistory(bool.Parse(WebConfigurationManager.AppSettings["InitData"])),
                "0 * * * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );


            
            RecurringJob.AddOrUpdate(
                () => SparepartHistoryLogic.WSSalesforce_SparepartHistory(bool.Parse(WebConfigurationManager.AppSettings["InitData"])),
                "15 * * * *",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );


            RecurringJob.AddOrUpdate(
                () => IndenPartExpiredReminderLogic.SendMailIndenPartExpired(),
                "0 3 * * MON-FRI",
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            );
        }
    }
}
