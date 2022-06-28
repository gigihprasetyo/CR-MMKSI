
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
namespace KTB.DNet.Interface.Repository
{
    public class BaseRepository : BaseControl
    {
        protected string UserLogin { get; set; }

        public void SetUserLogin(string username)
        {
            UserLogin = username;
        }

        protected void SetCreatedLog(BaseDomain domain)
        {
            domain.CreatedBy = UserLogin;
            domain.CreatedTime = DateTime.Now;
            SetLastModifiedLog(domain);
        }

        protected void SetLastModifiedLog(BaseDomain domain)
        {
            domain.UpdatedBy = UserLogin;
            domain.UpdatedTime = DateTime.Now;
        }

        protected TaskFactory _taskFactory = new
        TaskFactory(CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.Default);

        protected TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _taskFactory
                .StartNew(() => func())
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }


        protected void RunSync(Func<Task> func)
        {
            _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        protected void GetPostModelData<T>(DataTablePostModel postModel, out string keyword, string defaultOrderedPropertyName, PropertyInfo orderedProperty, out bool orderDir, out int take, out int skip) where T : class
        {
            keyword = postModel.Search ?? string.Empty;
            keyword = keyword.ToUpper();

            take = postModel.Length;
            skip = postModel.Start;
            string sortBy = defaultOrderedPropertyName;
            orderDir = false;

            if (postModel.Order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = string.IsNullOrEmpty(postModel.Order.Column) ? sortBy : postModel.Order.Column;
                orderDir = postModel.Order.Dir.ToLower() == "asc";
            }

            orderedProperty = typeof(T).GetProperty(sortBy);

        }

        protected void GetPostModelData<T>(DataTablePostModel postModel, out string keyword, string defaultOrderedPropertyName, out PropertyInfo orderedProperty, out bool orderDir, out int take, out int skip) where T : class
        {
            keyword = postModel.Search ?? string.Empty;
            keyword = keyword.ToUpper();

            take = postModel.Length;
            skip = postModel.Start;
            string sortBy = defaultOrderedPropertyName;
            orderDir = false;

            if (postModel.Order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = string.IsNullOrEmpty(postModel.Order.Column) ? sortBy : postModel.Order.Column;
                orderDir = postModel.Order.Dir.ToLower() == "asc";
            }

            orderedProperty = typeof(T).GetProperty(sortBy);
        }

    }
}
