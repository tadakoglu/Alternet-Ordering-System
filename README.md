# AlternetOrderingSystem
Includes the Alternet Online Medicine Ordering Software developed by ASP.NET Core MVC 2
**ENGLISH**
**TECH STACK**
ASP.NET Core MVC 2, Entity Framework Core (Code First), C#, HTML5, CSS3, Bootstrap, 
JQuery UI, JQuery AJAX, XUnit Framework, Moq, Dependency Injection, SQL Server Express, MVVM software architecture.

**JUST CHANGE THAT TO SET UP APP IN YOUR COMPUTER**
#1 SET THE SERVER VARIABLE TO YOUR SQL SERVER INSTANCE IN ConnectionString IN THE APPSETTINGS.JSON FILE AS FOLLOWING:
"Server=YOUR_SERVER_ISTANCE_NAME_HERE;Database=AlternetSiparisVT;Trusted_Connection=True;MultipleActiveResultSets=true"

**TURKISH**
Uygulama ASP.NET Core MVC 2, Entity Framework Core (Code First Teknikleri ile), C#, HTML5, CSS3, Bootstrap, 
JQuery UI, JQuery AJAX, XUnit,Moq, Dependency Injection ve SQL Server Express teknolojileri kullanarak geliþtirilmiþtir. 
MVVM kullanıldı.

Uygulama SQL Server Ekspress ve SQL Server Local Db üzerinde düzgün çalıştığı test edilmiþtir. SQL Server diğer sürümleri üzerinde de çalışıyor.
Uygulama "Connection String" değeri appsettings.json dosyası içinde belirtilmiştir. Bir adet SQL Server Express bir adet de 
Local Db için string tanımladım. Varsayilan ayarlar "SQL Server" üzerinde çalışmasıdır. 

***Ancak "SADECE" SQL Server'in üzerinde bulunduğu sunucu adını, connection string'deki server değişkenine
aşağıdaki gibi(Server=TADAKOGLU\\SQLEXPRESS olarak değil de Server=SunucuAdiniz) atamanız gerekmektedir.
"Server=TADAKOGLU\\SQLEXPRESS;Database=AlternetSiparisVT;Trusted_Connection=True;MultipleActiveResultSets=true" ***
Uygulama hatasiz çalışmaktadır. Tüm testleri geçmiþtir. İlginiz için teşekkür ederim.
