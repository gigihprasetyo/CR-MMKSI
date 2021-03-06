//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTB.DNet.Interface.Repository.Dapper.SqlQuery.MsApplication {
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
    internal class MsApplicationQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MsApplicationQuery() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KTB.DNet.Interface.Repository.Dapper.SqlQuery.MsApplication.MsApplicationQuery", typeof(MsApplicationQuery).Assembly);
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
        ///   Looks up a localized string similar to DELETE FROM [MsApplicationPermission]
        ///      WHERE AppId = @AppId
        ///      
        ///DELETE FROM [MsApplication]
        ///      WHERE AppId = @AppId.
        /// </summary>
        internal static string DeleteApplication {
            get {
                return ResourceManager.GetString("DeleteApplication", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM [MsApplicationPermission]
        ///      WHERE Id in @ListOfId
        ///      .
        /// </summary>
        internal static string DeleteApplicationPermissions {
            get {
                return ResourceManager.GetString("DeleteApplicationPermissions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [AppId]
        ///      ,[Name]
        ///      ,[DeploymentJenkinsJobName]
        ///      ,[DeploymentBackupFolder]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [MsApplication].
        /// </summary>
        internal static string GetAllApplication {
            get {
                return ResourceManager.GetString("GetAllApplication", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [AppId]
        ///      ,[Name]
        ///      ,[DeploymentJenkinsJobName]
        ///      ,[DeploymentBackupFolder]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [MsApplication]
        ///WHERE AppId = @Id.
        /// </summary>
        internal static string GetApplicationById {
            get {
                return ResourceManager.GetString("GetApplicationById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [AppId]
        ///      ,[Name]
        ///      ,[DeploymentJenkinsJobName]
        ///      ,[DeploymentBackupFolder]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [MsApplication]
        ///WHERE Name = @Name.
        /// </summary>
        internal static string GetApplicationByName {
            get {
                return ResourceManager.GetString("GetApplicationByName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [Id]
        ///      ,[AppId]
        ///      ,[PermissionId]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [MsApplicationPermission]
        ///WHERE AppId = @AppId
        ///
        ///
        ///.
        /// </summary>
        internal static string GetApplicationPermissionByAppId {
            get {
                return ResourceManager.GetString("GetApplicationPermissionByAppId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Application.[AppId]
        ///      ,Application.[Name]
        ///      ,Application.[DeploymentJenkinsJobName]
        ///      ,Application.[DeploymentBackupFolder]
        ///  FROM [MsApplication] AS Application
        ///  INNER JOIN [APIClient] AS Client ON Application.AppId = Client.AppId
        ///
        ///WHERE Client.ClientId = @clientId.
        /// </summary>
        internal static string GetByClientId {
            get {
                return ResourceManager.GetString("GetByClientId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT EndpointPermission.[Id]
        ///      ,EndpointPermission.[Name]
        ///      ,EndpointPermission.[PermissionCode]
        ///      ,EndpointPermission.[URI]
        ///      ,EndpointPermission.[EndpointType]
        ///      ,EndpointPermission.[OperationType]
        ///      ,EndpointPermission.[Description]
        ///      ,EndpointPermission.[IsScheduled]
        ///      ,EndpointPermission.[CreatedBy]
        ///      ,EndpointPermission.[CreatedTime]
        ///      ,EndpointPermission.[UpdatedBy]
        ///      ,EndpointPermission.[UpdatedTime]
        ///  FROM [APIEndpointPermission] As Endpoint [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetPermissionByAppId {
            get {
                return ResourceManager.GetString("GetPermissionByAppId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [MsApplication]
        ///           ([AppId]
        ///           ,[Name]
        ///           ,[DeploymentJenkinsJobName]
        ///           ,[DeploymentBackupFolder]
        ///           ,[CreatedBy]
        ///           ,[CreatedTime]
        ///           ,[UpdatedBy]
        ///           ,[UpdatedTime])
        ///     VALUES
        ///           (@AppId
        ///           ,@Name
        ///           ,@DeploymentJenkinsJobName
        ///           ,@DeploymentBackupFolder
        ///           ,@CreatedBy
        ///           ,@CreatedTime
        ///           ,@UpdatedBy
        ///           ,@UpdatedTime)
        ///.
        /// </summary>
        internal static string InsertApplication {
            get {
                return ResourceManager.GetString("InsertApplication", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    /**PagingIndexQuery**/
        ///      [AppId]
        ///      ,[Name]
        ///      ,[DeploymentJenkinsJobName]
        ///      ,[DeploymentBackupFolder]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///    /**EndPagingIndexQuery**/
        ///FROM [MsApplication]
        ///WHERE @Keyword = &apos;&apos; OR Name LIKE &apos;%&apos; + @Keyword + &apos;%&apos;
        ///.
        /// </summary>
        internal static string SearchApplication {
            get {
                return ResourceManager.GetString("SearchApplication", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE [MsApplication]
        ///   SET [AppId] = @AppId
        ///      ,[Name] = @Name
        ///      ,[DeploymentJenkinsJobName] = @DeploymentJenkinsJobName
        ///      ,[DeploymentBackupFolder] = @DeploymentBackupFolder
        ///      ,[UpdatedBy] = @UpdatedBy
        ///      ,[UpdatedTime] = @UpdatedTime
        /// WHERE AppId = @AppId
        ///
        ///
        ///.
        /// </summary>
        internal static string UpdateApplication {
            get {
                return ResourceManager.GetString("UpdateApplication", resourceCulture);
            }
        }
    }
}
