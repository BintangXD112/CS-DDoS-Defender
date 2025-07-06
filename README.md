# CS-DDoS-Defender
Simple storng AntiDDoS using csharp

---

## Instalasi

1. **Clone repository:**
   ```bash
   git clone https://github.com/username/CSharpDefender.git
   cd CSharpDefender
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build aplikasi:**
   ```bash
   dotnet build
   ```

4. **Jalankan aplikasi:**
   ```bash
   dotnet run
   ```

---

## Konfigurasi

Edit file `appsettings.json` atau `appsettings.Development.json` untuk mengatur:

- Daftar blocklist/whitelist IP
- Pengaturan rate limiter
- Negara yang diblokir (GeoIP)
- Mode pemeliharaan
- Webhook URL, dsb.

**Contoh konfigurasi:**
```json
{
  "RateLimiter": {
    "RequestsPerMinute": 100
  },
  "GeoIp": {
    "BlockedCountries": [ "CN", "RU" ]
  },
  "Maintenance": {
    "Enabled": false,
    "Message": "Situs sedang dalam pemeliharaan. Silakan kembali nanti."
  },
  "Webhook": {
    "Url": "https://your-webhook-url"
  }
}
```

---

## Penggunaan

- Middleware akan otomatis aktif sesuai konfigurasi.
- Endpoint API tersedia di `/api/` untuk monitoring dan pengaturan dinamis (lihat folder `Controllers/`).
- Integrasikan ke aplikasi ASP.NET Core Anda dengan menambahkan middleware di `Program.cs`.

---

## API Endpoint

Beberapa endpoint API yang tersedia (cek folder `Controllers/` untuk detail):

- `POST /api/blocklist` — Tambah IP ke blocklist
- `DELETE /api/blocklist/{ip}` — Hapus IP dari blocklist
- `GET /api/blocklist` — Lihat daftar blocklist
- `POST /api/whitelist` — Tambah IP ke whitelist
- `GET /api/stats` — Statistik serangan & request
- `POST /api/maintenance` — Aktifkan/nonaktifkan mode pemeliharaan
- `POST /api/captcha/verify` — Verifikasi captcha

**Contoh request menambah IP ke blocklist:**
```http
POST /api/blocklist
Content-Type: application/json

{
  "ip": "1.2.3.4"
}
```

---

## Contoh Integrasi

Tambahkan middleware di `Program.cs`:
```csharp
app.UseMiddleware<LoggerMiddleware>();
app.UseMiddleware<RateLimiterMiddleware>();
app.UseMiddleware<BlocklistMiddleware>();
app.UseMiddleware<WhitelistMiddleware>();
app.UseMiddleware<GeoIpBlockMiddleware>();
app.UseMiddleware<CaptchaMiddleware>();
app.UseMiddleware<JsChallengeMiddleware>();
app.UseMiddleware<ConnectionLimitMiddleware>();
app.UseMiddleware<MaintenanceMiddleware>();
// ...middleware lain sesuai kebutuhan
```

---

## FAQ

**Q: Apakah aplikasi ini bisa digunakan di production?**  
A: Ya, aplikasi ini dirancang untuk digunakan di lingkungan production maupun development.

**Q: Bagaimana cara menambah fitur baru?**  
A: Tambahkan middleware baru di folder `Middleware/` dan daftarkan di pipeline aplikasi.

**Q: Apakah mendukung notifikasi real-time?**  
A: Ya, melalui fitur webhook.

---

## Kontribusi

Kontribusi sangat terbuka!  
Silakan fork repository ini, buat branch baru, dan ajukan pull request.  
Pastikan kode Anda sudah teruji dan mengikuti standar yang ada.

---

## Lisensi

Proyek ini dilisensikan di bawah MIT License.  
Lihat file [LICENSE](LICENSE) untuk detail lebih lanjut.

---

**CSharp Defender** — Perlindungan aplikasi web Anda dari serangan siber secara mudah dan efisien.
