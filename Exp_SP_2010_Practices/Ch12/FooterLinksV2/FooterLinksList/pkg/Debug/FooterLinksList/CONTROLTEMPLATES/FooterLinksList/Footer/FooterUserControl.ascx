<%@ Assembly Name="FooterLinksList, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fb9b0d6fb3385040" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterUserControl.ascx.cs" Inherits="FooterLinksList.Footer.FooterUserControl" %>

<SharePoint:CssRegistration ID="CssRegistration1" Name="/_layouts/City of Lethbridge/css/Footer.css" runat="server" />

<div class="footerControl">
    <asp:Repeater ID="FooterList" runat="server">
        <ItemTemplate>
            <a href='<%# Eval("Url") %>'><%# Eval("Title") %></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
