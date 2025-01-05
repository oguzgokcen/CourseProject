
# Kurs Satın Alma Sitesi

Bu projede bir kurs satın alma sitesi geliştirilmiştir. Websitesine kullanıclar kayıt olma / üye olma işlemleri gerçekleştirebilir, kursları listeleyebilir , kurs satın alabilir.
Projenin frontendi react ile backendi .net core ile yapılmıştır.

# CourseApi Projesi

## Genel Bakış

CourseApi, kurslar, kullanıcılar ve ödemeleri yönetmek için tasarlanmış bir backend hizmetidir. .NET 9.0 kullanılarak geliştirilmiş olup, Entity Framework Core, MassTransit ve RabbitMQ gibi çeşitli teknolojilerden yararlanmaktadır.

## Özellikler

- Kimlik doğrulama ve yetkilendirme ile kullanıcı yönetimi.
- Kursların oluşturulması, güncellenmesi ve silinmesi dahil kurs yönetimi.
- Ödeme işleme ve kaydı.
- Asenkron işlemler için RabbitMQ ile mesajlaşma.

## Gereksinimler

- .NET 9.0 SDK
- SQL Server
- RabbitMQ





## Yükleme 

benim-projem'i npm kullanarak yükleyin

```bash 
  git clone https://github.com/oguzgokcen/CourseProject.git
  cd CourseApi
```

### Veritabanı

Projede veritabanı olarak sql server kullanılmıştır. Sql serverı kullanabilmek için `appsettings.Development.json` dosyasındaki CourseDbConnection değerini güncelleyin.

### Migrasyonları Çalıştırın

Veritabanındaki tabloları kurmak için migrationları veritabanına uygulayın.

Package manager consolunda:
```bash 
  update-database
```
komutunu çalıştırın.

### Rabbit Mq 

Docker kullanarak rabbit mq'yu çalıştırmak için solutionun dosya konumunda

```bash 
  docker compose up -d
```

komutunu çalıştırın.

Not : Rabbit mq çalışmazsa uygulama işleyişinde bir sıkıntı oluşmaz. Sadece gerekli loglar databasede güncellenmeyebilir.

### Uygulamayı Çalıştırın

Uygulamayı aşağıdaki komutla çalıştırabilirsiniz:

CourseApi rootunda
```bash 
  dotnet run
```
## Uygulama Yapısı
Uygulamada n - katmanlı mimari kullanılmıştır.
Proje Yapısı:
- **CourseApi**: Ana API projesi.
- **DataLayer**: Veritabanı bağlamı, varlıklar ve migrasyonları içerir.
- **Services**: İş mantığı ve doğrulama.
- **CourseApi.Messaging**: RabbitMQ ile mesajlaşmayı yönetir.

## Identity Yapısı

Projede identity yapısı için bearer token kullanılmıştır. Kullanıcı başarılı bir şekilde login veya kayıt olduğunda kullanıcıya dönülen token ile authentication işlemi yapılabilmektedir. 
Identity yapısı için:

### Register (Kayıt Olma)

- **Amaç**: Yeni kullanıcıların sisteme kaydolmasını sağlar.
- **İşleyiş**:
  - Kullanıcıdan gerekli bilgileri (örneğin, tam ad, e-posta, şifre) alır.
  - Kullanıcı bilgilerini doğrulamak için bir validator kullanır.
  - Kullanıcıyı veritabanına kaydeder.
  - Yanlış bilgiler girildiğinde, uygun bir hata mesajı döndürülür.

### Login (Giriş Yapma)

- **Amaç**: Mevcut kullanıcıların sisteme giriş yapmasını sağlar.
- **İşleyiş**:
  - Kullanıcıdan e-posta ve şifre bilgilerini alır.
  - Veritabanında kullanıcıyı doğrular.
  - Kullanıcı bilgileri doğruysa, kullanıcıya bir erişim token'ı (JWT) oluşturur ve döndürür.
  - Yanlış bilgiler girildiğinde, uygun bir hata mesajı döndürülür.

### GetAccessToken (Erişim Token'ı Alma)

- **Amaç**: Kullanıcı eğer 401 hatası almışsa atılan refresh token ile yeni bir jwt token geri döndürülür
- **İşleyiş**:
  - Kullanıcı giriş yaptıktan sonra, sistem bir refresh token ve jwt token oluşturur.
  - Jwt Token, belirli bir süre için geçerlidir ve süresi dolduğunda yenilenmesi gerekir.
  - Bu endpoint sayesinde kullanıcı sürekli olarak login durumda kalabilir.

### Güvenlik

- **Şifreleme**: Kullanıcı şifreleri, veritabanında güvenli bir şekilde saklanır.
- **Token Doğrulama**: JWT'ler, sunucu tarafında doğrulanır ve yalnızca geçerli token'lar kabul edilir.
- **Rol Tabanlı Erişim**: Kullanıcılar, rollerine göre farklı kaynaklara erişim hakkına sahip olabilir.


## Course Controller

- **Amaç** : Kurs yönetimi ile ilgili işlemleri sağlar.
- Search : Kurs araması yapabilmeyi sağlar. Veritabanından verilen parametrelere uyan kursları döndürür. Ayrıca paginated şekilde arama yapabilmeyi sağlar.
- GetCourseDetail : Verilen id'ye ait kursun detaylarını veri tabanından döndürür.
- CheckIfBought : İstek atan kullanıcının bu kursa daha önce satın aldığı veya şuan sepetinde yer alıp almadığını kontrol eder. 
- GetCategoryCourses : Verilen search parametrelerine göre o kategoriye ait kursları döner.
- GetAllKeywords : Ana sayfada listelemek için tüm kayıtlı kategorileri kullanıcıya döner.

## Cart Controller
- **Amaç** : Sadece kayıtlı kullanıcıların istek atabileceği ve kullanıcının sepetini yönetmeyi sağlayan endpoint.
- Kullanıcının sepetine kurs ekleme , silme ve getirme işlemleri yapar.

## Payment Controller
- **Amaç** : Sadece kayıtlı kullanıcıların istek atabileceği ve kullanıcının sepetindeki kursları satın alabilmek için ödeme yapmayı sağlar. 
- Atılan isteği kontrol eder ve ödeme yapılan kartın bilgilerini doğrular. Ayrıca yapılacak ödemenin fiyatı ile UI'daki fiyatın uyuşup uyuşmadığını da kontrol eder. Validasyonlar başarılıysa kullanıcının kurslarına satın alınan kursları ekler ve kullanıcının sepetini sıfırlar. Ayrıca rabbitmq'ya gerekli işlemleri yapabilmesi için paymentlogdto nesnesini gönderir.

## User Controller
- **Amaç** : Kullanıcının profil bilgilerini getirmeyi ve  güncellemesini sağlar. Kullanıcı profil bilgilerini güncellerken gerekli validasyonlar yapılır. 
- Kullanıcının sahip olduğu kursları da dönen endpointe sahiptir.

# Business Katmanı

Bu katman controllerlar ve veri katmanı arasında bir ana katman olarak görev yapar. Burada servisler kendi contextlerine göre ayrılmıştır. Bu katmanda yer alan servisler gelen veriyi doğrular veriyi data katmanına iletir ve data katmanından uygun verileri çekecek metodları çağırır. Ayrıca bu süreçte herhangi bir hata karşılaşırsa uygun hata mesajı da döner.
Ayıca projemizin kullanacağı servislerin yaşam döngüsü de bu katmanda tanımlanmıştır.
# Data katmanı 

- Bu katmanda sistemimizde kullanılan modeller tanımlanmıştır. Bu modeller veri tabanında tutulan modeller ve request-response sürecinde UI ile iletişim için kullanılan DTO modelleridir. Ayrıca veri tabanından çekilen nesnelerin DTO'lara maplenme configurasyonu ve UOW de bu katmanda tanımlamıştır.
- Api projemiz ilk çalıştığında eğer veri tabanında veri yoksa dummy veriler yükleyen dbinitializer sınıfı da bu katmanda tanımlanmıştır.

## Course Db Context

`CourseDbContext`, projenin veritabanı bağlamını temsil eder ve Entity Framework Core kullanılarak veritabanı işlemlerini yönetir. Bu bağlam, uygulamanın veri modellerini ve ilişkilerini tanımlar.

Ayrıca masstransit'in outbox için kullanacağı tablolar da burada tanımlanmıştır.

## Entities
**AppUser , AppRole** : Kullanıcı bilgilerini ve rollerini tutan tablo. 
**Cart Item ** : Kullanıcıların sepetini temsil eden entity.
** Category Keyword** : Kursların kategorilerini temsil eden entity.
**Course** : Veri tabanında tutulan kursları temsil eden tablo.
** RefreshToken ** : Kullanıcıların refresh tokenlerini tutan tablo
**Payment Log** : Ödeme bilgilerini tutan tablo.
**Bought Course** : Satın alınan kurslar ve satın alan kullanıcı arasındaki ilişkiyi tutan tablo. Bu tablo ayrıca kursların satın alma tarihini ve hangi paymenta ait olduklarını da tutar.

# Messaging Katmanı

RabbitMQ'dan gelen mesajları işleyen katman. Bu katmanda yer alan PaymentCreatedConsumer ödeme sonrası yapılan ödeme bilgilerini kaydeder.