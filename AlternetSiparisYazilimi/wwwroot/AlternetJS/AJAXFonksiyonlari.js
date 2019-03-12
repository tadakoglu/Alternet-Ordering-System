//KATEGORİ FİLTRELEME JQUERY AJAX İSTEKLERİ, ONCHANGE >> LOAD
$(document).ready(function () {
    $("#Kategori").change(function () {
        var kategori = $("#Kategori").val();
        kategori = kategori.replace(/\s+/g, '%20') // Bu ifade boşlukları %20 ile değiştirir. Aksi halde boşluklu olarak bir değişken ile istek(query) yapıldığında( Soğuk Algınlığı gibi) dönüşüm JQUERY ile otomatik yapılmıyor.
        //alert(kategori); // Test et.
        $("#urunListesi").html('<div class="YukleDiv"><img class="yukleniyorGif" src="yukleniyor.gif"/><span class="yukleniyorYazisi">Yükleniyor</span></div>').load('@(Url.Action("JQueryAjaxListele", "Urun"))?Kategori=' + kategori);
        //Tüm ürünler seçilidğinde "" null değeri gönderilir ve bu Controller(ya da Web API) fonksiyonları tarafından algılanarak tüm ürünler olarak sorgu yapılır.
    });
});
//SAYFALAYICI JQUERY AJAX İSTEKLERİ ONCLICK >> LOAD
//@* $(document).ready(function () {
//    $(".SayfaLinki").click(function () {
//        var sayfa = $(this).html();
//        // sayfa = sayfa.replace(/\s+/g, '%20') // Bu ifade boşlukları %20 ile değiştirir. Aksi halde boşluk olarak istek yapıldığında dönüşüm JQUERY ile otomatik yapılmıyor.
//        alert(sayfa); // Test et.
//        $("#urunListesi").load('@(Url.Action("JQueryAjaxListele", "Urun"))?SayfaNo=' + sayfa);
//        //Tüm ürünler seçilidğinde "" null değeri gönderilir ve bu Controller(ya da Web API) fonksiyonları tarafından algılanarak tüm ürünler olarak sorgu yapılır.
//    });
//});*@

             //SAYFALAYICI VERSIYON 2 JQUERY AJAX İSTEKLERİ ONCLICK >> LOAD
            //SayfalayiciHTMLRendererHelper sınıfı ÜZERİNDE  yeniLink.Attributes.Add("onclick", "SayfayaGit("+sayfa+")"); İLE BAŞARILDI

                var SayfayaGit = function (sayfaNo, kategori) {
    kategori = kategori.replace(/\s+/g, '%20') // Bu ifade boşlukları %20 ile değiştirir. Aksi halde boşluk olarak istek yapıldığında dönüşüm JQUERY ile otomatik yapılmadığı için boş sonuç döner.
    //alert(kategori); // Test et. Başarılı
    $("#urunListesi").html('<div class="YukleDiv"><img class="yukleniyorGif" src="yukleniyor.gif"/><span class="yukleniyorYazisi">Yükleniyor</span></div>').load('@(Url.Action("JQueryAjaxListele", "Urun"))?Kategori=' + kategori + '&SayfaNo=' + sayfaNo);
    //Tüm ürünler seçilidğinde "" null değeri gönderilir ve bu Controller(ya da Web API) fonksiyonları tarafından algılanarak tüm ürünler olarak sorgu yapılır.
}



//ARAMA KUTUCUĞU JQUERY AJAX INPUT EVENT(herhangi bir değer girilmesi VE DE SİLİNMESİ DAHİL) >> LOAD

$(document).ready(function () {
    $("#aramaKutucugu").bind('input', function () {
        var aranacakDeger = $("#aramaKutucugu").val();
        // sayfa = sayfa.replace(/\s+/g, '%20') // Bu ifade boşlukları %20 ile değiştirir. Aksi halde boşluk olarak istek yapıldığında dönüşüm JQUERY ile otomatik yapılmıyor.

        $("#urunListesi").html('<div class="YukleDiv"><img class="yukleniyorGif" src="yukleniyor.gif"/><span class="yukleniyorYazisi">Yükleniyor</span></div>').load('@(Url.Action("JQueryAjaxListele", "Urun"))?Arama=' + aranacakDeger);

    });


});