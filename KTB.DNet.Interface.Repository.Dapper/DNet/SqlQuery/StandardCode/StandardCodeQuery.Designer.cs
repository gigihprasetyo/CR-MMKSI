//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTB.DNet.Interface.Repository.Dapper.DNet.SqlQuery.StandardCode {
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
    internal class StandardCodeQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StandardCodeQuery() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KTB.DNet.Interface.Repository.Dapper.DNet.SqlQuery.StandardCode.StandardCodeQuery" +
                            "", typeof(StandardCodeQuery).Assembly);
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
        ///   Looks up a localized string similar to DELETE FROM [StandardCode]
        ///      WHERE ID = @Id
        ///
        ///
        ///.
        /// </summary>
        internal static string DeleteStandardCode {
            get {
                return ResourceManager.GetString("DeleteStandardCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///SELECT * FROM StandardCode
        ///
        ///
        ///.
        /// </summary>
        internal static string GetAllStandardCode {
            get {
                return ResourceManager.GetString("GetAllStandardCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///	ID,
        ///	Category,
        ///	ValueId,
        ///	ValueCode,
        ///	ValueDesc,
        ///	Sequence,
        ///	RowStatus,
        ///	CreatedBy,
        ///	CreatedTime,
        ///	LastUpdateBy,
        ///	LastUpdateTime
        ///FROM StandardCode
        ///WHERE Category = @Category
        ///ORDER BY Sequence.
        /// </summary>
        internal static string GetStandardCodeByCategory {
            get {
                return ResourceManager.GetString("GetStandardCodeByCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///SELECT 
        ///	ID,
        ///	Category,
        ///	ValueId,
        ///	ValueCode,
        ///	ValueDesc,
        ///	Sequence,
        ///	RowStatus,
        ///	CreatedBy,
        ///	CreatedTime,
        ///	LastUpdateBy,
        ///	LastUpdateTime
        ///FROM StandardCode
        ///WHERE ID = @Id.
        /// </summary>
        internal static string GetStandardCodeById {
            get {
                return ResourceManager.GetString("GetStandardCodeById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///
        ///INSERT INTO StandardCode
        ///(
        ///	Category,
        ///	ValueId,
        ///	ValueCode,
        ///	ValueDesc,
        ///	Sequence,
        ///	RowStatus,
        ///	CreatedBy,
        ///	CreatedTime,
        ///	LastUpdateBy,
        ///	LastUpdateTime
        ///)
        ///OUTPUT INSERTED.ID
        ///VALUES
        ///(
        ///	@Category,
        ///	@ValueId,
        ///	@ValueCode,
        ///	@ValueDesc,
        ///	@Sequence,
        ///	@RowStatus,
        ///	@CreatedBy,
        ///	@CreatedTime,
        ///	@LastUpdateBy,
        ///	@LastUpdateTime
        ///);  
        ///
        ///
        ///.
        /// </summary>
        internal static string InsertStandardCode {
            get {
                return ResourceManager.GetString("InsertStandardCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ////**PagingIndexQuery**/
        ///	ID,
        ///	Category,
        ///	ValueId,
        ///	ValueCode,
        ///	ValueDesc,
        ///	Sequence,
        ///	RowStatus,
        ///	CreatedBy,
        ///	CreatedTime,
        ///	LastUpdateBy,
        ///	LastUpdateTime
        ///	/**EndPagingIndexQuery**/
        ///FROM StandardCode
        ///WHERE
        ///	@Keyword = &apos;&apos; OR
        ///	ValueCode LIKE &apos;%&apos;+@Keyword+&apos;%&apos; OR
        ///	ValueDesc LIKE &apos;%&apos;+@Keyword+&apos;%&apos; OR
        ///	Category LIKE &apos;%&apos;+@Keyword+&apos;%&apos;
        ///	
        ///
        ///.
        /// </summary>
        internal static string SearchStandardCode {
            get {
                return ResourceManager.GetString("SearchStandardCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE StandardCode
        ///   SET 
        ///	Category = @Category,
        ///	ValueId = @ValueId,
        ///	ValueCode = @ValueCode,
        ///	ValueDesc = @ValueDesc,
        ///	Sequence = @Sequence,
        ///	RowStatus = @RowStatus,
        ///	CreatedBy = @CreatedBy,
        ///	CreatedTime = @CreatedTime,
        ///	LastUpdateBy = @LastUpdateBy,
        ///	LastUpdateTime = @LastUpdateTime
        /// WHERE ID = @ID
        ///
        ///
        /// 
        ///
        ///
        ///.
        /// </summary>
        internal static string UpdateStandardCode {
            get {
                return ResourceManager.GetString("UpdateStandardCode", resourceCulture);
            }
        }
    }
}
