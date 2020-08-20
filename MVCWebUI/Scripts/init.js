var raster = new ol.layer.Tile({
    source: new ol.source.OSM()
});

var source = new ol.source.Vector({ wrapX: false });

var vector = new ol.layer.Vector({
    source: source
});

var kapi_layer = new ol.layer.Vector({
    source: source
});

var styles = {
    'MultiPolygon': new ol.style.Style({
        stroke: new ol.style.Stroke({
            color: 'yellow',
            width: 1
        }),
        fill: new ol.style.Fill({
            color: 'rgba(255, 255, 0, 0.1)'
        })
    }),
};

var styleFunction = function (feature) {
    return styles[feature.getGeometry().getType()];
};


function GetCoordinates() {
    var coordinates;
    $.ajax({
        type: "GET",
        url: "/Map/ListPolygon",
        dataType: 'json',
        async: false,
        success: function (response) {
            coordinates = response;
        },

        error: function () {
            alert("upsss");
        }
    });
    console.log(coordinates);
    return coordinates;
}

var geojsonObject = {

    'type': 'FeatureCollection',
    'crs': {
        'type': 'name',
        'properties': {
            'name': 'EPSG:3857'
        }
    },
    'features': [{
        'type': 'Feature',
        'geometry': {
            'type': 'MultiPolygon',
            'coordinates':
                [
                    GetCoordinates()

                ]
        }
    }]
};

var vectorSource = new ol.source.Vector({
    features: (new ol.format.GeoJSON()).readFeatures(geojsonObject)

});

vectorSource.addFeature(new ol.Feature(new ol.geom.Circle([5e6, 7e6], 1e6)));

var vectorLayer = new ol.layer.Vector({
    source: vectorSource,
    style: styleFunction
});

var map = new ol.Map({
    controls: ol.control.defaults({
        attributionOptions: ({
            collapsible: false
        })
    }),
    layers: [raster, vector, kapi_layer, vectorLayer,],
    target: 'map',
    view: new ol.View({
        center: ol.proj.fromLonLat([35.96, 39.03]),
        zoom: 7,
        projection: 'EPSG:3857'
    })

});

document.getElementById('btn3').onclick = function () {
    $.ajax({
        type: "GET",
        url: "/Map/ListPoint",
        dataType: 'json',
        success: function (response) {

            var _features = [];

            for (var i = 0; i < response.length; i++) {

                //her bir pointin x,y koordinatlarını aldım.

                var _point = response[i];
                var _id = _point.Id;
                var _geo = new ol.geom.Point([_point.x, _point.y]);

                var featurething = new ol.Feature({
                    name: "Kapı",
                    geometry: _geo

                });

                featurething.setId(_id);

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
                    })
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
};

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
};

addDaireInteraction();

mahalle_ciz.on('drawend', function (e) {
    //point atıldıktan sonra yapılacak işler bu scope de yer almalı
    var currentFeature = e.feature;

    var _coords = currentFeature.getGeometry().getCoordinates();
    mahalle_ciz.setActive(false);

    var js = jsPanel.create({
        id: "mahalle_ciz_panel",
        theme: 'success',
        headerTitle: 'mahalle ekle',
        position: 'center-top 0 58',
        contentSize: '300 250',
        content:
            'No: <input id="mahalle_no" type="text"/><br><br><br><button style="height:40px;width:60px" id="mahalle_kaydet" class="btn btn-success">Ekle</button>',
        callback: function () {
            this.content.style.padding = '20px';
        }
    });
    document.getElementById('mahalle_kaydet').onclick = function () {

        var _no = $('#mahalle_no').val();

        if (_no.length < 1) {

            alert("Mahalle Numarası Girmediniz");

            return;
        }
        //kapının kordinatlarını x ve y değişkenlerine attım
        var _data = {
            coordinatesArr: _coords[0],
            no: _no
        };

        $.ajax({
            type: "POST",
            url: "/Map/SavePolygon",
            dataType: 'json',
            data: _data,
            success: function () {
                alert("Başarıyla Eklendi");
                js.close();
                mahalle_ciz.setActive(false);
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

var features = new ol.Collection();
var featureOverlay = new ol.layer.Vector({
    source: new ol.source.Vector({ features: features }),
    style: new ol.style.Style({
        fill: new ol.style.Fill({
            color: 'rgba(255, 255, 255, 0.2)'
        }),
        stroke: new ol.style.Stroke({
            color: '#ffcc33',
            width: 2
        }),
        image: new ol.style.Circle({
            radius: 7,
            fill: new ol.style.Fill({
                color: '#ffcc33'
            })
        })
    })
});
featureOverlay.setMap(map);

var modify = new ol.interaction.Modify({
    features: features,
    // the SHIFT key must be pressed to delete vertices, so
    // that new vertices can be drawn at the same position
    // of existing vertices
    deleteCondition: function (event) {
        return ol.events.condition.shiftKeyOnly(event) &&
            ol.events.condition.singleClick(event);
    }
});
map.addInteraction(modify);

var draw; // global so we can remove it later
var typeSelect = document.getElementById('type');

function addInteraction() {
    draw = new ol.interaction.Draw({
        features: features,
        type: /** @type {ol.geom.GeometryType} */ (typeSelect.value)
    });
    map.addInteraction(draw);
}


/**
 * Handle change event.
 */
typeSelect.onchange = function () {
    map.removeInteraction(draw);
    addInteraction();
};

addInteraction();






