<%@ Page Title="Contact The Ranger" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="The_Ranger.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-hero">
        <div class="container">
            <div class="page-kicker">THE RANGER SUPPORT</div>
            <h1 class="page-title">Need a hand with your safari?</h1>
            <p class="mb-0 opacity-75">Use the right part of The Ranger for your next step.</p>
        </div>
    </section>

    <section class="page-shell">
        <div class="container">
            <div class="row g-4 justify-content-center">
                <div class="col-md-6 col-xl-4">
                    <div class="surface p-4 h-100">
                        <div class="feature-icon mb-3"><i class="fa-solid fa-calendar-check"></i></div>
                        <h2 class="font-display h4">Already booked?</h2>
                        <p class="muted">Sign in to view your accommodation and game-drive reservations, check their status or cancel an eligible booking.</p>
                        <a href="Login.aspx" class="btn btn-ranger">View My Bookings</a>
                    </div>
                </div>
                <div class="col-md-6 col-xl-4">
                    <div class="surface p-4 h-100">
                        <div class="feature-icon mb-3"><i class="fa-solid fa-house"></i></div>
                        <h2 class="font-display h4">Looking for a stay?</h2>
                        <p class="muted">Browse the accommodation currently available in the system and choose a stay that suits your group.</p>
                        <a href="Accommodation.aspx" class="btn btn-ranger">Browse Accommodation</a>
                    </div>
                </div>
                <div class="col-md-6 col-xl-4">
                    <div class="surface p-4 h-100">
                        <div class="feature-icon mb-3"><i class="fa-solid fa-binoculars"></i></div>
                        <h2 class="font-display h4">Ready for a drive?</h2>
                        <p class="muted">Explore the available game drives, choose a date and reserve seats directly through The Ranger.</p>
                        <a href="GameDrives.aspx" class="btn btn-ranger">Browse Game Drives</a>
                    </div>
                </div>
            </div>

            <div class="surface p-4 p-lg-5 mt-5 text-center">
                <div class="page-kicker">OWNER SUPPORT</div>
                <h2 class="font-display">Manage your safari inventory</h2>
                <p class="muted mb-4">Owners can sign in to manage accommodation, game drives and guest booking statuses.</p>
                <a href="Login.aspx" class="btn btn-outline-dark rounded-pill px-4">Owner Sign In</a>
            </div>
        </div>
    </section>
</asp:Content>
