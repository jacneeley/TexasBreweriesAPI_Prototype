<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapPage.aspx.cs" Inherits="Breweries.MapPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src='https://api.mapbox.com/mapbox-gl-js/v2.8.1/mapbox-gl.js'></script>
    <link href='https://api.mapbox.com/mapbox-gl-js/v2.8.1/mapbox-gl.css' rel='stylesheet' />
    <script src="https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-directions/v4.1.0/mapbox-gl-directions.js"></script>
<link rel="stylesheet" href="https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-directions/v4.1.0/mapbox-gl-directions.css" type="text/css">
    <title>Brew Map</title>
    <style>
        #map
        {
            height: 85vh;
            width: 85vh;
        }
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <h3>Brew Map</h3>
    <form id="form1" runat="server">
        <p>
            <asp:Button ID="Button1" runat="server" PostBackUrl="~/WebForm1.aspx" Text="Return" />
        </p>
    <div id='map'></div>
<script>
mapboxgl.accessToken = 'pk.eyJ1IjoiamFjbmVlbGV5IiwiYSI6ImNsMmpwbWRyYTA2bDgzZW8zZnRmbGo0ZGIifQ.hRiBH4zifZodxJFPQrQ1_g';
    var map = new mapboxgl.Map
        ({
container: 'map',
    style: 'mapbox://styles/mapbox/streets-v11',
    center: { lng: -100.07684, lat:31.168934999999998  },
    zoom:5  
        });
    var directions = new MapboxDirections({
  accessToken: 'pk.eyJ1IjoiamFjbmVlbGV5IiwiYSI6ImNsMmpwbWRyYTA2bDgzZW8zZnRmbGo0ZGIifQ.hRiBH4zifZodxJFPQrQ1_g',
});

map.addControl(directions, 'top-left');
</script>
        <div>
        </div>
    </form>
</body>
</html>
