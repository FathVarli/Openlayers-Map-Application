var raster = new ol.layer.Tile({
    source: new ol.source.OSM()
});

var source = new ol.source.Vector({ wrapX: false });

var vector = new ol.layer.Vector({
    source: source,
    style: new ol.style.Style({
        fill: new ol.style.Fill({
            color: 'rgba(255, 255, 255, 0.2)',
        }),
        stroke: new ol.style.Stroke({
            color: '#ffcc33',
            width: 2,
        }),
        image: new ol.style.Circle({
            radius: 7,
            fill: new ol.style.Fill({
                color: '#ffcc33',
            }),
        }),
    }),
});

var mousePositionControl = new ol.control.MousePosition({
    coordinateFormat: ol.coordinate.createStringXY(4),
    projection: 'EPSG:4326',
    className: 'custom-mouse-position',
    target: document.getElementById('mouse-position'),
    undefinedHTML: '&nbsp;'
});

var map = new ol.Map({
    controls: ol.control.defaults({
        attributionOptions: ({
            collapsible: false
        })
    }).extend([mousePositionControl]),
    layers: [raster, vector],
    target: 'map',
    view: new ol.View({
        center: ol.proj.fromLonLat([35.96, 39.03]),
        zoom: 7
    })

});

var modify = new ol.interaction.Modify({ source: source });
map.addInteraction(modify);

var draw, snap; 
var typeSelect = document.getElementById('type');
function addInteractions() {
    draw = new ol.interaction.Draw({
        source: source,
        type: typeSelect.value,
    });
    map.addInteraction(draw);
    snap = new ol.interaction.Snap({ source: source });
    map.addInteraction(snap);
}

typeSelect.onchange = function () {
    map.removeInteraction(draw);
    map.removeInteraction(snap);
    addInteractions();
};

addInteractions();

var projectionSelect = $('#projection');
projectionSelect.on('change', function () {
    mousePositionControl.setProjection(ol.proj.get(this.value));
});
projectionSelect.val(mousePositionControl.getProjection().getCode());

var precisionInput = $('#precision');
precisionInput.on('change', function () {
    var format = ol.coordinate.createStringXY(this.valueAsNumber);
    mousePositionControl.setCoordinateFormat(format);
});





