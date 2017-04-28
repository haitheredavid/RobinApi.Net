# RobinApi.Net

A C#/.net wrapper for Robin's API

This README is a work in progress.

Full API documentation is available at [docs.robinpowered.com](http://docs.robinpowered.com).

# Installation

RobinApi.Net can easily be installed using the NuGet package

```
Install-Package RobinApi
```

# Getting started

Using the Robin API requires an API access token, which can be generated by logging in a https://dashboard.robinpowered.com.

Once you have an access token, you can instantiate the `RobinApiClient` class to easily make API calls:

```
RobinApiClient client = new RobinApiClient("my token");
Presence[] presence = await client.GetSpacePresence(mySpaceId);
```
