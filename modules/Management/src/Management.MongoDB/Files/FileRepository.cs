using Management.Models;
using Management.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using System.Linq.Dynamic.Core;
using MongoDB.Driver;


namespace Management.Files
{
    public class FileRepository : MongoDbRepository<ManagementMongoDbContext, File, Guid>, IFileRepository
    {
        public FileRepository(IMongoDbContextProvider<ManagementMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<File>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<File, IMongoQueryable<File>>(
                    !string.IsNullOrEmpty(filter),
                    file => !string.IsNullOrEmpty(file.Filename) && file.Filename.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<File>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<File> UploadFileAsync(File file)
        {
            if (file?.DownloadBinary == null)
                throw new ArgumentNullException(nameof(file));

            file.DownloadObjectId = await GridFSBucketUploadFromBytesAsync(file.Filename, file.DownloadBinary);

            file.DownloadBinary = null;

            await InsertAsync(file);

            return file;
        }

        private async Task<string> GridFSBucketUploadFromBytesAsync(string filename, byte[] source)
        {
            var bucket = await GetBucketAsync();
            var id = await bucket.UploadFromBytesAsync(filename, source);
            return id.ToString();
        }

        public async Task<Byte[]?> GetFileByIdAsync(string id)
        {
            var bucket = await GetBucketAsync();

            var objectId = new ObjectId(id);

            var bytes = await bucket.DownloadAsBytesAsync(objectId);

            return bytes;
        }

        public async Task<Byte[]?> GetFileByNameAsync(string name)
        {
            var bucket = await GetBucketAsync();

            var bytes = await bucket.DownloadAsBytesByNameAsync(name);

            return bytes;
        }

        public async Task DeleteFileAsync(File file)
        {
            var bucket = await GetBucketAsync();

            var objectId = new ObjectId(file.DownloadObjectId);

            await bucket.DeleteAsync(objectId);

            await DeleteAsync(file);
        }

        private async Task<GridFSBucket> GetBucketAsync()
        {
            var database = await GetDatabaseAsync();
            return new GridFSBucket(database);
        }
    }
}
