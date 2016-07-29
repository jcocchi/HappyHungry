<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
            <div class="col-md-6">
                <h2>Link Your Photo</h2>
                <p>
                    Please type the URL of a photo you want to use to get your food suggestion.
                </p><br>

                <asp:TextBox ID="EmotionPhoto" runat="server"/><br> 
                <asp:Button ID="ShowResultsBttn" runat="server" OnClick="ShowResultsBttn_Click" Text="Show Results" CssClass="btn btn-default"/><br>
            </div>
        </div>
</asp:Content>
