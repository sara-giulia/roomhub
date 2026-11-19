# AI-LOG

Write down, in order, what you asked the AI. It is not a punishment: a missing or false log lowers the level D grade.

| Time (approx.) | What you asked (briefly) | What you kept | What you changed or rejected |
|---|---|---|---|
| aproape 3 min | prompt 1: mutat endpoint-urile in Features/ (5 slice-uri) | structura cu cele 5 foldere in Features/ (CreateRoom, GetRoom, CreateBooking, GetBooking, CancelBooking); endpointurile mutate fiecare in folderul lui, fiecare cu Request si Response proprii; stergerea folderului Endpoints/ | nu l-am lasat sa faca commit singur, in rest totul bine |
| 4 min | prompt 2: domeniu bogat, Result, handlere, regulile R1–R8 in domeniu | Room si Booking cu setteri privayi si Create; Booking.Cancel(); regulile R5–R8; handlerele; ToProblem(); stergerea Services/ si a middleware-ulu | am verificat cu roomhub.http si am cautat DateTime.UtcNow si nu apare|
| 2 min | R9, max 2 rezervari active pe zi pentru acelasi bookedBy, regula in domeniu | eroarea DailyLimitExceeded (409); metoda noua din repository ListActiveByBookedByOnDate; regula in Booking.Create, dupa R7; handlerul incarca rezervarile zilei| am adaugat R9 si in ASSIGNMENT.md DateOnly.FromDateTime nu merge cu EF, l-am schimbat la promptul 3 | 
| 4 min | R10, R11 si EF Core cu SQLite, repository si Unit of Work | R10: max 2 ore pentru sali cu capacitate peste 10, eroare 400; R11: anularea doar cu minim 60 de minute inainte, cu TimeProvider, eroare 409; interfetele in Domain: IRoomRepository, IBookingRepository, IUnitOfWork; RoomHubDbContext, configurarile, UtcDateTimeConverter; EnsureCreated, roomhub.db; stergerea repository-urilor vechi in memorie | i-am cerut sa nu foloseasca DateOnly.FromDateTime in query-ul R9, ca EF nu-l traduce; am verificat ca datele raman dupa restart; nu l-am lasat sa dea commit |
