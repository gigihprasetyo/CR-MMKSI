using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.BusinessValidation
{
    public static class StandardCodeHelper
    {
        #region variables
        private static readonly IMapper _standardcodeMapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
        private static readonly IMapper _standardcodeCharMapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCodeChar).ToString());
        #endregion

        /// <summary>
        /// Get enum by category and value id
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StandardCode GetByCategoryAndValue(string category, string value)
        {
            var result = new StandardCode();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.EndsWith, category));
                criterias.opAnd(new Criteria(typeof(StandardCode), "ValueId", MatchType.Exact, value));

                var data = _standardcodeMapper.RetrieveByCriteria(criterias).OfType<StandardCode>().ToList();
                if (data.Count > 0)
                {
                    result = data.FirstOrDefault();
                }
                else
                {
                    throw new Exception(string.Format("Enum Data yang dicari di Kategori  {0} tidak ditemukan di dalam database.", category + " ValueId " + value));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Get enum by category and value id
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StandardCodeChar GetCharByCategoryAndValue(string category, string value)
        {
            var result = new StandardCodeChar();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCodeChar), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StandardCodeChar), "Category", MatchType.EndsWith, category));
                criterias.opAnd(new Criteria(typeof(StandardCodeChar), "ValueId", MatchType.Exact, value));

                var data = _standardcodeCharMapper.RetrieveByCriteria(criterias).OfType<StandardCodeChar>().ToList();
                if (data.Count > 0)
                {
                    result = data.FirstOrDefault();
                }
                else
                {
                    throw new Exception(string.Format("Enum Data yang dicari di Kategori  {0} tidak ditemukan di dalam database.", category + " ValueId " + value));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Get by category and code
        /// </summary>
        /// <param name="category"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static StandardCode GetByCategoryAndCode(string category, string code)
        {
            var result = new StandardCode();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.EndsWith, category));
                criterias.opAnd(new Criteria(typeof(StandardCode), "ValueCode", MatchType.Exact, code));

                var data = _standardcodeMapper.RetrieveByCriteria(criterias).OfType<StandardCode>().ToList(); ;
                if (data.Count > 0)
                {
                    result = data.FirstOrDefault();
                }
                else
                {
                    throw new Exception(string.Format("Enum Data yang dicari di Kategori  {0} tidak ditemukan di dalam database.", category));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Get standard code by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static List<StandardCode> GetByCategory(string category)
        {
            var result = new List<StandardCode>();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.EndsWith, category));

                var data = _standardcodeMapper.RetrieveByCriteria(criterias).OfType<StandardCode>().ToList(); ;
                if (data.Count > 0)
                {
                    result = data;
                }
                else
                {
                    throw new Exception(string.Format("Enum Data yang dicari di Kategori  {0} tidak ditemukan di dalam database.", category));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Check whether the passed category and desc are exist in database
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsExistByCategoryAndCode(string category, string code)
        {
            try
            {
                GetByCategoryAndCode(category, code);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the passed category and value are exist in database
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsExistByCategoryAndValue(string category, string value)
        {
            try
            {
                GetByCategoryAndValue(category, value);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
