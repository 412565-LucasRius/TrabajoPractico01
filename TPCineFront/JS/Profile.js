

async function FetchBookings() {

  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token")
  console.log(token, userId);
  

  if (!userId || !token) {
    console.error("Usuario no autenticado.");
    return;
  }

  try {
    const response = await fetch(`https://localhost:7276/api/Booking/GetBookingByUser?userId=${userId}`, {
      method: 'GET',
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    })

    if (response.ok) {
      const userData = await response.json();
      console.log("Reservas del usuario: ", userData);
    }
  } catch (error) {
    console.error("Error al hacer la solicitud:", error);
  }
}

async function LoadUserProfile() {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token")
  console.log(token, userId);

  if (!userId || !token) {
    console.error("Usuario no autenticado.");
    return;
  }

  try {
    const response = await fetch(`https://localhost:7276/api/User/byid?userAccountId=${userId}`, {
      method: 'GET',
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    })

    if (response.ok) {
      const userData = await response.json();
      console.log("Datos del usuario: ", userData);

      document.getElementById("userName").textContent = userData.username
      document.getElementById("userEmail").textContent = userData.email

    }
  } catch (error) {
    console.error("Error al hacer la solicitud:", error);
  }
}

window.addEventListener("DOMContentLoaded", LoadUserProfile)
window.addEventListener("DOMContentLoaded", FetchBookings)