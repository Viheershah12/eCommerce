﻿using Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Management.Contents
{
    public interface IContentRepository : IRepository<Content, Guid>
    {
        Task<List<Content>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null);
    }
}
