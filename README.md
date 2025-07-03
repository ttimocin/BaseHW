# BaseHW - Model Car Website

Bu proje Hot Wheels ve Matchbox model arabaları ile ilgili bir web sitesidir.

## 🚀 Railway Deployment

Bu proje Railway platformunda deploy edilmek üzere hazırlanmıştır.

### Gereksinimler

- .NET 6.0
- PostgreSQL (Railway'de otomatik sağlanır)

### Railway'de Deploy Etme

1. [Railway.app](https://railway.app) adresine gidin
2. GitHub hesabınızla giriş yapın
3. "New Project" > "Deploy from GitHub repo" seçin
4. Bu repository'yi seçin
5. Railway otomatik olarak PostgreSQL veritabanı oluşturacak
6. Environment variables otomatik olarak ayarlanacak

### Environment Variables

Railway otomatik olarak şu environment variable'ları ayarlar:
- `DATABASE_URL`: PostgreSQL connection string
- `ASPNETCORE_ENVIRONMENT`: Production

### Local Development

```bash
# Projeyi klonlayın
git clone https://github.com/ttimocin/BaseHW.git

# BaseHW klasörüne gidin
cd BaseHW

# Bağımlılıkları yükleyin
dotnet restore

# Veritabanını oluşturun
dotnet ef database update

# Projeyi çalıştırın
dotnet run
```

## 📁 Proje Yapısı

- `Controllers/`: MVC Controller'ları
- `Models/`: Entity Framework modelleri
- `Views/`: Razor view'ları
- `wwwroot/`: Statik dosyalar (CSS, JS, resimler)
- `Areas/Admin/`: Admin paneli

## 🛠️ Teknolojiler

- ASP.NET Core 6.0
- Entity Framework Core
- Identity Framework
- Bootstrap
- jQuery
- PostgreSQL (Railway)

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. 