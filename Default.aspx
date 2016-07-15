<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
            <h2>Link Your Photo</h2>
            <p>
                Please type the URL of a photo of your current facial expression.
            </p><br>

            <asp:TextBox ID="EmotionPhoto" runat="server"/><br> 
            <asp:Button ID="ShowResultsBttn" runat="server" OnClick="ShowResultsBttn_Click" Text="Show Results" CssClass="btn btn-default"/><br>
        </div>
        <div class="col-md-6">
            <h2>Results</h2>
            <p>
                Based on your image, you're craving a...
            </p><br>
        </div>
    </div>
</asp:Content>
