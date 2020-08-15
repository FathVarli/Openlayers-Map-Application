var raster = new ol.layer.Tile({
    source: new ol.source.OSM()
});

var source = new ol.source.Vector({ wrapX: false });

var vector = new ol.layer.Vector({
    source: source
});

var kapi_layer = new ol.layer.Vector({
    source: new ol.source.Vector()
});

var map = new ol.Map({

    layers: [raster, vector, kapi_layer],
    target: 'map',
    view: new ol.View({
        center: [28.96, 41.03],
        zoom: 4,
        projection: "EPSG:4326"

    })

});
//buraya kadar olan kodların neden yazıldığını, ne işe yaradığını önceki yazımda anlatmıştım.
var kapi;
function addKapiInteraction() {
    kapi = new ol.interaction.Draw({
        source: source,
        type: 'Point'
    });

    map.addInteraction(kapi);
    kapi.setActive(false);

}

document.getElementById('btn1').onclick = function () {
    kapi.setActive(true);
}

addKapiInteraction();

var mahalle_ciz;
function addDaireInteraction() {
    mahalle_ciz = new ol.interaction.Draw({
        source: source,
        type: 'Polygon'
    });

    map.addInteraction(mahalle_ciz);
    mahalle_ciz.setActive(false);

}

document.getElementById('btn2').onclick = function () {
    mahalle_ciz.setActive(true);
}

addDaireInteraction();

kapi.on('drawend', function (e) {
    //point atıldıktan sonra yapılacak işler bu scope de yer almalı
    var currentFeature = e.feature;

    var _coords = currentFeature.getGeometry().getCoordinates();
    kapi.setActive(false);

    var js = jsPanel.create({
        id: "kapi_ekle_panel",
        theme: 'success',
        headerTitle: 'kapı ekle',
        position: 'center-top 0 58',
        contentSize: '300 250',
        content:
            'No: <input id="kapi_no" type="text"/><br><br><br><button style="height:40px;width:60px" id="kapi_kaydet" class="btn btn-success">Ekle</button>',
        callback: function () {
            this.content.style.padding = '20px';
        }
    });
    document.getElementById('kapi_kaydet').onclick = function () {

        var _no = $('#kapi_no').val();

        if (_no.length < 1) {

            alert("Kapı Numarası Girmediniz");

            return;
        }
        //kapının kordinatlarını x ve y değişkenlerine attım
        var _data = {
            x: _coords[0].toString().replace('.', ','),
            y: _coords[1].toString().replace('.', ','),
            no: _no
        };

        $.ajax({
            type: "POST",
            url: "/Map/SavePoint",
            dataType: 'json',
            data: _data,
            success: function () {
                alert("Başarıyla Eklendi");
                js.close();
                kapi.setActive(false);
            },

            error: function () {
                alert("Hata Oluştu");
            },
            onbeforeclose: function () {
                return onbeforeclose();
            }
        });
    };
});



function ListAllPoints() {

    $.ajax({
        type: "GET",
        url: "/Map/GetAllPoint",
        dataType: 'json',
        success: function (response) {

            var _features = [];

            for (var i = 0; i < response.length; i++) {

                //her bir pointin x,y koordinatlarını aldım.

                var _point = response[i];
                var _id = _point.Id
                var _geo = new ol.geom.Point([_point.x, _point.y]);

                var featurething = new ol.Feature({
                    name: "Kapı",
                    geometry: _geo,

                });

                featurething.setId(_id)

                //feature oluşutup buna noktaları atadım ve style verdim

                var _style = new ol.style.Style({
                    image: new ol.style.Circle({
                        fill: new ol.style.Fill({
                            color: 'rgba(0,0,255,0.3)'
                        }),
                        stroke: new ol.style.Stroke({
                            color: '#8000ff'
                        }),
                        radius: 10
                    }),
                });

                featurething.setStyle(_style);

                _features.push(featurething);
            }
            //oluşturduğum style ı feature a set ettım ve featuring nesnemi de  boş olan _features listesine attım.
            //layer ve source olayını daha önce vurgulamıstım yazdıgım ve elde ettıgım feature harita kaynağına (source) atılmazsa 
            //dataları map üstünde göremeyiz

            var _pointSource = kapi_layer.getSource();

            _pointSource.addFeatures(_features);
        },

        error: function () {
            alert("upsss");
        },

    });
};

