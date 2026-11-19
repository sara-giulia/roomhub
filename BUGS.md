# BUGS

The starter contains behaviors that contradict `ASSIGNMENT.md`. Write here every discrepancy you found.
Fill it in yourself, in your own words. There is no required number: what counts is that they are real.

| Which rule from ASSIGNMENT.md is violated (R1–R8) | Where the problem was in the starter (file, approximate line) | How you discovered it (test, reading, AI) | What you changed |
|---|---|---|---|
| R7 | Services/BookingService.cs, linia 28: <= | citind codul am vazut de repede greseala pentru ca semana cu ce facusem in curs | < strict |
  | R7/R8 | Services/BookingService.cs, linia 26: ia si rezervarile anulate | tot citind codul | sa ia doar rezervarile Active |
| R8 |  Services/BookingService.cs, liniile 56-60 : anulare dubla si da 209 | citind codul | returneaza 409 |

