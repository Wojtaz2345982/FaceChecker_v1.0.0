# FACECHECKER - Automatyzacja sprawdzania obecności
# https://facechecker.duckdns.org

# Filmik przedstawiający działanie aplikacji:
# https://youtu.be/Dz7-Vh192ek
## 🎯 Cel

Celem aplikacji FACECHECKER jest usprawnienie procesu rejestrowania obecności uczniów oraz nauczycieli, zwiększenie efektywności wykorzystania czasu lekcyjnego oraz eliminacja potencjalnych oszustw związanych z obecnością na zajęciach. Aplikacja rozpoznaję twarzę uczniów wchodzących do klasy i wykonuję odpowiednie działania w zależności od rozpoznanej twarzy.

## 🔨 Technologie i pakiety

Aplikacja została zbudowana z wykorzystaniem ASP.NET Core MVC w **.NET 7** i implementuje architekturę CQRS (Command Query Responsibility Segregation). Niezbędne API znajduje się w dołączonym do repozytorium folderze `API`.
  
## Aby rozpocząć działanie z naszą aplikacją niezbędny jest adres API który w tym repozytorium jest ukryty ze względów bezpieczeństwa.
## Jeżeli chciałbyś przetestować aplikację napisz do nas na email: faceckecker1@gmail.com, udostępnimy dostęp do API

## 📋 Wymagania systemowe

- **System operacyjny:** Windows 10+, macOS Mojave (10.14)+, lub kompatybilna dystrybucja Linux jak Ubuntu 18.04+.
- **Zależności systemowe:**
  - Kamera internetowa: Wymagana dla rozpoznawania twarzy.
  - Internet: Dla komunikacji z API.
  - SQL Server: Dla przechowywania danych.

## 💿 Instalacja


### Wymagane oprogramowanie:

- .NET 7 SDK
- ASP.NET Core Runtime 7.0
- Visual Studio 2022+ / Visual Studio Code z C# extension
- Entity Framework Core Tools

### Krok po kroku:

1. Zainstaluj .NET 7 SDK ze strony Microsoft.
2. Zaktualizuj środowisko deweloperskie do najnowszej wersji.
3. Zainstaluj Entity Framework Core Tools:
   ```bash
   dotnet tool install --global dotnet-ef
Skonfiguruj połączenie z SQL Server (Po zainstalowaniu odpowiednich paczek NuGet): 
W pliku appsettings.json w sekcji connection strings zamiast "Connection string do bazy danych" umieść connection string do swojej bazy danych.
Następnie w konsoli pakietów NuGet wykonaj: 
  ```bash
  Update-Database
  ```
📦 Pakiety NuGet:
  Instalacja w konsoli pakietów nuGet:
   ```bash
  Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
  Install-Package FluentValidation.AspNetCore
  Install-Package Flurl.Http
  Install-Package MediatR
  Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
  Install-Package Microsoft.AspNetCore.Identity.UI
  Install-Package Microsoft.EntityFrameworkCore.Design
  Install-Package Microsoft.EntityFrameworkCore.SqlServer
  Install-Package Microsoft.EntityFrameworkCore.Tools
  Install-Package Microsoft.Extensions.Identity.Stores
  Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design
  Install-Package Wangkanai.Detection
 ```
  Instalacja w wierszu poleceń .Net core CLI:
   ```bash
  dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
  dotnet add package FluentValidation.AspNetCore
  dotnet add package Flurl.Http
  dotnet add package MediatR
  dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
  dotnet add package Microsoft.AspNetCore.Identity.UI
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  dotnet add package Microsoft.Extensions.Identity.Stores
  dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
  dotnet add package Wangkanai.Detection
 ```

AutoMapper.Extensions.Microsoft.DependencyInjection
Pozwala na łatwą integrację AutoMapper z kontenerem zależności ASP.NET Core.

FluentValidation.AspNetCore
Zapewnia integrację biblioteki FluentValidation z ASP.NET Core, co pozwala na wydajną walidację modeli przychodzących z żądań.

Flurl.Http
Jest to wygodna biblioteka do wykonywania zapytań HTTP, która wspiera testowalność i elastyczność w dostępie do zewnętrznych usług webowych.

MediatR
Jest to implementacja wzorca Mediator, która ułatwia zastosowanie CQRS w aplikacji, umożliwiając prostą obsługę komend i zapytań.

Microsoft.AspNetCore.Identity.EntityFrameworkCore
Zapewnia integrację ASP.NET Core Identity (system zarządzania tożsamościami) z Entity Framework Core, co pozwala na łatwą obsługę uwierzytelniania i autoryzacji.

Microsoft.AspNetCore.Identity.UI
Zawiera domyślne interfejsy użytkownika dla ASP.NET Core Identity, które można łatwo włączyć do projektu.

Microsoft.EntityFrameworkCore.Design
Dostarcza narzędzia konieczne do projektowania podczas pracy z Entity Framework Core, w tym migracje i generowanie schematu bazy danych.

Microsoft.EntityFrameworkCore.SqlServer
Jest to dostawca bazy danych SQL Server dla Entity Framework Core, umożliwiający korzystanie z SQL Server jako bazy danych.

Microsoft.EntityFrameworkCore.Tools
Oferuje zestaw narzędzi dla konsoli NuGet Package Manager, które wspomagają rozwój przy użyciu Entity Framework Core.

Microsoft.Extensions.Identity.Stores
Zapewnia implementację przechowywania informacji o tożsamości, które można dostosować do potrzeb aplikacji.

Microsoft.VisualStudio.Web.CodeGeneration.Design
Narzędzie generowania kodu dla ASP.NET Core, które pomaga w szybkim tworzeniu kontrolerów i widoków.

Wangkanai.Detection
Biblioteka do wykrywania informacji o urządzeniu klienta, takich jak rodzaj urządzenia, przeglądarka czy system operacyjny.

🚀 Jak używać
Aby wykorzystać pakiet w projekcie, można dodać konfigurację w Program.cs jak poniżej:

    ```bash

    // Konfiguracja AutoMapper
    builder.Services.AddAutoMapper(typeof(Startup));
    
    // Konfiguracja FluentValidation
    builder.Services.AddControllers().AddFluentValidation(fv => 
        fv.RegisterValidatorsFromAssemblyContaining<Program>());
    
    // Konfiguracja MediatR
    builder.Services.AddMediatR(typeof(Program));
    
    // Konfiguracja Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();
    
    // Konfiguracja Entity Framework Core
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

## Biblioteki Javascript użyte w naszej aplikacji:
- toastr.js - komunikaty w aplikacji,
- Webcam.js - obsługa kamery internetowej na stronie

-JQuery


## ✨ Funkcjonalności
- Utworzenie i zarządzanie kontami przez nauczycieli.
- Tworzenie klas i zarządzanie nimi.
- Dodawanie ucziów do klasy i zarządzanie nimi.
- Dodawanie zdjęć uczniów do bazy danych dla ich rozpoznawania twarzy.
- Planowanie i uruchamianie lekcji.
- Rozpoznawanie obecności uczniów.
- Identyfikowanie przez kamerę intruzów wchodzących do klasy .
- Apliakcja zarejestruje również osoby spóźnione
- Szczegółowe raporty obecności.

## 📖 Dokumentacja API
Zapoznaj się z folderem API dla szczegółowych informacji o konfiguracji i użytkowaniu API rozpoznającego twarze.

## 🙌 Współpraca
Zachęcamy do zgłaszania Issue oraz Pull Requestów w celu wspólnego rozwoju projektu.

## 👤 Autorzy
Zespół FACECHECKER – Tworzymy z pasją, aby ułatwić życie szkolne.

## Kontakt: 
email - facechecker1@gmail.com

## 📄 Licencja
Projekt jest udostępniany na licencji MIT.
Plik z licencją jest dostępny LICENSE

