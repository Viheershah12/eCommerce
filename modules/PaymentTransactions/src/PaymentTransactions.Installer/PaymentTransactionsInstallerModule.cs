﻿using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace PaymentTransactions;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class PaymentTransactionsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PaymentTransactionsInstallerModule>();
        });
    }
}
