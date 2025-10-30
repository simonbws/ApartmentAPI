# Apartment Management API & Web Application

## Opis projektu
Projekt składa się z **API** oraz **Web Application** do zarządzania apartamentami i użytkownikami.  
API wykorzystuje **ASP.NET Core**, a autoryzacja i uwierzytelnianie są realizowane za pomocą wbudowanej biblioteki **Microsoft Identity** oraz **JWT** (JSON Web Tokens).  

Celem projektu jest pokazanie pełnego procesu CRUD dla apartamentów oraz  rejestracji i logowania użytkowników z rolami (`Admin` i `Customer`).  

---

## Funkcjonalności

### API
- CRUD dla apartamentów i numerów apartamentów
- Rejestracja użytkowników z rolami 
- Logowanie użytkowników i generowanie JWT tokenów
- Zarządzanie rolami przy użyciu `RoleManager`
- Obsługa autoryzacji z użyciem JWT i ról
-Paginacja
-Wersjonowanie
### Web Application
- Wyświetlanie listy apartamentów i numerów apartamentów
- Formularze tworzenia, edycji i usuwania apartamentów
- Rejestracja i logowanie użytkowników
- Autoryzacja widoków według ról

### Utility
- Static Details file

---

## Technologie
- **ASP.NET Core**
- **Entity Framework Core**
- **Microsoft Identity** (wbudowana biblioteka do zarządzania użytkownikami, rolami i hasłami)
- **JWT (JSON Web Token)** do autoryzacji
- **AutoMapper** do mapowania DTO i modeli
- **Newtonsoft.Json** do serializacji
- **SQL Server** jako baza danych
- **Bootstrap 5** w Web Application

---
## Wzorce projektowe i dobre praktyki
- **Repository Pattern** – separacja logiki dostępu do danych od kontrolerów.  
- **DTO (Data Transfer Objects)** – przesyłanie danych między API a frontem w sposób bezpieczny i kontrolowany.  
- **Razor Pages / MVC** – prosty front-end z dynamicznymi widokami i walidacją danych.  
- **AutoMapper** – mapowanie między modelami a DTO w celu uproszczenia kodu i uniknięcia powtarzania mapowań ręcznie.

---
## Struktura projektu
- **Apartment_API/** – projekt API
  - Controllers/ – kontrolery
  - Models/ – modele i DTO
  - Data/ – DbContext
  - Repository/ – logika dostępu do danych
    
- **Apartment_Web/** – projekt WebApp
  - Controllers/
  - Data
  - Repository
  - Models/
  - Views/ – Razor Pages
---
# Jak uruchomić projekt

1. **Klonowanie repozytorium:** Sklonować repozytorium na lokalny komputer.  
2. **Konfiguracja bazy danych:** W pliku `appsettings.json` w folderze API ustawić połączenie do bazy danych w `ConnectionStrings:DefaultConnection`. Upewnić się, że serwer SQL działa i baza danych jest dostępna.  
3. **Wykonanie migracji:** W terminalu w katalogu API uruchomić `dotnet ef database update`, aby utworzyć wszystkie tabele, w tym Identity, role i użytkowników w bazie danych.  
4. **Uruchomienie:** Ustawić projekty startowe API oraz WebApp. Backend będzie gotowy do obsługi żądań z WebApp, Swaggera lub Postmana.  
5. **Rejestracja i logowanie:** Zarejestrować użytkownika z rolą `Admin` lub `Customer`. W przypadku korzystania z API przez Postmana, podczas logowania zostaje wygenerowany JWT token, który należy użyć w nagłówku `Authorization: Bearer <token>` przy dostępie do chronionych endpointów API.

## Inspiracja
Projekt powstał w oparciu o kurs online na Udemy, który pokazywał wykorzystanie ASP.NET Core, Microsoft Identity i JWT w praktycznych scenariuszach.  
