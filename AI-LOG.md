# AI-LOG

Write down, in order, what you asked the AI. It is not a punishment: a missing or false log lowers the level D grade.

| Time (approx.) | What you asked (briefly) | What you kept | What you changed or rejected |
|---|---|---|---|
| aproape 3 min | prompt 1: mutat endpoint-urile in Features/ (5 slice-uri) | structura cu cele 5 foldere in Features/ (CreateRoom, GetRoom, CreateBooking, GetBooking, CancelBooking); endpointurile mutate fiecare in folderul lui, fiecare cu Request si Response proprii; stergerea folderului Endpoints/ | nu l-am lasat sa faca commit singur, in rest totul bine |
| 4 min | prompt 2: domeniu bogat, Result, handlere, regulile R1–R8 in domeniu | Room si Booking cu setteri privayi si Create; Booking.Cancel(); regulile R5–R8; handlerele; ToProblem(); stergerea Services/ si a middleware-ulu | am verificat cu roomhub.http si am cautat DateTime.UtcNow si nu apare|
 | 2 min | R9, max 2 rezervari active pe zi pentru acelasi bookedBy, regula in domeniu | eroarea DailyLimitExceeded (409); metoda noua din repository ListActiveByBookedByOnDate; regula in Booking.Create, dupa R7; handlerul incarca rezervarile zilei| am adaugat R9 si in ASSIGNMENT.md DateOnly.FromDateTime nu merge cu EF, l-am schimbat la promptul 3 | 
