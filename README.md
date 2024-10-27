# Bankomat
Detta är ett skolprojekt där jag skulle lära mig .Net hur man manipulerar data. Uppdraget var att göra en konsol app där man kan logga in som användare och sedan kunna göra några saker som att kunna se konton och saldon, föra över mellan konton och ta ut pengar. Vi hade en begränsning i det här projektet. Begränsningen var att vi inte fick använda någon form av klasser och vissa metoder för att söka igenom en Array lite lättare.

## Hur man kör programmet
1.	Öppna projektet i en konsol
2.	Kör kommandot dotnet run
3.	bankomat är nu i gång
    1.	Användarnamn du kan välja mellan Musse, Kalle Anka, Långben, Pluto eller Scar. Se till att stava det på samma sätt.
    2.	Pinkoden är densamma för alla användare, "1234".
4.	Följ instruktionerna i konsolen

## Kodstruktur
Bankomaten består av fyra olika sektioner för att göra så att den fungerar. Den första sektioner är en sektion där jag sparar användare, användare konton och om man behöver trycka på enter till nästa gång man kommer i menyn. Den andra sektionen är menyn som kommer upp när man är inloggad. Näst sista sektionen är alla menyval skrivna i sin egen metod för att göra det enkelt att kalla på dem i menyn. Sista är hjälp metoder för att försöka hålla koden ”DRY code” (Don't repeat yourself code).

### Globala variabler 
Alla variabler som ska användas i resten av projektet ska ligga längst upp i Program klassen. Detta är för att man sen ska kunna använda variabler i alla metoder. Användare, användarekonton är sparade i en 2D array för att enkelt kunna plocka ut värden. Det ger också en känsla av att det är sparat i en NoSQL databas.

### Meny
Menyn startar med att man kommer till login. Där man ska mata in sina inloggningsuppgifter för att komma in i banken. Man har tre försök på sig att komma in annars så blir man utkastad från bankomaten med en ``return``. För att få tre nya försök att komma in behöver man starta bankomaten på nytt. Om man lyckas att komma in på banken är man presenterad av menyn. Menyn är byggd med en ``switch`` och ``Console.WriteLine`` för att vissa vilka altnativ som användaren kan välja på.

### Meny metoder
Varje menyval ska ha sin egen metode. Detta är för att det ska vara enkelt att kalla på i menyn. Det ska nästa bete sig som en sida på en hemsida. 

- **SignIn:** Tar in namn och en pin kod för att sedan kolla i användare 2D array. Användaren har tre försök på sig innan bankomaten stänger av. 
- **AccountsBalance:** Går igenom användare konton för en specifik användare för att sedan kunna skriva ut dom.
- **TransferBetweenAccounts:** Visar alla användare konton och sedan kan användaren välja två konton att överföra mellan. Sedan väljer man en summa att välja mellan användare kontona. Man kan inte överföra mellan samma konton.
- **WithdrawMoney:** Visar alla användare konton och sedan kan användaren välja ett konto och en summa att ta ut. Summan som användaren tar ut kan inte vara större än vad som finns på kontot. Det kan inte heller vara ett negativt tal.

### Hjälp metoder
Dessa metoder är för att hjälpa till att gör koden mer lätt läst och att hålla koden ”DRY code” (Don't repeat yourself code).

- **GetUserAccounts:** Tar ut alla användare konton för en specifik användare i en 2D array. Om den inte hittar något användare konto skriver den ut i konsolen "Account does not exist" och returnerar en tom 2D array. 
- **Ask:** metoden hjälper till med att kombinera både en ``Console.WriteLine`` och en ``Console.ReadLine``. Detta för att man vid många tillfällen vill man fråga en fråga till användaren och sen snabbt kunna spara svaret i en variabel.

## Reflektioner
Jag valde att dela upp min kod i fyra sektioner. Globala variabler, Meny, Meny metoder och Hjälp metoder. Detta var för att kunna hitta lättare vad jag är ute efter när man kodar. 

Till exempel på en sektion är Menyn som är så högt upp i koden för att den är det som ska kalla på de andra metoderna som är längre ner i koden. Varför jag inte valde att göra en metod av menyn är för att det inte skulle ge någon större fördel med hur lätt de är att läsa koden. Däremot gjorde jag metoder för ``Signin``, ``AccountBalance``, ``TranferBetweenAccount`` och ``WithdrawMoney`` eftersom det skulle vara mycket svårare att följa koden om allt var i switchen utan någon metod.

Varför jag valde att lägga hjälp metoderna längst ner var för att jag inte ville att de skulle ta extra mycket plats när man vill komma åt metoderna för menyn. Metoderna ligger längst ner för att jag inte vet i framtiden om det kommer komma mer hjälp metoder som kommer trycka ner den viktiga koden. 

### Globala variabler 
När jag skrev mina variabler så valde jag att göra en 2D array för att enkelt kunna lägga till flera och inte vara begränsad till att bara spara en viss mängd. Detta gjorde jag för att jag hade som mål att göra extrautmaningarna. En utmaning var att som användare skulle kunna öppna en ny användare konto.  

Något jag kunde göra annorlunda är att använda mig av flera variabler för att spara användare konton så att man inte behöver göra en ``double.Parse`` överallt och konvertera tillbaka till string. Detta skulle nog leda till att koden blir ännu lättare att hantera. 

### Meny
Jag valde att skapa menyn med att användaren får se fyra val och sen kan skriva in en siffra som motsvarar det altnativ. Sen för att välja det alternativ görs med att det går in i en ``switch`` där den kallar på rätt meny metod. 

Något jag kunde göra är att låta användaren välja meny val med hjälp av piltangenterna upp och ner för att välja rät meny val. Detta skulle öka upplevelsen för användaren när den använder denna bankomat. Sen skulle den inte tillåta att man skrev en siffra som inte var med av de altnativ. 

### Meny metoder
Som jag sa om menyn så har jag gjort en metod för varje meny val en användare kan gör. Detta var för att göra det lite lättare att läsa koden. 

Det finns saker som jag skulle vilja ändra på som är att städa upp konsolen mellan man går ut och in i olika meny val. Sen att när användaren har skrivit fel att konsolen städas upp där också. Detta leder till att konsolen inte känns så klottrig med historik som inte behövs längre. 

Det finns mycket av kod i de olika meny metoderna som går att göra till hjälp metoder. Det finns två saker som det skulle förbättra. Den första är att det ökar läsbarheten i koden och det andra gör att man bara behöver ändra på ett ställe för att det ska ändras på flera ställen samtidigt. 

När jag frågar användaren efter ett nummer så använder jag mig av ``int.TryParse`` i en ``while loop``. Detta för att den ska fortsätta att fråga efter en siffra tills man har skrivit en siffra och på vissa ställen uppfyller lite extra validering. Något man skulle kunna ändra på här är att göra några separata ``if`` satser i ``while loopen`` som ger användaren lite mer felmeddelanden. Så att användaren vet lite mer vad den har gjort fel. 

I metoderna TransferBetweenAccounts och WithdrawMoney finns det ställen där jag visar vilka konton som användaren har och att man ska kunna välja någon av de. Liksom i menyn för bankomaten så skulle det vara trevligare för användaren om man kan använda piltangenterna upp och ner för att välja rätt användare konto. 

### Hjälp metoder
Jag känner att jag inte kan ha gjort någon skillnad på de metoder som finns. De metoder som finns känns inte som att det är onödigt skapade för att jag har använt de mer en gång. Sen finns det några fler metoder som skulle behöva läggas till som jag har diskuterat innan som ska öka läsbarheten i koden.  
