# Bankomaten
## 🔒 Start av programmet och inloggning
- [x] När programmet startar ska användaren välkomnas till banken.
- [x] Användaren ska mata in sitt användarnummer/användarnamn (valfritt hur detta ser ut) och en pin-kod som ska avgöra vilken användare det är som vill använda bankomaten.
- [x] Bankomaten ska ha 5 olika användare som ska kunna använda den. Det behöver inte gå att lägga till fler användare.
- [x] Om användaren skriver in fel pinkod tre gånger ska det inte gå att försöka logga in igen utan att starta om programmet.

## 🧭 Navigera som användare

- [x]  När användaren lyckats logga in ska bankomaten fråga vad användaren vill göra. Det ska finnas fyra val:
    
```
    1. Se dina konton och saldo
    2. Överföring mellan konton
    3. Ta ut pengar
    4. Logga ut
```
    
- [x]  Användaren ska kunna välja en av funktionerna ovan genom att skriva in en siffra.
- [x]  När en funktion har kört klart ska användaren få upp texten "Klicka enter för att komma till huvudmenyn". När användaren klickat enter kommer menyn upp igen.
- [x]  Om användaren väljer "Logga ut" ska programmet inte stängas av. Användaren ska komma tillbaka till inloggningen igen.
- [x]  Om användaren skriver ett nummer som inte finns i menyn, eller något annat än ett nummer, ska systemet meddela att det är ett "ogiltigt val".

## 🔢 Se konton och saldo

- [x]  Denna funktion ska köras när användaren navigerat in till alternativet "Se dina konton och saldo".
- [x]  Användaren ska få en utskrift av de olika konton som användaren har och hur mycket pengar det finns på dessa.
- [x]  Konton ska kunna ha både kronor och ören.
- [x]  Alla användare ska ha olika antal konton och alla ska ha minst ett konto.
- [x]  Varje konto ska ha ett namn, ex. "lönekonto" eller "sparkonto".
- [x]  Saldon för alla konton sätts vid starten av programmet (du ställer in en en summa som finns på varje konto i koden) så om programmet startas om återställs alla saldon.

## 🔁 Överföring mellan konton

- [x]  Denna funktion ska köras när användaren navigerat in till alternativet "Överföring mellan konton".
- [x]  Användaren ska kunna välja ett konto att ta pengar från, ett konto att flytta pengarna till och sen en summa som ska flyttas mellan dessa.
- [x]  Denna summa ska sedan flyttas mellan dessa konton och efteråt ska användaren få se vilken summa som finns på de två konton som påverkades.

## ⏏️ Ta ut pengar
- [x]  Denna funktion ska köras när användaren navigerat in till alternativet "Ta ut pengar".
- [x]  Användaren ska kunna välja ett av sina konton samt en summa att ta ut.
- [x]  Efter detta måste användaren skriva in sin pinkod för att bekräfta att de vill ta ut pengar.
- [x]  Lägg till ett felmeddelande om användaren försöker ta ut mer pengar än vad som finns på kontot.
- [ ]  Pengarna ska sedan tas bort från det konto som valdes.
- [ ]  Sist av allt ska systemet skriva ut det nya saldot på det kontot.


## 💡 Extrautmaningar

Om du känner att du hinner och vill göra mer kommer här förslag på ytterligare funktionalitet du kan bygga in i systemet. Dessa utmaningar är helt frivilliga och inget krav!

- [ ]  Lägg till funktionalitet så att användaren kan öppna nya konton.
- [ ]  Lägg till så att användaren kan sätta in pengar.
- [ ]  Gör så att olika konton har olika valuta, inklusive att valuta omvandlas när pengar flyttas mellan dem.
- [ ]  Lägg till så att användaren kan göra överföringar till andra användare.
- [ ]  Lägg till så att om användaren skriver fel pinkod tre gånger stängs inloggning för den användaren av i tre minuter istället för att programmet måste startas om.
- [ ]  Lägg till så att saldon för alla konton för alla användare sparas mellan körningarna av programmet så att saldon inte återställs.

## Extra exta utmaningar

- [ ] Skapa en tabell som går att visa alla konton och saldon för specifik användare.
- [ ] Skriv om menyn så den ser snyggare ut. [Youtube vidio](https://www.youtube.com/watch?v=YyD1MRJY0qI) 
- [ ] Refactorera login coden.
  ```C#
    private static string[]? SignIn(string[][] users)
        {
            Console.Clear();
            string[]? user = null;

            /* If the user is not found givs tow more trays */
            for (int i = 0; i < 3; i++)
            {
                string name = Ask("What is your name?");
                int pin;
                while (!int.TryParse(Ask("What is your pin?"), out pin))
                {
                    Console.WriteLine("Invalid pin, try again");
                }

                /* Finds the user */
                for (int k = 0; k < users.GetLength(0); k++)
                {
                    if (users[k][1] == name && int.Parse(users[k][2]) == pin)
                    {
                        // user = users[k];
                        return users[k];
                    }
                }

                // if (user != null)
                // {
                //     return user;
                // }

                Console.WriteLine("Wrong name or pin, try again");
            }

            return user;
        }
  ```
- [ ] kompletera med flera kommentarer.