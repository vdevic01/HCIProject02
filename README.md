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

## Uputstvo za Mape:

Potrebno je instalirati paket koji podrzava Bing mape:
U Solution Explorer-u, desnim klikom na projekat otvoriti meni konteksta i izabrati opciju "Manage NuGet Packages...",
zatim u Browse tabu pretraziti "Microsoft.Maps.MapControl.WPF" i instlirati taj paket.

Radi rukovanja sa privatnim kljucem za mape, potrebno je kreirati App.config fajl na sledeci nacin:
1.U Solution Explorer-u, desnik klikom na projekat otvoriti meni konteksta i izabrati opciju "Add" > "New item".<br/>
2.Izabrati opciju "Application Configuration File" ili "App.config" i kliknuti na dugme "Add".
3.Otvorice se novi fajl App.congif u kome treba napisati konfiguraciju:
	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
	<appSettings>
		<add key="MapKey" value="NAVESTI VREDNOST KLJUCA"/>
	</appSettings>
	</configuration>
pri cemu treba navesti vrednost kljuca.

Kada zelimo da mape koristimo u nasem xaml fajlu moramo:
1. Navesti u zaglavlju nase UserControl sledeci namespace:
	  xmlns:m="clr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF"  
          xmlns:System.Configuration="clr-namespace:System.Configuration;assembly=System.Configuration"
2. Mapa se u nasem GRID-u dodaje kao:
	   <m:Map CredentialsProvider="YOUR_MAP_KEY" x:Name="myMap"/>
   pri cemu NE TREBA umesto YOUR_MAP_KEY staviti vrednost kljuca, to ce se obaviti kroz logiku klase koja 
implementira XAML.
3. Za primer rukovanja sa mapom i kako se postavlja kljuc iz konfiguracionog fajla u varijablu, pogledati klasu NewHotelView.xaml.cs.
4. Za dobavljanje adrese na osnovu koordinata salje se HTTP zahtev preko Newtonsoft.Json.Linq, pa ce verovatno trebati da se uradi install tog paketa
