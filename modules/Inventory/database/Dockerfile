FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
COPY . .

WORKDIR /templates/service/host/IdentityServerHost
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-IdentityServerHost.sql

WORKDIR /templates/service/host/Inventory.Host
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-Inventory.sql

FROM mcr.microsoft.com/mssql-tools AS final
WORKDIR /src
COPY --from=build /templates/service/host/IdentityServerHost/migrations-IdentityServerHost.sql migrations-IdentityServerHost.sql
COPY --from=build /templates/service/host/Inventory.Host/migrations-Inventory.sql migrations-Inventory.sql
COPY --from=build /templates/service/database/entrypoint.sh .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]