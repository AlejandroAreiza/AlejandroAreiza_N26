# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
RUN apt-get update && apt-get install -y curl

WORKDIR /app

COPY . ./

RUN dotnet restore Petstore.ApiTAF.sln


# Dynamically build all test projects (those ending with .tests.csproj)
RUN find . -name "*.tests.csproj" -exec dotnet build {} -c Release --no-restore \;

# ------------------- Test runner stage -------------------
FROM build AS testrunner

# Define optional build-time and runtime variables
ARG TEST_PROJECT=pet
ARG TEST_FILTER=""

ENV TEST_PROJECT=$TEST_PROJECT
ENV TEST_FILTER=$TEST_FILTER

# Copy the entrypoint script and make it executable
COPY docker/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Set default entrypoint
ENTRYPOINT ["/entrypoint.sh"]
