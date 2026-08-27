<%@ Page Title="Book Accommodation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccommodationBooking.aspx.cs" Inherits="TheRanger.AccommodationBooking" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server"><section class="page-shell"><div class="container"><asp:Panel ID="pnlLogin" runat="server" Visible="false" CssClass="surface p-5 text-center"><h2 class="font-display">Sign in to book</h2><p class="muted">Create a client account or sign in before making a reservation.</p><a href="Login.aspx" class="btn btn-ranger">Sign in</a><a href="Register.aspx" class="btn btn-outline-dark rounded-pill ms-2">Register</a></asp:Panel><asp:Panel ID="pnlBooking" runat="server"><div class="row g-5"><div class="col-lg-6"><div class="surface overflow-hidden"><div style="height:360px;background-image:url('<%= ImageUrl %>');background-position:center;background-size:cover"></div><div class="p-4"><span class="badge-soft"><asp:Literal ID="litLocation" runat="server" /></span><h1 class="font-display mt-3"><asp:Literal ID="litName" runat="server" /></h1><p class="muted"><asp:Literal ID="litDescription" runat="server" /></p><div class="d-flex justify-content-between"><span class="price">R <asp:Literal ID="litPrice" runat="server" /></span><span class="muted">Up to <asp:Literal ID="litGuests" runat="server" /> guests</span></div></div></div></div><div class="col-lg-5 offset-lg-1"><div class="surface p-4 p-lg-5"><div class="page-kicker">RESERVE YOUR STAY</div><h2 class="font-display mb-4">Booking details</h2><asp:Label ID="lblMessage" runat="server" CssClass="d-block mb-3"></asp:Label><label class="form-label">Check-in</label><asp:TextBox ID="txtCheckIn" runat="server" TextMode="Date" CssClass="form-control mb-3"></asp:TextBox><label class="form-label">Check-out</label><asp:TextBox ID="txtCheckOut" runat="server" TextMode="Date" CssClass="form-control mb-3"></asp:TextBox><label class="form-label">Guests</label><asp:TextBox ID="txtGuests" runat="server" TextMode="Number" CssClass="form-control mb-4"></asp:TextBox><div class="alert-ranger p-3 mb-4"><small>Total is calculated automatically from nights × nightly rate.</small><div class="fw-bold mt-1">Estimated total: R <span id="estimatedAccommodationTotal"><asp:Literal ID="litTotal" runat="server">0.00</asp:Literal></span></div></div><asp:Button ID="btnBook" runat="server" Text="Confirm booking" CssClass="btn btn-ranger w-100" OnClick="btnBook_Click" /></div></div></div></asp:Panel></div></section><script type="text/javascript">
(function () {
    function wireBookingTotal() {
        var price = parseFloat('<%= Price.ToString(System.Globalization.CultureInfo.InvariantCulture) %>') || 0;
        var checkIn = document.getElementById('<%= txtCheckIn.ClientID %>');
        var checkOut = document.getElementById('<%= txtCheckOut.ClientID %>');
        var total = document.getElementById('estimatedAccommodationTotal');
        if (!checkIn || !checkOut || !total) return;
        var today = new Date();
        var yyyy = today.getFullYear();
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var dd = String(today.getDate()).padStart(2, '0');
        var minDate = yyyy + '-' + mm + '-' + dd;
        checkIn.min = minDate;
        checkOut.min = minDate;
        function update() {
            if (checkIn.value) checkOut.min = checkIn.value;
            var start = new Date(checkIn.value);
            var end = new Date(checkOut.value);
            if (!isNaN(start) && !isNaN(end) && end > start) {
                var nights = Math.round((end - start) / 86400000);
                total.textContent = (nights * price).toLocaleString('en-ZA', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            } else {
                total.textContent = '0.00';
            }
        }
        checkIn.addEventListener('change', update);
        checkOut.addEventListener('change', update);
        update();
    }
    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', wireBookingTotal);
    else wireBookingTotal();
})();
</script></asp:Content>
