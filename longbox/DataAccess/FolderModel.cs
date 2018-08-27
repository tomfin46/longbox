using longbox.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace longbox.DataAccess
{
    class FolderModel : DatabaseExecutor
    {
        public Task<List<Folder>> GetAll()
        {
            return ExecuteWithContextAsync(async context =>
            {
                return await context.Folders.ToListAsync();
            });
        }

        public Task Add(Folder folder)
        {
            return ExecuteWithContextAsync(async context =>
            {
                context.Folders.Add(folder);
                await context.SaveChangesAsync();
            });
        }
    }
}
