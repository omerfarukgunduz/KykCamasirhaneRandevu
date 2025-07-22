
# KYK Çamaşırhane Randevu Sistemi

Bu proje, KYK yurtlarındaki çamaşırhane kullanımını dijitalleştirmek amacıyla geliştirilmiş bir web tabanlı randevu sistemidir. Öğrencilerin belirlenen saat aralıklarında adil şekilde çamaşırhane randevusu almasını ve bu süreci yönetimini amaçlamaktadır.

## 🚀 Tanıtım Videosu https://drive.google.com/file/d/1sGt82QafbLyp7fGc4ft3BT2VWnKmfz5o/view?usp=sharing 

## 🚀 Özellikler

- Öğrenci kayıt ve giriş sistemi
- Randevu alma ve iptal etme
- Randevularım ekranı
- Ceza durumu takip sistemi
- Yönetici paneli ile çamaşırhane takibi
- .NET 6 ile geliştirilmiş backend altyapısı
- Entity Framework Core ile veritabanı işlemleri
- JSON tabanlı konfigürasyon

## 🛠️ Kullanılan Teknolojiler

- ASP.NET Core MVC
- .NET 6
- Entity Framework Core
- SQL Server
- Razor Pages / HTML / CSS / JS
- Visual Studio 2022

## ⚙️ Kurulum

1. Bu repoyu klonlayın veya ZIP olarak indirin.
2. Visual Studio 2022 ile `KYKCamasirhaneUygulamasi.sln` dosyasını açın.
3. Gerekirse `appsettings.json` içindeki veritabanı bağlantı cümlesini güncelleyin.
4. NuGet paketlerini geri yükleyin (`Tools > NuGet Package Manager > Restore Packages`)
5. Veritabanını oluşturmak için terminalde aşağıdaki komutu çalıştırın:

   ```bash
   dotnet ef database update
   ```

6. Uygulamayı çalıştırmak için `F5` veya `Ctrl + F5` tuşlarına basın.

## 📁 Proje Yapısı

```
KYKCamasirhaneUygulamasi/
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
├── appsettings.json
├── Startup.cs
└── Program.cs
```

## 👨‍💻 Geliştiriciler

- Ömer Faruk Gündüz (Yazılım Geliştirici)


## 📜 Lisans

Bu proje açık kaynak değildir ve yalnızca akademik / eğitimsel amaçlarla kullanılabilir.
