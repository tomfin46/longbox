using Microsoft.EntityFrameworkCore;

namespace longbox.DataAccess
{
    class Database : DatabaseExecutor
    {
        public FolderModel Folders { get; }

        public Database()
        {
            Folders = new FolderModel();
        }

        public void Migrate()
        {
            ExecuteWithContext(context => context.Database.Migrate());
        }
    }
}
