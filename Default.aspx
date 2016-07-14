<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1 style="text-align:center">Happy Hungry</h1><br/>
        <h3>
            Do you ever get so overwhelmed with emotion that you can't decide what you want to eat? Look no further! 
            Upload a picture of your face to determine your current mood and get a suggestion of what type of food you're ~really~ craving.
        </h3>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Upload Your Photo</h2>
            <p>
                Please upload a photo of your current facial expression. Must be a JPEG, PNG, GIF, or BMP less than 4GB.
            </p><br>

            <asp:FileUpload ID="EmotionPhoto" runat="server"/><br> 
            <asp:Button ID="ShowResultsBttn" Text="Show Results" runat="server"/><br>
        </div>
        <div class="col-md-6">
            <h2>Results</h2>
            <p>
                Based on your image, you're craving a...
            </p><br>
        </div>
    </div>
</asp:Content>
