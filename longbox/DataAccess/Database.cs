using longbox.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace longbox.DataAccess
{
    class Database
    {
        public void Migrate()
        {
            ExecuteWithContext(context => context.Database.Migrate());
        }

        private void ExecuteWithContext(Action<LongboxContext> action)
        {
            using (var context = new LongboxContext())
            {
                action(context);
            }
        }

        private Task ExecuteWithContextAsync(Func<LongboxContext, Task> action)
        {
            using (var context = new LongboxContext())
            {
                return action(context);
            }
        }
    }
}
