
namespace KTB.DNet.Interface.Framework
{
    public partial class Constants
    {
        public static class ConfigKey
        {
            public const string System_ExpiryTokenUTC = "System_ExpiryTokenUTC";
            public const string System_ExpiryTokenTime = "System_ExpiryTokenTime";

            public const string WebAPI_LoggingOnlyForFailedTransaction_Enable = "WebAPI_LoggingOnlyForFailedTransaction_Enable";
            public const string WebAPI_TransactionRuntime_Enable = "WebAPI_TransactionRuntime_Enable";
            public const string WebAPI_TransactionLogging_Enable = "WebAPI_TransactionLogging_Enable";
            public const string WebAPI_IsDealerAuthChecked = "IsDealerAuthChecked";

            public const string WebUI_TopWidgetRow = "WebUI_TopWidgetRow";
            public const string WebUI_BottomWidgetRow = "WebUI_BottomWidgetRow";
            public const string WebUI_ReadLoggingInFailedTransaction_Display = "WebUI_ReadLoggingInFailedTransaction_Display";
            public const string WebUI_PageSize = "WebUI_PageSize";
            public const string WebUI_ErrorMessageMaxLength = "WebUI_ErrorMessageMaxLength";

        }
    }
}
