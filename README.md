# HCIProjekat02


## Uputstvo
1. VisualStudio
2. Obrisi sve iz Migrations direktorijuma (sem .gitkeep)
3. Core -> Persistence -> Obrisi sve fajlove koji imaju .db u imenu
4. Tools -> NuGet Package Manager -> Package Manager Console
5. Kucas sledecu komandu: Add-Migration [NazivMigracije]
6. Kucas sledecu komandu: Update-Database
7. Otvori fajl App.xaml.cs
8. Odkomentarisi sledecu liniju: DatabaseContextSeed.Seed(db);
9. Pokreni aplikaciju
10. Zakomentarisi liniju od malo pre

Ovo ce generisati bazu podataka i napuniti je podacima iz DatabaseContextSeed.cs fajla.
Podaci se ne brisu prilikom gasenja aplikacije.
Podaci se ne insertuju ponovo prilikom pokretanja aplikacije. (sem ako ne pratis prethodno definisane korake)
