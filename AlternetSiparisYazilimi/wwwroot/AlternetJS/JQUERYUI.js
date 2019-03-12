//Burada JQuery kaynaklı bir sıkıntı var gibi. Tekrar bak.
//$("#aramaKutucugu").autocomplete({
//    source: function (request, response) {
//        $.ajax({
//            url: "/AlternetAjaxApi/AramaKutusuOtomatikTamamla",
//            type: "POST",
//            dataType: "json",
//            data: { OnEk: request.term },
//            success: function (data) {
//                return data;
//            }
//        })
//    },
//    messages: {
//        noResults: "", results: ""
//    }
//});