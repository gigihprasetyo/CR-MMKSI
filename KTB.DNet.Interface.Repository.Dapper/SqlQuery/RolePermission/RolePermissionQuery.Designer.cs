//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTB.DNet.Interface.Repository.Dapper.SqlQuery.RolePermission {
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
    internal class RolePermissionQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RolePermissionQuery() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KTB.DNet.Interface.Repository.Dapper.SqlQuery.RolePermission.RolePermissionQuery", typeof(RolePermissionQuery).Assembly);
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
        ///   Looks up a localized string similar to DELETE FROM APIRolePermission WHERE ID IN @listOfId.
        /// </summary>
        internal static string DeleteByListOfId {
            get {
                return ResourceManager.GetString("DeleteByListOfId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///SELECT [Id]
        ///      ,[ClientRoleId]
        ///      ,[PermissionId]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [APIRolePermission] 
        ///  WHERE ClientRoleId = @ClientRoleId.
        /// </summary>
        internal static string GetByClientRoleId {
            get {
                return ResourceManager.GetString("GetByClientRoleId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///SELECT [Id]
        ///      ,[ClientRoleId]
        ///      ,[PermissionId]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [APIRolePermission] 
        ///  WHERE ClientRoleId IN @ClientRoleIds.
        /// </summary>
        internal static string GetByClientRoleIds {
            get {
                return ResourceManager.GetString("GetByClientRoleIds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///SELECT [Id]
        ///      ,[ClientRoleId]
        ///      ,[PermissionId]
        ///      ,[CreatedBy]
        ///      ,[CreatedTime]
        ///      ,[UpdatedBy]
        ///      ,[UpdatedTime]
        ///  FROM [APIRolePermission] 
        ///  WHERE Id IN @Ids.
        /// </summary>
        internal static string GetByIds {
            get {
                return ResourceManager.GetString("GetByIds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT DISTINCT rp.PermissionId 
        ///FROM APIRolePermission rp
        ///JOIN APIClientRole cr ON cr.ClientId = @ClientId AND cr.Id = rp.ClientRoleId
        ///WHERE 
        ///	cr.RoleId IN @ListOfNewRoleId AND
        ///	rp.PermissionId NOT IN @ListOfExistingPermissionId
        ///.
        /// </summary>
        internal static string GetNewPermissionForUserFromNewRole {
            get {
                return ResourceManager.GetString("GetNewPermissionForUserFromNewRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [APIRolePermission]
        ///           ([ClientRoleId]
        ///           ,[PermissionId]
        ///           ,[CreatedBy]
        ///           ,[CreatedTime]
        ///           ,[UpdatedBy]
        ///           ,[UpdatedTime])
        ///OUTPUT INSERTED.ID
        ///     VALUES
        ///           (@ClientRoleId
        ///           ,@PermissionId
        ///           ,@CreatedBy
        ///           ,@CreatedTime
        ///           ,@UpdatedBy
        ///           ,@UpdatedTime)
        ///.
        /// </summary>
        internal static string InsertClientRolePermission {
            get {
                return ResourceManager.GetString("InsertClientRolePermission", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE APIRolePermission WHERE ClientRoleId IN @ListOfClientRoleId.
        /// </summary>
        internal static string RemoveBasedOnRemovedClientRole {
            get {
                return ResourceManager.GetString("RemoveBasedOnRemovedClientRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE rolePermission FROM APIRolePermission rolePermission
        ///JOIN APIClientRole clientRole ON clientRole.ClientId = @ClientId AND clientRole.Id = rolePermission.ClientRoleId  WHERE 
        ///rolePermission.PermissionId IN @ListOfRemovedPermissionId
        ///
        ///
        ///.
        /// </summary>
        internal static string RemovedListOfPermissionByRemovedClientPermission {
            get {
                return ResourceManager.GetString("RemovedListOfPermissionByRemovedClientPermission", resourceCulture);
            }
        }
    }
}
