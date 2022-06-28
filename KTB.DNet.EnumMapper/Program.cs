using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.ConsoleApp
{
    /// <summary>
    /// Enum classes reader from domain dll
    /// </summary>
    public class Program
    {
        // constant
        private const string DNET_DOMAIN = "KTB.DNet.Domain";
        private const string USER = "ADMIN";

        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // get the enum list
            var result = GetEnumList();

            // insert into db
            InsertToDB(result);
        }

        /// <summary>
        /// Insert the enum to DB
        /// </summary>
        /// <param name="enumList"></param>
        private static void InsertToDB(List<EnumModel> enumList)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                // Create the connectionString
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();

                // proceed the list
                foreach (var item in enumList)
                {
                    foreach (var value in item.Values)
                    {
                        // Create the command, to insert the data into the Table!
                        SqlCommand insertCommand = new SqlCommand("INSERT INTO StandardCode (Category, ValueId, ValueDesc, CreatedBy, CreatedTime, RowStatus) VALUES (@Category, @ValueId, @ValueDesc, @CreatedBy, @CreatedTime, @RowStatus)", conn);

                        // replace the parameters
                        insertCommand.Parameters.Add(new SqlParameter("Category", item.Name));
                        insertCommand.Parameters.Add(new SqlParameter("ValueId", value.Value));
                        insertCommand.Parameters.Add(new SqlParameter("ValueDesc", value.Key));
                        insertCommand.Parameters.Add(new SqlParameter("CreatedBy", USER));
                        insertCommand.Parameters.Add(new SqlParameter("CreatedTime", DateTime.Now));
                        insertCommand.Parameters.Add("@RowStatus", SqlDbType.Int);
                        insertCommand.Parameters["@RowStatus"].Value = 0;

                        insertCommand.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Done! Press enter to exit");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Get enum model list
        /// </summary>
        /// <returns></returns>
        private static List<EnumModel> GetEnumList()
        {
            // declaress
            var result = new List<EnumModel>();
            bool isValid = false;

            // get enum object list using reflection
            var allEnums = Assembly.Load(DNET_DOMAIN).GetTypes().Where(a => a.IsEnum).ToList();

            // repopulate the enum and its values
            foreach (var item in allEnums)
            {
                // special handling for status enum
                string enumName = item.Name;
                if (item.DeclaringType != null)
                    enumName = item.DeclaringType.Name + "." + item.Name;

                // isntantiate the model
                var newEnum = new EnumModel(enumName);

                // populate all keys and values
                foreach (var fieldInfo in item.GetFields())
                {
                    // enum validation
                    if (fieldInfo.FieldType.IsEnum)
                    {
                        // update the flag
                        isValid = true;

                        // add to values list
                        newEnum.Values.Add(fieldInfo.Name, fieldInfo.GetRawConstantValue());
                    }
                }

                // check if its valid enum
                if (isValid)
                {
                    // add it to list
                    result.Add(newEnum);

                    // reset the flag
                    isValid = false;
                }
            }

            return result;
        }
    }
}
