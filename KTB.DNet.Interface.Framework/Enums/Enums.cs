
namespace KTB.DNet.Interface.Framework.Enums
{
    /// <summary>
    /// Handle Schedule Day Enum
    /// </summary>
    public enum ScheduleDay
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

    public struct DayName
    {
        public const string Monday = "Monday";
        public const string Tuesday = "Tuesday";
        public const string Wednesday = "Wednesday";
        public const string Thursday = "Thursday";
        public const string Friday = "Friday";
        public const string Saturday = "Saturday";
        public const string Sunday = "Sunday";

    }

    /// <summary>
    /// Handle Monthly, Weekly and Daily
    /// </summary>
    public enum ScheduleType
    {
        Monthly = 0,
        Weekly = 1,
        Daily = 2
    }

    /// <summary>
    /// Handle Transaction Type
    /// </summary>
    public enum TransactionType
    {
        Master = 0,
        Transaction = 1,
        Report = 2
    }

    /// <summary>
    /// Handle Operation Type
    /// </summary>
    public enum OperationType
    {
        Read = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
        Deploy = 4
    }

    /// <summary>
    /// Handle UserActivity Type
    /// </summary>
    public enum UserActivityType
    {
        Login = 0,
        Logout = 1,
        Read = 2,
        Create = 3,
        Update = 4,
        Delete = 5,
        Deploy = 6
    }
}
