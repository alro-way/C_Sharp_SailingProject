Website "SV WalkAbout"

Version 0.0
This is personal sailer's travel blog in the form of Landing page.
Features:
1) Landing page with nice looking design, JS and Bootstrap's features;
2) Private Admin page;
3) Google map that shows current location of a sailer 
(or iframe tag that hold link to maps.findmespot.com to show location)
4) Contact me - will send message to the sailer's email
5) Model Route (later will be more models) that allow Admin to edit Route section


Before run code add this (but it should still run without this):
1) Uncomment Registration section in AdminIndex.cshtml
2) Link to Admin registration will be: http://localhost:5000/adminH49L0LWow239
3) Uncomment Google map script in _Layout.cshtml
and place your API_key in the link: https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&callback=initMap
4) To show Map with location from maps.findmespot.com - uncomment Map#2 in _Layout.cshtml
and place your shared link: src="https://share.findmespot.com/shared/faces/viewspots.js_YOUR_KEy_HERE"
5) Place your email address and password in MailSenderController.cs inside this lines:
var credentials = new NetworkCredential("YOUR_EMAIL", "YOUR_PASSWORD");
From = new MailAddress("YOUR_EMAIL"),
mail.To.Add(new MailAddress("YOUR_EMAIL"));

Project was made during 3 days by Justin Alexander and Aleksandra Gribtsova, February 2020.