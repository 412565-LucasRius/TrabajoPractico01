async function FetchUserBookings() {

  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token")

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
                      <strong>Sala: </strong> ${booking.tickets[0].showtime.showtimeId}
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
                    <p>__________________________________</p>
                    
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
  const token = localStorage.getItem("JWT-Token");

  if (!userId || !token) {
    console.error("Usuario no autenticado.");
    window.location.href = 'login.html';
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

      document.getElementById("userName").textContent = userData.username
      document.getElementById("userEmail").textContent = userData.email

    }
  } catch (error) {
    console.error("Error al hacer la solicitud:", error);
  }
}

window.addEventListener("DOMContentLoaded", LoadUserProfile)
window.addEventListener("DOMContentLoaded", FetchUserBookings)

// Función para abrir el modal
function openModal() {
  document.getElementById('editUsernameModal').style.display = 'block';
}

// Función para cerrar el modal
function closeModal() {
  document.getElementById('editUsernameModal').style.display = 'none';
}

// Función para abrir el modal al hacer clic en el botón de "Actualizar Información"
document.querySelector('.update-button').addEventListener('click', openModal);


async function UpdateUserData() {
  const userId = localStorage.getItem("userId");
  const newUsername = document.getElementById('newUsername').value;

  if (!newUsername.trim()) {
    alert("El nombre de usuario no puede estar vacío.");
    return;
  }

  const token = localStorage.getItem("JWT-Token");

  if (!token || !userId) {
    alert("No se pudo encontrar el token o el ID del usuario.");
    console.log(token, userId);

    return;
  }

  const requestBody = {
    userAccountId: userId,
    newUsername: newUsername
  };

  try {
    const response = await fetch(`https://localhost:7276/api/User/update`, {
      method: 'PUT',
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(requestBody)
    });

    if (response.ok) {
      const result = await response.json();
      alert("Nombre de usuario actualizado correctamente");
      document.getElementById('userName').textContent = newUsername;
      document.getElementById('editUsernameModal').style.display = 'none';
    } else {
      const errorData = await response.json();
      alert(`Error: ${errorData.message || 'No se pudo actualizar el nombre de usuario'}`);
    }
  } catch (error) {
    alert("Error al intentar actualizar el nombre de usuario: " + error.message);
  }
}


async function SetUserAsInactive() {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token");

  fetch(`${API_URL}/deactivate?userId=${userId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify({ userId: userId })
  })

  

}