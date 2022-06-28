using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository
{
    public class DealerRepository : BaseRepository, IDealerRepository<Dealer, int>
    {
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dealer Get(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Dealers.FirstOrDefault(x => x.RowStatus == 0 && x.Id == id);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Get dealer by code
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public Dealer GetByCode(string dealerCode)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Dealers.FirstOrDefault(x => x.RowStatus == 0 && x.DealerCode == dealerCode);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        ///     Gets all 'Dealer' entities as an IQueryable.
        /// </summary>
        /// <returns>An IQueryable of all 'Dealer' entities.</returns>
        public List<Dealer> GetActiveDealers()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Dealers.Where(w => w.RowStatus == 0).ToList();
                }
            }
            catch (Exception ex)
            {

                return new List<Dealer>();
            }
        }

        /// <summary>
        ///     Gets all 'Dealer' entities as an IQueryable.
        /// </summary>
        /// <returns>An IQueryable of all 'Dealer' entities.</returns>
        public List<Dealer> GetAll()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Dealers.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Dealer>();
            }
        }

        /// <summary>
        /// Get dealer count
        /// </summary>
        /// <returns></returns>
        public int GetDealerCount(int userId)
        {
            try
            {
                if (userId == AppConfigs.GetInt("DMSAdminRoleId"))
                {
                    {
                        using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                        {
                            return db.Dealers.Count();
                        }
                    }
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public Framework.ResponseMessage Create(Dealer entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(Dealer entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Dealer> Search(Framework.DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}
