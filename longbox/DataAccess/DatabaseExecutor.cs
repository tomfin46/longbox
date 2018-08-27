using longbox.Model;
using System;
using System.Threading.Tasks;

namespace longbox.DataAccess
{
    class DatabaseExecutor
    {

        protected void ExecuteWithContext(Action<LongboxContext> action)
        {
            using (var context = new LongboxContext())
            {
                action(context);
            }
        }

        protected Task ExecuteWithContextAsync(Func<LongboxContext, Task> action)
        {
            using (var context = new LongboxContext())
            {
                return action(context);
            }
        }

        protected Task<T> ExecuteWithContextAsync<T>(Func<LongboxContext, Task<T>> action)
        {
            using (var context = new LongboxContext())
            {
                return action(context);
            }
        }
    }
}
