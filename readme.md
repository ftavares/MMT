
## SQL

I choose to use EntityFramwork to query the db, but this sql query gives me the necessary information if hadn't use EntityFramwork.

SELECT *
  FROM [dbo].[ORDERS]
  JOIN [dbo].[ORDERITEMS] ON [dbo].[ORDERS].[ORDERID] = [dbo].[ORDERITEMS].ORDERID
  JOIN [dbo].PRODUCTS ON [dbo].[PRODUCTS].PRODUCTID = [dbo].[ORDERITEMS].PRODUCTID
WHERE [dbo].[ORDERS].ORDERID IN (SELECT top(1) ORDERID FROM [dbo].[ORDERS] where [dbo].[ORDERS].CUSTOMERID= 'C34454' order by [dbo].[ORDERS].ORDERDATE desc)  

## EntityFramwork

I have a Db First approch.

I ran this 2 commands to create the context and the Db Entities.

- dotnet tool install --global dotnet-ef 

- dotnet ef dbcontext scaffold "[connectionString]" Microsoft.EntityFrameworkCore.SqlServer -o Entities -t "[dbo].[Products]" -t "[dbo].[Orders]" -t "[dbo].[OrderItems]"      

You want to run it again please replace [connectionString] with the correct connectionString, and then it from the SSE_TestContext Class. 
If the context and the Entities already exists and you want to override them add "-f" to the command.

## Logging

- I'm using Serilog, that is writing the logs to file and console, but it allows to configure other outputs, ex ApplicationInsights.
- You can find a full list [here.](https://github.com/serilog/serilog/wiki/Provided-Sinks)

## Model Validation

- For model validation i'm using FluentValidator that gives me to add validation rules for each model property.
- Each validation rule have a very specific error message.

## Error Handling

- For Error Handling i created a custom error controller, that allow specific exceptions to have a proper Status Code.
- All the Other will have 500 Status Code.
- All Error are logged.

## AppSettings

- The appSettings.json on the MMT.Api project, have 3 properties.
- When releasing you should add the values for "ConnectionStrings.connectionString" , "CustomerDetailsApi.BaseUrl" and "CustomerDetailsApi.Key".
- If you need to run this solution locally,  create a file "appsettings.development.json", copy all appsettings.json content, and add the variables.

**Do not commit appsettings.development.json**

## Testing

- Testing project for MMT.Application 
- Testing project needed for MMT.Api 
- Mocks created for Context, CustomerDetailsService, Logger, and Mapper.
- I need to add test for : parameter validation, data validation (ex. address building, product name on Gift ).


## Security

Api should required a Api Token

## Api Tests

I created a PostMan Collection, MMT.postman_collection.json, with several tests calls to the api. 

## Swaggger

You can access swagger [here](https://localhost:44345/swagger/index.html).