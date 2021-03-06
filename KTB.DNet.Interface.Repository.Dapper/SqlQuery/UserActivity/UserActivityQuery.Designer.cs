//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTB.DNet.Interface.Repository.Dapper.SqlQuery.UserActivity {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class UserActivityQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UserActivityQuery() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KTB.DNet.Interface.Repository.Dapper.SqlQuery.UserActivity.UserActivityQuery", typeof(UserActivityQuery).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM [UserActivity]
        ///WHERE @Id = Id
        ///.
        /// </summary>
        internal static string DeleteUserActivity {
            get {
                return ResourceManager.GetString("DeleteUserActivity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [Id]
        ///      ,[Username]
        ///      ,[Endpoint]
        ///      ,[Activity]
        ///      ,[ActivityTime]
        ///      ,[ActivityDesc]
        ///      ,[DealerCode]
        ///      ,[AppId]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [UserActivity]
        ///
        ///
        ///.
        /// </summary>
        internal static string GetAllUserActivity {
            get {
                return ResourceManager.GetString("GetAllUserActivity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [Id]
        ///    ,[Username]
        ///    ,[Endpoint]
        ///    ,[Activity]
        ///    ,[ActivityTime]
        ///    ,[ActivityDesc]
        ///    ,[DealerCode]
        ///    ,[AppId]
        ///    ,[CreatedBy]
        ///    ,[CreatedTime]
        ///    ,[UpdatedBy]
        ///    ,[UpdatedTime]
        ///FROM [UserActivity]
        ///WHERE Id = @Id.
        /// </summary>
        internal static string GetUserActivityById {
            get {
                return ResourceManager.GetString("GetUserActivityById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [UserActivity]
        ///           ([Username]
        ///           ,[Endpoint]
        ///           ,[Activity]
        ///           ,[ActivityTime]
        ///           ,[ActivityDesc]
        ///           ,[DealerCode]
        ///           ,[AppId]
        ///           ,[CreatedBy]
        ///           ,[CreatedTime]
        ///           ,[UpdatedBy]
        ///           ,[UpdatedTime])
        ///      OUTPUT INSERTED.ID
        ///     VALUES
        ///           (@Username
        ///           ,@Endpoint
        ///           ,@Activity
        ///           ,@ActivityTime
        ///           ,@ActivityDesc
        ///           ,@DealerCode
        ///           ,@AppI [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string InsertUserActivity {
            get {
                return ResourceManager.GetString("InsertUserActivity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///SELECT 
        ///    /**PagingIndexQuery**/
        ///    [Id]
        ///      ,[Username]
        ///      ,[Endpoint]
        ///      ,[Activity]
        ///      ,[ActivityTime]
        ///      ,[ActivityDesc]
        ///      ,[DealerCode]
        ///      ,[AppId]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///	/**EndPagingIndexQuery**/
        ///  FROM [UserActivity]
        ///WHERE ((@Keyword = &apos;&apos; ) 
        ///	OR ((Username LIKE &apos;%&apos; + @Keyword + &apos;%&apos;)
        ///		OR (ActivityDesc LIKE &apos;%&apos; + @Keyword + &apos;%&apos;)))
        ///	AND (@DealerCode = &apos;&apos; OR lower(DealerCode) = lower(@DealerCode))
        ///
        ///.
        /// </summary>
        internal static string SearchUserActivity {
            get {
                return ResourceManager.GetString("SearchUserActivity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE [UserActivity]
        ///   SET [Username] = @Username
        ///      ,[Endpoint] = @Endpoint
        ///      ,[Activity] = @Activity
        ///      ,[ActivityTime] = @ActivityTime
        ///      ,[ActivityDesc] = @ActivityDesc
        ///      ,[DealerCode] = @DealerCode
        ///      ,[AppId] = @AppId
        ///      ,[CreatedBy] = @CreatedBy
        ///      ,[CreatedTime] = @CreatedTime
        ///      ,[UpdatedBy] = @UpdatedBy
        ///      ,[UpdatedTime] = @UpdatedTime
        /// WHERE Id = @Id.
        /// </summary>
        internal static string UpdateUserActivity {
            get {
                return ResourceManager.GetString("UpdateUserActivity", resourceCulture);
            }
        }
    }
}
