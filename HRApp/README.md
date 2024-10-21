by Ezgi Çakır

[ENG]
#####Human Relations Management Application


[TUR]
#####Personel Yönetimi Uygulaması

Uygulama mimarisinde 3 rol bulunmaktadır. Bunlar Admin, Company Executive ve Employee rolleridir. 

Kişiler, username (AdSoyad boşuk bırakmadan ya da sadece Ad) email ve parola bilgileri ile sisteme kayıt olurlar. Kayıt olduktan sonra Employee rolündedirler ve şirketleri için başvuru yapabilirler. Admin (Admin kullanıcısı Seed Data ile oluşturulur: admin@example.com, Admin@123), bu başvuruları onaylayarak kişileri Company Executive yapabilir. Admin Ana sayfasında listlenen kişilerin en sonunda "Promote to Exedcutive" yazısına basarak onları firma yetkilisi yapabilir ya da bir kere daha basarak onları tekrar Employee rolüne çevirebilir.
Company Executive'ler ise şirket çalışanlarını ekleyerek giriş yapacakları parolaları email olarak atarlar.

BUG (Sonradan Çözülecek):
Kayıt olmuş kişiler şirket başvurusunda bulunabilirler. Şirket başvurusuna bulunurken iki tane uyarı mesajı çıkmaktadır ve veri kayıt edilmemiş gibi gözükmektedir fakat veri tabanına kayıt edilmektedir. Bu durum ileride düzeltilecektir.

1- Şirketleri adına hesap açmak isteyen çalışanlar hesap oluşturduktan sonra Admin'e başvuruda bulunurlar. Hesap oluştururken parolanın en az 6 karakter uzunluğunda, büyük-küçük harf, sayı ve özel karakter içermesi gerekmektedir.
2- Admin, bu kişileri Company Executive yapabilir ya da yetkilerini geri alabilir.
3- Company Executive'ler şirket çalışanlarını ekleyerek giriş yapacakları parolaları email olarak atarlar. 
4- Atılan emaille yapılan ilk girişten sonra kullanıcılar profil bilgilerini, eğitim, iş tecrübesi ve sertifika bilgilerini girebilirler. Veriler girildikten sonra sayfalarında gözükür. Company Executive rolündeki kişiler, üst menü Management sekmesi Employee Details'i seçerek çalışanların bilgilerini görebilirler.

