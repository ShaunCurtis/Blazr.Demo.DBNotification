# Blazr.Demo.DBNotification

This repo contains a solution that demonstrates the Event Notifcation pattern using the out-of-the-box Weather Forecast Blazor Template.

There are two runnable projects:

1. Blazr.Demo.DBNotification.Server.Web - runs the solution in a Blazor Server SPA.
2. Blazr.Demo.DBNotification.WASM.Web - runs the server in a Blazor WASM SPA with an API backend server.

The solution shows how to combine the code for WASM and Server projects and how to configure web projects to run the code in both modes.

## Solution Structure

The solution consists of the following projects:

1. Blazr.Demo.DBNotification - the shared application code in a Razor library
2. Blazr.Demo.DBNotification.Server.Web - A web project to run the Blazor Server SPA.  Set as the startup to run the Server SPA.
3. Blazr.Demo.DBNotification.WASM - A project to build out the WASM code for running a WASM SPA.
4. Blazr.Demo.DBNotification.WASM.Server - A web project to run the WASM SPA from.  It also runs the API from the data controllers.  Set as the startup to run the WASM SPA.

## Namespace Structure

The library code is split into three namespaces/domains:

1. *Data Domain* - the data access code.  It should only depend on external libraries and Core Domain project code.
2. *Core Domain* - the Logic/Business/Core application code.  It should only depend on external libraries with no dependancies on other Domain project code.
3. *UI Domain* - the component code.  It should only depend on external libraries and Core Domain project code.

## Data Structure

The data code is structured into the following hierarchy:

1. `WeatherForecastDataProvider` - this is the data source.  Normally either the EF layer or other data layer.  Here we build an internal set of records and provide copies of those record when requested.
2. `IWeatherForecastDataBroker` - the data interface to the logic/business/core application layer.  Defined as an interface to provide abstraction.  In the application there are two implementations: `WeatherForecastServerDataBroker` and `WeatherForecastAPIDataBroker`
3. `WeatherForecastViewService` - the logic/business/core application layer.  This holds the "single version of the truth" for the UI layer.

This may seem overkill for such a small project, but the point is to show how a solution should be structured, with proper separation of concerns.

Note that `WeatherForecast` is an immutable `record`. 

## UI

The original `FetchData` list code has moved into a component `WeatherForecastList`, with added buttons and handler for deleting a record.  There's a `WeatherForecastListHeader` component to show how to drive updates through events.

## Event Notification

The `WeatherForecastViewService` holds the WeatherForecast list, and handles the updates to the WeatherForecast list.  Whenever the list is updated, it raises the `ListUpdated` event.

Any component that needs to react to a `ListUpdated` event, such as `WeatherForecastListHeader`, injects the `WeatherForecastViewService`, registers an event handler with this event, and calls `StateHasChanged`.  As the comnponent is wired to the service data, it access the latest version of the truth.  

In the example both `WeatherForecastList` and `WeatherForecastListHeader` register for this event and re-render when it occurs.
