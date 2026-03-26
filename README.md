# Hacker News Best Stories API

## Description

This project is a RESTful API built with ASP.NET Core that retrieves the best stories from the Hacker News API and returns the top `n` stories ordered by score.

## How to Run

Requirements:

* .NET 8 SDK

Steps:

```bash
dotnet restore
dotnet build
dotnet run
```

Access Swagger UI:

```
https://localhost:5001/swagger
```

## Endpoint

```
GET /api/stories/best?n=10
```

Returns the top `n` best stories sorted by score.

## Example Response

```json
[
  {
    "title": "Example Story",
    "uri": "https://example.com",
    "postedBy": "user123",
    "time": "2026-03-26T12:00:00+00:00",
    "score": 150,
    "commentCount": 42
  }
]
```

## Technical Decisions

* Clean separation of concerns (Controller, Service, Client)
* Use of HttpClientFactory for external API calls
* DTO mapping to decouple external API contract
* Basic validation and error handling

## Performance Considerations

* Fetches only required number of items
* Parallel requests for improved performance
* Designed to support caching (can be extended)

## Assumptions

* Only valid "story" items are returned
* Missing values are handled with defaults
* Maximum allowed `n` is 200

## Future Improvements

* Add in-memory or distributed caching (Redis)
* Implement retry policies (Polly)
* Add rate limiting
* Improve logging and monitoring
* Add unit and integration tests
