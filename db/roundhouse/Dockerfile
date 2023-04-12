FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet tool install -g dotnet-script
RUN dotnet tool install -g dotnet-roundhouse --version 1.2.1

WORKDIR /db

# Copy all db files
COPY . .

ENTRYPOINT [ "dotnet-script", "WaitForMySql.csx", "--", "rh", "--silent", "-dt", "mysql" ]
CMD ["-cs", "Server=host.docker.internal;Database=CloudcallContacts;Uid=dev.admin;Pwd=dev.admin@localhost;SslMode=none;"]