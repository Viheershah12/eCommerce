using Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Management.Files
{
    public interface IFileRepository : IRepository<File, Guid>
    {
        Task<List<File>> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter = null);

        Task<File> UploadFileAsync(File file);

        Task<Byte[]?> GetFileByIdAsync(string id);

        Task<Byte[]?> GetFileByNameAsync(string name);

        Task DeleteFileAsync(File file);
    }
}
