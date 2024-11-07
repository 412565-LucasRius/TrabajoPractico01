

async function FetchUserBookings() {

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
      const bookings = await response.json();
      const reservationsList = document.getElementById("reservationsList");
      reservationsList.innerHTML = ""; // Limpiar la lista actual

      // Recorremos las reservas y creamos el HTML dinámicamente
      bookings.forEach(booking => {
        const bookingElement = document.createElement("div");
        bookingElement.classList.add("reservation-item");

        // Crear el contenido de la reserva
        const bookingDetails = `
                    <div>
                        <strong>Fecha de Reserva:</strong> ${new Date(booking.bookingDate).toLocaleDateString()}
                    </div>
                    <div>
                        <strong>Asientos:</strong>
                        <ul>
                            ${booking.tickets.map(ticket => `<li>Asiento ${ticket.seatNumber} - Precio: $${ticket.price}</li>`).join('')}
                        </ul>
                    </div>
                    <div>
                        <strong>Pelicula:</strong> ${booking.tickets[0].showtime.movie.title}
                    </div>
                `;

        bookingElement.innerHTML = bookingDetails;
        reservationsList.appendChild(bookingElement);
      });
    } else {
      console.error("Error al obtener las reservas:", response.statusText);
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
window.addEventListener("DOMContentLoaded", FetchUserBookings)