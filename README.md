# Instructions

## Setting up Database

### Using RoundehousE

Launch local database and run migration scripts using https://github.com/chucknorris/roundhouse

```
docker-compose -f docker-compose.dev-env.yml up
```

### Using Flyway

Launch local database and run migration scripts using https://github.com/flyway/flyway-docker

```
docker-compose -f docker-compose.dev-env.flyway.yml up
```

## Tear down Database

```
docker-compose -f docker-compose.dev-env.yml down -v --rmi local --remove-orphans
```

OR

```
docker-compose -f docker-compose.dev-env.flyway.yml down -v --rmi local --remove-orphans
```

## Running the app

Run the app locally:

```
docker-compose up --build
```

Navigate to http://localhost:1001/swagger/index.html to load Swagger UI.

### Testing

Run the integration tests:

```
dotnet test
```

#### Code Coverage

https://codeburst.io/code-coverage-in-net-core-projects-c3d6536fd7d7

Install Coverlet nuget package - https://github.com/coverlet-coverage/coverlet.

Run the integration tests with code coverage:

```
dotnet test --collect:"XPlat Code Coverage"
```

Run with specific format output(s):

```
dotnet test --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=json,cobertura,lcov
```

#### Reporting

Install Report Generator - https://github.com/danielpalme/ReportGenerator.

```
dotnet tool install dotnet-reportgenerator-globaltool --tool-path tools
```

Run report:

```
./tools/reportgenerator -reports:.\tests\LocalMySql.Api.Infrastructure.Integration.Tests\TestResults\*\coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html
```