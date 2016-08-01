<%@ Page Title="Results Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeFile="Results.aspx.cs" Inherits="Results" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Results</h2>
    
    <div class="row">
        <div class="col-md-6">
            <p id="PResults" runat="server"></p><br>
            <asp:Image ID="SuggestedFood" Width="500px" runat="server" /><br>

            <asp:Button ID="BackButton" runat="server" OnClick="BackBttn_Click" Text="Back" CssClass="btn btn-default"/><br>
        </div>
    </div>
</asp:Content>