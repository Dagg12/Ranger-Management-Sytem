<%@ Page Title="Book Game Drive" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GameDriveBooking.aspx.cs" Inherits="TheRanger.GameDriveBooking" %><asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server"><section class="page-shell"><div class="container"><asp:Panel ID="pnlLogin" runat="server" Visible="false" CssClass="surface p-5 text-center"><h2 class="font-display">Sign in to book</h2><p class="muted">Client accounts are required for bookings.</p><a href="Login.aspx" class="btn btn-ranger">Sign in</a><a href="Register.aspx" class="btn btn-outline-dark rounded-pill ms-2">Register</a></asp:Panel><asp:Panel ID="pnlBooking" runat="server"><div class="row g-5"><div class="col-lg-6"><div class="surface overflow-hidden"><div style="height:360px;background-image:url('<%= ImageUrl %>');background-position:center;background-size:cover"></div><div class="p-4"><span class="badge-soft"><asp:Literal ID="litLocation" runat="server" /></span><h1 class="font-display mt-3"><asp:Literal ID="litName" runat="server" /></h1><p class="muted"><asp:Literal ID="litDescription" runat="server" /></p><div class="d-flex justify-content-between"><span class="price">R <asp:Literal ID="litPrice" runat="server" /></span><span class="muted"><asp:Literal ID="litDuration" runat="server" /> hours · up to <asp:Literal ID="litGuests" runat="server" /> guests</span></div></div></div></div><div class="col-lg-5 offset-lg-1"><div class="surface p-4 p-lg-5"><div class="page-kicker">RESERVE YOUR DRIVE</div><h2 class="font-display mb-4">Drive details</h2><asp:Label ID="lblMessage" runat="server" CssClass="d-block mb-3"></asp:Label><label class="form-label">Drive date</label><asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control mb-3"></asp:TextBox><label class="form-label">Guests</label><asp:TextBox ID="txtGuests" runat="server" TextMode="Number" CssClass="form-control mb-4"></asp:TextBox><div class="alert-ranger p-3 mb-4"><small>Total is calculated automatically from guests × drive price.</small><div class="fw-bold mt-1">Estimated total: R <span id="estimatedDriveTotal"><asp:Literal ID="litTotal" runat="server">0.00</asp:Literal></span></div></div><asp:Button ID="btnBook" runat="server" Text="Confirm drive booking" CssClass="btn btn-ranger w-100" OnClick="btnBook_Click" /></div></div></div></asp:Panel></div></section><script type="text/javascript">
(function () {
    function wireDriveTotal() {
        var price = parseFloat('<%= Price.ToString(System.Globalization.CultureInfo.InvariantCulture) %>') || 0;
        var guests = document.getElementById('<%= txtGuests.ClientID %>');
        var total = document.getElementById('estimatedDriveTotal');
        if (!guests || !total) return;
        function update() {
            var count = parseInt(guests.value, 10) || 0;
            total.textContent = (count * price).toLocaleString('en-ZA', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        guests.addEventListener('input', update);
        guests.addEventListener('change', update);
        update();
    }
    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', wireDriveTotal);
    else wireDriveTotal();
})();
</script></asp:Content>
