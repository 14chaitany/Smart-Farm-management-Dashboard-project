<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userhome.aspx.cs" Inherits="CropPrediction.Userhome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agro Harvest Agriculture Category Flat Bootstrap Responsive Web Template | Contact :: w3layouts</title>
<!-- for-mobile-apps -->
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta charset="utf-8">
<meta name="keywords" content="Agro Harvest Responsive web template, Bootstrap Web Templates, Flat Web Templates, Android Compatible web template, 
Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />

    <script>
        addEventListener("load", function () {
            setTimeout(hideURLbar, 0);
        }, false);

        function hideURLbar() {
            window.scrollTo(0, 1);
        }
    </script>
	
	<!-- css files -->
    <link href="css/bootstrap.css" rel='stylesheet' type='text/css' /><!-- bootstrap css -->
    <link href="css/style.css" rel='stylesheet' type='text/css' /><!-- custom css -->
    <link href="css/font-awesome.min.css" rel="stylesheet"><!-- fontawesome css -->
	<!-- //css files -->
	
	<link href="css/css_slider.css" type="text/css" rel="stylesheet" media="all">

	<!-- google fonts -->
	<link href="//fonts.googleapis.com/css?family=Thasadith:400,400i,700,700i&amp;subset=latin-ext,thai,vietnamese" rel="stylesheet">
	<!-- //google fonts -->












    
          <style type="text/css">
#map_canvas { height: 100% }
</style>
   


    
<script type="text/javascript" src="https://www.google.com/jsapi"></script> 




     <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">  
    </script>  
  
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places">  
    </script>  
  
    <script type="text/javascript">  
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success);
        } else {
            alert("There is Some Problem on your current browser to get Geo Location!");
        }

        function success(position) {
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
            document.getElementById('lat').value = lat;
            document.getElementById('lon').value = long;
            var city = position.coords.locality;
            var LatLng = new google.maps.LatLng(lat, long);
            var mapOptions = {
                center: LatLng,
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var map = new google.maps.Map(document.getElementById("MyMapLOC"), mapOptions);
            var marker = new google.maps.Marker({
                position: LatLng,
                title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: "
                    + lat + +"<br />Longitude: " + long
            });

            marker.setMap(map);
            var getInfoWindow = new google.maps.InfoWindow({
                content: "<b>Your Current Location</b><br/> Latitude:" +
                    lat + "<br /> Longitude:" + long + ""
            });
            getInfoWindow.open(map, marker);
        }
    </script>  











</head>
<body>
    <header>
	<div class="container">
		<!-- nav -->
		<nav class="py-4 d-lg-flex">
			<div id="logo">
				<h1> <a href="index.html"><span class="fa fa-leaf"></span> Agro Harvest</a></h1>
			</div>
			<label for="drop" class="toggle"><span class="fa fa-bars"></span></label>
			<input type="checkbox" id="drop" />
			<ul class="menu mt-md-2 ml-auto">
				<li class="mr-lg-4 mr-2 active"><a href="Userhome.aspx">Home</a></li>
				<li class="mr-lg-4 mr-2"><a href="login.aspx">Logout</a></li>
				<%--<li class="mr-lg-4 mr-2"><a href="login.aspx">Login Here</a></li>--%>
				<%--<li class="mr-lg-4 mr-2"><a href="comingsoon.html">Gallery</a></li>
				<li class="mr-lg-4 mr-2"><a href="contact.html">Contact</a></li>--%>
				<%--<li class="mr-lg-4 mr-2"><span><span class="fa fa-phone"></span> +12 345 6789</span></li>--%>
			</ul>
		</nav>
		<!-- //nav -->
	</div>
</header>
    <section class="banner_w3lspvt" id="home">
	<div class="csslider infinity" id="slider1">
		<%--<input type="radio" name="slides" checked="checked" id="slides_1" />
		<input type="radio" name="slides" id="slides_2" />
		<input type="radio" name="slides" id="slides_3" />
		<input type="radio" name="slides" id="slides_4" />--%>
		<ul>
			<li>
				<div class="banner-top" style= "min-height:90px;">
					
				</div>
			</li>
			<%--<li>
				<div class="banner-top1">
					<div class="overlay1">
						<div class="container">
							<div class="w3layouts-banner-info text-center">
								<h3 class="text-wh">Farming</h3>
								<h4 class="text-wh mx-auto my-4">Cultivating new crops to make farmers increase profits</h4>
								<%--<p class="text-li mx-auto mt-2">Ut enim ad minim quis nostrud exerci sed do eiusmod tempor incididunt ut
									labore et dolore magna aliqua nostrud exerci sed.</p>
								<a href="about.html" class="button-style mt-sm-5 mt-4">Read More</a>
							</div>
						</div>
					</div>
				</div>
			</li>--%>
			<%--<li>
				<div class="banner-top2">
					<div class="overlay">
						<div class="container">
							<div class="w3layouts-banner-info text-center">
								<h3 class="text-wh">Cultivating</h3>
								<h4 class="text-wh mx-auto my-4">Cultivating new crops to make farmers increase profits</h4>
								<%--<p class="text-li mx-auto mt-2">Ut enim ad minim quis nostrud exerci sed do eiusmod tempor incididunt ut
									labore et dolore magna aliqua nostrud exerci sed.</p>
								<a href="about.html" class="button-style mt-sm-5 mt-4">Read More</a>
							</div>
						</div>
					</div>
				</div>
			</li>--%>
			<%--<li>
				<div class="banner-top3">
					<div class="overlay1">
						<div class="container">
							<div class="w3layouts-banner-info text-center">
								<h3 class="text-wh">Harvesting</h3>
								<h4 class="text-wh mx-auto my-4">Cultivating new crops to make farmers increase profits</h4>
								<%--<p class="text-li mx-auto mt-2">Ut enim ad minim quis nostrud exerci sed do eiusmod tempor incididunt ut
									labore et dolore magna aliqua nostrud exerci sed.</p>
								<a href="about.html" class="button-style mt-sm-5 mt-4">Read More</a>
							</div>
						</div>
					</div>
				</div>
			</li>--%>
		</ul>
		
	</div>
</section>
   <section class="contact py-5">
	<div class="container py-sm-3">
		<h3 class="heading mb-sm-5 mb-4 text-center"> Prediction Here</h3>
		<%--<div class="row map-pos">
			<div class="col-lg-4 col-md-6 address-row">
				<div class="row">
					<div class="col-2 address-left">
						<div class="contact-icon">
							<span class="fa fa-home" aria-hidden="true"></span>
						</div>
					</div>
					<div class="col-10 address-right">
						<h5>Visit Us</h5>
						<p>Agriculture Business, 2nd Block, Farm land, USA.</p>
					</div>
				</div>
			</div>
			<div class="col-lg-4 col-md-6 address-row w3-agileits">
				<div class="row">
					<div class="col-2 address-left">
						<div class="contact-icon">
							<span class="fa fa-envelope" aria-hidden="true"></span>
						</div>
					</div>
					<div class="col-10 address-right">
						<h5>Mail Us</h5>
						<p><a href="mailto:info@example.com">Example@gmail.com</a></p>
					</div>
				</div>
			</div>
			<div class="col-lg-4 col-md-6 address-row">
				<div class="row">
					<div class="col-2 address-left">
						<div class="contact-icon">
							<span class="fa fa-phone" aria-hidden="true"></span>
						</div>
					</div>
					<div class="col-10 address-right">
						<h5>Call Us</h5>
						<p>+12(345) 6789 111</p>
					</div>
				</div>
			</div>
		</div>--%>
		<form id="form2" runat="server">
            <div class="col-md-12">
                 <div id="MyMapLOC" style="width: 100%; height: 400px">  </div>
            </div>
			<div class="row">
                <div class="col-md-4 contact-left"></div>
				<div class="col-md-4 contact-right mt-md-0 mt-4">
					





                    
                    <br />

					<%--<div id="map_canvas" style="width: 100%; height: 300px"></div>--%>
                    <input runat="server" type="text" id="Location" placeholder="Location" readonly/>
                    <input runat="server" type="text" id="lat" />
                    <input runat="server" type="text" id="lon" />
                    <input runat="server" type="text" placeholder="Land in Acres" id="land"/>
					
                  <%--  
                    <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Enabled="true" Text="Select Types of soil" Value="0"></asp:ListItem>                                    
                                </asp:DropDownList> --%>
					<input runat="server" type="text" id="phvalue" placeholder="Soil pH value"/>
                   <asp:Button class="btn" ID="Button1" runat="server" Text="Analytics" OnClick="send" OnClientClick="return validation();"/>
				</div>
				<div class="col-md-4 ">
                    
				</div>
			</div>
            <asp:Panel ID="Panel1" runat="server" Style="padding-top:50px;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive"
                    CellPadding="4"  EmptyDataText="There are no data records to display." ForeColor="#C9A974"  GridLines="None" Width="100%"   EnableModelValidation="True">
                             <Columns>
                         
                            <asp:BoundField DataField="Crop" HeaderText="Crop Name" 
                            SortExpression="Crop" />    
                            <asp:BoundField DataField="Investment" HeaderText="Investment Per Hectare" 
                            SortExpression="Investment" /> 
                            <asp:BoundField DataField="Profit" HeaderText="Profit" 
                            SortExpression="Profit" />    
                            <asp:BoundField DataField="SeedsPerAcre" HeaderText="Seeds Per Acre" 
                            SortExpression="SeedsPerAcre" />      
                            <asp:BoundField DataField="FertilizersPerAcre" HeaderText="Fertilizers Per Acre" 
                            SortExpression="FertilizersPerAcre" />     
                            <asp:BoundField DataField="ApproxInvestment" HeaderText="Approx Investment" 
                            SortExpression="ApproxInvestment" />
                    </Columns>
                    <EditRowStyle BackColor="#000" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#009f4d" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#fff" Font-Bold="True" ForeColor="#000" />

                </asp:GridView>
                </asp:Panel>



            
            <asp:Panel ID="Panel2" runat="server" Style="padding-top:50px;">
                <h1>Overall Crop Predicted For Above Query</h1>
                 <asp:Literal ID="ltScripts" runat="server"></asp:Literal>  
                 <div id="chart_div" style="width: 760px; height: 400px;"> </div>


                </asp:Panel>








		</form>
		<!-- map -->
		<%--<div class="map mt-5">
			<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1859251.8642025779!2d-76.08274894689792!3d40.06224332601239!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89c0fb959e00409f%3A0x2cd27b07f83f6d8d!2sNew+Jersey%2C+USA!5e0!3m2!1sen!2sin!4v1474436926209"
			 allowfullscreen></iframe>
		</div>--%>
		<!-- //map -->
	</div>
</section>


  
 
</body>
</html>
