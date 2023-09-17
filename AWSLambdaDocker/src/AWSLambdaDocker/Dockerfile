FROM public.ecr.aws/lambda/dotnet:6 as base

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.16 AS build
WORKDIR /src
COPY AWSLambdaDocker.csproj base/
RUN dotnet restore "base/AWSLambdaDocker.csproj"

WORKDIR "/src"
COPY . .
RUN dotnet build "AWSLambdaDocker.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "AWSLambdaDocker.csproj" -c Release --framework net6.0 --runtime linux-x64 --self-contained false -o /app/publish

FROM base AS final
WORKDIR /var/task
COPY --from=publish /app/publish .
CMD ["AWSLambdaDocker::AWSLambdaDocker.Function::FunctionHandler"]