# DECISIONS

Answer **yourself**, in your own words, in 3–5 sentences per question.
Refer to concrete files and classes from your own repo (for example `Features/Bookings/CreateBooking/...`).
An unanswered question gives 0 points.

## 1. A rule you moved
Pick one business rule (R1–R8). In which file did it live in the starter and in which file does it live now? Why there?

Aleg R7. Era in Services/BookingService.cs, metoda Create linia 28, bucla foreach cu <=. Acum e in Domain/Bookings/Booking.cs, metoda Create.
Am pus- o acolo pentru ca regula tine de rezervare deci sta in entitatea ei. Asa e intr-un singur loc si nu poate fi ocolita (constructorul e privat, deci orice Booking trece prin Create).
Si acum endpoint-ul si handlerul nu mai contin reguli.

## 2. Result instead of an exception
Why is `Result` a better choice than an exception for "the room is occupied"? What happens in your code when an `Error` is returned?

E mai bun pentru ca “sala e ocupata” e o situatie asteptata, nu o eroare neasteptata. Semnatura Result<Booking> arata ca poate esua
Booking.Create intoarce un Error (Conflict), handlerul il da mai departe, endpointul apeleaza ToProblem() si raspunde 409 ProblemDetails

## 3. Who knows about EF Core?
Which classes in your project know about EF Core and which do not? What do you gain and what do you pay with Repository + Unit of Work?

Doar Infrastructure/Persistence: RoomHubDbContext, configurarile, RoomRepository, BookingRepository.
Domain si Features nu stiu de ef core, ele folosesc doar IRoomRepository, IBookingRepository, IUnitOfWork.
Pai castig ca handlerele nu depind de EF, se pot testa cu fake uri si salvarea e intr-un singur loc.
Costul ar fi ca am mai multe fisiere pentru o aplicatie mica

## 4. The flow of a request
Draw in text (ASCII arrows) the path of a `POST /api/rooms/{roomId}/bookings` request through your project, with the **real** names of files and classes, from the endpoint to the database.

POST /api/rooms/{roomId}/bookings
-> Features/Bookings/CreateBooking/CreateBookingEndpoint.cs (MapCreateBooking)
face CreateBookingCommand din request
-> ICommandHandler<CreateBookingCommand, CreateBookingResponse> = CreateBookingHandler
-> IRoomRepository.GetAsync (RoomRepository) -> daca lipseste: RoomErrors.RoomNotFound -> 404
-> TimeProvider.GetUtcNow() -> nowUtc
-> IBookingRepository.ListForRoomAsync 
-> IBookingRepository.ListActiveByBookedByOnDateAsync 
-> Domain/Bookings/Booking.Create(...) 
eroare -> Error -> ToProblem() -> 400 / 409 ProblemDetails
-> IBookingRepository.Add (BookingRepository, doar marcheaza)
-> IUnitOfWork.SaveChangesAsync = RoomHubDbContext -> roomhub.db
-> TypedResults.Created("/api/bookings/{id}") -> 201

## 5. What you rejected from what the AI gave you
Give a concrete example of code or an idea proposed by the AI that you corrected or rejected, and say why.

A folosit DateOnly.FromDateTime in query ul R9; EF nu-l poate traduce in SQL asa ca i-am cerut sa filtreze pe interval (StartUtc >= ziua si < ziua+1)

## 6. If you had 30 more minutes
What would you improve and what risk did you leave in the project?

Probabil teste automate (unit pe Booking, integration pe endpoint-uri) si migratii in loc de EnsureCreated.
Un risc lasat e ca doua request-uri simultane pot trece amandoua de verificarea de suprapunere sau de R9 si pot salva rezervari suprapuse, insa chiar nu mai am timp sa l repar din pacate
